namespace Unity.VisualScripting.Antlr3.Runtime.Tree
{
	public interface ITreeNodeStream : global::Unity.VisualScripting.Antlr3.Runtime.IIntStream
	{
		object TreeSource { get; }

		global::Unity.VisualScripting.Antlr3.Runtime.ITokenStream TokenStream { get; }

		global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITreeAdaptor TreeAdaptor { get; }

		bool HasUniqueNavigationNodes { set; }

		object Get(int i);

		object LT(int k);

		string ToString(object start, object stop);

		void ReplaceChildren(object parent, int startChildIndex, int stopChildIndex, object t);
	}
}
