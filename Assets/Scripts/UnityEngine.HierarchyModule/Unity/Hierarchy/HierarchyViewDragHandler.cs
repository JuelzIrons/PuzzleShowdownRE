namespace Unity.Hierarchy
{
	internal class HierarchyViewDragHandler
	{
		private struct HierarchyViewDragAndDropTargets : global::System.IEquatable<global::Unity.Hierarchy.HierarchyViewDragHandler.HierarchyViewDragAndDropTargets>
		{
			public int insertAtIndex;

			public int targetIndex;

			public int parentIndex;

			public int childIndex;

			public global::UnityEngine.UIElements.DragAndDropPosition dropPosition;

			public global::UnityEngine.UIElements.DragVisualMode dragVisualMode;

			[global::Unity.Scripting.LifecycleManagement.NoAutoStaticsCleanup]
			public static readonly global::Unity.Hierarchy.HierarchyViewDragHandler.HierarchyViewDragAndDropTargets Rejected = new global::Unity.Hierarchy.HierarchyViewDragHandler.HierarchyViewDragAndDropTargets(-1, -1, -1, -1, global::UnityEngine.UIElements.DragAndDropPosition.OverItem)
			{
				dragVisualMode = global::UnityEngine.UIElements.DragVisualMode.Rejected
			};

			public HierarchyViewDragAndDropTargets(int insertAtIndex, int targetIndex, int parentIndex, int childIndex, global::UnityEngine.UIElements.DragAndDropPosition dropPosition)
			{
				this.insertAtIndex = insertAtIndex;
				this.targetIndex = targetIndex;
				this.parentIndex = parentIndex;
				this.childIndex = childIndex;
				this.dropPosition = dropPosition;
				dragVisualMode = global::UnityEngine.UIElements.DragVisualMode.Move;
			}

			public bool Equals(global::Unity.Hierarchy.HierarchyViewDragHandler.HierarchyViewDragAndDropTargets other)
			{
				return parentIndex == other.parentIndex && childIndex == other.childIndex && dropPosition == other.dropPosition && targetIndex == other.targetIndex;
			}

			public override bool Equals(object obj)
			{
				return obj is global::Unity.Hierarchy.HierarchyViewDragHandler.HierarchyViewDragAndDropTargets other && Equals(other);
			}

			public override int GetHashCode()
			{
				return global::System.HashCode.Combine(parentIndex, childIndex, (int)dropPosition);
			}
		}

		private class AutoExpansionData
		{
			public global::Unity.Hierarchy.HierarchyNode[] expandedNodesBeforeDrag;

			public int lastItemIndex = -1;

			public float expandItemBeginTimerMs;

			public global::UnityEngine.Vector2 expandItemBeginPosition;
		}

		internal const string DragHoverBarStyleName = "hierarchy__container__drag-hover-bar";

		internal const string DragHoverBarItemName = "HierarchyHoverBar";

		internal const string DragHoverItemMarkerItemName = "HierarchyHoverItemMarker";

		internal const string DragHoverSiblingMarkerItemName = "HierarchyHoverSiblingMarker";

		private readonly global::Unity.Hierarchy.HierarchyView m_HierarchyView;

		private readonly global::UnityEngine.UIElements.MultiColumnListView m_MultiColumnListView;

		private global::Unity.Hierarchy.HierarchyViewDragHandler.HierarchyViewDragAndDropTargets m_LastDragPosition;

		private global::Unity.Hierarchy.HierarchyViewDragHandler.AutoExpansionData m_AutoExpansionData;

		private global::UnityEngine.UIElements.IVisualElementScheduledItem m_ExpandItemScheduledItem;

		private global::UnityEngine.UIElements.VisualElement m_DragHoverBar;

		private global::UnityEngine.UIElements.VisualElement m_DragHoverItemMarker;

		private global::UnityEngine.UIElements.VisualElement m_DragHoverSiblingMarker;

		private global::UnityEngine.EventModifiers m_CurrentEventModifiers;

		private float m_LeftIndentation = -1f;

		private float m_SiblingBottom = -1f;

		private const int k_DragHoverBarHeight = 2;

		private const int k_InvalidIndex = -1;

		private const long k_ExpandUpdateIntervalMs = 10L;

		private const float k_DropExpandTimeoutMs = 700f;

		private const float k_DropDeltaPosition = 100f;

		private const float k_HalfDropBetweenHeight = 4f;

		private const float k_DefaultIndentWidth = 14f;

		private const float k_DragHoverBarPositionOffset = 4f;

		private global::Unity.Hierarchy.Hierarchy Hierarchy => m_HierarchyView.Source;

		private global::Unity.Hierarchy.HierarchyFlattened HierarchyFlattened => m_HierarchyView.Flattened;

		private global::Unity.Hierarchy.HierarchyViewModel HierarchyViewModel => m_HierarchyView.ViewModel;

		private global::UnityEngine.UIElements.BaseVerticalCollectionView TargetView => m_MultiColumnListView;

		private global::UnityEngine.UIElements.ScrollView TargetScrollView => global::UnityEngine.UIElements.UQueryExtensions.Q<global::UnityEngine.UIElements.ScrollView>(TargetView);

		public HierarchyViewDragHandler(global::Unity.Hierarchy.HierarchyView hierarchyView)
		{
			m_HierarchyView = hierarchyView;
			m_MultiColumnListView = m_HierarchyView.ListView;
			m_AutoExpansionData = new global::Unity.Hierarchy.HierarchyViewDragHandler.AutoExpansionData();
			m_MultiColumnListView.canStartDrag += CanStartDrag;
			m_MultiColumnListView.setupDragAndDrop += SetupDragAndDrop;
			m_MultiColumnListView.dragAndDropUpdate += DragAndDropUpdate;
			m_MultiColumnListView.handleDrop += HandleDrop;
			m_MultiColumnListView.RegisterCallback<global::UnityEngine.UIElements.PointerLeaveEvent>(OnPointerLeave);
			m_MultiColumnListView.RegisterCallback<global::UnityEngine.UIElements.PointerDownEvent>(OnPointerDown, global::UnityEngine.UIElements.TrickleDown.TrickleDown);
			m_MultiColumnListView.RegisterCallback<global::UnityEngine.UIElements.PointerMoveEvent>(OnPointerMove, global::UnityEngine.UIElements.TrickleDown.TrickleDown);
			m_HierarchyView.RegisterCallback<global::UnityEngine.UIElements.PointerUpEvent>(OnPointerUp, global::UnityEngine.UIElements.TrickleDown.TrickleDown);
		}

		private void OnPointerLeave(global::UnityEngine.UIElements.PointerLeaveEvent evt)
		{
			ClearDragAndDropUI();
		}

		private void OnPointerDown(global::UnityEngine.UIElements.PointerDownEvent evt)
		{
			m_CurrentEventModifiers = evt.modifiers;
		}

		private void OnPointerMove(global::UnityEngine.UIElements.PointerMoveEvent evt)
		{
			m_CurrentEventModifiers = evt.modifiers;
		}

		private void OnPointerUp(global::UnityEngine.UIElements.PointerUpEvent evt)
		{
			m_CurrentEventModifiers = evt.modifiers;
		}

		private bool IsSearchActive()
		{
			return m_HierarchyView.Filtering;
		}

		private bool CanStartDrag(global::UnityEngine.UIElements.CanStartDragArgs args)
		{
			if (IsSearchActive())
			{
				return false;
			}
			global::UnityEngine.Pool.RentSpanUnmanaged<global::Unity.Hierarchy.HierarchyNode> rentSpan = new global::UnityEngine.Pool.RentSpanUnmanaged<global::Unity.Hierarchy.HierarchyNode>(HierarchyViewModel.HasAllFlagsCount(global::Unity.Hierarchy.HierarchyNodeFlags.Selected));
			try
			{
				HierarchyViewModel.GetNodesWithAllFlags(global::Unity.Hierarchy.HierarchyNodeFlags.Selected, rentSpan);
				foreach (global::Unity.Hierarchy.HierarchyNodeTypeHandler item in Hierarchy.EnumerateNodeTypeHandlers())
				{
					if (item is global::Unity.Hierarchy.IHierarchyEditorNodeTypeHandler hierarchyEditorNodeTypeHandler && !hierarchyEditorNodeTypeHandler.CanStartDrag(m_HierarchyView, rentSpan))
					{
						return false;
					}
				}
				return true;
			}
			finally
			{
				rentSpan.Dispose();
			}
		}

		private global::UnityEngine.UIElements.StartDragArgs SetupDragAndDrop(global::UnityEngine.UIElements.SetupDragAndDropArgs args)
		{
			int length = HierarchyViewModel.HasAllFlagsCount(global::Unity.Hierarchy.HierarchyNodeFlags.Selected);
			global::UnityEngine.Pool.RentSpanUnmanaged<global::Unity.Hierarchy.HierarchyNode> rentSpan = new global::UnityEngine.Pool.RentSpanUnmanaged<global::Unity.Hierarchy.HierarchyNode>(length);
			HierarchyViewModel.GetNodesWithAllFlags(global::Unity.Hierarchy.HierarchyNodeFlags.Selected, rentSpan);
			global::System.Collections.Generic.List<global::UnityEngine.EntityId> entityIds = new global::System.Collections.Generic.List<global::UnityEngine.EntityId>();
			global::System.Collections.Generic.List<string> value;
			using (global::UnityEngine.Pool.CollectionPool<global::System.Collections.Generic.List<string>, string>.Get(out value))
			{
				global::System.Collections.Generic.Dictionary<string, object> value2;
				using (global::UnityEngine.Pool.CollectionPool<global::System.Collections.Generic.Dictionary<string, object>, global::System.Collections.Generic.KeyValuePair<string, object>>.Get(out value2))
				{
					global::Unity.Hierarchy.HierarchyViewDragAndDropSetupData data = new global::Unity.Hierarchy.HierarchyViewDragAndDropSetupData(rentSpan, entityIds, value, m_HierarchyView, value2);
					foreach (global::Unity.Hierarchy.HierarchyNodeTypeHandler item in Hierarchy.EnumerateNodeTypeHandlers())
					{
						if (item is global::Unity.Hierarchy.IHierarchyEditorNodeTypeHandler hierarchyEditorNodeTypeHandler)
						{
							hierarchyEditorNodeTypeHandler.OnStartDrag(in data);
						}
					}
					global::UnityEngine.UIElements.StartDragArgs result = new global::UnityEngine.UIElements.StartDragArgs(args.startDragArgs.title, args.startDragArgs.visualMode);
					result.SetEntityIds(entityIds);
					result.SetPaths(value.ToArray());
					foreach (global::System.Collections.Generic.KeyValuePair<string, object> item2 in value2)
					{
						result.SetGenericData(item2.Key, item2.Value);
					}
					return result;
				}
			}
		}

		private global::UnityEngine.UIElements.DragVisualMode DragAndDropUpdate(global::UnityEngine.UIElements.HandleDragAndDropArgs args)
		{
			global::Unity.Hierarchy.HierarchyViewDragHandler.HierarchyViewDragAndDropTargets visualMode = GetVisualMode(in args);
			if (visualMode.dragVisualMode == global::UnityEngine.UIElements.DragVisualMode.Rejected)
			{
				ClearDragAndDropUI();
			}
			else
			{
				HandleAutoExpansion(visualMode, args.position);
				ApplyDragAndDropUI(visualMode);
			}
			return visualMode.dragVisualMode;
		}

		private global::Unity.Hierarchy.HierarchyViewDragHandler.HierarchyViewDragAndDropTargets GetVisualMode(in global::UnityEngine.UIElements.HandleDragAndDropArgs args)
		{
			if (args.insertAtIndex < 0)
			{
				return global::Unity.Hierarchy.HierarchyViewDragHandler.HierarchyViewDragAndDropTargets.Rejected;
			}
			global::Unity.Hierarchy.HierarchyViewDragHandler.HierarchyViewDragAndDropTargets dragAndDropTargets = GetDragAndDropTargets(in args);
			global::Unity.Hierarchy.HierarchyNode parentNode = ((dragAndDropTargets.parentIndex == -1) ? Hierarchy.Root : HierarchyViewModel[dragAndDropTargets.parentIndex]);
			dragAndDropTargets = HandleNodeHandlersDrop(dragAndDropTargets, args.dragAndDropData, in parentNode, perform: false);
			if (dragAndDropTargets.dragVisualMode != global::UnityEngine.UIElements.DragVisualMode.None)
			{
				return dragAndDropTargets;
			}
			return HandleDefaultCanDrop(in args, dragAndDropTargets, in parentNode);
		}

		private global::Unity.Hierarchy.HierarchyViewDragHandler.HierarchyViewDragAndDropTargets HandleDefaultCanDrop(in global::UnityEngine.UIElements.HandleDragAndDropArgs args, global::Unity.Hierarchy.HierarchyViewDragHandler.HierarchyViewDragAndDropTargets dragAndDropTargets, in global::Unity.Hierarchy.HierarchyNode parentNode)
		{
			if (!DragSourceIsCurrentListView(in args))
			{
				return global::Unity.Hierarchy.HierarchyViewDragHandler.HierarchyViewDragAndDropTargets.Rejected;
			}
			global::UnityEngine.Pool.RentSpanUnmanaged<global::Unity.Hierarchy.HierarchyNode> rentSpan = new global::UnityEngine.Pool.RentSpanUnmanaged<global::Unity.Hierarchy.HierarchyNode>(HierarchyViewModel.HasAllFlagsCount(global::Unity.Hierarchy.HierarchyNodeFlags.Selected));
			try
			{
				HierarchyViewModel.GetNodesWithAllFlags(global::Unity.Hierarchy.HierarchyNodeFlags.Selected, rentSpan);
				global::Unity.Hierarchy.IHierarchyEditorNodeTypeHandler hierarchyEditorNodeTypeHandler = ((parentNode == Hierarchy.Root) ? null : (Hierarchy.GetNodeTypeHandler(in parentNode) as global::Unity.Hierarchy.IHierarchyEditorNodeTypeHandler));
				for (int i = 0; i < rentSpan.Span.Length; i++)
				{
					global::Unity.Hierarchy.HierarchyNode target = rentSpan.Span[i];
					if (IsDescendant(in parentNode, in target))
					{
						return global::Unity.Hierarchy.HierarchyViewDragHandler.HierarchyViewDragAndDropTargets.Rejected;
					}
					global::Unity.Hierarchy.IHierarchyEditorNodeTypeHandler hierarchyEditorNodeTypeHandler2 = Hierarchy.GetNodeTypeHandler(in target) as global::Unity.Hierarchy.IHierarchyEditorNodeTypeHandler;
					if (hierarchyEditorNodeTypeHandler != null && !hierarchyEditorNodeTypeHandler.AcceptChild(m_HierarchyView, in target))
					{
						return global::Unity.Hierarchy.HierarchyViewDragHandler.HierarchyViewDragAndDropTargets.Rejected;
					}
					if (hierarchyEditorNodeTypeHandler2 != null && !hierarchyEditorNodeTypeHandler2.AcceptParent(m_HierarchyView, in parentNode))
					{
						return global::Unity.Hierarchy.HierarchyViewDragHandler.HierarchyViewDragAndDropTargets.Rejected;
					}
				}
				dragAndDropTargets.dragVisualMode = global::UnityEngine.UIElements.DragVisualMode.Move;
				return dragAndDropTargets;
			}
			finally
			{
				rentSpan.Dispose();
			}
		}

		private bool IsDescendant(in global::Unity.Hierarchy.HierarchyNode possibleDescendant, in global::Unity.Hierarchy.HierarchyNode target)
		{
			if (Hierarchy.GetDepth(in possibleDescendant) <= Hierarchy.GetDepth(in target))
			{
				return false;
			}
			global::Unity.Hierarchy.HierarchyNode lhs = Hierarchy.GetParent(in possibleDescendant);
			while (lhs != global::Unity.Hierarchy.HierarchyNode.Null)
			{
				if (lhs == target)
				{
					return true;
				}
				lhs = Hierarchy.GetParent(in lhs);
			}
			return false;
		}

		private global::Unity.Hierarchy.HierarchyViewDragHandler.HierarchyViewDragAndDropTargets HandleNodeHandlersDrop(global::Unity.Hierarchy.HierarchyViewDragHandler.HierarchyViewDragAndDropTargets dragAndDropTargets, global::UnityEngine.UIElements.DragAndDropData dragAndDropData, in global::Unity.Hierarchy.HierarchyNode parentNode, bool perform)
		{
			global::Unity.Hierarchy.HierarchyViewDragAndDropHandlingData data = new global::Unity.Hierarchy.HierarchyViewDragAndDropHandlingData(in parentNode, (dragAndDropTargets.targetIndex == -1 || dragAndDropTargets.targetIndex >= HierarchyViewModel.Count) ? global::Unity.Hierarchy.HierarchyNode.Null : HierarchyViewModel[dragAndDropTargets.targetIndex], dragAndDropTargets.insertAtIndex, dragAndDropTargets.dropPosition, dragAndDropData, m_HierarchyView, m_CurrentEventModifiers);
			foreach (global::Unity.Hierarchy.HierarchyNodeTypeHandler item in Hierarchy.EnumerateNodeTypeHandlers())
			{
				if (item is global::Unity.Hierarchy.IHierarchyEditorNodeTypeHandler hierarchyEditorNodeTypeHandler)
				{
					global::UnityEngine.UIElements.DragVisualMode dragVisualMode = (perform ? hierarchyEditorNodeTypeHandler.OnDrop(in data) : hierarchyEditorNodeTypeHandler.CanDrop(in data));
					if (dragVisualMode != global::UnityEngine.UIElements.DragVisualMode.None)
					{
						dragAndDropTargets.dragVisualMode = dragVisualMode;
						return dragAndDropTargets;
					}
				}
			}
			dragAndDropTargets.dragVisualMode = global::UnityEngine.UIElements.DragVisualMode.None;
			return dragAndDropTargets;
		}

		private global::UnityEngine.UIElements.DragVisualMode HandleDrop(global::UnityEngine.UIElements.HandleDragAndDropArgs args)
		{
			ClearDragAndDropUI();
			if (args.insertAtIndex < 0)
			{
				return global::UnityEngine.UIElements.DragVisualMode.Rejected;
			}
			global::Unity.Hierarchy.HierarchyViewDragHandler.HierarchyViewDragAndDropTargets dragAndDropTargets = GetDragAndDropTargets(in args);
			global::Unity.Hierarchy.HierarchyNode parentNode = ((dragAndDropTargets.parentIndex == -1) ? Hierarchy.Root : HierarchyViewModel[dragAndDropTargets.parentIndex]);
			int version = HierarchyViewModel.Version;
			dragAndDropTargets = HandleNodeHandlersDrop(dragAndDropTargets, args.dragAndDropData, in parentNode, perform: true);
			if (HierarchyViewModel.Version != version)
			{
				return global::UnityEngine.UIElements.DragVisualMode.Rejected;
			}
			global::UnityEngine.UIElements.DragVisualMode dragVisualMode = dragAndDropTargets.dragVisualMode;
			if (dragVisualMode == global::UnityEngine.UIElements.DragVisualMode.None)
			{
				dragVisualMode = HandleDefaultDrop(in args, dragAndDropTargets, in parentNode);
			}
			if (parentNode != Hierarchy.Root)
			{
				HierarchyViewModel.SetFlags(in parentNode, global::Unity.Hierarchy.HierarchyNodeFlags.Expanded);
			}
			ClearAutoExpansionData(restoreState: false);
			if (dragVisualMode != global::UnityEngine.UIElements.DragVisualMode.Rejected && dragVisualMode != global::UnityEngine.UIElements.DragVisualMode.None)
			{
				m_HierarchyView.EnqueuePostUpdateAction(delegate
				{
					global::Unity.Hierarchy.HierarchyViewModelNodesEnumerable.Enumerator enumerator = m_HierarchyView.ViewModel.EnumerateNodesWithAllFlags(global::Unity.Hierarchy.HierarchyNodeFlags.Selected).GetEnumerator();
					while (enumerator.MoveNext())
					{
						ref readonly global::Unity.Hierarchy.HierarchyNode current = ref enumerator.Current;
						if (!(current == global::Unity.Hierarchy.HierarchyNode.Null) && !(current == m_HierarchyView.Source.Root))
						{
							m_HierarchyView.Frame(in current);
							break;
						}
					}
				});
			}
			return dragVisualMode;
		}

		private global::UnityEngine.UIElements.DragVisualMode HandleDefaultDrop(in global::UnityEngine.UIElements.HandleDragAndDropArgs args, global::Unity.Hierarchy.HierarchyViewDragHandler.HierarchyViewDragAndDropTargets dragAndDropTargets, in global::Unity.Hierarchy.HierarchyNode parentNode)
		{
			if (!DragSourceIsCurrentListView(in args))
			{
				return global::UnityEngine.UIElements.DragVisualMode.Rejected;
			}
			global::UnityEngine.Pool.RentSpanUnmanaged<global::Unity.Hierarchy.HierarchyNode> rentSpanUnmanaged = new global::UnityEngine.Pool.RentSpanUnmanaged<global::Unity.Hierarchy.HierarchyNode>(HierarchyViewModel.HasAllFlagsCount(global::Unity.Hierarchy.HierarchyNodeFlags.Selected));
			try
			{
				global::System.Span<global::Unity.Hierarchy.HierarchyNode> outNodes = rentSpanUnmanaged.Span;
				HierarchyViewModel.GetNodesWithAllFlags(global::Unity.Hierarchy.HierarchyNodeFlags.Selected, outNodes);
				global::Unity.Hierarchy.HierarchyNode[] children = Hierarchy.GetChildren(in parentNode);
				int num = dragAndDropTargets.childIndex;
				for (int i = 0; i < outNodes.Length; i++)
				{
					if (outNodes[i] == parentNode)
					{
						for (int j = i; j < outNodes.Length - 1; j++)
						{
							outNodes[j] = outNodes[j + 1];
						}
						outNodes = outNodes.Slice(0, outNodes.Length - 1);
						break;
					}
				}
				global::System.Collections.Generic.List<global::Unity.Hierarchy.HierarchyNode> list = new global::System.Collections.Generic.List<global::Unity.Hierarchy.HierarchyNode>(outNodes.Length);
				global::System.Collections.Generic.List<int> list2 = new global::System.Collections.Generic.List<int>(outNodes.Length);
				int num2 = 0;
				for (int k = 0; k < outNodes.Length; k++)
				{
					global::Unity.Hierarchy.HierarchyNode lhs = Hierarchy.GetParent(in outNodes[k]);
					if (!(lhs == parentNode) && !list.Contains(lhs))
					{
						list.Add(lhs);
						int childrenCount = Hierarchy.GetChildrenCount(in lhs);
						list2.Add(childrenCount);
						num2 = global::System.Math.Max(num2, childrenCount);
					}
				}
				int num4;
				if (list.Count > 0)
				{
					global::Unity.Hierarchy.HierarchyNode[] array = new global::Unity.Hierarchy.HierarchyNode[num2];
					for (int l = 0; l < list.Count; l++)
					{
						global::Unity.Hierarchy.HierarchyNode node = list[l];
						Hierarchy.GetChildren(in node, array);
						int num3 = list2[l];
						num4 = 0;
						for (int m = 0; m < num3; m++)
						{
							global::Unity.Hierarchy.HierarchyNode node2 = array[m];
							if (!HierarchyViewModel.HasAllFlags(in node2, global::Unity.Hierarchy.HierarchyNodeFlags.Selected))
							{
								Hierarchy.SetSortIndex(in node2, num4++);
							}
						}
					}
				}
				for (int n = 0; n < outNodes.Length; n++)
				{
					Hierarchy.SetParent(in outNodes[n], in parentNode);
				}
				if (num == -1)
				{
					num = children.Length;
				}
				num4 = 0;
				for (int num5 = 0; num5 < num && num5 < children.Length; num5++)
				{
					global::Unity.Hierarchy.HierarchyNode node3 = children[num5];
					if (!HierarchyViewModel.HasAllFlags(in node3, global::Unity.Hierarchy.HierarchyNodeFlags.Selected))
					{
						Hierarchy.SetSortIndex(in node3, num4++);
					}
				}
				for (int num6 = 0; num6 < outNodes.Length; num6++)
				{
					Hierarchy.SetSortIndex(in outNodes[num6], num4++);
				}
				for (int num7 = num; num7 < children.Length; num7++)
				{
					global::Unity.Hierarchy.HierarchyNode node4 = children[num7];
					if (!HierarchyViewModel.HasAllFlags(in node4, global::Unity.Hierarchy.HierarchyNodeFlags.Selected))
					{
						Hierarchy.SetSortIndex(in node4, num4++);
					}
				}
				Hierarchy.SortChildren(in parentNode);
				return global::UnityEngine.UIElements.DragVisualMode.Move;
			}
			finally
			{
				rentSpanUnmanaged.Dispose();
			}
		}

		private global::Unity.Hierarchy.HierarchyViewDragHandler.HierarchyViewDragAndDropTargets GetDragAndDropTargets(in global::UnityEngine.UIElements.HandleDragAndDropArgs args)
		{
			global::System.ReadOnlySpan<int> draggedIndices;
			if (DragSourceIsCurrentListView(in args))
			{
				int length = HierarchyViewModel.HasAllFlagsCount(global::Unity.Hierarchy.HierarchyNodeFlags.Selected);
				global::UnityEngine.Pool.RentSpanUnmanaged<int> rentSpan = new global::UnityEngine.Pool.RentSpanUnmanaged<int>(length);
				try
				{
					HierarchyViewModel.GetIndicesWithAllFlags(global::Unity.Hierarchy.HierarchyNodeFlags.Selected, rentSpan);
					draggedIndices = rentSpan;
					return HandleTreePosition(in args, in draggedIndices);
				}
				finally
				{
					rentSpan.Dispose();
				}
			}
			draggedIndices = global::System.Array.Empty<int>();
			return HandleTreePosition(in args, in draggedIndices);
		}

		private bool DragSourceIsCurrentListView(in global::UnityEngine.UIElements.HandleDragAndDropArgs args)
		{
			return args.dragAndDropData.source == TargetView;
		}

		private global::Unity.Hierarchy.HierarchyViewDragHandler.HierarchyViewDragAndDropTargets HandleTreePosition(in global::UnityEngine.UIElements.HandleDragAndDropArgs dnDArgs, in global::System.ReadOnlySpan<int> draggedIndices)
		{
			m_LeftIndentation = -1f;
			m_SiblingBottom = -1f;
			if (dnDArgs.insertAtIndex < 0)
			{
				return new global::Unity.Hierarchy.HierarchyViewDragHandler.HierarchyViewDragAndDropTargets(dnDArgs.insertAtIndex, -1, -1, -1, global::UnityEngine.UIElements.DragAndDropPosition.OutsideItems);
			}
			if (dnDArgs.dropPosition == global::UnityEngine.UIElements.DragAndDropPosition.OverItem)
			{
				return new global::Unity.Hierarchy.HierarchyViewDragHandler.HierarchyViewDragAndDropTargets(dnDArgs.insertAtIndex, dnDArgs.insertAtIndex, dnDArgs.insertAtIndex, -1, global::UnityEngine.UIElements.DragAndDropPosition.OverItem);
			}
			if (dnDArgs.insertAtIndex <= 0)
			{
				return new global::Unity.Hierarchy.HierarchyViewDragHandler.HierarchyViewDragAndDropTargets(dnDArgs.insertAtIndex, 0, -1, 0, global::UnityEngine.UIElements.DragAndDropPosition.BetweenItems);
			}
			int indexFromWorldPosition = m_HierarchyView.GetIndexFromWorldPosition(dnDArgs.position, 4f);
			if (indexFromWorldPosition >= m_HierarchyView.ViewModel.Count)
			{
				return new global::Unity.Hierarchy.HierarchyViewDragHandler.HierarchyViewDragAndDropTargets(dnDArgs.insertAtIndex, 0, -1, -1, global::UnityEngine.UIElements.DragAndDropPosition.OutsideItems);
			}
			return HandleSiblingInsertionAtAvailableDepthsAndChangeTargetIfNeeded(in dnDArgs, dnDArgs.position, in draggedIndices);
		}

		private global::Unity.Hierarchy.HierarchyViewDragHandler.HierarchyViewDragAndDropTargets HandleSiblingInsertionAtAvailableDepthsAndChangeTargetIfNeeded(in global::UnityEngine.UIElements.HandleDragAndDropArgs dnDArgs, in global::UnityEngine.Vector2 pointerPosition, in global::System.ReadOnlySpan<int> draggedIndices)
		{
			int insertAtIndex = dnDArgs.insertAtIndex;
			GetPreviousAndNextIndexesIgnoringDraggedItems(dnDArgs.insertAtIndex, out var previousNodeIndex, out var nextNodeIndex, in draggedIndices);
			global::Unity.Hierarchy.HierarchyViewDragHandler.HierarchyViewDragAndDropTargets result = new global::Unity.Hierarchy.HierarchyViewDragHandler.HierarchyViewDragAndDropTargets(dnDArgs.insertAtIndex, dnDArgs.insertAtIndex, -1, -1, global::UnityEngine.UIElements.DragAndDropPosition.BetweenItems);
			if (previousNodeIndex == -1)
			{
				return result;
			}
			global::Unity.Hierarchy.HierarchyNode node = HierarchyViewModel[previousNodeIndex];
			global::Unity.Hierarchy.HierarchyNode node2 = ((nextNodeIndex == -1) ? global::Unity.Hierarchy.HierarchyNode.Null : HierarchyViewModel[nextNodeIndex]);
			bool flag = HierarchyFlattened.GetChildrenCount(in node) > 0 && HierarchyViewModel.HasAllFlags(in node, global::Unity.Hierarchy.HierarchyNodeFlags.Expanded);
			int depth = HierarchyFlattened.GetDepth(in node);
			int num = ((nextNodeIndex != -1) ? HierarchyFlattened.GetDepth(in node2) : 0);
			int num2 = num;
			int num3 = depth + (flag ? 1 : 0);
			int num4 = previousNodeIndex;
			result.targetIndex = previousNodeIndex;
			global::Unity.Hierarchy.HierarchyNode node3 = node;
			int num5 = depth;
			float num6 = 0f;
			float num7 = 14f;
			global::UnityEngine.UIElements.VisualElement visualElement = null;
			if (depth > 0)
			{
				visualElement = TargetView.GetRootElementForIndex(previousNodeIndex);
			}
			else
			{
				global::Unity.Hierarchy.HierarchyNode lhs = ((dnDArgs.insertAtIndex == -1 || dnDArgs.insertAtIndex >= HierarchyViewModel.Count) ? global::Unity.Hierarchy.HierarchyNode.Null : HierarchyViewModel[dnDArgs.insertAtIndex]);
				int num8 = ((!(lhs == global::Unity.Hierarchy.HierarchyNode.Null)) ? HierarchyFlattened.GetDepth(in lhs) : 0);
				if (num8 > 0)
				{
					visualElement = TargetView.GetRootElementForIndex(dnDArgs.insertAtIndex);
				}
			}
			global::Unity.Hierarchy.HierarchyViewItem hierarchyViewItem = global::UnityEngine.UIElements.UQueryExtensions.Q<global::Unity.Hierarchy.HierarchyViewItem>(visualElement?);
			if (hierarchyViewItem != null)
			{
				num6 = hierarchyViewItem.Toggle.layout.width;
				num7 = ((depth > 0) ? ((hierarchyViewItem.LeftContainer.style.translate.value.x.value + 4f) / (float)depth) : num7);
			}
			global::UnityEngine.UIElements.VisualElement nameColumn = GetNameColumn();
			bool flag2 = false;
			global::UnityEngine.Vector2 vector = global::UnityEngine.Vector2.zero;
			if (nameColumn != null)
			{
				vector = global::UnityEngine.UIElements.VisualElementExtensions.WorldToLocal(nameColumn, pointerPosition);
				flag2 = vector.x >= 0f && vector.x < nameColumn.layout.width;
			}
			if (num3 <= num2)
			{
				m_LeftIndentation = num6 + num7 * (float)num2;
				if (flag)
				{
					result.parentIndex = previousNodeIndex;
					result.childIndex = 0;
				}
				else
				{
					global::Unity.Hierarchy.HierarchyNode node4 = HierarchyFlattened.GetParent(in node);
					result.parentIndex = GetNodeIndex(in node4);
					result.childIndex = ((nextNodeIndex == -1) ? HierarchyFlattened.GetChildrenCount(in node4) : GetChildIndex(in node2));
				}
				return result;
			}
			int num9 = (flag2 ? global::UnityEngine.Mathf.FloorToInt((vector.x - num6) / num7) : num3);
			if (num9 >= num3)
			{
				m_LeftIndentation = num6 + num7 * (float)num3;
				if (flag)
				{
					result.parentIndex = previousNodeIndex;
					result.childIndex = 0;
				}
				else
				{
					result.parentIndex = GetNodeIndex(HierarchyFlattened.GetParent(in node));
					result.childIndex = GetChildIndex(in node) + 1;
				}
				return result;
			}
			while (num5 > num2 && num5 != num9)
			{
				node3 = HierarchyFlattened.GetParent(in node3);
				num4 = GetNodeIndex(in node3);
				num5--;
			}
			if (num4 != insertAtIndex)
			{
				global::UnityEngine.UIElements.VisualElement rootElementForIndex = TargetView.GetRootElementForIndex(num4);
				if (rootElementForIndex != null)
				{
					global::UnityEngine.UIElements.VisualElement contentViewport = TargetScrollView.contentViewport;
					global::UnityEngine.Rect rect = global::UnityEngine.UIElements.VisualElementExtensions.WorldToLocal(contentViewport, rootElementForIndex.worldBound);
					if (contentViewport.localBound.yMin < rect.yMax && rect.yMax < contentViewport.localBound.yMax)
					{
						m_SiblingBottom = rect.yMax;
					}
				}
			}
			result.parentIndex = GetNodeIndex(HierarchyFlattened.GetParent(in node3));
			result.targetIndex = num4;
			result.childIndex = GetChildIndex(in node3) + 1;
			m_LeftIndentation = num6 + num7 * (float)num5;
			return result;
		}

		private void GetPreviousAndNextIndexesIgnoringDraggedItems(int insertAtIndex, out int previousNodeIndex, out int nextNodeIndex, in global::System.ReadOnlySpan<int> draggedIndices)
		{
			previousNodeIndex = (nextNodeIndex = -1);
			int num = insertAtIndex - 1;
			int i = insertAtIndex;
			while (num >= 0)
			{
				if (!draggedIndices.Contains(num))
				{
					previousNodeIndex = num;
					break;
				}
				num--;
			}
			for (int count = HierarchyViewModel.Count; i < count; i++)
			{
				if (!draggedIndices.Contains(i))
				{
					nextNodeIndex = i;
					break;
				}
			}
		}

		private int GetChildIndex(in global::Unity.Hierarchy.HierarchyNode childNode)
		{
			return HierarchyFlattened.GetChildIndex(in childNode);
		}

		private int GetNodeIndex(in global::Unity.Hierarchy.HierarchyNode node)
		{
			return HierarchyViewModel.IndexOf(in node);
		}

		private void ApplyDragAndDropUI(global::Unity.Hierarchy.HierarchyViewDragHandler.HierarchyViewDragAndDropTargets dragTargets)
		{
			if (m_LastDragPosition.Equals(dragTargets))
			{
				return;
			}
			global::UnityEngine.UIElements.ScrollView targetScrollView = TargetScrollView;
			if (m_DragHoverBar == null)
			{
				m_DragHoverBar = new global::UnityEngine.UIElements.VisualElement
				{
					name = "HierarchyHoverBar"
				};
				m_DragHoverBar.AddToClassList(global::UnityEngine.UIElements.BaseVerticalCollectionView.dragHoverBarUssClassName);
				m_DragHoverBar.AddToClassList("hierarchy__container__drag-hover-bar");
				m_DragHoverBar.style.width = TargetView.localBound.width;
				m_DragHoverBar.style.visibility = global::UnityEngine.UIElements.Visibility.Hidden;
				m_DragHoverBar.pickingMode = global::UnityEngine.UIElements.PickingMode.Ignore;
				TargetView.RegisterCallback<global::UnityEngine.UIElements.GeometryChangedEvent>(GeometryChangedCallback);
				targetScrollView.contentViewport.Add(m_DragHoverBar);
			}
			if (m_DragHoverItemMarker == null)
			{
				m_DragHoverItemMarker = new global::UnityEngine.UIElements.VisualElement
				{
					name = "HierarchyHoverItemMarker"
				};
				m_DragHoverItemMarker.AddToClassList(global::UnityEngine.UIElements.BaseVerticalCollectionView.dragHoverMarkerUssClassName);
				m_DragHoverItemMarker.style.visibility = global::UnityEngine.UIElements.Visibility.Hidden;
				m_DragHoverItemMarker.pickingMode = global::UnityEngine.UIElements.PickingMode.Ignore;
				m_DragHoverBar.Add(m_DragHoverItemMarker);
				m_DragHoverSiblingMarker = new global::UnityEngine.UIElements.VisualElement
				{
					name = "HierarchyHoverSiblingMarker"
				};
				m_DragHoverSiblingMarker.AddToClassList(global::UnityEngine.UIElements.BaseVerticalCollectionView.dragHoverMarkerUssClassName);
				m_DragHoverSiblingMarker.style.visibility = global::UnityEngine.UIElements.Visibility.Hidden;
				m_DragHoverSiblingMarker.pickingMode = global::UnityEngine.UIElements.PickingMode.Ignore;
				targetScrollView.contentViewport.Add(m_DragHoverSiblingMarker);
			}
			ClearDragAndDropUI();
			m_LastDragPosition = dragTargets;
			switch (dragTargets.dropPosition)
			{
			case global::UnityEngine.UIElements.DragAndDropPosition.OverItem:
				break;
			case global::UnityEngine.UIElements.DragAndDropPosition.BetweenItems:
			{
				if (dragTargets.insertAtIndex == 0)
				{
					PlaceHoverBarAt(0f);
					break;
				}
				global::UnityEngine.UIElements.VisualElement rootElementForIndex2 = TargetView.GetRootElementForIndex(dragTargets.insertAtIndex - 1);
				global::UnityEngine.UIElements.VisualElement rootElementForIndex3 = TargetView.GetRootElementForIndex(dragTargets.insertAtIndex);
				PlaceHoverBarAtElement(rootElementForIndex2 ?? rootElementForIndex3);
				break;
			}
			case global::UnityEngine.UIElements.DragAndDropPosition.OutsideItems:
			{
				global::UnityEngine.UIElements.VisualElement rootElementForIndex = TargetView.GetRootElementForIndex(TargetView.itemsSource.Count - 1);
				if (rootElementForIndex != null)
				{
					PlaceHoverBarAtElement(rootElementForIndex);
				}
				else
				{
					PlaceHoverBarAt(0f);
				}
				break;
			}
			default:
				throw new global::System.ArgumentOutOfRangeException("dropPosition", dragTargets.dropPosition, "Unsupported dropPosition value");
			}
			void GeometryChangedCallback(global::UnityEngine.UIElements.GeometryChangedEvent e)
			{
				m_DragHoverBar.style.width = TargetView.localBound.width;
			}
		}

		private void ClearDragAndDropUI()
		{
			m_LastDragPosition = default(global::Unity.Hierarchy.HierarchyViewDragHandler.HierarchyViewDragAndDropTargets);
			if (m_DragHoverBar != null)
			{
				m_DragHoverBar.style.visibility = global::UnityEngine.UIElements.Visibility.Hidden;
			}
			if (m_DragHoverItemMarker != null)
			{
				m_DragHoverItemMarker.style.visibility = global::UnityEngine.UIElements.Visibility.Hidden;
			}
			if (m_DragHoverSiblingMarker != null)
			{
				m_DragHoverSiblingMarker.style.visibility = global::UnityEngine.UIElements.Visibility.Hidden;
			}
		}

		private void ClearDragAndDrop()
		{
			m_CurrentEventModifiers = global::UnityEngine.EventModifiers.None;
			ClearDragAndDropUI();
			ClearAutoExpansionData();
		}

		private void ClearAutoExpansionData(bool restoreState = true)
		{
			if (restoreState && m_AutoExpansionData?.expandedNodesBeforeDrag != null)
			{
				RestoreExpanded(m_AutoExpansionData.expandedNodesBeforeDrag);
			}
			m_AutoExpansionData = new global::Unity.Hierarchy.HierarchyViewDragHandler.AutoExpansionData();
			m_ExpandItemScheduledItem?.Pause();
		}

		private void RestoreExpanded(global::System.ReadOnlySpan<global::Unity.Hierarchy.HierarchyNode> expandedNodes)
		{
			using (new global::Unity.Hierarchy.HierarchyViewModelFlagsChangeScope(HierarchyViewModel))
			{
				HierarchyViewModel.ClearFlags(global::Unity.Hierarchy.HierarchyNodeFlags.Expanded);
				HierarchyViewModel.SetFlags(expandedNodes, global::Unity.Hierarchy.HierarchyNodeFlags.Expanded);
			}
		}

		private float GetHoverBarTopPosition(global::UnityEngine.UIElements.VisualElement item)
		{
			global::UnityEngine.UIElements.VisualElement contentViewport = TargetScrollView.contentViewport;
			return global::UnityEngine.Mathf.Min(global::UnityEngine.UIElements.VisualElementExtensions.WorldToLocal(contentViewport, item.worldBound).yMax, contentViewport.localBound.yMax - 2f);
		}

		private void PlaceHoverBarAtElement(global::UnityEngine.UIElements.VisualElement item)
		{
			PlaceHoverBarAt(GetHoverBarTopPosition(item), m_LeftIndentation, m_SiblingBottom);
		}

		private void PlaceHoverBarAt(float top, float indentationPadding = -1f, float siblingBottom = -1f)
		{
			m_DragHoverBar.style.top = top;
			m_DragHoverBar.style.visibility = global::UnityEngine.UIElements.Visibility.Visible;
			global::UnityEngine.Rect nameColumnLayout = GetNameColumnLayout();
			float xMin = nameColumnLayout.xMin;
			float width = TargetView.localBound.width;
			if (nameColumnLayout.width > 0f)
			{
				width = nameColumnLayout.width;
			}
			else
			{
				indentationPadding = -1f;
			}
			if (m_DragHoverItemMarker != null)
			{
				m_DragHoverItemMarker.style.visibility = global::UnityEngine.UIElements.Visibility.Visible;
			}
			if (indentationPadding >= 0f)
			{
				m_DragHoverBar.style.marginLeft = xMin + indentationPadding;
				m_DragHoverBar.style.width = width - indentationPadding;
				if (siblingBottom > 0f && m_DragHoverSiblingMarker != null)
				{
					m_DragHoverSiblingMarker.style.top = siblingBottom;
					m_DragHoverSiblingMarker.style.visibility = global::UnityEngine.UIElements.Visibility.Visible;
					m_DragHoverSiblingMarker.style.marginLeft = xMin + indentationPadding;
				}
			}
			else
			{
				m_DragHoverBar.style.marginLeft = xMin;
				m_DragHoverBar.style.width = width;
			}
		}

		private global::UnityEngine.UIElements.VisualElement GetNameColumn()
		{
			return global::UnityEngine.UIElements.UQueryExtensions.Q(TargetView, "HierarchyViewColumn Name");
		}

		private global::UnityEngine.Rect GetNameColumnLayout()
		{
			return GetNameColumn()?.layout ?? global::UnityEngine.Rect.zero;
		}

		private void HandleAutoExpansion(global::Unity.Hierarchy.HierarchyViewDragHandler.HierarchyViewDragAndDropTargets dropTargets, global::UnityEngine.Vector2 pointerPosition)
		{
			if (dropTargets.dropPosition == global::UnityEngine.UIElements.DragAndDropPosition.OverItem)
			{
				int parentIndex = dropTargets.parentIndex;
				global::UnityEngine.UIElements.VisualElement rootElementForIndex = m_MultiColumnListView.GetRootElementForIndex(parentIndex);
				if (rootElementForIndex != null)
				{
					HandleAutoExpansion(rootElementForIndex, parentIndex, pointerPosition);
				}
			}
		}

		private void HandleAutoExpansion(global::UnityEngine.UIElements.VisualElement item, int itemIndex, global::UnityEngine.Vector2 pointerPosition)
		{
			global::UnityEngine.Rect worldBound = item.worldBound;
			bool flag = new global::UnityEngine.Rect(worldBound.x, worldBound.y + 4f, worldBound.width, worldBound.height - 8f).Contains(pointerPosition);
			global::UnityEngine.Vector2 vector = m_AutoExpansionData.expandItemBeginPosition - pointerPosition;
			if (itemIndex != m_AutoExpansionData.lastItemIndex || !flag || vector.sqrMagnitude >= 100f)
			{
				m_AutoExpansionData.lastItemIndex = itemIndex;
				m_AutoExpansionData.expandItemBeginTimerMs = 0f;
				m_AutoExpansionData.expandItemBeginPosition = pointerPosition;
				DelayExpandItem();
			}
		}

		private void DelayExpandItem()
		{
			if (m_ExpandItemScheduledItem == null)
			{
				m_ExpandItemScheduledItem = m_MultiColumnListView.schedule.Execute(ExpandItem).Every(10L);
				return;
			}
			m_ExpandItemScheduledItem.Pause();
			m_ExpandItemScheduledItem.Resume();
		}

		internal void ExpandItem(global::UnityEngine.UIElements.TimerState state)
		{
			m_AutoExpansionData.expandItemBeginTimerMs = (float)state.deltaTime + m_AutoExpansionData.expandItemBeginTimerMs;
			bool flag = m_AutoExpansionData.expandItemBeginTimerMs > 700f;
			int lastItemIndex = m_AutoExpansionData.lastItemIndex;
			if (!flag || lastItemIndex < 0 || lastItemIndex >= HierarchyViewModel.Count)
			{
				return;
			}
			global::Unity.Hierarchy.HierarchyNode node = HierarchyViewModel[lastItemIndex];
			bool flag2 = HierarchyViewModel.GetChildrenCount(in node) > 0;
			bool flag3 = HierarchyViewModel.HasAllFlags(in node, global::Unity.Hierarchy.HierarchyNodeFlags.Expanded);
			if (!(!flag2 || flag3))
			{
				global::Unity.Hierarchy.HierarchyNode[] nodesWithAllFlags = HierarchyViewModel.GetNodesWithAllFlags(global::Unity.Hierarchy.HierarchyNodeFlags.Expanded);
				global::Unity.Hierarchy.HierarchyViewDragHandler.AutoExpansionData autoExpansionData = m_AutoExpansionData;
				if (autoExpansionData.expandedNodesBeforeDrag == null)
				{
					autoExpansionData.expandedNodesBeforeDrag = nodesWithAllFlags;
				}
				m_AutoExpansionData.expandItemBeginTimerMs = 0f;
				m_AutoExpansionData.lastItemIndex = -1;
				HierarchyViewModel.SetFlags(in node, global::Unity.Hierarchy.HierarchyNodeFlags.Expanded);
			}
		}
	}
}
