namespace Unity.VisualScripting
{
	public static class XGraphEventListener
	{
		public static void StartListening(this global::Unity.VisualScripting.IGraphEventListener listener, global::Unity.VisualScripting.GraphReference reference)
		{
			using global::Unity.VisualScripting.GraphStack stack = reference.ToStackPooled();
			listener.StartListening(stack);
		}

		public static void StopListening(this global::Unity.VisualScripting.IGraphEventListener listener, global::Unity.VisualScripting.GraphReference reference)
		{
			using global::Unity.VisualScripting.GraphStack stack = reference.ToStackPooled();
			listener.StopListening(stack);
		}

		public static bool IsHierarchyListening(global::Unity.VisualScripting.GraphReference reference)
		{
			using global::Unity.VisualScripting.GraphStack graphStack = reference.ToStackPooled();
			while (graphStack.isChild)
			{
				global::Unity.VisualScripting.IGraphParent parent = graphStack.parent;
				graphStack.ExitParentElement();
				if (parent is global::Unity.VisualScripting.IGraphEventListener graphEventListener && !graphEventListener.IsListening(graphStack))
				{
					return false;
				}
			}
			if (graphStack.graph is global::Unity.VisualScripting.IGraphEventListener graphEventListener2 && !graphEventListener2.IsListening(graphStack))
			{
				return false;
			}
			return true;
		}
	}
}
