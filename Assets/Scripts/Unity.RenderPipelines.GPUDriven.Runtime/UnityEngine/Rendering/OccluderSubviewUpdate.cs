namespace UnityEngine.Rendering
{
	public struct OccluderSubviewUpdate
	{
		public int subviewIndex;

		public int depthSliceIndex;

		public global::UnityEngine.Vector2Int depthOffset;

		public global::UnityEngine.Matrix4x4 viewMatrix;

		public global::UnityEngine.Matrix4x4 invViewMatrix;

		public global::UnityEngine.Matrix4x4 gpuProjMatrix;

		public global::UnityEngine.Vector3 viewOffsetWorldSpace;

		public OccluderSubviewUpdate(int subviewIndex)
		{
			this.subviewIndex = subviewIndex;
			depthSliceIndex = 0;
			depthOffset = global::UnityEngine.Vector2Int.zero;
			viewMatrix = global::UnityEngine.Matrix4x4.identity;
			invViewMatrix = global::UnityEngine.Matrix4x4.identity;
			gpuProjMatrix = global::UnityEngine.Matrix4x4.identity;
			viewOffsetWorldSpace = global::UnityEngine.Vector3.zero;
		}
	}
}
