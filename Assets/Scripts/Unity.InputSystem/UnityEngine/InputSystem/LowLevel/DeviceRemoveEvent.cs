namespace UnityEngine.InputSystem.LowLevel
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Explicit, Size = 20)]
	public struct DeviceRemoveEvent : global::UnityEngine.InputSystem.LowLevel.IInputEventTypeInfo
	{
		public const int Type = 1146242381;

		[global::System.Runtime.InteropServices.FieldOffset(0)]
		public global::UnityEngine.InputSystem.LowLevel.InputEvent baseEvent;

		public global::UnityEngine.InputSystem.Utilities.FourCC typeStatic => 1146242381;

		public unsafe global::UnityEngine.InputSystem.LowLevel.InputEventPtr ToEventPtr()
		{
			fixed (global::UnityEngine.InputSystem.LowLevel.DeviceRemoveEvent* eventPtr = &this)
			{
				return new global::UnityEngine.InputSystem.LowLevel.InputEventPtr((global::UnityEngine.InputSystem.LowLevel.InputEvent*)eventPtr);
			}
		}

		public static global::UnityEngine.InputSystem.LowLevel.DeviceRemoveEvent Create(int deviceId, double time = -1.0)
		{
			return new global::UnityEngine.InputSystem.LowLevel.DeviceRemoveEvent
			{
				baseEvent = new global::UnityEngine.InputSystem.LowLevel.InputEvent(1146242381, 20, deviceId, time)
			};
		}
	}
}
