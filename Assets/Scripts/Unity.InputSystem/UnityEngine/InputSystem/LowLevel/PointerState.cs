namespace UnityEngine.InputSystem.LowLevel
{
	internal struct PointerState : global::UnityEngine.InputSystem.LowLevel.IInputStateTypeInfo
	{
		private uint pointerId;

		[global::UnityEngine.InputSystem.Layouts.InputControl(layout = "Vector2", displayName = "Position", usage = "Point", dontReset = true)]
		public global::UnityEngine.Vector2 position;

		[global::UnityEngine.InputSystem.Layouts.InputControl(layout = "Delta", displayName = "Delta", usage = "Secondary2DMotion")]
		public global::UnityEngine.Vector2 delta;

		[global::UnityEngine.InputSystem.Layouts.InputControl(layout = "Analog", displayName = "Pressure", usage = "Pressure", defaultState = 1f)]
		public float pressure;

		[global::UnityEngine.InputSystem.Layouts.InputControl(layout = "Vector2", displayName = "Radius", usage = "Radius")]
		public global::UnityEngine.Vector2 radius;

		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "press", displayName = "Press", layout = "Button", format = "BIT", bit = 0u)]
		public ushort buttons;

		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "displayIndex", layout = "Integer", displayName = "Display Index")]
		public ushort displayIndex;

		public static global::UnityEngine.InputSystem.Utilities.FourCC kFormat => new global::UnityEngine.InputSystem.Utilities.FourCC('P', 'T', 'R');

		public global::UnityEngine.InputSystem.Utilities.FourCC format => kFormat;
	}
}
