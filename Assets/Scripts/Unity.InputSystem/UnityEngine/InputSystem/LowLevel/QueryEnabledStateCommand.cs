namespace UnityEngine.InputSystem.LowLevel
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Explicit, Size = 9)]
	public struct QueryEnabledStateCommand : global::UnityEngine.InputSystem.LowLevel.IInputDeviceCommandInfo
	{
		internal const int kSize = 9;

		[global::System.Runtime.InteropServices.FieldOffset(0)]
		public global::UnityEngine.InputSystem.LowLevel.InputDeviceCommand baseCommand;

		[global::System.Runtime.InteropServices.FieldOffset(8)]
		public bool isEnabled;

		public static global::UnityEngine.InputSystem.Utilities.FourCC Type => new global::UnityEngine.InputSystem.Utilities.FourCC('Q', 'E', 'N', 'B');

		public global::UnityEngine.InputSystem.Utilities.FourCC typeStatic => Type;

		public static global::UnityEngine.InputSystem.LowLevel.QueryEnabledStateCommand Create()
		{
			return new global::UnityEngine.InputSystem.LowLevel.QueryEnabledStateCommand
			{
				baseCommand = new global::UnityEngine.InputSystem.LowLevel.InputDeviceCommand(Type, 9)
			};
		}
	}
}
