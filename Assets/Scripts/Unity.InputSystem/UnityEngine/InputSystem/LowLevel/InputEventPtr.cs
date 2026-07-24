namespace UnityEngine.InputSystem.LowLevel
{
	public struct InputEventPtr : global::System.IEquatable<global::UnityEngine.InputSystem.LowLevel.InputEventPtr>
	{
		private unsafe readonly global::UnityEngine.InputSystem.LowLevel.InputEvent* m_EventPtr;

		public unsafe bool valid => m_EventPtr != null;

		public unsafe bool handled
		{
			get
			{
				if (!valid)
				{
					return false;
				}
				return m_EventPtr->handled;
			}
			set
			{
				if (!valid)
				{
					throw new global::System.InvalidOperationException("The InputEventPtr is not valid.");
				}
				m_EventPtr->handled = value;
			}
		}

		public unsafe int id
		{
			get
			{
				if (!valid)
				{
					return 0;
				}
				return m_EventPtr->eventId;
			}
			set
			{
				if (!valid)
				{
					throw new global::System.InvalidOperationException("The InputEventPtr is not valid.");
				}
				m_EventPtr->eventId = value;
			}
		}

		public unsafe global::UnityEngine.InputSystem.Utilities.FourCC type
		{
			get
			{
				if (!valid)
				{
					return default(global::UnityEngine.InputSystem.Utilities.FourCC);
				}
				return m_EventPtr->type;
			}
		}

		public unsafe uint sizeInBytes
		{
			get
			{
				if (!valid)
				{
					return 0u;
				}
				return m_EventPtr->sizeInBytes;
			}
		}

		public unsafe int deviceId
		{
			get
			{
				if (!valid)
				{
					return 0;
				}
				return m_EventPtr->deviceId;
			}
			set
			{
				if (!valid)
				{
					throw new global::System.InvalidOperationException("The InputEventPtr is not valid.");
				}
				m_EventPtr->deviceId = value;
			}
		}

		public unsafe double time
		{
			get
			{
				if (!valid)
				{
					return 0.0;
				}
				return m_EventPtr->time;
			}
			set
			{
				if (!valid)
				{
					throw new global::System.InvalidOperationException("The InputEventPtr is not valid.");
				}
				m_EventPtr->time = value;
			}
		}

		internal unsafe double internalTime
		{
			get
			{
				if (!valid)
				{
					return 0.0;
				}
				return m_EventPtr->internalTime;
			}
			set
			{
				if (!valid)
				{
					throw new global::System.InvalidOperationException("The InputEventPtr is not valid.");
				}
				m_EventPtr->internalTime = value;
			}
		}

		public unsafe global::UnityEngine.InputSystem.LowLevel.InputEvent* data => m_EventPtr;

		internal unsafe global::UnityEngine.InputSystem.Utilities.FourCC stateFormat
		{
			get
			{
				global::UnityEngine.InputSystem.Utilities.FourCC fourCC = type;
				if (fourCC == 1398030676)
				{
					return global::UnityEngine.InputSystem.LowLevel.StateEvent.FromUnchecked(this)->stateFormat;
				}
				if (fourCC == 1145852993)
				{
					return global::UnityEngine.InputSystem.LowLevel.DeltaStateEvent.FromUnchecked(this)->stateFormat;
				}
				global::UnityEngine.InputSystem.LowLevel.InputEventPtr inputEventPtr = this;
				throw new global::System.InvalidOperationException("Event must be a StateEvent or DeltaStateEvent but is " + inputEventPtr.ToString());
			}
		}

		internal unsafe uint stateSizeInBytes
		{
			get
			{
				if (IsA<global::UnityEngine.InputSystem.LowLevel.StateEvent>())
				{
					return global::UnityEngine.InputSystem.LowLevel.StateEvent.From(this)->stateSizeInBytes;
				}
				if (IsA<global::UnityEngine.InputSystem.LowLevel.DeltaStateEvent>())
				{
					return global::UnityEngine.InputSystem.LowLevel.DeltaStateEvent.From(this)->deltaStateSizeInBytes;
				}
				global::UnityEngine.InputSystem.LowLevel.InputEventPtr inputEventPtr = this;
				throw new global::System.InvalidOperationException("Event must be a StateEvent or DeltaStateEvent but is " + inputEventPtr.ToString());
			}
		}

		internal unsafe uint stateOffset
		{
			get
			{
				if (IsA<global::UnityEngine.InputSystem.LowLevel.DeltaStateEvent>())
				{
					return global::UnityEngine.InputSystem.LowLevel.DeltaStateEvent.From(this)->stateOffset;
				}
				global::UnityEngine.InputSystem.LowLevel.InputEventPtr inputEventPtr = this;
				throw new global::System.InvalidOperationException("Event must be a DeltaStateEvent but is " + inputEventPtr.ToString());
			}
		}

		public unsafe InputEventPtr(global::UnityEngine.InputSystem.LowLevel.InputEvent* eventPtr)
		{
			m_EventPtr = eventPtr;
		}

		public unsafe bool IsA<TOtherEvent>() where TOtherEvent : struct, global::UnityEngine.InputSystem.LowLevel.IInputEventTypeInfo
		{
			if (m_EventPtr == null)
			{
				return false;
			}
			return m_EventPtr->type == default(TOtherEvent).typeStatic;
		}

		public unsafe global::UnityEngine.InputSystem.LowLevel.InputEventPtr Next()
		{
			if (!valid)
			{
				return default(global::UnityEngine.InputSystem.LowLevel.InputEventPtr);
			}
			return new global::UnityEngine.InputSystem.LowLevel.InputEventPtr(global::UnityEngine.InputSystem.LowLevel.InputEvent.GetNextInMemory(m_EventPtr));
		}

		public unsafe override string ToString()
		{
			if (!valid)
			{
				return "null";
			}
			global::UnityEngine.InputSystem.LowLevel.InputEvent eventPtr = *m_EventPtr;
			return eventPtr.ToString();
		}

		public unsafe global::UnityEngine.InputSystem.LowLevel.InputEvent* ToPointer()
		{
			return this;
		}

		public unsafe bool Equals(global::UnityEngine.InputSystem.LowLevel.InputEventPtr other)
		{
			if (m_EventPtr != other.m_EventPtr)
			{
				return global::UnityEngine.InputSystem.LowLevel.InputEvent.Equals(m_EventPtr, other.m_EventPtr);
			}
			return true;
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (obj is global::UnityEngine.InputSystem.LowLevel.InputEventPtr other)
			{
				return Equals(other);
			}
			return false;
		}

		public unsafe override int GetHashCode()
		{
			return (int)m_EventPtr;
		}

		public unsafe static bool operator ==(global::UnityEngine.InputSystem.LowLevel.InputEventPtr left, global::UnityEngine.InputSystem.LowLevel.InputEventPtr right)
		{
			return left.m_EventPtr == right.m_EventPtr;
		}

		public unsafe static bool operator !=(global::UnityEngine.InputSystem.LowLevel.InputEventPtr left, global::UnityEngine.InputSystem.LowLevel.InputEventPtr right)
		{
			return left.m_EventPtr != right.m_EventPtr;
		}

		public unsafe static implicit operator global::UnityEngine.InputSystem.LowLevel.InputEventPtr(global::UnityEngine.InputSystem.LowLevel.InputEvent* eventPtr)
		{
			return new global::UnityEngine.InputSystem.LowLevel.InputEventPtr(eventPtr);
		}

		public unsafe static global::UnityEngine.InputSystem.LowLevel.InputEventPtr From(global::UnityEngine.InputSystem.LowLevel.InputEvent* eventPtr)
		{
			return new global::UnityEngine.InputSystem.LowLevel.InputEventPtr(eventPtr);
		}

		public unsafe static implicit operator global::UnityEngine.InputSystem.LowLevel.InputEvent*(global::UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr)
		{
			return eventPtr.data;
		}

		public unsafe static global::UnityEngine.InputSystem.LowLevel.InputEvent* FromInputEventPtr(global::UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr)
		{
			return eventPtr.data;
		}
	}
}
