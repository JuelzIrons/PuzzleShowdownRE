namespace UnityEngine.InputSystem.LowLevel
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Explicit, Size = 9)]
	public struct EnableIMECompositionCommand : global::UnityEngine.InputSystem.LowLevel.IInputDeviceCommandInfo
	{
		internal const int kSize = 12;

		[global::System.Runtime.InteropServices.FieldOffset(0)]
		public global::UnityEngine.InputSystem.LowLevel.InputDeviceCommand baseCommand;

		[global::System.Runtime.InteropServices.FieldOffset(8)]
		private byte m_ImeEnabled;

		public static global::UnityEngine.InputSystem.Utilities.FourCC Type => new global::UnityEngine.InputSystem.Utilities.FourCC('I', 'M', 'E', 'M');

		public bool imeEnabled => m_ImeEnabled != 0;

		public global::UnityEngine.InputSystem.Utilities.FourCC typeStatic => Type;

		public static global::UnityEngine.InputSystem.LowLevel.EnableIMECompositionCommand Create(bool enabled)
		{
			return new global::UnityEngine.InputSystem.LowLevel.EnableIMECompositionCommand
			{
				baseCommand = new global::UnityEngine.InputSystem.LowLevel.InputDeviceCommand(Type, 9),
				m_ImeEnabled = (byte)(enabled ? byte.MaxValue : 0)
			};
		}
	}
}
