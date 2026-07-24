namespace Unity.Networking.Transport
{
	public struct NetworkConnection : global::System.IEquatable<global::Unity.Networking.Transport.NetworkConnection>
	{
		public enum State
		{
			Disconnected = 0,
			[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
			Disconnecting = 1,
			Connecting = 2,
			Connected = 3
		}

		private global::Unity.Networking.Transport.ConnectionId m_ConnectionId;

		internal global::Unity.Networking.Transport.ConnectionId ConnectionId => new global::Unity.Networking.Transport.ConnectionId
		{
			Id = InternalId,
			Version = Version
		};

		public bool IsCreated => m_ConnectionId.Version != 0;

		internal int InternalId => m_ConnectionId.Id;

		internal int Version => m_ConnectionId.Version & 0xFFFFFF;

		internal int DriverId
		{
			get
			{
				return m_ConnectionId.Version >> 24;
			}
			set
			{
				m_ConnectionId.Version |= value << 24;
			}
		}

		internal NetworkConnection(global::Unity.Networking.Transport.ConnectionId connectionId)
		{
			m_ConnectionId = connectionId;
		}

		public int Disconnect(global::Unity.Networking.Transport.NetworkDriver driver)
		{
			return driver.Disconnect(this);
		}

		public global::Unity.Networking.Transport.NetworkEvent.Type PopEvent(global::Unity.Networking.Transport.NetworkDriver driver, out global::Unity.Collections.DataStreamReader stream)
		{
			return driver.PopEventForConnection(this, out stream);
		}

		public global::Unity.Networking.Transport.NetworkEvent.Type PopEvent(global::Unity.Networking.Transport.NetworkDriver driver, out global::Unity.Collections.DataStreamReader stream, out global::Unity.Networking.Transport.NetworkPipeline pipeline)
		{
			return driver.PopEventForConnection(this, out stream, out pipeline);
		}

		public int Close(global::Unity.Networking.Transport.NetworkDriver driver)
		{
			return driver.Disconnect(this);
		}

		public global::Unity.Networking.Transport.NetworkConnection.State GetState(global::Unity.Networking.Transport.NetworkDriver driver)
		{
			return driver.GetConnectionState(this);
		}

		public static bool operator ==(global::Unity.Networking.Transport.NetworkConnection lhs, global::Unity.Networking.Transport.NetworkConnection rhs)
		{
			return lhs.m_ConnectionId == rhs.m_ConnectionId;
		}

		public static bool operator !=(global::Unity.Networking.Transport.NetworkConnection lhs, global::Unity.Networking.Transport.NetworkConnection rhs)
		{
			return lhs.m_ConnectionId != rhs.m_ConnectionId;
		}

		public override bool Equals(object o)
		{
			return this == (global::Unity.Networking.Transport.NetworkConnection)o;
		}

		public bool Equals(global::Unity.Networking.Transport.NetworkConnection o)
		{
			return this == o;
		}

		public override int GetHashCode()
		{
			return m_ConnectionId.GetHashCode();
		}

		public override string ToString()
		{
			return $"NetworkConnection[id{InternalId},v{Version}]";
		}

		public global::Unity.Collections.FixedString128Bytes ToFixedString()
		{
			return global::Unity.Collections.FixedString.Format("NetworkConnection[id{0},v{1}]", InternalId, Version);
		}
	}
}
