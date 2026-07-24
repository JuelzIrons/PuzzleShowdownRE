namespace UnityEngine.Rendering
{
	internal struct InstanceOcclusionEventStats
	{
		public int viewInstanceID;

		public global::UnityEngine.Rendering.InstanceOcclusionEventType eventType;

		public int occluderVersion;

		public int subviewMask;

		public global::UnityEngine.Rendering.OcclusionTest occlusionTest;

		public int visibleInstances;

		public int culledInstances;

		public int visiblePrimitives;

		public int culledPrimitives;
	}
}
