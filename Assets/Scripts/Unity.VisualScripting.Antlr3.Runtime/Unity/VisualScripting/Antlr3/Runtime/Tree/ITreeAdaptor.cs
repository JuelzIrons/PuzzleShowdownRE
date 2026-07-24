namespace Unity.VisualScripting.Antlr3.Runtime.Tree
{
	public interface ITreeAdaptor
	{
		object Create(global::Unity.VisualScripting.Antlr3.Runtime.IToken payload);

		object DupNode(object treeNode);

		object DupTree(object tree);

		object GetNilNode();

		object ErrorNode(global::Unity.VisualScripting.Antlr3.Runtime.ITokenStream input, global::Unity.VisualScripting.Antlr3.Runtime.IToken start, global::Unity.VisualScripting.Antlr3.Runtime.IToken stop, global::Unity.VisualScripting.Antlr3.Runtime.RecognitionException e);

		bool IsNil(object tree);

		void AddChild(object t, object child);

		object BecomeRoot(object newRoot, object oldRoot);

		object RulePostProcessing(object root);

		int GetUniqueID(object node);

		object BecomeRoot(global::Unity.VisualScripting.Antlr3.Runtime.IToken newRoot, object oldRoot);

		object Create(int tokenType, global::Unity.VisualScripting.Antlr3.Runtime.IToken fromToken);

		object Create(int tokenType, global::Unity.VisualScripting.Antlr3.Runtime.IToken fromToken, string text);

		object Create(int tokenType, string text);

		int GetNodeType(object t);

		void SetNodeType(object t, int type);

		string GetNodeText(object t);

		void SetNodeText(object t, string text);

		global::Unity.VisualScripting.Antlr3.Runtime.IToken GetToken(object treeNode);

		void SetTokenBoundaries(object t, global::Unity.VisualScripting.Antlr3.Runtime.IToken startToken, global::Unity.VisualScripting.Antlr3.Runtime.IToken stopToken);

		int GetTokenStartIndex(object t);

		int GetTokenStopIndex(object t);

		object GetChild(object t, int i);

		void SetChild(object t, int i, object child);

		object DeleteChild(object t, int i);

		int GetChildCount(object t);

		object GetParent(object t);

		void SetParent(object t, object parent);

		int GetChildIndex(object t);

		void SetChildIndex(object t, int index);

		void ReplaceChildren(object parent, int startChildIndex, int stopChildIndex, object t);
	}
}
