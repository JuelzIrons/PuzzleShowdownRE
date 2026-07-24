namespace Unity.Hierarchy
{
	internal readonly struct HierarchyNodeTypeHandlerEnumerable
	{
		public struct Enumerator : global::System.IDisposable
		{
			private readonly global::System.IntPtr[] m_Handlers;

			private readonly int m_Count;

			private int m_Index;

			public global::Unity.Hierarchy.HierarchyNodeTypeHandler Current
			{
				[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
				get
				{
					return global::Unity.Hierarchy.HierarchyNodeTypeHandlerBase.FromIntPtr(m_Handlers[m_Index]) as global::Unity.Hierarchy.HierarchyNodeTypeHandler;
				}
			}

			internal Enumerator(global::Unity.Hierarchy.Hierarchy hierarchy)
			{
				int nodeTypeHandlersBaseCount = hierarchy.GetNodeTypeHandlersBaseCount();
				m_Handlers = global::System.Buffers.ArrayPool<global::System.IntPtr>.Shared.Rent(nodeTypeHandlersBaseCount);
				m_Count = hierarchy.GetNodeTypeHandlersBaseSpan(global::System.MemoryExtensions.AsSpan(m_Handlers).Slice(0, nodeTypeHandlersBaseCount));
				m_Index = -1;
			}

			public void Dispose()
			{
				global::System.Buffers.ArrayPool<global::System.IntPtr>.Shared.Return(m_Handlers);
			}

			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public bool MoveNext()
			{
				while (++m_Index < m_Count)
				{
					if (global::Unity.Hierarchy.HierarchyNodeTypeHandlerBase.FromIntPtr(m_Handlers[m_Index]) is global::Unity.Hierarchy.HierarchyNodeTypeHandler)
					{
						return true;
					}
				}
				return false;
			}
		}

		private readonly global::Unity.Hierarchy.Hierarchy m_Hierarchy;

		internal HierarchyNodeTypeHandlerEnumerable(global::Unity.Hierarchy.Hierarchy hierarchy)
		{
			m_Hierarchy = hierarchy;
		}

		public global::Unity.Hierarchy.HierarchyNodeTypeHandlerEnumerable.Enumerator GetEnumerator()
		{
			return new global::Unity.Hierarchy.HierarchyNodeTypeHandlerEnumerable.Enumerator(m_Hierarchy);
		}
	}
}
