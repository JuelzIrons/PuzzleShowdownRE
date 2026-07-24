namespace Unity.Netcode
{
	public struct NetworkBehaviourReference : global::Unity.Netcode.INetworkSerializable, global::System.IEquatable<global::Unity.Netcode.NetworkBehaviourReference>
	{
		private global::Unity.Netcode.NetworkObjectReference m_NetworkObjectReference;

		private ushort m_NetworkBehaviourId;

		private static ushort s_NullId = ushort.MaxValue;

		public NetworkBehaviourReference(global::Unity.Netcode.NetworkBehaviour networkBehaviour)
		{
			if (networkBehaviour == null)
			{
				m_NetworkObjectReference = new global::Unity.Netcode.NetworkObjectReference((global::Unity.Netcode.NetworkObject)null);
				m_NetworkBehaviourId = s_NullId;
				return;
			}
			if (networkBehaviour.NetworkObject == null)
			{
				throw new global::System.ArgumentException("Cannot create NetworkBehaviourReference from NetworkBehaviour without a NetworkObject.");
			}
			m_NetworkObjectReference = networkBehaviour.NetworkObject;
			m_NetworkBehaviourId = networkBehaviour.NetworkBehaviourId;
		}

		public bool TryGet(out global::Unity.Netcode.NetworkBehaviour networkBehaviour, global::Unity.Netcode.NetworkManager networkManager = null)
		{
			networkBehaviour = GetInternal(this, networkManager);
			return networkBehaviour != null;
		}

		public bool TryGet<T>(out T networkBehaviour, global::Unity.Netcode.NetworkManager networkManager = null) where T : global::Unity.Netcode.NetworkBehaviour
		{
			networkBehaviour = GetInternal(this, networkManager) as T;
			return networkBehaviour != null;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		private static global::Unity.Netcode.NetworkBehaviour GetInternal(global::Unity.Netcode.NetworkBehaviourReference networkBehaviourRef, global::Unity.Netcode.NetworkManager networkManager = null)
		{
			if (networkBehaviourRef.m_NetworkBehaviourId == s_NullId)
			{
				return null;
			}
			if (networkBehaviourRef.m_NetworkObjectReference.TryGet(out var networkObject, networkManager))
			{
				return networkObject.GetNetworkBehaviourAtOrderIndex(networkBehaviourRef.m_NetworkBehaviourId);
			}
			return null;
		}

		public bool Equals(global::Unity.Netcode.NetworkBehaviourReference other)
		{
			if (m_NetworkObjectReference.Equals(other.m_NetworkObjectReference))
			{
				return m_NetworkBehaviourId == other.m_NetworkBehaviourId;
			}
			return false;
		}

		public override bool Equals(object obj)
		{
			if (obj is global::Unity.Netcode.NetworkBehaviourReference other)
			{
				return Equals(other);
			}
			return false;
		}

		public override int GetHashCode()
		{
			return (m_NetworkObjectReference.GetHashCode() * 397) ^ m_NetworkBehaviourId.GetHashCode();
		}

		public void NetworkSerialize<T>(global::Unity.Netcode.BufferSerializer<T> serializer) where T : global::Unity.Netcode.IReaderWriter
		{
			m_NetworkObjectReference.NetworkSerialize(serializer);
			serializer.SerializeValue(ref m_NetworkBehaviourId, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
		}

		public static implicit operator global::Unity.Netcode.NetworkBehaviour(global::Unity.Netcode.NetworkBehaviourReference networkBehaviourRef)
		{
			return GetInternal(networkBehaviourRef);
		}

		public static implicit operator global::Unity.Netcode.NetworkBehaviourReference(global::Unity.Netcode.NetworkBehaviour networkBehaviour)
		{
			return new global::Unity.Netcode.NetworkBehaviourReference(networkBehaviour);
		}
	}
}
