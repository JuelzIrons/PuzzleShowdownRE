namespace UnityEngine.Rendering.Universal
{
	internal static class ShaderGlobalKeywords
	{
		public static global::UnityEngine.Rendering.GlobalKeyword MainLightShadows;

		public static global::UnityEngine.Rendering.GlobalKeyword MainLightShadowCascades;

		public static global::UnityEngine.Rendering.GlobalKeyword MainLightShadowScreen;

		public static global::UnityEngine.Rendering.GlobalKeyword CastingPunctualLightShadow;

		public static global::UnityEngine.Rendering.GlobalKeyword AdditionalLightsVertex;

		public static global::UnityEngine.Rendering.GlobalKeyword AdditionalLightsPixel;

		public static global::UnityEngine.Rendering.GlobalKeyword ClusterLightLoop;

		public static global::UnityEngine.Rendering.GlobalKeyword AdditionalLightShadows;

		public static global::UnityEngine.Rendering.GlobalKeyword ReflectionProbeBoxProjection;

		public static global::UnityEngine.Rendering.GlobalKeyword ReflectionProbeBlending;

		public static global::UnityEngine.Rendering.GlobalKeyword ReflectionProbeAtlas;

		public static global::UnityEngine.Rendering.GlobalKeyword ReflectionProbeRotation;

		public static global::UnityEngine.Rendering.GlobalKeyword SoftShadows;

		public static global::UnityEngine.Rendering.GlobalKeyword SoftShadowsLow;

		public static global::UnityEngine.Rendering.GlobalKeyword SoftShadowsMedium;

		public static global::UnityEngine.Rendering.GlobalKeyword SoftShadowsHigh;

		public static global::UnityEngine.Rendering.GlobalKeyword MixedLightingSubtractive;

		public static global::UnityEngine.Rendering.GlobalKeyword LightmapShadowMixing;

		public static global::UnityEngine.Rendering.GlobalKeyword ShadowsShadowMask;

		public static global::UnityEngine.Rendering.GlobalKeyword LightLayers;

		public static global::UnityEngine.Rendering.GlobalKeyword RenderPassEnabled;

		public static global::UnityEngine.Rendering.GlobalKeyword BillboardFaceCameraPos;

		public static global::UnityEngine.Rendering.GlobalKeyword LightCookies;

		public static global::UnityEngine.Rendering.GlobalKeyword DepthNoMsaa;

		public static global::UnityEngine.Rendering.GlobalKeyword DepthMsaa2;

		public static global::UnityEngine.Rendering.GlobalKeyword DepthMsaa4;

		public static global::UnityEngine.Rendering.GlobalKeyword DepthMsaa8;

		public static global::UnityEngine.Rendering.GlobalKeyword DBufferMRT1;

		public static global::UnityEngine.Rendering.GlobalKeyword DBufferMRT2;

		public static global::UnityEngine.Rendering.GlobalKeyword DBufferMRT3;

		public static global::UnityEngine.Rendering.GlobalKeyword DecalNormalBlendLow;

		public static global::UnityEngine.Rendering.GlobalKeyword DecalNormalBlendMedium;

		public static global::UnityEngine.Rendering.GlobalKeyword DecalNormalBlendHigh;

		public static global::UnityEngine.Rendering.GlobalKeyword DecalLayers;

		public static global::UnityEngine.Rendering.GlobalKeyword WriteRenderingLayers;

		public static global::UnityEngine.Rendering.GlobalKeyword ScreenSpaceOcclusion;

		public static global::UnityEngine.Rendering.GlobalKeyword ScreenSpaceIrradiance;

		public static global::UnityEngine.Rendering.GlobalKeyword _SPOT;

		public static global::UnityEngine.Rendering.GlobalKeyword _DIRECTIONAL;

		public static global::UnityEngine.Rendering.GlobalKeyword _POINT;

		public static global::UnityEngine.Rendering.GlobalKeyword _DEFERRED_STENCIL;

		public static global::UnityEngine.Rendering.GlobalKeyword _DEFERRED_FIRST_LIGHT;

		public static global::UnityEngine.Rendering.GlobalKeyword _DEFERRED_MAIN_LIGHT;

		public static global::UnityEngine.Rendering.GlobalKeyword _GBUFFER_NORMALS_OCT;

		public static global::UnityEngine.Rendering.GlobalKeyword _DEFERRED_MIXED_LIGHTING;

		public static global::UnityEngine.Rendering.GlobalKeyword LIGHTMAP_ON;

		public static global::UnityEngine.Rendering.GlobalKeyword DYNAMICLIGHTMAP_ON;

		public static global::UnityEngine.Rendering.GlobalKeyword _ALPHATEST_ON;

		public static global::UnityEngine.Rendering.GlobalKeyword DIRLIGHTMAP_COMBINED;

		public static global::UnityEngine.Rendering.GlobalKeyword _DETAIL_MULX2;

		public static global::UnityEngine.Rendering.GlobalKeyword _DETAIL_SCALED;

		public static global::UnityEngine.Rendering.GlobalKeyword _CLEARCOAT;

		public static global::UnityEngine.Rendering.GlobalKeyword _CLEARCOATMAP;

		public static global::UnityEngine.Rendering.GlobalKeyword DEBUG_DISPLAY;

		public static global::UnityEngine.Rendering.GlobalKeyword LOD_FADE_CROSSFADE;

		public static global::UnityEngine.Rendering.GlobalKeyword USE_UNITY_CROSSFADE;

		public static global::UnityEngine.Rendering.GlobalKeyword _EMISSION;

		public static global::UnityEngine.Rendering.GlobalKeyword _RECEIVE_SHADOWS_OFF;

		public static global::UnityEngine.Rendering.GlobalKeyword _SURFACE_TYPE_TRANSPARENT;

		public static global::UnityEngine.Rendering.GlobalKeyword _ALPHAPREMULTIPLY_ON;

		public static global::UnityEngine.Rendering.GlobalKeyword _ALPHAMODULATE_ON;

		public static global::UnityEngine.Rendering.GlobalKeyword _NORMALMAP;

		public static global::UnityEngine.Rendering.GlobalKeyword _ADD_PRECOMPUTED_VELOCITY;

		public static global::UnityEngine.Rendering.GlobalKeyword EDITOR_VISUALIZATION;

		public static global::UnityEngine.Rendering.GlobalKeyword FoveatedRenderingNonUniformRaster;

		public static global::UnityEngine.Rendering.GlobalKeyword DisableTexture2DXArray;

		public static global::UnityEngine.Rendering.GlobalKeyword BlitSingleSlice;

		public static global::UnityEngine.Rendering.GlobalKeyword XROcclusionMeshCombined;

		public static global::UnityEngine.Rendering.GlobalKeyword SCREEN_COORD_OVERRIDE;

		public static global::UnityEngine.Rendering.GlobalKeyword DOWNSAMPLING_SIZE_2;

		public static global::UnityEngine.Rendering.GlobalKeyword DOWNSAMPLING_SIZE_4;

		public static global::UnityEngine.Rendering.GlobalKeyword DOWNSAMPLING_SIZE_8;

		public static global::UnityEngine.Rendering.GlobalKeyword DOWNSAMPLING_SIZE_16;

		public static global::UnityEngine.Rendering.GlobalKeyword EVALUATE_SH_MIXED;

		public static global::UnityEngine.Rendering.GlobalKeyword EVALUATE_SH_VERTEX;

		public static global::UnityEngine.Rendering.GlobalKeyword ProbeVolumeL1;

		public static global::UnityEngine.Rendering.GlobalKeyword ProbeVolumeL2;

		public static global::UnityEngine.Rendering.GlobalKeyword LIGHTMAP_BICUBIC_SAMPLING;

		public static global::UnityEngine.Rendering.GlobalKeyword _OUTPUT_DEPTH;

		public static global::UnityEngine.Rendering.GlobalKeyword LinearToSRGBConversion;

		public static global::UnityEngine.Rendering.GlobalKeyword _ENABLE_ALPHA_OUTPUT;

		public static global::UnityEngine.Rendering.GlobalKeyword ForwardPlus;

		public static void InitializeShaderGlobalKeywords()
		{
			MainLightShadows = global::UnityEngine.Rendering.GlobalKeyword.Create("_MAIN_LIGHT_SHADOWS");
			MainLightShadowCascades = global::UnityEngine.Rendering.GlobalKeyword.Create("_MAIN_LIGHT_SHADOWS_CASCADE");
			MainLightShadowScreen = global::UnityEngine.Rendering.GlobalKeyword.Create("_MAIN_LIGHT_SHADOWS_SCREEN");
			CastingPunctualLightShadow = global::UnityEngine.Rendering.GlobalKeyword.Create("_CASTING_PUNCTUAL_LIGHT_SHADOW");
			AdditionalLightsVertex = global::UnityEngine.Rendering.GlobalKeyword.Create("_ADDITIONAL_LIGHTS_VERTEX");
			AdditionalLightsPixel = global::UnityEngine.Rendering.GlobalKeyword.Create("_ADDITIONAL_LIGHTS");
			ClusterLightLoop = global::UnityEngine.Rendering.GlobalKeyword.Create("_CLUSTER_LIGHT_LOOP");
			AdditionalLightShadows = global::UnityEngine.Rendering.GlobalKeyword.Create("_ADDITIONAL_LIGHT_SHADOWS");
			ReflectionProbeBoxProjection = global::UnityEngine.Rendering.GlobalKeyword.Create("_REFLECTION_PROBE_BOX_PROJECTION");
			ReflectionProbeBlending = global::UnityEngine.Rendering.GlobalKeyword.Create("_REFLECTION_PROBE_BLENDING");
			ReflectionProbeAtlas = global::UnityEngine.Rendering.GlobalKeyword.Create("_REFLECTION_PROBE_ATLAS");
			ReflectionProbeRotation = global::UnityEngine.Rendering.GlobalKeyword.Create("REFLECTION_PROBE_ROTATION");
			SoftShadows = global::UnityEngine.Rendering.GlobalKeyword.Create("_SHADOWS_SOFT");
			SoftShadowsLow = global::UnityEngine.Rendering.GlobalKeyword.Create("_SHADOWS_SOFT_LOW");
			SoftShadowsMedium = global::UnityEngine.Rendering.GlobalKeyword.Create("_SHADOWS_SOFT_MEDIUM");
			SoftShadowsHigh = global::UnityEngine.Rendering.GlobalKeyword.Create("_SHADOWS_SOFT_HIGH");
			MixedLightingSubtractive = global::UnityEngine.Rendering.GlobalKeyword.Create("_MIXED_LIGHTING_SUBTRACTIVE");
			LightmapShadowMixing = global::UnityEngine.Rendering.GlobalKeyword.Create("LIGHTMAP_SHADOW_MIXING");
			ShadowsShadowMask = global::UnityEngine.Rendering.GlobalKeyword.Create("SHADOWS_SHADOWMASK");
			LightLayers = global::UnityEngine.Rendering.GlobalKeyword.Create("_LIGHT_LAYERS");
			RenderPassEnabled = global::UnityEngine.Rendering.GlobalKeyword.Create("_RENDER_PASS_ENABLED");
			BillboardFaceCameraPos = global::UnityEngine.Rendering.GlobalKeyword.Create("BILLBOARD_FACE_CAMERA_POS");
			LightCookies = global::UnityEngine.Rendering.GlobalKeyword.Create("_LIGHT_COOKIES");
			DepthNoMsaa = global::UnityEngine.Rendering.GlobalKeyword.Create("_DEPTH_NO_MSAA");
			DepthMsaa2 = global::UnityEngine.Rendering.GlobalKeyword.Create("_DEPTH_MSAA_2");
			DepthMsaa4 = global::UnityEngine.Rendering.GlobalKeyword.Create("_DEPTH_MSAA_4");
			DepthMsaa8 = global::UnityEngine.Rendering.GlobalKeyword.Create("_DEPTH_MSAA_8");
			DBufferMRT1 = global::UnityEngine.Rendering.GlobalKeyword.Create("_DBUFFER_MRT1");
			DBufferMRT2 = global::UnityEngine.Rendering.GlobalKeyword.Create("_DBUFFER_MRT2");
			DBufferMRT3 = global::UnityEngine.Rendering.GlobalKeyword.Create("_DBUFFER_MRT3");
			DecalNormalBlendLow = global::UnityEngine.Rendering.GlobalKeyword.Create("_DECAL_NORMAL_BLEND_LOW");
			DecalNormalBlendMedium = global::UnityEngine.Rendering.GlobalKeyword.Create("_DECAL_NORMAL_BLEND_MEDIUM");
			DecalNormalBlendHigh = global::UnityEngine.Rendering.GlobalKeyword.Create("_DECAL_NORMAL_BLEND_HIGH");
			DecalLayers = global::UnityEngine.Rendering.GlobalKeyword.Create("_DECAL_LAYERS");
			WriteRenderingLayers = global::UnityEngine.Rendering.GlobalKeyword.Create("_WRITE_RENDERING_LAYERS");
			ScreenSpaceOcclusion = global::UnityEngine.Rendering.GlobalKeyword.Create("_SCREEN_SPACE_OCCLUSION");
			ScreenSpaceIrradiance = global::UnityEngine.Rendering.GlobalKeyword.Create("_SCREEN_SPACE_IRRADIANCE");
			_SPOT = global::UnityEngine.Rendering.GlobalKeyword.Create("_SPOT");
			_DIRECTIONAL = global::UnityEngine.Rendering.GlobalKeyword.Create("_DIRECTIONAL");
			_POINT = global::UnityEngine.Rendering.GlobalKeyword.Create("_POINT");
			_DEFERRED_STENCIL = global::UnityEngine.Rendering.GlobalKeyword.Create("_DEFERRED_STENCIL");
			_DEFERRED_FIRST_LIGHT = global::UnityEngine.Rendering.GlobalKeyword.Create("_DEFERRED_FIRST_LIGHT");
			_DEFERRED_MAIN_LIGHT = global::UnityEngine.Rendering.GlobalKeyword.Create("_DEFERRED_MAIN_LIGHT");
			_GBUFFER_NORMALS_OCT = global::UnityEngine.Rendering.GlobalKeyword.Create("_GBUFFER_NORMALS_OCT");
			_DEFERRED_MIXED_LIGHTING = global::UnityEngine.Rendering.GlobalKeyword.Create("_DEFERRED_MIXED_LIGHTING");
			LIGHTMAP_ON = global::UnityEngine.Rendering.GlobalKeyword.Create("LIGHTMAP_ON");
			DYNAMICLIGHTMAP_ON = global::UnityEngine.Rendering.GlobalKeyword.Create("DYNAMICLIGHTMAP_ON");
			_ALPHATEST_ON = global::UnityEngine.Rendering.GlobalKeyword.Create("_ALPHATEST_ON");
			DIRLIGHTMAP_COMBINED = global::UnityEngine.Rendering.GlobalKeyword.Create("DIRLIGHTMAP_COMBINED");
			_DETAIL_MULX2 = global::UnityEngine.Rendering.GlobalKeyword.Create("_DETAIL_MULX2");
			_DETAIL_SCALED = global::UnityEngine.Rendering.GlobalKeyword.Create("_DETAIL_SCALED");
			_CLEARCOAT = global::UnityEngine.Rendering.GlobalKeyword.Create("_CLEARCOAT");
			_CLEARCOATMAP = global::UnityEngine.Rendering.GlobalKeyword.Create("_CLEARCOATMAP");
			DEBUG_DISPLAY = global::UnityEngine.Rendering.GlobalKeyword.Create("DEBUG_DISPLAY");
			LOD_FADE_CROSSFADE = global::UnityEngine.Rendering.GlobalKeyword.Create("LOD_FADE_CROSSFADE");
			USE_UNITY_CROSSFADE = global::UnityEngine.Rendering.GlobalKeyword.Create("USE_UNITY_CROSSFADE");
			_EMISSION = global::UnityEngine.Rendering.GlobalKeyword.Create("_EMISSION");
			_RECEIVE_SHADOWS_OFF = global::UnityEngine.Rendering.GlobalKeyword.Create("_RECEIVE_SHADOWS_OFF");
			_SURFACE_TYPE_TRANSPARENT = global::UnityEngine.Rendering.GlobalKeyword.Create("_SURFACE_TYPE_TRANSPARENT");
			_ALPHAPREMULTIPLY_ON = global::UnityEngine.Rendering.GlobalKeyword.Create("_ALPHAPREMULTIPLY_ON");
			_ALPHAMODULATE_ON = global::UnityEngine.Rendering.GlobalKeyword.Create("_ALPHAMODULATE_ON");
			_NORMALMAP = global::UnityEngine.Rendering.GlobalKeyword.Create("_NORMALMAP");
			_ADD_PRECOMPUTED_VELOCITY = global::UnityEngine.Rendering.GlobalKeyword.Create("_ADD_PRECOMPUTED_VELOCITY");
			EDITOR_VISUALIZATION = global::UnityEngine.Rendering.GlobalKeyword.Create("EDITOR_VISUALIZATION");
			FoveatedRenderingNonUniformRaster = global::UnityEngine.Rendering.GlobalKeyword.Create("_FOVEATED_RENDERING_NON_UNIFORM_RASTER");
			DisableTexture2DXArray = global::UnityEngine.Rendering.GlobalKeyword.Create("DISABLE_TEXTURE2D_X_ARRAY");
			BlitSingleSlice = global::UnityEngine.Rendering.GlobalKeyword.Create("BLIT_SINGLE_SLICE");
			XROcclusionMeshCombined = global::UnityEngine.Rendering.GlobalKeyword.Create("XR_OCCLUSION_MESH_COMBINED");
			SCREEN_COORD_OVERRIDE = global::UnityEngine.Rendering.GlobalKeyword.Create("SCREEN_COORD_OVERRIDE");
			DOWNSAMPLING_SIZE_2 = global::UnityEngine.Rendering.GlobalKeyword.Create("DOWNSAMPLING_SIZE_2");
			DOWNSAMPLING_SIZE_4 = global::UnityEngine.Rendering.GlobalKeyword.Create("DOWNSAMPLING_SIZE_4");
			DOWNSAMPLING_SIZE_8 = global::UnityEngine.Rendering.GlobalKeyword.Create("DOWNSAMPLING_SIZE_8");
			DOWNSAMPLING_SIZE_16 = global::UnityEngine.Rendering.GlobalKeyword.Create("DOWNSAMPLING_SIZE_16");
			EVALUATE_SH_MIXED = global::UnityEngine.Rendering.GlobalKeyword.Create("EVALUATE_SH_MIXED");
			EVALUATE_SH_VERTEX = global::UnityEngine.Rendering.GlobalKeyword.Create("EVALUATE_SH_VERTEX");
			ProbeVolumeL1 = global::UnityEngine.Rendering.GlobalKeyword.Create("PROBE_VOLUMES_L1");
			ProbeVolumeL2 = global::UnityEngine.Rendering.GlobalKeyword.Create("PROBE_VOLUMES_L2");
			LIGHTMAP_BICUBIC_SAMPLING = global::UnityEngine.Rendering.GlobalKeyword.Create("LIGHTMAP_BICUBIC_SAMPLING");
			_OUTPUT_DEPTH = global::UnityEngine.Rendering.GlobalKeyword.Create("_OUTPUT_DEPTH");
			LinearToSRGBConversion = global::UnityEngine.Rendering.GlobalKeyword.Create("_LINEAR_TO_SRGB_CONVERSION");
			_ENABLE_ALPHA_OUTPUT = global::UnityEngine.Rendering.GlobalKeyword.Create("_ENABLE_ALPHA_OUTPUT");
			ForwardPlus = global::UnityEngine.Rendering.GlobalKeyword.Create("_FORWARD_PLUS");
		}
	}
}
