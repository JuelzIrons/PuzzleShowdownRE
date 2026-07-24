namespace UnityEngine.InputSystem.DualShock.LowLevel
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Explicit, Size = 86)]
	internal struct DualSenseHIDBluetoothOutputReport : global::UnityEngine.InputSystem.LowLevel.IInputDeviceCommandInfo
	{
		internal const int kSize = 86;

		[global::System.Runtime.InteropServices.FieldOffset(0)]
		public global::UnityEngine.InputSystem.LowLevel.InputDeviceCommand baseCommand;

		[global::System.Runtime.InteropServices.FieldOffset(8)]
		public byte reportId;

		[global::System.Runtime.InteropServices.FieldOffset(9)]
		public byte tag1;

		[global::System.Runtime.InteropServices.FieldOffset(10)]
		public byte tag2;

		[global::System.Runtime.InteropServices.FieldOffset(11)]
		public global::UnityEngine.InputSystem.DualShock.LowLevel.DualSenseHIDOutputReportPayload payload;

		[global::System.Runtime.InteropServices.FieldOffset(82)]
		public uint crc32;

		[global::System.Runtime.InteropServices.FieldOffset(8)]
		public unsafe fixed byte rawData[74];

		public static global::UnityEngine.InputSystem.Utilities.FourCC Type => new global::UnityEngine.InputSystem.Utilities.FourCC('H', 'I', 'D', 'O');

		public global::UnityEngine.InputSystem.Utilities.FourCC typeStatic => Type;

		public static global::UnityEngine.InputSystem.DualShock.LowLevel.DualSenseHIDBluetoothOutputReport Create(global::UnityEngine.InputSystem.DualShock.LowLevel.DualSenseHIDOutputReportPayload payload, byte outputSequenceId, int outputReportSize)
		{
			return new global::UnityEngine.InputSystem.DualShock.LowLevel.DualSenseHIDBluetoothOutputReport
			{
				baseCommand = new global::UnityEngine.InputSystem.LowLevel.InputDeviceCommand(Type, 8 + outputReportSize),
				reportId = 49,
				tag1 = (byte)((outputSequenceId & 0xF) << 4),
				tag2 = 16,
				payload = payload
			};
		}
	}
}
