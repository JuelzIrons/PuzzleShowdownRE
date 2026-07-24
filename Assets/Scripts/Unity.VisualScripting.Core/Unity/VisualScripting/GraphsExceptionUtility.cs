namespace Unity.VisualScripting
{
	public static class GraphsExceptionUtility
	{
		private const string handledKey = "Bolt.Core.Handled";

		public static global::System.Exception GetException(this global::Unity.VisualScripting.IGraphElementWithDebugData element, global::Unity.VisualScripting.GraphPointer pointer)
		{
			if (!pointer.hasDebugData)
			{
				return null;
			}
			return pointer.GetElementDebugData<global::Unity.VisualScripting.IGraphElementDebugData>(element).runtimeException;
		}

		public static void SetException(this global::Unity.VisualScripting.IGraphElementWithDebugData element, global::Unity.VisualScripting.GraphPointer pointer, global::System.Exception ex)
		{
			if (pointer.hasDebugData)
			{
				pointer.GetElementDebugData<global::Unity.VisualScripting.IGraphElementDebugData>(element).runtimeException = ex;
			}
		}

		public static void HandleException(this global::Unity.VisualScripting.IGraphElementWithDebugData element, global::Unity.VisualScripting.GraphPointer pointer, global::System.Exception ex)
		{
			global::Unity.VisualScripting.Ensure.That("ex").IsNotNull(ex);
			if (pointer == null)
			{
				global::UnityEngine.Debug.LogError("Caught exception with null graph pointer (flow was likely disposed):\n" + ex);
				return;
			}
			global::Unity.VisualScripting.GraphReference graphReference = pointer.AsReference();
			if (!ex.HandledIn(graphReference))
			{
				element.SetException(pointer, ex);
			}
			while (graphReference.isChild)
			{
				global::Unity.VisualScripting.IGraphParentElement parentElement = graphReference.parentElement;
				graphReference = graphReference.ParentReference(ensureValid: true);
				if (parentElement is global::Unity.VisualScripting.IGraphElementWithDebugData element2 && !ex.HandledIn(graphReference))
				{
					element2.SetException(graphReference, ex);
				}
			}
		}

		private static bool HandledIn(this global::System.Exception ex, global::Unity.VisualScripting.GraphReference reference)
		{
			global::Unity.VisualScripting.Ensure.That("ex").IsNotNull(ex);
			if (!ex.Data.Contains("Bolt.Core.Handled"))
			{
				ex.Data.Add("Bolt.Core.Handled", new global::System.Collections.Generic.HashSet<global::Unity.VisualScripting.GraphReference>());
			}
			global::System.Collections.Generic.HashSet<global::Unity.VisualScripting.GraphReference> hashSet = (global::System.Collections.Generic.HashSet<global::Unity.VisualScripting.GraphReference>)ex.Data["Bolt.Core.Handled"];
			if (hashSet.Contains(reference))
			{
				return true;
			}
			hashSet.Add(reference);
			return false;
		}
	}
}
