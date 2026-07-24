namespace UnityEngine.Rendering
{
	internal struct SharedInstanceHandle : global::System.IEquatable<global::UnityEngine.Rendering.SharedInstanceHandle>, global::System.IComparable<global::UnityEngine.Rendering.SharedInstanceHandle>
	{
		public static readonly global::UnityEngine.Rendering.SharedInstanceHandle Invalid = new global::UnityEngine.Rendering.SharedInstanceHandle
		{
			index = -1
		};

		public int index { get; set; }

		public bool valid => index != -1;

		public bool Equals(global::UnityEngine.Rendering.SharedInstanceHandle other)
		{
			return index == other.index;
		}

		public int CompareTo(global::UnityEngine.Rendering.SharedInstanceHandle other)
		{
			return index.CompareTo(other.index);
		}

		public override int GetHashCode()
		{
			return index;
		}
	}
}
