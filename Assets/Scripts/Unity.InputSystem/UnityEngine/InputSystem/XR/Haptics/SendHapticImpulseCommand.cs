namespace UnityEngine.InputSystem.XR.Haptics
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Explicit, Size = 20)]
	public struct SendHapticImpulseCommand : global::UnityEngine.InputSystem.LowLevel.IInputDeviceCommandInfo
	{
		private const int kSize = 20;

		[global::System.Runtime.InteropServices.FieldOffset(0)]
		private global::UnityEngine.InputSystem.LowLevel.InputDeviceCommand baseCommand;

		[global::System.Runtime.InteropServices.FieldOffset(8)]
		private int channel;

		[global::System.Runtime.InteropServices.FieldOffset(12)]
		private float amplitude;

		[global::System.Runtime.InteropServices.FieldOffset(16)]
		private float duration;

		private static global::UnityEngine.InputSystem.Utilities.FourCC Type => new global::UnityEngine.InputSystem.Utilities.FourCC('X', 'H', 'I', '0');

		public global::UnityEngine.InputSystem.Utilities.FourCC typeStatic => Type;

		public static global::UnityEngine.InputSystem.XR.Haptics.SendHapticImpulseCommand Create(int motorChannel, float motorAmplitude, float motorDuration)
		{
			return new global::UnityEngine.InputSystem.XR.Haptics.SendHapticImpulseCommand
			{
				baseCommand = new global::UnityEngine.InputSystem.LowLevel.InputDeviceCommand(Type, 20),
				channel = motorChannel,
				amplitude = motorAmplitude,
				duration = motorDuration
			};
		}
	}
}
