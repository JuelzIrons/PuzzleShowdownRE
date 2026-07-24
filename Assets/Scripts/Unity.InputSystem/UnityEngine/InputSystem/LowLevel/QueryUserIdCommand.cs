namespace UnityEngine.InputSystem.LowLevel
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Explicit, Size = 520)]
	internal struct QueryUserIdCommand : global::UnityEngine.InputSystem.LowLevel.IInputDeviceCommandInfo
	{
		public const int kMaxIdLength = 256;

		internal const int kSize = 520;

		[global::System.Runtime.InteropServices.FieldOffset(0)]
		public global::UnityEngine.InputSystem.LowLevel.InputDeviceCommand baseCommand;

		[global::System.Runtime.InteropServices.FieldOffset(8)]
		public unsafe fixed byte idBuffer[512];

		public static global::UnityEngine.InputSystem.Utilities.FourCC Type => new global::UnityEngine.InputSystem.Utilities.FourCC('U', 'S', 'E', 'R');

		public global::UnityEngine.InputSystem.Utilities.FourCC typeStatic => Type;

		public unsafe string ReadId()
		{
			fixed (global::UnityEngine.InputSystem.LowLevel.QueryUserIdCommand* ptr = &this)
			{
				return global::UnityEngine.InputSystem.Utilities.StringHelpers.ReadStringFromBuffer(new global::System.IntPtr(ptr->idBuffer), 256);
			}
		}

		public static global::UnityEngine.InputSystem.LowLevel.QueryUserIdCommand Create()
		{
			return new global::UnityEngine.InputSystem.LowLevel.QueryUserIdCommand
			{
				baseCommand = new global::UnityEngine.InputSystem.LowLevel.InputDeviceCommand(Type, 520)
			};
		}
	}
}
