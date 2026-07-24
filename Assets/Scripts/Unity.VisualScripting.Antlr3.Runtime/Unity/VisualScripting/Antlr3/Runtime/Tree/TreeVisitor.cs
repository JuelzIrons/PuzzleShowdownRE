namespace Unity.VisualScripting.Antlr3.Runtime.Tree
{
	public class TreeVisitor
	{
		protected global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITreeAdaptor adaptor;

		public TreeVisitor(global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITreeAdaptor adaptor)
		{
			this.adaptor = adaptor;
		}

		public TreeVisitor()
			: this(new global::Unity.VisualScripting.Antlr3.Runtime.Tree.CommonTreeAdaptor())
		{
		}

		public object Visit(object t, global::Unity.VisualScripting.Antlr3.Runtime.Tree.ITreeVisitorAction action)
		{
			bool flag = adaptor.IsNil(t);
			if (action != null && !flag)
			{
				t = action.Pre(t);
			}
			int childCount = adaptor.GetChildCount(t);
			for (int i = 0; i < childCount; i++)
			{
				object child = adaptor.GetChild(t, i);
				object obj = Visit(child, action);
				object child2 = adaptor.GetChild(t, i);
				if (obj != child2)
				{
					adaptor.SetChild(t, i, obj);
				}
			}
			if (action != null && !flag)
			{
				t = action.Post(t);
			}
			return t;
		}
	}
}
