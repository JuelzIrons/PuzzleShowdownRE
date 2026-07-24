namespace UnityEngine.Rendering.Universal
{
	[global::System.Serializable]
	[global::UnityEngine.Rendering.ReloadGroup]
	[global::System.Obsolete("Moved to UniversalRenderPipelineRuntimeShaders on GraphicsSettings. #from(2023.3)")]
	public sealed class ShaderResources
	{
		[global::System.Obsolete("Moved to UniversalRenderPipelineRuntimeShaders on GraphicsSettings. #from(2023.3)")]
		[global::UnityEngine.Rendering.Reload("Shaders/Utils/Blit.shader", global::UnityEngine.Rendering.ReloadAttribute.Package.Root)]
		public global::UnityEngine.Shader blitPS;

		[global::UnityEngine.Rendering.Reload("Shaders/Utils/CopyDepth.shader", global::UnityEngine.Rendering.ReloadAttribute.Package.Root)]
		[global::System.Obsolete("Moved to UniversalRenderPipelineRuntimeShaders on GraphicsSettings. #from(2023.3)")]
		public global::UnityEngine.Shader copyDepthPS;

		[global::System.Obsolete("Obsolete, this feature will be supported by new 'ScreenSpaceShadows' renderer feature. #from(2023.3) #breakingFrom(2023.3)", true)]
		public global::UnityEngine.Shader screenSpaceShadowPS;

		[global::System.Obsolete("Moved to UniversalRenderPipelineRuntimeShaders on GraphicsSettings. #from(2023.3)")]
		[global::UnityEngine.Rendering.Reload("Shaders/Utils/Sampling.shader", global::UnityEngine.Rendering.ReloadAttribute.Package.Root)]
		public global::UnityEngine.Shader samplingPS;

		[global::UnityEngine.Rendering.Reload("Shaders/Utils/StencilDeferred.shader", global::UnityEngine.Rendering.ReloadAttribute.Package.Root)]
		[global::System.Obsolete("Moved to UniversalRenderPipelineRuntimeShaders on GraphicsSettings. #from(2023.3)")]
		public global::UnityEngine.Shader stencilDeferredPS;

		[global::UnityEngine.Rendering.Reload("Shaders/Utils/FallbackError.shader", global::UnityEngine.Rendering.ReloadAttribute.Package.Root)]
		[global::System.Obsolete("Moved to UniversalRenderPipelineRuntimeShaders on GraphicsSettings. #from(2023.3)")]
		public global::UnityEngine.Shader fallbackErrorPS;

		[global::UnityEngine.Rendering.Reload("Shaders/Utils/FallbackLoading.shader", global::UnityEngine.Rendering.ReloadAttribute.Package.Root)]
		[global::System.Obsolete("Moved to UniversalRenderPipelineRuntimeShaders on GraphicsSettings. #from(2023.3)")]
		public global::UnityEngine.Shader fallbackLoadingPS;

		[global::System.Obsolete("Use fallbackErrorPS instead. #from(2023.3) #breakingFrom(2023.3)", true)]
		public global::UnityEngine.Shader materialErrorPS;

		[global::UnityEngine.Rendering.Reload("Shaders/Utils/CoreBlit.shader", global::UnityEngine.Rendering.ReloadAttribute.Package.Root)]
		[global::UnityEngine.SerializeField]
		[global::System.Obsolete("Moved to UniversalRenderPipelineRuntimeShaders on GraphicsSettings. #from(2023.3)")]
		internal global::UnityEngine.Shader coreBlitPS;

		[global::UnityEngine.Rendering.Reload("Shaders/Utils/CoreBlitColorAndDepth.shader", global::UnityEngine.Rendering.ReloadAttribute.Package.Root)]
		[global::UnityEngine.SerializeField]
		[global::System.Obsolete("Moved to UniversalRenderPipelineRuntimeShaders on GraphicsSettings. #from(2023.3)")]
		internal global::UnityEngine.Shader coreBlitColorAndDepthPS;

		[global::UnityEngine.Rendering.Reload("Shaders/Utils/BlitHDROverlay.shader", global::UnityEngine.Rendering.ReloadAttribute.Package.Root)]
		[global::UnityEngine.SerializeField]
		[global::System.Obsolete("Moved to UniversalRenderPipelineRuntimeShaders on GraphicsSettings. #from(2023.3)")]
		internal global::UnityEngine.Shader blitHDROverlay;

		[global::UnityEngine.Rendering.Reload("Shaders/CameraMotionVectors.shader", global::UnityEngine.Rendering.ReloadAttribute.Package.Root)]
		[global::System.Obsolete("Moved to UniversalRenderPipelineRuntimeShaders on GraphicsSettings. #from(2023.3)")]
		public global::UnityEngine.Shader cameraMotionVector;

		[global::UnityEngine.Rendering.Reload("Shaders/PostProcessing/LensFlareScreenSpace.shader", global::UnityEngine.Rendering.ReloadAttribute.Package.Root)]
		[global::System.Obsolete("Moved to UniversalRenderPipelineRuntimeShaders on GraphicsSettings. #from(2023.3)")]
		public global::UnityEngine.Shader screenSpaceLensFlare;

		[global::UnityEngine.Rendering.Reload("Shaders/PostProcessing/LensFlareDataDriven.shader", global::UnityEngine.Rendering.ReloadAttribute.Package.Root)]
		[global::System.Obsolete("Moved to UniversalRenderPipelineRuntimeShaders on GraphicsSettings. #from(2023.3)")]
		public global::UnityEngine.Shader dataDrivenLensFlare;
	}
}
