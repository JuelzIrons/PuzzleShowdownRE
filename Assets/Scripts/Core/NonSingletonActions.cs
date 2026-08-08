public class NonSingletonActions : global::UnityEngine.InputSystem.IInputActionCollection2, global::UnityEngine.InputSystem.IInputActionCollection, global::System.Collections.Generic.IEnumerable<global::UnityEngine.InputSystem.InputAction>, global::System.Collections.IEnumerable, global::System.IDisposable
{
	public struct PlayerActions
	{
		private NonSingletonActions m_Wrapper;

		public global::UnityEngine.InputSystem.InputAction MoveUp => m_Wrapper.m_Player_MoveUp;

		public global::UnityEngine.InputSystem.InputAction MoveDown => m_Wrapper.m_Player_MoveDown;

		public global::UnityEngine.InputSystem.InputAction MoveRight => m_Wrapper.m_Player_MoveRight;

		public global::UnityEngine.InputSystem.InputAction MoveLeft => m_Wrapper.m_Player_MoveLeft;

		public global::UnityEngine.InputSystem.InputAction ForceUp => m_Wrapper.m_Player_ForceUp;

		public global::UnityEngine.InputSystem.InputAction SwapBlocks => m_Wrapper.m_Player_SwapBlocks;

		public global::UnityEngine.InputSystem.InputAction AltSwapBlocks => m_Wrapper.m_Player_AltSwapBlocks;

		public global::UnityEngine.InputSystem.InputAction Pause => m_Wrapper.m_Player_Pause;

		public global::UnityEngine.InputSystem.InputAction SpecialEnter => m_Wrapper.m_Player_SpecialEnter;

		public bool enabled => Get().enabled;

		public PlayerActions(NonSingletonActions wrapper)
		{
			m_Wrapper = wrapper;
		}

		public global::UnityEngine.InputSystem.InputActionMap Get()
		{
			return m_Wrapper.m_Player;
		}

		public void Enable()
		{
			Get().Enable();
		}

		public void Disable()
		{
			Get().Disable();
		}

		public static implicit operator global::UnityEngine.InputSystem.InputActionMap(NonSingletonActions.PlayerActions set)
		{
			return set.Get();
		}

		public void AddCallbacks(NonSingletonActions.IPlayerActions instance)
		{
			if (instance != null && !m_Wrapper.m_PlayerActionsCallbackInterfaces.Contains(instance))
			{
				m_Wrapper.m_PlayerActionsCallbackInterfaces.Add(instance);
				MoveUp.started += instance.OnMoveUp;
				MoveUp.performed += instance.OnMoveUp;
				MoveUp.canceled += instance.OnMoveUp;
				MoveDown.started += instance.OnMoveDown;
				MoveDown.performed += instance.OnMoveDown;
				MoveDown.canceled += instance.OnMoveDown;
				MoveRight.started += instance.OnMoveRight;
				MoveRight.performed += instance.OnMoveRight;
				MoveRight.canceled += instance.OnMoveRight;
				MoveLeft.started += instance.OnMoveLeft;
				MoveLeft.performed += instance.OnMoveLeft;
				MoveLeft.canceled += instance.OnMoveLeft;
				ForceUp.started += instance.OnForceUp;
				ForceUp.performed += instance.OnForceUp;
				ForceUp.canceled += instance.OnForceUp;
				SwapBlocks.started += instance.OnSwapBlocks;
				SwapBlocks.performed += instance.OnSwapBlocks;
				SwapBlocks.canceled += instance.OnSwapBlocks;
				AltSwapBlocks.started += instance.OnAltSwapBlocks;
				AltSwapBlocks.performed += instance.OnAltSwapBlocks;
				AltSwapBlocks.canceled += instance.OnAltSwapBlocks;
				Pause.started += instance.OnPause;
				Pause.performed += instance.OnPause;
				Pause.canceled += instance.OnPause;
				SpecialEnter.started += instance.OnSpecialEnter;
				SpecialEnter.performed += instance.OnSpecialEnter;
				SpecialEnter.canceled += instance.OnSpecialEnter;
			}
		}

		private void UnregisterCallbacks(NonSingletonActions.IPlayerActions instance)
		{
			MoveUp.started -= instance.OnMoveUp;
			MoveUp.performed -= instance.OnMoveUp;
			MoveUp.canceled -= instance.OnMoveUp;
			MoveDown.started -= instance.OnMoveDown;
			MoveDown.performed -= instance.OnMoveDown;
			MoveDown.canceled -= instance.OnMoveDown;
			MoveRight.started -= instance.OnMoveRight;
			MoveRight.performed -= instance.OnMoveRight;
			MoveRight.canceled -= instance.OnMoveRight;
			MoveLeft.started -= instance.OnMoveLeft;
			MoveLeft.performed -= instance.OnMoveLeft;
			MoveLeft.canceled -= instance.OnMoveLeft;
			ForceUp.started -= instance.OnForceUp;
			ForceUp.performed -= instance.OnForceUp;
			ForceUp.canceled -= instance.OnForceUp;
			SwapBlocks.started -= instance.OnSwapBlocks;
			SwapBlocks.performed -= instance.OnSwapBlocks;
			SwapBlocks.canceled -= instance.OnSwapBlocks;
			AltSwapBlocks.started -= instance.OnAltSwapBlocks;
			AltSwapBlocks.performed -= instance.OnAltSwapBlocks;
			AltSwapBlocks.canceled -= instance.OnAltSwapBlocks;
			Pause.started -= instance.OnPause;
			Pause.performed -= instance.OnPause;
			Pause.canceled -= instance.OnPause;
			SpecialEnter.started -= instance.OnSpecialEnter;
			SpecialEnter.performed -= instance.OnSpecialEnter;
			SpecialEnter.canceled -= instance.OnSpecialEnter;
		}

		public void RemoveCallbacks(NonSingletonActions.IPlayerActions instance)
		{
			if (m_Wrapper.m_PlayerActionsCallbackInterfaces.Remove(instance))
			{
				UnregisterCallbacks(instance);
			}
		}

		public void SetCallbacks(NonSingletonActions.IPlayerActions instance)
		{
			foreach (NonSingletonActions.IPlayerActions playerActionsCallbackInterface in m_Wrapper.m_PlayerActionsCallbackInterfaces)
			{
				UnregisterCallbacks(playerActionsCallbackInterface);
			}
			m_Wrapper.m_PlayerActionsCallbackInterfaces.Clear();
			AddCallbacks(instance);
		}
	}

	public struct UIActions
	{
		private NonSingletonActions m_Wrapper;

		public global::UnityEngine.InputSystem.InputAction Navigate => m_Wrapper.m_UI_Navigate;

		public global::UnityEngine.InputSystem.InputAction Submit => m_Wrapper.m_UI_Submit;

		public global::UnityEngine.InputSystem.InputAction Cancel => m_Wrapper.m_UI_Cancel;

		public global::UnityEngine.InputSystem.InputAction Point => m_Wrapper.m_UI_Point;

		public global::UnityEngine.InputSystem.InputAction Click => m_Wrapper.m_UI_Click;

		public global::UnityEngine.InputSystem.InputAction RightClick => m_Wrapper.m_UI_RightClick;

		public global::UnityEngine.InputSystem.InputAction MiddleClick => m_Wrapper.m_UI_MiddleClick;

		public global::UnityEngine.InputSystem.InputAction ScrollWheel => m_Wrapper.m_UI_ScrollWheel;

		public global::UnityEngine.InputSystem.InputAction TrackedDevicePosition => m_Wrapper.m_UI_TrackedDevicePosition;

		public global::UnityEngine.InputSystem.InputAction TrackedDeviceOrientation => m_Wrapper.m_UI_TrackedDeviceOrientation;

		public bool enabled => Get().enabled;

		public UIActions(NonSingletonActions wrapper)
		{
			m_Wrapper = wrapper;
		}

		public global::UnityEngine.InputSystem.InputActionMap Get()
		{
			return m_Wrapper.m_UI;
		}

		public void Enable()
		{
			Get().Enable();
		}

		public void Disable()
		{
			Get().Disable();
		}

		public static implicit operator global::UnityEngine.InputSystem.InputActionMap(NonSingletonActions.UIActions set)
		{
			return set.Get();
		}

		public void AddCallbacks(NonSingletonActions.IUIActions instance)
		{
			if (instance != null && !m_Wrapper.m_UIActionsCallbackInterfaces.Contains(instance))
			{
				m_Wrapper.m_UIActionsCallbackInterfaces.Add(instance);
				Navigate.started += instance.OnNavigate;
				Navigate.performed += instance.OnNavigate;
				Navigate.canceled += instance.OnNavigate;
				Submit.started += instance.OnSubmit;
				Submit.performed += instance.OnSubmit;
				Submit.canceled += instance.OnSubmit;
				Cancel.started += instance.OnCancel;
				Cancel.performed += instance.OnCancel;
				Cancel.canceled += instance.OnCancel;
				Point.started += instance.OnPoint;
				Point.performed += instance.OnPoint;
				Point.canceled += instance.OnPoint;
				Click.started += instance.OnClick;
				Click.performed += instance.OnClick;
				Click.canceled += instance.OnClick;
				RightClick.started += instance.OnRightClick;
				RightClick.performed += instance.OnRightClick;
				RightClick.canceled += instance.OnRightClick;
				MiddleClick.started += instance.OnMiddleClick;
				MiddleClick.performed += instance.OnMiddleClick;
				MiddleClick.canceled += instance.OnMiddleClick;
				ScrollWheel.started += instance.OnScrollWheel;
				ScrollWheel.performed += instance.OnScrollWheel;
				ScrollWheel.canceled += instance.OnScrollWheel;
				TrackedDevicePosition.started += instance.OnTrackedDevicePosition;
				TrackedDevicePosition.performed += instance.OnTrackedDevicePosition;
				TrackedDevicePosition.canceled += instance.OnTrackedDevicePosition;
				TrackedDeviceOrientation.started += instance.OnTrackedDeviceOrientation;
				TrackedDeviceOrientation.performed += instance.OnTrackedDeviceOrientation;
				TrackedDeviceOrientation.canceled += instance.OnTrackedDeviceOrientation;
			}
		}

		private void UnregisterCallbacks(NonSingletonActions.IUIActions instance)
		{
			Navigate.started -= instance.OnNavigate;
			Navigate.performed -= instance.OnNavigate;
			Navigate.canceled -= instance.OnNavigate;
			Submit.started -= instance.OnSubmit;
			Submit.performed -= instance.OnSubmit;
			Submit.canceled -= instance.OnSubmit;
			Cancel.started -= instance.OnCancel;
			Cancel.performed -= instance.OnCancel;
			Cancel.canceled -= instance.OnCancel;
			Point.started -= instance.OnPoint;
			Point.performed -= instance.OnPoint;
			Point.canceled -= instance.OnPoint;
			Click.started -= instance.OnClick;
			Click.performed -= instance.OnClick;
			Click.canceled -= instance.OnClick;
			RightClick.started -= instance.OnRightClick;
			RightClick.performed -= instance.OnRightClick;
			RightClick.canceled -= instance.OnRightClick;
			MiddleClick.started -= instance.OnMiddleClick;
			MiddleClick.performed -= instance.OnMiddleClick;
			MiddleClick.canceled -= instance.OnMiddleClick;
			ScrollWheel.started -= instance.OnScrollWheel;
			ScrollWheel.performed -= instance.OnScrollWheel;
			ScrollWheel.canceled -= instance.OnScrollWheel;
			TrackedDevicePosition.started -= instance.OnTrackedDevicePosition;
			TrackedDevicePosition.performed -= instance.OnTrackedDevicePosition;
			TrackedDevicePosition.canceled -= instance.OnTrackedDevicePosition;
			TrackedDeviceOrientation.started -= instance.OnTrackedDeviceOrientation;
			TrackedDeviceOrientation.performed -= instance.OnTrackedDeviceOrientation;
			TrackedDeviceOrientation.canceled -= instance.OnTrackedDeviceOrientation;
		}

		public void RemoveCallbacks(NonSingletonActions.IUIActions instance)
		{
			if (m_Wrapper.m_UIActionsCallbackInterfaces.Remove(instance))
			{
				UnregisterCallbacks(instance);
			}
		}

		public void SetCallbacks(NonSingletonActions.IUIActions instance)
		{
			foreach (NonSingletonActions.IUIActions uIActionsCallbackInterface in m_Wrapper.m_UIActionsCallbackInterfaces)
			{
				UnregisterCallbacks(uIActionsCallbackInterface);
			}
			m_Wrapper.m_UIActionsCallbackInterfaces.Clear();
			AddCallbacks(instance);
		}
	}

	public interface IPlayerActions
	{
		void OnMoveUp(global::UnityEngine.InputSystem.InputAction.CallbackContext context);

		void OnMoveDown(global::UnityEngine.InputSystem.InputAction.CallbackContext context);

		void OnMoveRight(global::UnityEngine.InputSystem.InputAction.CallbackContext context);

		void OnMoveLeft(global::UnityEngine.InputSystem.InputAction.CallbackContext context);

		void OnForceUp(global::UnityEngine.InputSystem.InputAction.CallbackContext context);

		void OnSwapBlocks(global::UnityEngine.InputSystem.InputAction.CallbackContext context);

		void OnAltSwapBlocks(global::UnityEngine.InputSystem.InputAction.CallbackContext context);

		void OnPause(global::UnityEngine.InputSystem.InputAction.CallbackContext context);

		void OnSpecialEnter(global::UnityEngine.InputSystem.InputAction.CallbackContext context);
	}

	public interface IUIActions
	{
		void OnNavigate(global::UnityEngine.InputSystem.InputAction.CallbackContext context);

		void OnSubmit(global::UnityEngine.InputSystem.InputAction.CallbackContext context);

		void OnCancel(global::UnityEngine.InputSystem.InputAction.CallbackContext context);

		void OnPoint(global::UnityEngine.InputSystem.InputAction.CallbackContext context);

		void OnClick(global::UnityEngine.InputSystem.InputAction.CallbackContext context);

		void OnRightClick(global::UnityEngine.InputSystem.InputAction.CallbackContext context);

		void OnMiddleClick(global::UnityEngine.InputSystem.InputAction.CallbackContext context);

		void OnScrollWheel(global::UnityEngine.InputSystem.InputAction.CallbackContext context);

		void OnTrackedDevicePosition(global::UnityEngine.InputSystem.InputAction.CallbackContext context);

		void OnTrackedDeviceOrientation(global::UnityEngine.InputSystem.InputAction.CallbackContext context);
	}

	private readonly global::UnityEngine.InputSystem.InputActionMap m_Player;

	private global::System.Collections.Generic.List<NonSingletonActions.IPlayerActions> m_PlayerActionsCallbackInterfaces = new global::System.Collections.Generic.List<NonSingletonActions.IPlayerActions>();

	private readonly global::UnityEngine.InputSystem.InputAction m_Player_MoveUp;

	private readonly global::UnityEngine.InputSystem.InputAction m_Player_MoveDown;

	private readonly global::UnityEngine.InputSystem.InputAction m_Player_MoveRight;

	private readonly global::UnityEngine.InputSystem.InputAction m_Player_MoveLeft;

	private readonly global::UnityEngine.InputSystem.InputAction m_Player_ForceUp;

	private readonly global::UnityEngine.InputSystem.InputAction m_Player_SwapBlocks;

	private readonly global::UnityEngine.InputSystem.InputAction m_Player_AltSwapBlocks;

	private readonly global::UnityEngine.InputSystem.InputAction m_Player_Pause;

	private readonly global::UnityEngine.InputSystem.InputAction m_Player_SpecialEnter;

	private readonly global::UnityEngine.InputSystem.InputActionMap m_UI;

	private global::System.Collections.Generic.List<NonSingletonActions.IUIActions> m_UIActionsCallbackInterfaces = new global::System.Collections.Generic.List<NonSingletonActions.IUIActions>();

	private readonly global::UnityEngine.InputSystem.InputAction m_UI_Navigate;

	private readonly global::UnityEngine.InputSystem.InputAction m_UI_Submit;

	private readonly global::UnityEngine.InputSystem.InputAction m_UI_Cancel;

	private readonly global::UnityEngine.InputSystem.InputAction m_UI_Point;

	private readonly global::UnityEngine.InputSystem.InputAction m_UI_Click;

	private readonly global::UnityEngine.InputSystem.InputAction m_UI_RightClick;

	private readonly global::UnityEngine.InputSystem.InputAction m_UI_MiddleClick;

	private readonly global::UnityEngine.InputSystem.InputAction m_UI_ScrollWheel;

	private readonly global::UnityEngine.InputSystem.InputAction m_UI_TrackedDevicePosition;

	private readonly global::UnityEngine.InputSystem.InputAction m_UI_TrackedDeviceOrientation;

	private int m_KeyboardMouseSchemeIndex = -1;

	private int m_GamepadSchemeIndex = -1;

	private int m_TouchSchemeIndex = -1;

	private int m_JoystickSchemeIndex = -1;

	private int m_XRSchemeIndex = -1;

	public global::UnityEngine.InputSystem.InputActionAsset asset { get; }

	public global::UnityEngine.InputSystem.InputBinding? bindingMask
	{
		get
		{
			return asset.bindingMask;
		}
		set
		{
			asset.bindingMask = value;
		}
	}

	public global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.InputDevice>? devices
	{
		get
		{
			return asset.devices;
		}
		set
		{
			asset.devices = value;
		}
	}

	public global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.InputControlScheme> controlSchemes => asset.controlSchemes;

	public global::System.Collections.Generic.IEnumerable<global::UnityEngine.InputSystem.InputBinding> bindings => asset.bindings;

	public NonSingletonActions.PlayerActions Player => new NonSingletonActions.PlayerActions(this);

	public NonSingletonActions.UIActions UI => new NonSingletonActions.UIActions(this);

	public global::UnityEngine.InputSystem.InputControlScheme KeyboardMouseScheme
	{
		get
		{
			if (m_KeyboardMouseSchemeIndex == -1)
			{
				m_KeyboardMouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard&Mouse");
			}
			return asset.controlSchemes[m_KeyboardMouseSchemeIndex];
		}
	}

	public global::UnityEngine.InputSystem.InputControlScheme GamepadScheme
	{
		get
		{
			if (m_GamepadSchemeIndex == -1)
			{
				m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
			}
			return asset.controlSchemes[m_GamepadSchemeIndex];
		}
	}

	public global::UnityEngine.InputSystem.InputControlScheme TouchScheme
	{
		get
		{
			if (m_TouchSchemeIndex == -1)
			{
				m_TouchSchemeIndex = asset.FindControlSchemeIndex("Touch");
			}
			return asset.controlSchemes[m_TouchSchemeIndex];
		}
	}

	public global::UnityEngine.InputSystem.InputControlScheme JoystickScheme
	{
		get
		{
			if (m_JoystickSchemeIndex == -1)
			{
				m_JoystickSchemeIndex = asset.FindControlSchemeIndex("Joystick");
			}
			return asset.controlSchemes[m_JoystickSchemeIndex];
		}
	}

	public global::UnityEngine.InputSystem.InputControlScheme XRScheme
	{
		get
		{
			if (m_XRSchemeIndex == -1)
			{
				m_XRSchemeIndex = asset.FindControlSchemeIndex("XR");
			}
			return asset.controlSchemes[m_XRSchemeIndex];
		}
	}

	public NonSingletonActions()
	{
		asset = global::UnityEngine.InputSystem.InputActionAsset.FromJson("{\n    \"version\": 1,\n    \"name\": \"NonSingletonActions\",\n    \"maps\": [\n        {\n            \"name\": \"Player\",\n            \"id\": \"df70fa95-8a34-4494-b137-73ab6b9c7d37\",\n            \"actions\": [\n                {\n                    \"name\": \"Move Up\",\n                    \"type\": \"Button\",\n                    \"id\": \"351f2ccd-1f9f-44bf-9bec-d62ac5c5f408\",\n                    \"expectedControlType\": \"\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": true\n                },\n                {\n                    \"name\": \"Move Down\",\n                    \"type\": \"Button\",\n                    \"id\": \"7f71f6cb-202d-4b8c-bd91-fb95cc726a0f\",\n                    \"expectedControlType\": \"\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": true\n                },\n                {\n                    \"name\": \"Move Right\",\n                    \"type\": \"Button\",\n                    \"id\": \"36684392-fb97-4412-a715-7a9cf51396f6\",\n                    \"expectedControlType\": \"\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": true\n                },\n                {\n                    \"name\": \"Move Left\",\n                    \"type\": \"Button\",\n                    \"id\": \"03827e9c-0d04-4b81-98e8-ed1643401120\",\n                    \"expectedControlType\": \"\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": true\n                },\n                {\n                    \"name\": \"ForceUp\",\n                    \"type\": \"Button\",\n                    \"id\": \"852140f2-7766-474d-8707-702459ba45f3\",\n                    \"expectedControlType\": \"\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"SwapBlocks\",\n                    \"type\": \"Button\",\n                    \"id\": \"f1ba0d36-48eb-4cd5-b651-1c94a6531f70\",\n                    \"expectedControlType\": \"\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"AltSwapBlocks\",\n                    \"type\": \"Button\",\n                    \"id\": \"1fe825ed-6015-4232-a4df-18a4c6367034\",\n                    \"expectedControlType\": \"\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"Pause\",\n                    \"type\": \"Button\",\n                    \"id\": \"41ae8c6a-c013-44c3-a0e7-6016421eb60f\",\n                    \"expectedControlType\": \"\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"SpecialEnter\",\n                    \"type\": \"Button\",\n                    \"id\": \"ffafd939-6bbf-4f2f-9f07-b1c6dc2384ac\",\n                    \"expectedControlType\": \"\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                }\n            ],\n            \"bindings\": [\n                {\n                    \"name\": \"WASD\",\n                    \"id\": \"00ca640b-d935-4593-8157-c05846ea39b3\",\n                    \"path\": \"Dpad\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"Move Up\",\n                    \"isComposite\": true,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"up\",\n                    \"id\": \"8180e8bd-4097-4f4e-ab88-4523101a6ce9\",\n                    \"path\": \"<Keyboard>/upArrow\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Keyboard&Mouse\",\n                    \"action\": \"Move Up\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"DPAD\",\n                    \"id\": \"1211ac0e-78dc-40d3-a9fa-93d4c35112fe\",\n                    \"path\": \"Dpad\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"Move Up\",\n                    \"isComposite\": true,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"up\",\n                    \"id\": \"b01ecf7b-471b-4e38-b85a-ed8ecff951f4\",\n                    \"path\": \"<Gamepad>/dpad/up\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Gamepad\",\n                    \"action\": \"Move Up\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"WASD\",\n                    \"id\": \"1811de8f-d447-481c-9e06-4b67cba55b7d\",\n                    \"path\": \"Dpad\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"Move Down\",\n                    \"isComposite\": true,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"Down\",\n                    \"id\": \"f16b5a99-6b72-4a10-9dd9-5e0402653c78\",\n                    \"path\": \"<Keyboard>/downArrow\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Keyboard&Mouse\",\n                    \"action\": \"Move Down\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"DPAD\",\n                    \"id\": \"9d958723-b80b-43fd-82dd-78abb06fa2cf\",\n                    \"path\": \"Dpad\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"Move Down\",\n                    \"isComposite\": true,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"Down\",\n                    \"id\": \"9c23a639-c173-4a90-b7a6-cf4e33a08fe3\",\n                    \"path\": \"<Gamepad>/dpad/down\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Gamepad\",\n                    \"action\": \"Move Down\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"WASD\",\n                    \"id\": \"8bf5046b-0e74-4c19-afcd-1722704072fb\",\n                    \"path\": \"Dpad\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"Move Right\",\n                    \"isComposite\": true,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"Right\",\n                    \"id\": \"b237b67f-9e07-43e8-ad0b-dc059eaef324\",\n                    \"path\": \"<Keyboard>/rightArrow\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Keyboard&Mouse\",\n                    \"action\": \"Move Right\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"DPAD\",\n                    \"id\": \"af4780f6-e821-490f-bc32-30cdc2f3500d\",\n                    \"path\": \"Dpad\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"Move Right\",\n                    \"isComposite\": true,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"Right\",\n                    \"id\": \"c5e71d8e-14dd-46da-acef-37c491d59687\",\n                    \"path\": \"<Gamepad>/dpad/right\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Gamepad\",\n                    \"action\": \"Move Right\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"WASD\",\n                    \"id\": \"44185e5a-5057-4375-882d-4348f12f652d\",\n                    \"path\": \"Dpad\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"Move Left\",\n                    \"isComposite\": true,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"Left\",\n                    \"id\": \"b4d56289-ffc5-4b99-8186-14112c68799f\",\n                    \"path\": \"<Keyboard>/leftArrow\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Keyboard&Mouse\",\n                    \"action\": \"Move Left\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"DPAD\",\n                    \"id\": \"382c2628-2d75-4737-8876-9a6a6e70b8b1\",\n                    \"path\": \"Dpad\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"Move Left\",\n                    \"isComposite\": true,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"Left\",\n                    \"id\": \"046f2442-9cd2-4c4d-8849-aa0e41fc9ef6\",\n                    \"path\": \"<Gamepad>/dpad/left\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Gamepad\",\n                    \"action\": \"Move Left\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"eb40bb66-4559-4dfa-9a2f-820438abb426\",\n                    \"path\": \"<Keyboard>/s\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Keyboard&Mouse\",\n                    \"action\": \"SwapBlocks\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"98e9a949-17b8-44ce-b58d-79207fdda707\",\n                    \"path\": \"<Gamepad>/buttonSouth\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Gamepad\",\n                    \"action\": \"SwapBlocks\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"006d553e-779e-4f49-b722-ef9fbd6a4e07\",\n                    \"path\": \"<Keyboard>/d\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Keyboard&Mouse\",\n                    \"action\": \"AltSwapBlocks\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"33ed26ff-aa84-4937-83ad-499ac57a186c\",\n                    \"path\": \"<Gamepad>/buttonWest\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Gamepad\",\n                    \"action\": \"AltSwapBlocks\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"1c04ea5f-b012-41d1-a6f7-02e963b52893\",\n                    \"path\": \"<Keyboard>/f\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Keyboard&Mouse\",\n                    \"action\": \"ForceUp\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"8314bc0a-bf6e-44f6-b6b0-7eedeae7b732\",\n                    \"path\": \"<Gamepad>/rightTrigger\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Gamepad\",\n                    \"action\": \"ForceUp\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"ce06c6d7-10b9-40f8-b9e4-d13676ca6b7a\",\n                    \"path\": \"<Gamepad>/leftTrigger\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Gamepad\",\n                    \"action\": \"ForceUp\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"68cff5d4-d4a8-40ad-9124-5769c85975c7\",\n                    \"path\": \"<Gamepad>/rightShoulder\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Gamepad\",\n                    \"action\": \"ForceUp\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"38539a32-d55e-4de3-bfcf-2227fa06f1fd\",\n                    \"path\": \"<Gamepad>/leftShoulder\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Gamepad\",\n                    \"action\": \"ForceUp\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"41a5c548-347f-4898-a96d-15d8d97f810d\",\n                    \"path\": \"<Keyboard>/escape\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Keyboard&Mouse\",\n                    \"action\": \"Pause\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"d36a8a8a-1b05-4737-be7c-fe51b1fca52e\",\n                    \"path\": \"<Keyboard>/backspace\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Keyboard&Mouse\",\n                    \"action\": \"Pause\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"15219550-60ae-479e-8ee7-7adec7fb0f93\",\n                    \"path\": \"<Gamepad>/start\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Gamepad\",\n                    \"action\": \"Pause\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"e667148d-a99a-42e3-a9d5-944f38ae8715\",\n                    \"path\": \"<Gamepad>/select\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Gamepad\",\n                    \"action\": \"Pause\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"7e690ae8-97ae-4f72-b2cd-7d4edc1b2357\",\n                    \"path\": \"<Keyboard>/enter\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Keyboard&Mouse\",\n                    \"action\": \"SpecialEnter\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                }\n            ]\n        },\n        {\n            \"name\": \"UI\",\n            \"id\": \"272f6d14-89ba-496f-b7ff-215263d3219f\",\n            \"actions\": [\n                {\n                    \"name\": \"Navigate\",\n                    \"type\": \"PassThrough\",\n                    \"id\": \"c95b2375-e6d9-4b88-9c4c-c5e76515df4b\",\n                    \"expectedControlType\": \"Vector2\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"Submit\",\n                    \"type\": \"Button\",\n                    \"id\": \"7607c7b6-cd76-4816-beef-bd0341cfe950\",\n                    \"expectedControlType\": \"\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"Cancel\",\n                    \"type\": \"Button\",\n                    \"id\": \"15cef263-9014-4fd5-94d9-4e4a6234a6ef\",\n                    \"expectedControlType\": \"\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"Point\",\n                    \"type\": \"PassThrough\",\n                    \"id\": \"32b35790-4ed0-4e9a-aa41-69ac6d629449\",\n                    \"expectedControlType\": \"Vector2\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": true\n                },\n                {\n                    \"name\": \"Click\",\n                    \"type\": \"PassThrough\",\n                    \"id\": \"3c7022bf-7922-4f7c-a998-c437916075ad\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": true\n                },\n                {\n                    \"name\": \"RightClick\",\n                    \"type\": \"PassThrough\",\n                    \"id\": \"44b200b1-1557-4083-816c-b22cbdf77ddf\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"MiddleClick\",\n                    \"type\": \"PassThrough\",\n                    \"id\": \"dad70c86-b58c-4b17-88ad-f5e53adf419e\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"ScrollWheel\",\n                    \"type\": \"PassThrough\",\n                    \"id\": \"0489e84a-4833-4c40-bfae-cea84b696689\",\n                    \"expectedControlType\": \"Vector2\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"TrackedDevicePosition\",\n                    \"type\": \"PassThrough\",\n                    \"id\": \"24908448-c609-4bc3-a128-ea258674378a\",\n                    \"expectedControlType\": \"Vector3\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"TrackedDeviceOrientation\",\n                    \"type\": \"PassThrough\",\n                    \"id\": \"9caa3d8a-6b2f-4e8e-8bad-6ede561bd9be\",\n                    \"expectedControlType\": \"Quaternion\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                }\n            ],\n            \"bindings\": [\n                {\n                    \"name\": \"Gamepad\",\n                    \"id\": \"809f371f-c5e2-4e7a-83a1-d867598f40dd\",\n                    \"path\": \"2DVector\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"Navigate\",\n                    \"isComposite\": true,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"up\",\n                    \"id\": \"14a5d6e8-4aaf-4119-a9ef-34b8c2c548bf\",\n                    \"path\": \"<Gamepad>/leftStick/up\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Gamepad\",\n                    \"action\": \"Navigate\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"up\",\n                    \"id\": \"9144cbe6-05e1-4687-a6d7-24f99d23dd81\",\n                    \"path\": \"<Gamepad>/rightStick/up\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Gamepad\",\n                    \"action\": \"Navigate\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"down\",\n                    \"id\": \"2db08d65-c5fb-421b-983f-c71163608d67\",\n                    \"path\": \"<Gamepad>/leftStick/down\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Gamepad\",\n                    \"action\": \"Navigate\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"down\",\n                    \"id\": \"58748904-2ea9-4a80-8579-b500e6a76df8\",\n                    \"path\": \"<Gamepad>/rightStick/down\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Gamepad\",\n                    \"action\": \"Navigate\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"left\",\n                    \"id\": \"8ba04515-75aa-45de-966d-393d9bbd1c14\",\n                    \"path\": \"<Gamepad>/leftStick/left\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Gamepad\",\n                    \"action\": \"Navigate\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"left\",\n                    \"id\": \"712e721c-bdfb-4b23-a86c-a0d9fcfea921\",\n                    \"path\": \"<Gamepad>/rightStick/left\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Gamepad\",\n                    \"action\": \"Navigate\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"right\",\n                    \"id\": \"fcd248ae-a788-4676-a12e-f4d81205600b\",\n                    \"path\": \"<Gamepad>/leftStick/right\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Gamepad\",\n                    \"action\": \"Navigate\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"right\",\n                    \"id\": \"1f04d9bc-c50b-41a1-bfcc-afb75475ec20\",\n                    \"path\": \"<Gamepad>/rightStick/right\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Gamepad\",\n                    \"action\": \"Navigate\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"fb8277d4-c5cd-4663-9dc7-ee3f0b506d90\",\n                    \"path\": \"<Gamepad>/dpad\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Gamepad\",\n                    \"action\": \"Navigate\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"Joystick\",\n                    \"id\": \"e25d9774-381c-4a61-b47c-7b6b299ad9f9\",\n                    \"path\": \"2DVector\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"Navigate\",\n                    \"isComposite\": true,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"up\",\n                    \"id\": \"3db53b26-6601-41be-9887-63ac74e79d19\",\n                    \"path\": \"<Joystick>/stick/up\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Joystick\",\n                    \"action\": \"Navigate\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"down\",\n                    \"id\": \"0cb3e13e-3d90-4178-8ae6-d9c5501d653f\",\n                    \"path\": \"<Joystick>/stick/down\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Joystick\",\n                    \"action\": \"Navigate\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"left\",\n                    \"id\": \"0392d399-f6dd-4c82-8062-c1e9c0d34835\",\n                    \"path\": \"<Joystick>/stick/left\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Joystick\",\n                    \"action\": \"Navigate\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"right\",\n                    \"id\": \"942a66d9-d42f-43d6-8d70-ecb4ba5363bc\",\n                    \"path\": \"<Joystick>/stick/right\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Joystick\",\n                    \"action\": \"Navigate\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"Keyboard\",\n                    \"id\": \"ff527021-f211-4c02-933e-5976594c46ed\",\n                    \"path\": \"2DVector\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"Navigate\",\n                    \"isComposite\": true,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"up\",\n                    \"id\": \"eb480147-c587-4a33-85ed-eb0ab9942c43\",\n                    \"path\": \"<Keyboard>/upArrow\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Keyboard&Mouse\",\n                    \"action\": \"Navigate\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"down\",\n                    \"id\": \"85d264ad-e0a0-4565-b7ff-1a37edde51ac\",\n                    \"path\": \"<Keyboard>/downArrow\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Keyboard&Mouse\",\n                    \"action\": \"Navigate\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"left\",\n                    \"id\": \"cea9b045-a000-445b-95b8-0c171af70a3b\",\n                    \"path\": \"<Keyboard>/leftArrow\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Keyboard&Mouse\",\n                    \"action\": \"Navigate\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"right\",\n                    \"id\": \"4cda81dc-9edd-4e03-9d7c-a71a14345d0b\",\n                    \"path\": \"<Keyboard>/rightArrow\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Keyboard&Mouse\",\n                    \"action\": \"Navigate\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"9e92bb26-7e3b-4ec4-b06b-3c8f8e498ddc\",\n                    \"path\": \"<Keyboard>/space\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Keyboard&Mouse;Gamepad;Touch;Joystick;XR\",\n                    \"action\": \"Submit\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"4ed95d2f-ce68-424f-8190-ee9b03cdd0b7\",\n                    \"path\": \"<Gamepad>/buttonSouth\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Keyboard&Mouse;Gamepad;Touch;Joystick;XR\",\n                    \"action\": \"Submit\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"43e8e758-a8d0-4e78-944e-5edf4c31d447\",\n                    \"path\": \"<Gamepad>/buttonSouth\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Gamepad\",\n                    \"action\": \"Submit\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"58ae1dcb-1ff8-4980-af91-f840394f1915\",\n                    \"path\": \"<Keyboard>/space\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Keyboard&Mouse\",\n                    \"action\": \"Submit\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"737165eb-bf33-40aa-9aeb-71458f77b56d\",\n                    \"path\": \"<Gamepad>/buttonWest\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Gamepad\",\n                    \"action\": \"Submit\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"82627dcc-3b13-4ba9-841d-e4b746d6553e\",\n                    \"path\": \"<Keyboard>/escape\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Keyboard&Mouse;Gamepad;Touch;Joystick;XR\",\n                    \"action\": \"Cancel\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"998b7a1d-ee8e-49a5-9d5f-892ea537de9c\",\n                    \"path\": \"<Gamepad>/buttonWest\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Keyboard&Mouse;Gamepad;Touch;Joystick;XR\",\n                    \"action\": \"Cancel\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"76ab8170-7a42-4158-90f1-1b866def305a\",\n                    \"path\": \"<Gamepad>/buttonEast\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Keyboard&Mouse;Gamepad;Joystick;XR\",\n                    \"action\": \"Cancel\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"eeafb378-19bd-408d-a1ff-302d541f5fea\",\n                    \"path\": \"<Gamepad>/buttonNorth\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Keyboard&Mouse;Gamepad;Touch;Joystick;XR\",\n                    \"action\": \"Cancel\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"c52c8e0b-8179-41d3-b8a1-d149033bbe86\",\n                    \"path\": \"<Mouse>/position\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Keyboard&Mouse\",\n                    \"action\": \"Point\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"e1394cbc-336e-44ce-9ea8-6007ed6193f7\",\n                    \"path\": \"<Pen>/position\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Keyboard&Mouse\",\n                    \"action\": \"Point\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"5693e57a-238a-46ed-b5ae-e64e6e574302\",\n                    \"path\": \"<Touchscreen>/touch*/position\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Touch\",\n                    \"action\": \"Point\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"4faf7dc9-b979-4210-aa8c-e808e1ef89f5\",\n                    \"path\": \"<Mouse>/leftButton\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Keyboard&Mouse\",\n                    \"action\": \"Click\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"8d66d5ba-88d7-48e6-b1cd-198bbfef7ace\",\n                    \"path\": \"<Pen>/tip\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Keyboard&Mouse\",\n                    \"action\": \"Click\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"47c2a644-3ebc-4dae-a106-589b7ca75b59\",\n                    \"path\": \"<Touchscreen>/touch*/press\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Touch\",\n                    \"action\": \"Click\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"bb9e6b34-44bf-4381-ac63-5aa15d19f677\",\n                    \"path\": \"<XRController>/trigger\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"XR\",\n                    \"action\": \"Click\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"38c99815-14ea-4617-8627-164d27641299\",\n                    \"path\": \"<Mouse>/scroll\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Keyboard&Mouse\",\n                    \"action\": \"ScrollWheel\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"4c191405-5738-4d4b-a523-c6a301dbf754\",\n                    \"path\": \"<Mouse>/rightButton\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Keyboard&Mouse\",\n                    \"action\": \"RightClick\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"24066f69-da47-44f3-a07e-0015fb02eb2e\",\n                    \"path\": \"<Mouse>/middleButton\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Keyboard&Mouse\",\n                    \"action\": \"MiddleClick\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"7236c0d9-6ca3-47cf-a6ee-a97f5b59ea77\",\n                    \"path\": \"<XRController>/devicePosition\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"XR\",\n                    \"action\": \"TrackedDevicePosition\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"23e01e3a-f935-4948-8d8b-9bcac77714fb\",\n                    \"path\": \"<XRController>/deviceRotation\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"XR\",\n                    \"action\": \"TrackedDeviceOrientation\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                }\n            ]\n        }\n    ],\n    \"controlSchemes\": [\n        {\n            \"name\": \"Keyboard&Mouse\",\n            \"bindingGroup\": \"Keyboard&Mouse\",\n            \"devices\": [\n                {\n                    \"devicePath\": \"<Keyboard>\",\n                    \"isOptional\": false,\n                    \"isOR\": false\n                },\n                {\n                    \"devicePath\": \"<Mouse>\",\n                    \"isOptional\": false,\n                    \"isOR\": false\n                }\n            ]\n        },\n        {\n            \"name\": \"Gamepad\",\n            \"bindingGroup\": \"Gamepad\",\n            \"devices\": [\n                {\n                    \"devicePath\": \"<Gamepad>\",\n                    \"isOptional\": false,\n                    \"isOR\": false\n                }\n            ]\n        },\n        {\n            \"name\": \"Touch\",\n            \"bindingGroup\": \"Touch\",\n            \"devices\": [\n                {\n                    \"devicePath\": \"<Touchscreen>\",\n                    \"isOptional\": false,\n                    \"isOR\": false\n                }\n            ]\n        },\n        {\n            \"name\": \"Joystick\",\n            \"bindingGroup\": \"Joystick\",\n            \"devices\": [\n                {\n                    \"devicePath\": \"<Joystick>\",\n                    \"isOptional\": false,\n                    \"isOR\": false\n                }\n            ]\n        },\n        {\n            \"name\": \"XR\",\n            \"bindingGroup\": \"XR\",\n            \"devices\": [\n                {\n                    \"devicePath\": \"<XRController>\",\n                    \"isOptional\": false,\n                    \"isOR\": false\n                }\n            ]\n        }\n    ]\n}");
		m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
		m_Player_MoveUp = m_Player.FindAction("Move Up", throwIfNotFound: true);
		m_Player_MoveDown = m_Player.FindAction("Move Down", throwIfNotFound: true);
		m_Player_MoveRight = m_Player.FindAction("Move Right", throwIfNotFound: true);
		m_Player_MoveLeft = m_Player.FindAction("Move Left", throwIfNotFound: true);
		m_Player_ForceUp = m_Player.FindAction("ForceUp", throwIfNotFound: true);
		m_Player_SwapBlocks = m_Player.FindAction("SwapBlocks", throwIfNotFound: true);
		m_Player_AltSwapBlocks = m_Player.FindAction("AltSwapBlocks", throwIfNotFound: true);
		m_Player_Pause = m_Player.FindAction("Pause", throwIfNotFound: true);
		m_Player_SpecialEnter = m_Player.FindAction("SpecialEnter", throwIfNotFound: true);
		m_UI = asset.FindActionMap("UI", throwIfNotFound: true);
		m_UI_Navigate = m_UI.FindAction("Navigate", throwIfNotFound: true);
		m_UI_Submit = m_UI.FindAction("Submit", throwIfNotFound: true);
		m_UI_Cancel = m_UI.FindAction("Cancel", throwIfNotFound: true);
		m_UI_Point = m_UI.FindAction("Point", throwIfNotFound: true);
		m_UI_Click = m_UI.FindAction("Click", throwIfNotFound: true);
		m_UI_RightClick = m_UI.FindAction("RightClick", throwIfNotFound: true);
		m_UI_MiddleClick = m_UI.FindAction("MiddleClick", throwIfNotFound: true);
		m_UI_ScrollWheel = m_UI.FindAction("ScrollWheel", throwIfNotFound: true);
		m_UI_TrackedDevicePosition = m_UI.FindAction("TrackedDevicePosition", throwIfNotFound: true);
		m_UI_TrackedDeviceOrientation = m_UI.FindAction("TrackedDeviceOrientation", throwIfNotFound: true);
	}

	~NonSingletonActions()
	{
	}

	public void Dispose()
	{
		global::UnityEngine.Object.Destroy(asset);
	}

	public bool Contains(global::UnityEngine.InputSystem.InputAction action)
	{
		return asset.Contains(action);
	}

	public global::System.Collections.Generic.IEnumerator<global::UnityEngine.InputSystem.InputAction> GetEnumerator()
	{
		return asset.GetEnumerator();
	}

	global::System.Collections.IEnumerator global::System.Collections.IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}

	public void Enable()
	{
		asset.Enable();
	}

	public void Disable()
	{
		asset.Disable();
	}

	public global::UnityEngine.InputSystem.InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
	{
		return asset.FindAction(actionNameOrId, throwIfNotFound);
	}

	public int FindBinding(global::UnityEngine.InputSystem.InputBinding bindingMask, out global::UnityEngine.InputSystem.InputAction action)
	{
		return asset.FindBinding(bindingMask, out action);
	}
}
