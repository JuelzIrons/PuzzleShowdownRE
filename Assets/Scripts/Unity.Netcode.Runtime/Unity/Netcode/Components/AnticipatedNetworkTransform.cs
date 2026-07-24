namespace Unity.Netcode.Components
{
	[global::UnityEngine.DisallowMultipleComponent]
	[global::UnityEngine.AddComponentMenu("Netcode/Anticipated Network Transform")]
	[global::UnityEngine.HelpURL("https://docs.unity3d.com/Packages/com.unity.netcode.gameobjects@latest/?subfolder=/manual/advanced-topics/client-anticipation.html")]
	public class AnticipatedNetworkTransform : global::Unity.Netcode.Components.NetworkTransform
	{
		public struct TransformState
		{
			public global::UnityEngine.Vector3 Position;

			public global::UnityEngine.Quaternion Rotation;

			public global::UnityEngine.Vector3 Scale;
		}

		internal class AnticipatedObject : global::Unity.Netcode.IAnticipationEventReceiver, global::Unity.Netcode.IAnticipatedObject
		{
			public global::Unity.Netcode.Components.AnticipatedNetworkTransform Transform;

			public global::Unity.Netcode.NetworkObject OwnerObject => Transform.NetworkObject;

			public void SetupForRender()
			{
				if (Transform.CanCommitToTransform)
				{
					global::UnityEngine.Transform transform = Transform.transform;
					Transform.m_AuthoritativeTransform = new global::Unity.Netcode.Components.AnticipatedNetworkTransform.TransformState
					{
						Position = transform.position,
						Rotation = transform.rotation,
						Scale = transform.localScale
					};
					if (Transform.m_CurrentSmoothTime >= Transform.m_SmoothDuration)
					{
						Transform.m_AnticipatedTransform = Transform.m_AuthoritativeTransform;
					}
					transform.SetPositionAndRotation(Transform.m_AnticipatedTransform.Position, Transform.m_AnticipatedTransform.Rotation);
					transform.localScale = Transform.m_AnticipatedTransform.Scale;
				}
			}

			public void SetupForUpdate()
			{
				if (Transform.CanCommitToTransform)
				{
					global::UnityEngine.Transform transform = Transform.transform;
					transform.SetPositionAndRotation(Transform.m_AuthoritativeTransform.Position, Transform.m_AuthoritativeTransform.Rotation);
					transform.localScale = Transform.m_AuthoritativeTransform.Scale;
				}
			}

			public void Update()
			{
			}

			public void ResetAnticipation()
			{
				Transform.ShouldReanticipate = false;
			}
		}

		private global::Unity.Netcode.Components.AnticipatedNetworkTransform.TransformState m_AuthoritativeTransform;

		private global::Unity.Netcode.Components.AnticipatedNetworkTransform.TransformState m_AnticipatedTransform;

		private global::Unity.Netcode.Components.AnticipatedNetworkTransform.TransformState m_PreviousAnticipatedTransform;

		private ulong m_LastAnticipaionCounter;

		private ulong m_LastAuthorityUpdateCounter;

		private global::Unity.Netcode.Components.AnticipatedNetworkTransform.TransformState m_SmoothFrom;

		private global::Unity.Netcode.Components.AnticipatedNetworkTransform.TransformState m_SmoothTo;

		private float m_SmoothDuration;

		private float m_CurrentSmoothTime;

		private bool m_OutstandingAuthorityChange;

		public global::Unity.Netcode.StaleDataHandling StaleDataHandling = global::Unity.Netcode.StaleDataHandling.Reanticipate;

		private global::Unity.Netcode.Components.AnticipatedNetworkTransform.AnticipatedObject m_AnticipatedObject;

		public global::Unity.Netcode.Components.AnticipatedNetworkTransform.TransformState AuthoritativeState => m_AuthoritativeTransform;

		public global::Unity.Netcode.Components.AnticipatedNetworkTransform.TransformState AnticipatedState => m_AnticipatedTransform;

		public bool ShouldReanticipate { get; private set; }

		public global::Unity.Netcode.Components.AnticipatedNetworkTransform.TransformState PreviousAnticipatedState => m_PreviousAnticipatedTransform;

		public void AnticipateMove(global::UnityEngine.Vector3 newPosition)
		{
			if (!(m_CachedNetworkManager == null) && !m_CachedNetworkManager.ShutdownInProgress && m_CachedNetworkManager.IsListening)
			{
				base.transform.position = newPosition;
				m_AnticipatedTransform.Position = newPosition;
				if (base.CanCommitToTransform)
				{
					m_AuthoritativeTransform.Position = newPosition;
				}
				m_PreviousAnticipatedTransform = m_AnticipatedTransform;
				m_LastAnticipaionCounter = m_CachedNetworkManager.AnticipationSystem.AnticipationCounter;
				m_SmoothDuration = 0f;
				m_CurrentSmoothTime = 0f;
			}
		}

		public void AnticipateRotate(global::UnityEngine.Quaternion newRotation)
		{
			if (!(m_CachedNetworkManager == null) && !m_CachedNetworkManager.ShutdownInProgress && m_CachedNetworkManager.IsListening)
			{
				base.transform.rotation = newRotation;
				m_AnticipatedTransform.Rotation = newRotation;
				if (base.CanCommitToTransform)
				{
					m_AuthoritativeTransform.Rotation = newRotation;
				}
				m_PreviousAnticipatedTransform = m_AnticipatedTransform;
				m_LastAnticipaionCounter = m_CachedNetworkManager.AnticipationSystem.AnticipationCounter;
				m_SmoothDuration = 0f;
				m_CurrentSmoothTime = 0f;
			}
		}

		public void AnticipateScale(global::UnityEngine.Vector3 newScale)
		{
			if (!(m_CachedNetworkManager == null) && !m_CachedNetworkManager.ShutdownInProgress && m_CachedNetworkManager.IsListening)
			{
				base.transform.localScale = newScale;
				m_AnticipatedTransform.Scale = newScale;
				if (base.CanCommitToTransform)
				{
					m_AuthoritativeTransform.Scale = newScale;
				}
				m_PreviousAnticipatedTransform = m_AnticipatedTransform;
				m_LastAnticipaionCounter = m_CachedNetworkManager.AnticipationSystem.AnticipationCounter;
				m_SmoothDuration = 0f;
				m_CurrentSmoothTime = 0f;
			}
		}

		public void AnticipateState(global::Unity.Netcode.Components.AnticipatedNetworkTransform.TransformState newState)
		{
			if (!(m_CachedNetworkManager == null) && !m_CachedNetworkManager.ShutdownInProgress && m_CachedNetworkManager.IsListening)
			{
				global::UnityEngine.Transform obj = base.transform;
				obj.SetPositionAndRotation(newState.Position, newState.Rotation);
				obj.localScale = newState.Scale;
				m_AnticipatedTransform = newState;
				if (base.CanCommitToTransform)
				{
					m_AuthoritativeTransform = newState;
				}
				m_PreviousAnticipatedTransform = m_AnticipatedTransform;
				m_SmoothDuration = 0f;
				m_CurrentSmoothTime = 0f;
			}
		}

		private void ProcessSmoothing()
		{
			if (base.IsSpawned && m_CurrentSmoothTime < m_SmoothDuration)
			{
				m_CurrentSmoothTime += m_CachedNetworkManager.RealTimeProvider.DeltaTime;
				global::UnityEngine.Transform transform = base.transform;
				float t = global::Unity.Mathematics.math.min(m_CurrentSmoothTime / m_SmoothDuration, 1f);
				m_AnticipatedTransform = new global::Unity.Netcode.Components.AnticipatedNetworkTransform.TransformState
				{
					Position = global::UnityEngine.Vector3.Lerp(m_SmoothFrom.Position, m_SmoothTo.Position, t),
					Rotation = global::UnityEngine.Quaternion.Lerp(m_SmoothFrom.Rotation, m_SmoothTo.Rotation, t),
					Scale = global::UnityEngine.Vector3.Lerp(m_SmoothFrom.Scale, m_SmoothTo.Scale, t)
				};
				m_PreviousAnticipatedTransform = m_AnticipatedTransform;
				if (!base.CanCommitToTransform)
				{
					transform.SetPositionAndRotation(m_AnticipatedTransform.Position, m_AnticipatedTransform.Rotation);
					transform.localScale = m_AnticipatedTransform.Scale;
				}
			}
		}

		public override void OnUpdate()
		{
			ProcessSmoothing();
		}

		private void Update()
		{
			if (base.CanCommitToTransform && base.IsSpawned)
			{
				ProcessSmoothing();
			}
		}

		private void ResetAnticipatedState()
		{
			global::UnityEngine.Transform transform = base.transform;
			m_AuthoritativeTransform = new global::Unity.Netcode.Components.AnticipatedNetworkTransform.TransformState
			{
				Position = transform.position,
				Rotation = transform.rotation,
				Scale = transform.localScale
			};
			m_AnticipatedTransform = m_AuthoritativeTransform;
			m_PreviousAnticipatedTransform = m_AnticipatedTransform;
			m_SmoothDuration = 0f;
			m_CurrentSmoothTime = 0f;
		}

		protected internal override void InternalOnNetworkSessionSynchronized()
		{
			bool isSynchronizing = SynchronizeState.IsSynchronizing;
			base.InternalOnNetworkSessionSynchronized();
			if (!base.CanCommitToTransform && isSynchronizing && !SynchronizeState.IsSynchronizing)
			{
				m_OutstandingAuthorityChange = true;
				ApplyAuthoritativeState();
				ResetAnticipatedState();
				m_AnticipatedObject = new global::Unity.Netcode.Components.AnticipatedNetworkTransform.AnticipatedObject
				{
					Transform = this
				};
				m_CachedNetworkManager.AnticipationSystem.RegisterForAnticipationEvents(m_AnticipatedObject);
				m_CachedNetworkManager.AnticipationSystem.AllAnticipatedObjects.Add(m_AnticipatedObject);
			}
		}

		protected internal override void InternalOnNetworkPostSpawn()
		{
			base.InternalOnNetworkPostSpawn();
			if (!base.CanCommitToTransform && m_CachedNetworkManager.IsConnectedClient && !SynchronizeState.IsSynchronizing)
			{
				m_OutstandingAuthorityChange = true;
				ApplyAuthoritativeState();
				ResetAnticipatedState();
				m_AnticipatedObject = new global::Unity.Netcode.Components.AnticipatedNetworkTransform.AnticipatedObject
				{
					Transform = this
				};
				m_CachedNetworkManager.AnticipationSystem.RegisterForAnticipationEvents(m_AnticipatedObject);
				m_CachedNetworkManager.AnticipationSystem.AllAnticipatedObjects.Add(m_AnticipatedObject);
			}
		}

		public override void OnNetworkSpawn()
		{
			m_CachedNetworkManager = base.NetworkManager;
			if (m_CachedNetworkManager.DistributedAuthorityMode)
			{
				global::UnityEngine.Debug.LogWarning("This component is not currently supported in distributed authority.");
			}
			base.OnNetworkSpawn();
			if (!SynchronizeState.IsSynchronizing || base.CanCommitToTransform)
			{
				m_OutstandingAuthorityChange = true;
				ApplyAuthoritativeState();
				ResetAnticipatedState();
				m_AnticipatedObject = new global::Unity.Netcode.Components.AnticipatedNetworkTransform.AnticipatedObject
				{
					Transform = this
				};
				m_CachedNetworkManager.AnticipationSystem.RegisterForAnticipationEvents(m_AnticipatedObject);
				m_CachedNetworkManager.AnticipationSystem.AllAnticipatedObjects.Add(m_AnticipatedObject);
			}
		}

		public override void OnNetworkDespawn()
		{
			if (m_AnticipatedObject != null)
			{
				m_CachedNetworkManager.AnticipationSystem.DeregisterForAnticipationEvents(m_AnticipatedObject);
				m_CachedNetworkManager.AnticipationSystem.AllAnticipatedObjects.Remove(m_AnticipatedObject);
				m_CachedNetworkManager.AnticipationSystem.ObjectsToReanticipate.Remove(m_AnticipatedObject);
				m_AnticipatedObject = null;
			}
			ResetAnticipatedState();
			base.OnNetworkDespawn();
		}

		public override void OnDestroy()
		{
			if (m_AnticipatedObject != null)
			{
				m_CachedNetworkManager.AnticipationSystem.DeregisterForAnticipationEvents(m_AnticipatedObject);
				m_CachedNetworkManager.AnticipationSystem.AllAnticipatedObjects.Remove(m_AnticipatedObject);
				m_CachedNetworkManager.AnticipationSystem.ObjectsToReanticipate.Remove(m_AnticipatedObject);
				m_AnticipatedObject = null;
			}
			base.OnDestroy();
		}

		public void Smooth(global::Unity.Netcode.Components.AnticipatedNetworkTransform.TransformState from, global::Unity.Netcode.Components.AnticipatedNetworkTransform.TransformState to, float durationSeconds)
		{
			global::UnityEngine.Transform transform = base.transform;
			if (durationSeconds <= 0f)
			{
				m_AnticipatedTransform = to;
				m_PreviousAnticipatedTransform = m_AnticipatedTransform;
				transform.SetPositionAndRotation(to.Position, to.Rotation);
				transform.localScale = to.Scale;
				m_SmoothDuration = 0f;
				m_CurrentSmoothTime = 0f;
				return;
			}
			m_AnticipatedTransform = from;
			m_PreviousAnticipatedTransform = m_AnticipatedTransform;
			if (!base.CanCommitToTransform)
			{
				transform.SetPositionAndRotation(from.Position, from.Rotation);
				transform.localScale = from.Scale;
			}
			m_SmoothFrom = from;
			m_SmoothTo = to;
			m_SmoothDuration = durationSeconds;
			m_CurrentSmoothTime = 0f;
		}

		protected override void OnBeforeUpdateTransformState()
		{
			m_LastAuthorityUpdateCounter = m_CachedNetworkManager.AnticipationSystem.LastAnticipationAck;
			m_OutstandingAuthorityChange = true;
		}

		protected override void OnNetworkTransformStateUpdated(ref global::Unity.Netcode.Components.NetworkTransform.NetworkTransformState oldState, ref global::Unity.Netcode.Components.NetworkTransform.NetworkTransformState newState)
		{
			base.OnNetworkTransformStateUpdated(ref oldState, ref newState);
			ApplyAuthoritativeState();
		}

		protected override void OnTransformUpdated()
		{
			if (!base.CanCommitToTransform && m_AnticipatedObject != null)
			{
				global::UnityEngine.Transform transform = base.transform;
				global::Unity.Netcode.Components.AnticipatedNetworkTransform.TransformState anticipatedTransform = m_AnticipatedTransform;
				m_AuthoritativeTransform.Position = transform.position;
				m_AuthoritativeTransform.Rotation = transform.rotation;
				m_AuthoritativeTransform.Scale = transform.localScale;
				if (!m_OutstandingAuthorityChange)
				{
					transform.SetPositionAndRotation(anticipatedTransform.Position, anticipatedTransform.Rotation);
					transform.localScale = anticipatedTransform.Scale;
					return;
				}
				if (StaleDataHandling == global::Unity.Netcode.StaleDataHandling.Ignore && m_LastAnticipaionCounter > m_LastAuthorityUpdateCounter)
				{
					transform.SetPositionAndRotation(anticipatedTransform.Position, anticipatedTransform.Rotation);
					transform.localScale = anticipatedTransform.Scale;
					return;
				}
				m_SmoothDuration = 0f;
				m_CurrentSmoothTime = 0f;
				m_OutstandingAuthorityChange = false;
				m_AnticipatedTransform = m_AuthoritativeTransform;
				ShouldReanticipate = true;
				m_CachedNetworkManager.AnticipationSystem.ObjectsToReanticipate.Add(m_AnticipatedObject);
			}
		}

		protected override void __initializeVariables()
		{
			base.__initializeVariables();
		}

		protected override void __initializeRpcs()
		{
			base.__initializeRpcs();
		}

		protected internal override string __getTypeName()
		{
			return "AnticipatedNetworkTransform";
		}
	}
}
