namespace UnityEngine.InputSystem.LowLevel
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Explicit, Size = 264)]
	public struct QueryKeyboardLayoutCommand : global::UnityEngine.InputSystem.LowLevel.IInputDeviceCommandInfo
	{
		internal const int kMaxNameLength = 256;

		[global::System.Runtime.InteropServices.FieldOffset(0)]
		public global::UnityEngine.InputSystem.LowLevel.InputDeviceCommand baseCommand;

		[global::System.Runtime.InteropServices.FieldOffset(8)]
		public unsafe fixed byte nameBuffer[256];

		public static global::UnityEngine.InputSystem.Utilities.FourCC Type => new global::UnityEngine.InputSystem.Utilities.FourCC('K', 'B', 'L', 'T');

		public global::UnityEngine.InputSystem.Utilities.FourCC typeStatic => Type;

		public unsafe string ReadLayoutName()
		{
			fixed (global::UnityEngine.InputSystem.LowLevel.QueryKeyboardLayoutCommand* ptr = &this)
			{
				return global::UnityEngine.InputSystem.Utilities.StringHelpers.ReadStringFromBuffer(new global::System.IntPtr(ptr->nameBuffer), 256);
			}
		}

		public unsafe void WriteLayoutName(string name)
		{
			fixed (global::UnityEngine.InputSystem.LowLevel.QueryKeyboardLayoutCommand* ptr = &this)
			{
				global::UnityEngine.InputSystem.Utilities.StringHelpers.WriteStringToBuffer(name, new global::System.IntPtr(ptr->nameBuffer), 256);
			}
		}

		public static global::UnityEngine.InputSystem.LowLevel.QueryKeyboardLayoutCommand Create()
		{
			return new global::UnityEngine.InputSystem.LowLevel.QueryKeyboardLayoutCommand
			{
				baseCommand = new global::UnityEngine.InputSystem.LowLevel.InputDeviceCommand(Type, 264)
			};
		}
	}
}
