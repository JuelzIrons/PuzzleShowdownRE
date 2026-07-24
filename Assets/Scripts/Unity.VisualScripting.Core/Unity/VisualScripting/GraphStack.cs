namespace Unity.VisualScripting
{
	public sealed class GraphStack : global::Unity.VisualScripting.GraphPointer, global::Unity.VisualScripting.IPoolable, global::System.IDisposable
	{
		private GraphStack()
		{
		}

		private void InitializeNoAlloc(global::Unity.VisualScripting.IGraphRoot root, global::System.Collections.Generic.List<global::Unity.VisualScripting.IGraphParentElement> parentElements, bool ensureValid)
		{
			Initialize(root);
			global::Unity.VisualScripting.Ensure.That("parentElements").IsNotNull(parentElements);
			foreach (global::Unity.VisualScripting.IGraphParentElement parentElement in parentElements)
			{
				if (!TryEnterParentElement(parentElement, out var error))
				{
					if (ensureValid)
					{
						throw new global::Unity.VisualScripting.GraphPointerException(error, this);
					}
					break;
				}
			}
		}

		internal static global::Unity.VisualScripting.GraphStack New(global::Unity.VisualScripting.IGraphRoot root, global::System.Collections.Generic.List<global::Unity.VisualScripting.IGraphParentElement> parentElements)
		{
			global::Unity.VisualScripting.GraphStack obj = global::Unity.VisualScripting.GenericPool<global::Unity.VisualScripting.GraphStack>.New(() => new global::Unity.VisualScripting.GraphStack());
			obj.InitializeNoAlloc(root, parentElements, ensureValid: true);
			return obj;
		}

		internal static global::Unity.VisualScripting.GraphStack New(global::Unity.VisualScripting.GraphPointer model)
		{
			global::Unity.VisualScripting.GraphStack obj = global::Unity.VisualScripting.GenericPool<global::Unity.VisualScripting.GraphStack>.New(() => new global::Unity.VisualScripting.GraphStack());
			obj.CopyFrom(model);
			return obj;
		}

		public global::Unity.VisualScripting.GraphStack Clone()
		{
			return New(this);
		}

		public void Dispose()
		{
			global::Unity.VisualScripting.GenericPool<global::Unity.VisualScripting.GraphStack>.Free(this);
		}

		void global::Unity.VisualScripting.IPoolable.New()
		{
		}

		void global::Unity.VisualScripting.IPoolable.Free()
		{
			base.root = null;
			parentStack.Clear();
			parentElementStack.Clear();
			graphStack.Clear();
			dataStack.Clear();
			debugDataStack.Clear();
		}

		public override global::Unity.VisualScripting.GraphReference AsReference()
		{
			return ToReference();
		}

		public global::Unity.VisualScripting.GraphReference ToReference()
		{
			return global::Unity.VisualScripting.GraphReference.Intern(this);
		}

		internal void ClearReference()
		{
			global::Unity.VisualScripting.GraphReference.ClearIntern(this);
		}

		public new void EnterParentElement(global::Unity.VisualScripting.IGraphParentElement parentElement)
		{
			base.EnterParentElement(parentElement);
		}

		public bool TryEnterParentElement(global::Unity.VisualScripting.IGraphParentElement parentElement)
		{
			string error;
			return TryEnterParentElement(parentElement, out error);
		}

		public bool TryEnterParentElementUnsafe(global::Unity.VisualScripting.IGraphParentElement parentElement)
		{
			string error;
			return TryEnterParentElement(parentElement, out error, null, skipContainsCheck: true);
		}

		public new void ExitParentElement()
		{
			base.ExitParentElement();
		}
	}
}
