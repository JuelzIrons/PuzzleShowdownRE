namespace UnityEngine.Rendering.Universal
{
	[global::System.Serializable]
	public abstract class ShadowShape2DProvider
	{
		public virtual string ProviderName(string componentName)
		{
			return componentName;
		}

		public virtual int Priority()
		{
			return 0;
		}

		public virtual void Enabled(global::UnityEngine.Component sourceComponent, global::UnityEngine.Rendering.Universal.ShadowShape2D persistantShadowShape)
		{
		}

		public virtual void Disabled(global::UnityEngine.Component sourceComponent, global::UnityEngine.Rendering.Universal.ShadowShape2D persistantShadowShape)
		{
		}

		public abstract bool IsShapeSource(global::UnityEngine.Component sourceComponent);

		public virtual void OnPersistantDataCreated(global::UnityEngine.Component sourceComponent, global::UnityEngine.Rendering.Universal.ShadowShape2D persistantShadowShape)
		{
		}

		public virtual void OnBeforeRender(global::UnityEngine.Component sourceComponent, global::UnityEngine.Bounds worldCullingBounds, global::UnityEngine.Rendering.Universal.ShadowShape2D persistantShadowShape)
		{
		}
	}
}
