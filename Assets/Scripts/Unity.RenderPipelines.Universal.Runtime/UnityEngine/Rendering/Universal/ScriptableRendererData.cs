namespace UnityEngine.Rendering.Universal
{
	public abstract class ScriptableRendererData : global::UnityEngine.ScriptableObject
	{
		[global::System.Serializable]
		[global::System.Obsolete("Moved to UniversalRenderPipelineDebugShaders on GraphicsSettings. #from(2023.3)")]
		[global::UnityEngine.Rendering.ReloadGroup]
		public sealed class DebugShaderResources
		{
			[global::System.Obsolete("Moved to UniversalRenderPipelineDebugShaders on GraphicsSettings. #from(2023.3)")]
			[global::UnityEngine.Rendering.Reload("Shaders/Debug/DebugReplacement.shader", global::UnityEngine.Rendering.ReloadAttribute.Package.Root)]
			public global::UnityEngine.Shader debugReplacementPS;

			[global::System.Obsolete("Moved to UniversalRenderPipelineDebugShaders on GraphicsSettings. #from(2023.3)")]
			[global::UnityEngine.Rendering.Reload("Shaders/Debug/HDRDebugView.shader", global::UnityEngine.Rendering.ReloadAttribute.Package.Root)]
			public global::UnityEngine.Shader hdrDebugViewPS;
		}

		[global::System.Serializable]
		[global::UnityEngine.Rendering.ReloadGroup]
		[global::System.Obsolete("Probe volume debug resource are now in the ProbeVolumeDebugResources class. #from(2023.3)")]
		public sealed class ProbeVolumeResources
		{
			[global::System.Obsolete("This shader is now in the ProbeVolumeDebugResources class. #from(2023.3)")]
			public global::UnityEngine.Shader probeVolumeDebugShader;

			[global::System.Obsolete("This shader is now in the ProbeVolumeDebugResources class. #from(2023.3)")]
			public global::UnityEngine.Shader probeVolumeFragmentationDebugShader;

			[global::System.Obsolete("This shader is now in the ProbeVolumeDebugResources class. #from(2023.3)")]
			public global::UnityEngine.Shader probeVolumeOffsetDebugShader;

			[global::System.Obsolete("This shader is now in the ProbeVolumeDebugResources class. #from(2023.3)")]
			public global::UnityEngine.Shader probeVolumeSamplingDebugShader;

			[global::System.Obsolete("This shader is now in the ProbeVolumeDebugResources class. #from(2023.3)")]
			public global::UnityEngine.Mesh probeSamplingDebugMesh;

			[global::System.Obsolete("This shader is now in the ProbeVolumeDebugResources class. #from(2023.3)")]
			public global::UnityEngine.Texture2D probeSamplingDebugTexture;

			[global::System.Obsolete("This shader is now in the ProbeVolumeRuntimeResources class. #from(2023.3)")]
			public global::UnityEngine.ComputeShader probeVolumeBlendStatesCS;
		}

		[global::System.Obsolete("Moved to UniversalRenderPipelineDebugShaders on GraphicsSettings. #from(2023.3)")]
		public global::UnityEngine.Rendering.Universal.ScriptableRendererData.DebugShaderResources debugShaders;

		[global::System.Obsolete("Probe volume debug resource are now in the ProbeVolumeDebugResources class. #from(2023.3)")]
		public global::UnityEngine.Rendering.Universal.ScriptableRendererData.ProbeVolumeResources probeVolumeResources;

		[global::UnityEngine.SerializeField]
		internal global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.ScriptableRendererFeature> m_RendererFeatures = new global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.ScriptableRendererFeature>(10);

		[global::UnityEngine.SerializeField]
		internal global::System.Collections.Generic.List<long> m_RendererFeatureMap = new global::System.Collections.Generic.List<long>(10);

		[global::UnityEngine.SerializeField]
		private bool m_UseNativeRenderPass;

		[global::System.NonSerialized]
		private bool m_StripShadowsOffVariants;

		[global::System.NonSerialized]
		private bool m_StripAdditionalLightOffVariants;

		internal bool isInvalidated { get; set; }

		internal virtual bool stripShadowsOffVariants
		{
			get
			{
				return m_StripShadowsOffVariants;
			}
			set
			{
				m_StripShadowsOffVariants = value;
			}
		}

		internal virtual bool stripAdditionalLightOffVariants
		{
			get
			{
				return m_StripAdditionalLightOffVariants;
			}
			set
			{
				m_StripAdditionalLightOffVariants = value;
			}
		}

		public global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.ScriptableRendererFeature> rendererFeatures => m_RendererFeatures;

		public bool useNativeRenderPass
		{
			get
			{
				return m_UseNativeRenderPass;
			}
			set
			{
				SetDirty();
				m_UseNativeRenderPass = value;
			}
		}

		protected abstract global::UnityEngine.Rendering.Universal.ScriptableRenderer Create();

		public new void SetDirty()
		{
			isInvalidated = true;
		}

		internal global::UnityEngine.Rendering.Universal.ScriptableRenderer InternalCreateRenderer()
		{
			isInvalidated = false;
			return Create();
		}

		protected virtual void OnValidate()
		{
			SetDirty();
		}

		protected virtual void OnEnable()
		{
			SetDirty();
		}

		public bool TryGetRendererFeature<T>(out T rendererFeature) where T : global::UnityEngine.Rendering.Universal.ScriptableRendererFeature
		{
			foreach (global::UnityEngine.Rendering.Universal.ScriptableRendererFeature rendererFeature2 in rendererFeatures)
			{
				if (rendererFeature2.GetType() == typeof(T))
				{
					rendererFeature = rendererFeature2 as T;
					return true;
				}
			}
			rendererFeature = null;
			return false;
		}
	}
}
