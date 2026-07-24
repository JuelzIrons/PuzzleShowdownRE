namespace UnityEngine.InputSystem.XInput.LowLevel
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Explicit, Size = 4)]
	internal struct XInputControllerWindowsState : global::UnityEngine.InputSystem.LowLevel.IInputStateTypeInfo
	{
		public enum Button
		{
			DPadUp = 0,
			DPadDown = 1,
			DPadLeft = 2,
			DPadRight = 3,
			Start = 4,
			Select = 5,
			LeftThumbstickPress = 6,
			RightThumbstickPress = 7,
			LeftShoulder = 8,
			RightShoulder = 9,
			A = 12,
			B = 13,
			X = 14,
			Y = 15
		}

		[global::System.Runtime.InteropServices.FieldOffset(0)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "dpad", layout = "Dpad", sizeInBits = 4u, bit = 0u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "dpad/up", bit = 0u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "dpad/down", bit = 1u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "dpad/left", bit = 2u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "dpad/right", bit = 3u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "start", bit = 4u, displayName = "Start")]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "select", bit = 5u, displayName = "Select")]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "leftStickPress", bit = 6u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "rightStickPress", bit = 7u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "leftShoulder", bit = 8u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "rightShoulder", bit = 9u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "buttonSouth", bit = 12u, displayName = "A")]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "buttonEast", bit = 13u, displayName = "B")]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "buttonWest", bit = 14u, displayName = "X")]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "buttonNorth", bit = 15u, displayName = "Y")]
		public ushort buttons;

		[global::System.Runtime.InteropServices.FieldOffset(2)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "leftTrigger", format = "BYTE")]
		public byte leftTrigger;

		[global::System.Runtime.InteropServices.FieldOffset(3)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "rightTrigger", format = "BYTE")]
		public byte rightTrigger;

		[global::System.Runtime.InteropServices.FieldOffset(4)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "leftStick", layout = "Stick", format = "VC2S")]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "leftStick/x", offset = 0u, format = "SHRT", parameters = "clamp=false,invert=false,normalize=false")]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "leftStick/left", offset = 0u, format = "SHRT")]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "leftStick/right", offset = 0u, format = "SHRT")]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "leftStick/y", offset = 2u, format = "SHRT", parameters = "clamp=false,invert=false,normalize=false")]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "leftStick/up", offset = 2u, format = "SHRT")]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "leftStick/down", offset = 2u, format = "SHRT")]
		public short leftStickX;

		[global::System.Runtime.InteropServices.FieldOffset(6)]
		public short leftStickY;

		[global::System.Runtime.InteropServices.FieldOffset(8)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "rightStick", layout = "Stick", format = "VC2S")]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "rightStick/x", offset = 0u, format = "SHRT", parameters = "clamp=false,invert=false,normalize=false")]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "rightStick/left", offset = 0u, format = "SHRT")]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "rightStick/right", offset = 0u, format = "SHRT")]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "rightStick/y", offset = 2u, format = "SHRT", parameters = "clamp=false,invert=false,normalize=false")]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "rightStick/up", offset = 2u, format = "SHRT")]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "rightStick/down", offset = 2u, format = "SHRT")]
		public short rightStickX;

		[global::System.Runtime.InteropServices.FieldOffset(10)]
		public short rightStickY;

		public global::UnityEngine.InputSystem.Utilities.FourCC format => new global::UnityEngine.InputSystem.Utilities.FourCC('X', 'I', 'N', 'P');

		public global::UnityEngine.InputSystem.XInput.LowLevel.XInputControllerWindowsState WithButton(global::UnityEngine.InputSystem.XInput.LowLevel.XInputControllerWindowsState.Button button)
		{
			buttons |= (ushort)(1 << (int)button);
			return this;
		}
	}
}
