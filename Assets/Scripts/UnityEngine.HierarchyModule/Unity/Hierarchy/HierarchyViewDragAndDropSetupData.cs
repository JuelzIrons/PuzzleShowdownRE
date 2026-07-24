namespace Unity.Hierarchy
{
	[global::UnityEngine.Bindings.VisibleToOtherModules]
	internal readonly ref struct HierarchyViewDragAndDropSetupData
	{
		private readonly global::System.Collections.Generic.Dictionary<string, object> m_GenericData;

		public global::System.ReadOnlySpan<global::Unity.Hierarchy.HierarchyNode> Nodes { get; }

		public global::System.Collections.Generic.List<global::UnityEngine.EntityId> EntityIds { get; }

		public global::System.Collections.Generic.List<string> Paths { get; }

		public global::Unity.Hierarchy.HierarchyView View { get; }

		public void SetGenericData(string key, object value)
		{
			m_GenericData[key] = value;
		}

		internal HierarchyViewDragAndDropSetupData(global::System.ReadOnlySpan<global::Unity.Hierarchy.HierarchyNode> nodes, global::System.Collections.Generic.List<global::UnityEngine.EntityId> entityIds, global::System.Collections.Generic.List<string> paths, global::Unity.Hierarchy.HierarchyView view, global::System.Collections.Generic.Dictionary<string, object> genericData)
		{
			Nodes = nodes;
			EntityIds = entityIds;
			Paths = paths;
			View = view;
			m_GenericData = genericData;
		}
	}
}
