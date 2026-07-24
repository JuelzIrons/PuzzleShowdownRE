namespace UnityEngine.InputSystem.LowLevel
{
	internal struct LinearAccelerationState : global::UnityEngine.InputSystem.LowLevel.IInputStateTypeInfo
	{
		[global::UnityEngine.InputSystem.Layouts.InputControl(displayName = "Acceleration", processors = "CompensateDirection", noisy = true)]
		public global::UnityEngine.Vector3 acceleration;

		public static global::UnityEngine.InputSystem.Utilities.FourCC kFormat => new global::UnityEngine.InputSystem.Utilities.FourCC('L', 'A', 'A', 'C');

		public global::UnityEngine.InputSystem.Utilities.FourCC format => kFormat;
	}
}
