namespace UnityEngine.Rendering.RadeonRays
{
	internal struct InstanceInfo
	{
		public int blasOffset;

		public int instanceMask;

		public int vertexOffset;

		public int indexOffset;

		public uint disableTriangleCulling;

		public uint invertTriangleCulling;

		public uint userInstanceID;

		public int isOpaque;

		public global::UnityEngine.Rendering.RadeonRays.Transform worldToLocalTransform;

		public global::UnityEngine.Rendering.RadeonRays.Transform localToWorldTransform;
	}
}
