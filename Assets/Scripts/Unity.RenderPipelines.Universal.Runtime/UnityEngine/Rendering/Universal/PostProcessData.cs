namespace UnityEngine.Rendering.Universal
{
	[global::System.Serializable]
	public class PostProcessData : global::UnityEngine.ScriptableObject
	{
		[global::System.Serializable]
		[global::UnityEngine.Rendering.SupportedOnRenderPipeline(typeof(global::UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset))]
		[global::UnityEngine.Categorization.CategoryInfo(Name = "R: Default PostProcess Shaders", Order = 1000)]
		[global::UnityEngine.Categorization.ElementInfo(Order = 0)]
		[global::UnityEngine.HideInInspector]
		public sealed class ShaderResources : global::UnityEngine.Rendering.IRenderPipelineResources, global::UnityEngine.Rendering.IRenderPipelineGraphicsSettings
		{
			[global::UnityEngine.Rendering.ResourcePath("Shaders/PostProcessing/StopNaN.shader", global::UnityEngine.Rendering.SearchType.ProjectPath)]
			public global::UnityEngine.Shader stopNanPS;

			[global::UnityEngine.Rendering.ResourcePath("Shaders/PostProcessing/SubpixelMorphologicalAntialiasing.shader", global::UnityEngine.Rendering.SearchType.ProjectPath)]
			public global::UnityEngine.Shader subpixelMorphologicalAntialiasingPS;

			[global::UnityEngine.Rendering.ResourcePath("Shaders/PostProcessing/GaussianDepthOfField.shader", global::UnityEngine.Rendering.SearchType.ProjectPath)]
			public global::UnityEngine.Shader gaussianDepthOfFieldPS;

			[global::UnityEngine.Rendering.ResourcePath("Shaders/PostProcessing/BokehDepthOfField.shader", global::UnityEngine.Rendering.SearchType.ProjectPath)]
			public global::UnityEngine.Shader bokehDepthOfFieldPS;

			[global::UnityEngine.Rendering.ResourcePath("Shaders/PostProcessing/CameraMotionBlur.shader", global::UnityEngine.Rendering.SearchType.ProjectPath)]
			public global::UnityEngine.Shader cameraMotionBlurPS;

			[global::UnityEngine.Rendering.ResourcePath("Shaders/PostProcessing/PaniniProjection.shader", global::UnityEngine.Rendering.SearchType.ProjectPath)]
			public global::UnityEngine.Shader paniniProjectionPS;

			[global::UnityEngine.Rendering.ResourcePath("Shaders/PostProcessing/LutBuilderLdr.shader", global::UnityEngine.Rendering.SearchType.ProjectPath)]
			public global::UnityEngine.Shader lutBuilderLdrPS;

			[global::UnityEngine.Rendering.ResourcePath("Shaders/PostProcessing/LutBuilderHdr.shader", global::UnityEngine.Rendering.SearchType.ProjectPath)]
			public global::UnityEngine.Shader lutBuilderHdrPS;

			[global::UnityEngine.Rendering.ResourcePath("Shaders/PostProcessing/Bloom.shader", global::UnityEngine.Rendering.SearchType.ProjectPath)]
			public global::UnityEngine.Shader bloomPS;

			[global::UnityEngine.Rendering.ResourcePath("Shaders/PostProcessing/TemporalAA.shader", global::UnityEngine.Rendering.SearchType.ProjectPath)]
			public global::UnityEngine.Shader temporalAntialiasingPS;

			[global::UnityEngine.Rendering.ResourcePath("Shaders/PostProcessing/LensFlareDataDriven.shader", global::UnityEngine.Rendering.SearchType.ProjectPath)]
			public global::UnityEngine.Shader LensFlareDataDrivenPS;

			[global::UnityEngine.Rendering.ResourcePath("Shaders/PostProcessing/LensFlareScreenSpace.shader", global::UnityEngine.Rendering.SearchType.ProjectPath)]
			public global::UnityEngine.Shader LensFlareScreenSpacePS;

			[global::UnityEngine.Rendering.ResourcePath("Shaders/PostProcessing/ScalingSetup.shader", global::UnityEngine.Rendering.SearchType.ProjectPath)]
			public global::UnityEngine.Shader scalingSetupPS;

			[global::UnityEngine.Rendering.ResourcePath("Shaders/PostProcessing/EdgeAdaptiveSpatialUpsampling.shader", global::UnityEngine.Rendering.SearchType.ProjectPath)]
			public global::UnityEngine.Shader easuPS;

			[global::UnityEngine.Rendering.ResourcePath("Shaders/PostProcessing/UberPost.shader", global::UnityEngine.Rendering.SearchType.ProjectPath)]
			public global::UnityEngine.Shader uberPostPS;

			[global::UnityEngine.Rendering.ResourcePath("Shaders/PostProcessing/FinalPost.shader", global::UnityEngine.Rendering.SearchType.ProjectPath)]
			public global::UnityEngine.Shader finalPostPassPS;

			[global::UnityEngine.SerializeField]
			[global::UnityEngine.HideInInspector]
			private int m_ShaderResourcesVersion;

			public int version => m_ShaderResourcesVersion;

			public bool isAvailableInPlayerBuild => false;
		}

		[global::System.Serializable]
		[global::UnityEngine.Rendering.SupportedOnRenderPipeline(typeof(global::UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset))]
		[global::UnityEngine.Categorization.CategoryInfo(Name = "R: Default PostProcess Textures", Order = 1000)]
		[global::UnityEngine.Categorization.ElementInfo(Order = 0)]
		[global::UnityEngine.HideInInspector]
		public sealed class TextureResources : global::UnityEngine.Rendering.IRenderPipelineResources, global::UnityEngine.Rendering.IRenderPipelineGraphicsSettings
		{
			public global::UnityEngine.Texture2D[] blueNoise16LTex;

			[global::UnityEngine.Rendering.ResourcePaths(new string[] { "Textures/FilmGrain/Thin01.png", "Textures/FilmGrain/Thin02.png", "Textures/FilmGrain/Medium01.png", "Textures/FilmGrain/Medium02.png", "Textures/FilmGrain/Medium03.png", "Textures/FilmGrain/Medium04.png", "Textures/FilmGrain/Medium05.png", "Textures/FilmGrain/Medium06.png", "Textures/FilmGrain/Large01.png", "Textures/FilmGrain/Large02.png" }, global::UnityEngine.Rendering.SearchType.ProjectPath)]
			public global::UnityEngine.Texture2D[] filmGrainTex;

			[global::UnityEngine.Rendering.ResourcePath("Textures/SMAA/AreaTex.tga", global::UnityEngine.Rendering.SearchType.ProjectPath)]
			public global::UnityEngine.Texture2D smaaAreaTex;

			[global::UnityEngine.Rendering.ResourcePath("Textures/SMAA/SearchTex.tga", global::UnityEngine.Rendering.SearchType.ProjectPath)]
			public global::UnityEngine.Texture2D smaaSearchTex;

			[global::UnityEngine.SerializeField]
			[global::UnityEngine.HideInInspector]
			private int m_TexturesResourcesVersion;

			public int version => m_TexturesResourcesVersion;

			public bool isAvailableInPlayerBuild => false;
		}

		public global::UnityEngine.Rendering.Universal.PostProcessData.ShaderResources shaders;

		public global::UnityEngine.Rendering.Universal.PostProcessData.TextureResources textures;
	}
}
