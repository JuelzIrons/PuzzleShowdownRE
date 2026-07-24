namespace UnityEngine.Rendering
{
	internal struct GPUInstanceIndex : global::System.IEquatable<global::UnityEngine.Rendering.GPUInstanceIndex>, global::System.IComparable<global::UnityEngine.Rendering.GPUInstanceIndex>
	{
		public static readonly global::UnityEngine.Rendering.GPUInstanceIndex Invalid = new global::UnityEngine.Rendering.GPUInstanceIndex
		{
			index = -1
		};

		public int index { get; set; }

		public bool valid => index != -1;

		public bool Equals(global::UnityEngine.Rendering.GPUInstanceIndex other)
		{
			return index == other.index;
		}

		public int CompareTo(global::UnityEngine.Rendering.GPUInstanceIndex other)
		{
			return index.CompareTo(other.index);
		}

		public override int GetHashCode()
		{
			return index;
		}
	}
}
