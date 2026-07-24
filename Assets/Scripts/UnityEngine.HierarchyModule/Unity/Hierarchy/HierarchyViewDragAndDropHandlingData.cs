namespace Unity.Hierarchy
{
	[global::UnityEngine.Bindings.VisibleToOtherModules(new string[] { "UnityEditor.UIToolkitAuthoringModule" })]
	internal readonly ref struct HierarchyViewDragAndDropHandlingData
	{
		private readonly global::UnityEngine.UIElements.DragAndDropData m_DragAndDropData;

		public global::Unity.Hierarchy.HierarchyNode Parent { get; }

		public global::Unity.Hierarchy.HierarchyNode Target { get; }

		public int InsertAtIndex { get; }

		public global::UnityEngine.UIElements.DragAndDropPosition DropPosition { get; }

		public global::Unity.Hierarchy.HierarchyView View { get; }

		public global::System.Collections.Generic.IReadOnlyList<global::UnityEngine.EntityId> EntityIds => m_DragAndDropData.entityIds;

		public string[] Paths => m_DragAndDropData.paths;

		public object Source => m_DragAndDropData.source;

		[global::UnityEngine.Bindings.VisibleToOtherModules(new string[] { "UnityEditor.HierarchyModule" })]
		internal global::UnityEngine.EventModifiers EventModifiers { get; }

		public object GetGenericData(string key)
		{
			return m_DragAndDropData.GetGenericData(key);
		}

		internal HierarchyViewDragAndDropHandlingData(in global::Unity.Hierarchy.HierarchyNode parent, in global::Unity.Hierarchy.HierarchyNode target, int insertAtIndex, global::UnityEngine.UIElements.DragAndDropPosition dropPosition, global::UnityEngine.UIElements.DragAndDropData dragAndDropData, global::Unity.Hierarchy.HierarchyView view, global::UnityEngine.EventModifiers eventModifiers)
		{
			Parent = parent;
			Target = target;
			InsertAtIndex = insertAtIndex;
			DropPosition = dropPosition;
			m_DragAndDropData = dragAndDropData;
			View = view;
			EventModifiers = eventModifiers;
		}
	}
}
