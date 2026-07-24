namespace Unity.Multiplayer.Tools.Adapters
{
	internal interface IGetClientId : global::Unity.Multiplayer.Tools.Adapters.IAdapterComponent
	{
		global::Unity.Multiplayer.Tools.Adapters.ClientId LocalClientId { get; }

		global::Unity.Multiplayer.Tools.Adapters.ClientId ServerClientId { get; }
	}
}
