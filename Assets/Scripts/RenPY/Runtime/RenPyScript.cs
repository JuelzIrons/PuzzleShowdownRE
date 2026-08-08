using System;
using System.Collections.Generic;
using RenPy.Ast;
using RenPy.Pickle;
using RenPy.Python;

namespace RenPy.Runtime
{
    /// <summary>
    /// The loaded game script: every statement from every .rpyc, indexed the way
    /// the engine needs to run it.
    /// </summary>
    public class RenPyScript
    {
        public readonly List<Node> TopLevel = new List<Node>();
        public readonly Dictionary<string, LabelNode> Labels = new Dictionary<string, LabelNode>();
        public readonly List<InitNode> Inits = new List<InitNode>();
        public readonly Dictionary<string, ImageNode> ImageStatements = new Dictionary<string, ImageNode>();
        public readonly Dictionary<string, SLScreen> Screens = new Dictionary<string, SLScreen>();
        public readonly List<TransformNode> Transforms = new List<TransformNode>();
        public readonly List<StyleNode> Styles = new List<StyleNode>();

        /// <summary>Image name to asset path, from scanning the images directory.</summary>
        public readonly Dictionary<string, string> ImageFiles = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        public readonly List<string> Warnings = new List<string>();

        /// <summary>Parses a .rpyc file and folds its statements into this script.</summary>
        public void AddRpyc(string name, byte[] data)
        {
            var builder = new RenPyAstBuilder();
            object root;

            try
            {
                root = RpycFile.LoadScript(data, null);
            }
            catch (Exception e)
            {
                Warnings.Add("failed to load " + name + ": " + e.Message);
                return;
            }

            var statements = builder.BuildScript(root);

            // Ren'Py links statements into an execution chain after loading rather
            // than pickling the links, so the `next` fields arrive as null.
            ChainBlock(statements, null);

            TopLevel.AddRange(statements);

            foreach (var kv in builder.UnhandledTypes)
                Warnings.Add("unhandled node type " + kv.Key + " x" + kv.Value + " in " + name);

            foreach (var node in statements) Index(node);
        }

        // ---------------------------------------------------------------- chaining

        /// <summary>
        /// Links each statement to the one that follows it, mirroring Ren'Py's
        /// chain_block(). The last statement of a block continues at <paramref name="next"/>.
        /// </summary>
        public static void ChainBlock(List<Node> block, Node next)
        {
            if (block == null || block.Count == 0) return;

            for (int i = 0; i < block.Count - 1; i++) Chain(block[i], block[i + 1]);
            Chain(block[block.Count - 1], next);
        }

        static void Chain(Node node, Node next)
        {
            if (node == null) return;

            // Jump and return never fall through.
            if (node is JumpNode || node is ReturnNode) { node.Next = null; return; }

            var label = node as LabelNode;
            if (label != null)
            {
                if (label.Block.Count > 0)
                {
                    label.Next = label.Block[0];
                    ChainBlock(label.Block, next);
                }
                else label.Next = next;
                return;
            }

            var translate = node as TranslateNode;
            if (translate != null)
            {
                if (translate.Block.Count > 0)
                {
                    translate.Next = translate.Block[0];
                    ChainBlock(translate.Block, next);
                }
                else translate.Next = next;
                return;
            }

            node.Next = next;

            // An init block runs on its own, so its body terminates rather than
            // falling into the statement after the init.
            var init = node as InitNode;
            if (init != null) { ChainBlock(init.Block, null); return; }

            var ifNode = node as IfNode;
            if (ifNode != null)
            {
                foreach (var entry in ifNode.Entries) ChainBlock(entry.Block, next);
                return;
            }

            var whileNode = node as WhileNode;
            if (whileNode != null)
            {
                // The body loops back to the while statement to re-test the condition.
                ChainBlock(whileNode.Block, whileNode);
                return;
            }

            var menu = node as MenuNode;
            if (menu != null)
            {
                foreach (var item in menu.Items)
                    if (item.Block != null) ChainBlock(item.Block, next);
                return;
            }

            var user = node as UserStatementNode;
            if (user != null)
            {
                if (user.CodeBlock != null) ChainBlock(user.CodeBlock, next);
                return;
            }
        }

        void Index(Node node)
        {
            var seen = new HashSet<Node>();
            IndexChain(node, seen);
        }

        void IndexChain(Node node, HashSet<Node> seen)
        {
            while (node != null && seen.Add(node))
            {
                var label = node as LabelNode;
                if (label != null)
                {
                    if (!string.IsNullOrEmpty(label.LabelName)) Labels[label.LabelName] = label;
                    foreach (var child in label.Block) IndexChain(child, seen);
                }

                var init = node as InitNode;
                if (init != null)
                {
                    Inits.Add(init);
                    foreach (var child in init.Block) IndexChain(child, seen);
                }

                var image = node as ImageNode;
                if (image != null) ImageStatements[image.FullName] = image;

                var screen = node as ScreenNode;
                if (screen != null && screen.Screen != null && screen.Screen.ScreenName != null)
                    Screens[screen.Screen.ScreenName] = screen.Screen;

                var transform = node as TransformNode;
                if (transform != null) Transforms.Add(transform);

                var style = node as StyleNode;
                if (style != null) Styles.Add(style);

                var ifNode = node as IfNode;
                if (ifNode != null)
                    foreach (var entry in ifNode.Entries)
                        foreach (var child in entry.Block) IndexChain(child, seen);

                var whileNode = node as WhileNode;
                if (whileNode != null)
                    foreach (var child in whileNode.Block) IndexChain(child, seen);

                var menu = node as MenuNode;
                if (menu != null)
                    foreach (var item in menu.Items)
                        if (item.Block != null)
                            foreach (var child in item.Block) IndexChain(child, seen);

                var user = node as UserStatementNode;
                if (user != null)
                {
                    foreach (var child in user.Block) IndexChain(child, seen);
                    if (user.CodeBlock != null)
                        foreach (var child in user.CodeBlock) IndexChain(child, seen);
                }

                var translate = node as TranslateNode;
                if (translate != null)
                    foreach (var child in translate.Block) IndexChain(child, seen);

                node = node.Next;
            }
        }

