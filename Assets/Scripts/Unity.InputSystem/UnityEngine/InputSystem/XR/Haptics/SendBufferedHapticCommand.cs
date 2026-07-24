namespace UnityEngine.InputSystem.XR.Haptics
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Explicit, Size = 1040)]
	public struct SendBufferedHapticCommand : global::UnityEngine.InputSystem.LowLevel.IInputDeviceCommandInfo
	{
		private const int kMaxHapticBufferSize = 1024;

		private const int kSize = 1040;

		[global::System.Runtime.InteropServices.FieldOffset(0)]
		private global::UnityEngine.InputSystem.LowLevel.InputDeviceCommand baseCommand;

		[global::System.Runtime.InteropServices.FieldOffset(8)]
		private int channel;

		[global::System.Runtime.InteropServices.FieldOffset(12)]
		private int bufferSize;

		[global::System.Runtime.InteropServices.FieldOffset(16)]
		private unsafe fixed byte buffer[1024];

		private static global::UnityEngine.InputSystem.Utilities.FourCC Type => new global::UnityEngine.InputSystem.Utilities.FourCC('X', 'H', 'U', '0');

		public global::UnityEngine.InputSystem.Utilities.FourCC typeStatic => Type;

		public unsafe static global::UnityEngine.InputSystem.XR.Haptics.SendBufferedHapticCommand Create(byte[] rumbleBuffer)
		{
			if (rumbleBuffer == null)
			{
				throw new global::System.ArgumentNullException("rumbleBuffer");
			}
			int num = global::UnityEngine.Mathf.Min(1024, rumbleBuffer.Length);
			global::UnityEngine.InputSystem.XR.Haptics.SendBufferedHapticCommand result = new global::UnityEngine.InputSystem.XR.Haptics.SendBufferedHapticCommand
			{
				baseCommand = new global::UnityEngine.InputSystem.LowLevel.InputDeviceCommand(Type, 1040),
				bufferSize = num
			};
			global::UnityEngine.InputSystem.XR.Haptics.SendBufferedHapticCommand* ptr = &result;
			fixed (byte* ptr2 = rumbleBuffer)
			{
				for (int i = 0; i < num; i++)
				{
					ptr->buffer[i] = ptr2[i];
				}
			}
			return result;
		}
	}
}
