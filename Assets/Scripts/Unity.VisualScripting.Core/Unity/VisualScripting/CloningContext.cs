namespace Unity.VisualScripting
{
	public sealed class CloningContext : global::Unity.VisualScripting.IPoolable, global::System.IDisposable
	{
		private bool disposed;

		public global::System.Collections.Generic.Dictionary<object, object> clonings { get; } = new global::System.Collections.Generic.Dictionary<object, object>(global::Unity.VisualScripting.ReferenceEqualityComparer.Instance);

		public global::Unity.VisualScripting.ICloner fallbackCloner { get; private set; }

		public bool tryPreserveInstances { get; private set; }

		void global::Unity.VisualScripting.IPoolable.New()
		{
			disposed = false;
		}

		void global::Unity.VisualScripting.IPoolable.Free()
		{
			disposed = true;
			clonings.Clear();
		}

		public void Dispose()
		{
			if (disposed)
			{
				throw new global::System.ObjectDisposedException(ToString());
			}
			global::Unity.VisualScripting.GenericPool<global::Unity.VisualScripting.CloningContext>.Free(this);
		}

		public static global::Unity.VisualScripting.CloningContext New(global::Unity.VisualScripting.ICloner fallbackCloner, bool tryPreserveInstances)
		{
			global::Unity.VisualScripting.CloningContext cloningContext = global::Unity.VisualScripting.GenericPool<global::Unity.VisualScripting.CloningContext>.New(() => new global::Unity.VisualScripting.CloningContext());
			cloningContext.fallbackCloner = fallbackCloner;
			cloningContext.tryPreserveInstances = tryPreserveInstances;
			return cloningContext;
		}
	}
}
