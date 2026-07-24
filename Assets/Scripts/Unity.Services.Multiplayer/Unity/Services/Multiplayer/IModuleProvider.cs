namespace Unity.Services.Multiplayer
{
	internal interface IModuleProvider
	{
		global::System.Type Type { get; }

		int Priority { get; }

		global::Unity.Services.Multiplayer.IModule Build(global::Unity.Services.Multiplayer.ISession session);
	}
}
