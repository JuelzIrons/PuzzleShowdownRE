namespace UnityEngine.InputSystem.LowLevel
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Explicit, Size = 37)]
	internal struct ActionEvent : global::UnityEngine.InputSystem.LowLevel.IInputEventTypeInfo
	{
		[global::System.Runtime.InteropServices.FieldOffset(0)]
		public global::UnityEngine.InputSystem.LowLevel.InputEvent baseEvent;

		[global::System.Runtime.InteropServices.FieldOffset(20)]
		private ushort m_ControlIndex;

		[global::System.Runtime.InteropServices.FieldOffset(22)]
		private ushort m_BindingIndex;

		[global::System.Runtime.InteropServices.FieldOffset(24)]
		private ushort m_InteractionIndex;

		[global::System.Runtime.InteropServices.FieldOffset(26)]
		private byte m_StateIndex;

		[global::System.Runtime.InteropServices.FieldOffset(27)]
		private byte m_Phase;

		[global::System.Runtime.InteropServices.FieldOffset(28)]
		private double m_StartTime;

		[global::System.Runtime.InteropServices.FieldOffset(36)]
		public unsafe fixed byte m_ValueData[1];

		public static global::UnityEngine.InputSystem.Utilities.FourCC Type => new global::UnityEngine.InputSystem.Utilities.FourCC('A', 'C', 'T', 'N');

		public double startTime
		{
			get
			{
				return m_StartTime;
			}
			set
			{
				m_StartTime = value;
			}
		}

		public global::UnityEngine.InputSystem.InputActionPhase phase
		{
			get
			{
				return (global::UnityEngine.InputSystem.InputActionPhase)m_Phase;
			}
			set
			{
				m_Phase = (byte)value;
			}
		}

		public unsafe byte* valueData
		{
			get
			{
				fixed (byte* result = m_ValueData)
				{
					return result;
				}
			}
		}

		public int valueSizeInBytes => (int)(baseEvent.sizeInBytes - 20 - 16);

		public int stateIndex
		{
			get
			{
				return m_StateIndex;
			}
			set
			{
				if (value < 0 || value > 255)
				{
					throw new global::System.NotSupportedException("State count cannot exceed byte.MaxValue");
				}
				m_StateIndex = (byte)value;
			}
		}

		public int controlIndex
		{
			get
			{
				return m_ControlIndex;
			}
			set
			{
				if (value < 0 || value > 65535)
				{
					throw new global::System.NotSupportedException("Control count cannot exceed ushort.MaxValue");
				}
				m_ControlIndex = (ushort)value;
			}
		}

		public int bindingIndex
		{
			get
			{
				return m_BindingIndex;
			}
			set
			{
				if (value < 0 || value > 65535)
				{
					throw new global::System.NotSupportedException("Binding count cannot exceed ushort.MaxValue");
				}
				m_BindingIndex = (ushort)value;
			}
		}

		public int interactionIndex
		{
			get
			{
				if (m_InteractionIndex == ushort.MaxValue)
				{
					return -1;
				}
				return m_InteractionIndex;
			}
			set
			{
				if (value == -1)
				{
					m_InteractionIndex = ushort.MaxValue;
					return;
				}
				if (value < 0 || value >= 65535)
				{
					throw new global::System.NotSupportedException("Interaction count cannot exceed ushort.MaxValue-1");
				}
				m_InteractionIndex = (ushort)value;
			}
		}

		public global::UnityEngine.InputSystem.Utilities.FourCC typeStatic => Type;

		public unsafe global::UnityEngine.InputSystem.LowLevel.InputEventPtr ToEventPtr()
		{
			fixed (global::UnityEngine.InputSystem.LowLevel.ActionEvent* eventPtr = &this)
			{
				return new global::UnityEngine.InputSystem.LowLevel.InputEventPtr((global::UnityEngine.InputSystem.LowLevel.InputEvent*)eventPtr);
			}
		}

		public static int GetEventSizeWithValueSize(int valueSizeInBytes)
		{
			return 36 + valueSizeInBytes;
		}

		public unsafe static global::UnityEngine.InputSystem.LowLevel.ActionEvent* From(global::UnityEngine.InputSystem.LowLevel.InputEventPtr ptr)
		{
			if (!ptr.valid)
			{
				throw new global::System.ArgumentNullException("ptr");
			}
			if (!ptr.IsA<global::UnityEngine.InputSystem.LowLevel.ActionEvent>())
			{
				throw new global::System.InvalidCastException($"Cannot cast event with type '{ptr.type}' into ActionEvent");
			}
			return (global::UnityEngine.InputSystem.LowLevel.ActionEvent*)ptr.data;
		}
	}
}
