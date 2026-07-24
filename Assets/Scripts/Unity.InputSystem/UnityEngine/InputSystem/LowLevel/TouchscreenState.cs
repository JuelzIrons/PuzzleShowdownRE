namespace UnityEngine.InputSystem.LowLevel
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Explicit, Size = 560)]
	internal struct TouchscreenState : global::UnityEngine.InputSystem.LowLevel.IInputStateTypeInfo
	{
		public const int MaxTouches = 10;

		[global::System.Runtime.InteropServices.FieldOffset(0)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "primaryTouch", displayName = "Primary Touch", layout = "Touch", synthetic = true)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "primaryTouch/tap", usage = "PrimaryAction")]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "position", useStateFrom = "primaryTouch/position")]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "delta", useStateFrom = "primaryTouch/delta", layout = "Delta")]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "pressure", useStateFrom = "primaryTouch/pressure")]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "radius", useStateFrom = "primaryTouch/radius")]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "press", useStateFrom = "primaryTouch/phase", layout = "TouchPress", synthetic = true, usages = new string[] { })]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "displayIndex", useStateFrom = "primaryTouch/displayIndex", format = "BYTE")]
		public unsafe fixed byte primaryTouchData[56];

		internal const int kTouchDataOffset = 56;

		[global::System.Runtime.InteropServices.FieldOffset(56)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(layout = "Touch", name = "touch", displayName = "Touch", arraySize = 10)]
		public unsafe fixed byte touchData[560];

		public static global::UnityEngine.InputSystem.Utilities.FourCC Format => new global::UnityEngine.InputSystem.Utilities.FourCC('T', 'S', 'C', 'R');

		public unsafe global::UnityEngine.InputSystem.LowLevel.TouchState* primaryTouch
		{
			get
			{
				fixed (byte* result = primaryTouchData)
				{
					return (global::UnityEngine.InputSystem.LowLevel.TouchState*)result;
				}
			}
		}

		public unsafe global::UnityEngine.InputSystem.LowLevel.TouchState* touches
		{
			get
			{
				fixed (byte* result = touchData)
				{
					return (global::UnityEngine.InputSystem.LowLevel.TouchState*)result;
				}
			}
		}

		public global::UnityEngine.InputSystem.Utilities.FourCC format => Format;
	}
}
