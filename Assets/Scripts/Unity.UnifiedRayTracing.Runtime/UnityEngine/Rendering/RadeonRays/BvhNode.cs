namespace UnityEngine.Rendering.RadeonRays
{
	internal struct BvhNode
	{
		public uint child0;

		public uint child1;

		public uint parent;

		public uint update;

		public global::Unity.Mathematics.float3 aabb0_min;

		public global::Unity.Mathematics.float3 aabb0_max;

		public global::Unity.Mathematics.float3 aabb1_min;

		public global::Unity.Mathematics.float3 aabb1_max;
	}
}
