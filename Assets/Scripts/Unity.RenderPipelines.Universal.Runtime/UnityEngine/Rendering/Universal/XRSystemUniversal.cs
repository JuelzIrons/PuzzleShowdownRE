namespace UnityEngine.Rendering.Universal
{
	internal static class XRSystemUniversal
	{
		private static global::UnityEngine.Matrix4x4[] s_projMatrix = new global::UnityEngine.Matrix4x4[2];

		private static global::UnityEngine.MaterialPropertyBlock s_XRSharedPropertyBlock = new global::UnityEngine.MaterialPropertyBlock();

		internal static global::UnityEngine.MaterialPropertyBlock GetMaterialPropertyBlock()
		{
			return s_XRSharedPropertyBlock;
		}

		internal static void BeginLateLatching(global::UnityEngine.Camera camera, global::UnityEngine.Rendering.Universal.XRPassUniversal xrPass)
		{
			global::UnityEngine.XR.XRDisplaySubsystem activeDisplay = global::UnityEngine.Experimental.Rendering.XRSystem.GetActiveDisplay();
			if (activeDisplay != null && xrPass.viewCount == 2)
			{
				activeDisplay.BeginRecordingIfLateLatched(camera);
				xrPass.isLateLatchEnabled = true;
			}
		}

		internal static void EndLateLatching(global::UnityEngine.Camera camera, global::UnityEngine.Rendering.Universal.XRPassUniversal xrPass)
		{
			global::UnityEngine.XR.XRDisplaySubsystem activeDisplay = global::UnityEngine.Experimental.Rendering.XRSystem.GetActiveDisplay();
			if (activeDisplay != null && xrPass.isLateLatchEnabled)
			{
				activeDisplay.EndRecordingIfLateLatched(camera);
				xrPass.isLateLatchEnabled = false;
			}
		}

		internal static void UnmarkShaderProperties(global::UnityEngine.Rendering.RasterCommandBuffer cmd, global::UnityEngine.Rendering.Universal.XRPassUniversal xrPass)
		{
			if (xrPass.isLateLatchEnabled && xrPass.hasMarkedLateLatch)
			{
				cmd.UnmarkLateLatchMatrix(global::UnityEngine.Rendering.CameraLateLatchMatrixType.View);
				cmd.UnmarkLateLatchMatrix(global::UnityEngine.Rendering.CameraLateLatchMatrixType.InverseView);
				cmd.UnmarkLateLatchMatrix(global::UnityEngine.Rendering.CameraLateLatchMatrixType.ViewProjection);
				cmd.UnmarkLateLatchMatrix(global::UnityEngine.Rendering.CameraLateLatchMatrixType.InverseViewProjection);
				xrPass.hasMarkedLateLatch = false;
			}
		}

		internal static void MarkShaderProperties(global::UnityEngine.Rendering.RasterCommandBuffer cmd, global::UnityEngine.Rendering.Universal.XRPassUniversal xrPass, bool renderIntoTexture)
		{
			if (xrPass.isLateLatchEnabled && xrPass.canMarkLateLatch)
			{
				cmd.MarkLateLatchMatrixShaderPropertyID(global::UnityEngine.Rendering.CameraLateLatchMatrixType.View, global::UnityEngine.Experimental.Rendering.XRBuiltinShaderConstants.unity_StereoMatrixV);
				cmd.MarkLateLatchMatrixShaderPropertyID(global::UnityEngine.Rendering.CameraLateLatchMatrixType.InverseView, global::UnityEngine.Experimental.Rendering.XRBuiltinShaderConstants.unity_StereoMatrixInvV);
				cmd.MarkLateLatchMatrixShaderPropertyID(global::UnityEngine.Rendering.CameraLateLatchMatrixType.ViewProjection, global::UnityEngine.Experimental.Rendering.XRBuiltinShaderConstants.unity_StereoMatrixVP);
				cmd.MarkLateLatchMatrixShaderPropertyID(global::UnityEngine.Rendering.CameraLateLatchMatrixType.InverseViewProjection, global::UnityEngine.Experimental.Rendering.XRBuiltinShaderConstants.unity_StereoMatrixInvVP);
				for (int i = 0; i < 2; i++)
				{
					s_projMatrix[i] = global::UnityEngine.GL.GetGPUProjectionMatrix(xrPass.GetProjMatrix(i), renderIntoTexture);
				}
				cmd.SetLateLatchProjectionMatrices(s_projMatrix);
				xrPass.hasMarkedLateLatch = true;
			}
		}
	}
}
