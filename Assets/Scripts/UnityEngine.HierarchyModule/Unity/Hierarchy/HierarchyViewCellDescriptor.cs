namespace Unity.Hierarchy
{
	[global::UnityEngine.Bindings.VisibleToOtherModules(new string[] { "UnityEditor.UIToolkitAuthoringModule" })]
	internal sealed class HierarchyViewCellDescriptor
	{
		private bool m_IsColumnBound;

		public readonly string ColumnId;

		public readonly global::System.Type HandlerType;

		public global::System.Action<global::Unity.Hierarchy.HierarchyViewCell> BindCell;

		public global::System.Action<global::Unity.Hierarchy.HierarchyViewCell> UnbindCell;

		public global::System.Action<global::Unity.Hierarchy.HierarchyViewColumnDescriptor, global::Unity.Hierarchy.HierarchyView> BindColumn;

		public global::System.Action<global::Unity.Hierarchy.HierarchyViewColumnDescriptor, global::Unity.Hierarchy.HierarchyView> UnbindColumn;

		public bool ClearCellContent;

		public object UserData;

		public HierarchyViewCellDescriptor(string columnId, global::System.Type handlerType = null)
		{
			ColumnId = columnId;
			HandlerType = handlerType;
			ClearCellContent = true;
		}

		internal void InvokeBindColumn(global::Unity.Hierarchy.HierarchyViewColumnDescriptor descriptor, global::Unity.Hierarchy.HierarchyView view)
		{
			if (!m_IsColumnBound)
			{
				BindColumn?.Invoke(descriptor, view);
				m_IsColumnBound = true;
			}
		}

		internal void InvokeUnbindColumn(global::Unity.Hierarchy.HierarchyViewColumnDescriptor descriptor, global::Unity.Hierarchy.HierarchyView view)
		{
			if (m_IsColumnBound)
			{
				UnbindColumn?.Invoke(descriptor, view);
				m_IsColumnBound = false;
			}
		}

		public bool ValidForColumn(global::Unity.Hierarchy.HierarchyViewColumnDescriptor colDesc)
		{
			return ColumnId == colDesc.Id;
		}

		public override string ToString()
		{
			string text = ((HandlerType != null) ? HandlerType.Name : "<GenericNodeHandler>");
			return ColumnId + " - " + text;
		}
	}
}
