namespace UnityEngine.Rendering.Universal
{
	[global::System.Obsolete("Moved to GraphicsSettings. #from(2023.3)")]
	public class UniversalRenderPipelineEditorResources : global::UnityEngine.ScriptableObject
	{
		[global::System.Serializable]
		[global::UnityEngine.Rendering.ReloadGroup]
		[global::System.Obsolete("UniversalRenderPipelineEditorResources.ShaderResources is obsolete GraphicsSettings.TryGetRenderPipelineSettings<UniversalRenderPipelineEditorShaders>(). #from(2023.3)")]
		public sealed class ShaderResources
		{
			[global::UnityEngine.Rendering.Reload("Shaders/AutodeskInteractive/AutodeskInteractive.shadergraph", global::UnityEngine.Rendering.ReloadAttribute.Package.Root)]
			public global::UnityEngine.Shader autodeskInteractivePS;

			[global::UnityEngine.Rendering.Reload("Shaders/AutodeskInteractive/AutodeskInteractiveTransparent.shadergraph", global::UnityEngine.Rendering.ReloadAttribute.Package.Root)]
			public global::UnityEngine.Shader autodeskInteractiveTransparentPS;

			[global::UnityEngine.Rendering.Reload("Shaders/AutodeskInteractive/AutodeskInteractiveMasked.shadergraph", global::UnityEngine.Rendering.ReloadAttribute.Package.Root)]
			public global::UnityEngine.Shader autodeskInteractiveMaskedPS;

			[global::UnityEngine.Rendering.Reload("Shaders/Terrain/TerrainDetailLit.shader", global::UnityEngine.Rendering.ReloadAttribute.Package.Root)]
			public global::UnityEngine.Shader terrainDetailLitPS;

			[global::UnityEngine.Rendering.Reload("Shaders/Terrain/WavingGrass.shader", global::UnityEngine.Rendering.ReloadAttribute.Package.Root)]
			public global::UnityEngine.Shader terrainDetailGrassPS;

			[global::UnityEngine.Rendering.Reload("Shaders/Terrain/WavingGrassBillboard.shader", global::UnityEngine.Rendering.ReloadAttribute.Package.Root)]
			public global::UnityEngine.Shader terrainDetailGrassBillboardPS;

			[global::UnityEngine.Rendering.Reload("Shaders/Nature/SpeedTree7.shader", global::UnityEngine.Rendering.ReloadAttribute.Package.Root)]
			public global::UnityEngine.Shader defaultSpeedTree7PS;

			[global::UnityEngine.Rendering.Reload("Shaders/Nature/SpeedTree8_PBRLit.shadergraph", global::UnityEngine.Rendering.ReloadAttribute.Package.Root)]
			public global::UnityEngine.Shader defaultSpeedTree8PS;
		}

		[global::System.Serializable]
		[global::UnityEngine.Rendering.ReloadGroup]
		[global::System.Obsolete("UniversalRenderPipelineEditorResources.MaterialResources is obsolete GraphicsSettings.TryGetRenderPipelineSettings<UniversalRenderPipelineEditorMaterials>(). #from(2023.3)")]
		public sealed class MaterialResources
		{
			[global::UnityEngine.Rendering.Reload("Runtime/Materials/Lit.mat", global::UnityEngine.Rendering.ReloadAttribute.Package.Root)]
			public global::UnityEngine.Material lit;

			[global::UnityEngine.Rendering.Reload("Runtime/Materials/ParticlesUnlit.mat", global::UnityEngine.Rendering.ReloadAttribute.Package.Root)]
			public global::UnityEngine.Material particleLit;

			[global::UnityEngine.Rendering.Reload("Runtime/Materials/TerrainLit.mat", global::UnityEngine.Rendering.ReloadAttribute.Package.Root)]
			public global::UnityEngine.Material terrainLit;

			[global::UnityEngine.Rendering.Reload("Runtime/Materials/Decal.mat", global::UnityEngine.Rendering.ReloadAttribute.Package.Root)]
			public global::UnityEngine.Material decal;
		}

		[global::System.Obsolete("UniversalRenderPipelineEditorResources.ShaderResources is obsolete GraphicsSettings.TryGetRenderPipelineSettings<UniversalRenderPipelineEditorShaders>(). #from(2023.3)")]
		public global::UnityEngine.Rendering.Universal.UniversalRenderPipelineEditorResources.ShaderResources shaders;

		[global::System.Obsolete("UniversalRenderPipelineEditorResources.MaterialResources is obsolete GraphicsSettings.TryGetRenderPipelineSettings<UniversalRenderPipelineEditorMaterials>(). #from(2023.3)")]
		public global::UnityEngine.Rendering.Universal.UniversalRenderPipelineEditorResources.MaterialResources materials;
	}
}
