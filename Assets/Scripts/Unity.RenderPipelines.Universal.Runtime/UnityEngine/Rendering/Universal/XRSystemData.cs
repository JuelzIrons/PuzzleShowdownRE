namespace UnityEngine.Rendering.Universal
{
	[global::System.Serializable]
	[global::System.Obsolete("Moved to UniversalRenderPipelineRuntimeXRResources on GraphicsSettings. #from(2023.3)")]
	public class XRSystemData : global::UnityEngine.ScriptableObject
	{
		[global::System.Serializable]
		[global::UnityEngine.Rendering.ReloadGroup]
		[global::System.Obsolete("Moved to UniversalRenderPipelineRuntimeXRResources on GraphicsSettings. #from(2023.3)")]
		public sealed class ShaderResources
		{
			[global::UnityEngine.Rendering.Reload("Shaders/XR/XROcclusionMesh.shader", global::UnityEngine.Rendering.ReloadAttribute.Package.Root)]
			public global::UnityEngine.Shader xrOcclusionMeshPS;

			[global::UnityEngine.Rendering.Reload("Shaders/XR/XRMirrorView.shader", global::UnityEngine.Rendering.ReloadAttribute.Package.Root)]
			public global::UnityEngine.Shader xrMirrorViewPS;
		}

		[global::System.Obsolete("Moved to UniversalRenderPipelineRuntimeXRResources on GraphicsSettings. #from(2023.3)")]
		public global::UnityEngine.Rendering.Universal.XRSystemData.ShaderResources shaders;
	}
}
