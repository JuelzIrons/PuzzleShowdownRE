namespace UnityEngine.InputSystem.XR.Haptics
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Explicit, Size = 28)]
	public struct GetHapticCapabilitiesCommand : global::UnityEngine.InputSystem.LowLevel.IInputDeviceCommandInfo
	{
		private const int kSize = 28;

		[global::System.Runtime.InteropServices.FieldOffset(0)]
		private global::UnityEngine.InputSystem.LowLevel.InputDeviceCommand baseCommand;

		[global::System.Runtime.InteropServices.FieldOffset(8)]
		public uint numChannels;

		[global::System.Runtime.InteropServices.FieldOffset(12)]
		public bool supportsImpulse;

		[global::System.Runtime.InteropServices.FieldOffset(13)]
		public bool supportsBuffer;

		[global::System.Runtime.InteropServices.FieldOffset(16)]
		public uint frequencyHz;

		[global::System.Runtime.InteropServices.FieldOffset(20)]
		public uint maxBufferSize;

		[global::System.Runtime.InteropServices.FieldOffset(24)]
		public uint optimalBufferSize;

		private static global::UnityEngine.InputSystem.Utilities.FourCC Type => new global::UnityEngine.InputSystem.Utilities.FourCC('X', 'H', 'C', '0');

		public global::UnityEngine.InputSystem.Utilities.FourCC typeStatic => Type;

		public global::UnityEngine.InputSystem.XR.Haptics.HapticCapabilities capabilities => new global::UnityEngine.InputSystem.XR.Haptics.HapticCapabilities(numChannels, supportsImpulse, supportsBuffer, frequencyHz, maxBufferSize, optimalBufferSize);

		public static global::UnityEngine.InputSystem.XR.Haptics.GetHapticCapabilitiesCommand Create()
		{
			return new global::UnityEngine.InputSystem.XR.Haptics.GetHapticCapabilitiesCommand
			{
				baseCommand = new global::UnityEngine.InputSystem.LowLevel.InputDeviceCommand(Type, 28)
			};
		}
	}
}
