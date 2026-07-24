namespace UnityEngine.InputSystem
{
	[global::UnityEngine.InputSystem.Layouts.InputControlLayout(stateType = typeof(global::UnityEngine.InputSystem.LowLevel.GamepadState), isGenericTypeOfDevice = true)]
	public class Gamepad : global::UnityEngine.InputSystem.InputDevice, global::UnityEngine.InputSystem.Haptics.IDualMotorRumble, global::UnityEngine.InputSystem.Haptics.IHaptics
	{
		private global::UnityEngine.InputSystem.Haptics.DualMotorRumble m_Rumble;

		private static int s_GamepadCount;

		private static global::UnityEngine.InputSystem.Gamepad[] s_Gamepads;

		public global::UnityEngine.InputSystem.Controls.ButtonControl buttonWest { get; protected set; }

		public global::UnityEngine.InputSystem.Controls.ButtonControl buttonNorth { get; protected set; }

		public global::UnityEngine.InputSystem.Controls.ButtonControl buttonSouth { get; protected set; }

		public global::UnityEngine.InputSystem.Controls.ButtonControl buttonEast { get; protected set; }

		public global::UnityEngine.InputSystem.Controls.ButtonControl leftStickButton { get; protected set; }

		public global::UnityEngine.InputSystem.Controls.ButtonControl rightStickButton { get; protected set; }

		public global::UnityEngine.InputSystem.Controls.ButtonControl startButton { get; protected set; }

		public global::UnityEngine.InputSystem.Controls.ButtonControl selectButton { get; protected set; }

		public global::UnityEngine.InputSystem.Controls.DpadControl dpad { get; protected set; }

		public global::UnityEngine.InputSystem.Controls.ButtonControl leftShoulder { get; protected set; }

		public global::UnityEngine.InputSystem.Controls.ButtonControl rightShoulder { get; protected set; }

		public global::UnityEngine.InputSystem.Controls.StickControl leftStick { get; protected set; }

		public global::UnityEngine.InputSystem.Controls.StickControl rightStick { get; protected set; }

		public global::UnityEngine.InputSystem.Controls.ButtonControl leftTrigger { get; protected set; }

		public global::UnityEngine.InputSystem.Controls.ButtonControl rightTrigger { get; protected set; }

		public global::UnityEngine.InputSystem.Controls.ButtonControl aButton => buttonSouth;

		public global::UnityEngine.InputSystem.Controls.ButtonControl bButton => buttonEast;

		public global::UnityEngine.InputSystem.Controls.ButtonControl xButton => buttonWest;

		public global::UnityEngine.InputSystem.Controls.ButtonControl yButton => buttonNorth;

		public global::UnityEngine.InputSystem.Controls.ButtonControl triangleButton => buttonNorth;

		public global::UnityEngine.InputSystem.Controls.ButtonControl squareButton => buttonWest;

		public global::UnityEngine.InputSystem.Controls.ButtonControl circleButton => buttonEast;

		public global::UnityEngine.InputSystem.Controls.ButtonControl crossButton => buttonSouth;

		public global::UnityEngine.InputSystem.Controls.ButtonControl this[global::UnityEngine.InputSystem.LowLevel.GamepadButton button] => button switch
		{
			global::UnityEngine.InputSystem.LowLevel.GamepadButton.North => buttonNorth, 
			global::UnityEngine.InputSystem.LowLevel.GamepadButton.South => buttonSouth, 
			global::UnityEngine.InputSystem.LowLevel.GamepadButton.East => buttonEast, 
			global::UnityEngine.InputSystem.LowLevel.GamepadButton.West => buttonWest, 
			global::UnityEngine.InputSystem.LowLevel.GamepadButton.Start => startButton, 
			global::UnityEngine.InputSystem.LowLevel.GamepadButton.Select => selectButton, 
			global::UnityEngine.InputSystem.LowLevel.GamepadButton.LeftShoulder => leftShoulder, 
			global::UnityEngine.InputSystem.LowLevel.GamepadButton.RightShoulder => rightShoulder, 
			global::UnityEngine.InputSystem.LowLevel.GamepadButton.LeftTrigger => leftTrigger, 
			global::UnityEngine.InputSystem.LowLevel.GamepadButton.RightTrigger => rightTrigger, 
			global::UnityEngine.InputSystem.LowLevel.GamepadButton.LeftStick => leftStickButton, 
			global::UnityEngine.InputSystem.LowLevel.GamepadButton.RightStick => rightStickButton, 
			global::UnityEngine.InputSystem.LowLevel.GamepadButton.DpadUp => dpad.up, 
			global::UnityEngine.InputSystem.LowLevel.GamepadButton.DpadDown => dpad.down, 
			global::UnityEngine.InputSystem.LowLevel.GamepadButton.DpadLeft => dpad.left, 
			global::UnityEngine.InputSystem.LowLevel.GamepadButton.DpadRight => dpad.right, 
			_ => throw new global::System.ComponentModel.InvalidEnumArgumentException("button", (int)button, typeof(global::UnityEngine.InputSystem.LowLevel.GamepadButton)), 
		};

		public static global::UnityEngine.InputSystem.Gamepad current { get; private set; }

		public new static global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.Gamepad> all => new global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.Gamepad>(s_Gamepads, 0, s_GamepadCount);

		protected override void FinishSetup()
		{
			buttonWest = GetChildControl<global::UnityEngine.InputSystem.Controls.ButtonControl>("buttonWest");
			buttonNorth = GetChildControl<global::UnityEngine.InputSystem.Controls.ButtonControl>("buttonNorth");
			buttonSouth = GetChildControl<global::UnityEngine.InputSystem.Controls.ButtonControl>("buttonSouth");
			buttonEast = GetChildControl<global::UnityEngine.InputSystem.Controls.ButtonControl>("buttonEast");
			startButton = GetChildControl<global::UnityEngine.InputSystem.Controls.ButtonControl>("start");
			selectButton = GetChildControl<global::UnityEngine.InputSystem.Controls.ButtonControl>("select");
			leftStickButton = GetChildControl<global::UnityEngine.InputSystem.Controls.ButtonControl>("leftStickPress");
			rightStickButton = GetChildControl<global::UnityEngine.InputSystem.Controls.ButtonControl>("rightStickPress");
			dpad = GetChildControl<global::UnityEngine.InputSystem.Controls.DpadControl>("dpad");
			leftShoulder = GetChildControl<global::UnityEngine.InputSystem.Controls.ButtonControl>("leftShoulder");
			rightShoulder = GetChildControl<global::UnityEngine.InputSystem.Controls.ButtonControl>("rightShoulder");
			leftStick = GetChildControl<global::UnityEngine.InputSystem.Controls.StickControl>("leftStick");
			rightStick = GetChildControl<global::UnityEngine.InputSystem.Controls.StickControl>("rightStick");
			leftTrigger = GetChildControl<global::UnityEngine.InputSystem.Controls.ButtonControl>("leftTrigger");
			rightTrigger = GetChildControl<global::UnityEngine.InputSystem.Controls.ButtonControl>("rightTrigger");
			base.FinishSetup();
		}

		public override void MakeCurrent()
		{
			base.MakeCurrent();
			current = this;
		}

		protected override void OnAdded()
		{
			global::UnityEngine.InputSystem.Utilities.ArrayHelpers.AppendWithCapacity(ref s_Gamepads, ref s_GamepadCount, this);
		}

		protected override void OnRemoved()
		{
			if (current == this)
			{
				current = null;
			}
			int num = global::UnityEngine.InputSystem.Utilities.ArrayHelpers.IndexOfReference(s_Gamepads, this, s_GamepadCount);
			if (num != -1)
			{
				global::UnityEngine.InputSystem.Utilities.ArrayHelpers.EraseAtWithCapacity(s_Gamepads, ref s_GamepadCount, num);
			}
		}

		public virtual void PauseHaptics()
		{
			m_Rumble.PauseHaptics(this);
		}

		public virtual void ResumeHaptics()
		{
			m_Rumble.ResumeHaptics(this);
		}

		public virtual void ResetHaptics()
		{
			m_Rumble.ResetHaptics(this);
		}

		public virtual void SetMotorSpeeds(float lowFrequency, float highFrequency)
		{
			m_Rumble.SetMotorSpeeds(this, lowFrequency, highFrequency);
		}
	}
}
