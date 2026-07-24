namespace Unity.Multiplayer.Tools.Adapters
{
	internal interface IGetOwnership : global::Unity.Multiplayer.Tools.Adapters.IAdapterComponent
	{
		global::Unity.Multiplayer.Tools.Adapters.ClientId GetOwner(global::Unity.Multiplayer.Tools.Adapters.ObjectId objectId);
	}
}
