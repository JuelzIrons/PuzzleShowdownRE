namespace UnityEngine.InputSystem.LowLevel
{
	internal struct GravityState : global::UnityEngine.InputSystem.LowLevel.IInputStateTypeInfo
	{
		[global::UnityEngine.InputSystem.Layouts.InputControl(displayName = "Gravity", processors = "CompensateDirection", noisy = true)]
		public global::UnityEngine.Vector3 gravity;

		public static global::UnityEngine.InputSystem.Utilities.FourCC kFormat => new global::UnityEngine.InputSystem.Utilities.FourCC('G', 'R', 'V');

		public global::UnityEngine.InputSystem.Utilities.FourCC format => kFormat;
	}
}
