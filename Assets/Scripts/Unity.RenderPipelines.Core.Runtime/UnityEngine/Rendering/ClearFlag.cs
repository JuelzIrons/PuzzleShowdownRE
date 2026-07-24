namespace UnityEngine.Rendering
{
	[global::System.Flags]
	public enum ClearFlag
	{
		None = 0,
		Color = 1,
		Depth = 2,
		Stencil = 4,
		DepthStencil = 6,
		ColorStencil = 5,
		All = 7
	}
}
