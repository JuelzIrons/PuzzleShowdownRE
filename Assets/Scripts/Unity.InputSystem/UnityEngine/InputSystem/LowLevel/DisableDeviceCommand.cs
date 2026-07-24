namespace UnityEngine.InputSystem.LowLevel
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Explicit, Size = 8)]
	public struct DisableDeviceCommand : global::UnityEngine.InputSystem.LowLevel.IInputDeviceCommandInfo
	{
		internal const int kSize = 8;

		[global::System.Runtime.InteropServices.FieldOffset(0)]
		public global::UnityEngine.InputSystem.LowLevel.InputDeviceCommand baseCommand;

		public static global::UnityEngine.InputSystem.Utilities.FourCC Type => new global::UnityEngine.InputSystem.Utilities.FourCC('D', 'S', 'B', 'L');

		public global::UnityEngine.InputSystem.Utilities.FourCC typeStatic => Type;

		public static global::UnityEngine.InputSystem.LowLevel.DisableDeviceCommand Create()
		{
			return new global::UnityEngine.InputSystem.LowLevel.DisableDeviceCommand
			{
				baseCommand = new global::UnityEngine.InputSystem.LowLevel.InputDeviceCommand(Type)
			};
		}
	}
}
