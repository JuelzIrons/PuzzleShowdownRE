namespace UnityEngine.Experimental.Rendering
{
	internal readonly struct XRView
	{
		internal readonly global::UnityEngine.Matrix4x4 projMatrix;

		internal readonly global::UnityEngine.Matrix4x4 viewMatrix;

		internal readonly global::UnityEngine.Matrix4x4 prevViewMatrix;

		internal readonly global::UnityEngine.Rect viewport;

		internal readonly global::UnityEngine.Mesh occlusionMesh;

		internal readonly global::UnityEngine.Mesh visibleMesh;

		internal readonly int textureArraySlice;

		internal readonly global::UnityEngine.Vector2 eyeCenterUV;

		internal readonly bool isPrevViewMatrixValid;

		internal XRView(global::UnityEngine.Matrix4x4 projMatrix, global::UnityEngine.Matrix4x4 viewMatrix, global::UnityEngine.Matrix4x4 prevViewMatrix, bool isPrevViewMatrixValid, global::UnityEngine.Rect viewport, global::UnityEngine.Mesh occlusionMesh, global::UnityEngine.Mesh visibleMesh, int textureArraySlice)
		{
			this.projMatrix = projMatrix;
			this.viewMatrix = viewMatrix;
			this.prevViewMatrix = prevViewMatrix;
			this.viewport = viewport;
			this.occlusionMesh = occlusionMesh;
			this.visibleMesh = visibleMesh;
			this.textureArraySlice = textureArraySlice;
			this.isPrevViewMatrixValid = isPrevViewMatrixValid;
			eyeCenterUV = ComputeEyeCenterUV(projMatrix);
		}

		private static global::UnityEngine.Vector2 ComputeEyeCenterUV(global::UnityEngine.Matrix4x4 proj)
		{
			global::UnityEngine.FrustumPlanes decomposeProjection = proj.decomposeProjection;
			float num = global::System.Math.Abs(decomposeProjection.left);
			float num2 = global::System.Math.Abs(decomposeProjection.right);
			float num3 = global::System.Math.Abs(decomposeProjection.top);
			float num4 = global::System.Math.Abs(decomposeProjection.bottom);
			return new global::UnityEngine.Vector2(num / (num2 + num), num3 / (num3 + num4));
		}
	}
}
