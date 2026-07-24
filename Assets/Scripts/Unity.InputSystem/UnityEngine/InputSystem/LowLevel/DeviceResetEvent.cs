namespace UnityEngine.InputSystem.LowLevel
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Explicit, Size = 20)]
	public struct DeviceResetEvent : global::UnityEngine.InputSystem.LowLevel.IInputEventTypeInfo
	{
		public const int Type = 1146245972;

		[global::System.Runtime.InteropServices.FieldOffset(0)]
		public global::UnityEngine.InputSystem.LowLevel.InputEvent baseEvent;

		[global::System.Runtime.InteropServices.FieldOffset(8)]
		public bool hardReset;

		public global::UnityEngine.InputSystem.Utilities.FourCC typeStatic => 1146245972;

		public static global::UnityEngine.InputSystem.LowLevel.DeviceResetEvent Create(int deviceId, bool hardReset = false, double time = -1.0)
		{
			global::UnityEngine.InputSystem.LowLevel.DeviceResetEvent result = new global::UnityEngine.InputSystem.LowLevel.DeviceResetEvent
			{
				baseEvent = new global::UnityEngine.InputSystem.LowLevel.InputEvent(1146245972, 20, deviceId, time)
			};
			result.hardReset = hardReset;
			return result;
		}
	}
}
