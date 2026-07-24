namespace UnityEngine.InputSystem.LowLevel
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Explicit, Size = 1040)]
	public struct QueryPairedUserAccountCommand : global::UnityEngine.InputSystem.LowLevel.IInputDeviceCommandInfo
	{
		[global::System.Flags]
		public enum Result : long
		{
			DevicePairedToUserAccount = 2L,
			UserAccountSelectionInProgress = 4L,
			UserAccountSelectionComplete = 8L,
			UserAccountSelectionCanceled = 0x10L
		}

		internal const int kMaxNameLength = 256;

		internal const int kMaxIdLength = 256;

		internal const int kSize = 1040;

		[global::System.Runtime.InteropServices.FieldOffset(0)]
		public global::UnityEngine.InputSystem.LowLevel.InputDeviceCommand baseCommand;

		[global::System.Runtime.InteropServices.FieldOffset(8)]
		public ulong handle;

		[global::System.Runtime.InteropServices.FieldOffset(16)]
		internal unsafe fixed byte nameBuffer[512];

		[global::System.Runtime.InteropServices.FieldOffset(528)]
		internal unsafe fixed byte idBuffer[512];

		public static global::UnityEngine.InputSystem.Utilities.FourCC Type => new global::UnityEngine.InputSystem.Utilities.FourCC('P', 'A', 'C', 'C');

		public unsafe string id
		{
			get
			{
				fixed (byte* value = idBuffer)
				{
					return global::UnityEngine.InputSystem.Utilities.StringHelpers.ReadStringFromBuffer(new global::System.IntPtr(value), 256);
				}
			}
			set
			{
				if (value == null)
				{
					throw new global::System.ArgumentNullException("value");
				}
				if (value.Length > 256)
				{
					throw new global::System.ArgumentException($"ID '{value}' exceeds maximum supported length of {256} characters", "value");
				}
				fixed (byte* value2 = idBuffer)
				{
					global::UnityEngine.InputSystem.Utilities.StringHelpers.WriteStringToBuffer(value, new global::System.IntPtr(value2), 256);
				}
			}
		}

		public unsafe string name
		{
			get
			{
				fixed (byte* value = nameBuffer)
				{
					return global::UnityEngine.InputSystem.Utilities.StringHelpers.ReadStringFromBuffer(new global::System.IntPtr(value), 256);
				}
			}
			set
			{
				if (value == null)
				{
					throw new global::System.ArgumentNullException("value");
				}
				if (value.Length > 256)
				{
					throw new global::System.ArgumentException($"Name '{value}' exceeds maximum supported length of {256} characters", "value");
				}
				fixed (byte* value2 = nameBuffer)
				{
					global::UnityEngine.InputSystem.Utilities.StringHelpers.WriteStringToBuffer(value, new global::System.IntPtr(value2), 256);
				}
			}
		}

		public global::UnityEngine.InputSystem.Utilities.FourCC typeStatic => Type;

		public static global::UnityEngine.InputSystem.LowLevel.QueryPairedUserAccountCommand Create()
		{
			return new global::UnityEngine.InputSystem.LowLevel.QueryPairedUserAccountCommand
			{
				baseCommand = new global::UnityEngine.InputSystem.LowLevel.InputDeviceCommand(Type, 1040)
			};
		}
	}
}
