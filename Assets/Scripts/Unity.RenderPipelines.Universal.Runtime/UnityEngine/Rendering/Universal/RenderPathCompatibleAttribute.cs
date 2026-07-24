namespace UnityEngine.Rendering.Universal
{
	[global::System.AttributeUsage(global::System.AttributeTargets.Field)]
	internal sealed class RenderPathCompatibleAttribute : global::System.Attribute
	{
		public global::UnityEngine.Rendering.Universal.RenderPathCompatibility renderPath;

		public RenderPathCompatibleAttribute(global::UnityEngine.Rendering.Universal.RenderPathCompatibility renderPath)
		{
			this.renderPath = renderPath;
		}
	}
}
