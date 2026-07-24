namespace UnityEngine.Rendering.Universal.Internal
{
	public static class NormalReconstruction
	{
		private static readonly int s_NormalReconstructionMatrixID = global::UnityEngine.Shader.PropertyToID("_NormalReconstructionMatrix");

		private static global::UnityEngine.Matrix4x4[] s_NormalReconstructionMatrix = new global::UnityEngine.Matrix4x4[2];

		public static void SetupProperties(global::UnityEngine.Rendering.CommandBuffer cmd, in global::UnityEngine.Rendering.Universal.CameraData cameraData)
		{
			SetupProperties(global::UnityEngine.Rendering.CommandBufferHelpers.GetRasterCommandBuffer(cmd), in cameraData);
		}

		public static void SetupProperties(global::UnityEngine.Rendering.RasterCommandBuffer cmd, in global::UnityEngine.Rendering.Universal.CameraData cameraData)
		{
			SetupProperties(cmd, cameraData.universalCameraData);
		}

		public static void SetupProperties(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData)
		{
			SetupProperties(global::UnityEngine.Rendering.CommandBufferHelpers.GetRasterCommandBuffer(cmd), in cameraData);
		}

		public static void SetupProperties(global::UnityEngine.Rendering.RasterCommandBuffer cmd, in global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData)
		{
			int num = ((!cameraData.xr.enabled || !cameraData.xr.singlePassEnabled) ? 1 : 2);
			for (int i = 0; i < num; i++)
			{
				global::UnityEngine.Matrix4x4 viewMatrix = cameraData.GetViewMatrix(i);
				global::UnityEngine.Matrix4x4 projectionMatrix = cameraData.GetProjectionMatrix(i);
				s_NormalReconstructionMatrix[i] = projectionMatrix * viewMatrix;
				global::UnityEngine.Matrix4x4 matrix4x = viewMatrix;
				matrix4x.SetColumn(3, new global::UnityEngine.Vector4(0f, 0f, 0f, 1f));
				global::UnityEngine.Matrix4x4 inverse = (projectionMatrix * matrix4x).inverse;
				s_NormalReconstructionMatrix[i] = inverse;
			}
			cmd.SetGlobalMatrixArray(s_NormalReconstructionMatrixID, s_NormalReconstructionMatrix);
		}
	}
}
