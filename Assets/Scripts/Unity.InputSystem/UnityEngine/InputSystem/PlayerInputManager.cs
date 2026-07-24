namespace UnityEngine.InputSystem
{
	[global::UnityEngine.AddComponentMenu("Input/Player Input Manager")]
	[global::UnityEngine.HelpURL("https://docs.unity3d.com/Packages/com.unity.inputsystem@1.18/manual/PlayerInputManager.html")]
	public class PlayerInputManager : global::UnityEngine.MonoBehaviour
	{
		[global::System.Serializable]
		public class PlayerJoinedEvent : global::UnityEngine.Events.UnityEvent<global::UnityEngine.InputSystem.PlayerInput>
		{
		}

		[global::System.Serializable]
		public class PlayerLeftEvent : global::UnityEngine.Events.UnityEvent<global::UnityEngine.InputSystem.PlayerInput>
		{
		}

		public const string PlayerJoinedMessage = "OnPlayerJoined";

		public const string PlayerLeftMessage = "OnPlayerLeft";

		[global::UnityEngine.SerializeField]
		internal global::UnityEngine.InputSystem.PlayerNotifications m_NotificationBehavior;

		[global::UnityEngine.Tooltip("Set a limit for the maximum number of players who are able to join.")]
		[global::UnityEngine.SerializeField]
		internal int m_MaxPlayerCount = -1;

		[global::UnityEngine.SerializeField]
		internal bool m_AllowJoining = true;

		[global::UnityEngine.SerializeField]
		internal global::UnityEngine.InputSystem.PlayerJoinBehavior m_JoinBehavior;

		[global::UnityEngine.SerializeField]
		internal global::UnityEngine.InputSystem.PlayerInputManager.PlayerJoinedEvent m_PlayerJoinedEvent;

		[global::UnityEngine.SerializeField]
		internal global::UnityEngine.InputSystem.PlayerInputManager.PlayerLeftEvent m_PlayerLeftEvent;

		[global::UnityEngine.SerializeField]
		internal global::UnityEngine.InputSystem.InputActionProperty m_JoinAction;

		[global::UnityEngine.SerializeField]
		internal global::UnityEngine.GameObject m_PlayerPrefab;

		[global::UnityEngine.SerializeField]
		internal bool m_SplitScreen;

		[global::UnityEngine.SerializeField]
		internal bool m_MaintainAspectRatioInSplitScreen;

		[global::UnityEngine.Tooltip("Explicitly set a fixed number of screens or otherwise allow the screen to be divided automatically to best fit the number of players.")]
		[global::UnityEngine.SerializeField]
		internal int m_FixedNumberOfSplitScreens = -1;

		[global::UnityEngine.SerializeField]
		internal global::UnityEngine.Rect m_SplitScreenRect = new global::UnityEngine.Rect(0f, 0f, 1f, 1f);

		[global::System.NonSerialized]
		private bool m_JoinActionDelegateHooked;

		[global::System.NonSerialized]
		private bool m_UnpairedDeviceUsedDelegateHooked;

		[global::System.NonSerialized]
		private global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext> m_JoinActionDelegate;

		[global::System.NonSerialized]
		private global::System.Action<global::UnityEngine.InputSystem.InputControl, global::UnityEngine.InputSystem.LowLevel.InputEventPtr> m_UnpairedDeviceUsedDelegate;

		[global::System.NonSerialized]
		private global::UnityEngine.InputSystem.Utilities.CallbackArray<global::System.Action<global::UnityEngine.InputSystem.PlayerInput>> m_PlayerJoinedCallbacks;

		[global::System.NonSerialized]
		private global::UnityEngine.InputSystem.Utilities.CallbackArray<global::System.Action<global::UnityEngine.InputSystem.PlayerInput>> m_PlayerLeftCallbacks;

		public bool splitScreen
		{
			get
			{
				return m_SplitScreen;
			}
			set
			{
				if (m_SplitScreen == value)
				{
					return;
				}
				m_SplitScreen = value;
				if (!m_SplitScreen)
				{
					foreach (global::UnityEngine.InputSystem.PlayerInput item in global::UnityEngine.InputSystem.PlayerInput.all)
					{
						global::UnityEngine.Camera camera = item.camera;
						if (camera != null)
						{
							camera.rect = new global::UnityEngine.Rect(0f, 0f, 1f, 1f);
						}
					}
					return;
				}
				UpdateSplitScreen();
			}
		}

		public bool maintainAspectRatioInSplitScreen => m_MaintainAspectRatioInSplitScreen;

		public int fixedNumberOfSplitScreens => m_FixedNumberOfSplitScreens;

		public global::UnityEngine.Rect splitScreenArea => m_SplitScreenRect;

		public int playerCount => global::UnityEngine.InputSystem.PlayerInput.s_AllActivePlayersCount;

		public int maxPlayerCount => m_MaxPlayerCount;

		public bool joiningEnabled => m_AllowJoining;

		public global::UnityEngine.InputSystem.PlayerJoinBehavior joinBehavior
		{
			get
			{
				return m_JoinBehavior;
			}
			set
			{
				if (m_JoinBehavior != value)
				{
					bool allowJoining = m_AllowJoining;
					if (allowJoining)
					{
						DisableJoining();
					}
					m_JoinBehavior = value;
					if (allowJoining)
					{
						EnableJoining();
					}
				}
			}
		}

		public global::UnityEngine.InputSystem.InputActionProperty joinAction
		{
			get
			{
				return m_JoinAction;
			}
			set
			{
				if (m_JoinAction == value)
				{
					return;
				}
				int num;
				if (m_AllowJoining)
				{
					num = ((m_JoinBehavior == global::UnityEngine.InputSystem.PlayerJoinBehavior.JoinPlayersWhenJoinActionIsTriggered) ? 1 : 0);
					if (num != 0)
					{
						DisableJoining();
					}
				}
				else
				{
					num = 0;
				}
				m_JoinAction = value;
				if (num != 0)
				{
					EnableJoining();
				}
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
				m_NotificationBehavior = value;
			}
		}

		public global::UnityEngine.InputSystem.PlayerInputManager.PlayerJoinedEvent playerJoinedEvent
		{
			get
			{
				if (m_PlayerJoinedEvent == null)
				{
					m_PlayerJoinedEvent = new global::UnityEngine.InputSystem.PlayerInputManager.PlayerJoinedEvent();
				}
				return m_PlayerJoinedEvent;
			}
		}

		public global::UnityEngine.InputSystem.PlayerInputManager.PlayerLeftEvent playerLeftEvent
		{
			get
			{
				if (m_PlayerLeftEvent == null)
				{
					m_PlayerLeftEvent = new global::UnityEngine.InputSystem.PlayerInputManager.PlayerLeftEvent();
				}
				return m_PlayerLeftEvent;
			}
		}

		public global::UnityEngine.GameObject playerPrefab
		{
			get
			{
				return m_PlayerPrefab;
			}
			set
			{
				m_PlayerPrefab = value;
			}
		}

		public static global::UnityEngine.InputSystem.PlayerInputManager instance { get; private set; }

		internal static string[] messages => new string[2] { "OnPlayerJoined", "OnPlayerLeft" };

		public event global::System.Action<global::UnityEngine.InputSystem.PlayerInput> onPlayerJoined
		{
			add
			{
				if (value == null)
				{
					throw new global::System.ArgumentNullException("value");
				}
				m_PlayerJoinedCallbacks.AddCallback(value);
			}
			remove
			{
				if (value == null)
				{
					throw new global::System.ArgumentNullException("value");
				}
				m_PlayerJoinedCallbacks.RemoveCallback(value);
			}
		}

		public event global::System.Action<global::UnityEngine.InputSystem.PlayerInput> onPlayerLeft
		{
			add
			{
				if (value == null)
				{
					throw new global::System.ArgumentNullException("value");
				}
				m_PlayerLeftCallbacks.AddCallback(value);
			}
			remove
			{
				if (value == null)
				{
					throw new global::System.ArgumentNullException("value");
				}
				m_PlayerLeftCallbacks.RemoveCallback(value);
			}
		}

		public void EnableJoining()
		{
			switch (m_JoinBehavior)
			{
			case global::UnityEngine.InputSystem.PlayerJoinBehavior.JoinPlayersWhenButtonIsPressed:
				ValidateInputActionAsset();
				if (!m_UnpairedDeviceUsedDelegateHooked)
				{
					if (m_UnpairedDeviceUsedDelegate == null)
					{
						m_UnpairedDeviceUsedDelegate = OnUnpairedDeviceUsed;
					}
					global::UnityEngine.InputSystem.Users.InputUser.onUnpairedDeviceUsed += m_UnpairedDeviceUsedDelegate;
					m_UnpairedDeviceUsedDelegateHooked = true;
					global::UnityEngine.InputSystem.Users.InputUser.listenForUnpairedDeviceActivity++;
				}
				break;
			case global::UnityEngine.InputSystem.PlayerJoinBehavior.JoinPlayersWhenJoinActionIsTriggered:
				if (m_JoinAction.action != null)
				{
					if (!m_JoinActionDelegateHooked)
					{
						if (m_JoinActionDelegate == null)
						{
							m_JoinActionDelegate = JoinPlayerFromActionIfNotAlreadyJoined;
						}
						m_JoinAction.action.performed += m_JoinActionDelegate;
						m_JoinActionDelegateHooked = true;
					}
					m_JoinAction.action.Enable();
				}
				else
				{
					global::UnityEngine.Debug.LogError("No join action configured on PlayerInputManager but join behavior is set to JoinPlayersWhenJoinActionIsTriggered", this);
				}
				break;
			}
			m_AllowJoining = true;
		}

		public void DisableJoining()
		{
			switch (m_JoinBehavior)
			{
			case global::UnityEngine.InputSystem.PlayerJoinBehavior.JoinPlayersWhenButtonIsPressed:
				if (m_UnpairedDeviceUsedDelegateHooked)
				{
					global::UnityEngine.InputSystem.Users.InputUser.onUnpairedDeviceUsed -= m_UnpairedDeviceUsedDelegate;
					m_UnpairedDeviceUsedDelegateHooked = false;
					global::UnityEngine.InputSystem.Users.InputUser.listenForUnpairedDeviceActivity--;
				}
				break;
			case global::UnityEngine.InputSystem.PlayerJoinBehavior.JoinPlayersWhenJoinActionIsTriggered:
				if (m_JoinActionDelegateHooked)
				{
					if (m_JoinAction.action != null)
					{
						m_JoinAction.action.performed -= m_JoinActionDelegate;
					}
					m_JoinActionDelegateHooked = false;
				}
				m_JoinAction.action?.Disable();
				break;
			}
			m_AllowJoining = false;
		}

		internal void JoinPlayerFromUI()
		{
			if (!CheckIfPlayerCanJoin())
			{
				return;
			}
			throw new global::System.NotImplementedException();
		}

		public void JoinPlayerFromAction(global::UnityEngine.InputSystem.InputAction.CallbackContext context)
		{
			if (CheckIfPlayerCanJoin())
			{
				global::UnityEngine.InputSystem.InputDevice device = context.control.device;
				JoinPlayer(-1, -1, null, device);
			}
		}

		public void JoinPlayerFromActionIfNotAlreadyJoined(global::UnityEngine.InputSystem.InputAction.CallbackContext context)
		{
			if (CheckIfPlayerCanJoin())
			{
				global::UnityEngine.InputSystem.InputDevice device = context.control.device;
				if (!(global::UnityEngine.InputSystem.PlayerInput.FindFirstPairedToDevice(device) != null))
				{
					JoinPlayer(-1, -1, null, device);
				}
			}
		}

		public global::UnityEngine.InputSystem.PlayerInput JoinPlayer(int playerIndex = -1, int splitScreenIndex = -1, string controlScheme = null, global::UnityEngine.InputSystem.InputDevice pairWithDevice = null)
		{
			if (!CheckIfPlayerCanJoin(playerIndex))
			{
				return null;
			}
			global::UnityEngine.InputSystem.PlayerInput.s_DestroyIfDeviceSetupUnsuccessful = true;
			return global::UnityEngine.InputSystem.PlayerInput.Instantiate(m_PlayerPrefab, playerIndex, controlScheme, splitScreenIndex, pairWithDevice);
		}

		public global::UnityEngine.InputSystem.PlayerInput JoinPlayer(int playerIndex = -1, int splitScreenIndex = -1, string controlScheme = null, params global::UnityEngine.InputSystem.InputDevice[] pairWithDevices)
		{
			if (!CheckIfPlayerCanJoin(playerIndex))
			{
				return null;
			}
			global::UnityEngine.InputSystem.PlayerInput.s_DestroyIfDeviceSetupUnsuccessful = true;
			return global::UnityEngine.InputSystem.PlayerInput.Instantiate(m_PlayerPrefab, playerIndex, controlScheme, splitScreenIndex, pairWithDevices);
		}

		private bool CheckIfPlayerCanJoin(int playerIndex = -1)
		{
			if (m_PlayerPrefab == null)
			{
				global::UnityEngine.Debug.LogError("playerPrefab must be set in order to be able to join new players", this);
				return false;
			}
			if (m_MaxPlayerCount >= 0 && playerCount >= m_MaxPlayerCount)
			{
				global::UnityEngine.Debug.LogWarning("Maximum number of supported players reached: " + maxPlayerCount, this);
				return false;
			}
			if (playerIndex != -1)
			{
				for (int i = 0; i < global::UnityEngine.InputSystem.PlayerInput.s_AllActivePlayersCount; i++)
				{
					if (global::UnityEngine.InputSystem.PlayerInput.s_AllActivePlayers[i].playerIndex == playerIndex)
					{
						global::UnityEngine.Debug.LogError($"Player index #{playerIndex} is already taken by player {(global::UnityEngine.InputSystem.PlayerInput.s_AllActivePlayers[i])}", global::UnityEngine.InputSystem.PlayerInput.s_AllActivePlayers[i]);
						return false;
					}
				}
			}
			return true;
		}

		private void OnUnpairedDeviceUsed(global::UnityEngine.InputSystem.InputControl control, global::UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr)
		{
			if (m_AllowJoining && m_JoinBehavior == global::UnityEngine.InputSystem.PlayerJoinBehavior.JoinPlayersWhenButtonIsPressed && control is global::UnityEngine.InputSystem.Controls.ButtonControl && IsDeviceUsableWithPlayerActions(control.device))
			{
				JoinPlayer(-1, -1, null, control.device);
			}
		}

		private void OnEnable()
		{
			if (instance == null)
			{
				instance = this;
				if (joinAction.reference != null && joinAction.action?.actionMap?.asset != null)
				{
					global::UnityEngine.InputSystem.InputActionReference reference = global::UnityEngine.InputSystem.InputActionReference.Create(global::UnityEngine.Object.Instantiate(joinAction.action.actionMap.asset).FindAction(joinAction.action.name));
					joinAction = new global::UnityEngine.InputSystem.InputActionProperty(reference);
				}
				for (int i = 0; i < global::UnityEngine.InputSystem.PlayerInput.s_AllActivePlayersCount; i++)
				{
					NotifyPlayerJoined(global::UnityEngine.InputSystem.PlayerInput.s_AllActivePlayers[i]);
				}
				if (m_AllowJoining)
				{
					EnableJoining();
				}
			}
			else
			{
				global::UnityEngine.Debug.LogWarning("Multiple PlayerInputManagers in the game. There should only be one PlayerInputManager", this);
			}
		}

		private void OnDisable()
		{
			if (instance == this)
			{
				instance = null;
			}
			if (m_AllowJoining)
			{
				DisableJoining();
			}
		}

		private void UpdateSplitScreen()
		{
			if (!m_SplitScreen)
			{
				return;
			}
			int num = 0;
			foreach (global::UnityEngine.InputSystem.PlayerInput item in global::UnityEngine.InputSystem.PlayerInput.all)
			{
				if (item.playerIndex >= num)
				{
					num = item.playerIndex + 1;
				}
			}
			if (m_FixedNumberOfSplitScreens > 0)
			{
				if (m_FixedNumberOfSplitScreens < num)
				{
					global::UnityEngine.Debug.LogWarning($"Highest playerIndex of {num} exceeds fixed number of split-screens of {m_FixedNumberOfSplitScreens}", this);
				}
				num = m_FixedNumberOfSplitScreens;
			}
			int num2 = global::UnityEngine.Mathf.CeilToInt(global::UnityEngine.Mathf.Sqrt(num));
			int num3 = num2;
			if (!m_MaintainAspectRatioInSplitScreen && num2 * (num2 - 1) >= num)
			{
				num3--;
			}
			foreach (global::UnityEngine.InputSystem.PlayerInput item2 in global::UnityEngine.InputSystem.PlayerInput.all)
			{
				int splitScreenIndex = item2.splitScreenIndex;
				if (splitScreenIndex >= num2 * num3)
				{
					global::UnityEngine.Debug.LogError($"Split-screen index of {splitScreenIndex} on player is out of range (have {num2 * num3} screens); resetting to playerIndex", item2);
					item2.m_SplitScreenIndex = item2.playerIndex;
				}
				global::UnityEngine.Camera camera = item2.camera;
				if (camera == null)
				{
					global::UnityEngine.Debug.LogError("Player has no camera associated with it. Cannot set up split-screen. Point PlayerInput.camera to camera for player.", item2);
					continue;
				}
				int num4 = splitScreenIndex % num2;
				int num5 = splitScreenIndex / num2;
				global::UnityEngine.Rect rect = new global::UnityEngine.Rect
				{
					width = m_SplitScreenRect.width / (float)num2,
					height = m_SplitScreenRect.height / (float)num3
				};
				rect.x = m_SplitScreenRect.x + (float)num4 * rect.width;
				rect.y = m_SplitScreenRect.y + m_SplitScreenRect.height - (float)(num5 + 1) * rect.height;
				camera.rect = rect;
			}
		}

		private bool IsDeviceUsableWithPlayerActions(global::UnityEngine.InputSystem.InputDevice device)
		{
			if (m_PlayerPrefab == null)
			{
				return true;
			}
			global::UnityEngine.InputSystem.PlayerInput componentInChildren = m_PlayerPrefab.GetComponentInChildren<global::UnityEngine.InputSystem.PlayerInput>();
			if (componentInChildren == null)
			{
				return true;
			}
			global::UnityEngine.InputSystem.InputActionAsset actions = componentInChildren.actions;
			if (actions == null)
			{
				return true;
			}
			if (actions.controlSchemes.Count > 0)
			{
				using (global::UnityEngine.InputSystem.InputControlList<global::UnityEngine.InputSystem.InputDevice> devices = global::UnityEngine.InputSystem.Users.InputUser.GetUnpairedInputDevices())
				{
					if (!global::UnityEngine.InputSystem.InputControlScheme.FindControlSchemeForDevices(devices, actions.controlSchemes, device).HasValue)
					{
						return false;
					}
				}
				return true;
			}
			foreach (global::UnityEngine.InputSystem.InputActionMap actionMap in actions.actionMaps)
			{
				if (actionMap.IsUsableWithDevice(device))
				{
					return true;
				}
			}
			return false;
		}

		private void ValidateInputActionAsset()
		{
		}

		internal void NotifyPlayerJoined(global::UnityEngine.InputSystem.PlayerInput player)
		{
			UpdateSplitScreen();
			switch (m_NotificationBehavior)
			{
			case global::UnityEngine.InputSystem.PlayerNotifications.SendMessages:
				SendMessage("OnPlayerJoined", player, global::UnityEngine.SendMessageOptions.DontRequireReceiver);
				break;
			case global::UnityEngine.InputSystem.PlayerNotifications.BroadcastMessages:
				BroadcastMessage("OnPlayerJoined", player, global::UnityEngine.SendMessageOptions.DontRequireReceiver);
				break;
			case global::UnityEngine.InputSystem.PlayerNotifications.InvokeUnityEvents:
				m_PlayerJoinedEvent?.Invoke(player);
				break;
			case global::UnityEngine.InputSystem.PlayerNotifications.InvokeCSharpEvents:
				global::UnityEngine.InputSystem.Utilities.DelegateHelpers.InvokeCallbacksSafe(ref m_PlayerJoinedCallbacks, player, "onPlayerJoined");
				break;
			}
		}

		internal void NotifyPlayerLeft(global::UnityEngine.InputSystem.PlayerInput player)
		{
			UpdateSplitScreen();
			switch (m_NotificationBehavior)
			{
			case global::UnityEngine.InputSystem.PlayerNotifications.SendMessages:
				SendMessage("OnPlayerLeft", player, global::UnityEngine.SendMessageOptions.DontRequireReceiver);
				break;
			case global::UnityEngine.InputSystem.PlayerNotifications.BroadcastMessages:
				BroadcastMessage("OnPlayerLeft", player, global::UnityEngine.SendMessageOptions.DontRequireReceiver);
				break;
			case global::UnityEngine.InputSystem.PlayerNotifications.InvokeUnityEvents:
				m_PlayerLeftEvent?.Invoke(player);
				break;
			case global::UnityEngine.InputSystem.PlayerNotifications.InvokeCSharpEvents:
				global::UnityEngine.InputSystem.Utilities.DelegateHelpers.InvokeCallbacksSafe(ref m_PlayerLeftCallbacks, player, "onPlayerLeft");
				break;
			}
		}
	}
}
