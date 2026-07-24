namespace UnityEngine.InputSystem.LowLevel
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Explicit, Size = 16)]
	internal struct DualMotorRumbleCommand : global::UnityEngine.InputSystem.LowLevel.IInputDeviceCommandInfo
	{
		internal const int kSize = 16;

		[global::System.Runtime.InteropServices.FieldOffset(0)]
		public global::UnityEngine.InputSystem.LowLevel.InputDeviceCommand baseCommand;

		[global::System.Runtime.InteropServices.FieldOffset(8)]
		public float lowFrequencyMotorSpeed;

		[global::System.Runtime.InteropServices.FieldOffset(12)]
		public float highFrequencyMotorSpeed;

		public static global::UnityEngine.InputSystem.Utilities.FourCC Type => new global::UnityEngine.InputSystem.Utilities.FourCC('R', 'M', 'B', 'L');

		public global::UnityEngine.InputSystem.Utilities.FourCC typeStatic => Type;

		public static global::UnityEngine.InputSystem.LowLevel.DualMotorRumbleCommand Create(float lowFrequency, float highFrequency)
		{
			return new global::UnityEngine.InputSystem.LowLevel.DualMotorRumbleCommand
			{
				baseCommand = new global::UnityEngine.InputSystem.LowLevel.InputDeviceCommand(Type, 16),
				lowFrequencyMotorSpeed = lowFrequency,
				highFrequencyMotorSpeed = highFrequency
			};
		}
	}
}
