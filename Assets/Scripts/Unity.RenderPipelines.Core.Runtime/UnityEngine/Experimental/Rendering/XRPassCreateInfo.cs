namespace UnityEngine.Experimental.Rendering
{
	public struct XRPassCreateInfo
	{
		internal global::UnityEngine.Rendering.RenderTargetIdentifier renderTarget;

		internal global::UnityEngine.RenderTextureDescriptor renderTargetDesc;

		internal global::UnityEngine.Rendering.RenderTargetIdentifier motionVectorRenderTarget;

		internal global::UnityEngine.RenderTextureDescriptor motionVectorRenderTargetDesc;

		internal global::UnityEngine.Rendering.ScriptableCullingParameters cullingParameters;

		internal global::UnityEngine.Material occlusionMeshMaterial;

		internal float occlusionMeshScale;

		internal int renderTargetScaledWidth;

		internal int renderTargetScaledHeight;

		internal global::System.IntPtr foveatedRenderingInfo;

		internal int multipassId;

		internal int cullingPassId;

		internal bool copyDepth;

		internal bool hasMotionVectorPass;

		internal bool spaceWarpRightHandedNDC;

		internal bool isLastCameraPass;

		internal global::UnityEngine.XR.XRDisplaySubsystem.XRRenderPass xrSdkRenderPass;
	}
}
