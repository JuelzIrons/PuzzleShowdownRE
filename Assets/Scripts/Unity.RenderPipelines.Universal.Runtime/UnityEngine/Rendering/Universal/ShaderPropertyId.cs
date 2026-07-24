namespace UnityEngine.Rendering.Universal
{
	internal static class ShaderPropertyId
	{
		public static readonly int glossyEnvironmentColor = global::UnityEngine.Shader.PropertyToID("_GlossyEnvironmentColor");

		public static readonly int subtractiveShadowColor = global::UnityEngine.Shader.PropertyToID("_SubtractiveShadowColor");

		public static readonly int glossyEnvironmentCubeMap = global::UnityEngine.Shader.PropertyToID("_GlossyEnvironmentCubeMap");

		public static readonly int glossyEnvironmentCubeMapHDR = global::UnityEngine.Shader.PropertyToID("_GlossyEnvironmentCubeMap_HDR");

		public static readonly int ambientSkyColor = global::UnityEngine.Shader.PropertyToID("unity_AmbientSky");

		public static readonly int ambientEquatorColor = global::UnityEngine.Shader.PropertyToID("unity_AmbientEquator");

		public static readonly int ambientGroundColor = global::UnityEngine.Shader.PropertyToID("unity_AmbientGround");

		public static readonly int time = global::UnityEngine.Shader.PropertyToID("_Time");

		public static readonly int sinTime = global::UnityEngine.Shader.PropertyToID("_SinTime");

		public static readonly int cosTime = global::UnityEngine.Shader.PropertyToID("_CosTime");

		public static readonly int deltaTime = global::UnityEngine.Shader.PropertyToID("unity_DeltaTime");

		public static readonly int timeParameters = global::UnityEngine.Shader.PropertyToID("_TimeParameters");

		public static readonly int lastTimeParameters = global::UnityEngine.Shader.PropertyToID("_LastTimeParameters");

		public static readonly int scaledScreenParams = global::UnityEngine.Shader.PropertyToID("_ScaledScreenParams");

		public static readonly int worldSpaceCameraPos = global::UnityEngine.Shader.PropertyToID("_WorldSpaceCameraPos");

		public static readonly int screenParams = global::UnityEngine.Shader.PropertyToID("_ScreenParams");

		public static readonly int alphaToMaskAvailable = global::UnityEngine.Shader.PropertyToID("_AlphaToMaskAvailable");

		public static readonly int projectionParams = global::UnityEngine.Shader.PropertyToID("_ProjectionParams");

		public static readonly int zBufferParams = global::UnityEngine.Shader.PropertyToID("_ZBufferParams");

		public static readonly int orthoParams = global::UnityEngine.Shader.PropertyToID("unity_OrthoParams");

		public static readonly int globalMipBias = global::UnityEngine.Shader.PropertyToID("_GlobalMipBias");

		public static readonly int screenSize = global::UnityEngine.Shader.PropertyToID("_ScreenSize");

		public static readonly int screenCoordScaleBias = global::UnityEngine.Shader.PropertyToID("_ScreenCoordScaleBias");

		public static readonly int screenSizeOverride = global::UnityEngine.Shader.PropertyToID("_ScreenSizeOverride");

		public static readonly int viewMatrix = global::UnityEngine.Shader.PropertyToID("unity_MatrixV");

		public static readonly int projectionMatrix = global::UnityEngine.Shader.PropertyToID("glstate_matrix_projection");

		public static readonly int viewAndProjectionMatrix = global::UnityEngine.Shader.PropertyToID("unity_MatrixVP");

		public static readonly int inverseViewMatrix = global::UnityEngine.Shader.PropertyToID("unity_MatrixInvV");

		public static readonly int inverseProjectionMatrix = global::UnityEngine.Shader.PropertyToID("unity_MatrixInvP");

		public static readonly int inverseViewAndProjectionMatrix = global::UnityEngine.Shader.PropertyToID("unity_MatrixInvVP");

		public static readonly int cameraProjectionMatrix = global::UnityEngine.Shader.PropertyToID("unity_CameraProjection");

		public static readonly int inverseCameraProjectionMatrix = global::UnityEngine.Shader.PropertyToID("unity_CameraInvProjection");

		public static readonly int worldToCameraMatrix = global::UnityEngine.Shader.PropertyToID("unity_WorldToCamera");

		public static readonly int cameraToWorldMatrix = global::UnityEngine.Shader.PropertyToID("unity_CameraToWorld");

		public static readonly int shadowBias = global::UnityEngine.Shader.PropertyToID("_ShadowBias");

		public static readonly int lightDirection = global::UnityEngine.Shader.PropertyToID("_LightDirection");

		public static readonly int lightPosition = global::UnityEngine.Shader.PropertyToID("_LightPosition");

		public static readonly int cameraWorldClipPlanes = global::UnityEngine.Shader.PropertyToID("unity_CameraWorldClipPlanes");

		public static readonly int billboardNormal = global::UnityEngine.Shader.PropertyToID("unity_BillboardNormal");

		public static readonly int billboardTangent = global::UnityEngine.Shader.PropertyToID("unity_BillboardTangent");

		public static readonly int billboardCameraParams = global::UnityEngine.Shader.PropertyToID("unity_BillboardCameraParams");

		public static readonly int previousViewProjectionNoJitter = global::UnityEngine.Shader.PropertyToID("_PrevViewProjMatrix");

		public static readonly int viewProjectionNoJitter = global::UnityEngine.Shader.PropertyToID("_NonJitteredViewProjMatrix");

		public static readonly int previousViewProjectionNoJitterStereo = global::UnityEngine.Shader.PropertyToID("_PrevViewProjMatrixStereo");

		public static readonly int viewProjectionNoJitterStereo = global::UnityEngine.Shader.PropertyToID("_NonJitteredViewProjMatrixStereo");

		public static readonly int blitTexture = global::UnityEngine.Shader.PropertyToID("_BlitTexture");

		public static readonly int blitScaleBias = global::UnityEngine.Shader.PropertyToID("_BlitScaleBias");

		public static readonly int sourceTex = global::UnityEngine.Shader.PropertyToID("_SourceTex");

		public static readonly int scaleBias = global::UnityEngine.Shader.PropertyToID("_ScaleBias");

		public static readonly int scaleBiasRt = global::UnityEngine.Shader.PropertyToID("_ScaleBiasRt");

		public static readonly int rtHandleScale = global::UnityEngine.Shader.PropertyToID("_RTHandleScale");

		public static readonly int rendererColor = global::UnityEngine.Shader.PropertyToID("_RendererColor");

		public static readonly int ditheringTexture = global::UnityEngine.Shader.PropertyToID("_DitheringTexture");

		public static readonly int ditheringTextureInvSize = global::UnityEngine.Shader.PropertyToID("_DitheringTextureInvSize");

		public static readonly int renderingLayerMaxInt = global::UnityEngine.Shader.PropertyToID("_RenderingLayerMaxInt");

		public static readonly int overlayUITexture = global::UnityEngine.Shader.PropertyToID("_OverlayUITexture");

		public static readonly int hdrOutputLuminanceParams = global::UnityEngine.Shader.PropertyToID("_HDROutputLuminanceParams");

		public static readonly int hdrOutputGradingParams = global::UnityEngine.Shader.PropertyToID("_HDROutputGradingParams");

		public static readonly int offscreenUIViewportParams = global::UnityEngine.Shader.PropertyToID("_OffscreenUIViewportParams");

		public static readonly int screenSpaceIrradiance = global::UnityEngine.Shader.PropertyToID("_ScreenSpaceIrradiance");
	}
}
