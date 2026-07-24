namespace Unity.Netcode
{
	[global::System.Serializable]
	[global::Unity.Netcode.GenerateSerializationForGenericParameter(0)]
	public class AnticipatedNetworkVariable<T> : global::Unity.Netcode.NetworkVariableBase
	{
		public delegate void OnAuthoritativeValueChangedDelegate(global::Unity.Netcode.AnticipatedNetworkVariable<T> variable, in T previousValue, in T newValue);

		private class AnticipatedObject : global::Unity.Netcode.IAnticipatedObject
		{
			public global::Unity.Netcode.AnticipatedNetworkVariable<T> Variable;

			public global::Unity.Netcode.NetworkObject OwnerObject => Variable.m_NetworkBehaviour.NetworkObject;

			public void Update()
			{
				Variable.Update();
			}

			public void ResetAnticipation()
			{
				Variable.ShouldReanticipate = false;
			}
		}

		public delegate T SmoothDelegate(T authoritativeValue, T anticipatedValue, float amount);

		[global::UnityEngine.SerializeField]
		private global::Unity.Netcode.NetworkVariable<T> m_AuthoritativeValue;

		private T m_AnticipatedValue;

		private T m_PreviousAnticipatedValue;

		private ulong m_LastAuthorityUpdateCounter;

		private ulong m_LastAnticipationCounter;

		private bool m_IsDisposed;

		private bool m_SettingAuthoritativeValue;

		private T m_SmoothFrom;

		private T m_SmoothTo;

		private float m_SmoothDuration;

		private float m_CurrentSmoothTime;

		private bool m_HasSmoothValues;

		public global::Unity.Netcode.StaleDataHandling StaleDataHandling;

		public global::Unity.Netcode.AnticipatedNetworkVariable<T>.OnAuthoritativeValueChangedDelegate OnAuthoritativeValueChanged;

		private global::Unity.Netcode.AnticipatedNetworkVariable<T>.AnticipatedObject m_AnticipatedObject;

		private global::Unity.Netcode.AnticipatedNetworkVariable<T>.SmoothDelegate m_SmoothDelegate;

		public T Value => m_AnticipatedValue;

		public bool ShouldReanticipate { get; private set; }

		public T PreviousAnticipatedValue => m_PreviousAnticipatedValue;

		public T AuthoritativeValue
		{
			get
			{
				return m_AuthoritativeValue.Value;
			}
			set
			{
				m_SettingAuthoritativeValue = true;
				try
				{
					m_AuthoritativeValue.Value = value;
					m_AnticipatedValue = value;
					global::Unity.Netcode.NetworkVariableSerialization<T>.Duplicate(in m_AnticipatedValue, ref m_PreviousAnticipatedValue);
				}
				finally
				{
					m_SettingAuthoritativeValue = false;
				}
			}
		}

		public event global::Unity.Netcode.NetworkVariable<T>.CheckExceedsDirtinessThresholdDelegate CheckExceedsDirtinessThreshold
		{
			add
			{
				global::Unity.Netcode.NetworkVariable<T> authoritativeValue = m_AuthoritativeValue;
				authoritativeValue.CheckExceedsDirtinessThreshold = (global::Unity.Netcode.NetworkVariable<T>.CheckExceedsDirtinessThresholdDelegate)global::System.Delegate.Combine(authoritativeValue.CheckExceedsDirtinessThreshold, value);
			}
			remove
			{
				global::Unity.Netcode.NetworkVariable<T> authoritativeValue = m_AuthoritativeValue;
				authoritativeValue.CheckExceedsDirtinessThreshold = (global::Unity.Netcode.NetworkVariable<T>.CheckExceedsDirtinessThresholdDelegate)global::System.Delegate.Remove(authoritativeValue.CheckExceedsDirtinessThreshold, value);
			}
		}

		public override void OnInitialize()
		{
			m_AuthoritativeValue.Initialize(m_NetworkBehaviour);
			global::Unity.Netcode.NetworkVariableSerialization<T>.Duplicate(m_AuthoritativeValue.Value, ref m_AnticipatedValue);
			global::Unity.Netcode.NetworkVariableSerialization<T>.Duplicate(in m_AnticipatedValue, ref m_PreviousAnticipatedValue);
			if (m_NetworkBehaviour != null && m_NetworkBehaviour.NetworkManager != null && m_NetworkBehaviour.NetworkManager.AnticipationSystem != null)
			{
				m_AnticipatedObject = new global::Unity.Netcode.AnticipatedNetworkVariable<T>.AnticipatedObject
				{
					Variable = this
				};
				m_NetworkBehaviour.NetworkManager.AnticipationSystem.AllAnticipatedObjects.Add(m_AnticipatedObject);
			}
		}

		public override bool ExceedsDirtinessThreshold()
		{
			return m_AuthoritativeValue.ExceedsDirtinessThreshold();
		}

		public void Anticipate(T value)
		{
			if (!m_NetworkBehaviour.NetworkManager.ShutdownInProgress && m_NetworkBehaviour.NetworkManager.IsListening)
			{
				m_SmoothDuration = 0f;
				m_CurrentSmoothTime = 0f;
				m_LastAnticipationCounter = m_NetworkBehaviour.NetworkManager.AnticipationSystem.AnticipationCounter;
				m_AnticipatedValue = value;
				global::Unity.Netcode.NetworkVariableSerialization<T>.Duplicate(in m_AnticipatedValue, ref m_PreviousAnticipatedValue);
				if (CanWrite())
				{
					AuthoritativeValue = value;
				}
			}
		}

		public AnticipatedNetworkVariable(T value = default(T), global::Unity.Netcode.StaleDataHandling staleDataHandling = global::Unity.Netcode.StaleDataHandling.Ignore)
		{
			StaleDataHandling = staleDataHandling;
			m_AuthoritativeValue = new global::Unity.Netcode.NetworkVariable<T>(value)
			{
				OnValueChanged = OnValueChangedInternal
			};
		}

		public void Update()
		{
			if (m_CurrentSmoothTime < m_SmoothDuration)
			{
				m_CurrentSmoothTime += m_NetworkBehaviour.NetworkManager.RealTimeProvider.DeltaTime;
				float amount = global::Unity.Mathematics.math.min(m_CurrentSmoothTime / m_SmoothDuration, 1f);
				m_AnticipatedValue = m_SmoothDelegate(m_SmoothFrom, m_SmoothTo, amount);
				global::Unity.Netcode.NetworkVariableSerialization<T>.Duplicate(in m_AnticipatedValue, ref m_PreviousAnticipatedValue);
			}
		}

		public override void Dispose()
		{
			if (m_IsDisposed)
			{
				return;
			}
			if (m_NetworkBehaviour != null && m_NetworkBehaviour.NetworkManager != null && m_NetworkBehaviour.NetworkManager.AnticipationSystem != null && m_AnticipatedObject != null)
			{
				m_NetworkBehaviour.NetworkManager.AnticipationSystem.AllAnticipatedObjects.Remove(m_AnticipatedObject);
				m_NetworkBehaviour.NetworkManager.AnticipationSystem.ObjectsToReanticipate.Remove(m_AnticipatedObject);
				m_AnticipatedObject = null;
			}
			m_IsDisposed = true;
			m_AuthoritativeValue.Dispose();
			if (m_AnticipatedValue is global::System.IDisposable disposable)
			{
				disposable.Dispose();
			}
			m_AnticipatedValue = default(T);
			if (m_PreviousAnticipatedValue is global::System.IDisposable disposable2)
			{
				disposable2.Dispose();
				m_PreviousAnticipatedValue = default(T);
			}
			if (m_HasSmoothValues)
			{
				if (m_SmoothFrom is global::System.IDisposable disposable3)
				{
					disposable3.Dispose();
					m_SmoothFrom = default(T);
				}
				if (m_SmoothTo is global::System.IDisposable disposable4)
				{
					disposable4.Dispose();
					m_SmoothTo = default(T);
				}
				m_HasSmoothValues = false;
			}
		}

		~AnticipatedNetworkVariable()
		{
			Dispose();
		}

		private void OnValueChangedInternal(T previousValue, T newValue)
		{
			if (!m_SettingAuthoritativeValue)
			{
				m_LastAuthorityUpdateCounter = m_NetworkBehaviour.NetworkManager.AnticipationSystem.LastAnticipationAck;
				if (StaleDataHandling == global::Unity.Netcode.StaleDataHandling.Ignore && m_LastAnticipationCounter > m_LastAuthorityUpdateCounter)
				{
					return;
				}
				ShouldReanticipate = true;
				m_NetworkBehaviour.NetworkManager.AnticipationSystem.ObjectsToReanticipate.Add(m_AnticipatedObject);
			}
			global::Unity.Netcode.NetworkVariableSerialization<T>.Duplicate(AuthoritativeValue, ref m_AnticipatedValue);
			m_SmoothDuration = 0f;
			m_CurrentSmoothTime = 0f;
			OnAuthoritativeValueChanged?.Invoke(this, in previousValue, in newValue);
		}

		public void Smooth(in T from, in T to, float durationSeconds, global::Unity.Netcode.AnticipatedNetworkVariable<T>.SmoothDelegate how)
		{
			if (durationSeconds <= 0f)
			{
				global::Unity.Netcode.NetworkVariableSerialization<T>.Duplicate(in to, ref m_AnticipatedValue);
				m_SmoothDuration = 0f;
				m_CurrentSmoothTime = 0f;
				m_SmoothDelegate = null;
			}
			else
			{
				global::Unity.Netcode.NetworkVariableSerialization<T>.Duplicate(in from, ref m_AnticipatedValue);
				global::Unity.Netcode.NetworkVariableSerialization<T>.Duplicate(in from, ref m_SmoothFrom);
				global::Unity.Netcode.NetworkVariableSerialization<T>.Duplicate(in to, ref m_SmoothTo);
				m_SmoothDuration = durationSeconds;
				m_CurrentSmoothTime = 0f;
				m_SmoothDelegate = how;
				m_HasSmoothValues = true;
			}
		}

		public override bool IsDirty()
		{
			return m_AuthoritativeValue.IsDirty();
		}

		public override void ResetDirty()
		{
			m_AuthoritativeValue.ResetDirty();
		}

		public override void WriteDelta(global::Unity.Netcode.FastBufferWriter writer)
		{
			m_AuthoritativeValue.WriteDelta(writer);
		}

		public override void WriteField(global::Unity.Netcode.FastBufferWriter writer)
		{
			m_AuthoritativeValue.WriteField(writer);
		}

		public override void ReadField(global::Unity.Netcode.FastBufferReader reader)
		{
			m_AuthoritativeValue.ReadField(reader);
			global::Unity.Netcode.NetworkVariableSerialization<T>.Duplicate(m_AuthoritativeValue.Value, ref m_AnticipatedValue);
			global::Unity.Netcode.NetworkVariableSerialization<T>.Duplicate(in m_AnticipatedValue, ref m_PreviousAnticipatedValue);
		}

		public override void ReadDelta(global::Unity.Netcode.FastBufferReader reader, bool keepDirtyDelta)
		{
			m_AuthoritativeValue.ReadDelta(reader, keepDirtyDelta);
			m_AuthoritativeValue.PostDeltaRead();
		}
	}
}
