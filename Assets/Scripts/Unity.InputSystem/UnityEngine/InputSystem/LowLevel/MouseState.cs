namespace UnityEngine.InputSystem.LowLevel
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Explicit, Size = 30)]
	public struct MouseState : global::UnityEngine.InputSystem.LowLevel.IInputStateTypeInfo
	{
		[global::System.Runtime.InteropServices.FieldOffset(0)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(usage = "Point", dontReset = true)]
		public global::UnityEngine.Vector2 position;

		[global::System.Runtime.InteropServices.FieldOffset(8)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(usage = "Secondary2DMotion", layout = "Delta")]
		public global::UnityEngine.Vector2 delta;

		[global::System.Runtime.InteropServices.FieldOffset(16)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(displayName = "Scroll", layout = "Delta")]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "scroll/x", aliases = new string[] { "horizontal" }, usage = "ScrollHorizontal", displayName = "Left/Right")]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "scroll/y", aliases = new string[] { "vertical" }, usage = "ScrollVertical", displayName = "Up/Down", shortDisplayName = "Wheel")]
		public global::UnityEngine.Vector2 scroll;

		[global::System.Runtime.InteropServices.FieldOffset(24)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "press", useStateFrom = "leftButton", synthetic = true, usages = new string[] { })]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "leftButton", layout = "Button", bit = 0u, usage = "PrimaryAction", displayName = "Left Button", shortDisplayName = "LMB")]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "rightButton", layout = "Button", bit = 1u, usage = "SecondaryAction", displayName = "Right Button", shortDisplayName = "RMB")]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "middleButton", layout = "Button", bit = 2u, displayName = "Middle Button", shortDisplayName = "MMB")]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "forwardButton", layout = "Button", bit = 3u, usage = "Forward", displayName = "Forward")]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "backButton", layout = "Button", bit = 4u, usage = "Back", displayName = "Back")]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "pressure", layout = "Axis", usage = "Pressure", offset = 4294967294u, format = "FLT", sizeInBits = 32u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "radius", layout = "Vector2", usage = "Radius", offset = 4294967294u, format = "VEC2", sizeInBits = 64u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "pointerId", layout = "Digital", format = "BIT", sizeInBits = 1u, offset = 4294967294u)]
		public ushort buttons;

		[global::System.Runtime.InteropServices.FieldOffset(26)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "displayIndex", layout = "Integer", displayName = "Display Index")]
		public ushort displayIndex;

		[global::System.Runtime.InteropServices.FieldOffset(28)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "clickCount", layout = "Integer", displayName = "Click Count", synthetic = true)]
		public ushort clickCount;

		public static global::UnityEngine.InputSystem.Utilities.FourCC Format => new global::UnityEngine.InputSystem.Utilities.FourCC('M', 'O', 'U', 'S');

		public global::UnityEngine.InputSystem.Utilities.FourCC format => Format;

		public global::UnityEngine.InputSystem.LowLevel.MouseState WithButton(global::UnityEngine.InputSystem.LowLevel.MouseButton button, bool state = true)
		{
			uint num = (uint)(1 << (int)button);
			if (state)
			{
				buttons |= (ushort)num;
			}
			else
			{
				buttons &= (ushort)(~num);
			}
			return this;
		}
	}
}
