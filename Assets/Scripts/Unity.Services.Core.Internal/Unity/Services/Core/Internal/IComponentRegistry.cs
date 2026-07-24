namespace Unity.Services.Core.Internal
{
	internal interface IComponentRegistry
	{
		void RegisterServiceComponent<TComponent>([global::JetBrains.Annotations.NotNull] TComponent component) where TComponent : global::Unity.Services.Core.Internal.IServiceComponent;

		TComponent GetServiceComponent<TComponent>() where TComponent : global::Unity.Services.Core.Internal.IServiceComponent;

		bool TryGetServiceComponent<TComponent>(out TComponent component) where TComponent : global::Unity.Services.Core.Internal.IServiceComponent;

		void ResetProvidedComponents(global::System.Collections.Generic.IDictionary<int, global::Unity.Services.Core.Internal.IServiceComponent> componentTypeHashToInstance);
	}
}
