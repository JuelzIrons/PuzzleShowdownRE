namespace Unity.Netcode
{
	public struct ForceNetworkSerializeByMemcpy<T> : global::Unity.Netcode.INetworkSerializeByMemcpy, global::System.IEquatable<global::Unity.Netcode.ForceNetworkSerializeByMemcpy<T>> where T : unmanaged, global::System.IEquatable<T>
	{
		public T Value;

		public ForceNetworkSerializeByMemcpy(T value)
		{
			Value = value;
		}

		public static implicit operator T(global::Unity.Netcode.ForceNetworkSerializeByMemcpy<T> container)
		{
			return container.Value;
		}

		public static implicit operator global::Unity.Netcode.ForceNetworkSerializeByMemcpy<T>(T underlyingValue)
		{
			return new global::Unity.Netcode.ForceNetworkSerializeByMemcpy<T>
			{
				Value = underlyingValue
			};
		}

		public bool Equals(global::Unity.Netcode.ForceNetworkSerializeByMemcpy<T> other)
		{
			return Value.Equals(other.Value);
		}

		public override bool Equals(object obj)
		{
			if (obj is global::Unity.Netcode.ForceNetworkSerializeByMemcpy<T> other)
			{
				return Equals(other);
			}
			return false;
		}

		public override int GetHashCode()
		{
			return Value.GetHashCode();
		}
	}
}
