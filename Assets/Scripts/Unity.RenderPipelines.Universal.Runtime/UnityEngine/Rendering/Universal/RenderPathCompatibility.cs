namespace UnityEngine.Rendering.Universal
{
	[global::System.Flags]
	public enum RenderPathCompatibility
	{
		Forward = 1,
		Deferred = 2,
		ForwardPlus = 4,
		DeferredPlus = 8,
		All = 0xF
	}
}
