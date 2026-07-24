namespace UnityEngine.Rendering
{
	public static class XRGraphicsAutomatedTests
	{
		public static bool running = false;

		private static bool activatedFromCommandLine => false;

		public static bool enabled { get; set; } = activatedFromCommandLine;

		internal static void OverrideLayout(global::UnityEngine.Experimental.Rendering.XRLayout layout, global::UnityEngine.Camera camera)
		{
			if (!enabled || !running)
			{
				return;
			}
			global::UnityEngine.Matrix4x4 projectionMatrix = camera.projectionMatrix;
			global::UnityEngine.Matrix4x4 worldToCameraMatrix = camera.worldToCameraMatrix;
			if (!camera.TryGetCullingParameters(stereoAware: false, out var cullingParameters))
			{
				return;
			}
			cullingParameters.stereoProjectionMatrix = projectionMatrix;
			cullingParameters.stereoViewMatrix = worldToCameraMatrix;
			cullingParameters.stereoSeparationDistance = 0f;
			global::System.Collections.Generic.List<(global::UnityEngine.Camera, global::UnityEngine.Experimental.Rendering.XRPass)> activePasses = layout.GetActivePasses();
			for (int i = 0; i < activePasses.Count; i++)
			{
				global::UnityEngine.Experimental.Rendering.XRPass item = activePasses[i].Item2;
				item.AssignCullingParams(item.cullingPassId, cullingParameters);
				for (int j = 0; j < item.viewCount; j++)
				{
					global::UnityEngine.Matrix4x4 projMatrix = projectionMatrix;
					global::UnityEngine.Matrix4x4 viewMatrix = worldToCameraMatrix;
					bool num = activePasses.Count == 2 && i == 0;
					bool flag = activePasses.Count == 1 && j == 0;
					if (num || flag)
					{
						global::UnityEngine.FrustumPlanes decomposeProjection = projMatrix.decomposeProjection;
						decomposeProjection.left *= 0.44f;
						decomposeProjection.right *= 0.88f;
						decomposeProjection.top *= 0.11f;
						decomposeProjection.bottom *= 0.33f;
						projMatrix = global::UnityEngine.Matrix4x4.Frustum(decomposeProjection);
						viewMatrix *= global::UnityEngine.Matrix4x4.Translate(new global::UnityEngine.Vector3(0.34f, 0.25f, -0.08f));
					}
					global::UnityEngine.Experimental.Rendering.XRView xrView = new global::UnityEngine.Experimental.Rendering.XRView(projMatrix, viewMatrix, global::UnityEngine.Matrix4x4.identity, isPrevViewMatrixValid: false, item.GetViewport(j), null, null, item.GetTextureArraySlice(j));
					item.AssignView(j, xrView);
				}
			}
		}
	}
}
