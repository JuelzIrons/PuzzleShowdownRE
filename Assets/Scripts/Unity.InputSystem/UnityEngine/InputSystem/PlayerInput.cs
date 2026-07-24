namespace UnityEngine.InputSystem
{
	[global::UnityEngine.AddComponentMenu("Input/Player Input")]
	[global::UnityEngine.DisallowMultipleComponent]
	[global::UnityEngine.HelpURL("https://docs.unity3d.com/Packages/com.unity.inputsystem@1.18/manual/PlayerInput.html")]
	public class PlayerInput : global::UnityEngine.MonoBehaviour
	{
		[global::System.Serializable]
		public class ActionEvent : global::UnityEngine.Events.UnityEvent<global::UnityEngine.InputSystem.InputAction.CallbackContext>
		{
			[global::UnityEngine.SerializeField]
			private string m_ActionId;

			[global::UnityEngine.SerializeField]
			private string m_ActionName;

			public string actionId => m_ActionId;

			public string actionName => m_ActionName;

			public ActionEvent()
			{
			}

			public ActionEvent(global::UnityEngine.InputSystem.InputAction action)
			{
				if (action == null)
				{
					throw new global::System.ArgumentNullException("action");
				}
				if (action.isSingletonAction)
				{
					throw new global::System.ArgumentException($"Action must be part of an asset (given action '{action}' is a singleton)");
				}
				if (action.actionMap.asset == null)
				{
					throw new global::System.ArgumentException($"Action must be part of an asset (given action '{action}' is not)");
				}
				m_ActionId = action.id.ToString();
				m_ActionName = action.actionMap.name + "/" + action.name;
			}

			public ActionEvent(global::System.Guid actionGUID, string name = null)
			{
				m_ActionId = actionGUID.ToString();
				m_ActionName = name;
			}
		}

		[global::System.Serializable]
		public class DeviceLostEvent : global::UnityEngine.Events.UnityEvent<global::UnityEngine.InputSystem.PlayerInput>
		{
		}

		[global::System.Serializable]
		public class DeviceRegainedEvent : global::UnityEngine.Events.UnityEvent<global::UnityEngine.InputSystem.PlayerInput>
		{
		}

		[global::System.Serializable]
		public class ControlsChangedEvent : global::UnityEngine.Events.UnityEvent<global::UnityEngine.InputSystem.PlayerInput>
		{
		}

		public const string DeviceLostMessage = "OnDeviceLost";

		public const string DeviceRegainedMessage = "OnDeviceRegained";

		public const string ControlsChangedMessage = "OnControlsChanged";

		private int m_AllMapsHashCode;

		[global::UnityEngine.Tooltip("Input actions associated with the player.")]
		[global::UnityEngine.SerializeField]
		internal global::UnityEngine.InputSystem.InputActionAsset m_Actions;

		[global::UnityEngine.Tooltip("Determine how notifications should be sent when an input-related event associated with the player happens.")]
		[global::UnityEngine.SerializeField]
		internal global::UnityEngine.InputSystem.PlayerNotifications m_NotificationBehavior;

		[global::UnityEngine.Tooltip("UI InputModule that should have it's input actions synchronized to this PlayerInput's actions.")]
		[global::UnityEngine.SerializeField]
		internal global::UnityEngine.InputSystem.UI.InputSystemUIInputModule m_UIInputModule;

		[global::UnityEngine.Tooltip("Event that is triggered when the PlayerInput loses a paired device (e.g. its battery runs out).")]
		[global::UnityEngine.SerializeField]
		internal global::UnityEngine.InputSystem.PlayerInput.DeviceLostEvent m_DeviceLostEvent;

		[global::UnityEngine.SerializeField]
		internal global::UnityEngine.InputSystem.PlayerInput.DeviceRegainedEvent m_DeviceRegainedEvent;

		[global::UnityEngine.SerializeField]
		internal global::UnityEngine.InputSystem.PlayerInput.ControlsChangedEvent m_ControlsChangedEvent;

		[global::UnityEngine.SerializeField]
		internal global::UnityEngine.InputSystem.PlayerInput.ActionEvent[] m_ActionEvents;

		[global::UnityEngine.SerializeField]
		internal bool m_NeverAutoSwitchControlSchemes;

		[global::UnityEngine.SerializeField]
		internal string m_DefaultControlScheme;

		[global::UnityEngine.SerializeField]
		internal string m_DefaultActionMap;

		[global::UnityEngine.SerializeField]
		internal int m_SplitScreenIndex = -1;

		[global::UnityEngine.Tooltip("Reference to the player's view camera. Note that this is only required when using split-screen and/or per-player UIs. Otherwise it is safe to leave this property uninitialized.")]
		[global::UnityEngine.SerializeField]
		internal global::UnityEngine.Camera m_Camera;

		[global::System.NonSerialized]
		private global::UnityEngine.InputSystem.InputValue m_InputValueObject;

		[global::System.NonSerialized]
		internal global::UnityEngine.InputSystem.InputActionMap m_CurrentActionMap;

		[global::System.NonSerialized]
		private int m_PlayerIndex = -1;

		[global::System.NonSerialized]
		private bool m_InputActive;

		[global::System.NonSerialized]
		private bool m_Enabled;

		[global::System.NonSerialized]
		internal bool m_ActionsInitialized;

		[global::System.NonSerialized]
		private global::System.Collections.Generic.Dictionary<string, string> m_ActionMessageNames;

		[global::System.NonSerialized]
		private global::UnityEngine.InputSystem.Users.InputUser m_InputUser;

		[global::System.NonSerialized]
		private global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext> m_ActionTriggeredDelegate;

		[global::System.NonSerialized]
		private global::UnityEngine.InputSystem.Utilities.CallbackArray<global::System.Action<global::UnityEngine.InputSystem.PlayerInput>> m_DeviceLostCallbacks;

		[global::System.NonSerialized]
		private global::UnityEngine.InputSystem.Utilities.CallbackArray<global::System.Action<global::UnityEngine.InputSystem.PlayerInput>> m_DeviceRegainedCallbacks;

		[global::System.NonSerialized]
		private global::UnityEngine.InputSystem.Utilities.CallbackArray<global::System.Action<global::UnityEngine.InputSystem.PlayerInput>> m_ControlsChangedCallbacks;

		[global::System.NonSerialized]
		private global::UnityEngine.InputSystem.Utilities.CallbackArray<global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>> m_ActionTriggeredCallbacks;

		[global::System.NonSerialized]
		private global::System.Action<global::UnityEngine.InputSystem.InputControl, global::UnityEngine.InputSystem.LowLevel.InputEventPtr> m_UnpairedDeviceUsedDelegate;

		[global::System.NonSerialized]
		private global::System.Func<global::UnityEngine.InputSystem.InputDevice, global::UnityEngine.InputSystem.LowLevel.InputEventPtr, bool> m_PreFilterUnpairedDeviceUsedDelegate;

		[global::System.NonSerialized]
		private bool m_OnUnpairedDeviceUsedHooked;

		[global::System.NonSerialized]
		private global::System.Action<global::UnityEngine.InputSystem.InputDevice, global::UnityEngine.InputSystem.InputDeviceChange> m_DeviceChangeDelegate;

		[global::System.NonSerialized]
		private bool m_OnDeviceChangeHooked;

		internal static int s_AllActivePlayersCount;

		internal static global::UnityEngine.InputSystem.PlayerInput[] s_AllActivePlayers;

		private static global::System.Action<global::UnityEngine.InputSystem.Users.InputUser, global::UnityEngine.InputSystem.Users.InputUserChange, global::UnityEngine.InputSystem.InputDevice> s_UserChangeDelegate;

		private static int s_InitPairWithDevicesCount;

		private static global::UnityEngine.InputSystem.InputDevice[] s_InitPairWithDevices;

		private static int s_InitPlayerIndex = -1;

		private static int s_InitSplitScreenIndex = -1;

		private static string s_InitControlScheme;

		internal static bool s_DestroyIfDeviceSetupUnsuccessful;

		public bool inputIsActive => m_InputActive;

		[global::System.Obsolete("Use inputIsActive instead.")]
		public bool active => inputIsActive;

		public int playerIndex => m_PlayerIndex;

		public int splitScreenIndex => m_SplitScreenIndex;

		public global::UnityEngine.InputSystem.InputActionAsset actions
		{
			get
			{
				if (!m_ActionsInitialized && base.gameObject.activeInHierarchy)
				{
					InitializeActions();
				}
				return m_Actions;
			}
			set
			{
				if (m_Actions == value)
				{
					return;
				}
				if (m_Actions != null)
				{
					m_Actions.Disable();
					if (m_ActionsInitialized)
					{
						UninitializeActions();
					}
				}
				m_Actions = value;
				if (m_Enabled)
				{
					ClearCaches();
					AssignUserAndDevices();
					InitializeActions();
					if (m_InputActive)
					{
						ActivateInput();
					}
				}
			}
		}

		public string currentControlScheme
		{
			get
			{
				if (!m_InputUser.valid)
				{
					return null;
				}
				return m_InputUser.controlScheme?.name;
			}
		}

		public string defaultControlScheme
		{
			get
			{
				return m_DefaultControlScheme;
			}
			set
			{
				m_DefaultControlScheme = value;
			}
		}

		public bool neverAutoSwitchControlSchemes
		{
			get
			{
				return m_NeverAutoSwitchControlSchemes;
			}
			set
			{
				if (m_NeverAutoSwitchControlSchemes == value)
				{
					return;
				}
				m_NeverAutoSwitchControlSchemes = value;
				if (m_Enabled)
				{
					if (!value && !m_OnUnpairedDeviceUsedHooked)
					{
						StartListeningForUnpairedDeviceActivity();
					}
					else if (value && m_OnUnpairedDeviceUsedHooked)
					{
						StopListeningForUnpairedDeviceActivity();
					}
				}
			}
		}

		public global::UnityEngine.InputSystem.InputActionMap currentActionMap
		{
			get
			{
				return m_CurrentActionMap;
			}
			set
			{
				global::UnityEngine.InputSystem.InputActionMap inputActionMap = m_CurrentActionMap;
				m_CurrentActionMap = null;
				inputActionMap?.Disable();
				m_CurrentActionMap = value;
				m_CurrentActionMap?.Enable();
			}
		}

		public string defaultActionMap
		{
			get
			{
				return m_DefaultActionMap;
			}
			set
			{
				m_DefaultActionMap = value;
			}
		}

		public global::UnityEngine.InputSystem.PlayerNotifications notificationBehavior
		{
			get
			{
				return m_NotificationBehavior;
			}
			set
			{
				if (m_NotificationBehavior != value)
				{
					if (m_Enabled)
					{
						UninitializeActions();
					}
					m_NotificationBehavior = value;
					if (m_Enabled)
					{
						InitializeActions();
					}
				}
			}
		}

		public global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.PlayerInput.ActionEvent> actionEvents
		{
			get
			{
				return m_ActionEvents;
			}
			set
			{
				if (m_Enabled)
				{
					UninitializeActions();
				}
				m_ActionEvents = value.ToArray();
				if (m_Enabled)
				{
					InitializeActions();
				}
			}
		}

		public global::UnityEngine.InputSystem.PlayerInput.DeviceLostEvent deviceLostEvent
		{
			get
			{
				if (m_DeviceLostEvent == null)
				{
					m_DeviceLostEvent = new global::UnityEngine.InputSystem.PlayerInput.DeviceLostEvent();
				}
				return m_DeviceLostEvent;
			}
		}

		public global::UnityEngine.InputSystem.PlayerInput.DeviceRegainedEvent deviceRegainedEvent
		{
			get
			{
				if (m_DeviceRegainedEvent == null)
				{
					m_DeviceRegainedEvent = new global::UnityEngine.InputSystem.PlayerInput.DeviceRegainedEvent();
				}
				return m_DeviceRegainedEvent;
			}
		}

		public global::UnityEngine.InputSystem.PlayerInput.ControlsChangedEvent controlsChangedEvent
		{
			get
			{
				if (m_ControlsChangedEvent == null)
				{
					m_ControlsChangedEvent = new global::UnityEngine.InputSystem.PlayerInput.ControlsChangedEvent();
				}
				return m_ControlsChangedEvent;
			}
		}

		public global::UnityEngine.Camera camera
		{
			get
			{
				return m_Camera;
			}
			set
			{
				m_Camera = value;
			}
		}

		public global::UnityEngine.InputSystem.UI.InputSystemUIInputModule uiInputModule
		{
			get
			{
				return m_UIInputModule;
			}
			set
			{
				if (!(m_UIInputModule == value))
				{
					if (m_UIInputModule != null && m_UIInputModule.actionsAsset == m_Actions)
					{
						m_UIInputModule.actionsAsset = null;
					}
					m_UIInputModule = value;
					if (m_UIInputModule != null && m_Actions != null)
					{
						m_UIInputModule.actionsAsset = m_Actions;
					}
				}
			}
		}

		public global::UnityEngine.InputSystem.Users.InputUser user => m_InputUser;

		public global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.InputDevice> devices
		{
			get
			{
				if (!m_InputUser.valid)
				{
					return default(global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.InputDevice>);
				}
				return m_InputUser.pairedDevices;
			}
		}

		public bool hasMissingRequiredDevices
		{
			get
			{
				if (user.valid)
				{
					return user.hasMissingRequiredDevices;
				}
				return false;
			}
		}

		public static global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.PlayerInput> all => new global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.PlayerInput>(s_AllActivePlayers, 0, s_AllActivePlayersCount);

		public static bool isSinglePlayer
		{
			get
			{
				if (s_AllActivePlayersCount <= 1)
				{
					if (!(global::UnityEngine.InputSystem.PlayerInputManager.instance == null))
					{
						return !global::UnityEngine.InputSystem.PlayerInputManager.instance.joiningEnabled;
					}
					return true;
				}
				return false;
			}
		}

		public event global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext> onActionTriggered
		{
			add
			{
				if (value == null)
				{
					throw new global::System.ArgumentNullException("value");
				}
				m_ActionTriggeredCallbacks.AddCallback(value);
			}
			remove
			{
				if (value == null)
				{
					throw new global::System.ArgumentNullException("value");
				}
				m_ActionTriggeredCallbacks.RemoveCallback(value);
			}
		}

		public event global::System.Action<global::UnityEngine.InputSystem.PlayerInput> onDeviceLost
		{
			add
			{
				if (value == null)
				{
					throw new global::System.ArgumentNullException("value");
				}
				m_DeviceLostCallbacks.AddCallback(value);
			}
			remove
			{
				if (value == null)
				{
					throw new global::System.ArgumentNullException("value");
				}
				m_DeviceLostCallbacks.RemoveCallback(value);
			}
		}

		public event global::System.Action<global::UnityEngine.InputSystem.PlayerInput> onDeviceRegained
		{
			add
			{
				if (value == null)
				{
					throw new global::System.ArgumentNullException("value");
				}
				m_DeviceRegainedCallbacks.AddCallback(value);
			}
			remove
			{
				if (value == null)
				{
					throw new global::System.ArgumentNullException("value");
				}
				m_DeviceRegainedCallbacks.RemoveCallback(value);
			}
		}

		public event global::System.Action<global::UnityEngine.InputSystem.PlayerInput> onControlsChanged
		{
			add
			{
				if (value == null)
				{
					throw new global::System.ArgumentNullException("value");
				}
				m_ControlsChangedCallbacks.AddCallback(value);
			}
			remove
			{
				if (value == null)
				{
					throw new global::System.ArgumentNullException("value");
				}
				m_ControlsChangedCallbacks.RemoveCallback(value);
			}
		}

		public TDevice GetDevice<TDevice>() where TDevice : global::UnityEngine.InputSystem.InputDevice
		{
			foreach (global::UnityEngine.InputSystem.InputDevice device in devices)
			{
				if (device is TDevice result)
				{
					return result;
				}
			}
			return null;
		}

		public void ActivateInput()
		{
			UpdateDelegates();
			m_InputActive = true;
			if (m_CurrentActionMap == null && m_Actions != null && !string.IsNullOrEmpty(m_DefaultActionMap))
			{
				SwitchCurrentActionMap(m_DefaultActionMap);
			}
			else
			{
				m_CurrentActionMap?.Enable();
			}
		}

		private void UpdateDelegates()
		{
			if (m_Actions == null)
			{
				m_AllMapsHashCode = 0;
				return;
			}
			int num = 0;
			foreach (global::UnityEngine.InputSystem.InputActionMap actionMap in m_Actions.actionMaps)
			{
				num ^= actionMap.GetHashCode();
			}
			if (m_AllMapsHashCode != num)
			{
				if (m_NotificationBehavior != global::UnityEngine.InputSystem.PlayerNotifications.InvokeUnityEvents)
				{
					InstallOnActionTriggeredHook();
				}
				CacheMessageNames();
				m_AllMapsHashCode = num;
			}
		}

		public void DeactivateInput()
		{
			m_CurrentActionMap?.Disable();
			m_InputActive = false;
		}

		[global::System.Obsolete("Use DeactivateInput instead.")]
		public void PassivateInput()
		{
			DeactivateInput();
		}

		public bool SwitchCurrentControlScheme(params global::UnityEngine.InputSystem.InputDevice[] devices)
		{
			if (devices == null)
			{
				throw new global::System.ArgumentNullException("devices");
			}
			if (actions == null)
			{
				throw new global::System.InvalidOperationException("Must set actions on PlayerInput in order to be able to switch control schemes");
			}
			global::UnityEngine.InputSystem.InputControlScheme? inputControlScheme = global::UnityEngine.InputSystem.InputControlScheme.FindControlSchemeForDevices(devices, actions.controlSchemes);
			if (!inputControlScheme.HasValue)
			{
				return false;
			}
			global::UnityEngine.InputSystem.InputControlScheme controlScheme = inputControlScheme.Value;
			SwitchControlSchemeInternal(ref controlScheme, devices);
			return true;
		}

		public void SwitchCurrentControlScheme(string controlScheme, params global::UnityEngine.InputSystem.InputDevice[] devices)
		{
			if (string.IsNullOrEmpty(controlScheme))
			{
				throw new global::System.ArgumentNullException("controlScheme");
			}
			if (devices == null)
			{
				throw new global::System.ArgumentNullException("devices");
			}
			user.FindControlScheme(controlScheme, out var scheme);
			SwitchControlSchemeInternal(ref scheme, devices);
		}

		public void SwitchCurrentActionMap(string mapNameOrId)
		{
			if (!m_Enabled)
			{
				global::UnityEngine.Debug.LogError("Cannot switch to actions '" + mapNameOrId + "'; input is not enabled", this);
				return;
			}
			if (m_Actions == null)
			{
				global::UnityEngine.Debug.LogError("Cannot switch to actions '" + mapNameOrId + "'; no actions set on PlayerInput", this);
				return;
			}
			global::UnityEngine.InputSystem.InputActionMap inputActionMap = m_Actions.FindActionMap(mapNameOrId);
			if (inputActionMap == null)
			{
				global::UnityEngine.Debug.LogError($"Cannot find action map '{mapNameOrId}' in actions '{m_Actions}'", this);
			}
			else
			{
				currentActionMap = inputActionMap;
			}
		}

		public static global::UnityEngine.InputSystem.PlayerInput GetPlayerByIndex(int playerIndex)
		{
			for (int i = 0; i < s_AllActivePlayersCount; i++)
			{
				if (s_AllActivePlayers[i].playerIndex == playerIndex)
				{
					return s_AllActivePlayers[i];
				}
			}
			return null;
		}

		public static global::UnityEngine.InputSystem.PlayerInput FindFirstPairedToDevice(global::UnityEngine.InputSystem.InputDevice device)
		{
			if (device == null)
			{
				throw new global::System.ArgumentNullException("device");
			}
			for (int i = 0; i < s_AllActivePlayersCount; i++)
			{
				if (global::UnityEngine.InputSystem.Utilities.ReadOnlyArrayExtensions.ContainsReference(s_AllActivePlayers[i].devices, device))
				{
					return s_AllActivePlayers[i];
				}
			}
			return null;
		}

		public static global::UnityEngine.InputSystem.PlayerInput Instantiate(global::UnityEngine.GameObject prefab, int playerIndex = -1, string controlScheme = null, int splitScreenIndex = -1, global::UnityEngine.InputSystem.InputDevice pairWithDevice = null)
		{
			if (prefab == null)
			{
				throw new global::System.ArgumentNullException("prefab");
			}
			s_InitPlayerIndex = playerIndex;
			s_InitSplitScreenIndex = splitScreenIndex;
			s_InitControlScheme = controlScheme;
			if (pairWithDevice != null)
			{
				global::UnityEngine.InputSystem.Utilities.ArrayHelpers.AppendWithCapacity(ref s_InitPairWithDevices, ref s_InitPairWithDevicesCount, pairWithDevice);
			}
			return DoInstantiate(prefab);
		}

		public static global::UnityEngine.InputSystem.PlayerInput Instantiate(global::UnityEngine.GameObject prefab, int playerIndex = -1, string controlScheme = null, int splitScreenIndex = -1, params global::UnityEngine.InputSystem.InputDevice[] pairWithDevices)
		{
			if (prefab == null)
			{
				throw new global::System.ArgumentNullException("prefab");
			}
			s_InitPlayerIndex = playerIndex;
			s_InitSplitScreenIndex = splitScreenIndex;
			s_InitControlScheme = controlScheme;
			if (pairWithDevices != null)
			{
				for (int i = 0; i < pairWithDevices.Length; i++)
				{
					global::UnityEngine.InputSystem.Utilities.ArrayHelpers.AppendWithCapacity(ref s_InitPairWithDevices, ref s_InitPairWithDevicesCount, pairWithDevices[i]);
				}
			}
			return DoInstantiate(prefab);
		}

		private static global::UnityEngine.InputSystem.PlayerInput DoInstantiate(global::UnityEngine.GameObject prefab)
		{
			bool flag = s_DestroyIfDeviceSetupUnsuccessful;
			global::UnityEngine.GameObject gameObject;
			try
			{
				gameObject = global::UnityEngine.Object.Instantiate(prefab);
				gameObject.SetActive(value: true);
			}
			finally
			{
				s_InitPairWithDevicesCount = 0;
				if (s_InitPairWithDevices != null)
				{
					global::System.Array.Clear(s_InitPairWithDevices, 0, s_InitPairWithDevicesCount);
				}
				s_InitControlScheme = null;
				s_InitPlayerIndex = -1;
				s_InitSplitScreenIndex = -1;
				s_DestroyIfDeviceSetupUnsuccessful = false;
			}
			global::UnityEngine.InputSystem.PlayerInput componentInChildren = gameObject.GetComponentInChildren<global::UnityEngine.InputSystem.PlayerInput>();
			if (componentInChildren == null)
			{
				global::UnityEngine.Object.DestroyImmediate(gameObject);
				global::UnityEngine.Debug.LogError("The GameObject does not have a PlayerInput component", prefab);
				return null;
			}
			if (flag && (!componentInChildren.user.valid || componentInChildren.hasMissingRequiredDevices))
			{
				global::UnityEngine.Object.DestroyImmediate(gameObject);
				return null;
			}
			return componentInChildren;
		}

		private void InitializeActions()
		{
			if (m_ActionsInitialized || m_Actions == null)
			{
				return;
			}
			for (int i = 0; i < s_AllActivePlayersCount; i++)
			{
				if (s_AllActivePlayers[i].m_Actions == m_Actions && s_AllActivePlayers[i] != this)
				{
					CopyActionAssetAndApplyBindingOverrides();
					break;
				}
			}
			if (uiInputModule != null)
			{
				uiInputModule.actionsAsset = m_Actions;
			}
			switch (m_NotificationBehavior)
			{
			case global::UnityEngine.InputSystem.PlayerNotifications.SendMessages:
			case global::UnityEngine.InputSystem.PlayerNotifications.BroadcastMessages:
				InstallOnActionTriggeredHook();
				if (m_ActionMessageNames == null)
				{
					CacheMessageNames();
				}
				break;
			case global::UnityEngine.InputSystem.PlayerNotifications.InvokeCSharpEvents:
				InstallOnActionTriggeredHook();
				break;
			case global::UnityEngine.InputSystem.PlayerNotifications.InvokeUnityEvents:
			{
				if (m_ActionEvents == null)
				{
					break;
				}
				global::UnityEngine.InputSystem.PlayerInput.ActionEvent[] array = m_ActionEvents;
				foreach (global::UnityEngine.InputSystem.PlayerInput.ActionEvent actionEvent in array)
				{
					string actionId = actionEvent.actionId;
					if (!string.IsNullOrEmpty(actionId))
					{
						global::UnityEngine.InputSystem.InputAction inputAction = m_Actions.FindAction(actionId);
						if (inputAction != null)
						{
							inputAction.performed += actionEvent.Invoke;
							inputAction.canceled += actionEvent.Invoke;
							inputAction.started += actionEvent.Invoke;
						}
					}
				}
				break;
			}
			}
			m_ActionsInitialized = true;
		}

		private void CopyActionAssetAndApplyBindingOverrides()
		{
			global::UnityEngine.InputSystem.InputActionAsset inputActionAsset = m_Actions;
			m_Actions = global::UnityEngine.Object.Instantiate(m_Actions);
			for (int i = 0; i < inputActionAsset.actionMaps.Count; i++)
			{
				for (int j = 0; j < inputActionAsset.actionMaps[i].bindings.Count; j++)
				{
					m_Actions.actionMaps[i].ApplyBindingOverride(j, inputActionAsset.actionMaps[i].bindings[j]);
				}
			}
		}

		private void UninitializeActions()
		{
			if (!m_ActionsInitialized || m_Actions == null)
			{
				return;
			}
			UninstallOnActionTriggeredHook();
			if (m_NotificationBehavior == global::UnityEngine.InputSystem.PlayerNotifications.InvokeUnityEvents && m_ActionEvents != null)
			{
				global::UnityEngine.InputSystem.PlayerInput.ActionEvent[] array = m_ActionEvents;
				foreach (global::UnityEngine.InputSystem.PlayerInput.ActionEvent actionEvent in array)
				{
					string actionId = actionEvent.actionId;
					if (!string.IsNullOrEmpty(actionId))
					{
						global::UnityEngine.InputSystem.InputAction inputAction = m_Actions.FindAction(actionId);
						if (inputAction != null)
						{
							inputAction.performed -= actionEvent.Invoke;
							inputAction.canceled -= actionEvent.Invoke;
							inputAction.started -= actionEvent.Invoke;
						}
					}
				}
			}
			m_CurrentActionMap = null;
			m_ActionsInitialized = false;
		}

		private void InstallOnActionTriggeredHook()
		{
			if (m_ActionTriggeredDelegate == null)
			{
				m_ActionTriggeredDelegate = OnActionTriggered;
			}
			foreach (global::UnityEngine.InputSystem.InputActionMap actionMap in m_Actions.actionMaps)
			{
				actionMap.actionTriggered += m_ActionTriggeredDelegate;
			}
		}

		private void UninstallOnActionTriggeredHook()
		{
			if (m_ActionTriggeredDelegate == null)
			{
				return;
			}
			foreach (global::UnityEngine.InputSystem.InputActionMap actionMap in m_Actions.actionMaps)
			{
				actionMap.actionTriggered -= m_ActionTriggeredDelegate;
			}
		}

		private void OnActionTriggered(global::UnityEngine.InputSystem.InputAction.CallbackContext context)
		{
			if (!m_InputActive)
			{
				return;
			}
			switch (m_NotificationBehavior)
			{
			case global::UnityEngine.InputSystem.PlayerNotifications.InvokeCSharpEvents:
				global::UnityEngine.InputSystem.Utilities.DelegateHelpers.InvokeCallbacksSafe(ref m_ActionTriggeredCallbacks, context, "PlayerInput.onActionTriggered");
				break;
			case global::UnityEngine.InputSystem.PlayerNotifications.SendMessages:
			case global::UnityEngine.InputSystem.PlayerNotifications.BroadcastMessages:
			{
				global::UnityEngine.InputSystem.InputAction action = context.action;
				if (context.performed || (context.canceled && action.type == global::UnityEngine.InputSystem.InputActionType.Value))
				{
					if (m_ActionMessageNames == null)
					{
						CacheMessageNames();
					}
					string methodName = m_ActionMessageNames[action.m_Id];
					if (m_InputValueObject == null)
					{
						m_InputValueObject = new global::UnityEngine.InputSystem.InputValue();
					}
					m_InputValueObject.m_Context = context;
					if (m_NotificationBehavior == global::UnityEngine.InputSystem.PlayerNotifications.BroadcastMessages)
					{
						BroadcastMessage(methodName, m_InputValueObject, global::UnityEngine.SendMessageOptions.DontRequireReceiver);
					}
					else
					{
						SendMessage(methodName, m_InputValueObject, global::UnityEngine.SendMessageOptions.DontRequireReceiver);
					}
					m_InputValueObject.m_Context = null;
				}
				break;
			}
			}
		}

		private void CacheMessageNames()
		{
			if (m_Actions == null)
			{
				return;
			}
			if (m_ActionMessageNames != null)
			{
				m_ActionMessageNames.Clear();
			}
			else
			{
				m_ActionMessageNames = new global::System.Collections.Generic.Dictionary<string, string>();
			}
			foreach (global::UnityEngine.InputSystem.InputAction action in m_Actions)
			{
				action.MakeSureIdIsInPlace();
				string text = global::UnityEngine.InputSystem.Utilities.CSharpCodeHelpers.MakeTypeName(action.name);
				m_ActionMessageNames[action.m_Id] = "On" + text;
			}
		}

		private void ClearCaches()
		{
			if (m_ActionMessageNames != null)
			{
				m_ActionMessageNames.Clear();
			}
		}

		private void AssignUserAndDevices()
		{
			if (m_InputUser.valid)
			{
				m_InputUser.UnpairDevices();
			}
			if (m_Actions == null)
			{
				if (s_InitPairWithDevicesCount > 0)
				{
					for (int i = 0; i < s_InitPairWithDevicesCount; i++)
					{
						m_InputUser = global::UnityEngine.InputSystem.Users.InputUser.PerformPairingWithDevice(s_InitPairWithDevices[i], m_InputUser);
					}
				}
				else
				{
					m_InputUser = default(global::UnityEngine.InputSystem.Users.InputUser);
				}
				return;
			}
			if (m_Actions.controlSchemes.Count > 0)
			{
				if (!string.IsNullOrEmpty(s_InitControlScheme))
				{
					global::UnityEngine.InputSystem.InputControlScheme? inputControlScheme = m_Actions.FindControlScheme(s_InitControlScheme);
					if (!inputControlScheme.HasValue)
					{
						global::UnityEngine.Debug.LogError($"No control scheme '{s_InitControlScheme}' in '{m_Actions}'", this);
					}
					else
					{
						TryToActivateControlScheme(inputControlScheme.Value);
					}
				}
				else if (!string.IsNullOrEmpty(m_DefaultControlScheme))
				{
					global::UnityEngine.InputSystem.InputControlScheme? inputControlScheme2 = m_Actions.FindControlScheme(m_DefaultControlScheme);
					if (!inputControlScheme2.HasValue)
					{
						global::UnityEngine.Debug.LogError($"Cannot find default control scheme '{m_DefaultControlScheme}' in '{m_Actions}'", this);
					}
					else
					{
						TryToActivateControlScheme(inputControlScheme2.Value);
					}
				}
				if (s_InitPairWithDevicesCount > 0 && (!m_InputUser.valid || !m_InputUser.controlScheme.HasValue))
				{
					global::UnityEngine.InputSystem.InputControlScheme? inputControlScheme3 = global::UnityEngine.InputSystem.InputControlScheme.FindControlSchemeForDevices(new global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.InputDevice>(s_InitPairWithDevices, 0, s_InitPairWithDevicesCount), m_Actions.controlSchemes, null, allowUnsuccesfulMatch: true);
					if (inputControlScheme3.HasValue)
					{
						TryToActivateControlScheme(inputControlScheme3.Value);
					}
				}
				else if ((!m_InputUser.valid || !m_InputUser.controlScheme.HasValue) && string.IsNullOrEmpty(s_InitControlScheme))
				{
					using global::UnityEngine.InputSystem.InputControlList<global::UnityEngine.InputSystem.InputDevice> inputControlList = global::UnityEngine.InputSystem.Users.InputUser.GetUnpairedInputDevices();
					global::UnityEngine.InputSystem.InputControlScheme? inputControlScheme4 = global::UnityEngine.InputSystem.InputControlScheme.FindControlSchemeForDevices(inputControlList, m_Actions.controlSchemes);
					if (inputControlScheme4.HasValue)
					{
						TryToActivateControlScheme(inputControlScheme4.Value);
					}
					else if (global::UnityEngine.InputSystem.InputSystem.devices.Count > 0 && inputControlList.Count == 0)
					{
						global::UnityEngine.Debug.LogWarning("Cannot find matching control scheme for " + base.name + " (all control schemes are already paired to matching devices)", this);
					}
				}
			}
			else if (s_InitPairWithDevicesCount > 0)
			{
				for (int j = 0; j < s_InitPairWithDevicesCount; j++)
				{
					m_InputUser = global::UnityEngine.InputSystem.Users.InputUser.PerformPairingWithDevice(s_InitPairWithDevices[j], m_InputUser);
				}
			}
			else
			{
				using global::UnityEngine.InputSystem.InputControlList<global::UnityEngine.InputSystem.InputDevice> inputControlList2 = global::UnityEngine.InputSystem.Users.InputUser.GetUnpairedInputDevices();
				for (int k = 0; k < inputControlList2.Count; k++)
				{
					global::UnityEngine.InputSystem.InputDevice device = inputControlList2[k];
					if (HaveBindingForDevice(device))
					{
						m_InputUser = global::UnityEngine.InputSystem.Users.InputUser.PerformPairingWithDevice(device, m_InputUser);
					}
				}
			}
			if (m_InputUser.valid)
			{
				m_InputUser.AssociateActionsWithUser(m_Actions);
			}
		}

		private bool HaveBindingForDevice(global::UnityEngine.InputSystem.InputDevice device)
		{
			if (m_Actions == null)
			{
				return false;
			}
			global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.InputActionMap> actionMaps = m_Actions.actionMaps;
			for (int i = 0; i < actionMaps.Count; i++)
			{
				if (actionMaps[i].IsUsableWithDevice(device))
				{
					return true;
				}
			}
			return false;
		}

		private void UnassignUserAndDevices()
		{
			if (m_InputUser.valid)
			{
				m_InputUser.UnpairDevicesAndRemoveUser();
			}
			if (m_Actions != null)
			{
				m_Actions.devices = null;
			}
		}

		private bool TryToActivateControlScheme(global::UnityEngine.InputSystem.InputControlScheme controlScheme)
		{
			if (s_InitPairWithDevicesCount > 0)
			{
				for (int i = 0; i < s_InitPairWithDevicesCount; i++)
				{
					global::UnityEngine.InputSystem.InputDevice device = s_InitPairWithDevices[i];
					if (!controlScheme.SupportsDevice(device))
					{
						return false;
					}
				}
				for (int j = 0; j < s_InitPairWithDevicesCount; j++)
				{
					global::UnityEngine.InputSystem.InputDevice device2 = s_InitPairWithDevices[j];
					m_InputUser = global::UnityEngine.InputSystem.Users.InputUser.PerformPairingWithDevice(device2, m_InputUser);
				}
			}
			if (!m_InputUser.valid)
			{
				m_InputUser = global::UnityEngine.InputSystem.Users.InputUser.CreateUserWithoutPairedDevices();
			}
			m_InputUser.ActivateControlScheme(controlScheme).AndPairRemainingDevices();
			if (user.hasMissingRequiredDevices)
			{
				m_InputUser.ActivateControlScheme(null);
				m_InputUser.UnpairDevices();
				return false;
			}
			return true;
		}

		private void AssignPlayerIndex()
		{
			if (s_InitPlayerIndex != -1)
			{
				m_PlayerIndex = s_InitPlayerIndex;
				return;
			}
			int num = int.MaxValue;
			int num2 = int.MinValue;
			for (int i = 0; i < s_AllActivePlayersCount; i++)
			{
				int val = s_AllActivePlayers[i].playerIndex;
				num = global::System.Math.Min(num, val);
				num2 = global::System.Math.Max(num2, val);
			}
			if (num != int.MaxValue && num > 0)
			{
				m_PlayerIndex = num - 1;
			}
			else if (num2 != int.MinValue)
			{
				for (int j = num; j < num2; j++)
				{
					if (GetPlayerByIndex(j) == null)
					{
						m_PlayerIndex = j;
						return;
					}
				}
				m_PlayerIndex = num2 + 1;
			}
			else
			{
				m_PlayerIndex = 0;
			}
		}

		private void OnEnable()
		{
			m_Enabled = true;
			using (global::UnityEngine.InputSystem.InputActionRebindingExtensions.DeferBindingResolution())
			{
				AssignPlayerIndex();
				InitializeActions();
				AssignUserAndDevices();
				ActivateInput();
			}
			if (s_InitSplitScreenIndex >= 0)
			{
				m_SplitScreenIndex = s_InitSplitScreenIndex;
			}
			else
			{
				m_SplitScreenIndex = playerIndex;
			}
			global::UnityEngine.InputSystem.Utilities.ArrayHelpers.AppendWithCapacity(ref s_AllActivePlayers, ref s_AllActivePlayersCount, this);
			for (int i = 1; i < s_AllActivePlayersCount; i++)
			{
				int num = i;
				while (num > 0 && s_AllActivePlayers[num - 1].playerIndex > s_AllActivePlayers[num].playerIndex)
				{
					global::UnityEngine.InputSystem.Utilities.ArrayHelpers.SwapElements(s_AllActivePlayers, num, num - 1);
					num--;
				}
			}
			if (s_AllActivePlayersCount == 1)
			{
				if (s_UserChangeDelegate == null)
				{
					s_UserChangeDelegate = OnUserChange;
				}
				global::UnityEngine.InputSystem.Users.InputUser.onChange += s_UserChangeDelegate;
			}
			if (isSinglePlayer)
			{
				if (m_Actions != null && m_Actions.controlSchemes.Count == 0)
				{
					StartListeningForDeviceChanges();
				}
				else if (!neverAutoSwitchControlSchemes)
				{
					StartListeningForUnpairedDeviceActivity();
				}
			}
			HandleControlsChanged();
			global::UnityEngine.InputSystem.PlayerInputManager.instance?.NotifyPlayerJoined(this);
		}

		private void StartListeningForUnpairedDeviceActivity()
		{
			if (!m_OnUnpairedDeviceUsedHooked)
			{
				if (m_UnpairedDeviceUsedDelegate == null)
				{
					m_UnpairedDeviceUsedDelegate = OnUnpairedDeviceUsed;
				}
				if (m_PreFilterUnpairedDeviceUsedDelegate == null)
				{
					m_PreFilterUnpairedDeviceUsedDelegate = OnPreFilterUnpairedDeviceUsed;
				}
				global::UnityEngine.InputSystem.Users.InputUser.onUnpairedDeviceUsed += m_UnpairedDeviceUsedDelegate;
				global::UnityEngine.InputSystem.Users.InputUser.onPrefilterUnpairedDeviceActivity += m_PreFilterUnpairedDeviceUsedDelegate;
				global::UnityEngine.InputSystem.Users.InputUser.listenForUnpairedDeviceActivity++;
				m_OnUnpairedDeviceUsedHooked = true;
			}
		}

		private void StopListeningForUnpairedDeviceActivity()
		{
			if (m_OnUnpairedDeviceUsedHooked)
			{
				global::UnityEngine.InputSystem.Users.InputUser.onUnpairedDeviceUsed -= m_UnpairedDeviceUsedDelegate;
				global::UnityEngine.InputSystem.Users.InputUser.onPrefilterUnpairedDeviceActivity -= m_PreFilterUnpairedDeviceUsedDelegate;
				global::UnityEngine.InputSystem.Users.InputUser.listenForUnpairedDeviceActivity--;
				m_OnUnpairedDeviceUsedHooked = false;
			}
		}

		private void StartListeningForDeviceChanges()
		{
			if (!m_OnDeviceChangeHooked)
			{
				if (m_DeviceChangeDelegate == null)
				{
					m_DeviceChangeDelegate = OnDeviceChange;
				}
				global::UnityEngine.InputSystem.InputSystem.onDeviceChange += m_DeviceChangeDelegate;
				m_OnDeviceChangeHooked = true;
			}
		}

		private void StopListeningForDeviceChanges()
		{
			if (m_OnDeviceChangeHooked)
			{
				global::UnityEngine.InputSystem.InputSystem.onDeviceChange -= m_DeviceChangeDelegate;
				m_OnDeviceChangeHooked = false;
			}
		}

		private void OnDisable()
		{
			m_Enabled = false;
			int num = global::UnityEngine.InputSystem.Utilities.ArrayHelpers.IndexOfReference(s_AllActivePlayers, this, s_AllActivePlayersCount);
			if (num != -1)
			{
				global::UnityEngine.InputSystem.Utilities.ArrayHelpers.EraseAtWithCapacity(s_AllActivePlayers, ref s_AllActivePlayersCount, num);
			}
			if (s_AllActivePlayersCount == 0 && s_UserChangeDelegate != null)
			{
				global::UnityEngine.InputSystem.Users.InputUser.onChange -= s_UserChangeDelegate;
			}
			StopListeningForUnpairedDeviceActivity();
			StopListeningForDeviceChanges();
			global::UnityEngine.InputSystem.PlayerInputManager.instance?.NotifyPlayerLeft(this);
			using (global::UnityEngine.InputSystem.InputActionRebindingExtensions.DeferBindingResolution())
			{
				DeactivateInput();
				UnassignUserAndDevices();
				UninitializeActions();
			}
			m_PlayerIndex = -1;
		}

		public void DebugLogAction(global::UnityEngine.InputSystem.InputAction.CallbackContext context)
		{
			global::UnityEngine.Debug.Log(context.ToString());
		}

		private void HandleDeviceLost()
		{
			switch (m_NotificationBehavior)
			{
			case global::UnityEngine.InputSystem.PlayerNotifications.SendMessages:
				SendMessage("OnDeviceLost", this, global::UnityEngine.SendMessageOptions.DontRequireReceiver);
				break;
			case global::UnityEngine.InputSystem.PlayerNotifications.BroadcastMessages:
				BroadcastMessage("OnDeviceLost", this, global::UnityEngine.SendMessageOptions.DontRequireReceiver);
				break;
			case global::UnityEngine.InputSystem.PlayerNotifications.InvokeUnityEvents:
				m_DeviceLostEvent?.Invoke(this);
				break;
			case global::UnityEngine.InputSystem.PlayerNotifications.InvokeCSharpEvents:
				global::UnityEngine.InputSystem.Utilities.DelegateHelpers.InvokeCallbacksSafe(ref m_DeviceLostCallbacks, this, "onDeviceLost");
				break;
			}
		}

		private void HandleDeviceRegained()
		{
			switch (m_NotificationBehavior)
			{
			case global::UnityEngine.InputSystem.PlayerNotifications.SendMessages:
				SendMessage("OnDeviceRegained", this, global::UnityEngine.SendMessageOptions.DontRequireReceiver);
				break;
			case global::UnityEngine.InputSystem.PlayerNotifications.BroadcastMessages:
				BroadcastMessage("OnDeviceRegained", this, global::UnityEngine.SendMessageOptions.DontRequireReceiver);
				break;
			case global::UnityEngine.InputSystem.PlayerNotifications.InvokeUnityEvents:
				m_DeviceRegainedEvent?.Invoke(this);
				break;
			case global::UnityEngine.InputSystem.PlayerNotifications.InvokeCSharpEvents:
				global::UnityEngine.InputSystem.Utilities.DelegateHelpers.InvokeCallbacksSafe(ref m_DeviceRegainedCallbacks, this, "onDeviceRegained");
				break;
			}
		}

		private void HandleControlsChanged()
		{
			switch (m_NotificationBehavior)
			{
			case global::UnityEngine.InputSystem.PlayerNotifications.SendMessages:
				SendMessage("OnControlsChanged", this, global::UnityEngine.SendMessageOptions.DontRequireReceiver);
				break;
			case global::UnityEngine.InputSystem.PlayerNotifications.BroadcastMessages:
				BroadcastMessage("OnControlsChanged", this, global::UnityEngine.SendMessageOptions.DontRequireReceiver);
				break;
			case global::UnityEngine.InputSystem.PlayerNotifications.InvokeUnityEvents:
				m_ControlsChangedEvent?.Invoke(this);
				break;
			case global::UnityEngine.InputSystem.PlayerNotifications.InvokeCSharpEvents:
				global::UnityEngine.InputSystem.Utilities.DelegateHelpers.InvokeCallbacksSafe(ref m_ControlsChangedCallbacks, this, "onControlsChanged");
				break;
			}
		}

		private static void OnUserChange(global::UnityEngine.InputSystem.Users.InputUser user, global::UnityEngine.InputSystem.Users.InputUserChange change, global::UnityEngine.InputSystem.InputDevice device)
		{
			switch (change)
			{
			case global::UnityEngine.InputSystem.Users.InputUserChange.DeviceLost:
			case global::UnityEngine.InputSystem.Users.InputUserChange.DeviceRegained:
			{
				for (int j = 0; j < s_AllActivePlayersCount; j++)
				{
					global::UnityEngine.InputSystem.PlayerInput playerInput2 = s_AllActivePlayers[j];
					if (playerInput2.m_InputUser == user)
					{
						switch (change)
						{
						case global::UnityEngine.InputSystem.Users.InputUserChange.DeviceLost:
							playerInput2.HandleDeviceLost();
							break;
						case global::UnityEngine.InputSystem.Users.InputUserChange.DeviceRegained:
							playerInput2.HandleDeviceRegained();
							break;
						}
					}
				}
				break;
			}
			case global::UnityEngine.InputSystem.Users.InputUserChange.ControlsChanged:
			{
				for (int i = 0; i < s_AllActivePlayersCount; i++)
				{
					global::UnityEngine.InputSystem.PlayerInput playerInput = s_AllActivePlayers[i];
					if (playerInput.m_InputUser == user)
					{
						playerInput.HandleControlsChanged();
					}
				}
				break;
			}
			}
		}

		private static bool OnPreFilterUnpairedDeviceUsed(global::UnityEngine.InputSystem.InputDevice device, global::UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr)
		{
			global::UnityEngine.InputSystem.InputActionAsset inputActionAsset = all[0].actions;
			if (inputActionAsset != null && (!global::UnityEngine.InputSystem.OnScreen.OnScreenControl.HasAnyActive || !(device is global::UnityEngine.InputSystem.Pointer)))
			{
				return inputActionAsset.IsUsableWithDevice(device);
			}
			return false;
		}

		private void OnUnpairedDeviceUsed(global::UnityEngine.InputSystem.InputControl control, global::UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr)
		{
			if (!isSinglePlayer || neverAutoSwitchControlSchemes)
			{
				return;
			}
			global::UnityEngine.InputSystem.PlayerInput playerInput = all[0];
			if (playerInput.m_Actions == null)
			{
				return;
			}
			global::UnityEngine.InputSystem.InputDevice device = control.device;
			using (global::UnityEngine.InputSystem.InputActionRebindingExtensions.DeferBindingResolution())
			{
				using global::UnityEngine.InputSystem.InputControlList<global::UnityEngine.InputSystem.InputDevice> inputControlList = global::UnityEngine.InputSystem.Users.InputUser.GetUnpairedInputDevices();
				if (inputControlList.Count > 1)
				{
					int index = inputControlList.IndexOf(device);
					inputControlList.SwapElements(0, index);
				}
				global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.InputDevice> readOnlyArray = playerInput.devices;
				for (int i = 0; i < readOnlyArray.Count; i++)
				{
					inputControlList.Add(readOnlyArray[i]);
				}
				if (!global::UnityEngine.InputSystem.InputControlScheme.FindControlSchemeForDevices(inputControlList, playerInput.m_Actions.controlSchemes, out var controlScheme, out var matchResult, device))
				{
					return;
				}
				try
				{
					bool valid = playerInput.user.valid;
					if (valid)
					{
						playerInput.user.UnpairDevices();
					}
					global::UnityEngine.InputSystem.InputControlList<global::UnityEngine.InputSystem.InputDevice> inputControlList2 = matchResult.devices;
					for (int j = 0; j < inputControlList2.Count; j++)
					{
						playerInput.m_InputUser = global::UnityEngine.InputSystem.Users.InputUser.PerformPairingWithDevice(inputControlList2[j], playerInput.m_InputUser);
						if (!valid && playerInput.actions != null)
						{
							playerInput.m_InputUser.AssociateActionsWithUser(playerInput.actions);
						}
					}
					playerInput.user.ActivateControlScheme(controlScheme);
				}
				finally
				{
					matchResult.Dispose();
				}
			}
		}

		private void OnDeviceChange(global::UnityEngine.InputSystem.InputDevice device, global::UnityEngine.InputSystem.InputDeviceChange change)
		{
			if (change == global::UnityEngine.InputSystem.InputDeviceChange.Added && isSinglePlayer && m_Actions != null && m_Actions.controlSchemes.Count == 0 && HaveBindingForDevice(device) && m_InputUser.valid)
			{
				global::UnityEngine.InputSystem.Users.InputUser.PerformPairingWithDevice(device, m_InputUser);
			}
		}

		private void SwitchControlSchemeInternal(ref global::UnityEngine.InputSystem.InputControlScheme controlScheme, params global::UnityEngine.InputSystem.InputDevice[] devices)
		{
			using (global::UnityEngine.InputSystem.InputActionRebindingExtensions.DeferBindingResolution())
			{
				for (int num = user.pairedDevices.Count - 1; num >= 0; num--)
				{
					if (!global::UnityEngine.InputSystem.Utilities.ArrayHelpers.ContainsReference(devices, user.pairedDevices[num]))
					{
						user.UnpairDevice(user.pairedDevices[num]);
					}
				}
				foreach (global::UnityEngine.InputSystem.InputDevice inputDevice in devices)
				{
					if (!global::UnityEngine.InputSystem.Utilities.ReadOnlyArrayExtensions.ContainsReference(user.pairedDevices, inputDevice))
					{
						global::UnityEngine.InputSystem.Users.InputUser.PerformPairingWithDevice(inputDevice, user);
					}
				}
				if (!user.controlScheme.HasValue || !user.controlScheme.Value.Equals(controlScheme))
				{
					user.ActivateControlScheme(controlScheme);
				}
			}
		}
	}
}
