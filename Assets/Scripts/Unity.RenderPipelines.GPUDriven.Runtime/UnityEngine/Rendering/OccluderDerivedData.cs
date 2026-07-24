namespace UnityEngine.Rendering
{
	internal struct OccluderDerivedData
	{
		public global::UnityEngine.Matrix4x4 viewProjMatrix;

		public global::UnityEngine.Vector4 viewOriginWorldSpace;

		public global::UnityEngine.Vector4 radialDirWorldSpace;

		public global::UnityEngine.Vector4 facingDirWorldSpace;

		public static global::UnityEngine.Rendering.OccluderDerivedData FromParameters(in global::UnityEngine.Rendering.OccluderSubviewUpdate occluderSubviewUpdate)
		{
			global::UnityEngine.Vector3 vector = occluderSubviewUpdate.viewOffsetWorldSpace + (global::UnityEngine.Vector3)occluderSubviewUpdate.invViewMatrix.GetColumn(3);
			global::UnityEngine.Vector3 vector2 = occluderSubviewUpdate.invViewMatrix.GetColumn(0);
			global::UnityEngine.Vector3 vector3 = occluderSubviewUpdate.invViewMatrix.GetColumn(1);
			global::UnityEngine.Vector3 vector4 = occluderSubviewUpdate.invViewMatrix.GetColumn(2);
			global::UnityEngine.Matrix4x4 viewMatrix = occluderSubviewUpdate.viewMatrix;
			viewMatrix.SetColumn(3, new global::UnityEngine.Vector4(0f, 0f, 0f, 1f));
			return new global::UnityEngine.Rendering.OccluderDerivedData
			{
				viewOriginWorldSpace = vector,
				facingDirWorldSpace = vector4.normalized,
				radialDirWorldSpace = (vector2 + vector3).normalized,
				viewProjMatrix = occluderSubviewUpdate.gpuProjMatrix * viewMatrix
			};
		}
	}
}
