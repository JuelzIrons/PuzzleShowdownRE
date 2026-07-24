namespace Unity.Hierarchy
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential)]
	[global::UnityEngine.Scripting.RequiredByNativeCode(Optional = true)]
	[global::UnityEngine.Bindings.VisibleToOtherModules(new string[] { "UnityEditor.UIToolkitAuthoringModule" })]
	internal abstract class HierarchyNodeTypeHandler : global::Unity.Hierarchy.HierarchyNodeTypeHandlerBase
	{
		private readonly global::System.Lazy<global::UnityEngine.Pool.ObjectPool<global::Unity.Hierarchy.HierarchyViewItem>> m_ViewItemPool;

		internal global::UnityEngine.Pool.ObjectPool<global::Unity.Hierarchy.HierarchyViewItem> ViewItemPool => m_ViewItemPool.Value;

		protected HierarchyNodeTypeHandler()
		{
			m_ViewItemPool = new global::System.Lazy<global::UnityEngine.Pool.ObjectPool<global::Unity.Hierarchy.HierarchyViewItem>>(() => new global::UnityEngine.Pool.ObjectPool<global::Unity.Hierarchy.HierarchyViewItem>(() => new global::Unity.Hierarchy.HierarchyViewItem(), null, null, null, collectionCheck: true, 0));
		}

		[global::UnityEngine.Bindings.VisibleToOtherModules(new string[] { "UnityEditor.HierarchyModule" })]
		internal HierarchyNodeTypeHandler(global::System.IntPtr nativePtr, global::Unity.Hierarchy.Hierarchy hierarchy, global::Unity.Hierarchy.HierarchyCommandList cmdList)
			: base(nativePtr, hierarchy, cmdList)
		{
			m_ViewItemPool = new global::System.Lazy<global::UnityEngine.Pool.ObjectPool<global::Unity.Hierarchy.HierarchyViewItem>>(() => new global::UnityEngine.Pool.ObjectPool<global::Unity.Hierarchy.HierarchyViewItem>(() => new global::Unity.Hierarchy.HierarchyViewItem(), null, null, null, collectionCheck: true, 0));
		}

		protected virtual void OnBindView(global::Unity.Hierarchy.HierarchyView view)
		{
		}

		protected virtual void OnUnbindView(global::Unity.Hierarchy.HierarchyView view)
		{
		}

		protected virtual void OnBindItem(global::Unity.Hierarchy.HierarchyViewItem item)
		{
		}

		protected virtual void OnUnbindItem(global::Unity.Hierarchy.HierarchyViewItem item)
		{
		}

		internal void Internal_BindView(global::Unity.Hierarchy.HierarchyView view)
		{
			OnBindView(view);
		}

		internal void Internal_UnbindView(global::Unity.Hierarchy.HierarchyView view)
		{
			OnUnbindView(view);
		}

		internal void Internal_BindItem(global::Unity.Hierarchy.HierarchyViewItem item)
		{
			OnBindItem(item);
		}

		internal void Internal_UnbindItem(global::Unity.Hierarchy.HierarchyViewItem item)
		{
			OnUnbindItem(item);
		}
	}
}
