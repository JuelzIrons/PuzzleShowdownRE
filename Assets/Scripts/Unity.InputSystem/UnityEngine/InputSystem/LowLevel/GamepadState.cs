namespace UnityEngine.InputSystem.LowLevel
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Explicit, Size = 28)]
	public struct GamepadState : global::UnityEngine.InputSystem.LowLevel.IInputStateTypeInfo
	{
		internal const string ButtonSouthShortDisplayName = "A";

		internal const string ButtonNorthShortDisplayName = "Y";

		internal const string ButtonWestShortDisplayName = "X";

		internal const string ButtonEastShortDisplayName = "B";

		[global::System.Runtime.InteropServices.FieldOffset(0)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "dpad", layout = "Dpad", usage = "Hatswitch", displayName = "D-Pad", format = "BIT", sizeInBits = 4u, bit = 0u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "buttonSouth", layout = "Button", bit = 6u, usages = new string[] { "PrimaryAction", "Submit" }, aliases = new string[] { "a", "cross" }, displayName = "Button South", shortDisplayName = "A")]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "buttonWest", layout = "Button", bit = 7u, usage = "SecondaryAction", aliases = new string[] { "x", "square" }, displayName = "Button West", shortDisplayName = "X")]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "buttonNorth", layout = "Button", bit = 4u, aliases = new string[] { "y", "triangle" }, displayName = "Button North", shortDisplayName = "Y")]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "buttonEast", layout = "Button", bit = 5u, usages = new string[] { "Back", "Cancel" }, aliases = new string[] { "b", "circle" }, displayName = "Button East", shortDisplayName = "B")]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "leftStickPress", layout = "Button", bit = 8u, displayName = "Left Stick Press")]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "rightStickPress", layout = "Button", bit = 9u, displayName = "Right Stick Press")]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "leftShoulder", layout = "Button", bit = 10u, displayName = "Left Shoulder", shortDisplayName = "LB")]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "rightShoulder", layout = "Button", bit = 11u, displayName = "Right Shoulder", shortDisplayName = "RB")]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "start", layout = "Button", bit = 12u, usage = "Menu", displayName = "Start")]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "select", layout = "Button", bit = 13u, displayName = "Select")]
		public uint buttons;

		[global::System.Runtime.InteropServices.FieldOffset(4)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(layout = "Stick", usage = "Primary2DMotion", processors = "stickDeadzone", displayName = "Left Stick", shortDisplayName = "LS")]
		public global::UnityEngine.Vector2 leftStick;

		[global::System.Runtime.InteropServices.FieldOffset(12)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(layout = "Stick", usage = "Secondary2DMotion", processors = "stickDeadzone", displayName = "Right Stick", shortDisplayName = "RS")]
		public global::UnityEngine.Vector2 rightStick;

		[global::System.Runtime.InteropServices.FieldOffset(20)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(layout = "Button", format = "FLT", usage = "SecondaryTrigger", displayName = "Left Trigger", shortDisplayName = "LT")]
		public float leftTrigger;

		[global::System.Runtime.InteropServices.FieldOffset(24)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(layout = "Button", format = "FLT", usage = "SecondaryTrigger", displayName = "Right Trigger", shortDisplayName = "RT")]
		public float rightTrigger;

		public static global::UnityEngine.InputSystem.Utilities.FourCC Format => new global::UnityEngine.InputSystem.Utilities.FourCC('G', 'P', 'A', 'D');

		public global::UnityEngine.InputSystem.Utilities.FourCC format => Format;

		public GamepadState(params global::UnityEngine.InputSystem.LowLevel.GamepadButton[] buttons)
		{
			this = default(global::UnityEngine.InputSystem.LowLevel.GamepadState);
			if (buttons == null)
			{
				throw new global::System.ArgumentNullException("buttons");
			}
			foreach (global::UnityEngine.InputSystem.LowLevel.GamepadButton gamepadButton in buttons)
			{
				uint num = (uint)(1 << (int)gamepadButton);
				this.buttons |= num;
			}
		}

		public global::UnityEngine.InputSystem.LowLevel.GamepadState WithButton(global::UnityEngine.InputSystem.LowLevel.GamepadButton button, bool value = true)
		{
			uint num = (uint)(1 << (int)button);
			if (value)
			{
				buttons |= num;
			}
			else
			{
				buttons &= ~num;
			}
			return this;
		}
	}
}
