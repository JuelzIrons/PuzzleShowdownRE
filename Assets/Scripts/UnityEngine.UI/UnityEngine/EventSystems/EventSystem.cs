namespace UnityEngine.EventSystems
{
	[global::UnityEngine.AddComponentMenu("Event/Event System")]
	[global::UnityEngine.DisallowMultipleComponent]
	public class EventSystem : global::UnityEngine.EventSystems.UIBehaviour
	{
		private struct UIToolkitOverrideConfigOld
		{
			public global::UnityEngine.EventSystems.EventSystem activeEventSystem;

			public bool sendEvents;

			public bool createPanelGameObjectsOnStart;
		}

		private global::System.Collections.Generic.List<global::UnityEngine.EventSystems.BaseInputModule> m_SystemInputModules = new global::System.Collections.Generic.List<global::UnityEngine.EventSystems.BaseInputModule>();

		private global::UnityEngine.EventSystems.BaseInputModule m_CurrentInputModule;

		private static global::System.Collections.Generic.List<global::UnityEngine.EventSystems.EventSystem> m_EventSystems = new global::System.Collections.Generic.List<global::UnityEngine.EventSystems.EventSystem>();

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Serialization.FormerlySerializedAs("m_Selected")]
		private global::UnityEngine.GameObject m_FirstSelected;

		[global::UnityEngine.SerializeField]
		private bool m_sendNavigationEvents = true;

		[global::UnityEngine.SerializeField]
		private int m_DragThreshold = 10;

		private global::UnityEngine.GameObject m_CurrentSelected;

		private bool m_HasFocus = true;

		private bool m_SelectionGuard;

		private global::UnityEngine.EventSystems.BaseEventData m_DummyData;

		private static readonly global::System.Comparison<global::UnityEngine.EventSystems.RaycastResult> s_RaycastComparer = RaycastComparer;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.HideInInspector]
		private global::UnityEngine.UIElements.UIToolkitInteroperabilityBridge m_UIToolkitInterop = new global::UnityEngine.UIElements.UIToolkitInteroperabilityBridge();

		private static global::UnityEngine.EventSystems.EventSystem.UIToolkitOverrideConfigOld? s_UIToolkitOverrideConfigOld = null;

		public static global::UnityEngine.EventSystems.EventSystem current
		{
			get
			{
				if (m_EventSystems.Count <= 0)
				{
					return null;
				}
				return m_EventSystems[0];
			}
			set
			{
				int num = m_EventSystems.IndexOf(value);
				if (num > 0)
				{
					m_EventSystems.RemoveAt(num);
					m_EventSystems.Insert(0, value);
				}
				else if (num < 0)
				{
					global::UnityEngine.Debug.LogError("Failed setting EventSystem.current to unknown EventSystem " + value);
				}
			}
		}

		public bool sendNavigationEvents
		{
			get
			{
				return m_sendNavigationEvents;
			}
			set
			{
				m_sendNavigationEvents = value;
			}
		}

		public int pixelDragThreshold
		{
			get
			{
				return m_DragThreshold;
			}
			set
			{
				m_DragThreshold = value;
			}
		}

		public global::UnityEngine.EventSystems.BaseInputModule currentInputModule => m_CurrentInputModule;

		public global::UnityEngine.GameObject firstSelectedGameObject
		{
			get
			{
				return m_FirstSelected;
			}
			set
			{
				m_FirstSelected = value;
			}
		}

		public global::UnityEngine.GameObject currentSelectedGameObject => m_CurrentSelected;

		[global::System.Obsolete("lastSelectedGameObject is no longer supported")]
		public global::UnityEngine.GameObject lastSelectedGameObject => null;

		public bool isFocused => m_HasFocus;

		public bool alreadySelecting => m_SelectionGuard;

		private global::UnityEngine.EventSystems.BaseEventData baseEventDataCache
		{
			get
			{
				if (m_DummyData == null)
				{
					m_DummyData = new global::UnityEngine.EventSystems.BaseEventData(this);
				}
				return m_DummyData;
			}
		}

		internal global::UnityEngine.UIElements.UIToolkitInteroperabilityBridge uiToolkitInterop => m_UIToolkitInterop;

		internal bool isOverridingUIToolkitEvents
		{
			get
			{
				if (uiToolkitInterop.overrideUIToolkitEvents)
				{
					return global::UnityEngine.UIElements.UIDocument.EnabledDocumentCount > 0;
				}
				return false;
			}
		}

		protected EventSystem()
		{
		}

		public void UpdateModules()
		{
			GetComponents(m_SystemInputModules);
			for (int num = m_SystemInputModules.Count - 1; num >= 0; num--)
			{
				if (!m_SystemInputModules[num] || !m_SystemInputModules[num].IsActive())
				{
					m_SystemInputModules.RemoveAt(num);
				}
			}
		}

		public void SetSelectedGameObject(global::UnityEngine.GameObject selected, global::UnityEngine.EventSystems.BaseEventData pointer)
		{
			if (m_SelectionGuard)
			{
				global::UnityEngine.Debug.LogError("Attempting to select " + selected?.ToString() + "while already selecting an object.");
				return;
			}
			m_SelectionGuard = true;
			if (selected == m_CurrentSelected)
			{
				m_SelectionGuard = false;
				return;
			}
			global::UnityEngine.EventSystems.ExecuteEvents.Execute(m_CurrentSelected, pointer, global::UnityEngine.EventSystems.ExecuteEvents.deselectHandler);
			m_CurrentSelected = selected;
			global::UnityEngine.EventSystems.ExecuteEvents.Execute(m_CurrentSelected, pointer, global::UnityEngine.EventSystems.ExecuteEvents.selectHandler);
			m_SelectionGuard = false;
		}

		public void SetSelectedGameObject(global::UnityEngine.GameObject selected)
		{
			SetSelectedGameObject(selected, baseEventDataCache);
		}

		private static int RaycastComparer(global::UnityEngine.EventSystems.RaycastResult lhs, global::UnityEngine.EventSystems.RaycastResult rhs)
		{
			if (lhs.module != rhs.module)
			{
				global::UnityEngine.Camera eventCamera = lhs.module.eventCamera;
				global::UnityEngine.Camera eventCamera2 = rhs.module.eventCamera;
				if (eventCamera != null && eventCamera2 != null && eventCamera.depth != eventCamera2.depth)
				{
					if (eventCamera.depth < eventCamera2.depth)
					{
						return 1;
					}
					if (eventCamera.depth == eventCamera2.depth)
					{
						return 0;
					}
					return -1;
				}
				if (lhs.module.sortOrderPriority != rhs.module.sortOrderPriority)
				{
					return rhs.module.sortOrderPriority.CompareTo(lhs.module.sortOrderPriority);
				}
				if (lhs.module.renderOrderPriority != rhs.module.renderOrderPriority)
				{
					return rhs.module.renderOrderPriority.CompareTo(lhs.module.renderOrderPriority);
				}
			}
			if (lhs.sortingLayer != rhs.sortingLayer)
			{
				int layerValueFromID = global::UnityEngine.SortingLayer.GetLayerValueFromID(rhs.sortingLayer);
				int layerValueFromID2 = global::UnityEngine.SortingLayer.GetLayerValueFromID(lhs.sortingLayer);
				return layerValueFromID.CompareTo(layerValueFromID2);
			}
			if (lhs.sortingOrder != rhs.sortingOrder)
			{
				return rhs.sortingOrder.CompareTo(lhs.sortingOrder);
			}
			if (lhs.depth != rhs.depth && lhs.module.rootRaycaster == rhs.module.rootRaycaster)
			{
				return rhs.depth.CompareTo(lhs.depth);
			}
			if (lhs.distance != rhs.distance)
			{
				return lhs.distance.CompareTo(rhs.distance);
			}
			if (lhs.sortingGroupID != global::UnityEngine.Rendering.SortingGroup.invalidSortingGroupID && rhs.sortingGroupID != global::UnityEngine.Rendering.SortingGroup.invalidSortingGroupID)
			{
				if (lhs.sortingGroupID != rhs.sortingGroupID)
				{
					return lhs.sortingGroupID.CompareTo(rhs.sortingGroupID);
				}
				if (lhs.sortingGroupOrder != rhs.sortingGroupOrder)
				{
					return rhs.sortingGroupOrder.CompareTo(lhs.sortingGroupOrder);
				}
			}
			return lhs.index.CompareTo(rhs.index);
		}

		public void RaycastAll(global::UnityEngine.EventSystems.PointerEventData eventData, global::System.Collections.Generic.List<global::UnityEngine.EventSystems.RaycastResult> raycastResults)
		{
			raycastResults.Clear();
			global::System.Collections.Generic.List<global::UnityEngine.EventSystems.BaseRaycaster> raycasters = global::UnityEngine.EventSystems.RaycasterManager.GetRaycasters();
			int count = raycasters.Count;
			for (int i = 0; i < count; i++)
			{
				global::UnityEngine.EventSystems.BaseRaycaster baseRaycaster = raycasters[i];
				if (!(baseRaycaster == null) && baseRaycaster.IsActive())
				{
					baseRaycaster.Raycast(eventData, raycastResults);
				}
			}
			raycastResults.Sort(s_RaycastComparer);
		}

		public bool IsPointerOverGameObject()
		{
			return IsPointerOverGameObject(-1);
		}

		public bool IsPointerOverGameObject(int pointerId)
		{
			if (m_CurrentInputModule != null)
			{
				return m_CurrentInputModule.IsPointerOverGameObject(pointerId);
			}
			return false;
		}

		[global::System.Obsolete("Use PanelInputConfiguration component instead.")]
		public static void SetUITookitEventSystemOverride(global::UnityEngine.EventSystems.EventSystem activeEventSystem, bool sendEvents = true, bool createPanelGameObjectsOnStart = true)
		{
			s_UIToolkitOverrideConfigOld = ((activeEventSystem == null && sendEvents && createPanelGameObjectsOnStart) ? ((global::UnityEngine.EventSystems.EventSystem.UIToolkitOverrideConfigOld?)null) : new global::UnityEngine.EventSystems.EventSystem.UIToolkitOverrideConfigOld?(new global::UnityEngine.EventSystems.EventSystem.UIToolkitOverrideConfigOld
			{
				activeEventSystem = activeEventSystem,
				sendEvents = sendEvents,
				createPanelGameObjectsOnStart = createPanelGameObjectsOnStart
			}));
			global::UnityEngine.EventSystems.EventSystem eventSystem = ((activeEventSystem != null) ? activeEventSystem : current);
			if (global::UnityEngine.UIElements.UIElementsRuntimeUtility.activeEventSystem != null && global::UnityEngine.UIElements.UIElementsRuntimeUtility.activeEventSystem != eventSystem)
			{
				((global::UnityEngine.EventSystems.EventSystem)global::UnityEngine.UIElements.UIElementsRuntimeUtility.activeEventSystem).uiToolkitInterop.overrideUIToolkitEvents = false;
			}
			if (eventSystem != null && eventSystem.isActiveAndEnabled)
			{
				eventSystem.uiToolkitInterop.overrideUIToolkitEvents = sendEvents;
				eventSystem.uiToolkitInterop.handlerTypes = (createPanelGameObjectsOnStart ? ((global::UnityEngine.UIElements.UIToolkitInteroperabilityBridge.EventHandlerTypes)(-1)) : ((global::UnityEngine.UIElements.UIToolkitInteroperabilityBridge.EventHandlerTypes)0));
			}
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			m_EventSystems.Add(this);
			if (s_UIToolkitOverrideConfigOld.HasValue)
			{
				m_UIToolkitInterop = new global::UnityEngine.UIElements.UIToolkitInteroperabilityBridge();
				if (!s_UIToolkitOverrideConfigOld.Value.sendEvents)
				{
					m_UIToolkitInterop.overrideUIToolkitEvents = false;
				}
				if (!s_UIToolkitOverrideConfigOld.Value.createPanelGameObjectsOnStart)
				{
					m_UIToolkitInterop.handlerTypes = (global::UnityEngine.UIElements.UIToolkitInteroperabilityBridge.EventHandlerTypes)0;
				}
			}
			m_UIToolkitInterop.eventSystem = this;
			m_UIToolkitInterop.OnEnable();
		}

		protected override void OnDisable()
		{
			m_UIToolkitInterop.OnDisable();
			if (m_CurrentInputModule != null)
			{
				m_CurrentInputModule.DeactivateModule();
				m_CurrentInputModule = null;
			}
			m_EventSystems.Remove(this);
			base.OnDisable();
		}

		protected override void Start()
		{
			base.Start();
			m_UIToolkitInterop.Start();
		}

		private void TickModules()
		{
			int count = m_SystemInputModules.Count;
			for (int i = 0; i < count; i++)
			{
				if (m_SystemInputModules[i] != null)
				{
					m_SystemInputModules[i].UpdateModule();
				}
			}
		}

		protected virtual void OnApplicationFocus(bool hasFocus)
		{
			m_HasFocus = hasFocus;
			if (!m_HasFocus)
			{
				TickModules();
			}
		}

		protected virtual void Update()
		{
			m_UIToolkitInterop.Update();
			if (current != this)
			{
				return;
			}
			TickModules();
			bool flag = false;
			int count = m_SystemInputModules.Count;
			for (int i = 0; i < count; i++)
			{
				global::UnityEngine.EventSystems.BaseInputModule baseInputModule = m_SystemInputModules[i];
				if (baseInputModule.IsModuleSupported() && baseInputModule.ShouldActivateModule())
				{
					if (m_CurrentInputModule != baseInputModule)
					{
						ChangeEventModule(baseInputModule);
						flag = true;
					}
					break;
				}
			}
			if (m_CurrentInputModule == null)
			{
				for (int j = 0; j < count; j++)
				{
					global::UnityEngine.EventSystems.BaseInputModule baseInputModule2 = m_SystemInputModules[j];
					if (baseInputModule2.IsModuleSupported())
					{
						ChangeEventModule(baseInputModule2);
						flag = true;
						break;
					}
				}
			}
			if (!flag && m_CurrentInputModule != null)
			{
				m_CurrentInputModule.Process();
			}
		}

		private void ChangeEventModule(global::UnityEngine.EventSystems.BaseInputModule module)
		{
			if (!(m_CurrentInputModule == module))
			{
				if (m_CurrentInputModule != null)
				{
					m_CurrentInputModule.DeactivateModule();
				}
				if (module != null)
				{
					module.ActivateModule();
				}
				m_CurrentInputModule = module;
			}
		}

		public override string ToString()
		{
			global::System.Text.StringBuilder stringBuilder = new global::System.Text.StringBuilder();
			stringBuilder.AppendLine("<b>Selected:</b>" + currentSelectedGameObject);
			stringBuilder.AppendLine();
			stringBuilder.AppendLine();
			stringBuilder.AppendLine((m_CurrentInputModule != null) ? m_CurrentInputModule.ToString() : "No module");
			return stringBuilder.ToString();
		}
	}
}
