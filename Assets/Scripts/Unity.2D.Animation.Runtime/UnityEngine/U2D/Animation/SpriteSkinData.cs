namespace UnityEngine.U2D.Animation
{
	internal struct SpriteSkinData
	{
		public global::UnityEngine.U2D.Animation.NativeCustomSlice<global::UnityEngine.Vector3> vertices;

		public global::UnityEngine.U2D.Animation.NativeCustomSlice<global::UnityEngine.BoneWeight> boneWeights;

		public global::UnityEngine.U2D.Animation.NativeCustomSlice<global::UnityEngine.Matrix4x4> bindPoses;

		public global::UnityEngine.U2D.Animation.NativeCustomSlice<global::UnityEngine.Vector4> tangents;

		public bool hasTangents;

		public int spriteVertexStreamSize;

		public int spriteVertexCount;

		public int tangentVertexOffset;

		public int deformVerticesStartPos;

		public int previousDeformVerticesStartPos;

		public int transformId;

		public global::UnityEngine.U2D.Animation.NativeCustomSlice<int> boneTransformId;
	}
}
