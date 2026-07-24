namespace UnityEngine.InputSystem.XR
{
	public class XRControllerWithRumble : global::UnityEngine.InputSystem.XR.XRController
	{
		public void SendImpulse(float amplitude, float duration)
		{
			global::UnityEngine.InputSystem.XR.Haptics.SendHapticImpulseCommand command = global::UnityEngine.InputSystem.XR.Haptics.SendHapticImpulseCommand.Create(0, amplitude, duration);
			ExecuteCommand(ref command);
		}
	}
}
