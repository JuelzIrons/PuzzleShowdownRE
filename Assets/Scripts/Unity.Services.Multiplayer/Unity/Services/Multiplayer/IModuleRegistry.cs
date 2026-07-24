namespace Unity.Services.Multiplayer
{
	internal interface IModuleRegistry
	{
		global::System.Collections.Generic.List<global::Unity.Services.Multiplayer.IModuleProvider> ModuleProviders { get; }

		void RegisterModuleProvider(global::Unity.Services.Multiplayer.IModuleProvider moduleProvider);
	}
}
