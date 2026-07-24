namespace UnityEngine.InputSystem.LowLevel
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Explicit, Size = 24)]
	public struct TextEvent : global::UnityEngine.InputSystem.LowLevel.IInputEventTypeInfo
	{
		public const int Type = 1413830740;

		[global::System.Runtime.InteropServices.FieldOffset(0)]
		public global::UnityEngine.InputSystem.LowLevel.InputEvent baseEvent;

		[global::System.Runtime.InteropServices.FieldOffset(20)]
		public int character;

		public global::UnityEngine.InputSystem.Utilities.FourCC typeStatic => 1413830740;

		public unsafe static global::UnityEngine.InputSystem.LowLevel.TextEvent* From(global::UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr)
		{
			if (!eventPtr.valid)
			{
				throw new global::System.ArgumentNullException("eventPtr");
			}
			if (!eventPtr.IsA<global::UnityEngine.InputSystem.LowLevel.TextEvent>())
			{
				throw new global::System.InvalidCastException($"Cannot cast event with type '{eventPtr.type}' into TextEvent");
			}
			return (global::UnityEngine.InputSystem.LowLevel.TextEvent*)eventPtr.data;
		}

		public static global::UnityEngine.InputSystem.LowLevel.TextEvent Create(int deviceId, char character, double time = -1.0)
		{
			return new global::UnityEngine.InputSystem.LowLevel.TextEvent
			{
				baseEvent = new global::UnityEngine.InputSystem.LowLevel.InputEvent(1413830740, 24, deviceId, time),
				character = character
			};
		}

		public static global::UnityEngine.InputSystem.LowLevel.TextEvent Create(int deviceId, int character, double time = -1.0)
		{
			return new global::UnityEngine.InputSystem.LowLevel.TextEvent
			{
				baseEvent = new global::UnityEngine.InputSystem.LowLevel.InputEvent(1413830740, 24, deviceId, time),
				character = character
			};
		}
	}
}
