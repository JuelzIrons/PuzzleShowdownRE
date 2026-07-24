namespace UnityEngine.InputSystem.DualShock.LowLevel
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Explicit, Size = 56)]
	internal struct DualSenseHIDUSBOutputReport : global::UnityEngine.InputSystem.LowLevel.IInputDeviceCommandInfo
	{
		internal const int kSize = 56;

		[global::System.Runtime.InteropServices.FieldOffset(0)]
		public global::UnityEngine.InputSystem.LowLevel.InputDeviceCommand baseCommand;

		[global::System.Runtime.InteropServices.FieldOffset(8)]
		public byte reportId;

		[global::System.Runtime.InteropServices.FieldOffset(9)]
		public global::UnityEngine.InputSystem.DualShock.LowLevel.DualSenseHIDOutputReportPayload payload;

		public static global::UnityEngine.InputSystem.Utilities.FourCC Type => new global::UnityEngine.InputSystem.Utilities.FourCC('H', 'I', 'D', 'O');

		public global::UnityEngine.InputSystem.Utilities.FourCC typeStatic => Type;

		public static global::UnityEngine.InputSystem.DualShock.LowLevel.DualSenseHIDUSBOutputReport Create(global::UnityEngine.InputSystem.DualShock.LowLevel.DualSenseHIDOutputReportPayload payload, int outputReportSize)
		{
			return new global::UnityEngine.InputSystem.DualShock.LowLevel.DualSenseHIDUSBOutputReport
			{
				baseCommand = new global::UnityEngine.InputSystem.LowLevel.InputDeviceCommand(Type, 8 + outputReportSize),
				reportId = 2,
				payload = payload
			};
		}
	}
}
