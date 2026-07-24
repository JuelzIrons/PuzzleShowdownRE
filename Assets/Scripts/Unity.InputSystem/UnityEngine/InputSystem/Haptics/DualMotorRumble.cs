namespace UnityEngine.InputSystem.Haptics
{
	internal struct DualMotorRumble
	{
		public float lowFrequencyMotorSpeed { get; private set; }

		public float highFrequencyMotorSpeed { get; private set; }

		public bool isRumbling
		{
			get
			{
				if (global::UnityEngine.Mathf.Approximately(lowFrequencyMotorSpeed, 0f))
				{
					return !global::UnityEngine.Mathf.Approximately(highFrequencyMotorSpeed, 0f);
				}
				return true;
			}
		}

		public void PauseHaptics(global::UnityEngine.InputSystem.InputDevice device)
		{
			if (device == null)
			{
				throw new global::System.ArgumentNullException("device");
			}
			if (isRumbling)
			{
				global::UnityEngine.InputSystem.LowLevel.DualMotorRumbleCommand command = global::UnityEngine.InputSystem.LowLevel.DualMotorRumbleCommand.Create(0f, 0f);
				device.ExecuteCommand(ref command);
			}
		}

		public void ResumeHaptics(global::UnityEngine.InputSystem.InputDevice device)
		{
			if (device == null)
			{
				throw new global::System.ArgumentNullException("device");
			}
			if (isRumbling)
			{
				SetMotorSpeeds(device, lowFrequencyMotorSpeed, highFrequencyMotorSpeed);
			}
		}

		public void ResetHaptics(global::UnityEngine.InputSystem.InputDevice device)
		{
			if (device == null)
			{
				throw new global::System.ArgumentNullException("device");
			}
			if (isRumbling)
			{
				SetMotorSpeeds(device, 0f, 0f);
			}
		}

		public void SetMotorSpeeds(global::UnityEngine.InputSystem.InputDevice device, float lowFrequency, float highFrequency)
		{
			if (device == null)
			{
				throw new global::System.ArgumentNullException("device");
			}
			lowFrequencyMotorSpeed = global::UnityEngine.Mathf.Clamp(lowFrequency, 0f, 1f);
			highFrequencyMotorSpeed = global::UnityEngine.Mathf.Clamp(highFrequency, 0f, 1f);
			global::UnityEngine.InputSystem.LowLevel.DualMotorRumbleCommand command = global::UnityEngine.InputSystem.LowLevel.DualMotorRumbleCommand.Create(lowFrequencyMotorSpeed, highFrequencyMotorSpeed);
			device.ExecuteCommand(ref command);
		}
	}
}
