namespace Unity.Services.Multiplayer
{
	internal class ModuleRegistry : global::Unity.Services.Multiplayer.IModuleRegistry
	{
		internal readonly global::System.Collections.Generic.Dictionary<global::System.Type, global::Unity.Services.Multiplayer.IModuleProvider> ModuleProviders = new global::System.Collections.Generic.Dictionary<global::System.Type, global::Unity.Services.Multiplayer.IModuleProvider>();

		global::System.Collections.Generic.List<global::Unity.Services.Multiplayer.IModuleProvider> global::Unity.Services.Multiplayer.IModuleRegistry.ModuleProviders
		{
			get
			{
				global::System.Collections.Generic.List<global::Unity.Services.Multiplayer.IModuleProvider> list = global::System.Linq.Enumerable.ToList(ModuleProviders.Values);
				list.Sort((global::Unity.Services.Multiplayer.IModuleProvider x, global::Unity.Services.Multiplayer.IModuleProvider y) => x.Priority.CompareTo(y.Priority));
				return list;
			}
		}

		internal ModuleRegistry()
		{
		}

		public void RegisterModuleProvider(global::Unity.Services.Multiplayer.IModuleProvider moduleProvider)
		{
			if (!ModuleProviders.TryAdd(moduleProvider.Type, moduleProvider))
			{
				global::Unity.Services.Multiplayer.Logger.LogWarning("Failed to register module provider for module type:'" + moduleProvider.Type.Name + "'");
			}
		}
	}
}
