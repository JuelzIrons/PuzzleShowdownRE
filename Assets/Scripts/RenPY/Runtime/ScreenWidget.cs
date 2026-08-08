using System;
using System.Collections.Generic;
using RenPy.Python;

namespace RenPy.Runtime
{
    /// <summary>
    /// A resolved screen-language widget: the output of evaluating an SL tree with a
    /// concrete scope. Deliberately host-agnostic so the evaluator stays testable and
    /// the Unity layer only has to build uGUI from plain data.
    /// </summary>
    public sealed class ScreenWidget
    {
        /// <summary>Statement name: "vbox", "imagebutton", "text", "add", ...</summary>
        public string Kind = "fixed";

        public readonly List<ScreenWidget> Children = new List<ScreenWidget>();

        /// <summary>Evaluated keyword properties, e.g. xpos, idle, spacing.</summary>
        public readonly PyDict Properties = new PyDict();

        /// <summary>Positional arguments, e.g. the displayable for `add`.</summary>
        public readonly List<object> Positional = new List<object>();

        /// <summary>Text content for text/label/textbutton.</summary>
        public string Text;

        /// <summary>Image paths for the button states, when resolvable.</summary>
        public string Idle, Hover, Selected, Insensitive;

        /// <summary>Set when the image is a movie or solid rather than a file.</summary>
        public Displayable Displayable;

        /// <summary>Action invoked on click, as a Python callable or list of them.</summary>
        public object Action;
        public object Hovered, Unhovered;

        public string ActivateSound, HoverSound;

        /// <summary>Transform from `at`, already resolved to a timeline.</summary>
        public TransformState Transform = new TransformState();
        public AtlTimeline Timeline;

        /// <summary>Style prefix inherited from the screen or a container.</summary>
        public string StylePrefix;

        public object Get(string name) { return Properties.GetStr(name, null); }

        public bool Has(string name) { return Properties.Contains(name); }

        public float Float(string name, float dflt)
        {
            object v = Properties.GetStr(name, null);
            if (v == null) return dflt;
            try { return (float)Py.ToDouble(v); }
            catch (PyError) { return dflt; }
        }

        public string Str(string name)
        {
            object v = Properties.GetStr(name, null);
            return v == null ? null : PyOps.ToStr(v);
        }

        public override string ToString()
        {
            return Kind + (Text != null ? " \"" + Text + "\"" : "") + " (" + Children.Count + " children)";
        }
    }

    /// <summary>A screen that is currently displayed.</summary>
    public sealed class ShownScreen
    {
        public string Name;
        /// <summary>Tag the screen occupies; showing another screen with the tag replaces it.</summary>
        public string Tag;
        public int ZOrder;
        public bool Modal;
        /// <summary>Arguments the screen was shown with.</summary>
        public PyDict Scope = new PyDict();
        public ScreenWidget Root;

        public override string ToString() { return Name + " (z " + ZOrder + ")"; }
    }
}
