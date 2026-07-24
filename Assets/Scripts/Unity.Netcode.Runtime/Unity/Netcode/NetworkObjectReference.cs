namespace Unity.Netcode
{
	public struct NetworkObjectReference : global::Unity.Netcode.INetworkSerializable, global::System.IEquatable<global::Unity.Netcode.NetworkObjectReference>
	{
		private ulong m_NetworkObjectId;

		private static ulong s_NullId = ulong.MaxValue;

		public ulong NetworkObjectId
		{
			get
			{
				return m_NetworkObjectId;
			}
			internal set
			{
				m_NetworkObjectId = value;
			}
		}

		public NetworkObjectReference(global::Unity.Netcode.NetworkObject networkObject)
		{
			if (networkObject == null)
			{
				m_NetworkObjectId = s_NullId;
				return;
			}
			if (!networkObject.IsSpawned)
			{
				throw new global::System.ArgumentException("NetworkObjectReference can only be created from spawned NetworkObjects.");
			}
			m_NetworkObjectId = networkObject.NetworkObjectId;
		}

		public NetworkObjectReference(global::UnityEngine.GameObject gameObject)
		{
			if (gameObject == null)
			{
				m_NetworkObjectId = s_NullId;
				return;
			}
			global::Unity.Netcode.NetworkObject component = gameObject.GetComponent<global::Unity.Netcode.NetworkObject>();
			if (!component)
			{
				throw new global::System.ArgumentException("Cannot create NetworkObjectReference from GameObject without a NetworkObject component.");
			}
			if (!component.IsSpawned)
			{
				throw new global::System.ArgumentException("NetworkObjectReference can only be created from spawned NetworkObjects.");
			}
			m_NetworkObjectId = component.NetworkObjectId;
		}

		public bool TryGet(out global::Unity.Netcode.NetworkObject networkObject, global::Unity.Netcode.NetworkManager networkManager = null)
		{
			networkObject = Resolve(this, networkManager);
			return networkObject != null;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		private static global::Unity.Netcode.NetworkObject Resolve(global::Unity.Netcode.NetworkObjectReference networkObjectRef, global::Unity.Netcode.NetworkManager networkManager = null)
		{
			if (networkObjectRef.m_NetworkObjectId == s_NullId)
			{
				return null;
			}
			networkManager = networkManager ?? global::Unity.Netcode.NetworkManager.Singleton;
			networkManager.SpawnManager.SpawnedObjects.TryGetValue(networkObjectRef.m_NetworkObjectId, out var value);
			return value;
		}

		public bool Equals(global::Unity.Netcode.NetworkObjectReference other)
		{
			return m_NetworkObjectId == other.m_NetworkObjectId;
		}

		public override bool Equals(object obj)
		{
			if (obj is global::Unity.Netcode.NetworkObjectReference other)
			{
				return Equals(other);
			}
			return false;
		}

		public override int GetHashCode()
		{
			return m_NetworkObjectId.GetHashCode();
		}

		public void NetworkSerialize<T>(global::Unity.Netcode.BufferSerializer<T> serializer) where T : global::Unity.Netcode.IReaderWriter
		{
			serializer.SerializeValue(ref m_NetworkObjectId, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
		}

		public static implicit operator global::Unity.Netcode.NetworkObject(global::Unity.Netcode.NetworkObjectReference networkObjectRef)
		{
			return Resolve(networkObjectRef);
		}

		public static implicit operator global::Unity.Netcode.NetworkObjectReference(global::Unity.Netcode.NetworkObject networkObject)
		{
			return new global::Unity.Netcode.NetworkObjectReference(networkObject);
		}

		public static implicit operator global::UnityEngine.GameObject(global::Unity.Netcode.NetworkObjectReference networkObjectRef)
		{
			global::Unity.Netcode.NetworkObject networkObject = Resolve(networkObjectRef);
			if (networkObject != null)
			{
				return networkObject.gameObject;
			}
			return null;
		}

		public static implicit operator global::Unity.Netcode.NetworkObjectReference(global::UnityEngine.GameObject gameObject)
		{
			return new global::Unity.Netcode.NetworkObjectReference(gameObject);
		}
	}
}
