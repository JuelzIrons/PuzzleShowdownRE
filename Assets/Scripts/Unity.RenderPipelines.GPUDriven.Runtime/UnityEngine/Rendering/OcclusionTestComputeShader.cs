namespace UnityEngine.Rendering
{
	internal struct OcclusionTestComputeShader
	{
		public global::UnityEngine.ComputeShader cs;

		public global::UnityEngine.Rendering.LocalKeyword occlusionDebugKeyword;

		public void Init(global::UnityEngine.ComputeShader cs)
		{
			this.cs = cs;
			occlusionDebugKeyword = new global::UnityEngine.Rendering.LocalKeyword(cs, "OCCLUSION_DEBUG");
		}
	}
}
