namespace UnityEngine.InputSystem.XR.Haptics
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Explicit, Size = 16)]
	public struct GetCurrentHapticStateCommand : global::UnityEngine.InputSystem.LowLevel.IInputDeviceCommandInfo
	{
		private const int kSize = 16;

		[global::System.Runtime.InteropServices.FieldOffset(0)]
		private global::UnityEngine.InputSystem.LowLevel.InputDeviceCommand baseCommand;

		[global::System.Runtime.InteropServices.FieldOffset(8)]
		public uint samplesQueued;

		[global::System.Runtime.InteropServices.FieldOffset(12)]
		public uint samplesAvailable;

		private static global::UnityEngine.InputSystem.Utilities.FourCC Type => new global::UnityEngine.InputSystem.Utilities.FourCC('X', 'H', 'S', '0');

		public global::UnityEngine.InputSystem.Utilities.FourCC typeStatic => Type;

		public global::UnityEngine.InputSystem.XR.Haptics.HapticState currentState => new global::UnityEngine.InputSystem.XR.Haptics.HapticState(samplesQueued, samplesAvailable);

		public static global::UnityEngine.InputSystem.XR.Haptics.GetCurrentHapticStateCommand Create()
		{
			return new global::UnityEngine.InputSystem.XR.Haptics.GetCurrentHapticStateCommand
			{
				baseCommand = new global::UnityEngine.InputSystem.LowLevel.InputDeviceCommand(Type, 16)
			};
		}
	}
}
