namespace UnityEngine.Shaders
{
	[global::System.Flags]
	public enum ShaderStageFlags
	{
		None = 0,
		Vertex = 1,
		Fragment = 2,
		Hull = 4,
		Domain = 8,
		Geometry = 0x10,
		Compute = 0x20,
		RayTracing = 0x40,
		Basic = 3,
		Tessellation = 0xC,
		Graphics = 0x1F,
		Any = 0x7F
	}
}
