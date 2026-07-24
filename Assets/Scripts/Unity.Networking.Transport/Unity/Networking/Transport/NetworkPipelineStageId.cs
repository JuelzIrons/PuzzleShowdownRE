namespace Unity.Networking.Transport
{
	public struct NetworkPipelineStageId : global::System.IEquatable<global::Unity.Networking.Transport.NetworkPipelineStageId>
	{
		private long m_TypeHash;

		internal static global::Unity.Networking.Transport.NetworkPipelineStageId Get(global::System.Type stage)
		{
			return new global::Unity.Networking.Transport.NetworkPipelineStageId
			{
				m_TypeHash = global::Unity.Burst.BurstRuntime.GetHashCode64(stage)
			};
		}

		public static global::Unity.Networking.Transport.NetworkPipelineStageId Get<T>() where T : unmanaged, global::Unity.Networking.Transport.INetworkPipelineStage
		{
			return new global::Unity.Networking.Transport.NetworkPipelineStageId
			{
				m_TypeHash = global::Unity.Burst.BurstRuntime.GetHashCode64<T>()
			};
		}

		public override int GetHashCode()
		{
			return (int)m_TypeHash;
		}

		public override bool Equals(object other)
		{
			return this == (global::Unity.Networking.Transport.NetworkPipelineStageId)other;
		}

		public bool Equals(global::Unity.Networking.Transport.NetworkPipelineStageId other)
		{
			return m_TypeHash == other.m_TypeHash;
		}

		public static bool operator ==(global::Unity.Networking.Transport.NetworkPipelineStageId lhs, global::Unity.Networking.Transport.NetworkPipelineStageId rhs)
		{
			return lhs.m_TypeHash == rhs.m_TypeHash;
		}

		public static bool operator !=(global::Unity.Networking.Transport.NetworkPipelineStageId lhs, global::Unity.Networking.Transport.NetworkPipelineStageId rhs)
		{
			return lhs.m_TypeHash != rhs.m_TypeHash;
		}
	}
}
