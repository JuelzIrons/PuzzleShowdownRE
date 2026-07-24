namespace Unity.VisualScripting.Antlr3.Runtime.Tree
{
	public interface ITree
	{
		int ChildCount { get; }

		global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITree Parent { get; set; }

		int ChildIndex { get; set; }

		bool IsNil { get; }

		int Type { get; }

		string Text { get; }

		int Line { get; }

		int CharPositionInLine { get; }

		int TokenStartIndex { get; set; }

		int TokenStopIndex { get; set; }

		bool HasAncestor(int ttype);

		global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITree GetAncestor(int ttype);

		global::System.Collections.IList GetAncestors();

		void FreshenParentAndChildIndexes();

		global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITree GetChild(int i);

		void AddChild(global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITree t);

		void SetChild(int i, global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITree t);

		object DeleteChild(int i);

		void ReplaceChildren(int startChildIndex, int stopChildIndex, object t);

		global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITree DupNode();

		string ToStringTree();

		new string ToString();
	}
}
