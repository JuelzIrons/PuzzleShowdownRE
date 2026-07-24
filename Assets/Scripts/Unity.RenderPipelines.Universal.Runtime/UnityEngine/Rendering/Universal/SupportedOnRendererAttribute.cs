namespace UnityEngine.Rendering.Universal
{
	[global::System.AttributeUsage(global::System.AttributeTargets.Class)]
	public class SupportedOnRendererAttribute : global::System.Attribute
	{
		public global::System.Type[] rendererTypes { get; }

		public SupportedOnRendererAttribute(global::System.Type renderer)
			: this(new global::System.Type[1] { renderer })
		{
		}

		public SupportedOnRendererAttribute(params global::System.Type[] renderers)
		{
			if (renderers == null)
			{
				global::UnityEngine.Debug.LogError("The SupportedOnRendererAttribute parameters cannot be null.");
				return;
			}
			foreach (global::System.Type type in renderers)
			{
				if (type == null || !typeof(global::UnityEngine.Rendering.Universal.ScriptableRendererData).IsAssignableFrom(type))
				{
					global::UnityEngine.Debug.LogError("The SupportedOnRendererAttribute Attribute targets an invalid ScriptableRendererData. One of the types cannot be assigned from ScriptableRendererData");
					return;
				}
			}
			rendererTypes = renderers;
		}
	}
}
