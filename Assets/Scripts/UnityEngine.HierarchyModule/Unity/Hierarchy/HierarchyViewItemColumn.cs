namespace Unity.Hierarchy
{
	[global::UnityEngine.Bindings.VisibleToOtherModules(new string[] { "UnityEditor.HierarchyModule" })]
	internal class HierarchyViewItemColumn : global::UnityEngine.UIElements.Column
	{
		internal const string k_HierarchyNameColumnName = "HierarchyViewColumn Name";

		[global::Unity.Scripting.LifecycleManagement.NoAutoStaticsCleanup]
		private static readonly global::UnityEngine.UIElements.BindingId k_ColumnStretchableProperty = "stretchable";

		private static readonly global::UnityEngine.UIElements.Length k_DefaultMinimumWidth = new global::UnityEngine.UIElements.Length(35f, global::UnityEngine.UIElements.LengthUnit.Pixel);

		private static readonly global::UnityEngine.UIElements.Length k_MinimumWidth = new global::UnityEngine.UIElements.Length(200f, global::UnityEngine.UIElements.LengthUnit.Pixel);

		private static readonly global::UnityEngine.UIElements.Length k_DefaultWidth = new global::UnityEngine.UIElements.Length(300f, global::UnityEngine.UIElements.LengthUnit.Pixel);

		private readonly global::Unity.Hierarchy.HierarchyView m_View;

		private readonly global::UnityEngine.Pool.ObjectPool<global::Unity.Hierarchy.HierarchyViewItemContainer> m_ViewItemContainerPool;

		public event global::System.Action<global::Unity.Hierarchy.HierarchyViewItem> OnBindItem;

		public event global::System.Action<global::Unity.Hierarchy.HierarchyViewItem> OnUnbindItem;

		public HierarchyViewItemColumn(global::Unity.Hierarchy.HierarchyView view)
		{
			m_View = view;
			m_ViewItemContainerPool = new global::UnityEngine.Pool.ObjectPool<global::Unity.Hierarchy.HierarchyViewItemContainer>(() => new global::Unity.Hierarchy.HierarchyViewItemContainer());
			base.title = "Name";
			base.name = "HierarchyViewColumn Name";
			ApplyDefaultColumnProperties();
			base.makeCell = MakeCell;
			base.destroyCell = DestroyCell;
			base.bindCell = BindCell;
			base.unbindCell = UnbindCell;
			base.propertyChanged += delegate(object _, global::UnityEngine.UIElements.BindablePropertyChangedEventArgs args)
			{
				if (args.propertyName == k_ColumnStretchableProperty)
				{
					base.minWidth = (base.stretchable ? k_MinimumWidth : k_DefaultMinimumWidth);
					if (base.width.value < base.minWidth.value)
					{
						base.width = base.minWidth;
					}
				}
			};
		}

		private global::UnityEngine.UIElements.VisualElement MakeCell()
		{
			return m_ViewItemContainerPool.Get();
		}

		internal void ApplyDefaultColumnProperties()
		{
			base.width = k_DefaultWidth;
			base.minWidth = (base.stretchable ? k_MinimumWidth : k_DefaultMinimumWidth);
			base.visible = true;
			base.optional = false;
			base.resizable = true;
			base.sortable = false;
		}

		private void DestroyCell(global::UnityEngine.UIElements.VisualElement element)
		{
			if (element == null)
			{
				throw new global::System.ArgumentNullException("element");
			}
			if (!(element is global::Unity.Hierarchy.HierarchyViewItemContainer hierarchyViewItemContainer))
			{
				throw new global::System.ArgumentException("Expected element to be a HierarchyViewItemContainer");
			}
			hierarchyViewItemContainer.ReleaseViewItem();
			m_ViewItemContainerPool.Release(hierarchyViewItemContainer);
		}

		private void BindCell(global::UnityEngine.UIElements.VisualElement element, int index)
		{
			if (element == null)
			{
				throw new global::System.ArgumentNullException("element");
			}
			if (!(element is global::Unity.Hierarchy.HierarchyViewItemContainer hierarchyViewItemContainer))
			{
				throw new global::System.ArgumentException("Expected element to be a HierarchyViewItemContainer");
			}
			global::Unity.Hierarchy.HierarchyNode lhs = m_View.ViewModel[index];
			if (lhs == global::Unity.Hierarchy.HierarchyNode.Null)
			{
				throw new global::System.InvalidOperationException("Expected node to be valid");
			}
			if (m_View.Source.Exists(in lhs))
			{
				hierarchyViewItemContainer.Bind(in lhs, m_View);
				this.OnBindItem?.Invoke(hierarchyViewItemContainer.ViewItem);
			}
		}

		private void UnbindCell(global::UnityEngine.UIElements.VisualElement element, int index)
		{
			if (element == null)
			{
				throw new global::System.ArgumentNullException("element");
			}
			if (!(element is global::Unity.Hierarchy.HierarchyViewItemContainer hierarchyViewItemContainer))
			{
				throw new global::System.ArgumentException("Expected element to be a HierarchyViewItemContainer");
			}
			if (hierarchyViewItemContainer.ViewItem != null)
			{
				this.OnUnbindItem?.Invoke(hierarchyViewItemContainer.ViewItem);
			}
			hierarchyViewItemContainer.Unbind();
		}
	}
}
