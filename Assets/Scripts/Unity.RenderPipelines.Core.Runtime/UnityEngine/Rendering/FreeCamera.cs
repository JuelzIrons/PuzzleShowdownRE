namespace UnityEngine.Rendering
{
	public class FreeCamera : global::UnityEngine.MonoBehaviour
	{
		private const float k_MouseSensitivityMultiplier = 0.01f;

		public float m_LookSpeedController = 120f;

		public float m_LookSpeedMouse = 4f;

		public float m_MoveSpeed = 10f;

		public float m_MoveSpeedIncrement = 2.5f;

		public float m_Turbo = 10f;

		private global::UnityEngine.InputSystem.InputAction lookAction;

		private global::UnityEngine.InputSystem.InputAction moveAction;

		private global::UnityEngine.InputSystem.InputAction speedAction;

		private global::UnityEngine.InputSystem.InputAction yMoveAction;

		private float inputRotateAxisX;

		private float inputRotateAxisY;

		private float inputChangeSpeed;

		private float inputVertical;

		private float inputHorizontal;

		private float inputYAxis;

		private bool leftShiftBoost;

		private bool leftShift;

		private bool fire1;

		private void OnEnable()
		{
			RegisterInputs();
		}

		private void RegisterInputs()
		{
			global::UnityEngine.InputSystem.InputActionMap map = new global::UnityEngine.InputSystem.InputActionMap("Free Camera");
			lookAction = global::UnityEngine.InputSystem.InputActionSetupExtensions.AddAction(map, "look", global::UnityEngine.InputSystem.InputActionType.Value, "<Mouse>/delta");
			moveAction = global::UnityEngine.InputSystem.InputActionSetupExtensions.AddAction(map, "move", global::UnityEngine.InputSystem.InputActionType.Value, "<Gamepad>/leftStick");
			speedAction = global::UnityEngine.InputSystem.InputActionSetupExtensions.AddAction(map, "speed", global::UnityEngine.InputSystem.InputActionType.Value, "<Gamepad>/dpad");
			yMoveAction = global::UnityEngine.InputSystem.InputActionSetupExtensions.AddAction(map, "yMove");
			global::UnityEngine.InputSystem.InputActionSetupExtensions.AddBinding(lookAction, "<Gamepad>/rightStick").WithProcessor("scaleVector2(x=15, y=15)");
			global::UnityEngine.InputSystem.InputActionSetupExtensions.AddCompositeBinding(moveAction, "Dpad").With("Up", "<Keyboard>/w").With("Up", "<Keyboard>/upArrow")
				.With("Down", "<Keyboard>/s")
				.With("Down", "<Keyboard>/downArrow")
				.With("Left", "<Keyboard>/a")
				.With("Left", "<Keyboard>/leftArrow")
				.With("Right", "<Keyboard>/d")
				.With("Right", "<Keyboard>/rightArrow");
			global::UnityEngine.InputSystem.InputActionSetupExtensions.AddCompositeBinding(speedAction, "Dpad").With("Up", "<Keyboard>/home").With("Down", "<Keyboard>/end");
			global::UnityEngine.InputSystem.InputActionSetupExtensions.AddCompositeBinding(yMoveAction, "Dpad").With("Up", "<Keyboard>/pageUp").With("Down", "<Keyboard>/pageDown")
				.With("Up", "<Keyboard>/e")
				.With("Down", "<Keyboard>/q")
				.With("Up", "<Gamepad>/rightshoulder")
				.With("Down", "<Gamepad>/leftshoulder");
			moveAction.Enable();
			lookAction.Enable();
			speedAction.Enable();
			yMoveAction.Enable();
		}

		private void UpdateInputs()
		{
			inputRotateAxisX = 0f;
			inputRotateAxisY = 0f;
			leftShiftBoost = false;
			fire1 = false;
			global::UnityEngine.Vector2 vector = lookAction.ReadValue<global::UnityEngine.Vector2>();
			inputRotateAxisX = vector.x * m_LookSpeedMouse * 0.01f;
			inputRotateAxisY = vector.y * m_LookSpeedMouse * 0.01f;
			leftShift = global::UnityEngine.InputSystem.Keyboard.current?.leftShiftKey?.isPressed == true;
			global::UnityEngine.InputSystem.Mouse current = global::UnityEngine.InputSystem.Mouse.current;
			int num;
			if (current == null || current.leftButton?.isPressed != true)
			{
				global::UnityEngine.InputSystem.Gamepad current2 = global::UnityEngine.InputSystem.Gamepad.current;
				num = ((current2 != null && current2.xButton?.isPressed == true) ? 1 : 0);
			}
			else
			{
				num = 1;
			}
			fire1 = (byte)num != 0;
			inputChangeSpeed = speedAction.ReadValue<global::UnityEngine.Vector2>().y;
			global::UnityEngine.Vector2 vector2 = moveAction.ReadValue<global::UnityEngine.Vector2>();
			inputVertical = vector2.y;
			inputHorizontal = vector2.x;
			inputYAxis = yMoveAction.ReadValue<global::UnityEngine.Vector2>().y;
		}

		private void Update()
		{
			if (global::UnityEngine.Rendering.DebugManager.instance.displayRuntimeUI)
			{
				return;
			}
			UpdateInputs();
			if (inputChangeSpeed != 0f)
			{
				m_MoveSpeed += inputChangeSpeed * m_MoveSpeedIncrement;
				if (m_MoveSpeed < m_MoveSpeedIncrement)
				{
					m_MoveSpeed = m_MoveSpeedIncrement;
				}
			}
			if (inputRotateAxisX != 0f || inputRotateAxisY != 0f || inputVertical != 0f || inputHorizontal != 0f || inputYAxis != 0f)
			{
				float x = base.transform.localEulerAngles.x;
				float y = base.transform.localEulerAngles.y + inputRotateAxisX;
				float num = x - inputRotateAxisY;
				if (x <= 90f && num >= 0f)
				{
					num = global::UnityEngine.Mathf.Clamp(num, 0f, 90f);
				}
				if (x >= 270f)
				{
					num = global::UnityEngine.Mathf.Clamp(num, 270f, 360f);
				}
				base.transform.localRotation = global::UnityEngine.Quaternion.Euler(num, y, base.transform.localEulerAngles.z);
				float num2 = global::UnityEngine.Time.deltaTime * m_MoveSpeed;
				if (fire1 || (leftShiftBoost && leftShift))
				{
					num2 *= m_Turbo;
				}
				base.transform.position += base.transform.forward * (num2 * inputVertical) + base.transform.right * (num2 * inputHorizontal) + global::UnityEngine.Vector3.up * (num2 * inputYAxis);
			}
		}
	}
}
