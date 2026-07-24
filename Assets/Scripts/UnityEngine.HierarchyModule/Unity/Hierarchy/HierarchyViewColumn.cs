namespace Unity.Hierarchy
{
	internal sealed class HierarchyViewColumn : global::UnityEngine.UIElements.Column
	{
		internal const string k_NonDefaultValue = "non-default-value";

		private readonly global::Unity.Hierarchy.HierarchyView m_View;

		private readonly global::System.Collections.Generic.List<global::Unity.Hierarchy.HierarchyViewCellDescriptor> m_CellDescriptors = new global::System.Collections.Generic.List<global::Unity.Hierarchy.HierarchyViewCellDescriptor>();

		public readonly global::Unity.Hierarchy.HierarchyViewColumnDescriptor Descriptor;

		internal global::Unity.Hierarchy.HierarchyView View => m_View;

		public global::System.Collections.Generic.IReadOnlyCollection<global::Unity.Hierarchy.HierarchyViewCellDescriptor> CellDescriptors => m_CellDescriptors;

		public HierarchyViewColumn(global::Unity.Hierarchy.HierarchyView view, global::Unity.Hierarchy.HierarchyViewColumnDescriptor descriptor)
		{
			m_View = view;
			Descriptor = descriptor;
			base.name = Descriptor.Id;
			base.resizable = true;
			base.stretchable = false;
			base.sortable = false;
			base.title = Descriptor.Title;
			if (Descriptor.MakeHeader != null)
			{
				base.makeHeader = (global::System.Func<global::UnityEngine.UIElements.VisualElement>)global::System.Delegate.Combine(base.makeHeader, new global::System.Func<global::UnityEngine.UIElements.VisualElement>(MakeHeader));
				base.bindHeader = (global::System.Action<global::UnityEngine.UIElements.VisualElement>)global::System.Delegate.Combine(base.bindHeader, new global::System.Action<global::UnityEngine.UIElements.VisualElement>(BindHeader));
				base.unbindHeader = (global::System.Action<global::UnityEngine.UIElements.VisualElement>)global::System.Delegate.Combine(base.unbindHeader, new global::System.Action<global::UnityEngine.UIElements.VisualElement>(UnbindHeader));
				base.destroyHeader = (global::System.Action<global::UnityEngine.UIElements.VisualElement>)global::System.Delegate.Combine(base.destroyHeader, new global::System.Action<global::UnityEngine.UIElements.VisualElement>(DestroyHeader));
			}
			else if ((bool)Descriptor.Icon)
			{
				base.icon = global::UnityEngine.UIElements.Background.FromTexture2D(Descriptor.Icon);
			}
			if (descriptor.DefaultWidth > 0)
			{
				base.width = descriptor.DefaultWidth;
			}
			base.makeCell = (global::System.Func<global::UnityEngine.UIElements.VisualElement>)global::System.Delegate.Combine(base.makeCell, new global::System.Func<global::UnityEngine.UIElements.VisualElement>(MakeCell));
			base.bindCell = (global::System.Action<global::UnityEngine.UIElements.VisualElement, int>)global::System.Delegate.Combine(base.bindCell, new global::System.Action<global::UnityEngine.UIElements.VisualElement, int>(BindCell));
			base.unbindCell = (global::System.Action<global::UnityEngine.UIElements.VisualElement, int>)global::System.Delegate.Combine(base.unbindCell, new global::System.Action<global::UnityEngine.UIElements.VisualElement, int>(UnbindCell));
		}

		public void AddCell(global::Unity.Hierarchy.HierarchyViewCellDescriptor desc)
		{
			if (!desc.ValidForColumn(Descriptor))
			{
				global::UnityEngine.Debug.LogError("Cannot register Cell: " + desc.ColumnId + " with Column: " + Descriptor.Id);
				return;
			}
			foreach (global::Unity.Hierarchy.HierarchyViewCellDescriptor cellDescriptor in CellDescriptors)
			{
				if (cellDescriptor.HandlerType == desc.HandlerType)
				{
					global::UnityEngine.Debug.LogError($"Cell: for NodeType {desc.HandlerType} is already registered.");
					return;
				}
			}
			m_CellDescriptors.Add(desc);
		}

		internal void ApplyDefaultColumnProperties()
		{
			if (Descriptor.DefaultWidth > 0)
			{
				SetWidth(this, Descriptor.DefaultWidth);
			}
			base.visible = Descriptor.DefaultVisibility;
		}

		internal static void SetWidth(global::UnityEngine.UIElements.Column col, float newWidth)
		{
			if (!(newWidth <= 0f))
			{
				col.width = newWidth;
				if (col.minWidth.value > newWidth)
				{
					col.minWidth = newWidth;
				}
			}
		}

		private global::UnityEngine.UIElements.VisualElement MakeHeader()
		{
			return Descriptor.MakeHeader();
		}

		private void BindHeader(global::UnityEngine.UIElements.VisualElement header)
		{
			Descriptor?.BindHeader(header, m_View);
		}

		private void UnbindHeader(global::UnityEngine.UIElements.VisualElement header)
		{
			Descriptor?.UnbindHeader(header, m_View);
		}

		private void DestroyHeader(global::UnityEngine.UIElements.VisualElement header)
		{
			Descriptor?.DestroyHeader(header, m_View);
		}

		private global::UnityEngine.UIElements.VisualElement MakeCell()
		{
			return new global::Unity.Hierarchy.HierarchyViewCell(m_View, this);
		}

		internal void BindColumn(global::Unity.Hierarchy.HierarchyView view)
		{
			Descriptor.InvokeBindColumn(this, view);
			foreach (global::Unity.Hierarchy.HierarchyViewCellDescriptor cellDescriptor in CellDescriptors)
			{
				cellDescriptor.InvokeBindColumn(Descriptor, view);
			}
		}

		internal void UnbindColumn(global::Unity.Hierarchy.HierarchyView view)
		{
			foreach (global::Unity.Hierarchy.HierarchyViewCellDescriptor cellDescriptor in CellDescriptors)
			{
				cellDescriptor.InvokeUnbindColumn(Descriptor, view);
			}
			if (Descriptor.UnbindHeader != null || Descriptor.DestroyHeader != null)
			{
				global::System.Collections.Generic.List<global::UnityEngine.UIElements.VisualElement> list = global::UnityEngine.UIElements.UQueryExtensions.Query<global::UnityEngine.UIElements.VisualElement>(view.ListView, null, "unity-multi-column-header__column").ToList();
				foreach (global::UnityEngine.UIElements.VisualElement item in list)
				{
					if (item.name == Descriptor.Id)
					{
						global::UnityEngine.UIElements.VisualElement arg = global::UnityEngine.UIElements.UQueryExtensions.Q(item, null, "unity-multi-column-header__column__content");
						Descriptor.UnbindHeader?.Invoke(arg, view);
						Descriptor.DestroyHeader?.Invoke(arg, view);
						break;
					}
				}
			}
			Descriptor.InvokeUnbindColumn(this, view);
		}

		private void BindCell(global::UnityEngine.UIElements.VisualElement cellElement, int index)
		{
			if (!(cellElement is global::Unity.Hierarchy.HierarchyViewCell hierarchyViewCell))
			{
				return;
			}
			global::Unity.Hierarchy.HierarchyNode lhs = m_View.ViewModel[index];
			if (lhs == global::Unity.Hierarchy.HierarchyNode.Null || !m_View.Source.Exists(in lhs))
			{
				return;
			}
			hierarchyViewCell.Node = lhs;
			hierarchyViewCell.NodeIndex = index;
			hierarchyViewCell.Handler = m_View.Source.GetNodeTypeHandler(in lhs);
			if (hierarchyViewCell.Handler == null)
			{
				return;
			}
			foreach (global::Unity.Hierarchy.HierarchyViewCellDescriptor cellDescriptor in CellDescriptors)
			{
				if (cellDescriptor.HandlerType == null || cellDescriptor.HandlerType == hierarchyViewCell.Handler.GetType())
				{
					hierarchyViewCell.Descriptor = cellDescriptor;
					break;
				}
			}
			if (hierarchyViewCell.Descriptor != null)
			{
				hierarchyViewCell.BindCell();
			}
		}

		private void UnbindCell(global::UnityEngine.UIElements.VisualElement cellElement, int index)
		{
			if (cellElement is global::Unity.Hierarchy.HierarchyViewCell hierarchyViewCell)
			{
				hierarchyViewCell.UnbindCell();
			}
		}

		public override string ToString()
		{
			return Descriptor.ToString();
		}
	}
}
