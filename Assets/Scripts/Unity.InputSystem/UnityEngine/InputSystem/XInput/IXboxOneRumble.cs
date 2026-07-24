namespace UnityEngine.InputSystem.XInput
{
	public interface IXboxOneRumble : global::UnityEngine.InputSystem.Haptics.IDualMotorRumble, global::UnityEngine.InputSystem.Haptics.IHaptics
	{
		void SetMotorSpeeds(float lowFrequency, float highFrequency, float leftTrigger, float rightTrigger);
	}
}
