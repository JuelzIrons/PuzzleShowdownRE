namespace UnityEngine.InputSystem.LowLevel
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Explicit, Size = 268)]
	public struct QueryKeyNameCommand : global::UnityEngine.InputSystem.LowLevel.IInputDeviceCommandInfo
	{
		internal const int kMaxNameLength = 256;

		internal const int kSize = 268;

		[global::System.Runtime.InteropServices.FieldOffset(0)]
		public global::UnityEngine.InputSystem.LowLevel.InputDeviceCommand baseCommand;

		[global::System.Runtime.InteropServices.FieldOffset(8)]
		public int scanOrKeyCode;

		[global::System.Runtime.InteropServices.FieldOffset(12)]
		public unsafe fixed byte nameBuffer[256];

		public static global::UnityEngine.InputSystem.Utilities.FourCC Type => new global::UnityEngine.InputSystem.Utilities.FourCC('K', 'Y', 'C', 'F');

		public global::UnityEngine.InputSystem.Utilities.FourCC typeStatic => Type;

		public unsafe string ReadKeyName()
		{
			fixed (global::UnityEngine.InputSystem.LowLevel.QueryKeyNameCommand* ptr = &this)
			{
				return global::UnityEngine.InputSystem.Utilities.StringHelpers.ReadStringFromBuffer(new global::System.IntPtr(ptr->nameBuffer), 256);
			}
		}

		public static global::UnityEngine.InputSystem.LowLevel.QueryKeyNameCommand Create(global::UnityEngine.InputSystem.Key key)
		{
			return new global::UnityEngine.InputSystem.LowLevel.QueryKeyNameCommand
			{
				baseCommand = new global::UnityEngine.InputSystem.LowLevel.InputDeviceCommand(Type, 268),
				scanOrKeyCode = (int)key
			};
		}
	}
}
