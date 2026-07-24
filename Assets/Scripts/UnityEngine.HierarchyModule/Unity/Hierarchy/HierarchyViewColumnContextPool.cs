namespace Unity.Hierarchy
{
	[global::UnityEngine.Bindings.VisibleToOtherModules(new string[] { "UnityEditor.HierarchyModule" })]
	internal sealed class HierarchyViewColumnContextPool<TPooledObject> where TPooledObject : class
	{
		private class ContextPoolImplementation
		{
			public global::UnityEngine.Pool.ObjectPool<TPooledObject> Pool { get; private set; }

			public global::System.Collections.Generic.HashSet<TPooledObject> Active { get; private set; } = new global::System.Collections.Generic.HashSet<TPooledObject>();

			public ContextPoolImplementation(global::System.Func<TPooledObject> creator)
			{
				Pool = new global::UnityEngine.Pool.ObjectPool<TPooledObject>(creator);
			}
		}

		private readonly global::System.Func<TPooledObject> m_ObjectCreator;

		private readonly global::System.Collections.Generic.Dictionary<int, global::Unity.Hierarchy.HierarchyViewColumnContextPool<TPooledObject>.ContextPoolImplementation> m_Pools = new global::System.Collections.Generic.Dictionary<int, global::Unity.Hierarchy.HierarchyViewColumnContextPool<TPooledObject>.ContextPoolImplementation>();

		public HierarchyViewColumnContextPool(global::System.Func<TPooledObject> objectCreator)
		{
			m_ObjectCreator = objectCreator;
		}

		public TPooledObject Get(int contextId)
		{
			global::Unity.Hierarchy.HierarchyViewColumnContextPool<TPooledObject>.ContextPoolImplementation poolForContext = GetPoolForContext(contextId);
			TPooledObject val = poolForContext.Pool.Get();
			poolForContext.Active.Add(val);
			return val;
		}

		public void Release(int contextId, TPooledObject obj)
		{
			global::Unity.Hierarchy.HierarchyViewColumnContextPool<TPooledObject>.ContextPoolImplementation poolForContext = GetPoolForContext(contextId);
			poolForContext.Pool.Release(obj);
			poolForContext.Active.Remove(obj);
		}

		public global::System.Collections.Generic.IReadOnlyCollection<TPooledObject> GetActiveObjects(int contextId)
		{
			if (m_Pools.TryGetValue(contextId, out var value))
			{
				return value.Active;
			}
			return global::System.Array.Empty<TPooledObject>();
		}

		public void Clear(int contextId)
		{
			if (m_Pools.TryGetValue(contextId, out var value))
			{
				value.Pool.Dispose();
				value.Active.Clear();
				m_Pools.Remove(contextId);
			}
		}

		internal bool Exists(int contextId)
		{
			return m_Pools.ContainsKey(contextId);
		}

		private global::Unity.Hierarchy.HierarchyViewColumnContextPool<TPooledObject>.ContextPoolImplementation GetPoolForContext(int contextId)
		{
			if (!m_Pools.TryGetValue(contextId, out var value))
			{
				value = new global::Unity.Hierarchy.HierarchyViewColumnContextPool<TPooledObject>.ContextPoolImplementation(m_ObjectCreator);
				m_Pools[contextId] = value;
			}
			return value;
		}
	}
}