        /// <summary>
        /// Builds the image-name index the way Ren'Py's automatic image definition
        /// does: "images/ari domino angry.png" becomes the image "ari domino angry".
        /// </summary>
        public void ScanImages(IEnumerable<string> files)
        {
            foreach (var path in files)
            {
                string lower = path.Replace('\\', '/');

                int slash = lower.LastIndexOf('/');
                string dir = slash < 0 ? "" : lower.Substring(0, slash);
                string file = slash < 0 ? lower : lower.Substring(slash + 1);

                if (!dir.Equals("images", StringComparison.OrdinalIgnoreCase) &&
                    !dir.StartsWith("images/", StringComparison.OrdinalIgnoreCase)) continue;

                int dot = file.LastIndexOf('.');
                if (dot < 0) continue;

                string ext = file.Substring(dot + 1).ToLowerInvariant();
                if (ext != "png" && ext != "jpg" && ext != "jpeg" && ext != "webp" && ext != "bmp") continue;

                string name = file.Substring(0, dot);

                // Subdirectories under images/ contribute their names as leading tags.
                if (dir.Length > "images".Length)
                {
                    string sub = dir.Substring("images/".Length).Replace('/', ' ');
                    name = sub + " " + name;
                }

                name = Normalize(name);
                if (!ImageFiles.ContainsKey(name)) ImageFiles[name] = path;
            }
        }

        /// <summary>Collapses whitespace and lowercases, matching Ren'Py's name matching.</summary>
        public static string Normalize(string name)
        {
            if (string.IsNullOrEmpty(name)) return "";
            var parts = name.Split(new[] { ' ', '\t', '_' }, StringSplitOptions.RemoveEmptyEntries);
            return string.Join(" ", parts).ToLowerInvariant();
        }

        /// <summary>
        /// Resolves an image name to a file, trying progressively shorter attribute
        /// lists the way Ren'Py falls back from "eileen happy" to "eileen".
        /// </summary>
        public string ResolveImageFile(string name)
        {
            string key = Normalize(name);

            string path;
            if (ImageFiles.TryGetValue(key, out path)) return path;

            var parts = key.Split(' ');
            for (int drop = 1; drop < parts.Length; drop++)
            {
                string shorter = string.Join(" ", parts, 0, parts.Length - drop);
                if (ImageFiles.TryGetValue(shorter, out path)) return path;
            }

            return null;
        }

        readonly Dictionary<string, Node> nodesById = new Dictionary<string, Node>();

        /// <summary>
        /// A stable identity for a statement. Ren'Py gives every node a unique
        /// `name` tuple when it compiles, which survives in the .rpyc, so saves can
        /// point at a statement without depending on load order.
        /// </summary>
        public static string IdOf(Node node)
        {
            if (node == null || node.Name == null) return null;
            return PyOps.Repr(node.Name);
        }

        /// <summary>Finds a statement by the identity <see cref="IdOf"/> produced.</summary>
        public Node FindNode(string id)
        {
            if (string.IsNullOrEmpty(id)) return null;

            if (nodesById.Count == 0) BuildNodeIndex();

            Node node;
            return nodesById.TryGetValue(id, out node) ? node : null;
        }

        void BuildNodeIndex()
        {
            var seen = new HashSet<Node>();
            foreach (var node in TopLevel) IndexNodeIds(node, seen);
        }

        void IndexNodeIds(Node node, HashSet<Node> seen)
        {
            while (node != null && seen.Add(node))
            {
                string id = IdOf(node);
                if (id != null && !nodesById.ContainsKey(id)) nodesById[id] = node;

                var label = node as LabelNode;
                if (label != null) foreach (var c in label.Block) IndexNodeIds(c, seen);

                var init = node as InitNode;
                if (init != null) foreach (var c in init.Block) IndexNodeIds(c, seen);

                var ifNode = node as IfNode;
                if (ifNode != null)
                    foreach (var e in ifNode.Entries) foreach (var c in e.Block) IndexNodeIds(c, seen);

                var whileNode = node as WhileNode;
                if (whileNode != null) foreach (var c in whileNode.Block) IndexNodeIds(c, seen);

                var menu = node as MenuNode;
                if (menu != null)
                    foreach (var item in menu.Items)
                        if (item.Block != null) foreach (var c in item.Block) IndexNodeIds(c, seen);

                var user = node as UserStatementNode;
                if (user != null)
                {
                    foreach (var c in user.Block) IndexNodeIds(c, seen);
                    if (user.CodeBlock != null) foreach (var c in user.CodeBlock) IndexNodeIds(c, seen);
                }

                node = node.Next;
            }
        }

        public SLScreen FindScreen(string name)
        {
            SLScreen screen;
            return name != null && Screens.TryGetValue(name, out screen) ? screen : null;
        }

        public LabelNode FindLabel(string name)
        {
            LabelNode label;
            return name != null && Labels.TryGetValue(name, out label) ? label : null;
        }
    }
}
