namespace UnityEngine.Experimental.Rendering
{
	public class XRLayout
	{
		private readonly global::System.Collections.Generic.List<(global::UnityEngine.Camera, global::UnityEngine.Experimental.Rendering.XRPass)> m_ActivePasses = new global::System.Collections.Generic.List<(global::UnityEngine.Camera, global::UnityEngine.Experimental.Rendering.XRPass)>();

		public void AddCamera(global::UnityEngine.Camera camera, bool enableXR)
		{
			if (!(camera == null))
			{
				bool flag = (camera.cameraType == global::UnityEngine.CameraType.Game || camera.cameraType == global::UnityEngine.CameraType.VR) && camera.targetTexture == null && enableXR;
				if (global::UnityEngine.Experimental.Rendering.XRSystem.displayActive && flag)
				{
					global::UnityEngine.Experimental.Rendering.XRSystem.SetDisplayZRange(camera.nearClipPlane, camera.farClipPlane);
					global::UnityEngine.Experimental.Rendering.XRSystem.CreateDefaultLayout(camera, this);
				}
				else
				{
					AddPass(camera, global::UnityEngine.Experimental.Rendering.XRSystem.emptyPass);
				}
			}
		}

		public void ReconfigurePass(global::UnityEngine.Experimental.Rendering.XRPass xrPass, global::UnityEngine.Camera camera)
		{
			if (xrPass.enabled)
			{
				global::UnityEngine.Experimental.Rendering.XRSystem.ReconfigurePass(xrPass, camera);
				xrPass.UpdateCombinedOcclusionMesh();
			}
		}

		public global::System.Collections.Generic.List<(global::UnityEngine.Camera, global::UnityEngine.Experimental.Rendering.XRPass)> GetActivePasses()
		{
			return m_ActivePasses;
		}

		internal void AddPass(global::UnityEngine.Camera camera, global::UnityEngine.Experimental.Rendering.XRPass xrPass)
		{
			xrPass.UpdateCombinedOcclusionMesh();
			m_ActivePasses.Add((camera, xrPass));
		}

		internal void Clear()
		{
			for (int i = 0; i < m_ActivePasses.Count; i++)
			{
				global::UnityEngine.Experimental.Rendering.XRPass item = m_ActivePasses[m_ActivePasses.Count - i - 1].Item2;
				if (item != global::UnityEngine.Experimental.Rendering.XRSystem.emptyPass)
				{
					item.Release();
				}
			}
			m_ActivePasses.Clear();
		}

		internal void LogDebugInfo()
		{
			global::System.Text.StringBuilder stringBuilder = new global::System.Text.StringBuilder();
			stringBuilder.AppendFormat("XRSystem setup for frame {0}, active: {1}", global::UnityEngine.Time.frameCount, global::UnityEngine.Experimental.Rendering.XRSystem.displayActive);
			stringBuilder.AppendLine();
			for (int i = 0; i < m_ActivePasses.Count; i++)
			{
				global::UnityEngine.Experimental.Rendering.XRPass item = m_ActivePasses[i].Item2;
				for (int j = 0; j < item.viewCount; j++)
				{
					global::UnityEngine.Rect viewport = item.GetViewport(j);
					stringBuilder.AppendFormat("XR Pass {0} Cull {1} View {2} Slice {3} : {4} x {5}", item.multipassId, item.cullingPassId, j, item.GetTextureArraySlice(j), viewport.width, viewport.height);
					stringBuilder.AppendLine();
				}
			}
			global::UnityEngine.Debug.Log(stringBuilder);
		}
	}
}
