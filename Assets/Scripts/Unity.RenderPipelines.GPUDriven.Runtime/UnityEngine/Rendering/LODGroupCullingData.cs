namespace UnityEngine.Rendering
{
	internal struct LODGroupCullingData
	{
		public global::Unity.Mathematics.float3 worldSpaceReferencePoint;

		public int lodCount;

		public unsafe fixed float sqrDistances[8];

		public unsafe fixed float transitionDistances[8];

		public float worldSpaceSize;

		public unsafe fixed bool percentageFlags[8];

		public byte forceLODMask;
	}
}
