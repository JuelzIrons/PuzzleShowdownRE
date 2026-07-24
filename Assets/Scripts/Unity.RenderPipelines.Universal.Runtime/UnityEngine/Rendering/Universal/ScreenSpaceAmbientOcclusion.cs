namespace UnityEngine.Rendering.Universal
{
	[global::UnityEngine.Rendering.Universal.SupportedOnRenderer(typeof(global::UnityEngine.Rendering.Universal.UniversalRendererData))]
	[global::UnityEngine.Rendering.Universal.DisallowMultipleRendererFeature("Screen Space Ambient Occlusion")]
	[global::UnityEngine.Tooltip("The Ambient Occlusion effect darkens creases, holes, intersections and surfaces that are close to each other.")]
	public class ScreenSpaceAmbientOcclusion : global::UnityEngine.Rendering.Universal.ScriptableRendererFeature
	{
		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Rendering.Universal.ScreenSpaceAmbientOcclusionSettings m_Settings = new global::UnityEngine.Rendering.Universal.ScreenSpaceAmbientOcclusionSettings();

		private global::UnityEngine.Material m_Material;

		private global::UnityEngine.Rendering.Universal.ScreenSpaceAmbientOcclusionPass m_SSAOPass;

		private global::UnityEngine.Shader m_Shader;

		private global::UnityEngine.Texture2D[] m_BlueNoise256Textures;

		internal const string k_AOInterleavedGradientKeyword = "_INTERLEAVED_GRADIENT";

		internal const string k_AOBlueNoiseKeyword = "_BLUE_NOISE";

		internal const string k_OrthographicCameraKeyword = "_ORTHOGRAPHIC";

		internal const string k_SourceDepthLowKeyword = "_SOURCE_DEPTH_LOW";

		internal const string k_SourceDepthMediumKeyword = "_SOURCE_DEPTH_MEDIUM";

		internal const string k_SourceDepthHighKeyword = "_SOURCE_DEPTH_HIGH";

		internal const string k_SourceDepthNormalsKeyword = "_SOURCE_DEPTH_NORMALS";

		internal const string k_SampleCountLowKeyword = "_SAMPLE_COUNT_LOW";

		internal const string k_SampleCountMediumKeyword = "_SAMPLE_COUNT_MEDIUM";

		internal const string k_SampleCountHighKeyword = "_SAMPLE_COUNT_HIGH";

		internal ref global::UnityEngine.Rendering.Universal.ScreenSpaceAmbientOcclusionSettings settings => ref m_Settings;

		public override void Create()
		{
			if (m_SSAOPass == null)
			{
				m_SSAOPass = new global::UnityEngine.Rendering.Universal.ScreenSpaceAmbientOcclusionPass();
			}
			if (m_Settings.SampleCount > 0)
			{
				m_Settings.AOMethod = global::UnityEngine.Rendering.Universal.ScreenSpaceAmbientOcclusionSettings.AOMethodOptions.InterleavedGradient;
				if (m_Settings.SampleCount > 11)
				{
					m_Settings.Samples = global::UnityEngine.Rendering.Universal.ScreenSpaceAmbientOcclusionSettings.AOSampleOption.High;
				}
				else if (m_Settings.SampleCount > 8)
				{
					m_Settings.Samples = global::UnityEngine.Rendering.Universal.ScreenSpaceAmbientOcclusionSettings.AOSampleOption.Medium;
				}
				else
				{
					m_Settings.Samples = global::UnityEngine.Rendering.Universal.ScreenSpaceAmbientOcclusionSettings.AOSampleOption.Low;
				}
				m_Settings.SampleCount = -1;
			}
		}

		public override void AddRenderPasses(global::UnityEngine.Rendering.Universal.ScriptableRenderer renderer, ref global::UnityEngine.Rendering.Universal.RenderingData renderingData)
		{
			if (!global::UnityEngine.Rendering.Universal.UniversalRenderer.IsOffscreenDepthTexture(ref renderingData.cameraData) && TryPrepareResources() && m_SSAOPass.Setup(ref m_Settings, ref renderer, ref m_Material, ref m_BlueNoise256Textures))
			{
				renderer.EnqueuePass(m_SSAOPass);
			}
		}

		protected override void Dispose(bool disposing)
		{
			m_SSAOPass?.Dispose();
			m_SSAOPass = null;
			global::UnityEngine.Rendering.CoreUtils.Destroy(m_Material);
		}

		private bool TryPrepareResources()
		{
			if (m_Shader == null)
			{
				if (!global::UnityEngine.Rendering.GraphicsSettings.TryGetRenderPipelineSettings<global::UnityEngine.Rendering.Universal.ScreenSpaceAmbientOcclusionPersistentResources>(out var screenSpaceAmbientOcclusionPersistentResources))
				{
					global::UnityEngine.Debug.LogErrorFormat("Couldn't find the required resources for the ScreenSpaceAmbientOcclusion render feature. If this exception appears in the Player, make sure at least one ScreenSpaceAmbientOcclusion render feature is enabled or adjust your stripping settings.");
					return false;
				}
				m_Shader = screenSpaceAmbientOcclusionPersistentResources.Shader;
			}
			if (m_Settings.AOMethod == global::UnityEngine.Rendering.Universal.ScreenSpaceAmbientOcclusionSettings.AOMethodOptions.BlueNoise && (m_BlueNoise256Textures == null || m_BlueNoise256Textures.Length == 0))
			{
				if (!global::UnityEngine.Rendering.GraphicsSettings.TryGetRenderPipelineSettings<global::UnityEngine.Rendering.Universal.ScreenSpaceAmbientOcclusionDynamicResources>(out var screenSpaceAmbientOcclusionDynamicResources))
				{
					global::UnityEngine.Debug.LogErrorFormat("Couldn't load BlueNoise256Textures. If this exception appears in the Player, please check the SSAO options for ScreenSpaceAmbientOcclusion or adjust your stripping settings");
					return false;
				}
				m_BlueNoise256Textures = screenSpaceAmbientOcclusionDynamicResources.BlueNoise256Textures;
			}
			if (m_Material == null && m_Shader != null)
			{
				m_Material = global::UnityEngine.Rendering.CoreUtils.CreateEngineMaterial(m_Shader);
			}
			if (m_Material == null)
			{
				global::UnityEngine.Debug.LogError(GetType().Name + ".AddRenderPasses(): Missing material. " + base.name + " render pass will not be added.");
				return false;
			}
			return true;
		}
	}
}
