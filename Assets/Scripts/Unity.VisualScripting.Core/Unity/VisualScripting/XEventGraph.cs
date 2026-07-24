namespace Unity.VisualScripting
{
	public static class XEventGraph
	{
		public static void TriggerEventHandler<TArgs>(this global::Unity.VisualScripting.GraphReference reference, global::System.Func<global::Unity.VisualScripting.EventHook, bool> predicate, TArgs args, global::System.Func<global::Unity.VisualScripting.IGraphParentElement, bool> recurse, bool force)
		{
			global::Unity.VisualScripting.Ensure.That("reference").IsNotNull(reference);
			foreach (global::Unity.VisualScripting.IGraphElement element in reference.graph.elements)
			{
				if (element is global::Unity.VisualScripting.IGraphEventHandler<TArgs> graphEventHandler && (predicate == null || predicate(graphEventHandler.GetHook(reference))) && (force || graphEventHandler.IsListening(reference)))
				{
					graphEventHandler.Trigger(reference, args);
				}
				if (element is global::Unity.VisualScripting.IGraphParentElement graphParentElement && recurse(graphParentElement))
				{
					reference.ChildReference(graphParentElement, ensureValid: false, 0)?.TriggerEventHandler(predicate, args, recurse, force);
				}
			}
		}

		public static void TriggerEventHandler<TArgs>(this global::Unity.VisualScripting.GraphStack stack, global::System.Func<global::Unity.VisualScripting.EventHook, bool> predicate, TArgs args, global::System.Func<global::Unity.VisualScripting.IGraphParentElement, bool> recurse, bool force)
		{
			global::Unity.VisualScripting.Ensure.That("stack").IsNotNull(stack);
			global::Unity.VisualScripting.GraphReference graphReference = null;
			foreach (global::Unity.VisualScripting.IGraphElement element in stack.graph.elements)
			{
				if (element is global::Unity.VisualScripting.IGraphEventHandler<TArgs> graphEventHandler)
				{
					if (graphReference == null)
					{
						graphReference = stack.ToReference();
					}
					if ((predicate == null || predicate(graphEventHandler.GetHook(graphReference))) && (force || graphEventHandler.IsListening(graphReference)))
					{
						graphEventHandler.Trigger(graphReference, args);
					}
				}
				if (element is global::Unity.VisualScripting.IGraphParentElement graphParentElement && recurse(graphParentElement) && stack.TryEnterParentElementUnsafe(graphParentElement))
				{
					stack.TriggerEventHandler(predicate, args, recurse, force);
					stack.ExitParentElement();
				}
			}
		}
	}
}
