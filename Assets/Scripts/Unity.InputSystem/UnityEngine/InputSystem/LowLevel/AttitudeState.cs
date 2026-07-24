namespace UnityEngine.InputSystem.LowLevel
{
	internal struct AttitudeState : global::UnityEngine.InputSystem.LowLevel.IInputStateTypeInfo
	{
		[global::UnityEngine.InputSystem.Layouts.InputControl(displayName = "Attitude", processors = "CompensateRotation", noisy = true)]
		public global::UnityEngine.Quaternion attitude;

		public static global::UnityEngine.InputSystem.Utilities.FourCC kFormat => new global::UnityEngine.InputSystem.Utilities.FourCC('A', 'T', 'T', 'D');

		public global::UnityEngine.InputSystem.Utilities.FourCC format => kFormat;
	}
}
