namespace Unity.Hierarchy
{
	internal sealed class HierarchyViewColumnDescriptor
	{
		private bool m_IsBound;

		public readonly string Id;

		public string Title;

		public global::UnityEngine.Texture2D Icon;

		public string Tooltip;

		public int DefaultPriority;

		public int DefaultWidth = -1;

		public bool DefaultVisibility;

		public object UserData;

		public global::System.Func<global::UnityEngine.UIElements.VisualElement> MakeHeader;

		public global::System.Action<global::UnityEngine.UIElements.VisualElement, global::Unity.Hierarchy.HierarchyView> BindHeader;

		public global::System.Action<global::UnityEngine.UIElements.VisualElement, global::Unity.Hierarchy.HierarchyView> UnbindHeader;

		public global::System.Action<global::UnityEngine.UIElements.VisualElement, global::Unity.Hierarchy.HierarchyView> DestroyHeader;

		public global::System.Action<global::Unity.Hierarchy.HierarchyViewColumn, global::Unity.Hierarchy.HierarchyView> BindColumn;

		public global::System.Action<global::Unity.Hierarchy.HierarchyViewColumn, global::Unity.Hierarchy.HierarchyView> UnbindColumn;

		internal void InvokeBindColumn(global::Unity.Hierarchy.HierarchyViewColumn column, global::Unity.Hierarchy.HierarchyView view)
		{
			if (!m_IsBound)
			{
				BindColumn?.Invoke(column, view);
				m_IsBound = true;
			}
		}

		internal void InvokeUnbindColumn(global::Unity.Hierarchy.HierarchyViewColumn column, global::Unity.Hierarchy.HierarchyView view)
		{
			if (m_IsBound)
			{
				UnbindColumn?.Invoke(column, view);
				m_IsBound = false;
			}
		}

		public HierarchyViewColumnDescriptor(string columnId)
		{
			Id = columnId;
		}

		public override string ToString()
		{
			return Id;
		}
	}
}
