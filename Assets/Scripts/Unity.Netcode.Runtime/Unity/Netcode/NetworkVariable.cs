namespace Unity.Netcode
{
	[global::System.Serializable]
	[global::Unity.Netcode.GenerateSerializationForGenericParameter(0)]
	public class NetworkVariable<T> : global::Unity.Netcode.NetworkVariableBase
	{
		public delegate void OnValueChangedDelegate(T previousValue, T newValue);

		public delegate bool CheckExceedsDirtinessThresholdDelegate(in T previousValue, in T newValue);

		public global::Unity.Netcode.NetworkVariable<T>.OnValueChangedDelegate OnValueChanged;

		public global::Unity.Netcode.NetworkVariable<T>.CheckExceedsDirtinessThresholdDelegate CheckExceedsDirtinessThreshold;

		[global::UnityEngine.SerializeField]
		private protected T m_InternalValue;

		private protected T m_LastInternalValue;

		private protected T m_PreviousValue;

		private bool m_HasPreviousValue;

		private bool m_IsDisposed;

		public virtual T Value
		{
			get
			{
				return m_InternalValue;
			}
			set
			{
				if (CannotWrite())
				{
					LogWritePermissionError();
				}
				else if (!global::Unity.Netcode.NetworkVariableSerialization<T>.AreEqual(ref m_InternalValue, ref value))
				{
					T internalValue = m_InternalValue;
					m_InternalValue = value;
					global::Unity.Netcode.NetworkVariableSerialization<T>.Duplicate(in m_InternalValue, ref m_LastInternalValue);
					SetDirty(isDirty: true);
					m_IsDisposed = false;
					OnValueChanged?.Invoke(internalValue, m_InternalValue);
				}
			}
		}

		public override bool ExceedsDirtinessThreshold()
		{
			if (CheckExceedsDirtinessThreshold != null && m_HasPreviousValue)
			{
				return CheckExceedsDirtinessThreshold(in m_PreviousValue, in m_InternalValue);
			}
			return true;
		}

		public override void OnInitialize()
		{
			base.OnInitialize();
			m_HasPreviousValue = true;
			global::Unity.Netcode.NetworkVariableSerialization<T>.Duplicate(in m_InternalValue, ref m_LastInternalValue);
			global::Unity.Netcode.NetworkVariableSerialization<T>.Duplicate(in m_InternalValue, ref m_PreviousValue);
		}

		public NetworkVariable(T value = default(T), global::Unity.Netcode.NetworkVariableReadPermission readPerm = global::Unity.Netcode.NetworkVariableReadPermission.Everyone, global::Unity.Netcode.NetworkVariableWritePermission writePerm = global::Unity.Netcode.NetworkVariableWritePermission.Server)
			: base(readPerm, writePerm)
		{
			m_InternalValue = value;
			m_LastInternalValue = default(T);
			m_PreviousValue = default(T);
		}

		public void Reset(T value = default(T))
		{
			if (m_NetworkBehaviour == null || m_NetworkObject == null || !m_NetworkObject.IsSpawned)
			{
				m_InternalValue = value;
				global::Unity.Netcode.NetworkVariableSerialization<T>.Duplicate(in m_InternalValue, ref m_LastInternalValue);
				m_PreviousValue = default(T);
			}
		}

		public bool CheckDirtyState(bool forceCheck = false)
		{
			bool flag = base.IsDirty();
			if (CannotWrite())
			{
				if (!global::Unity.Netcode.NetworkVariableSerialization<T>.AreEqual(ref m_InternalValue, ref m_LastInternalValue))
				{
					global::Unity.Netcode.NetworkVariableSerialization<T>.Duplicate(in m_LastInternalValue, ref m_InternalValue);
				}
				return false;
			}
			if ((!flag || forceCheck) && !global::Unity.Netcode.NetworkVariableSerialization<T>.AreEqual(ref m_LastInternalValue, ref m_InternalValue))
			{
				SetDirty(isDirty: true);
				OnValueChanged?.Invoke(m_LastInternalValue, m_InternalValue);
				m_IsDisposed = false;
				flag = true;
				global::Unity.Netcode.NetworkVariableSerialization<T>.Duplicate(in m_InternalValue, ref m_LastInternalValue);
			}
			return flag;
		}

		internal override void OnCheckIsDirtyState()
		{
			CheckDirtyState();
			base.OnCheckIsDirtyState();
		}

		internal ref T RefValue()
		{
			return ref m_InternalValue;
		}

		public override void Dispose()
		{
			if (!m_IsDisposed)
			{
				m_IsDisposed = true;
				if (m_InternalValue is global::System.IDisposable disposable)
				{
					disposable.Dispose();
				}
				m_InternalValue = default(T);
				if (m_LastInternalValue is global::System.IDisposable disposable2)
				{
					disposable2.Dispose();
				}
				m_LastInternalValue = default(T);
				if (m_HasPreviousValue && m_PreviousValue is global::System.IDisposable disposable3)
				{
					m_HasPreviousValue = false;
					disposable3.Dispose();
				}
				m_PreviousValue = default(T);
				base.Dispose();
			}
		}

		~NetworkVariable()
		{
			Dispose();
		}

		public override bool IsDirty()
		{
			if (!NetworkUpdaterCheck && CannotWrite() && !global::Unity.Netcode.NetworkVariableSerialization<T>.AreEqual(ref m_InternalValue, ref m_LastInternalValue))
			{
				global::Unity.Netcode.NetworkVariableSerialization<T>.Duplicate(in m_LastInternalValue, ref m_InternalValue);
				return true;
			}
			if (base.IsDirty())
			{
				return true;
			}
			bool flag = !global::Unity.Netcode.NetworkVariableSerialization<T>.AreEqual(ref m_PreviousValue, ref m_InternalValue);
			SetDirty(flag);
			return flag;
		}

		public override void ResetDirty()
		{
			if (IsDirty())
			{
				m_HasPreviousValue = true;
				global::Unity.Netcode.NetworkVariableSerialization<T>.Duplicate(in m_InternalValue, ref m_PreviousValue);
				global::Unity.Netcode.NetworkVariableSerialization<T>.Duplicate(in m_InternalValue, ref m_LastInternalValue);
			}
			base.ResetDirty();
		}

		public override void WriteDelta(global::Unity.Netcode.FastBufferWriter writer)
		{
			global::Unity.Netcode.NetworkVariableSerialization<T>.WriteDelta(writer, ref m_InternalValue, ref m_PreviousValue);
		}

		public override void ReadDelta(global::Unity.Netcode.FastBufferReader reader, bool keepDirtyDelta)
		{
			if (CannotWrite() && !global::Unity.Netcode.NetworkVariableSerialization<T>.AreEqual(ref m_LastInternalValue, ref m_InternalValue))
			{
				global::Unity.Netcode.NetworkVariableSerialization<T>.Duplicate(in m_LastInternalValue, ref m_InternalValue);
			}
			global::Unity.Netcode.NetworkVariableSerialization<T>.ReadDelta(reader, ref m_InternalValue);
			if (keepDirtyDelta)
			{
				SetDirty(isDirty: true);
			}
			OnValueChanged?.Invoke(m_PreviousValue, m_InternalValue);
		}

		internal override void PostDeltaRead()
		{
			m_HasPreviousValue = true;
			global::Unity.Netcode.NetworkVariableSerialization<T>.Duplicate(in m_InternalValue, ref m_PreviousValue);
			global::Unity.Netcode.NetworkVariableSerialization<T>.Duplicate(in m_InternalValue, ref m_LastInternalValue);
		}

		public override void ReadField(global::Unity.Netcode.FastBufferReader reader)
		{
			if (CannotWrite() && !global::Unity.Netcode.NetworkVariableSerialization<T>.AreEqual(ref m_LastInternalValue, ref m_InternalValue))
			{
				global::Unity.Netcode.NetworkVariableSerialization<T>.Duplicate(in m_LastInternalValue, ref m_InternalValue);
			}
			global::Unity.Netcode.NetworkVariableSerialization<T>.Read(reader, ref m_InternalValue);
			m_HasPreviousValue = true;
			global::Unity.Netcode.NetworkVariableSerialization<T>.Duplicate(in m_InternalValue, ref m_PreviousValue);
			global::Unity.Netcode.NetworkVariableSerialization<T>.Duplicate(in m_InternalValue, ref m_LastInternalValue);
		}

		public override void WriteField(global::Unity.Netcode.FastBufferWriter writer)
		{
			global::Unity.Netcode.NetworkVariableSerialization<T>.Write(writer, ref m_InternalValue);
		}

		internal override void WriteFieldSynchronization(global::Unity.Netcode.FastBufferWriter writer)
		{
			if (base.IsDirty() && m_HasPreviousValue)
			{
				global::Unity.Netcode.NetworkVariableSerialization<T>.Write(writer, ref m_PreviousValue);
			}
			else
			{
				base.WriteFieldSynchronization(writer);
			}
		}
	}
}
