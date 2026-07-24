namespace Unity.Hierarchy
{
	[global::UnityEngine.Bindings.VisibleToOtherModules(new string[] { "UnityEditor.HierarchyModule", "UnityEditor.UIToolkitAuthoringModule" })]
	internal sealed class HierarchyViewItem : global::UnityEngine.UIElements.VisualElement
	{
		internal delegate void ExpandedStateChangedEventHandler(in global::Unity.Hierarchy.HierarchyNode node, bool isExpanded, bool recursive);

		private const string k_UnityListViewItem = "unity-list-view__item";

		private const string k_UnityTreeViewItem = "unity-tree-view__item";

		private const string k_UnityTreeViewItemToggle = "unity-tree-view__item-toggle";

		private const string k_UnityToggleCheckmark = "unity-toggle__checkmark";

		private const string k_HierarchyItemContainer = "hierarchy-item__container";

		private const string k_HierarchyItemOverrideBarContainer = "hierarchy-item__override-bar-container";

		private const string k_HierarchyItemIcon = "hierarchy-item__icon";

		private const string k_HierarchyItemIconCut = "hierarchy-item__icon--cut";

		private const string k_HierarchyItemOverlayIcon = "hierarchy-item__overlay-icon";

		private const string k_HierarchyItemName = "hierarchy-item__name";

		private const string k_HierarchyItemLeftContainer = "hierarchy-item__left-container";

		private const string k_HierarchyItemLeftCustomSection = "hierarchy-item__left-custom-section";

		private const string k_HierarchyItemRightContainer = "hierarchy-item__right-container";

		private const string k_HierarchyItemRightArrowButton = "hierarchy-item__right-arrow-button";

		private const string k_HierarchyItemToggleHidden = "hierarchy-item__toggle--hidden";

		internal const int k_IndentWidth = 14;

		private global::Unity.Hierarchy.HierarchyNode m_Node;

		private global::Unity.Hierarchy.HierarchyNodeTypeHandler m_Handler;

		private global::Unity.Hierarchy.HierarchyView m_View;

		private readonly global::UnityEngine.UIElements.Toggle m_Toggle;

		private readonly global::UnityEngine.UIElements.VisualElement m_OverrideBarContainer;

		private readonly global::UnityEngine.UIElements.VisualElement m_Icon;

		private readonly global::UnityEngine.UIElements.VisualElement m_OverlayIcon;

		private readonly global::Unity.Hierarchy.HierarchyViewItemName m_Name;

		private readonly global::System.Lazy<global::UnityEngine.UIElements.Button> m_NavigateIntoButton;

		private readonly global::UnityEngine.UIElements.VisualElement m_LeftCustomContainer;

		private readonly global::UnityEngine.UIElements.VisualElement m_RightCustomContainer;

		private readonly global::UnityEngine.UIElements.VisualElement m_LeftContainer;

		internal global::UnityEngine.UIElements.VisualElement LeftContainer => m_LeftContainer;

		public global::Unity.Hierarchy.HierarchyNodeType NodeType => m_Handler?.GetNodeType() ?? global::Unity.Hierarchy.HierarchyNodeType.Null;

		public ref readonly global::Unity.Hierarchy.HierarchyNode Node => ref m_Node;

		public global::UnityEngine.UIElements.Label Name => m_Name.Label;

		public global::UnityEngine.UIElements.VisualElement Icon => m_Icon;

		public global::UnityEngine.UIElements.VisualElement OverlayIcon => m_OverlayIcon;

		public global::UnityEngine.UIElements.VisualElement LeftCustomContainer => m_LeftCustomContainer;

		public global::UnityEngine.UIElements.VisualElement RightCustomContainer => m_RightCustomContainer;

		public global::UnityEngine.UIElements.Button NavigateIntoButton => m_NavigateIntoButton.Value;

		public global::UnityEngine.UIElements.VisualElement OverrideBarContainer => m_OverrideBarContainer;

		public global::UnityEngine.UIElements.Toggle Toggle => m_Toggle;

		public global::UnityEngine.UIElements.VisualElement RowContainer
		{
			get
			{
				global::UnityEngine.UIElements.VisualElement visualElement = base.parent;
				while (visualElement != null && !visualElement.ClassListContains("unity-multi-column-view__row-container"))
				{
					visualElement = visualElement.parent;
				}
				return visualElement;
			}
		}

		public global::Unity.Hierarchy.HierarchyNodeTypeHandler Handler => m_Handler;

		public global::Unity.Hierarchy.HierarchyView View => m_View;

		internal bool Bound => m_Node != global::Unity.Hierarchy.HierarchyNode.Null || m_View != null;

		internal event global::Unity.Hierarchy.HierarchyViewItem.ExpandedStateChangedEventHandler ExpandedStateChanged;

		internal HierarchyViewItem()
		{
			base.name = "unity-tree-view__item";
			base.style.flexDirection = global::UnityEngine.UIElements.FlexDirection.Row;
			global::UnityEngine.UIElements.VisualElement visualElement = new global::UnityEngine.UIElements.VisualElement();
			visualElement.AddToClassList("hierarchy-item__container");
			base.hierarchy.Add(visualElement);
			m_LeftContainer = new global::UnityEngine.UIElements.VisualElement();
			m_LeftContainer.AddToClassList("hierarchy-item__left-container");
			m_OverrideBarContainer = new global::UnityEngine.UIElements.VisualElement();
			m_OverrideBarContainer.AddToClassList("hierarchy-item__override-bar-container");
			m_Toggle = new global::UnityEngine.UIElements.Toggle();
			m_Toggle.AddToClassList("unity-tree-view__item-toggle");
			m_Toggle.AddToClassList(global::UnityEngine.UIElements.Foldout.toggleUssClassName);
			global::UnityEngine.UIElements.UQueryExtensions.Q(m_Toggle, null, "unity-toggle__checkmark").style.marginTop = 0f;
			m_Toggle.focusable = false;
			m_Icon = new global::UnityEngine.UIElements.VisualElement();
			m_Icon.AddToClassList("hierarchy-item__icon");
			m_OverlayIcon = new global::UnityEngine.UIElements.VisualElement();
			m_OverlayIcon.AddToClassList("hierarchy-item__overlay-icon");
			m_Name = new global::Unity.Hierarchy.HierarchyViewItemName();
			m_Name.AddToClassList("hierarchy-item__name");
			m_LeftCustomContainer = new global::UnityEngine.UIElements.VisualElement();
			m_LeftCustomContainer.AddToClassList("hierarchy-item__left-custom-section");
			m_LeftContainer.Add(m_OverrideBarContainer);
			m_LeftContainer.Add(m_Toggle);
			m_LeftContainer.Add(m_Icon);
			m_LeftContainer.Add(m_OverlayIcon);
			m_LeftContainer.Add(m_Name);
			m_LeftContainer.Add(m_LeftCustomContainer);
			m_RightCustomContainer = new global::UnityEngine.UIElements.VisualElement();
			m_RightCustomContainer.AddToClassList("hierarchy-item__right-container");
			m_NavigateIntoButton = new global::System.Lazy<global::UnityEngine.UIElements.Button>(delegate
			{
				global::UnityEngine.UIElements.Button button = new global::UnityEngine.UIElements.Button();
				button.AddToClassList("hierarchy-item__right-arrow-button");
				button.RemoveFromClassList(global::UnityEngine.UIElements.Button.ussClassName);
				button.style.display = global::UnityEngine.UIElements.DisplayStyle.None;
				m_RightCustomContainer.Add(button);
				return button;
			});
			visualElement.Add(m_OverrideBarContainer);
			visualElement.Add(m_LeftContainer);
			visualElement.Add(m_RightCustomContainer);
			AddToClassList("unity-tree-view__item");
			AddToClassList("unity-list-view__item");
		}

		internal void Bind(in global::Unity.Hierarchy.HierarchyNode node, global::Unity.Hierarchy.HierarchyView view)
		{
			if (Bound)
			{
				throw new global::System.InvalidOperationException("Cannot bind a hierarchy view item that is already bound.");
			}
			m_Node = node;
			m_Handler = view.Source.GetNodeTypeHandler(in node);
			m_View = view;
			global::Unity.Hierarchy.HierarchyViewModel viewModel = m_View.ViewModel;
			global::Unity.Hierarchy.HierarchyNode lhs = viewModel.GetRoot();
			int depth = viewModel.GetDepth(in m_Node);
			int num = ((lhs == m_View.Source.Root) ? depth : (depth - viewModel.GetDepth(in lhs) - 1));
			bool flag = !m_View.Filtering;
			int num2 = (flag ? (num * 14) : 0);
			global::UnityEngine.UIElements.Translate value = m_LeftContainer.style.translate.value;
			m_LeftContainer.style.translate = new global::UnityEngine.UIElements.Translate(global::UnityEngine.UIElements.AlignmentUtils.CeilToPanelPixelSize(m_LeftContainer, num2), value.y, value.z);
			bool flag2 = flag && viewModel.GetChildrenCount(in m_Node) > 0;
			m_Toggle.EnableInClassList("hierarchy-item__toggle--hidden", !flag2);
			bool flag3 = viewModel.HasAllFlags(in m_Node, global::Unity.Hierarchy.HierarchyNodeFlags.Expanded);
			m_Toggle.SetValueWithoutNotify(flag2 && flag3);
			Icon.EnableInClassList("hierarchy-item__icon--cut", viewModel.HasAllFlags(in m_Node, global::Unity.Hierarchy.HierarchyNodeFlags.Cut));
			if (m_Handler is global::Unity.Hierarchy.IHierarchyEditorNodeTypeHandler hierarchyEditorNodeTypeHandler)
			{
				m_Name.Text = hierarchyEditorNodeTypeHandler.GetDisplayName(m_View, in m_Node);
			}
			else
			{
				m_Name.Text = m_View.Source.GetName(in m_Node);
			}
			m_View.InvokeBindViewItem(this);
			m_Name.OnBeginRename += OnBeginRename;
			m_Name.OnEndRename += OnEndRename;
		}

		internal void Unbind()
		{
			if (Bound)
			{
				if (RowContainer != null && RowContainer.ClassListContains("hierarchy - item__ping-base"))
				{
					RowContainer.SendEvent(new global::UnityEngine.UIElements.TransitionEndEvent
					{
						target = RowContainer
					}, global::UnityEngine.UIElements.DispatchMode.Immediate);
					RowContainer.SendEvent(new global::UnityEngine.UIElements.TransitionEndEvent
					{
						target = RowContainer
					}, global::UnityEngine.UIElements.DispatchMode.Immediate);
				}
				m_Node = global::Unity.Hierarchy.HierarchyNode.Null;
				m_View.InvokeUnbindViewItem(this);
				m_Name.OnBeginRename -= OnBeginRename;
				m_Name.OnEndRename -= OnEndRename;
				m_Handler = null;
				m_View = null;
			}
		}

		[global::UnityEngine.UIElements.EventInterest(new global::System.Type[] { typeof(global::UnityEngine.UIElements.TooltipEvent) })]
		[global::UnityEngine.UIElements.EventInterest(new global::System.Type[] { typeof(global::UnityEngine.UIElements.ClickEvent) })]
		protected override void HandleEventBubbleUp(global::UnityEngine.UIElements.EventBase evt)
		{
			if (evt is global::UnityEngine.UIElements.TooltipEvent tooltipEvent)
			{
				bool filtering = m_View.Filtering;
				global::System.Text.StringBuilder stringBuilder = new global::System.Text.StringBuilder();
				m_View.InvokeGetTooltip(this, filtering, stringBuilder);
				if (stringBuilder.Length != 0)
				{
					tooltipEvent.rect = m_Name.worldBound;
					tooltipEvent.tooltip = stringBuilder.ToString();
				}
			}
			else if (evt is global::UnityEngine.UIElements.ClickEvent clickEvent && m_Toggle.visible && m_Toggle.worldBound.Contains(clickEvent.position))
			{
				bool isExpanded = !m_View.ViewModel.HasAllFlags(in m_Node, global::Unity.Hierarchy.HierarchyNodeFlags.Expanded);
				this.ExpandedStateChanged?.Invoke(in m_Node, isExpanded, clickEvent.altKey);
				evt.StopPropagation();
			}
		}

		[global::UnityEngine.UIElements.EventInterest(new global::System.Type[] { typeof(global::UnityEngine.UIElements.PointerDownEvent) })]
		protected override void HandleEventTrickleDown(global::UnityEngine.UIElements.EventBase evt)
		{
			if (evt is global::UnityEngine.UIElements.PointerDownEvent pointerDownEvent)
			{
				global::Unity.Hierarchy.HierarchyView view = m_View;
				if (view != null && view.m_IsRenamingItem && m_Toggle.worldBound.Contains(pointerDownEvent.position))
				{
					pointerDownEvent.StopImmediatePropagation();
				}
			}
		}

		public void BeginRename()
		{
			if (!(m_Node == global::Unity.Hierarchy.HierarchyNode.Null) && (!(m_Handler is global::Unity.Hierarchy.IHierarchyEditorNodeTypeHandler hierarchyEditorNodeTypeHandler) || hierarchyEditorNodeTypeHandler.CanSetName(m_View, in m_Node)))
			{
				m_Name.BeginRename();
			}
		}

		private void OnBeginRename()
		{
			m_View.SetRenamingItem(this);
		}

		private void OnEndRename(string text, bool canceled)
		{
			m_View.SetRenamingItem(null);
			if (!canceled && !(m_Node == global::Unity.Hierarchy.HierarchyNode.Null) && !string.IsNullOrEmpty(text))
			{
				if (m_Handler is global::Unity.Hierarchy.IHierarchyEditorNodeTypeHandler hierarchyEditorNodeTypeHandler)
				{
					hierarchyEditorNodeTypeHandler.OnSetName(m_View, in m_Node, text);
				}
				else
				{
					m_View.Source.SetName(in m_Node, text);
				}
			}
		}
	}
}
