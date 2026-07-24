namespace Unity.Netcode.Components
{
	public abstract class NetworkRigidbodyBase : global::Unity.Netcode.NetworkBehaviour
	{
		private enum InterpolationTypes
		{
			None = 0,
			Interpolate = 1,
			Extrapolate = 2
		}

		public enum RigidbodyTypes
		{
			Rigidbody = 0,
			Rigidbody2D = 1
		}

		[global::UnityEngine.Tooltip("When enabled and a NetworkTransform component is attached, the NetworkTransform will use the rigid body for motion and detecting changes in state.")]
		public bool UseRigidBodyForMotion;

		public bool AutoUpdateKinematicState = true;

		public bool AutoSetKinematicOnDespawn = true;

		private bool m_IsAuthority;

		internal global::Unity.Netcode.Components.NetworkTransform NetworkTransform;

		private float m_TickFrequency;

		private float m_TickRate;

		private global::Unity.Netcode.Components.NetworkRigidbodyBase.InterpolationTypes m_OriginalInterpolation;

		private global::UnityEngine.Vector4 m_QuaternionCheck = global::UnityEngine.Vector4.zero;

		internal global::System.Collections.Generic.List<global::Unity.Netcode.Components.NetworkRigidbodyBase> NetworkRigidbodyConnections = new global::System.Collections.Generic.List<global::Unity.Netcode.Components.NetworkRigidbodyBase>();

		internal global::Unity.Netcode.Components.NetworkRigidbodyBase ParentBody;

		private bool m_FixedJoint2DUsingGravity;

		private bool m_OriginalGravitySetting;

		private float m_OriginalGravityScale;

		private bool m_IsRigidbody2D => RigidbodyType == global::Unity.Netcode.Components.NetworkRigidbodyBase.RigidbodyTypes.Rigidbody2D;

		protected internal global::UnityEngine.Rigidbody m_InternalRigidbody { get; private set; }

		protected internal global::UnityEngine.Rigidbody2D m_InternalRigidbody2D { get; private set; }

		public global::Unity.Netcode.Components.NetworkRigidbodyBase.RigidbodyTypes RigidbodyType { get; private set; }

		public global::UnityEngine.FixedJoint2D FixedJoint2D { get; private set; }

		public global::UnityEngine.FixedJoint FixedJoint { get; private set; }

		protected void Initialize(global::Unity.Netcode.Components.NetworkRigidbodyBase.RigidbodyTypes rigidbodyType, global::Unity.Netcode.Components.NetworkTransform networkTransform = null, global::UnityEngine.Rigidbody2D rigidbody2D = null, global::UnityEngine.Rigidbody rigidbody = null)
		{
			if (base.IsSpawned)
			{
				global::UnityEngine.Debug.LogError("[" + base.name + "] Attempting to initialize while spawned is not allowed.");
				return;
			}
			RigidbodyType = rigidbodyType;
			m_InternalRigidbody2D = rigidbody2D;
			m_InternalRigidbody = rigidbody;
			NetworkTransform = networkTransform;
			if (m_IsRigidbody2D && m_InternalRigidbody2D == null)
			{
				m_InternalRigidbody2D = GetComponent<global::UnityEngine.Rigidbody2D>();
			}
			else if (m_InternalRigidbody == null)
			{
				m_InternalRigidbody = GetComponent<global::UnityEngine.Rigidbody>();
			}
			SetOriginalInterpolation();
			if (NetworkTransform == null)
			{
				NetworkTransform = GetComponent<global::Unity.Netcode.Components.NetworkTransform>();
			}
			if (NetworkTransform != null)
			{
				NetworkTransform.RegisterRigidbody(this);
				if (AutoUpdateKinematicState)
				{
					SetIsKinematic(isKinematic: true);
				}
				return;
			}
			throw new global::System.Exception("[Missing NetworkTransform] No NetworkTransform is assigned or can be found during initialization!");
		}

		internal global::UnityEngine.Vector3 GetAdjustedPositionThreshold()
		{
			float max = NetworkTransform.PositionThreshold * m_TickRate;
			global::UnityEngine.Vector3 result = GetLinearVelocity() * m_TickFrequency;
			float min = NetworkTransform.PositionThreshold * 0.1f;
			result.x = global::UnityEngine.Mathf.Clamp(global::UnityEngine.Mathf.Abs(result.x), min, max);
			result.y = global::UnityEngine.Mathf.Clamp(global::UnityEngine.Mathf.Abs(result.y), min, max);
			if (!m_IsRigidbody2D)
			{
				result.z = global::UnityEngine.Mathf.Clamp(global::UnityEngine.Mathf.Abs(result.z), min, max);
			}
			return result;
		}

		internal global::UnityEngine.Vector3 GetAdjustedRotationThreshold()
		{
			float max = NetworkTransform.RotAngleThreshold * m_TickRate;
			global::UnityEngine.Vector3 result = GetAngularVelocity() * 57.29578f * m_TickFrequency;
			float min = NetworkTransform.RotAngleThreshold * m_TickFrequency;
			if (!m_IsRigidbody2D)
			{
				result.x = global::UnityEngine.Mathf.Clamp(global::UnityEngine.Mathf.Abs(result.x), min, max);
				result.y = global::UnityEngine.Mathf.Clamp(global::UnityEngine.Mathf.Abs(result.y), min, max);
			}
			result.z = global::UnityEngine.Mathf.Clamp(global::UnityEngine.Mathf.Abs(result.z), min, max);
			return result;
		}

		public void SetLinearVelocity(global::UnityEngine.Vector3 linearVelocity)
		{
			if (m_IsRigidbody2D)
			{
				m_InternalRigidbody2D.linearVelocity = linearVelocity;
			}
			else
			{
				m_InternalRigidbody.linearVelocity = linearVelocity;
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public global::UnityEngine.Vector3 GetLinearVelocity()
		{
			if (m_IsRigidbody2D)
			{
				return m_InternalRigidbody2D.linearVelocity;
			}
			return m_InternalRigidbody.linearVelocity;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void SetAngularVelocity(global::UnityEngine.Vector3 angularVelocity)
		{
			if (m_IsRigidbody2D)
			{
				m_InternalRigidbody2D.angularVelocity = angularVelocity.z;
			}
			else
			{
				m_InternalRigidbody.angularVelocity = angularVelocity;
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public global::UnityEngine.Vector3 GetAngularVelocity()
		{
			if (m_IsRigidbody2D)
			{
				return global::UnityEngine.Vector3.forward * m_InternalRigidbody2D.angularVelocity;
			}
			return m_InternalRigidbody.angularVelocity;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public global::UnityEngine.Vector3 GetPosition()
		{
			if (m_IsRigidbody2D)
			{
				return m_InternalRigidbody2D.position;
			}
			return m_InternalRigidbody.position;
		}

		private global::UnityEngine.Quaternion Rotation2D()
		{
			global::UnityEngine.Quaternion identity = global::UnityEngine.Quaternion.identity;
			global::UnityEngine.Vector3 eulerAngles = identity.eulerAngles;
			eulerAngles.z = m_InternalRigidbody2D.rotation;
			identity.eulerAngles = eulerAngles;
			return identity;
		}

		private global::UnityEngine.Quaternion Rotation()
		{
			return m_InternalRigidbody.rotation;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public global::UnityEngine.Quaternion GetRotation()
		{
			if (!m_IsRigidbody2D)
			{
				return Rotation();
			}
			return Rotation2D();
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void MovePosition(global::UnityEngine.Vector3 position)
		{
			if (m_IsRigidbody2D)
			{
				m_InternalRigidbody2D.MovePosition(position);
			}
			else
			{
				m_InternalRigidbody.MovePosition(position);
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void SetPosition(global::UnityEngine.Vector3 position)
		{
			if (m_IsRigidbody2D)
			{
				m_InternalRigidbody2D.position = position;
			}
			else
			{
				m_InternalRigidbody.position = position;
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void ApplyCurrentTransform()
		{
			if (m_IsRigidbody2D)
			{
				m_InternalRigidbody2D.position = base.transform.position;
				m_InternalRigidbody2D.rotation = base.transform.eulerAngles.z;
			}
			else
			{
				m_InternalRigidbody.position = base.transform.position;
				m_InternalRigidbody.rotation = base.transform.rotation;
			}
		}

		private void InternalMoveRotation2D(global::UnityEngine.Quaternion rotation)
		{
			global::UnityEngine.Quaternion identity = global::UnityEngine.Quaternion.identity;
			global::UnityEngine.Vector3 eulerAngles = identity.eulerAngles;
			eulerAngles.z = m_InternalRigidbody2D.rotation;
			identity.eulerAngles = eulerAngles;
			m_InternalRigidbody2D.MoveRotation(identity);
		}

		private void InternalMoveRotation(global::UnityEngine.Quaternion rotation)
		{
			m_QuaternionCheck.x = rotation.x;
			m_QuaternionCheck.y = rotation.y;
			m_QuaternionCheck.z = rotation.z;
			m_QuaternionCheck.w = rotation.w;
			if (m_QuaternionCheck.magnitude != 1f)
			{
				rotation.Normalize();
			}
			m_InternalRigidbody.MoveRotation(rotation);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void MoveRotation(global::UnityEngine.Quaternion rotation)
		{
			if (m_IsRigidbody2D)
			{
				InternalMoveRotation2D(rotation);
			}
			else
			{
				InternalMoveRotation(rotation);
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void SetRotation(global::UnityEngine.Quaternion rotation)
		{
			if (m_IsRigidbody2D)
			{
				m_InternalRigidbody2D.rotation = rotation.eulerAngles.z;
			}
			else
			{
				m_InternalRigidbody.rotation = rotation;
			}
		}

		private void SetOriginalInterpolation2D()
		{
			switch (m_InternalRigidbody2D.interpolation)
			{
			case global::UnityEngine.RigidbodyInterpolation2D.None:
				m_OriginalInterpolation = global::Unity.Netcode.Components.NetworkRigidbodyBase.InterpolationTypes.None;
				break;
			case global::UnityEngine.RigidbodyInterpolation2D.Interpolate:
				m_OriginalInterpolation = global::Unity.Netcode.Components.NetworkRigidbodyBase.InterpolationTypes.Interpolate;
				break;
			case global::UnityEngine.RigidbodyInterpolation2D.Extrapolate:
				m_OriginalInterpolation = global::Unity.Netcode.Components.NetworkRigidbodyBase.InterpolationTypes.Extrapolate;
				break;
			}
		}

		private void SetOriginalInterpolation3D()
		{
			switch (m_InternalRigidbody.interpolation)
			{
			case global::UnityEngine.RigidbodyInterpolation.None:
				m_OriginalInterpolation = global::Unity.Netcode.Components.NetworkRigidbodyBase.InterpolationTypes.None;
				break;
			case global::UnityEngine.RigidbodyInterpolation.Interpolate:
				m_OriginalInterpolation = global::Unity.Netcode.Components.NetworkRigidbodyBase.InterpolationTypes.Interpolate;
				break;
			case global::UnityEngine.RigidbodyInterpolation.Extrapolate:
				m_OriginalInterpolation = global::Unity.Netcode.Components.NetworkRigidbodyBase.InterpolationTypes.Extrapolate;
				break;
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		private void SetOriginalInterpolation()
		{
			if (m_IsRigidbody2D)
			{
				SetOriginalInterpolation2D();
			}
			else
			{
				SetOriginalInterpolation3D();
			}
		}

		private void WakeIfSleeping2D()
		{
			if (m_InternalRigidbody2D.IsSleeping())
			{
				m_InternalRigidbody2D.WakeUp();
			}
		}

		private void WakeIfSleeping3D()
		{
			if (m_InternalRigidbody.IsSleeping())
			{
				m_InternalRigidbody.WakeUp();
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void WakeIfSleeping()
		{
			if (m_IsRigidbody2D)
			{
				WakeIfSleeping2D();
			}
			else
			{
				WakeIfSleeping3D();
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void SleepRigidbody()
		{
			if (m_IsRigidbody2D)
			{
				m_InternalRigidbody2D.Sleep();
			}
			else
			{
				m_InternalRigidbody.Sleep();
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public bool IsKinematic()
		{
			if (m_IsRigidbody2D)
			{
				return m_InternalRigidbody2D.bodyType == global::UnityEngine.RigidbodyType2D.Kinematic;
			}
			return m_InternalRigidbody.isKinematic;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void SetIsKinematic(bool isKinematic)
		{
			if (m_IsRigidbody2D)
			{
				m_InternalRigidbody2D.bodyType = (isKinematic ? global::UnityEngine.RigidbodyType2D.Kinematic : global::UnityEngine.RigidbodyType2D.Dynamic);
			}
			else
			{
				m_InternalRigidbody.isKinematic = isKinematic;
			}
			PostSetIsKinematic();
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		private void PostSetIsKinematic()
		{
			if (!base.IsSpawned)
			{
				return;
			}
			if (UseRigidBodyForMotion)
			{
				if (!NetworkTransform.Interpolate || m_OriginalInterpolation != global::Unity.Netcode.Components.NetworkRigidbodyBase.InterpolationTypes.Extrapolate)
				{
					return;
				}
				if (IsKinematic())
				{
					if (m_InternalRigidbody.interpolation == global::UnityEngine.RigidbodyInterpolation.Extrapolate)
					{
						SleepRigidbody();
						SetInterpolation(global::Unity.Netcode.Components.NetworkRigidbodyBase.InterpolationTypes.Interpolate);
					}
				}
				else
				{
					SetInterpolation(m_OriginalInterpolation);
				}
			}
			else
			{
				SetInterpolation(m_IsAuthority ? m_OriginalInterpolation : ((!NetworkTransform.Interpolate) ? m_OriginalInterpolation : global::Unity.Netcode.Components.NetworkRigidbodyBase.InterpolationTypes.None));
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		private void SetInterpolation2D(global::Unity.Netcode.Components.NetworkRigidbodyBase.InterpolationTypes interpolationType)
		{
			switch (interpolationType)
			{
			case global::Unity.Netcode.Components.NetworkRigidbodyBase.InterpolationTypes.None:
				m_InternalRigidbody2D.interpolation = global::UnityEngine.RigidbodyInterpolation2D.None;
				break;
			case global::Unity.Netcode.Components.NetworkRigidbodyBase.InterpolationTypes.Interpolate:
				m_InternalRigidbody2D.interpolation = global::UnityEngine.RigidbodyInterpolation2D.Interpolate;
				break;
			case global::Unity.Netcode.Components.NetworkRigidbodyBase.InterpolationTypes.Extrapolate:
				m_InternalRigidbody2D.interpolation = global::UnityEngine.RigidbodyInterpolation2D.Extrapolate;
				break;
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		private void SetInterpolation3D(global::Unity.Netcode.Components.NetworkRigidbodyBase.InterpolationTypes interpolationType)
		{
			switch (interpolationType)
			{
			case global::Unity.Netcode.Components.NetworkRigidbodyBase.InterpolationTypes.None:
				m_InternalRigidbody.interpolation = global::UnityEngine.RigidbodyInterpolation.None;
				break;
			case global::Unity.Netcode.Components.NetworkRigidbodyBase.InterpolationTypes.Interpolate:
				m_InternalRigidbody.interpolation = global::UnityEngine.RigidbodyInterpolation.Interpolate;
				break;
			case global::Unity.Netcode.Components.NetworkRigidbodyBase.InterpolationTypes.Extrapolate:
				m_InternalRigidbody.interpolation = global::UnityEngine.RigidbodyInterpolation.Extrapolate;
				break;
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		private void SetInterpolation(global::Unity.Netcode.Components.NetworkRigidbodyBase.InterpolationTypes interpolationType)
		{
			if (m_IsRigidbody2D)
			{
				SetInterpolation2D(interpolationType);
			}
			else
			{
				SetInterpolation3D(interpolationType);
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void ResetInterpolation()
		{
			SetInterpolation(m_OriginalInterpolation);
		}

		protected override void OnOwnershipChanged(ulong previous, ulong current)
		{
			UpdateOwnershipAuthority();
			base.OnOwnershipChanged(previous, current);
		}

		internal void UpdateOwnershipAuthority()
		{
			if (base.NetworkManager.DistributedAuthorityMode)
			{
				m_IsAuthority = base.HasAuthority;
			}
			else if (NetworkTransform.IsServerAuthoritative())
			{
				m_IsAuthority = base.NetworkManager.IsServer;
			}
			else
			{
				m_IsAuthority = base.IsOwner;
			}
			if (AutoUpdateKinematicState)
			{
				SetIsKinematic(!m_IsAuthority);
			}
		}

		public override void OnNetworkSpawn()
		{
			m_TickFrequency = 1f / (float)base.NetworkManager.NetworkConfig.TickRate;
			m_TickRate = base.NetworkManager.NetworkConfig.TickRate;
			UpdateOwnershipAuthority();
		}

		public override void OnNetworkDespawn()
		{
			if (UseRigidBodyForMotion && base.HasAuthority)
			{
				DetachFromFixedJoint();
				NetworkRigidbodyConnections.Clear();
			}
			if (AutoUpdateKinematicState || AutoSetKinematicOnDespawn)
			{
				SetIsKinematic(isKinematic: true);
			}
			SetInterpolation(m_OriginalInterpolation);
		}

		protected virtual void OnFixedJointCreated()
		{
		}

		protected virtual void OnFixedJoint2DCreated()
		{
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		private void ApplyFixedJoint2D(global::Unity.Netcode.Components.NetworkRigidbodyBase bodyToConnect, global::UnityEngine.Vector3 position, float connectedMassScale = 0f, float massScale = 1f, bool useGravity = false, bool zeroVelocity = true)
		{
			base.transform.position = position;
			m_InternalRigidbody2D.position = position;
			m_OriginalGravitySetting = bodyToConnect.m_InternalRigidbody.useGravity;
			m_FixedJoint2DUsingGravity = useGravity;
			if (!useGravity)
			{
				m_OriginalGravityScale = m_InternalRigidbody2D.gravityScale;
				m_InternalRigidbody2D.gravityScale = 0f;
			}
			if (zeroVelocity)
			{
				m_InternalRigidbody2D.linearVelocity = global::UnityEngine.Vector2.zero;
				m_InternalRigidbody2D.angularVelocity = 0f;
			}
			FixedJoint2D = base.gameObject.AddComponent<global::UnityEngine.FixedJoint2D>();
			FixedJoint2D.connectedBody = bodyToConnect.m_InternalRigidbody2D;
			OnFixedJoint2DCreated();
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		private void ApplyFixedJoint(global::Unity.Netcode.Components.NetworkRigidbodyBase bodyToConnectTo, global::UnityEngine.Vector3 position, float connectedMassScale = 0f, float massScale = 1f, bool useGravity = false, bool zeroVelocity = true)
		{
			base.transform.position = position;
			m_InternalRigidbody.position = position;
			if (zeroVelocity)
			{
				m_InternalRigidbody.linearVelocity = global::UnityEngine.Vector3.zero;
				m_InternalRigidbody.angularVelocity = global::UnityEngine.Vector3.zero;
			}
			m_OriginalGravitySetting = m_InternalRigidbody.useGravity;
			m_InternalRigidbody.useGravity = useGravity;
			FixedJoint = base.gameObject.AddComponent<global::UnityEngine.FixedJoint>();
			FixedJoint.connectedBody = bodyToConnectTo.m_InternalRigidbody;
			FixedJoint.connectedMassScale = connectedMassScale;
			FixedJoint.massScale = massScale;
			OnFixedJointCreated();
		}

		public bool AttachToFixedJoint(global::Unity.Netcode.Components.NetworkRigidbodyBase objectToConnectTo, global::UnityEngine.Vector3 positionOfConnection, float connectedMassScale = 0f, float massScale = 1f, bool useGravity = false, bool zeroVelocity = true, bool teleportObject = true)
		{
			if (!UseRigidBodyForMotion)
			{
				global::UnityEngine.Debug.LogError("[" + GetType().Name + "] " + base.name + " does not have UseRigidBodyForMotion set! Either enable UseRigidBodyForMotion on this component or do not use a FixedJoint when parenting under a NetworkObject.");
				return false;
			}
			if (IsKinematic())
			{
				global::UnityEngine.Debug.LogError("[" + GetType().Name + "] " + base.name + " is currently kinematic! You cannot use a FixedJoint with Kinematic bodies!");
				return false;
			}
			if (objectToConnectTo != null)
			{
				if (m_IsRigidbody2D)
				{
					ApplyFixedJoint2D(objectToConnectTo, positionOfConnection, connectedMassScale, massScale, useGravity, zeroVelocity);
				}
				else
				{
					ApplyFixedJoint(objectToConnectTo, positionOfConnection, connectedMassScale, massScale, useGravity, zeroVelocity);
				}
				ParentBody = objectToConnectTo;
				ParentBody.NetworkRigidbodyConnections.Add(this);
				if (teleportObject)
				{
					NetworkTransform.SetState(null, null, null, teleportDisabled: false);
				}
				return true;
			}
			return false;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		private void RemoveFromParentBody()
		{
			ParentBody.NetworkRigidbodyConnections.Remove(this);
			ParentBody = null;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		private void DetachFromFixedJoint2D()
		{
			if (!(FixedJoint2D == null))
			{
				if (!m_FixedJoint2DUsingGravity)
				{
					FixedJoint2D.connectedBody.gravityScale = m_OriginalGravityScale;
				}
				FixedJoint2D.connectedBody = null;
				global::UnityEngine.Object.Destroy(FixedJoint2D);
				FixedJoint2D = null;
				ResetInterpolation();
				RemoveFromParentBody();
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		private void DetachFromFixedJoint3D()
		{
			if (!(FixedJoint == null))
			{
				FixedJoint.connectedBody = null;
				m_InternalRigidbody.useGravity = m_OriginalGravitySetting;
				global::UnityEngine.Object.Destroy(FixedJoint);
				FixedJoint = null;
				ResetInterpolation();
				RemoveFromParentBody();
			}
		}

		public void DetachFromFixedJoint()
		{
			if (!base.HasAuthority)
			{
				global::UnityEngine.Debug.LogError("[" + base.name + "] Only authority can invoke DetachFromFixedJoint!");
			}
			if (UseRigidBodyForMotion)
			{
				if (m_IsRigidbody2D)
				{
					DetachFromFixedJoint2D();
				}
				else
				{
					DetachFromFixedJoint3D();
				}
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
			return "NetworkRigidbodyBase";
		}
	}
}
