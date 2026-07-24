namespace UnityEngine.Rendering
{
	[global::System.Serializable]
	[global::UnityEngine.Rendering.SupportedOnRenderPipeline(new global::System.Type[] { })]
	[global::UnityEngine.Categorization.CategoryInfo(Name = "Additional Shader Stripping Settings", Order = 40)]
	[global::UnityEngine.Categorization.ElementInfo(Order = 0)]
	public class ShaderStrippingSetting : global::UnityEngine.Rendering.IRenderPipelineGraphicsSettings
	{
		internal enum Version
		{
			Initial = 0
		}

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.HideInInspector]
		private global::UnityEngine.Rendering.ShaderStrippingSetting.Version m_Version;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Tooltip("Controls whether to output shader variant information to a file.")]
		private bool m_ExportShaderVariants = true;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Tooltip("Controls the level of logging of shader variant information outputted during the build process. Information appears in the Unity Console when the build finishes.")]
		private global::UnityEngine.Rendering.ShaderVariantLogLevel m_ShaderVariantLogLevel;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Tooltip("When enabled, all debug display shader variants are removed when you build for the Unity Player. This decreases build time, but prevents the use of most Rendering Debugger features in Player builds.")]
		private bool m_StripRuntimeDebugShaders = true;

		public int version => (int)m_Version;

		bool global::UnityEngine.Rendering.IRenderPipelineGraphicsSettings.isAvailableInPlayerBuild => true;

		public bool exportShaderVariants
		{
			get
			{
				return m_ExportShaderVariants;
			}
			set
			{
				this.SetValueAndNotify(ref m_ExportShaderVariants, value, "exportShaderVariants");
			}
		}

		public global::UnityEngine.Rendering.ShaderVariantLogLevel shaderVariantLogLevel
		{
			get
			{
				return m_ShaderVariantLogLevel;
			}
			set
			{
				this.SetValueAndNotify(ref m_ShaderVariantLogLevel, value, "shaderVariantLogLevel");
			}
		}

		public bool stripRuntimeDebugShaders
		{
			get
			{
				return m_StripRuntimeDebugShaders;
			}
			set
			{
				this.SetValueAndNotify(ref m_StripRuntimeDebugShaders, value, "stripRuntimeDebugShaders");
			}
		}
	}
}
