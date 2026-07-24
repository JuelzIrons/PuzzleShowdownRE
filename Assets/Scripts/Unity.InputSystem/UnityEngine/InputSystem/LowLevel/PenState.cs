namespace UnityEngine.InputSystem.LowLevel
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Explicit, Size = 36)]
	public struct PenState : global::UnityEngine.InputSystem.LowLevel.IInputStateTypeInfo
	{
		[global::System.Runtime.InteropServices.FieldOffset(0)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(usage = "Point", dontReset = true)]
		public global::UnityEngine.Vector2 position;

		[global::System.Runtime.InteropServices.FieldOffset(8)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(usage = "Secondary2DMotion", layout = "Delta")]
		public global::UnityEngine.Vector2 delta;

		[global::System.Runtime.InteropServices.FieldOffset(16)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(layout = "Vector2", displayName = "Tilt", usage = "Tilt")]
		public global::UnityEngine.Vector2 tilt;

		[global::System.Runtime.InteropServices.FieldOffset(24)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(layout = "Analog", usage = "Pressure", defaultState = 0f)]
		public float pressure;

		[global::System.Runtime.InteropServices.FieldOffset(28)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(layout = "Axis", displayName = "Twist", usage = "Twist")]
		public float twist;

		[global::System.Runtime.InteropServices.FieldOffset(32)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "tip", displayName = "Tip", layout = "Button", bit = 0u, usage = "PrimaryAction")]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "press", useStateFrom = "tip", synthetic = true, usages = new string[] { })]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "eraser", displayName = "Eraser", layout = "Button", bit = 1u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "inRange", displayName = "In Range?", layout = "Button", bit = 4u, synthetic = true)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "barrel1", displayName = "Barrel Button #1", layout = "Button", bit = 2u, alias = "barrelFirst", usage = "SecondaryAction")]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "barrel2", displayName = "Barrel Button #2", layout = "Button", bit = 3u, alias = "barrelSecond")]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "barrel3", displayName = "Barrel Button #3", layout = "Button", bit = 5u, alias = "barrelThird")]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "barrel4", displayName = "Barrel Button #4", layout = "Button", bit = 6u, alias = "barrelFourth")]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "radius", layout = "Vector2", format = "VEC2", sizeInBits = 64u, usage = "Radius", offset = 4294967294u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "pointerId", layout = "Digital", format = "UINT", sizeInBits = 32u, offset = 4294967294u)]
		public ushort buttons;

		[global::System.Runtime.InteropServices.FieldOffset(34)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "displayIndex", displayName = "Display Index", layout = "Integer")]
		private ushort displayIndex;

		public static global::UnityEngine.InputSystem.Utilities.FourCC Format => new global::UnityEngine.InputSystem.Utilities.FourCC('P', 'E', 'N');

		public global::UnityEngine.InputSystem.Utilities.FourCC format => Format;

		public global::UnityEngine.InputSystem.LowLevel.PenState WithButton(global::UnityEngine.InputSystem.PenButton button, bool state = true)
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
