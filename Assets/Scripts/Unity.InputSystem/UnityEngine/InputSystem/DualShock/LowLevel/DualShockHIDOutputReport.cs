namespace UnityEngine.InputSystem.DualShock.LowLevel
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Explicit, Size = 40)]
	internal struct DualShockHIDOutputReport : global::UnityEngine.InputSystem.LowLevel.IInputDeviceCommandInfo
	{
		[global::System.Flags]
		public enum Flags
		{
			Rumble = 1,
			Color = 2
		}

		internal const int kSize = 40;

		internal const int kReportId = 5;

		[global::System.Runtime.InteropServices.FieldOffset(0)]
		public global::UnityEngine.InputSystem.LowLevel.InputDeviceCommand baseCommand;

		[global::System.Runtime.InteropServices.FieldOffset(8)]
		public byte reportId;

		[global::System.Runtime.InteropServices.FieldOffset(9)]
		public byte flags;

		[global::System.Runtime.InteropServices.FieldOffset(10)]
		public unsafe fixed byte unknown1[2];

		[global::System.Runtime.InteropServices.FieldOffset(12)]
		public byte highFrequencyMotorSpeed;

		[global::System.Runtime.InteropServices.FieldOffset(13)]
		public byte lowFrequencyMotorSpeed;

		[global::System.Runtime.InteropServices.FieldOffset(14)]
		public byte redColor;

		[global::System.Runtime.InteropServices.FieldOffset(15)]
		public byte greenColor;

		[global::System.Runtime.InteropServices.FieldOffset(16)]
		public byte blueColor;

		[global::System.Runtime.InteropServices.FieldOffset(17)]
		public unsafe fixed byte unknown2[23];

		public static global::UnityEngine.InputSystem.Utilities.FourCC Type => new global::UnityEngine.InputSystem.Utilities.FourCC('H', 'I', 'D', 'O');

		public global::UnityEngine.InputSystem.Utilities.FourCC typeStatic => Type;

		public void SetMotorSpeeds(float lowFreq, float highFreq)
		{
			flags |= 1;
			lowFrequencyMotorSpeed = (byte)global::UnityEngine.Mathf.Clamp(lowFreq * 255f, 0f, 255f);
			highFrequencyMotorSpeed = (byte)global::UnityEngine.Mathf.Clamp(highFreq * 255f, 0f, 255f);
		}

		public void SetColor(global::UnityEngine.Color color)
		{
			flags |= 2;
			redColor = (byte)global::UnityEngine.Mathf.Clamp(color.r * 255f, 0f, 255f);
			greenColor = (byte)global::UnityEngine.Mathf.Clamp(color.g * 255f, 0f, 255f);
			blueColor = (byte)global::UnityEngine.Mathf.Clamp(color.b * 255f, 0f, 255f);
		}

		public static global::UnityEngine.InputSystem.DualShock.LowLevel.DualShockHIDOutputReport Create(int outputReportSize)
		{
			return new global::UnityEngine.InputSystem.DualShock.LowLevel.DualShockHIDOutputReport
			{
				baseCommand = new global::UnityEngine.InputSystem.LowLevel.InputDeviceCommand(Type, 8 + outputReportSize),
				reportId = 5
			};
		}
	}
}
