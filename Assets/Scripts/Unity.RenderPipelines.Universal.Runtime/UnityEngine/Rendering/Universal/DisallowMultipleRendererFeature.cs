namespace UnityEngine.Rendering.Universal
{
	[global::System.AttributeUsage(global::System.AttributeTargets.Class)]
	public class DisallowMultipleRendererFeature : global::System.Attribute
	{
		public string customTitle { get; private set; }

		public DisallowMultipleRendererFeature(string customTitle = null)
		{
			this.customTitle = customTitle;
		}
	}
}
