namespace Unity.Hierarchy
{
	[global::UnityEngine.Bindings.VisibleToOtherModules(new string[] { "UnityEditor.HierarchyModule" })]
	internal static class HierarchyViewOperationExtension
	{
		public static void OnCut(this global::Unity.Hierarchy.HierarchyView view)
		{
			foreach (global::Unity.Hierarchy.HierarchyNodeTypeHandler item in view.Source.EnumerateNodeTypeHandlers())
			{
				if (item is global::Unity.Hierarchy.IHierarchyEditorNodeTypeHandler hierarchyEditorNodeTypeHandler && hierarchyEditorNodeTypeHandler.CanCut(view))
				{
					hierarchyEditorNodeTypeHandler.OnCut(view);
				}
			}
		}

		public static void OnCopy(this global::Unity.Hierarchy.HierarchyView view)
		{
			foreach (global::Unity.Hierarchy.HierarchyNodeTypeHandler item in view.Source.EnumerateNodeTypeHandlers())
			{
				if (item is global::Unity.Hierarchy.IHierarchyEditorNodeTypeHandler hierarchyEditorNodeTypeHandler && hierarchyEditorNodeTypeHandler.CanCopy(view))
				{
					hierarchyEditorNodeTypeHandler.OnCopy(view);
				}
			}
		}

		public static void OnPaste(this global::Unity.Hierarchy.HierarchyView view)
		{
			foreach (global::Unity.Hierarchy.HierarchyNodeTypeHandler item in view.Source.EnumerateNodeTypeHandlers())
			{
				if (item is global::Unity.Hierarchy.IHierarchyEditorNodeTypeHandler hierarchyEditorNodeTypeHandler && hierarchyEditorNodeTypeHandler.CanPaste(view))
				{
					hierarchyEditorNodeTypeHandler.OnPaste(view);
				}
			}
		}

		public static void OnPasteAsChild(this global::Unity.Hierarchy.HierarchyView view, bool keepWorldPos)
		{
			foreach (global::Unity.Hierarchy.HierarchyNodeTypeHandler item in view.Source.EnumerateNodeTypeHandlers())
			{
				if (item is global::Unity.Hierarchy.IHierarchyEditorNodeTypeHandler hierarchyEditorNodeTypeHandler && hierarchyEditorNodeTypeHandler.CanPasteAsChild(view))
				{
					hierarchyEditorNodeTypeHandler.OnPasteAsChild(view, keepWorldPos);
				}
			}
		}

		public static void OnDuplicate(this global::Unity.Hierarchy.HierarchyView view)
		{
			foreach (global::Unity.Hierarchy.HierarchyNodeTypeHandler item in view.Source.EnumerateNodeTypeHandlers())
			{
				if (item is global::Unity.Hierarchy.IHierarchyEditorNodeTypeHandler hierarchyEditorNodeTypeHandler && hierarchyEditorNodeTypeHandler.CanDuplicate(view))
				{
					hierarchyEditorNodeTypeHandler.OnDuplicate(view);
				}
			}
		}

		public static void OnDelete(this global::Unity.Hierarchy.HierarchyView view)
		{
			foreach (global::Unity.Hierarchy.HierarchyNodeTypeHandler item in view.Source.EnumerateNodeTypeHandlers())
			{
				if (item is global::Unity.Hierarchy.IHierarchyEditorNodeTypeHandler hierarchyEditorNodeTypeHandler && hierarchyEditorNodeTypeHandler.CanDelete(view))
				{
					hierarchyEditorNodeTypeHandler.OnDelete(view);
				}
			}
		}

		public static void OnSetName(this global::Unity.Hierarchy.HierarchyView view, in global::Unity.Hierarchy.HierarchyNode node)
		{
			view.BeginRename(in node);
		}

		public static global::Unity.Hierarchy.HierarchyViewItem GetHierarchyViewItemForNode(this global::Unity.Hierarchy.HierarchyView view, in global::Unity.Hierarchy.HierarchyNode node)
		{
			if (node == global::Unity.Hierarchy.HierarchyNode.Null)
			{
				return null;
			}
			int num = view.ViewModel.IndexOf(in node);
			if (num < 0)
			{
				return null;
			}
			return global::UnityEngine.UIElements.UQueryExtensions.Q<global::Unity.Hierarchy.HierarchyViewItem>(view.ListView.GetRootElementForIndex(num)?);
		}

		[global::UnityEngine.Bindings.VisibleToOtherModules(new string[] { "UnityEditor.HierarchyModule" })]
		internal static bool DoesSelectedNodesHaveChildren(this global::Unity.Hierarchy.HierarchyView view)
		{
			global::Unity.Hierarchy.HierarchyViewModel viewModel = view.ViewModel;
			global::Unity.Hierarchy.HierarchyViewModelNodesEnumerable.Enumerator enumerator = viewModel.EnumerateNodesWithAllFlags(global::Unity.Hierarchy.HierarchyNodeFlags.Selected).GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (viewModel.GetChildrenCount(in enumerator.Current) > 0)
				{
					return true;
				}
			}
			return false;
		}
	}
}
