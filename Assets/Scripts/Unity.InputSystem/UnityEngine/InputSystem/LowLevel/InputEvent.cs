namespace UnityEngine.InputSystem.LowLevel
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Explicit, Pack = 1, Size = 20)]
	public struct InputEvent
	{
		private const uint kHandledMask = 2147483648u;

		private const uint kIdMask = 2147483647u;

		internal const int kBaseEventSize = 20;

		public const int InvalidEventId = 0;

		internal const int kAlignment = 4;

		[global::System.Runtime.InteropServices.FieldOffset(0)]
		private global::UnityEngineInternal.Input.NativeInputEvent m_Event;

		public global::UnityEngine.InputSystem.Utilities.FourCC type
		{
			get
			{
				return new global::UnityEngine.InputSystem.Utilities.FourCC((int)m_Event.type);
			}
			set
			{
				m_Event.type = (global::UnityEngineInternal.Input.NativeInputEventType)(int)value;
			}
		}

		public uint sizeInBytes
		{
			get
			{
				return m_Event.sizeInBytes;
			}
			set
			{
				if (value > 65535)
				{
					throw new global::System.ArgumentException("Maximum event size is " + ushort.MaxValue, "value");
				}
				m_Event.sizeInBytes = (ushort)value;
			}
		}

		public int eventId
		{
			get
			{
				return (int)((long)m_Event.eventId & 0x7FFFFFFFL);
			}
			set
			{
				m_Event.eventId = value | (int)(m_Event.eventId & 0x80000000u);
			}
		}

		public int deviceId
		{
			get
			{
				return m_Event.deviceId;
			}
			set
			{
				m_Event.deviceId = (ushort)value;
			}
		}

		public double time
		{
			get
			{
				return m_Event.time - global::UnityEngine.InputSystem.LowLevel.InputRuntime.s_CurrentTimeOffsetToRealtimeSinceStartup;
			}
			set
			{
				m_Event.time = value + global::UnityEngine.InputSystem.LowLevel.InputRuntime.s_CurrentTimeOffsetToRealtimeSinceStartup;
			}
		}

		internal double internalTime
		{
			get
			{
				return m_Event.time;
			}
			set
			{
				m_Event.time = value;
			}
		}

		public bool handled
		{
			get
			{
				return (m_Event.eventId & 0x80000000u) == 2147483648u;
			}
			set
			{
				if (value)
				{
					m_Event.eventId = (int)(m_Event.eventId | 0x80000000u);
				}
				else
				{
					m_Event.eventId = (int)((long)m_Event.eventId & 0x7FFFFFFFL);
				}
			}
		}

		public InputEvent(global::UnityEngine.InputSystem.Utilities.FourCC type, int sizeInBytes, int deviceId, double time = -1.0)
		{
			if (time < 0.0)
			{
				time = global::UnityEngine.InputSystem.LowLevel.InputRuntime.s_Instance.currentTime;
			}
			m_Event.type = (global::UnityEngineInternal.Input.NativeInputEventType)(int)type;
			m_Event.sizeInBytes = (ushort)sizeInBytes;
			m_Event.deviceId = (ushort)deviceId;
			m_Event.time = time;
			m_Event.eventId = 0;
		}

		public override string ToString()
		{
			return $"id={eventId} type={type} device={deviceId} size={sizeInBytes} time={time}";
		}

		internal unsafe static global::UnityEngine.InputSystem.LowLevel.InputEvent* GetNextInMemory(global::UnityEngine.InputSystem.LowLevel.InputEvent* currentPtr)
		{
			uint num = global::UnityEngine.InputSystem.Utilities.NumberHelpers.AlignToMultipleOf(currentPtr->sizeInBytes, 4u);
			return (global::UnityEngine.InputSystem.LowLevel.InputEvent*)((byte*)currentPtr + num);
		}

		internal unsafe static global::UnityEngine.InputSystem.LowLevel.InputEvent* GetNextInMemoryChecked(global::UnityEngine.InputSystem.LowLevel.InputEvent* currentPtr, ref global::UnityEngine.InputSystem.LowLevel.InputEventBuffer buffer)
		{
			uint num = global::UnityEngine.InputSystem.Utilities.NumberHelpers.AlignToMultipleOf(currentPtr->sizeInBytes, 4u);
			global::UnityEngine.InputSystem.LowLevel.InputEvent* ptr = (global::UnityEngine.InputSystem.LowLevel.InputEvent*)((byte*)currentPtr + num);
			if (!buffer.Contains(ptr))
			{
				throw new global::System.InvalidOperationException($"Event '{(new global::UnityEngine.InputSystem.LowLevel.InputEventPtr(currentPtr))}' is last event in given buffer with size {buffer.sizeInBytes}");
			}
			return ptr;
		}

		public unsafe static bool Equals(global::UnityEngine.InputSystem.LowLevel.InputEvent* first, global::UnityEngine.InputSystem.LowLevel.InputEvent* second)
		{
			if (first == second)
			{
				return true;
			}
			if (first == null || second == null)
			{
				return false;
			}
			if (first->m_Event.sizeInBytes != second->m_Event.sizeInBytes)
			{
				return false;
			}
			return global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCmp(first, second, first->m_Event.sizeInBytes) == 0;
		}
	}
}
