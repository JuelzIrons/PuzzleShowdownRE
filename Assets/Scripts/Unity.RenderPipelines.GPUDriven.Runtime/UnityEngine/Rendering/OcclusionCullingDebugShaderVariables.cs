namespace UnityEngine.Rendering
{
	[global::UnityEngine.Rendering.GenerateHLSL(global::UnityEngine.Rendering.PackingRules.Exact, true, false, false, 1, false, false, false, -1, ".\\Library\\PackageCache\\com.unity.render-pipelines.core@04ab0eefa0c3\\Runtime\\GPUDriven\\OcclusionCullingDebugShaderVariables.cs", needAccessors = false, generateCBuffer = true)]
	internal struct OcclusionCullingDebugShaderVariables
	{
		public global::UnityEngine.Vector4 _DepthSizeInOccluderPixels;

		[global::UnityEngine.Rendering.HLSLArray(8, typeof(global::UnityEngine.Rendering.ShaderGenUInt4))]
		public unsafe fixed uint _OccluderMipBounds[32];

		public uint _OccluderMipLayoutSizeX;

		public uint _OccluderMipLayoutSizeY;

		public uint _OcclusionCullingDebugPad0;

		public uint _OcclusionCullingDebugPad1;
	}
}
