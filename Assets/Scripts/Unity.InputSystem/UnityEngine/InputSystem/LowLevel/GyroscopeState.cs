namespace UnityEngine.InputSystem.LowLevel
{
	internal struct GyroscopeState : global::UnityEngine.InputSystem.LowLevel.IInputStateTypeInfo
	{
		[global::UnityEngine.InputSystem.Layouts.InputControl(displayName = "Angular Velocity", processors = "CompensateDirection", noisy = true)]
		public global::UnityEngine.Vector3 angularVelocity;

		public static global::UnityEngine.InputSystem.Utilities.FourCC kFormat => new global::UnityEngine.InputSystem.Utilities.FourCC('G', 'Y', 'R', 'O');

		public global::UnityEngine.InputSystem.Utilities.FourCC format => kFormat;
	}
}
