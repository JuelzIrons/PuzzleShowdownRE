namespace UnityEngine.InputSystem.LowLevel
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Explicit, Size = 20)]
	public struct DeviceConfigurationEvent : global::UnityEngine.InputSystem.LowLevel.IInputEventTypeInfo
	{
		public const int Type = 1145259591;

		[global::System.Runtime.InteropServices.FieldOffset(0)]
		public global::UnityEngine.InputSystem.LowLevel.InputEvent baseEvent;

		public global::UnityEngine.InputSystem.Utilities.FourCC typeStatic => 1145259591;

		public unsafe global::UnityEngine.InputSystem.LowLevel.InputEventPtr ToEventPtr()
		{
			fixed (global::UnityEngine.InputSystem.LowLevel.DeviceConfigurationEvent* eventPtr = &this)
			{
				return new global::UnityEngine.InputSystem.LowLevel.InputEventPtr((global::UnityEngine.InputSystem.LowLevel.InputEvent*)eventPtr);
			}
		}

		public static global::UnityEngine.InputSystem.LowLevel.DeviceConfigurationEvent Create(int deviceId, double time)
		{
			return new global::UnityEngine.InputSystem.LowLevel.DeviceConfigurationEvent
			{
				baseEvent = new global::UnityEngine.InputSystem.LowLevel.InputEvent(1145259591, 20, deviceId, time)
			};
		}
	}
}
