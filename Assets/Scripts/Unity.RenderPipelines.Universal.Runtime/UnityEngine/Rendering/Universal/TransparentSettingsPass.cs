namespace UnityEngine.Rendering.Universal
{
	internal class TransparentSettingsPass : global::UnityEngine.Rendering.Universal.ScriptableRenderPass
	{
		private bool m_shouldReceiveShadows;

		public TransparentSettingsPass(global::UnityEngine.Rendering.Universal.RenderPassEvent evt, bool shadowReceiveSupported)
		{
			base.profilingSampler = new global::UnityEngine.Rendering.ProfilingSampler("Set Transparent Parameters");
			base.renderPassEvent = evt;
			m_shouldReceiveShadows = shadowReceiveSupported;
		}

		public bool Setup()
		{
			return !m_shouldReceiveShadows;
		}

		public static void ExecutePass(global::UnityEngine.Rendering.RasterCommandBuffer rasterCommandBuffer)
		{
			global::UnityEngine.Rendering.Universal.Internal.MainLightShadowCasterPass.SetShadowParamsForEmptyShadowmap(rasterCommandBuffer);
			global::UnityEngine.Rendering.Universal.Internal.AdditionalLightsShadowCasterPass.SetShadowParamsForEmptyShadowmap(rasterCommandBuffer);
		}
	}
}
