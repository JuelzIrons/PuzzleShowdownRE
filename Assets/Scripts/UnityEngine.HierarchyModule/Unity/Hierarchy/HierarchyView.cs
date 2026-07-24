namespace Unity.Hierarchy
{
	[global::UnityEngine.Bindings.VisibleToOtherModules(new string[] { "UnityEditor.UIToolkitAuthoringModule" })]
	internal sealed class HierarchyView : global::UnityEngine.UIElements.VisualElement, global::System.IDisposable
	{
		private enum UpdateStage
		{
			UpdatingHierarchy = 0,
			UpdatingHierarchyFlattened = 1,
			UpdatingHierarchyViewModel = 2,
			UpdatingListView = 3,
			ExecutePostUpdateActions = 4,
			Count = 5,
			First = 0,
			Last = 4
		}

		private enum UpdateMode
		{
			Update = 0,
			UpdateIncremental = 1,
			UpdateIncrementalTimed = 2
		}

		internal class TestHelper
		{
			public static int FirstUpdateStage => 0;

			public static int LastUpdateStage => 4;

			public static int HierarchyUpdateStage => 0;

			public static int CurrentUpdateStage(global::Unity.Hierarchy.HierarchyView view)
			{
				return (int)view.m_UpdateStage;
			}

			public static bool ViewUpdateNeeded(global::Unity.Hierarchy.HierarchyView view)
			{
				return view.UpdateNeeded;
			}

			public static bool HierarchyUpdateNeeded(global::Unity.Hierarchy.HierarchyView view)
			{
				return view.m_Hierarchy.UpdateNeeded;
			}
		}

		public delegate void SourceHierarchyChangingEventHandler(global::Unity.Hierarchy.Hierarchy oldHierarchy, global::Unity.Hierarchy.Hierarchy newHierarchy, global::Unity.Hierarchy.HierarchyNodeFlags defaultFlags);

		public delegate void SourceHierarchyChangedEventHandler(global::Unity.Hierarchy.Hierarchy hierarchy, global::Unity.Hierarchy.HierarchyNodeFlags defaultFlags);

		public delegate void PopulateContextMenuEventHandler(global::Unity.Hierarchy.HierarchyViewItem item, global::UnityEngine.UIElements.DropdownMenu menu);

		public delegate void GetTooltipEventHandler(global::Unity.Hierarchy.HierarchyViewItem item, bool filtering, global::System.Text.StringBuilder tooltip);

		internal const int k_ItemHeight = 20;

		private const string k_ListViewName = "unity-tree-view__list-view";

		private const string k_HierarchyViewRootStyleName = "hierarchy";

		private const string k_HierarchyViewStyleContainerStyleName = "hierarchy__container";

		private const int k_RenamingDelayMs = 500;

		internal const string k_HierarchyPingBase = "hierarchy - item__ping-base";

		private const string k_HierarchyPingRampIn_Style = "hierarchy-item__ping-ramp-in-style";

		private const string k_HierarchyPingRampIn_Start = "hierarchy-item__ping-ramp-in-start";

		private const string k_HierarchyPingRampOut_Style = "hierarchy-item__ping-ramp-out-style";

		private const string k_HierarchyPingRampOut_Start = "hierarchy-item__ping-ramp-out-start";

		private global::Unity.Hierarchy.Hierarchy m_Hierarchy;

		private global::Unity.Hierarchy.HierarchyFlattened m_HierarchyFlattened;

		private global::Unity.Hierarchy.HierarchyViewModel m_HierarchyViewModel;

		private int m_Version;

		private global::Unity.Hierarchy.HierarchyView.UpdateStage m_UpdateStage = global::Unity.Hierarchy.HierarchyView.UpdateStage.UpdatingHierarchy;

		private readonly global::System.Diagnostics.Stopwatch m_UpdateTimer = new global::System.Diagnostics.Stopwatch();

		private readonly global::Unity.Hierarchy.CircularBuffer<global::System.Action> m_PostUpdateActionQueue = new global::Unity.Hierarchy.CircularBuffer<global::System.Action>(16);

		private readonly global::UnityEngine.UIElements.MultiColumnListView m_MultiColumnListView;

		private readonly global::Unity.Hierarchy.HierarchyViewItemColumn m_NameColumn;

		private readonly global::Unity.Hierarchy.HierarchyViewDragHandler m_DragHandler;

		private readonly global::UnityEngine.UIElements.VisualElement m_ListViewContentContainer;

		private global::UnityEngine.UIElements.VisualElement m_StyleContainer;

		private global::UnityEngine.UIElements.IVisualElementScheduledItem m_ScheduledItem;

		private readonly global::System.Collections.Generic.List<int> m_SelectedIndices = new global::System.Collections.Generic.List<int>();

		private bool m_SelectedIndicesChangedFromPointerDown;

		private int m_LastMouseUpSelectionIndex;

		private global::Unity.Hierarchy.HierarchyViewItem m_RenamingItem;

		internal int m_RenameDelayMs;

		internal bool m_IsRenamingItem => m_RenamingItem != null;

		public global::Unity.Hierarchy.Hierarchy Source => m_Hierarchy;

		public global::Unity.Hierarchy.HierarchyFlattened Flattened => m_HierarchyFlattened;

		public global::Unity.Hierarchy.HierarchyViewModel ViewModel => m_HierarchyViewModel;

		internal global::UnityEngine.UIElements.MultiColumnListView ListView => m_MultiColumnListView;

		public global::UnityEngine.UIElements.VisualElement StyleContainer => m_StyleContainer;

		public string Filter
		{
			get
			{
				return m_HierarchyViewModel.Query.ToString();
			}
			set
			{
				m_HierarchyViewModel.SetQuery(value);
			}
		}

		public bool Filtering => m_HierarchyViewModel.Filtering;

		public bool Updating
		{
			get
			{
				if (m_Hierarchy == null || !m_Hierarchy.IsCreated)
				{
					return false;
				}
				return m_UpdateStage != global::Unity.Hierarchy.HierarchyView.UpdateStage.UpdatingHierarchy || m_Hierarchy.Updating || m_HierarchyFlattened.Updating || m_HierarchyViewModel.Updating;
			}
		}

		public bool UpdateNeeded
		{
			get
			{
				if (m_Hierarchy == null || !m_Hierarchy.IsCreated)
				{
					return false;
				}
				return Updating || DataUpdateNeeded || DisplayUpdateNeeded || ExecutePostUpdateActionsNeeded;
			}
		}

		public float UpdateProgress
		{
			get
			{
				if (!Updating)
				{
					return 100f;
				}
				if (m_UpdateStage == global::Unity.Hierarchy.HierarchyView.UpdateStage.UpdatingHierarchyViewModel)
				{
					return m_HierarchyViewModel.UpdateProgress;
				}
				return 0f;
			}
		}

		internal bool DataUpdateNeeded => m_Hierarchy.UpdateNeeded || m_HierarchyFlattened.UpdateNeeded || m_HierarchyViewModel.UpdateNeeded;

		internal bool DisplayUpdateNeeded => m_Version != m_HierarchyViewModel.Version;

		internal bool ExecutePostUpdateActionsNeeded => m_PostUpdateActionQueue.Count > 0;

		internal global::Unity.Hierarchy.HierarchyViewDragHandler DragHandler => m_DragHandler;

		internal global::Unity.Hierarchy.HierarchyViewItemColumn NameColumn => m_NameColumn;

		public event global::Unity.Hierarchy.HierarchyView.SourceHierarchyChangingEventHandler SourceHierarchyChanging;

		public event global::Unity.Hierarchy.HierarchyView.SourceHierarchyChangedEventHandler SourceHierarchyChanged;

		public event global::System.Action<global::Unity.Hierarchy.HierarchyViewItem> BindViewItem;

		public event global::System.Action<global::Unity.Hierarchy.HierarchyViewItem> UnbindViewItem;

		public event global::Unity.Hierarchy.HierarchyViewModel.FlagsChangedEventHandler FlagsChanged;

		public event global::Unity.Hierarchy.HierarchyView.PopulateContextMenuEventHandler PopulateContextMenu;

		public event global::Unity.Hierarchy.HierarchyView.GetTooltipEventHandler GetTooltip;

		[global::UnityEngine.Bindings.VisibleToOtherModules(new string[] { "UnityEditor.HierarchyModule" })]
		internal event global::System.Action Initializing;

		public HierarchyView()
		{
			AddToClassList("hierarchy");
			global::UnityEngine.UIElements.VisualElementExtensions.AddManipulator(this, new global::UnityEngine.UIElements.ContextualMenuManipulator(InvokePopulateContextMenu));
			global::UnityEngine.UIElements.MultiColumnListView multiColumnListView = new global::UnityEngine.UIElements.MultiColumnListView();
			multiColumnListView.name = "unity-tree-view__list-view";
			multiColumnListView.fixedItemHeight = 20f;
			multiColumnListView.selectionType = global::UnityEngine.UIElements.SelectionType.Multiple;
			multiColumnListView.reorderMode = global::UnityEngine.UIElements.ListViewReorderMode.Simple;
			multiColumnListView.reorderable = true;
			multiColumnListView.itemsSource = null;
			multiColumnListView.columns.stretchMode = global::UnityEngine.UIElements.Columns.StretchMode.Grow;
			m_MultiColumnListView = multiColumnListView;
			m_NameColumn = new global::Unity.Hierarchy.HierarchyViewItemColumn(this);
			m_DragHandler = new global::Unity.Hierarchy.HierarchyViewDragHandler(this);
			m_MultiColumnListView.selectedIndicesChanged += OnSelectedIndicesChanged;
			m_MultiColumnListView.AddToClassList("unity-tree-view__list-view");
			m_MultiColumnListView.RegisterCallback<global::UnityEngine.UIElements.PointerUpEvent>(OnPointerUp);
			m_MultiColumnListView.RegisterCallback<global::UnityEngine.UIElements.KeyDownEvent>(OnKeyDown, global::UnityEngine.UIElements.TrickleDown.TrickleDown);
			m_MultiColumnListView.RegisterCallback<global::UnityEngine.UIElements.NavigationMoveEvent>(OnNavigationMove);
			global::UnityEngine.UIElements.UQueryExtensions.Q(m_MultiColumnListView, null, global::UnityEngine.UIElements.ScrollView.contentAndVerticalScrollUssClassName).RegisterCallback<global::UnityEngine.UIElements.ClickEvent>(OnListViewClick);
			m_MultiColumnListView.columns.Add(m_NameColumn);
			m_NameColumn.stretchable = true;
			m_NameColumn.OnBindItem += OnBindItem;
			m_NameColumn.OnUnbindItem += OnUnbindItem;
			global::UnityEngine.UIElements.ScrollView scrollView = global::UnityEngine.UIElements.UQueryExtensions.Q<global::UnityEngine.UIElements.ScrollView>(m_MultiColumnListView);
			m_ListViewContentContainer = scrollView.contentContainer;
			scrollView.mode = global::UnityEngine.UIElements.ScrollViewMode.VerticalAndHorizontal;
			m_ListViewContentContainer.RegisterCallback<global::UnityEngine.UIElements.ClickEvent>(OnClickEvent);
			m_ListViewContentContainer.RegisterCallback<global::UnityEngine.UIElements.NavigationCancelEvent>(OnNavigationCancel);
			m_StyleContainer = new global::UnityEngine.UIElements.VisualElement();
			m_StyleContainer.AddToClassList("hierarchy__container");
			m_StyleContainer.Add(m_MultiColumnListView);
			Add(m_StyleContainer);
			m_LastMouseUpSelectionIndex = -1;
			SetRenamingItem(null);
			m_RenameDelayMs = 500;
		}

		public void Dispose()
		{
			SetSourceHierarchy(null);
			this.BindViewItem = null;
			this.UnbindViewItem = null;
			this.PopulateContextMenu = null;
			this.GetTooltip = null;
		}

		public void SetSourceHierarchy(global::Unity.Hierarchy.Hierarchy hierarchy, global::Unity.Hierarchy.HierarchyNodeFlags defaultFlags = global::Unity.Hierarchy.HierarchyNodeFlags.None)
		{
			if (m_Hierarchy == hierarchy)
			{
				return;
			}
			if (m_Hierarchy != null)
			{
				m_Hierarchy.HandlerCreated -= OnHandlerCreated;
			}
			if (m_HierarchyViewModel != null)
			{
				m_HierarchyViewModel.FlagsChanged -= this.FlagsChanged;
			}
			this.SourceHierarchyChanging?.Invoke(m_Hierarchy, hierarchy, defaultFlags);
			ClearColumns();
			Reset();
			SetRenamingItem(null);
			m_LastMouseUpSelectionIndex = -1;
			m_SelectedIndicesChangedFromPointerDown = false;
			m_SelectedIndices.Clear();
			m_ScheduledItem = null;
			m_MultiColumnListView.itemsSource = null;
			m_PostUpdateActionQueue.Clear();
			m_UpdateStage = global::Unity.Hierarchy.HierarchyView.UpdateStage.UpdatingHierarchy;
			m_Version = 0;
			if (m_HierarchyViewModel != null)
			{
				if (m_HierarchyViewModel.IsCreated)
				{
					m_HierarchyViewModel.Dispose();
				}
				m_HierarchyViewModel = null;
			}
			if (m_HierarchyFlattened != null)
			{
				if (m_HierarchyFlattened.IsCreated)
				{
					m_HierarchyFlattened.Dispose();
				}
				m_HierarchyFlattened = null;
			}
			m_Hierarchy = null;
			if (hierarchy != null)
			{
				m_Hierarchy = hierarchy;
				m_HierarchyFlattened = new global::Unity.Hierarchy.HierarchyFlattened(m_Hierarchy);
				m_HierarchyViewModel = new global::Unity.Hierarchy.HierarchyViewModel(m_HierarchyFlattened, defaultFlags);
				m_Hierarchy.Update();
				m_HierarchyFlattened.Update();
				m_HierarchyViewModel.Update();
				m_MultiColumnListView.itemsSource = m_HierarchyViewModel.AsReadOnlyList();
				BindColumns();
				Initialize();
				this.SourceHierarchyChanged?.Invoke(hierarchy, defaultFlags);
				m_Hierarchy.HandlerCreated += OnHandlerCreated;
				m_HierarchyViewModel.FlagsChanged += this.FlagsChanged;
			}
		}

		public void Update()
		{
			while (DoUpdate(global::Unity.Hierarchy.HierarchyView.UpdateMode.Update))
			{
			}
		}

		public bool UpdateIncremental()
		{
			return DoUpdate(global::Unity.Hierarchy.HierarchyView.UpdateMode.UpdateIncremental);
		}

		public bool UpdateIncrementalTimed(double milliseconds)
		{
			do
			{
				m_UpdateTimer.Restart();
				if (!DoUpdate(global::Unity.Hierarchy.HierarchyView.UpdateMode.UpdateIncrementalTimed, milliseconds))
				{
					return false;
				}
				milliseconds -= m_UpdateTimer.ElapsedMillisecondsPrecise();
			}
			while (!(milliseconds <= 0.0));
			return true;
		}

		public void Select(in global::Unity.Hierarchy.HierarchyNode node)
		{
			m_HierarchyViewModel.SetFlags(in node, global::Unity.Hierarchy.HierarchyNodeFlags.Selected);
			Update();
		}

		public void Select(global::System.ReadOnlySpan<global::Unity.Hierarchy.HierarchyNode> nodes)
		{
			m_HierarchyViewModel.SetFlags(nodes, global::Unity.Hierarchy.HierarchyNodeFlags.Selected);
			Update();
		}

		public void SelectRecursive(in global::Unity.Hierarchy.HierarchyNode node, global::Unity.Hierarchy.HierarchyTraversalDirection direction = global::Unity.Hierarchy.HierarchyTraversalDirection.Children)
		{
			m_HierarchyViewModel.SetFlagsRecursive(in node, global::Unity.Hierarchy.HierarchyNodeFlags.Selected, direction);
			Update();
		}

		public void SelectRecursive(global::System.ReadOnlySpan<global::Unity.Hierarchy.HierarchyNode> nodes, global::Unity.Hierarchy.HierarchyTraversalDirection direction = global::Unity.Hierarchy.HierarchyTraversalDirection.Children)
		{
			m_HierarchyViewModel.SetFlagsRecursive(nodes, global::Unity.Hierarchy.HierarchyNodeFlags.Selected, direction);
			Update();
		}

		public void SelectAll(bool exposedOnly)
		{
			if (exposedOnly)
			{
				m_HierarchyViewModel.SetFlags(m_HierarchyViewModel.AsReadOnlySpan(), global::Unity.Hierarchy.HierarchyNodeFlags.Selected);
			}
			else
			{
				m_HierarchyViewModel.SetFlags(global::Unity.Hierarchy.HierarchyNodeFlags.Selected);
			}
			Update();
		}

		public void SetSelection(in global::Unity.Hierarchy.HierarchyNode node)
		{
			using (new global::Unity.Hierarchy.HierarchyViewModelFlagsChangeScope(m_HierarchyViewModel))
			{
				m_HierarchyViewModel.ClearFlags(global::Unity.Hierarchy.HierarchyNodeFlags.Selected);
				m_HierarchyViewModel.SetFlags(in node, global::Unity.Hierarchy.HierarchyNodeFlags.Selected);
			}
			Update();
		}

		public void SetSelection(global::System.ReadOnlySpan<global::Unity.Hierarchy.HierarchyNode> nodes)
		{
			using (new global::Unity.Hierarchy.HierarchyViewModelFlagsChangeScope(m_HierarchyViewModel))
			{
				m_HierarchyViewModel.ClearFlags(global::Unity.Hierarchy.HierarchyNodeFlags.Selected);
				m_HierarchyViewModel.SetFlags(nodes, global::Unity.Hierarchy.HierarchyNodeFlags.Selected);
			}
			Update();
		}

		public bool IsSelected(in global::Unity.Hierarchy.HierarchyNode node)
		{
			return m_HierarchyViewModel.HasAllFlags(in node, global::Unity.Hierarchy.HierarchyNodeFlags.Selected);
		}

		public bool IsSelectedOrAnyAncestorSelected(in global::Unity.Hierarchy.HierarchyNode node)
		{
			global::Unity.Hierarchy.HierarchyNode lhs = node;
			while (true)
			{
				if (lhs == m_Hierarchy.Root)
				{
					return false;
				}
				if (IsSelected(in lhs))
				{
					break;
				}
				lhs = m_HierarchyViewModel.GetParent(in lhs);
			}
			return true;
		}

		public void ToggleSelected(in global::Unity.Hierarchy.HierarchyNode node)
		{
			m_HierarchyViewModel.ToggleFlags(in node, global::Unity.Hierarchy.HierarchyNodeFlags.Selected);
			Update();
		}

		public void ToggleSelected(global::System.ReadOnlySpan<global::Unity.Hierarchy.HierarchyNode> nodes)
		{
			m_HierarchyViewModel.ToggleFlags(nodes, global::Unity.Hierarchy.HierarchyNodeFlags.Selected);
			Update();
		}

		public void ToggleSelectedRecursive(in global::Unity.Hierarchy.HierarchyNode node, global::Unity.Hierarchy.HierarchyTraversalDirection direction = global::Unity.Hierarchy.HierarchyTraversalDirection.Children)
		{
			m_HierarchyViewModel.ToggleFlagsRecursive(in node, global::Unity.Hierarchy.HierarchyNodeFlags.Selected, direction);
			Update();
		}

		public void ToggleSelectedRecursive(global::System.ReadOnlySpan<global::Unity.Hierarchy.HierarchyNode> nodes, global::Unity.Hierarchy.HierarchyTraversalDirection direction = global::Unity.Hierarchy.HierarchyTraversalDirection.Children)
		{
			m_HierarchyViewModel.ToggleFlagsRecursive(nodes, global::Unity.Hierarchy.HierarchyNodeFlags.Selected, direction);
			Update();
		}

		public void ToggleSelection()
		{
			m_HierarchyViewModel.ToggleFlags(global::Unity.Hierarchy.HierarchyNodeFlags.Selected);
			Update();
		}

		public void Deselect(in global::Unity.Hierarchy.HierarchyNode node)
		{
			m_HierarchyViewModel.ClearFlags(in node, global::Unity.Hierarchy.HierarchyNodeFlags.Selected);
			Update();
		}

		public void Deselect(global::System.ReadOnlySpan<global::Unity.Hierarchy.HierarchyNode> nodes)
		{
			m_HierarchyViewModel.ClearFlags(nodes, global::Unity.Hierarchy.HierarchyNodeFlags.Selected);
			Update();
		}

		public void DeselectRecursive(in global::Unity.Hierarchy.HierarchyNode node, global::Unity.Hierarchy.HierarchyTraversalDirection direction = global::Unity.Hierarchy.HierarchyTraversalDirection.Children)
		{
			m_HierarchyViewModel.ClearFlagsRecursive(in node, global::Unity.Hierarchy.HierarchyNodeFlags.Selected, direction);
			Update();
		}

		public void DeselectRecursive(global::System.ReadOnlySpan<global::Unity.Hierarchy.HierarchyNode> nodes, global::Unity.Hierarchy.HierarchyTraversalDirection direction = global::Unity.Hierarchy.HierarchyTraversalDirection.Children)
		{
			m_HierarchyViewModel.ClearFlagsRecursive(nodes, global::Unity.Hierarchy.HierarchyNodeFlags.Selected, direction);
			Update();
		}

		public void DeselectAll()
		{
			m_HierarchyViewModel.ClearFlags(global::Unity.Hierarchy.HierarchyNodeFlags.Selected);
			Update();
		}

		public void Expand(in global::Unity.Hierarchy.HierarchyNode node)
		{
			m_HierarchyViewModel.SetFlags(in node, global::Unity.Hierarchy.HierarchyNodeFlags.Expanded);
			Update();
		}

		public void Expand(global::System.ReadOnlySpan<global::Unity.Hierarchy.HierarchyNode> nodes)
		{
			m_HierarchyViewModel.SetFlags(nodes, global::Unity.Hierarchy.HierarchyNodeFlags.Expanded);
			Update();
		}

		public void ExpandRecursive(in global::Unity.Hierarchy.HierarchyNode node, global::Unity.Hierarchy.HierarchyTraversalDirection direction = global::Unity.Hierarchy.HierarchyTraversalDirection.Children)
		{
			m_HierarchyViewModel.SetFlagsRecursive(in node, global::Unity.Hierarchy.HierarchyNodeFlags.Expanded, direction);
			Update();
		}

		public void ExpandRecursive(global::System.ReadOnlySpan<global::Unity.Hierarchy.HierarchyNode> nodes, global::Unity.Hierarchy.HierarchyTraversalDirection direction = global::Unity.Hierarchy.HierarchyTraversalDirection.Children)
		{
			m_HierarchyViewModel.SetFlagsRecursive(nodes, global::Unity.Hierarchy.HierarchyNodeFlags.Expanded, direction);
			Update();
		}

		public void ExpandAll()
		{
			m_HierarchyViewModel.SetFlags(global::Unity.Hierarchy.HierarchyNodeFlags.Expanded);
			Update();
		}

		public bool IsExpanded(in global::Unity.Hierarchy.HierarchyNode node)
		{
			return m_HierarchyViewModel.HasAllFlags(in node, global::Unity.Hierarchy.HierarchyNodeFlags.Expanded);
		}

		public void Collapse(in global::Unity.Hierarchy.HierarchyNode node)
		{
			m_HierarchyViewModel.ClearFlags(in node, global::Unity.Hierarchy.HierarchyNodeFlags.Expanded);
			Update();
		}

		public void Collapse(global::System.ReadOnlySpan<global::Unity.Hierarchy.HierarchyNode> nodes)
		{
			m_HierarchyViewModel.ClearFlags(nodes, global::Unity.Hierarchy.HierarchyNodeFlags.Expanded);
			Update();
		}

		public void CollapseRecursive(in global::Unity.Hierarchy.HierarchyNode node, global::Unity.Hierarchy.HierarchyTraversalDirection direction = global::Unity.Hierarchy.HierarchyTraversalDirection.Children)
		{
			m_HierarchyViewModel.ClearFlagsRecursive(in node, global::Unity.Hierarchy.HierarchyNodeFlags.Expanded, direction);
			Update();
		}

		public void CollapseRecursive(global::System.ReadOnlySpan<global::Unity.Hierarchy.HierarchyNode> nodes, global::Unity.Hierarchy.HierarchyTraversalDirection direction = global::Unity.Hierarchy.HierarchyTraversalDirection.Children)
		{
			m_HierarchyViewModel.ClearFlagsRecursive(nodes, global::Unity.Hierarchy.HierarchyNodeFlags.Expanded, direction);
			Update();
		}

		public void CollapseAll()
		{
			m_HierarchyViewModel.ClearFlags(global::Unity.Hierarchy.HierarchyNodeFlags.Expanded);
			Update();
		}

		public bool IsCollapsed(in global::Unity.Hierarchy.HierarchyNode node)
		{
			return m_HierarchyViewModel.DoesNotHaveAllFlags(in node, global::Unity.Hierarchy.HierarchyNodeFlags.Expanded);
		}

		public void Show(in global::Unity.Hierarchy.HierarchyNode node)
		{
			m_HierarchyViewModel.ClearFlags(in node, global::Unity.Hierarchy.HierarchyNodeFlags.Hidden);
			Update();
		}

		public void Show(global::System.ReadOnlySpan<global::Unity.Hierarchy.HierarchyNode> nodes)
		{
			m_HierarchyViewModel.ClearFlags(nodes, global::Unity.Hierarchy.HierarchyNodeFlags.Hidden);
			Update();
		}

		public void ShowRecursive(in global::Unity.Hierarchy.HierarchyNode node, global::Unity.Hierarchy.HierarchyTraversalDirection direction = global::Unity.Hierarchy.HierarchyTraversalDirection.Children)
		{
			m_HierarchyViewModel.ClearFlagsRecursive(in node, global::Unity.Hierarchy.HierarchyNodeFlags.Hidden, direction);
			Update();
		}

		public void ShowRecursive(global::System.ReadOnlySpan<global::Unity.Hierarchy.HierarchyNode> nodes, global::Unity.Hierarchy.HierarchyTraversalDirection direction = global::Unity.Hierarchy.HierarchyTraversalDirection.Children)
		{
			m_HierarchyViewModel.ClearFlagsRecursive(nodes, global::Unity.Hierarchy.HierarchyNodeFlags.Hidden, direction);
			Update();
		}

		public void ShowAll()
		{
			m_HierarchyViewModel.ClearFlags(global::Unity.Hierarchy.HierarchyNodeFlags.Hidden);
			Update();
		}

		public bool IsShown(in global::Unity.Hierarchy.HierarchyNode node)
		{
			return m_HierarchyViewModel.DoesNotHaveAllFlags(in node, global::Unity.Hierarchy.HierarchyNodeFlags.Hidden);
		}

		public void Hide(in global::Unity.Hierarchy.HierarchyNode node)
		{
			m_HierarchyViewModel.SetFlags(in node, global::Unity.Hierarchy.HierarchyNodeFlags.Hidden);
			Update();
		}

		public void Hide(global::System.ReadOnlySpan<global::Unity.Hierarchy.HierarchyNode> nodes)
		{
			m_HierarchyViewModel.SetFlags(nodes, global::Unity.Hierarchy.HierarchyNodeFlags.Hidden);
			Update();
		}

		public void HideRecursive(in global::Unity.Hierarchy.HierarchyNode node, global::Unity.Hierarchy.HierarchyTraversalDirection direction = global::Unity.Hierarchy.HierarchyTraversalDirection.Children)
		{
			m_HierarchyViewModel.SetFlagsRecursive(in node, global::Unity.Hierarchy.HierarchyNodeFlags.Hidden, direction);
			Update();
		}

		public void HideRecursive(global::System.ReadOnlySpan<global::Unity.Hierarchy.HierarchyNode> nodes, global::Unity.Hierarchy.HierarchyTraversalDirection direction = global::Unity.Hierarchy.HierarchyTraversalDirection.Children)
		{
			m_HierarchyViewModel.SetFlagsRecursive(nodes, global::Unity.Hierarchy.HierarchyNodeFlags.Hidden, direction);
			Update();
		}

		public void HideAll()
		{
			m_HierarchyViewModel.SetFlags(global::Unity.Hierarchy.HierarchyNodeFlags.Hidden);
			Update();
		}

		public bool IsHidden(in global::Unity.Hierarchy.HierarchyNode node)
		{
			return m_HierarchyViewModel.HasAllFlags(in node, global::Unity.Hierarchy.HierarchyNodeFlags.Hidden);
		}

		public void Frame(in global::Unity.Hierarchy.HierarchyNode node)
		{
			if (!(node == global::Unity.Hierarchy.HierarchyNode.Null) && !(node == m_Hierarchy.Root))
			{
				ExpandParents(in node);
				m_HierarchyViewModel.Update();
				UpdateListView();
				ScrollToNode(in node);
			}
		}

		public void Frame(global::System.ReadOnlySpan<global::Unity.Hierarchy.HierarchyNode> nodes)
		{
			if (nodes.Length != 0)
			{
				ExpandParents(nodes);
				m_HierarchyViewModel.Update();
				UpdateListView();
				ScrollToNode(in nodes[0]);
			}
		}

		public void SetColumns(global::System.Collections.Generic.List<global::UnityEngine.UIElements.Column> columns, global::Unity.Hierarchy.HierarchyViewState state = null)
		{
			if (state != null && state.Columns != null && state.Columns.Length != 0)
			{
				global::Unity.Hierarchy.HierarchyViewColumnState[] columns2 = state.Columns;
				foreach (global::Unity.Hierarchy.HierarchyViewColumnState hierarchyViewColumnState in columns2)
				{
					global::UnityEngine.UIElements.Column columnWithId = global::Unity.Hierarchy.HierarchyViewColumnUtility.GetColumnWithId(columns, hierarchyViewColumnState.ColumnId);
					if (columnWithId != null)
					{
						columnWithId.visible = hierarchyViewColumnState.Visible;
						global::Unity.Hierarchy.HierarchyViewColumn.SetWidth(columnWithId, hierarchyViewColumnState.Width);
					}
				}
				columns.Sort(delegate(global::UnityEngine.UIElements.Column c1, global::UnityEngine.UIElements.Column c2)
				{
					int visibleIndex = global::Unity.Hierarchy.HierarchyViewColumnUtility.GetVisibleIndex(state, c1);
					int visibleIndex2 = global::Unity.Hierarchy.HierarchyViewColumnUtility.GetVisibleIndex(state, c2);
					return visibleIndex - visibleIndex2;
				});
			}
			else
			{
				foreach (global::UnityEngine.UIElements.Column column in columns)
				{
					if (column is global::Unity.Hierarchy.HierarchyViewColumn hierarchyViewColumn)
					{
						hierarchyViewColumn.ApplyDefaultColumnProperties();
					}
					else if (column is global::Unity.Hierarchy.HierarchyViewItemColumn hierarchyViewItemColumn)
					{
						hierarchyViewItemColumn.ApplyDefaultColumnProperties();
					}
				}
			}
			m_MultiColumnListView.columns.Clear();
			foreach (global::UnityEngine.UIElements.Column column2 in columns)
			{
				m_MultiColumnListView.columns.Add(column2);
			}
			BindColumns();
		}

		public void SetColumnDescriptors(global::System.Collections.Generic.IEnumerable<global::Unity.Hierarchy.HierarchyViewColumnDescriptor> columnDescriptors, global::System.Collections.Generic.IEnumerable<global::Unity.Hierarchy.HierarchyViewCellDescriptor> cellDescriptors, global::Unity.Hierarchy.HierarchyViewState state = null)
		{
			global::System.Collections.Generic.List<global::UnityEngine.UIElements.Column> list = new global::System.Collections.Generic.List<global::UnityEngine.UIElements.Column> { NameColumn };
			foreach (global::Unity.Hierarchy.HierarchyViewColumnDescriptor columnDescriptor in columnDescriptors)
			{
				global::Unity.Hierarchy.HierarchyViewColumn hierarchyViewColumn = new global::Unity.Hierarchy.HierarchyViewColumn(this, columnDescriptor);
				foreach (global::Unity.Hierarchy.HierarchyViewCellDescriptor cellDescriptor in cellDescriptors)
				{
					if (cellDescriptor.ValidForColumn(columnDescriptor))
					{
						hierarchyViewColumn.AddCell(cellDescriptor);
					}
				}
				list.Add(hierarchyViewColumn);
			}
			list.Sort(delegate(global::UnityEngine.UIElements.Column c1, global::UnityEngine.UIElements.Column c2)
			{
				int columnDefaultPriority = global::Unity.Hierarchy.HierarchyViewColumnUtility.GetColumnDefaultPriority(c1);
				int columnDefaultPriority2 = global::Unity.Hierarchy.HierarchyViewColumnUtility.GetColumnDefaultPriority(c2);
				return columnDefaultPriority - columnDefaultPriority2;
			});
			SetColumns(list, state);
		}

		public void SetState(global::Unity.Hierarchy.HierarchyViewState viewState)
		{
			if ((viewState.ValidContent & (global::Unity.Hierarchy.HierarchyViewState.Content.ViewModelState | global::Unity.Hierarchy.HierarchyViewState.Content.SearchText | global::Unity.Hierarchy.HierarchyViewState.Content.Columns)) != global::Unity.Hierarchy.HierarchyViewState.Content.Invalid)
			{
				EnqueuePostUpdateAction(delegate
				{
					if (viewState.ValidContent.HasFlag(global::Unity.Hierarchy.HierarchyViewState.Content.Columns))
					{
						SetColumnState(viewState);
					}
					if (viewState.ValidContent.HasFlag(global::Unity.Hierarchy.HierarchyViewState.Content.SearchText))
					{
						Filter = viewState.SearchText;
					}
					if (viewState.ValidContent.HasFlag(global::Unity.Hierarchy.HierarchyViewState.Content.ViewModelState))
					{
						m_HierarchyViewModel.SetState(viewState.ViewModelState);
					}
				});
			}
			if (viewState.ValidContent.HasFlag(global::Unity.Hierarchy.HierarchyViewState.Content.ScrollPosition))
			{
				m_MultiColumnListView.scrollView.scrollOffset = new global::UnityEngine.Vector2(viewState.ScrollPositionX, viewState.ScrollPositionY);
			}
		}

		public global::Unity.Hierarchy.HierarchyViewState GetState(global::Unity.Hierarchy.HierarchyViewState.Content content = global::Unity.Hierarchy.HierarchyViewState.Content.All)
		{
			global::Unity.Hierarchy.HierarchyViewState hierarchyViewState = new global::Unity.Hierarchy.HierarchyViewState(content);
			if (hierarchyViewState.ValidContent.HasFlag(global::Unity.Hierarchy.HierarchyViewState.Content.ViewModelState))
			{
				hierarchyViewState.ViewModelState = m_HierarchyViewModel.GetState();
			}
			if (hierarchyViewState.ValidContent.HasFlag(global::Unity.Hierarchy.HierarchyViewState.Content.SearchText))
			{
				hierarchyViewState.SearchText = Filter;
			}
			if (hierarchyViewState.ValidContent.HasFlag(global::Unity.Hierarchy.HierarchyViewState.Content.ScrollPosition))
			{
				global::UnityEngine.Vector2 vector = global::UnityEngine.UIElements.UQueryExtensions.Q<global::UnityEngine.UIElements.ScrollView>(m_MultiColumnListView)?.scrollOffset ?? new global::UnityEngine.Vector2(-1f, -1f);
				hierarchyViewState.ScrollPositionX = vector.x;
				hierarchyViewState.ScrollPositionY = vector.y;
			}
			if (hierarchyViewState.ValidContent.HasFlag(global::Unity.Hierarchy.HierarchyViewState.Content.Columns))
			{
				hierarchyViewState.Columns = new global::Unity.Hierarchy.HierarchyViewColumnState[m_MultiColumnListView.columns.Count];
				global::System.Collections.Generic.List<global::UnityEngine.UIElements.VisualElement> list = global::UnityEngine.UIElements.UQueryExtensions.Query<global::UnityEngine.UIElements.VisualElement>(m_MultiColumnListView, null, "unity-multi-column-header__column").ToList();
				int num = 0;
				foreach (global::UnityEngine.UIElements.Column column in m_MultiColumnListView.columns)
				{
					string columnId = global::Unity.Hierarchy.HierarchyViewColumnUtility.GetColumnId(column);
					int num2 = list.FindIndex((global::UnityEngine.UIElements.VisualElement header) => header.name == columnId);
					hierarchyViewState.Columns[num] = new global::Unity.Hierarchy.HierarchyViewColumnState
					{
						ColumnId = columnId,
						Width = column.width.value,
						Visible = column.visible,
						Index = ((num2 != -1) ? num2 : num)
					};
					num++;
				}
				global::System.Array.Sort(hierarchyViewState.Columns, (global::Unity.Hierarchy.HierarchyViewColumnState c1, global::Unity.Hierarchy.HierarchyViewColumnState c2) => c1.Index - c2.Index);
			}
			return hierarchyViewState;
		}

		[global::UnityEngine.Bindings.VisibleToOtherModules(new string[] { "UnityEditor.HierarchyModule" })]
		internal void Initialize()
		{
			BindHandlers();
			try
			{
				this.Initializing?.Invoke();
			}
			catch (global::System.Exception exception)
			{
				global::UnityEngine.Debug.LogException(exception);
			}
		}

		internal void Reset()
		{
			UnbindHandlers();
			m_StyleContainer.Remove(m_MultiColumnListView);
			m_StyleContainer.RemoveFromHierarchy();
			m_StyleContainer = new global::UnityEngine.UIElements.VisualElement();
			m_StyleContainer.AddToClassList("hierarchy__container");
			m_StyleContainer.Add(m_MultiColumnListView);
			Add(m_StyleContainer);
		}

		[global::UnityEngine.Bindings.VisibleToOtherModules(new string[] { "UnityEditor.HierarchyModule" })]
		internal void EnqueuePostUpdateAction(global::System.Action action)
		{
			if (m_PostUpdateActionQueue.Locked)
			{
				throw new global::System.InvalidOperationException("Cannot enqueue post update action while processing post update actions.");
			}
			m_PostUpdateActionQueue.PushBack(in action);
		}

		[global::UnityEngine.Bindings.VisibleToOtherModules(new string[] { "UnityEditor.HierarchyModule" })]
		internal void BeginRename(in global::Unity.Hierarchy.HierarchyNode node)
		{
			int num = m_HierarchyViewModel.IndexOf(in node);
			if (num >= 0)
			{
				GetHierarchyViewItemFromIndex(num)?.BeginRename();
			}
		}

		[global::UnityEngine.Bindings.VisibleToOtherModules(new string[] { "UnityEditor.HierarchyModule" })]
		internal void OnLostFocus()
		{
			m_LastMouseUpSelectionIndex = -1;
		}

		internal int GetIndexFromLocalPosition(global::UnityEngine.Vector2 pos)
		{
			return m_MultiColumnListView.virtualizationController.GetIndexFromPosition(pos);
		}

		internal int GetIndexFromWorldPosition(global::UnityEngine.Vector2 worldPos, float offset = 0f)
		{
			global::UnityEngine.Vector2 pos = global::UnityEngine.UIElements.VisualElementExtensions.WorldToLocal(p: new global::UnityEngine.Vector3(worldPos.x, worldPos.y - offset, 0f), ele: m_ListViewContentContainer);
			return GetIndexFromLocalPosition(pos);
		}

		internal void InvokeBindViewItem(global::Unity.Hierarchy.HierarchyViewItem item)
		{
			item.Handler?.Internal_BindItem(item);
			this.BindViewItem?.Invoke(item);
		}

		internal void InvokeUnbindViewItem(global::Unity.Hierarchy.HierarchyViewItem item)
		{
			item.Handler?.Internal_UnbindItem(item);
			this.UnbindViewItem?.Invoke(item);
		}

		internal void InvokePopulateContextMenu(global::UnityEngine.UIElements.ContextualMenuPopulateEvent evt)
		{
			if (!(evt.target is global::Unity.Hierarchy.HierarchyView src))
			{
				return;
			}
			if (m_IsRenamingItem)
			{
				global::UnityEngine.UIElements.UQueryExtensions.Q<global::Unity.Hierarchy.HierarchyViewItemName>(m_RenamingItem)?.CancelRename();
				SetRenamingItem(null);
			}
			evt.StopImmediatePropagation();
			global::UnityEngine.Vector2 pos = global::UnityEngine.UIElements.VisualElementExtensions.ChangeCoordinatesTo(src, m_ListViewContentContainer, evt.localMousePosition);
			int indexFromLocalPosition = GetIndexFromLocalPosition(pos);
			global::Unity.Hierarchy.HierarchyViewItem hierarchyViewItemFromIndex = GetHierarchyViewItemFromIndex(indexFromLocalPosition);
			if (hierarchyViewItemFromIndex == null)
			{
				m_MultiColumnListView.ClearSelection();
				foreach (global::Unity.Hierarchy.HierarchyNodeTypeHandler item in m_Hierarchy.EnumerateNodeTypeHandlers())
				{
					if (item is global::Unity.Hierarchy.IHierarchyEditorNodeTypeHandler hierarchyEditorNodeTypeHandler)
					{
						hierarchyEditorNodeTypeHandler.PopulateContextMenu(this, null, evt.menu);
					}
				}
			}
			else if (hierarchyViewItemFromIndex.Handler is global::Unity.Hierarchy.IHierarchyEditorNodeTypeHandler hierarchyEditorNodeTypeHandler2)
			{
				hierarchyEditorNodeTypeHandler2.PopulateContextMenu(this, hierarchyViewItemFromIndex, evt.menu);
			}
			this.PopulateContextMenu?.Invoke(hierarchyViewItemFromIndex, evt.menu);
		}

		internal void InvokeGetTooltip(global::Unity.Hierarchy.HierarchyViewItem item, bool filtering, global::System.Text.StringBuilder tooltip)
		{
			if (item.Handler is global::Unity.Hierarchy.IHierarchyEditorNodeTypeHandler hierarchyEditorNodeTypeHandler)
			{
				hierarchyEditorNodeTypeHandler.GetTooltip(item, filtering, tooltip);
			}
			this.GetTooltip?.Invoke(item, filtering, tooltip);
		}

		[global::UnityEngine.Bindings.VisibleToOtherModules(new string[] { "UnityEditor.HierarchyModule" })]
		internal void PingNode(global::Unity.Hierarchy.HierarchyNode node)
		{
			if (node == global::Unity.Hierarchy.HierarchyNode.Null || node == m_Hierarchy.Root || !m_Hierarchy.Exists(in node))
			{
				return;
			}
			ExpandParents(in node);
			Update();
			int num = m_HierarchyViewModel.IndexOf(in node);
			if (num < 0)
			{
				return;
			}
			m_MultiColumnListView.ScrollToItem(num);
			EnqueuePostUpdateAction(delegate
			{
				base.schedule.Execute((global::System.Action)delegate
				{
					DoPingAnimation(node);
				});
			});
		}

		private void DoPingAnimation(global::Unity.Hierarchy.HierarchyNode node)
		{
			int num = m_HierarchyViewModel.IndexOf(in node);
			if (num < 0)
			{
				return;
			}
			global::Unity.Hierarchy.HierarchyViewItem hierarchyViewItemFromIndex = GetHierarchyViewItemFromIndex(num);
			if (hierarchyViewItemFromIndex == null)
			{
				return;
			}
			global::UnityEngine.UIElements.VisualElement rowContainer = hierarchyViewItemFromIndex.RowContainer;
			if (rowContainer == null || rowContainer.ClassListContains("hierarchy - item__ping-base"))
			{
				return;
			}
			rowContainer.AddToClassList("hierarchy - item__ping-base");
			rowContainer.schedule.Execute((global::System.Action)delegate
			{
				rowContainer.AddToClassList("hierarchy-item__ping-ramp-in-style");
				rowContainer.AddToClassList("hierarchy-item__ping-ramp-in-start");
				rowContainer.RegisterCallbackOnce<global::UnityEngine.UIElements.TransitionEndEvent>(delegate
				{
					rowContainer.RemoveFromClassList("hierarchy-item__ping-ramp-in-start");
					rowContainer.RemoveFromClassList("hierarchy-item__ping-ramp-in-style");
					rowContainer.AddToClassList("hierarchy-item__ping-ramp-out-start");
					rowContainer.AddToClassList("hierarchy-item__ping-ramp-out-style");
					rowContainer.RegisterCallbackOnce<global::UnityEngine.UIElements.TransitionEndEvent>(delegate
					{
						rowContainer.RemoveFromClassList("hierarchy - item__ping-base");
						rowContainer.RemoveFromClassList("hierarchy-item__ping-ramp-out-start");
						rowContainer.RemoveFromClassList("hierarchy-item__ping-ramp-out-style");
					});
				});
			});
		}

		internal void ScrollToNode(in global::Unity.Hierarchy.HierarchyNode node)
		{
			if (!(node == global::Unity.Hierarchy.HierarchyNode.Null) && !(node == m_Hierarchy.Root))
			{
				int num = m_HierarchyViewModel.IndexOf(in node);
				if (num >= 0)
				{
					m_MultiColumnListView.ScrollToItem(num);
				}
			}
		}

		[global::UnityEngine.Bindings.VisibleToOtherModules(new string[] { "UnityEditor.HierarchyModule" })]
		internal void ExpandParents(in global::Unity.Hierarchy.HierarchyNode node)
		{
			if (!(node == global::Unity.Hierarchy.HierarchyNode.Null) && !(node == m_Hierarchy.Root))
			{
				global::Unity.Hierarchy.HierarchyNode lhs = m_Hierarchy.GetParent(in node);
				if (!(lhs == global::Unity.Hierarchy.HierarchyNode.Null) && !(lhs == m_Hierarchy.Root))
				{
					m_HierarchyViewModel.SetFlagsRecursive(in lhs, global::Unity.Hierarchy.HierarchyNodeFlags.Expanded, global::Unity.Hierarchy.HierarchyTraversalDirection.Parents);
				}
			}
		}

		internal void ExpandParents(global::System.ReadOnlySpan<global::Unity.Hierarchy.HierarchyNode> nodes)
		{
			global::UnityEngine.Pool.RentSpanUnmanaged<global::Unity.Hierarchy.HierarchyNode> rentSpanUnmanaged = new global::UnityEngine.Pool.RentSpanUnmanaged<global::Unity.Hierarchy.HierarchyNode>(nodes.Length, clear: true);
			try
			{
				int i = 0;
				for (int length = nodes.Length; i < length; i++)
				{
					ref readonly global::Unity.Hierarchy.HierarchyNode reference = ref nodes[i];
					if (!(reference == global::Unity.Hierarchy.HierarchyNode.Null) && !(reference == m_Hierarchy.Root))
					{
						rentSpanUnmanaged.Span[i] = m_Hierarchy.GetParent(in reference);
					}
				}
				m_HierarchyViewModel.SetFlagsRecursive(rentSpanUnmanaged.Span, global::Unity.Hierarchy.HierarchyNodeFlags.Expanded, global::Unity.Hierarchy.HierarchyTraversalDirection.Parents);
			}
			finally
			{
				rentSpanUnmanaged.Dispose();
			}
		}

		internal void SelectChildrenAndExpandRecursive()
		{
			int num = m_HierarchyViewModel.HasAllFlagsCount(global::Unity.Hierarchy.HierarchyNodeFlags.Selected);
			if (num == 0)
			{
				return;
			}
			global::UnityEngine.Pool.RentSpanUnmanaged<global::Unity.Hierarchy.HierarchyNode> rentSpan = new global::UnityEngine.Pool.RentSpanUnmanaged<global::Unity.Hierarchy.HierarchyNode>(num);
			try
			{
				m_HierarchyViewModel.GetNodesWithAllFlags(global::Unity.Hierarchy.HierarchyNodeFlags.Selected, rentSpan);
				m_HierarchyViewModel.SetFlagsRecursive(rentSpan, global::Unity.Hierarchy.HierarchyNodeFlags.Expanded | global::Unity.Hierarchy.HierarchyNodeFlags.Selected, global::Unity.Hierarchy.HierarchyTraversalDirection.Children);
				Update();
			}
			finally
			{
				rentSpan.Dispose();
			}
		}

		internal void SetRenamingItem(global::Unity.Hierarchy.HierarchyViewItem item)
		{
			m_RenamingItem = item;
		}

		private void BindHandlers()
		{
			if (m_Hierarchy == null || !m_Hierarchy.IsCreated)
			{
				return;
			}
			foreach (global::Unity.Hierarchy.HierarchyNodeTypeHandler item in m_Hierarchy.EnumerateNodeTypeHandlers())
			{
				item.Internal_BindView(this);
			}
		}

		private void UnbindHandlers()
		{
			if (m_Hierarchy == null || !m_Hierarchy.IsCreated)
			{
				return;
			}
			foreach (global::Unity.Hierarchy.HierarchyNodeTypeHandler item in m_Hierarchy.EnumerateNodeTypeHandlers())
			{
				item.Internal_UnbindView(this);
			}
		}

		private void OnClickEvent(global::UnityEngine.UIElements.ClickEvent evt)
		{
			m_ScheduledItem?.Pause();
			if (evt.button != 0)
			{
				return;
			}
			int indexFromLocalPosition = GetIndexFromLocalPosition(evt.localPosition);
			global::Unity.Hierarchy.HierarchyViewItem item = GetHierarchyViewItemFromIndex(indexFromLocalPosition);
			if (item == null)
			{
				return;
			}
			global::UnityEngine.Vector3 position = evt.position;
			global::Unity.Hierarchy.HierarchyViewItemName hierarchyViewItemName = global::UnityEngine.UIElements.UQueryExtensions.Q<global::Unity.Hierarchy.HierarchyViewItemName>(item);
			if (indexFromLocalPosition == m_LastMouseUpSelectionIndex && evt.clickCount == 1 && hierarchyViewItemName != null && hierarchyViewItemName.worldBound.Contains(position))
			{
				if (m_RenameDelayMs == 0)
				{
					item.BeginRename();
				}
				else
				{
					m_ScheduledItem = base.schedule.Execute((global::System.Action)delegate
					{
						item.BeginRename();
						m_ScheduledItem = null;
					}).StartingIn(m_RenameDelayMs);
				}
			}
			else if (evt.clickCount == 2)
			{
				ref readonly global::Unity.Hierarchy.HierarchyNode node = ref m_HierarchyViewModel[indexFromLocalPosition];
				global::Unity.Hierarchy.HierarchyNodeTypeHandler nodeTypeHandler = m_Hierarchy.GetNodeTypeHandler(in node);
				if (nodeTypeHandler is global::Unity.Hierarchy.IHierarchyEditorNodeTypeHandler hierarchyEditorNodeTypeHandler)
				{
					hierarchyEditorNodeTypeHandler.OnDoubleClick(this, in node);
				}
			}
			m_LastMouseUpSelectionIndex = indexFromLocalPosition;
		}

		private void OnPointerUp(global::UnityEngine.UIElements.PointerUpEvent evt)
		{
			if (m_SelectedIndicesChangedFromPointerDown)
			{
				this.FlagsChanged?.Invoke(global::Unity.Hierarchy.HierarchyNodeFlags.Selected);
				m_SelectedIndicesChangedFromPointerDown = false;
			}
		}

		private void OnKeyDown(global::UnityEngine.UIElements.KeyDownEvent evt)
		{
			if (!m_IsRenamingItem)
			{
				bool flag = true;
				switch (evt.keyCode)
				{
				case global::UnityEngine.KeyCode.Home:
				case global::UnityEngine.KeyCode.PageUp:
					m_MultiColumnListView.SetSelection(0);
					break;
				case global::UnityEngine.KeyCode.End:
				case global::UnityEngine.KeyCode.PageDown:
					m_MultiColumnListView.SetSelection(m_MultiColumnListView.itemsSource.Count - 1);
					break;
				case global::UnityEngine.KeyCode.Escape:
					m_SelectedIndices.Clear();
					m_SelectedIndicesChangedFromPointerDown = false;
					break;
				default:
					flag = false;
					break;
				}
				m_ListViewContentContainer.Focus();
				if (flag)
				{
					evt.StopPropagation();
				}
			}
		}

		private void OnNavigationMove(global::UnityEngine.UIElements.NavigationMoveEvent evt)
		{
			if (m_IsRenamingItem)
			{
				return;
			}
			bool flag = true;
			int selectedIndex = m_MultiColumnListView.selectedIndex;
			if (selectedIndex == -1)
			{
				global::UnityEngine.UIElements.NavigationMoveEvent.Direction direction = evt.direction;
				global::UnityEngine.UIElements.NavigationMoveEvent.Direction direction2 = direction;
				if (direction2 == global::UnityEngine.UIElements.NavigationMoveEvent.Direction.Up || direction2 == global::UnityEngine.UIElements.NavigationMoveEvent.Direction.Down)
				{
					m_MultiColumnListView.SetSelection(0);
				}
				else
				{
					flag = false;
				}
				m_ListViewContentContainer.Focus();
			}
			else
			{
				global::UnityEngine.UIElements.NavigationMoveEvent.Direction direction3 = evt.direction;
				global::UnityEngine.UIElements.NavigationMoveEvent.Direction direction4 = direction3;
				if (direction4 == global::UnityEngine.UIElements.NavigationMoveEvent.Direction.Left || direction4 == global::UnityEngine.UIElements.NavigationMoveEvent.Direction.Right)
				{
					int length = m_HierarchyViewModel.HasAnyFlagsCount(global::Unity.Hierarchy.HierarchyNodeFlags.Selected);
					global::UnityEngine.Pool.RentSpanUnmanaged<global::Unity.Hierarchy.HierarchyNode> rentSpan = new global::UnityEngine.Pool.RentSpanUnmanaged<global::Unity.Hierarchy.HierarchyNode>(length);
					try
					{
						m_HierarchyViewModel.GetNodesWithAnyFlags(global::Unity.Hierarchy.HierarchyNodeFlags.Selected, rentSpan);
						SetExpandedState(rentSpan, evt.direction == global::UnityEngine.UIElements.NavigationMoveEvent.Direction.Right, evt.altKey);
					}
					finally
					{
						rentSpan.Dispose();
					}
				}
				else
				{
					flag = false;
				}
			}
			if (flag)
			{
				evt.StopPropagation();
			}
		}

		private void OnNavigationCancel(global::UnityEngine.UIElements.NavigationCancelEvent evt)
		{
			m_HierarchyViewModel.ClearFlags(global::Unity.Hierarchy.HierarchyNodeFlags.Cut);
			Update();
			evt.StopImmediatePropagation();
		}

		private void OnListViewClick(global::UnityEngine.UIElements.ClickEvent evt)
		{
			global::UnityEngine.UIElements.VisualElement visualElement = evt.target as global::UnityEngine.UIElements.VisualElement;
			if (visualElement == global::UnityEngine.UIElements.UQueryExtensions.Q(m_MultiColumnListView, null, global::UnityEngine.UIElements.ScrollView.contentAndVerticalScrollUssClassName))
			{
				m_HierarchyViewModel.ClearFlags(global::Unity.Hierarchy.HierarchyNodeFlags.Selected);
				Update();
				m_LastMouseUpSelectionIndex = -1;
				evt.StopImmediatePropagation();
			}
		}

		private void OnUnbindItem(global::Unity.Hierarchy.HierarchyViewItem element)
		{
			element.ExpandedStateChanged -= SetExpandedState;
		}

		private void OnHandlerCreated(global::Unity.Hierarchy.HierarchyNodeTypeHandlerBase handler)
		{
			Reset();
			Initialize();
		}

		private global::Unity.Hierarchy.HierarchyViewItem GetHierarchyViewItemFromIndex(int index)
		{
			if (index == -1)
			{
				return null;
			}
			return global::UnityEngine.UIElements.UQueryExtensions.Q<global::Unity.Hierarchy.HierarchyViewItem>(m_MultiColumnListView.GetRootElementForIndex(index)?);
		}

		private void OnSelectedIndicesChanged(global::System.Collections.Generic.IEnumerable<int> indices)
		{
			bool flag = false;
			m_SelectedIndices.Clear();
			foreach (int index in indices)
			{
				if (index >= 0)
				{
					if (index == m_LastMouseUpSelectionIndex)
					{
						flag = true;
					}
					m_SelectedIndices.Add(index);
				}
			}
			if (!flag)
			{
				m_LastMouseUpSelectionIndex = -1;
			}
			global::UnityEngine.Pool.RentSpanUnmanaged<global::Unity.Hierarchy.HierarchyNode> rentSpanUnmanaged = new global::UnityEngine.Pool.RentSpanUnmanaged<global::Unity.Hierarchy.HierarchyNode>(m_SelectedIndices.Count, clear: true);
			try
			{
				for (int i = 0; i < m_SelectedIndices.Count; i++)
				{
					int num = m_SelectedIndices[i];
					if (num >= 0 && num < m_HierarchyViewModel.Count)
					{
						rentSpanUnmanaged.Span[i] = m_HierarchyViewModel[num];
					}
				}
				m_SelectedIndices.Clear();
				using (new global::Unity.Hierarchy.HierarchyViewModelFlagsChangeScope(m_HierarchyViewModel, notify: false))
				{
					m_HierarchyViewModel.ClearFlags(global::Unity.Hierarchy.HierarchyNodeFlags.Selected);
					m_HierarchyViewModel.SetFlags(rentSpanUnmanaged.Span, global::Unity.Hierarchy.HierarchyNodeFlags.Selected);
				}
				if (m_MultiColumnListView.pointerProcessingState == global::UnityEngine.UIElements.BaseVerticalCollectionView.pointerProcessingStateEnum.PointerDown && m_MultiColumnListView.currentPointerButton != 1)
				{
					m_SelectedIndicesChangedFromPointerDown = true;
				}
				else
				{
					this.FlagsChanged?.Invoke(global::Unity.Hierarchy.HierarchyNodeFlags.Selected);
				}
			}
			finally
			{
				rentSpanUnmanaged.Dispose();
			}
		}

		private void OnBindItem(global::Unity.Hierarchy.HierarchyViewItem item)
		{
			item.ExpandedStateChanged += SetExpandedState;
		}

		private void SetExpandedState(in global::Unity.Hierarchy.HierarchyNode node, bool isExpanded, bool recurse)
		{
			if (isExpanded)
			{
				if (recurse)
				{
					ExpandRecursive(in node);
				}
				else
				{
					Expand(in node);
				}
			}
			else if (recurse)
			{
				CollapseRecursive(in node);
			}
			else
			{
				Collapse(in node);
			}
		}

		private void SetExpandedState(global::System.ReadOnlySpan<global::Unity.Hierarchy.HierarchyNode> nodes, bool isExpanded, bool recurse)
		{
			if (isExpanded)
			{
				if (recurse)
				{
					ExpandRecursive(nodes);
				}
				else
				{
					Expand(nodes);
				}
			}
			else if (recurse)
			{
				CollapseRecursive(nodes);
			}
			else
			{
				Collapse(nodes);
			}
		}

		[global::UnityEngine.Bindings.VisibleToOtherModules(new string[] { "UnityEditor.HierarchyModule" })]
		internal bool UpdateListView()
		{
			if (m_Version == m_HierarchyViewModel.Version)
			{
				return false;
			}
			m_MultiColumnListView.RefreshItems();
			int num = m_HierarchyViewModel.HasAllFlagsCount(global::Unity.Hierarchy.HierarchyNodeFlags.Selected);
			if (num == 0)
			{
				SetListViewSelectionWithoutNotify(global::System.Array.Empty<int>());
			}
			else
			{
				global::UnityEngine.Pool.RentSpanUnmanaged<int> rentSpan = new global::UnityEngine.Pool.RentSpanUnmanaged<int>(num);
				try
				{
					m_HierarchyViewModel.GetIndicesWithAllFlags(global::Unity.Hierarchy.HierarchyNodeFlags.Selected, rentSpan);
					SetListViewSelectionWithoutNotify(rentSpan);
				}
				finally
				{
					rentSpan.Dispose();
				}
			}
			m_Version = m_HierarchyViewModel.Version;
			return false;
		}

		private void SetListViewSelectionWithoutNotify(global::System.Span<int> selection)
		{
			global::UnityEngine.Pool.RentSpanUnmanaged<int> rentSpanUnmanaged = new global::UnityEngine.Pool.RentSpanUnmanaged<int>(selection.Length);
			try
			{
				int length = 0;
				for (int i = 0; i < selection.Length; i++)
				{
					int num = selection[i];
					if (num >= 0 && num < m_HierarchyViewModel.Count)
					{
						rentSpanUnmanaged.Span[length++] = num;
					}
				}
				global::UnityEngine.UIElements.MultiColumnListView multiColumnListView = m_MultiColumnListView;
				global::System.Span<int> span = rentSpanUnmanaged.Span;
				multiColumnListView.SetSelectionWithoutNotify(span.Slice(0, length));
			}
			finally
			{
				rentSpanUnmanaged.Dispose();
			}
		}

		private void SetColumnState(global::Unity.Hierarchy.HierarchyViewState state)
		{
			global::System.Collections.Generic.List<global::UnityEngine.UIElements.Column> list = global::UnityEngine.Pool.CollectionPool<global::System.Collections.Generic.List<global::UnityEngine.UIElements.Column>, global::UnityEngine.UIElements.Column>.Get();
			foreach (global::UnityEngine.UIElements.Column column in m_MultiColumnListView.columns)
			{
				list.Add(column);
			}
			SetColumns(list, state);
		}

		private void ClearColumns()
		{
			global::System.Collections.Generic.List<global::UnityEngine.UIElements.VisualElement> list = global::UnityEngine.UIElements.UQueryExtensions.Query<global::UnityEngine.UIElements.VisualElement>(m_MultiColumnListView, "unity-multi-column-view__row-container").ToList();
			foreach (global::UnityEngine.UIElements.VisualElement item in list)
			{
				global::System.Collections.Generic.List<global::Unity.Hierarchy.HierarchyViewCell> list2 = global::UnityEngine.UIElements.UQueryExtensions.Query<global::Unity.Hierarchy.HierarchyViewCell>(item, "HierarchyViewCell").ToList();
				foreach (global::Unity.Hierarchy.HierarchyViewCell item2 in list2)
				{
					if (item2.Descriptor != null)
					{
						item2.UnbindCell();
					}
				}
			}
			foreach (global::UnityEngine.UIElements.Column column in m_MultiColumnListView.columns)
			{
				if (column is global::Unity.Hierarchy.HierarchyViewColumn hierarchyViewColumn)
				{
					hierarchyViewColumn.UnbindColumn(this);
				}
			}
		}

		private void BindColumns()
		{
			foreach (global::UnityEngine.UIElements.Column column in m_MultiColumnListView.columns)
			{
				if (column is global::Unity.Hierarchy.HierarchyViewColumn hierarchyViewColumn)
				{
					hierarchyViewColumn.BindColumn(this);
				}
			}
		}

		private bool DoUpdate(global::Unity.Hierarchy.HierarchyView.UpdateMode mode, double milliseconds = 0.0)
		{
			bool flag = DoUpdateStage(mode, milliseconds);
			if (!flag)
			{
				IncrementUpdateStage();
			}
			return flag || UpdateNeeded;
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			bool DoUpdateStage(global::Unity.Hierarchy.HierarchyView.UpdateMode mode2, double milliseconds2)
			{
				switch (m_UpdateStage)
				{
				case global::Unity.Hierarchy.HierarchyView.UpdateStage.UpdatingHierarchy:
					return UpdateHierarchy(mode2, milliseconds2);
				case global::Unity.Hierarchy.HierarchyView.UpdateStage.UpdatingHierarchyFlattened:
					return UpdateHierarchyFlattened(mode2, milliseconds2);
				case global::Unity.Hierarchy.HierarchyView.UpdateStage.UpdatingHierarchyViewModel:
					return UpdateHierarchyViewModel(mode2, milliseconds2);
				case global::Unity.Hierarchy.HierarchyView.UpdateStage.UpdatingListView:
					return UpdateListView();
				case global::Unity.Hierarchy.HierarchyView.UpdateStage.ExecutePostUpdateActions:
					ExecuteActions(m_PostUpdateActionQueue);
					return false;
				default:
					throw new global::System.NotImplementedException(m_UpdateStage.ToString());
				}
			}
			static void ExecuteActions(global::Unity.Hierarchy.CircularBuffer<global::System.Action> actions)
			{
				if (!actions.IsEmpty)
				{
				}
				while (!actions.IsEmpty)
				{
					global::System.Action action = actions.Front();
					try
					{
						actions.Locked = true;
						action?.Invoke();
					}
					catch (global::System.Exception exception)
					{
						global::UnityEngine.Debug.LogException(exception);
					}
					finally
					{
						actions.Locked = false;
						actions.PopFront();
					}
				}
			}
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			void IncrementUpdateStage()
			{
				m_UpdateStage = (global::Unity.Hierarchy.HierarchyView.UpdateStage)((int)(m_UpdateStage + 1) % 5);
			}
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			bool UpdateHierarchy(global::Unity.Hierarchy.HierarchyView.UpdateMode updateMode, double milliseconds2)
			{
				if (m_Hierarchy != null && m_Hierarchy.IsCreated && m_Hierarchy.UpdateNeeded)
				{
					switch (updateMode)
					{
					case global::Unity.Hierarchy.HierarchyView.UpdateMode.Update:
						m_Hierarchy.Update();
						return false;
					case global::Unity.Hierarchy.HierarchyView.UpdateMode.UpdateIncremental:
						return m_Hierarchy.UpdateIncremental();
					case global::Unity.Hierarchy.HierarchyView.UpdateMode.UpdateIncrementalTimed:
						return m_Hierarchy.UpdateIncrementalTimed(milliseconds2);
					default:
						throw new global::System.NotImplementedException(updateMode.ToString());
					}
				}
				return false;
			}
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			bool UpdateHierarchyFlattened(global::Unity.Hierarchy.HierarchyView.UpdateMode updateMode, double milliseconds2)
			{
				if (m_HierarchyFlattened != null && m_HierarchyFlattened.IsCreated && m_HierarchyFlattened.UpdateNeeded)
				{
					switch (updateMode)
					{
					case global::Unity.Hierarchy.HierarchyView.UpdateMode.Update:
						m_HierarchyFlattened.Update();
						return false;
					case global::Unity.Hierarchy.HierarchyView.UpdateMode.UpdateIncremental:
						return m_HierarchyFlattened.UpdateIncremental();
					case global::Unity.Hierarchy.HierarchyView.UpdateMode.UpdateIncrementalTimed:
						return m_HierarchyFlattened.UpdateIncrementalTimed(milliseconds2);
					default:
						throw new global::System.NotImplementedException(updateMode.ToString());
					}
				}
				return false;
			}
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			bool UpdateHierarchyViewModel(global::Unity.Hierarchy.HierarchyView.UpdateMode updateMode, double milliseconds2)
			{
				if (m_HierarchyViewModel != null && m_HierarchyViewModel.IsCreated && m_HierarchyViewModel.UpdateNeeded)
				{
					switch (updateMode)
					{
					case global::Unity.Hierarchy.HierarchyView.UpdateMode.Update:
						m_HierarchyViewModel.Update();
						return false;
					case global::Unity.Hierarchy.HierarchyView.UpdateMode.UpdateIncremental:
						return m_HierarchyViewModel.UpdateIncremental();
					case global::Unity.Hierarchy.HierarchyView.UpdateMode.UpdateIncrementalTimed:
						return m_HierarchyViewModel.UpdateIncrementalTimed(milliseconds2);
					default:
						throw new global::System.NotImplementedException(updateMode.ToString());
					}
				}
				return false;
			}
		}
	}
}
