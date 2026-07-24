namespace Unity.Netcode
{
	internal struct NetworkSceneHandle : global::System.IEquatable<global::Unity.Netcode.NetworkSceneHandle>, global::Unity.Netcode.INetworkSerializable
	{
		private global::UnityEngine.SceneManagement.SceneHandle m_Handle;

		public bool IsEmpty()
		{
			return Equals(default(global::Unity.Netcode.NetworkSceneHandle));
		}

		public void NetworkSerialize<T>(global::Unity.Netcode.BufferSerializer<T> serializer) where T : global::Unity.Netcode.IReaderWriter
		{
			if (serializer.IsWriter)
			{
				serializer.GetFastBufferWriter().WriteValueSafe<int>(GetRawData(), default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
				return;
			}
			serializer.GetFastBufferReader().ReadValueSafe(out int value, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
			m_Handle = value;
		}

		internal NetworkSceneHandle(global::UnityEngine.SceneManagement.SceneHandle handle)
		{
			m_Handle = handle;
		}

		internal NetworkSceneHandle(int handle, bool asMock)
		{
			m_Handle = handle;
		}

		public int GetRawData()
		{
			return m_Handle;
		}

		public static implicit operator global::Unity.Netcode.NetworkSceneHandle(global::UnityEngine.SceneManagement.SceneHandle handle)
		{
			return new global::Unity.Netcode.NetworkSceneHandle(handle);
		}

		public static implicit operator global::UnityEngine.SceneManagement.SceneHandle(global::Unity.Netcode.NetworkSceneHandle handle)
		{
			return handle.m_Handle;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public bool Equals(global::Unity.Netcode.NetworkSceneHandle other)
		{
			return m_Handle == other.m_Handle;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public override bool Equals(object obj)
		{
			if (obj is global::Unity.Netcode.NetworkSceneHandle other)
			{
				return Equals(other);
			}
			return false;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		private bool Equals(global::UnityEngine.SceneManagement.SceneHandle other)
		{
			return m_Handle == other;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return m_Handle.GetHashCode();
		}

		public static bool operator ==(global::Unity.Netcode.NetworkSceneHandle left, global::Unity.Netcode.NetworkSceneHandle right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(global::Unity.Netcode.NetworkSceneHandle left, global::Unity.Netcode.NetworkSceneHandle right)
		{
			return !left.Equals(right);
		}

		public static bool operator ==(global::UnityEngine.SceneManagement.SceneHandle left, global::Unity.Netcode.NetworkSceneHandle right)
		{
			return left.Equals(right.m_Handle);
		}

		public static bool operator !=(global::UnityEngine.SceneManagement.SceneHandle left, global::Unity.Netcode.NetworkSceneHandle right)
		{
			return !left.Equals(right.m_Handle);
		}

		public static bool operator ==(global::Unity.Netcode.NetworkSceneHandle left, global::UnityEngine.SceneManagement.SceneHandle right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(global::Unity.Netcode.NetworkSceneHandle left, global::UnityEngine.SceneManagement.SceneHandle right)
		{
			return !left.Equals(right);
		}
	}
}
