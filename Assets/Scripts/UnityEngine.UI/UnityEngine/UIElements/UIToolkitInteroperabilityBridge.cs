namespace UnityEngine.UIElements
{
	internal class UIToolkitInteroperabilityBridge
	{
		[global::System.Flags]
		public enum EventHandlerTypes
		{
			ScreenOverlay = 1,
			WorldSpace = 2
		}

		private global::UnityEngine.EventSystems.EventSystem m_EventSystem;

		private bool m_OverrideUIToolkitEvents = true;

		private global::UnityEngine.UIElements.UIToolkitInteroperabilityBridge.EventHandlerTypes m_HandlerTypes = global::UnityEngine.UIElements.UIToolkitInteroperabilityBridge.EventHandlerTypes.ScreenOverlay | global::UnityEngine.UIElements.UIToolkitInteroperabilityBridge.EventHandlerTypes.WorldSpace;

		private global::UnityEngine.LayerMask m_WorldPickingLayers = -5;

		private float m_WorldPickingMaxDistance = float.PositiveInfinity;

		private bool m_CreateDefaultPanelComponents = true;

		private bool m_Started;

		private bool m_Enabled;

		private bool m_IsTrackingPanels;

		private global::UnityEngine.GameObject m_WorldSpaceGo;

		private readonly global::System.Collections.Generic.HashSet<global::UnityEngine.UIElements.BaseRuntimePanel> trackedPanels = new global::System.Collections.Generic.HashSet<global::UnityEngine.UIElements.BaseRuntimePanel>();

		private readonly global::System.Collections.Generic.Dictionary<global::UnityEngine.UIElements.BaseRuntimePanel, global::System.Action> destroyedActions = new global::System.Collections.Generic.Dictionary<global::UnityEngine.UIElements.BaseRuntimePanel, global::System.Action>();

		private global::UnityEngine.UIElements.PanelInputConfiguration.Settings m_InputSettings = global::UnityEngine.UIElements.PanelInputConfiguration.Settings.Default;

		private bool m_OldOverrideUIToolkitEvents = true;

		private global::UnityEngine.UIElements.UIToolkitInteroperabilityBridge.EventHandlerTypes m_OldHandlerTypes = global::UnityEngine.UIElements.UIToolkitInteroperabilityBridge.EventHandlerTypes.ScreenOverlay | global::UnityEngine.UIElements.UIToolkitInteroperabilityBridge.EventHandlerTypes.WorldSpace;

		private bool m_OldCreateDefaultPanelComponents = true;

		private bool m_OldDefaultEventCameraIsMainCamera = true;

		private long m_OldEventCamerasHash;

		private global::System.Collections.Generic.List<global::UnityEngine.UIElements.BaseRuntimePanel> m_PanelsToRemove = new global::System.Collections.Generic.List<global::UnityEngine.UIElements.BaseRuntimePanel>();

		internal global::UnityEngine.EventSystems.EventSystem eventSystem
		{
			get
			{
				return m_EventSystem;
			}
			set
			{
				if (!(m_EventSystem == value))
				{
					m_EventSystem = value;
				}
			}
		}

		public bool overrideUIToolkitEvents
		{
			get
			{
				return m_OverrideUIToolkitEvents;
			}
			internal set
			{
				m_OverrideUIToolkitEvents = value;
				ApplyOverrideUIToolkitEvents();
			}
		}

		public global::UnityEngine.UIElements.UIToolkitInteroperabilityBridge.EventHandlerTypes handlerTypes
		{
			get
			{
				return m_HandlerTypes;
			}
			internal set
			{
				m_HandlerTypes = value;
				ApplyOtherProperties();
			}
		}

		public int worldPickingLayers
		{
			get
			{
				return m_WorldPickingLayers;
			}
			internal set
			{
				m_WorldPickingLayers = value;
			}
		}

		public float worldPickingMaxDistance
		{
			get
			{
				return m_WorldPickingMaxDistance;
			}
			internal set
			{
				m_WorldPickingMaxDistance = value;
			}
		}

		public bool createDefaultPanelComponents
		{
			get
			{
				return m_CreateDefaultPanelComponents;
			}
			internal set
			{
				m_CreateDefaultPanelComponents = value;
				ApplyOtherProperties();
			}
		}

		private bool shouldTrackPanels
		{
			get
			{
				if (overrideUIToolkitEvents && createDefaultPanelComponents && m_Started)
				{
					return m_Enabled;
				}
				return false;
			}
		}

		private void StartTrackingUIToolkitPanels()
		{
			if (m_IsTrackingPanels || !shouldTrackPanels)
			{
				return;
			}
			foreach (global::UnityEngine.UIElements.BaseRuntimePanel sortedPlayerPanel in global::UnityEngine.UIElements.UIElementsRuntimeUtility.GetSortedPlayerPanels())
			{
				StartTrackingPanel(sortedPlayerPanel);
			}
			global::UnityEngine.UIElements.UIElementsRuntimeUtility.onCreatePanel += StartTrackingPanel;
			m_IsTrackingPanels = true;
		}

		private void StartTrackingPanel(global::UnityEngine.UIElements.BaseRuntimePanel panel)
		{
			trackedPanels.Add(panel);
		}

		private void StopTrackingUIToolkitPanels()
		{
			if (!m_IsTrackingPanels)
			{
				return;
			}
			global::UnityEngine.UIElements.UIElementsRuntimeUtility.onCreatePanel -= StartTrackingPanel;
			m_IsTrackingPanels = false;
			foreach (global::UnityEngine.UIElements.BaseRuntimePanel trackedPanel in trackedPanels)
			{
				DestroyPanelGameObject(trackedPanel);
			}
			trackedPanels.Clear();
			DestroyWorldSpacePanelGameObject();
		}

		private void UpdatePanelGameObject(global::UnityEngine.UIElements.BaseRuntimePanel panel)
		{
			global::UnityEngine.UIElements.UIToolkitInteroperabilityBridge.EventHandlerTypes eventHandlerTypes = (panel.isFlat ? global::UnityEngine.UIElements.UIToolkitInteroperabilityBridge.EventHandlerTypes.ScreenOverlay : global::UnityEngine.UIElements.UIToolkitInteroperabilityBridge.EventHandlerTypes.WorldSpace);
			if ((m_HandlerTypes & eventHandlerTypes) != 0)
			{
				CreatePanelGameObject(panel);
			}
			else
			{
				DestroyPanelGameObject(panel);
			}
		}

		private void CreatePanelGameObject(global::UnityEngine.UIElements.BaseRuntimePanel panel)
		{
			if (panel.selectableGameObject == null)
			{
				global::UnityEngine.GameObject gameObject = new global::UnityEngine.GameObject(panel.name, typeof(global::UnityEngine.UIElements.PanelEventHandler), typeof(global::UnityEngine.UIElements.PanelRaycaster));
				gameObject.transform.SetParent(m_EventSystem.transform);
				panel.selectableGameObject = gameObject;
				global::System.Action action = (destroyedActions[panel] = delegate
				{
					DestroyPanelGameObject(panel);
				});
				global::System.Action value = action;
				panel.destroyed += value;
			}
		}

		private void DestroyPanelGameObject(global::UnityEngine.UIElements.BaseRuntimePanel panel)
		{
			global::UnityEngine.GameObject selectableGameObject = panel.selectableGameObject;
			if (selectableGameObject != null && destroyedActions.Remove(panel, out var value))
			{
				panel.destroyed -= value;
				panel.selectableGameObject = null;
				global::UnityEngine.UIElements.UIRUtility.Destroy(selectableGameObject);
			}
		}

		private void CreateWorldSpacePanelGameObject()
		{
			ApplyCameraProperties();
			if (!(m_WorldSpaceGo == null))
			{
				return;
			}
			global::UnityEngine.GameObject gameObject = new global::UnityEngine.GameObject("WorldDocumentRaycaster");
			gameObject.transform.SetParent(m_EventSystem.transform);
			if (m_InputSettings.defaultEventCameraIsMainCamera)
			{
				gameObject.AddComponent<global::UnityEngine.UIElements.WorldDocumentRaycaster>();
			}
			else
			{
				global::UnityEngine.Camera[] eventCameras = m_InputSettings.eventCameras;
				foreach (global::UnityEngine.Camera camera in eventCameras)
				{
					gameObject.AddComponent<global::UnityEngine.UIElements.WorldDocumentRaycaster>().camera = camera;
				}
			}
			m_WorldSpaceGo = gameObject;
		}

		private void DestroyWorldSpacePanelGameObject()
		{
			global::UnityEngine.GameObject worldSpaceGo = m_WorldSpaceGo;
			m_WorldSpaceGo = null;
			global::UnityEngine.UIElements.UIRUtility.Destroy(worldSpaceGo);
		}

		public void Start()
		{
			m_Started = true;
			StartTrackingUIToolkitPanels();
		}

		public void OnEnable()
		{
			if (!m_Enabled)
			{
				m_Enabled = true;
				if (global::UnityEngine.UIElements.PanelInputConfiguration.current != null)
				{
					Apply(global::UnityEngine.UIElements.PanelInputConfiguration.current);
				}
				global::UnityEngine.UIElements.PanelInputConfiguration.onApply = (global::System.Action<global::UnityEngine.UIElements.PanelInputConfiguration>)global::System.Delegate.Combine(global::UnityEngine.UIElements.PanelInputConfiguration.onApply, new global::System.Action<global::UnityEngine.UIElements.PanelInputConfiguration>(Apply));
				if (m_Started)
				{
					StartTrackingUIToolkitPanels();
				}
				if (m_OverrideUIToolkitEvents)
				{
					global::UnityEngine.UIElements.UIElementsRuntimeUtility.RegisterEventSystem(m_EventSystem);
				}
			}
		}

		public void OnDisable()
		{
			if (m_Enabled)
			{
				m_Enabled = false;
				global::UnityEngine.UIElements.PanelInputConfiguration.onApply = (global::System.Action<global::UnityEngine.UIElements.PanelInputConfiguration>)global::System.Delegate.Remove(global::UnityEngine.UIElements.PanelInputConfiguration.onApply, new global::System.Action<global::UnityEngine.UIElements.PanelInputConfiguration>(Apply));
				StopTrackingUIToolkitPanels();
				global::UnityEngine.UIElements.UIElementsRuntimeUtility.UnregisterEventSystem(m_EventSystem);
			}
		}

		public void Update()
		{
			UpdatePanelGameObjects();
		}

		private void Apply(global::UnityEngine.UIElements.PanelInputConfiguration input)
		{
			m_InputSettings = ((input != null) ? input.settings : global::UnityEngine.UIElements.PanelInputConfiguration.Settings.Default);
			m_OverrideUIToolkitEvents = m_InputSettings.panelInputRedirection != global::UnityEngine.UIElements.PanelInputConfiguration.PanelInputRedirection.Never;
			m_HandlerTypes = (global::UnityEngine.UIElements.UIToolkitInteroperabilityBridge.EventHandlerTypes)(1 | (m_InputSettings.processWorldSpaceInput ? 2 : 0));
			m_WorldPickingLayers = m_InputSettings.interactionLayers;
			m_WorldPickingMaxDistance = m_InputSettings.maxInteractionDistance;
			m_CreateDefaultPanelComponents = m_InputSettings.autoCreatePanelComponents;
			ApplyOverrideUIToolkitEvents();
			ApplyCameraProperties();
			ApplyOtherProperties();
		}

		private void ApplyOverrideUIToolkitEvents()
		{
			if (m_OldOverrideUIToolkitEvents == m_OverrideUIToolkitEvents)
			{
				return;
			}
			m_OldOverrideUIToolkitEvents = m_OverrideUIToolkitEvents;
			if (m_Enabled)
			{
				if (m_OverrideUIToolkitEvents)
				{
					global::UnityEngine.UIElements.UIElementsRuntimeUtility.RegisterEventSystem(m_EventSystem);
				}
				else
				{
					global::UnityEngine.UIElements.UIElementsRuntimeUtility.UnregisterEventSystem(m_EventSystem);
				}
				UpdatePanelTracking();
			}
		}

		private void ApplyCameraProperties()
		{
			bool flag = false;
			if (m_OldDefaultEventCameraIsMainCamera != m_InputSettings.defaultEventCameraIsMainCamera)
			{
				m_OldDefaultEventCameraIsMainCamera = m_InputSettings.defaultEventCameraIsMainCamera;
				flag = true;
			}
			if (!m_InputSettings.defaultEventCameraIsMainCamera)
			{
				int num = 0;
				global::UnityEngine.Camera[] eventCameras = m_InputSettings.eventCameras;
				foreach (global::UnityEngine.Camera camera in eventCameras)
				{
					num = (num * 397) ^ camera.GetHashCode();
				}
				if (m_OldEventCamerasHash != num)
				{
					m_OldEventCamerasHash = num;
					flag = true;
				}
			}
			else
			{
				m_OldEventCamerasHash = 0L;
			}
			if (flag)
			{
				DestroyWorldSpacePanelGameObject();
			}
		}

		private void ApplyOtherProperties()
		{
			bool flag = false;
			if (m_OldHandlerTypes != m_HandlerTypes)
			{
				m_OldHandlerTypes = m_HandlerTypes;
				flag = true;
			}
			if (m_OldCreateDefaultPanelComponents != m_CreateDefaultPanelComponents)
			{
				m_OldCreateDefaultPanelComponents = m_CreateDefaultPanelComponents;
				flag = true;
			}
			if (flag)
			{
				UpdatePanelTracking();
			}
		}

		private void UpdatePanelTracking()
		{
			if (shouldTrackPanels)
			{
				StartTrackingUIToolkitPanels();
			}
			else
			{
				StopTrackingUIToolkitPanels();
			}
		}

		private void UpdatePanelGameObjects()
		{
			if (!m_IsTrackingPanels)
			{
				return;
			}
			bool flag = false;
			foreach (global::UnityEngine.UIElements.BaseRuntimePanel trackedPanel in trackedPanels)
			{
				if (trackedPanel.disposed)
				{
					m_PanelsToRemove.Add(trackedPanel);
					continue;
				}
				UpdatePanelGameObject(trackedPanel);
				flag |= !trackedPanel.isFlat;
			}
			foreach (global::UnityEngine.UIElements.BaseRuntimePanel item in m_PanelsToRemove)
			{
				trackedPanels.Remove(item);
			}
			m_PanelsToRemove.Clear();
			if (flag && (m_HandlerTypes & global::UnityEngine.UIElements.UIToolkitInteroperabilityBridge.EventHandlerTypes.WorldSpace) != 0)
			{
				CreateWorldSpacePanelGameObject();
			}
			else
			{
				DestroyWorldSpacePanelGameObject();
			}
		}
	}
}
