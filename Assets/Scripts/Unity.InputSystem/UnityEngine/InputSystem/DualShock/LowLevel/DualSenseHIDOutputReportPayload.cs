namespace UnityEngine.InputSystem.DualShock.LowLevel
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Explicit, Size = 47)]
	internal struct DualSenseHIDOutputReportPayload
	{
		[global::System.Runtime.InteropServices.FieldOffset(0)]
		public byte enableFlags1;

		[global::System.Runtime.InteropServices.FieldOffset(1)]
		public byte enableFlags2;

		[global::System.Runtime.InteropServices.FieldOffset(2)]
		public byte highFrequencyMotorSpeed;

		[global::System.Runtime.InteropServices.FieldOffset(3)]
		public byte lowFrequencyMotorSpeed;

		[global::System.Runtime.InteropServices.FieldOffset(44)]
		public byte redColor;

		[global::System.Runtime.InteropServices.FieldOffset(45)]
		public byte greenColor;

		[global::System.Runtime.InteropServices.FieldOffset(46)]
		public byte blueColor;
	}
}
