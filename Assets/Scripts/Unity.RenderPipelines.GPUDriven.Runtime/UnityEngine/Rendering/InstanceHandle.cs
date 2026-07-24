namespace UnityEngine.Rendering
{
	internal struct InstanceHandle : global::System.IEquatable<global::UnityEngine.Rendering.InstanceHandle>, global::System.IComparable<global::UnityEngine.Rendering.InstanceHandle>
	{
		public static readonly global::UnityEngine.Rendering.InstanceHandle Invalid = new global::UnityEngine.Rendering.InstanceHandle
		{
			index = -1
		};

		public int index { get; private set; }

		public int instanceIndex => index >> 1;

		public global::UnityEngine.Rendering.InstanceType type => (global::UnityEngine.Rendering.InstanceType)((long)index & 1L);

		public bool valid => index != -1;

		public static global::UnityEngine.Rendering.InstanceHandle Create(int instanceIndex, global::UnityEngine.Rendering.InstanceType instanceType)
		{
			return new global::UnityEngine.Rendering.InstanceHandle
			{
				index = ((instanceIndex << 1) | (int)instanceType)
			};
		}

		public static global::UnityEngine.Rendering.InstanceHandle FromInt(int value)
		{
			return new global::UnityEngine.Rendering.InstanceHandle
			{
				index = value
			};
		}

		public bool Equals(global::UnityEngine.Rendering.InstanceHandle other)
		{
			return index == other.index;
		}

		public int CompareTo(global::UnityEngine.Rendering.InstanceHandle other)
		{
			return index.CompareTo(other.index);
		}

		public override int GetHashCode()
		{
			return index;
		}
	}
}
