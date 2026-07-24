namespace UnityEngine.Rendering
{
	[global::System.Obsolete("Use GraphicsSettings.GetRenderPipelineSettings<ShaderStrippingSetting>(). #from(2023.3)")]
	public interface IShaderVariantSettings
	{
		global::UnityEngine.Rendering.ShaderVariantLogLevel shaderVariantLogLevel { get; set; }

		bool exportShaderVariants { get; set; }

		bool stripDebugVariants
		{
			get
			{
				return false;
			}
			set
			{
			}
		}
	}
}
