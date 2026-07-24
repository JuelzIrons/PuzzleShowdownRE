namespace UnityEngine.InputSystem.XInput
{
	internal static class XInputSupport
	{
		public static void Initialize()
		{
			global::UnityEngine.InputSystem.InputSystem.RegisterLayout<global::UnityEngine.InputSystem.XInput.XInputController>();
			global::UnityEngine.InputSystem.InputSystem.RegisterLayout<global::UnityEngine.InputSystem.XInput.XInputControllerWindows>(null, default(global::UnityEngine.InputSystem.Layouts.InputDeviceMatcher).WithInterface("XInput"));
		}
	}
}
