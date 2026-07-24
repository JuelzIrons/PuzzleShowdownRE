namespace UnityEngine.Rendering.Universal
{
	public enum DepthFormat
	{
		[global::UnityEngine.Rendering.Universal.RenderPathCompatible(global::UnityEngine.Rendering.Universal.RenderPathCompatibility.All)]
		Default = 0,
		[global::UnityEngine.Rendering.Universal.RenderPathCompatible(global::UnityEngine.Rendering.Universal.RenderPathCompatibility.Forward | global::UnityEngine.Rendering.Universal.RenderPathCompatibility.ForwardPlus)]
		Depth_16 = 90,
		[global::UnityEngine.Rendering.Universal.RenderPathCompatible(global::UnityEngine.Rendering.Universal.RenderPathCompatibility.Forward | global::UnityEngine.Rendering.Universal.RenderPathCompatibility.ForwardPlus)]
		Depth_24 = 91,
		[global::UnityEngine.Rendering.Universal.RenderPathCompatible(global::UnityEngine.Rendering.Universal.RenderPathCompatibility.Forward | global::UnityEngine.Rendering.Universal.RenderPathCompatibility.ForwardPlus)]
		Depth_32 = 93,
		[global::UnityEngine.Rendering.Universal.RenderPathCompatible(global::UnityEngine.Rendering.Universal.RenderPathCompatibility.All)]
		Depth_16_Stencil_8 = 151,
		[global::UnityEngine.Rendering.Universal.RenderPathCompatible(global::UnityEngine.Rendering.Universal.RenderPathCompatibility.All)]
		Depth_24_Stencil_8 = 92,
		[global::UnityEngine.Rendering.Universal.RenderPathCompatible(global::UnityEngine.Rendering.Universal.RenderPathCompatibility.All)]
		Depth_32_Stencil_8 = 94
	}
}
