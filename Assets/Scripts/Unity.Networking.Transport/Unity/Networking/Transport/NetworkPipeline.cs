namespace Unity.Networking.Transport
{
	public struct NetworkPipeline : global::System.IEquatable<global::Unity.Networking.Transport.NetworkPipeline>
	{
		internal int Id;

		public static global::Unity.Networking.Transport.NetworkPipeline Null => default(global::Unity.Networking.Transport.NetworkPipeline);

		public static bool operator ==(global::Unity.Networking.Transport.NetworkPipeline lhs, global::Unity.Networking.Transport.NetworkPipeline rhs)
		{
			return lhs.Id == rhs.Id;
		}

		public static bool operator !=(global::Unity.Networking.Transport.NetworkPipeline lhs, global::Unity.Networking.Transport.NetworkPipeline rhs)
		{
			return lhs.Id != rhs.Id;
		}

		public override bool Equals(object compare)
		{
			return this == (global::Unity.Networking.Transport.NetworkPipeline)compare;
		}

		public override int GetHashCode()
		{
			return Id;
		}

		public bool Equals(global::Unity.Networking.Transport.NetworkPipeline connection)
		{
			return connection.Id == Id;
		}
	}
}
