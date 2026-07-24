namespace UnityEngine.InputSystem.DualShock.LowLevel
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Explicit, Size = 9)]
	internal struct DualShock4HIDInputReport : global::UnityEngine.InputSystem.LowLevel.IInputStateTypeInfo
	{
		public static global::UnityEngine.InputSystem.Utilities.FourCC Format = new global::UnityEngine.InputSystem.Utilities.FourCC('D', '4', 'V', 'S');

		[global::System.Runtime.InteropServices.FieldOffset(0)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "leftStick", layout = "Stick", format = "VC2B")]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "leftStick/x", offset = 0u, format = "BYTE", parameters = "normalize,normalizeMin=0,normalizeMax=1,normalizeZero=0.5")]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "leftStick/left", offset = 0u, format = "BYTE", parameters = "normalize,normalizeMin=0,normalizeMax=1,normalizeZero=0.5,clamp=1,clampMin=0,clampMax=0.5,invert")]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "leftStick/right", offset = 0u, format = "BYTE", parameters = "normalize,normalizeMin=0,normalizeMax=1,normalizeZero=0.5,clamp=1,clampMin=0.5,clampMax=1")]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "leftStick/y", offset = 1u, format = "BYTE", parameters = "invert,normalize,normalizeMin=0,normalizeMax=1,normalizeZero=0.5")]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "leftStick/up", offset = 1u, format = "BYTE", parameters = "normalize,normalizeMin=0,normalizeMax=1,normalizeZero=0.5,clamp=1,clampMin=0,clampMax=0.5,invert")]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "leftStick/down", offset = 1u, format = "BYTE", parameters = "normalize,normalizeMin=0,normalizeMax=1,normalizeZero=0.5,clamp=1,clampMin=0.5,clampMax=1,invert=false")]
		public byte leftStickX;

		[global::System.Runtime.InteropServices.FieldOffset(1)]
		public byte leftStickY;

		[global::System.Runtime.InteropServices.FieldOffset(2)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "rightStick", layout = "Stick", format = "VC2B")]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "rightStick/x", offset = 0u, format = "BYTE", parameters = "normalize,normalizeMin=0,normalizeMax=1,normalizeZero=0.5")]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "rightStick/left", offset = 0u, format = "BYTE", parameters = "normalize,normalizeMin=0,normalizeMax=1,normalizeZero=0.5,clamp=1,clampMin=0,clampMax=0.5,invert")]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "rightStick/right", offset = 0u, format = "BYTE", parameters = "normalize,normalizeMin=0,normalizeMax=1,normalizeZero=0.5,clamp=1,clampMin=0.5,clampMax=1")]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "rightStick/y", offset = 1u, format = "BYTE", parameters = "invert,normalize,normalizeMin=0,normalizeMax=1,normalizeZero=0.5")]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "rightStick/up", offset = 1u, format = "BYTE", parameters = "normalize,normalizeMin=0,normalizeMax=1,normalizeZero=0.5,clamp=1,clampMin=0,clampMax=0.5,invert")]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "rightStick/down", offset = 1u, format = "BYTE", parameters = "normalize,normalizeMin=0,normalizeMax=1,normalizeZero=0.5,clamp=1,clampMin=0.5,clampMax=1,invert=false")]
		public byte rightStickX;

		[global::System.Runtime.InteropServices.FieldOffset(3)]
		public byte rightStickY;

		[global::System.Runtime.InteropServices.FieldOffset(4)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "dpad", format = "BIT", layout = "Dpad", sizeInBits = 4u, defaultState = 8)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "dpad/up", format = "BIT", layout = "DiscreteButton", parameters = "minValue=7,maxValue=1,nullValue=8,wrapAtValue=7", bit = 0u, sizeInBits = 4u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "dpad/right", format = "BIT", layout = "DiscreteButton", parameters = "minValue=1,maxValue=3", bit = 0u, sizeInBits = 4u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "dpad/down", format = "BIT", layout = "DiscreteButton", parameters = "minValue=3,maxValue=5", bit = 0u, sizeInBits = 4u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "dpad/left", format = "BIT", layout = "DiscreteButton", parameters = "minValue=5, maxValue=7", bit = 0u, sizeInBits = 4u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "buttonWest", displayName = "Square", bit = 4u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "buttonSouth", displayName = "Cross", bit = 5u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "buttonEast", displayName = "Circle", bit = 6u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "buttonNorth", displayName = "Triangle", bit = 7u)]
		public byte buttons1;

		[global::System.Runtime.InteropServices.FieldOffset(5)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "leftShoulder", bit = 0u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "rightShoulder", bit = 1u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "leftTriggerButton", layout = "Button", bit = 2u, synthetic = true)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "rightTriggerButton", layout = "Button", bit = 3u, synthetic = true)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "select", displayName = "Share", bit = 4u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "start", displayName = "Options", bit = 5u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "leftStickPress", bit = 6u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "rightStickPress", bit = 7u)]
		public byte buttons2;

		[global::System.Runtime.InteropServices.FieldOffset(6)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "systemButton", layout = "Button", displayName = "System", bit = 0u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "touchpadButton", layout = "Button", displayName = "Touchpad Press", bit = 1u)]
		public byte buttons3;

		[global::System.Runtime.InteropServices.FieldOffset(7)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "leftTrigger", format = "BYTE")]
		public byte leftTrigger;

		[global::System.Runtime.InteropServices.FieldOffset(8)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "rightTrigger", format = "BYTE")]
		public byte rightTrigger;

		public global::UnityEngine.InputSystem.Utilities.FourCC format => Format;
	}
}
