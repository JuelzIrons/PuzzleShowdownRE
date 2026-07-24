namespace UnityEngine.InputSystem.LowLevel
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Explicit, Size = 8)]
	public struct InputDeviceCommand : global::UnityEngine.InputSystem.LowLevel.IInputDeviceCommandInfo
	{
		internal const int kBaseCommandSize = 8;

		public const int BaseCommandSize = 8;

		public const long GenericFailure = -1L;

		public const long GenericSuccess = 1L;

		[global::System.Runtime.InteropServices.FieldOffset(0)]
		public global::UnityEngine.InputSystem.Utilities.FourCC type;

		[global::System.Runtime.InteropServices.FieldOffset(4)]
		public int sizeInBytes;

		public int payloadSizeInBytes => sizeInBytes - 8;

		public unsafe void* payloadPtr
		{
			get
			{
				fixed (global::UnityEngine.InputSystem.LowLevel.InputDeviceCommand* ptr = &this)
				{
					void* ptr2 = ptr;
					return (byte*)ptr2 + 8;
				}
			}
		}

		public global::UnityEngine.InputSystem.Utilities.FourCC typeStatic => default(global::UnityEngine.InputSystem.Utilities.FourCC);

		public InputDeviceCommand(global::UnityEngine.InputSystem.Utilities.FourCC type, int sizeInBytes = 8)
		{
			this.type = type;
			this.sizeInBytes = sizeInBytes;
		}

		public unsafe static global::Unity.Collections.NativeArray<byte> AllocateNative(global::UnityEngine.InputSystem.Utilities.FourCC type, int payloadSize)
		{
			int length = payloadSize + 8;
			global::Unity.Collections.NativeArray<byte> nativeArray = new global::Unity.Collections.NativeArray<byte>(length, global::Unity.Collections.Allocator.Temp);
			global::UnityEngine.InputSystem.LowLevel.InputDeviceCommand* unsafePtr = (global::UnityEngine.InputSystem.LowLevel.InputDeviceCommand*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(nativeArray);
			unsafePtr->type = type;
			unsafePtr->sizeInBytes = length;
			return nativeArray;
		}
	}
}
