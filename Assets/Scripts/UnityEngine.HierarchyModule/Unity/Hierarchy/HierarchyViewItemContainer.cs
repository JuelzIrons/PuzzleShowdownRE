namespace Unity.Hierarchy
{
	internal class HierarchyViewItemContainer : global::UnityEngine.UIElements.VisualElement
	{
		[global::Unity.Scripting.LifecycleManagement.AutoStaticsCleanupOnCodeReload(CleanupStrategy = global::Unity.Scripting.LifecycleManagement.CleanupStrategy.Clear)]
		private static readonly global::UnityEngine.Pool.ObjectPool<global::Unity.Hierarchy.HierarchyViewItem> s_ViewItemPool = new global::UnityEngine.Pool.ObjectPool<global::Unity.Hierarchy.HierarchyViewItem>(() => new global::Unity.Hierarchy.HierarchyViewItem());

		private global::Unity.Hierarchy.HierarchyView m_View;

		private global::Unity.Hierarchy.HierarchyViewItem m_ViewItem;

		private global::Unity.Hierarchy.HierarchyNodeTypeHandler m_ViewItemNodeTypeHandler;

		public global::Unity.Hierarchy.HierarchyView View => m_View;

		public global::Unity.Hierarchy.HierarchyViewItem ViewItem => m_ViewItem;

		public global::Unity.Hierarchy.HierarchyNodeTypeHandler ViewItemNodeTypeHandler => m_ViewItemNodeTypeHandler;

		public void Bind(in global::Unity.Hierarchy.HierarchyNode node, global::Unity.Hierarchy.HierarchyView view)
		{
			if (node == global::Unity.Hierarchy.HierarchyNode.Null)
			{
				throw new global::System.ArgumentNullException("node");
			}
			if (view == null)
			{
				throw new global::System.ArgumentNullException("view");
			}
			global::Unity.Hierarchy.HierarchyNodeTypeHandler nodeTypeHandler = view.Source.GetNodeTypeHandler(in node);
			if (m_ViewItem == null || m_ViewItemNodeTypeHandler != nodeTypeHandler)
			{
				ReleaseViewItem();
				m_ViewItem = ((nodeTypeHandler != null) ? nodeTypeHandler.ViewItemPool.Get() : s_ViewItemPool.Get());
				if (m_ViewItem == null)
				{
					throw new global::System.NullReferenceException("Failed to get a view item from the pool");
				}
				Add(m_ViewItem);
				m_ViewItemNodeTypeHandler = nodeTypeHandler;
			}
			m_View = view;
			m_ViewItem.Bind(in node, m_View);
		}

		public void Unbind()
		{
			m_ViewItem?.Unbind();
		}

		public void ReleaseViewItem()
		{
			if (m_ViewItem != null)
			{
				if (m_ViewItem.Bound)
				{
					m_ViewItem.Unbind();
				}
				Remove(m_ViewItem);
				if (m_ViewItemNodeTypeHandler != null)
				{
					m_ViewItemNodeTypeHandler.ViewItemPool.Release(m_ViewItem);
				}
				else
				{
					s_ViewItemPool.Release(m_ViewItem);
				}
				m_ViewItem = null;
			}
			m_View = null;
			m_ViewItemNodeTypeHandler = null;
		}
	}
}
