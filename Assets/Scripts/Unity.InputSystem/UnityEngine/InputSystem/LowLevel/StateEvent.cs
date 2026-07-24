namespace UnityEngine.InputSystem.LowLevel
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Explicit, Pack = 1, Size = 25)]
	public struct StateEvent : global::UnityEngine.InputSystem.LowLevel.IInputEventTypeInfo
	{
		public const int Type = 1398030676;

		internal const int kStateDataSizeToSubtract = 1;

		[global::System.Runtime.InteropServices.FieldOffset(0)]
		public global::UnityEngine.InputSystem.LowLevel.InputEvent baseEvent;

		[global::System.Runtime.InteropServices.FieldOffset(20)]
		public global::UnityEngine.InputSystem.Utilities.FourCC stateFormat;

		[global::System.Runtime.InteropServices.FieldOffset(24)]
		internal unsafe fixed byte stateData[1];

		public uint stateSizeInBytes => baseEvent.sizeInBytes - 24;

		public unsafe void* state
		{
			get
			{
				fixed (byte* result = stateData)
				{
					return result;
				}
			}
		}

		public global::UnityEngine.InputSystem.Utilities.FourCC typeStatic => 1398030676;

		public unsafe global::UnityEngine.InputSystem.LowLevel.InputEventPtr ToEventPtr()
		{
			fixed (global::UnityEngine.InputSystem.LowLevel.StateEvent* eventPtr = &this)
			{
				return new global::UnityEngine.InputSystem.LowLevel.InputEventPtr((global::UnityEngine.InputSystem.LowLevel.InputEvent*)eventPtr);
			}
		}

		public unsafe TState GetState<TState>() where TState : struct, global::UnityEngine.InputSystem.LowLevel.IInputStateTypeInfo
		{
			TState output = default(TState);
			if (stateFormat != output.format)
			{
				throw new global::System.InvalidOperationException($"Expected state format '{output.format}' but got '{stateFormat}' instead");
			}
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AddressOf(ref output), state, global::System.Math.Min(stateSizeInBytes, global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<TState>()));
			return output;
		}

		public unsafe static TState GetState<TState>(global::UnityEngine.InputSystem.LowLevel.InputEventPtr ptr) where TState : struct, global::UnityEngine.InputSystem.LowLevel.IInputStateTypeInfo
		{
			return From(ptr)->GetState<TState>();
		}

		public static int GetEventSizeWithPayload<TState>() where TState : struct
		{
			return global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<TState>() + 20 + 4;
		}

		public unsafe static global::UnityEngine.InputSystem.LowLevel.StateEvent* From(global::UnityEngine.InputSystem.LowLevel.InputEventPtr ptr)
		{
			if (!ptr.valid)
			{
				throw new global::System.ArgumentNullException("ptr");
			}
			if (!ptr.IsA<global::UnityEngine.InputSystem.LowLevel.StateEvent>())
			{
				throw new global::System.InvalidCastException($"Cannot cast event with type '{ptr.type}' into StateEvent");
			}
			return FromUnchecked(ptr);
		}

		internal unsafe static global::UnityEngine.InputSystem.LowLevel.StateEvent* FromUnchecked(global::UnityEngine.InputSystem.LowLevel.InputEventPtr ptr)
		{
			return (global::UnityEngine.InputSystem.LowLevel.StateEvent*)ptr.data;
		}

		public static global::Unity.Collections.NativeArray<byte> From(global::UnityEngine.InputSystem.InputDevice device, out global::UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr, global::Unity.Collections.Allocator allocator = global::Unity.Collections.Allocator.Temp)
		{
			return From(device, out eventPtr, allocator, useDefaultState: false);
		}

		public static global::Unity.Collections.NativeArray<byte> FromDefaultStateFor(global::UnityEngine.InputSystem.InputDevice device, out global::UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr, global::Unity.Collections.Allocator allocator = global::Unity.Collections.Allocator.Temp)
		{
			return From(device, out eventPtr, allocator, useDefaultState: true);
		}

		private unsafe static global::Unity.Collections.NativeArray<byte> From(global::UnityEngine.InputSystem.InputDevice device, out global::UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr, global::Unity.Collections.Allocator allocator, bool useDefaultState)
		{
			if (device == null)
			{
				throw new global::System.ArgumentNullException("device");
			}
			if (!device.added)
			{
				throw new global::System.ArgumentException($"Device '{device}' has not been added to system", "device");
			}
			global::UnityEngine.InputSystem.Utilities.FourCC format = device.m_StateBlock.format;
			uint alignedSizeInBytes = device.m_StateBlock.alignedSizeInBytes;
			uint byteOffset = device.m_StateBlock.byteOffset;
			byte* source = (byte*)(useDefaultState ? device.defaultStatePtr : device.currentStatePtr) + (int)byteOffset;
			uint num = 24 + alignedSizeInBytes;
			global::Unity.Collections.NativeArray<byte> nativeArray = new global::Unity.Collections.NativeArray<byte>((int)global::UnityEngine.InputSystem.Utilities.NumberHelpers.AlignToMultipleOf(num, 4u), allocator);
			global::UnityEngine.InputSystem.LowLevel.StateEvent* unsafePtr = (global::UnityEngine.InputSystem.LowLevel.StateEvent*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(nativeArray);
			unsafePtr->baseEvent = new global::UnityEngine.InputSystem.LowLevel.InputEvent(1398030676, (int)num, device.deviceId, global::UnityEngine.InputSystem.LowLevel.InputRuntime.s_Instance.currentTime);
			unsafePtr->stateFormat = format;
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(unsafePtr->state, source, alignedSizeInBytes);
			eventPtr = unsafePtr->ToEventPtr();
			return nativeArray;
		}
	}
}
