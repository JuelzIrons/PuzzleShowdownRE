namespace UnityEngine.InputSystem
{
	[global::UnityEngine.InputSystem.Layouts.InputControlLayout(isGenericTypeOfDevice = true)]
	public class Sensor : global::UnityEngine.InputSystem.InputDevice
	{
		public float samplingFrequency
		{
			get
			{
				global::UnityEngine.InputSystem.LowLevel.QuerySamplingFrequencyCommand command = global::UnityEngine.InputSystem.LowLevel.QuerySamplingFrequencyCommand.Create();
				if (ExecuteCommand(ref command) >= 0)
				{
					return command.frequency;
				}
				throw new global::System.NotSupportedException($"Device '{this}' does not support querying sampling frequency");
			}
			set
			{
				global::UnityEngine.InputSystem.LowLevel.SetSamplingFrequencyCommand command = global::UnityEngine.InputSystem.LowLevel.SetSamplingFrequencyCommand.Create(value);
				ExecuteCommand(ref command);
			}
		}
	}
}
