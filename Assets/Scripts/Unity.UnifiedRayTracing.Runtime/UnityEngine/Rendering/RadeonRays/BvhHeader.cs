namespace UnityEngine.Rendering.RadeonRays
{
	internal struct BvhHeader
	{
		public uint internalNodeCount;

		public uint leafNodeCount;

		public uint root;

		public uint unused;

		public global::Unity.Mathematics.float3 globalAabbMin;

		public global::Unity.Mathematics.float3 globalAabbMax;

		public global::Unity.Mathematics.uint3 unused3;

		public global::Unity.Mathematics.uint3 unused4;
	}
}
