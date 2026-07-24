namespace UnityEngine.InputSystem.LowLevel
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Explicit, Pack = 1, Size = 29)]
	public struct DeltaStateEvent : global::UnityEngine.InputSystem.LowLevel.IInputEventTypeInfo
	{
		public const int Type = 1145852993;

		[global::System.Runtime.InteropServices.FieldOffset(0)]
		public global::UnityEngine.InputSystem.LowLevel.InputEvent baseEvent;

		[global::System.Runtime.InteropServices.FieldOffset(20)]
		public global::UnityEngine.InputSystem.Utilities.FourCC stateFormat;

		[global::System.Runtime.InteropServices.FieldOffset(24)]
		public uint stateOffset;

		[global::System.Runtime.InteropServices.FieldOffset(28)]
		internal unsafe fixed byte stateData[1];

		public uint deltaStateSizeInBytes => baseEvent.sizeInBytes - 28;

		public unsafe void* deltaState
		{
			get
			{
				fixed (byte* result = stateData)
				{
					return result;
				}
			}
		}

		public global::UnityEngine.InputSystem.Utilities.FourCC typeStatic => 1145852993;

		public unsafe global::UnityEngine.InputSystem.LowLevel.InputEventPtr ToEventPtr()
		{
			fixed (global::UnityEngine.InputSystem.LowLevel.DeltaStateEvent* eventPtr = &this)
			{
				return new global::UnityEngine.InputSystem.LowLevel.InputEventPtr((global::UnityEngine.InputSystem.LowLevel.InputEvent*)eventPtr);
			}
		}

		public unsafe static global::UnityEngine.InputSystem.LowLevel.DeltaStateEvent* From(global::UnityEngine.InputSystem.LowLevel.InputEventPtr ptr)
		{
			if (!ptr.valid)
			{
				throw new global::System.ArgumentNullException("ptr");
			}
			if (!ptr.IsA<global::UnityEngine.InputSystem.LowLevel.DeltaStateEvent>())
			{
				throw new global::System.InvalidCastException($"Cannot cast event with type '{ptr.type}' into DeltaStateEvent");
			}
			return FromUnchecked(ptr);
		}

		internal unsafe static global::UnityEngine.InputSystem.LowLevel.DeltaStateEvent* FromUnchecked(global::UnityEngine.InputSystem.LowLevel.InputEventPtr ptr)
		{
			return (global::UnityEngine.InputSystem.LowLevel.DeltaStateEvent*)ptr.data;
		}

		public unsafe static global::Unity.Collections.NativeArray<byte> From(global::UnityEngine.InputSystem.InputControl control, out global::UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr, global::Unity.Collections.Allocator allocator = global::Unity.Collections.Allocator.Temp)
		{
			if (control == null)
			{
				throw new global::System.ArgumentNullException("control");
			}
			global::UnityEngine.InputSystem.InputDevice device = control.device;
			if (!device.added)
			{
				throw new global::System.ArgumentException($"Device for control '{control}' has not been added to system", "control");
			}
			if (control.currentStatePtr == null)
			{
				throw new global::System.ArgumentNullException($"Control '{control}' does not have an associated state");
			}
			ref global::UnityEngine.InputSystem.LowLevel.InputStateBlock stateBlock = ref device.m_StateBlock;
			ref global::UnityEngine.InputSystem.LowLevel.InputStateBlock stateBlock2 = ref control.m_StateBlock;
			global::UnityEngine.InputSystem.Utilities.FourCC format = stateBlock.format;
			uint num = 0u;
			num = ((stateBlock2.bitOffset == 0) ? stateBlock2.alignedSizeInBytes : ((stateBlock2.bitOffset + stateBlock2.sizeInBits + 7) / 8));
			uint byteOffset = stateBlock2.byteOffset;
			byte* source = (byte*)control.currentStatePtr + (int)byteOffset;
			uint num2 = 28 + num;
			global::Unity.Collections.NativeArray<byte> nativeArray = new global::Unity.Collections.NativeArray<byte>((int)global::UnityEngine.InputSystem.Utilities.NumberHelpers.AlignToMultipleOf(num2, 4u), allocator);
			global::UnityEngine.InputSystem.LowLevel.DeltaStateEvent* unsafePtr = (global::UnityEngine.InputSystem.LowLevel.DeltaStateEvent*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(nativeArray);
			unsafePtr->baseEvent = new global::UnityEngine.InputSystem.LowLevel.InputEvent(1145852993, (int)num2, device.deviceId, global::UnityEngine.InputSystem.LowLevel.InputRuntime.s_Instance.currentTime);
			unsafePtr->stateFormat = format;
			unsafePtr->stateOffset = stateBlock2.byteOffset - stateBlock.byteOffset;
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(unsafePtr->deltaState, source, num);
			eventPtr = unsafePtr->ToEventPtr();
			return nativeArray;
		}
	}
}
