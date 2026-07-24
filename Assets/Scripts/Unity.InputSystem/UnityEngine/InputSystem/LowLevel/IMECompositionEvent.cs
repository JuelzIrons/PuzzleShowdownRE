namespace UnityEngine.InputSystem.LowLevel
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Explicit, Size = 152)]
	public struct IMECompositionEvent : global::UnityEngine.InputSystem.LowLevel.IInputEventTypeInfo
	{
		internal const int kIMECharBufferSize = 64;

		public const int Type = 1229800787;

		[global::System.Runtime.InteropServices.FieldOffset(0)]
		public global::UnityEngine.InputSystem.LowLevel.InputEvent baseEvent;

		[global::System.Runtime.InteropServices.FieldOffset(20)]
		public global::UnityEngine.InputSystem.LowLevel.IMECompositionString compositionString;

		public global::UnityEngine.InputSystem.Utilities.FourCC typeStatic => 1229800787;

		public static global::UnityEngine.InputSystem.LowLevel.IMECompositionEvent Create(int deviceId, string compositionString, double time)
		{
			return new global::UnityEngine.InputSystem.LowLevel.IMECompositionEvent
			{
				baseEvent = new global::UnityEngine.InputSystem.LowLevel.InputEvent(1229800787, 152, deviceId, time),
				compositionString = new global::UnityEngine.InputSystem.LowLevel.IMECompositionString(compositionString)
			};
		}
	}
}
