namespace Unity.Hierarchy
{
	[global::UnityEngine.Bindings.VisibleToOtherModules]
	internal interface IHierarchyEditorNodeTypeHandler
	{
		bool CanCut(global::Unity.Hierarchy.HierarchyView view);

		bool OnCut(global::Unity.Hierarchy.HierarchyView view);

		bool CanCopy(global::Unity.Hierarchy.HierarchyView view);

		bool OnCopy(global::Unity.Hierarchy.HierarchyView view);

		bool CanPaste(global::Unity.Hierarchy.HierarchyView view);

		bool OnPaste(global::Unity.Hierarchy.HierarchyView view);

		bool CanPasteAsChild(global::Unity.Hierarchy.HierarchyView view);

		bool OnPasteAsChild(global::Unity.Hierarchy.HierarchyView view, bool keepWorldPos);

		bool CanSetName(global::Unity.Hierarchy.HierarchyView view, in global::Unity.Hierarchy.HierarchyNode node);

		bool OnSetName(global::Unity.Hierarchy.HierarchyView view, in global::Unity.Hierarchy.HierarchyNode node, string name);

		string GetDisplayName(global::Unity.Hierarchy.HierarchyView view, in global::Unity.Hierarchy.HierarchyNode node);

		bool CanDuplicate(global::Unity.Hierarchy.HierarchyView view);

		bool OnDuplicate(global::Unity.Hierarchy.HierarchyView view);

		bool CanDelete(global::Unity.Hierarchy.HierarchyView view);

		bool OnDelete(global::Unity.Hierarchy.HierarchyView view);

		bool CanFindReferences(global::Unity.Hierarchy.HierarchyView view);

		bool OnFindReferences(global::Unity.Hierarchy.HierarchyView view);

		bool CanDoubleClick(global::Unity.Hierarchy.HierarchyView view, in global::Unity.Hierarchy.HierarchyNode node);

		bool OnDoubleClick(global::Unity.Hierarchy.HierarchyView view, in global::Unity.Hierarchy.HierarchyNode node);

		void GetTooltip(global::Unity.Hierarchy.HierarchyViewItem item, bool isFiltering, global::System.Text.StringBuilder tooltip);

		void PopulateContextMenu(global::Unity.Hierarchy.HierarchyView view, global::Unity.Hierarchy.HierarchyViewItem item, global::UnityEngine.UIElements.DropdownMenu menu);

		bool AcceptParent(global::Unity.Hierarchy.HierarchyView view, in global::Unity.Hierarchy.HierarchyNode parent);

		bool AcceptChild(global::Unity.Hierarchy.HierarchyView view, in global::Unity.Hierarchy.HierarchyNode child);

		bool CanStartDrag(global::Unity.Hierarchy.HierarchyView view, global::System.ReadOnlySpan<global::Unity.Hierarchy.HierarchyNode> nodes);

		void OnStartDrag(in global::Unity.Hierarchy.HierarchyViewDragAndDropSetupData data);

		global::UnityEngine.UIElements.DragVisualMode CanDrop(in global::Unity.Hierarchy.HierarchyViewDragAndDropHandlingData data);

		global::UnityEngine.UIElements.DragVisualMode OnDrop(in global::Unity.Hierarchy.HierarchyViewDragAndDropHandlingData data);
	}
}
