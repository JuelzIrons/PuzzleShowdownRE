namespace Unity.Services.Core.Internal
{
	internal class ComponentRegistry : global::Unity.Services.Core.Internal.IComponentRegistry
	{
		[global::JetBrains.Annotations.NotNull]
		internal global::System.Collections.Generic.Dictionary<int, global::Unity.Services.Core.Internal.IServiceComponent> ComponentTypeHashToInstance { get; }

		public ComponentRegistry()
		{
			ComponentTypeHashToInstance = new global::System.Collections.Generic.Dictionary<int, global::Unity.Services.Core.Internal.IServiceComponent>();
		}

		public ComponentRegistry([global::JetBrains.Annotations.NotNull] global::System.Collections.Generic.Dictionary<int, global::Unity.Services.Core.Internal.IServiceComponent> componentTypeHashToInstance)
		{
			ComponentTypeHashToInstance = componentTypeHashToInstance;
		}

		public void RegisterServiceComponent<TComponent>(TComponent component) where TComponent : global::Unity.Services.Core.Internal.IServiceComponent
		{
			global::System.Type typeFromHandle = typeof(TComponent);
			if (component.GetType() == typeFromHandle)
			{
				throw new global::System.ArgumentException("Interface type of component not specified.");
			}
			int hashCode = typeFromHandle.GetHashCode();
			if (IsComponentTypeRegistered(hashCode))
			{
				throw new global::System.InvalidOperationException("A component with the type " + typeFromHandle.FullName + " has already been registered.");
			}
			ComponentTypeHashToInstance[hashCode] = component;
		}

		public TComponent GetServiceComponent<TComponent>() where TComponent : global::Unity.Services.Core.Internal.IServiceComponent
		{
			global::System.Type typeFromHandle = typeof(TComponent);
			if (!ComponentTypeHashToInstance.TryGetValue(typeFromHandle.GetHashCode(), out var value) || value is global::Unity.Services.Core.Internal.MissingComponent)
			{
				throw new global::System.Collections.Generic.KeyNotFoundException("There is no component `" + typeFromHandle.Name + "` registered. Are you missing a package?");
			}
			return (TComponent)value;
		}

		public bool TryGetServiceComponent<TComponent>(out TComponent component) where TComponent : global::Unity.Services.Core.Internal.IServiceComponent
		{
			global::System.Type typeFromHandle = typeof(TComponent);
			global::Unity.Services.Core.Internal.IServiceComponent value;
			bool flag = ComponentTypeHashToInstance.TryGetValue(typeFromHandle.GetHashCode(), out value) && !(value is global::Unity.Services.Core.Internal.MissingComponent);
			component = (flag ? ((TComponent)value) : default(TComponent));
			return flag;
		}

		private bool IsComponentTypeRegistered(int componentTypeHash)
		{
			if (ComponentTypeHashToInstance.TryGetValue(componentTypeHash, out var value) && value != null)
			{
				return !(value is global::Unity.Services.Core.Internal.MissingComponent);
			}
			return false;
		}

		public void ResetProvidedComponents(global::System.Collections.Generic.IDictionary<int, global::Unity.Services.Core.Internal.IServiceComponent> componentTypeHashToInstance)
		{
			ComponentTypeHashToInstance.Clear();
			ComponentTypeHashToInstance.MergeAllowOverride(componentTypeHashToInstance);
		}
	}
}
