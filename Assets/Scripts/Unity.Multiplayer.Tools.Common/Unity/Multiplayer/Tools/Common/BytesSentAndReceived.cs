namespace Unity.Multiplayer.Tools.Common
{
	[global::System.Serializable]
	internal struct BytesSentAndReceived : global::System.IEquatable<global::Unity.Multiplayer.Tools.Common.BytesSentAndReceived>
	{
		public long Sent { get; set; }

		public long Received { get; set; }

		public long this[global::Unity.Multiplayer.Tools.Common.NetworkDirection direction] => (((direction & global::Unity.Multiplayer.Tools.Common.NetworkDirection.Sent) != global::Unity.Multiplayer.Tools.Common.NetworkDirection.None) ? Sent : 0) + (((direction & global::Unity.Multiplayer.Tools.Common.NetworkDirection.Received) != global::Unity.Multiplayer.Tools.Common.NetworkDirection.None) ? Received : 0);

		public global::Unity.Multiplayer.Tools.Common.NetworkDirection Direction => (global::Unity.Multiplayer.Tools.Common.NetworkDirection)((((float)Sent > 0f) ? 2 : 0) | (((float)Received > 0f) ? 1 : 0));

		public long Total => Sent + Received;

		public BytesSentAndReceived(long sent = 0L, long received = 0L)
		{
			Sent = sent;
			Received = received;
		}

		public BytesSentAndReceived(long count, global::Unity.Multiplayer.Tools.Common.NetworkDirection direction)
		{
			Sent = (((direction & global::Unity.Multiplayer.Tools.Common.NetworkDirection.Sent) != global::Unity.Multiplayer.Tools.Common.NetworkDirection.None) ? count : 0);
			Received = (((direction & global::Unity.Multiplayer.Tools.Common.NetworkDirection.Received) != global::Unity.Multiplayer.Tools.Common.NetworkDirection.None) ? count : 0);
		}

		public bool Equals(global::Unity.Multiplayer.Tools.Common.BytesSentAndReceived other)
		{
			if (Sent == other.Sent)
			{
				return Received == other.Received;
			}
			return false;
		}

		public override bool Equals(object obj)
		{
			if (obj is global::Unity.Multiplayer.Tools.Common.BytesSentAndReceived other)
			{
				return Equals(other);
			}
			return false;
		}

		public static global::Unity.Multiplayer.Tools.Common.BytesSentAndReceived operator +(global::Unity.Multiplayer.Tools.Common.BytesSentAndReceived a, global::Unity.Multiplayer.Tools.Common.BytesSentAndReceived b)
		{
			return new global::Unity.Multiplayer.Tools.Common.BytesSentAndReceived(a.Sent + b.Sent, a.Received + b.Received);
		}

		public override int GetHashCode()
		{
			return global::System.HashCode.Combine(Sent, Received);
		}

		public override string ToString()
		{
			return string.Format("{0}: {1}={2} {3}={4}", "BytesSentAndReceived", "Sent", Sent, "Received", Received);
		}
	}
}
