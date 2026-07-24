namespace Unity.Hierarchy
{
	[global::UnityEngine.Bindings.VisibleToOtherModules(new string[] { "UnityEditor.HierarchyModule", "UnityEditor.UIToolkitAuthoringModule" })]
	internal class HierarchyViewColumnUtility
	{
		public const string k_ToggleIcon = "toggle-icon";

		public const string k_CellPropField = "cell-prop-field";

		public static global::Unity.Hierarchy.HierarchyViewCell GetCellFromTarget(global::UnityEngine.UIElements.VisualElement target)
		{
			return (global::Unity.Hierarchy.HierarchyViewCell)target.parent;
		}

		public static global::Unity.Hierarchy.HierarchyViewCellValueEditor<TModel, TEditor, TValue> BindCellToValueEditor<TModel, TEditor, TValue>(TModel model, global::Unity.Hierarchy.HierarchyViewCell cell, global::Unity.Hierarchy.HierarchyViewColumnContextPool<global::Unity.Hierarchy.HierarchyViewCellValueEditor<TModel, TEditor, TValue>> pool, params string[] classes) where TEditor : global::UnityEngine.UIElements.VisualElement, global::UnityEngine.UIElements.INotifyValueChanged<TValue>, new()
		{
			TEditor orCreateEditor = GetOrCreateEditor<TEditor>(cell, classes);
			global::Unity.Hierarchy.HierarchyViewCellValueEditor<TModel, TEditor, TValue> hierarchyViewCellValueEditor = pool.Get(cell.View.GetHashCode());
			hierarchyViewCellValueEditor.Bind(model, cell, orCreateEditor);
			return hierarchyViewCellValueEditor;
		}

		public static void UnbindCellFromValueEditor<TModel, TEditor, TValue>(global::Unity.Hierarchy.HierarchyViewCell cell, global::Unity.Hierarchy.HierarchyViewColumnContextPool<global::Unity.Hierarchy.HierarchyViewCellValueEditor<TModel, TEditor, TValue>> pool) where TEditor : global::UnityEngine.UIElements.VisualElement, global::UnityEngine.UIElements.INotifyValueChanged<TValue>, new()
		{
			if (cell.userData is global::Unity.Hierarchy.HierarchyViewCellValueEditor<TModel, TEditor, TValue> hierarchyViewCellValueEditor)
			{
				pool.Release(cell.View.GetHashCode(), hierarchyViewCellValueEditor);
				hierarchyViewCellValueEditor.Unbind();
			}
		}

		public static TEditor GetOrCreateEditor<TEditor>(global::Unity.Hierarchy.HierarchyViewCell cell, params string[] classes) where TEditor : global::UnityEngine.UIElements.VisualElement, new()
		{
			TEditor val = global::UnityEngine.UIElements.UQueryExtensions.Q<TEditor>(cell);
			if (val == null)
			{
				val = new TEditor();
				AddToClassList(val, classes);
				cell.Add(val);
			}
			return val;
		}

		internal static global::UnityEngine.UIElements.VisualElement AddToClassList(global::UnityEngine.UIElements.VisualElement element, params string[] classes)
		{
			foreach (string className in classes)
			{
				element.AddToClassList(className);
			}
			return element;
		}

		[global::UnityEngine.Bindings.VisibleToOtherModules(new string[] { "UnityEditor.UIToolkitAuthoringModule" })]
		internal static global::Unity.Hierarchy.HierarchyViewCellValueEditor<TModel, TEditor, TValue> CreateCellValueEditor<TModel, TEditor, TValue>(TModel model, global::Unity.Hierarchy.HierarchyViewCell cell, global::System.Func<global::Unity.Hierarchy.HierarchyViewCellValueEditor<TModel, TEditor, TValue>, TValue> getModelValue, global::System.Action<global::Unity.Hierarchy.HierarchyViewCellValueEditor<TModel, TEditor, TValue>, TValue> setModelValue, global::System.Func<global::Unity.Hierarchy.HierarchyViewCellValueEditor<TModel, TEditor, TValue>, TValue, bool> isDefaultValue, params string[] classes) where TEditor : global::UnityEngine.UIElements.VisualElement, global::UnityEngine.UIElements.INotifyValueChanged<TValue>, new()
		{
			TEditor orCreateEditor = GetOrCreateEditor<TEditor>(cell, classes);
			global::Unity.Hierarchy.HierarchyViewCellValueEditor<TModel, TEditor, TValue> hierarchyViewCellValueEditor = new global::Unity.Hierarchy.HierarchyViewCellValueEditor<TModel, TEditor, TValue>(getModelValue, setModelValue, isDefaultValue);
			hierarchyViewCellValueEditor.Bind(model, cell, orCreateEditor);
			return hierarchyViewCellValueEditor;
		}

		internal static int GetVisibleIndex(global::Unity.Hierarchy.HierarchyViewState viewState, global::UnityEngine.UIElements.Column c)
		{
			string columnId = GetColumnId(c);
			global::Unity.Hierarchy.HierarchyViewColumnState[] columns = viewState.Columns;
			foreach (global::Unity.Hierarchy.HierarchyViewColumnState hierarchyViewColumnState in columns)
			{
				if (hierarchyViewColumnState.ColumnId == columnId)
				{
					return hierarchyViewColumnState.Index;
				}
			}
			return GetColumnDefaultPriority(c);
		}

		internal static string GetColumnId(global::UnityEngine.UIElements.Column col)
		{
			if (col is global::Unity.Hierarchy.HierarchyViewColumn hierarchyViewColumn)
			{
				return hierarchyViewColumn.Descriptor.Id;
			}
			if (col is global::Unity.Hierarchy.HierarchyViewItemColumn)
			{
				return "HierarchyViewColumn Name";
			}
			return null;
		}

		internal static int GetColumnDefaultPriority(global::UnityEngine.UIElements.Column col)
		{
			if (col is global::Unity.Hierarchy.HierarchyViewColumn hierarchyViewColumn)
			{
				return hierarchyViewColumn.Descriptor.DefaultPriority;
			}
			if (col is global::Unity.Hierarchy.HierarchyViewItemColumn)
			{
				return 0;
			}
			return 1000;
		}

		internal static global::UnityEngine.UIElements.Column GetColumnWithId(global::System.Collections.Generic.IEnumerable<global::UnityEngine.UIElements.Column> columns, string id)
		{
			foreach (global::UnityEngine.UIElements.Column column in columns)
			{
				if (GetColumnId(column) == id)
				{
					return column;
				}
			}
			return null;
		}
	}
}
