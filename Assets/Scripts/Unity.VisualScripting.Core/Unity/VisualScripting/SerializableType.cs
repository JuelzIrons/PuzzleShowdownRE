namespace Unity.VisualScripting
{
	[global::System.Serializable]
	[global::Unity.VisualScripting.SerializationVersion("A", new global::System.Type[] { })]
	public struct SerializableType : global::System.IEquatable<global::Unity.VisualScripting.SerializableType>, global::System.IComparable<global::Unity.VisualScripting.SerializableType>
	{
		[global::Unity.VisualScripting.Serialize]
		public string Identification;

		public SerializableType(string identification)
		{
			Identification = identification;
		}

		public bool Equals(global::Unity.VisualScripting.SerializableType other)
		{
			return string.Equals(Identification, other.Identification);
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (obj is global::Unity.VisualScripting.SerializableType other)
			{
				return Equals(other);
			}
			return false;
		}

		public override int GetHashCode()
		{
			return Identification?.GetHashCode() ?? 0;
		}

		public static bool operator ==(global::Unity.VisualScripting.SerializableType left, global::Unity.VisualScripting.SerializableType right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(global::Unity.VisualScripting.SerializableType left, global::Unity.VisualScripting.SerializableType right)
		{
			return !left.Equals(right);
		}

		public int CompareTo(global::Unity.VisualScripting.SerializableType other)
		{
			return string.Compare(Identification, other.Identification, global::System.StringComparison.Ordinal);
		}
	}
}
