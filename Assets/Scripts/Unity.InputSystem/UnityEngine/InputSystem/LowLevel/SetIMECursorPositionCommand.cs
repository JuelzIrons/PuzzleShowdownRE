namespace UnityEngine.InputSystem.LowLevel
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Explicit, Size = 16)]
	public struct SetIMECursorPositionCommand : global::UnityEngine.InputSystem.LowLevel.IInputDeviceCommandInfo
	{
		internal const int kSize = 16;

		[global::System.Runtime.InteropServices.FieldOffset(0)]
		public global::UnityEngine.InputSystem.LowLevel.InputDeviceCommand baseCommand;

		[global::System.Runtime.InteropServices.FieldOffset(8)]
		private global::UnityEngine.Vector2 m_Position;

		public static global::UnityEngine.InputSystem.Utilities.FourCC Type => new global::UnityEngine.InputSystem.Utilities.FourCC('I', 'M', 'E', 'P');

		public global::UnityEngine.Vector2 position => m_Position;

		public global::UnityEngine.InputSystem.Utilities.FourCC typeStatic => Type;

		public static global::UnityEngine.InputSystem.LowLevel.SetIMECursorPositionCommand Create(global::UnityEngine.Vector2 cursorPosition)
		{
			return new global::UnityEngine.InputSystem.LowLevel.SetIMECursorPositionCommand
			{
				baseCommand = new global::UnityEngine.InputSystem.LowLevel.InputDeviceCommand(Type, 16),
				m_Position = cursorPosition
			};
		}
	}
}
