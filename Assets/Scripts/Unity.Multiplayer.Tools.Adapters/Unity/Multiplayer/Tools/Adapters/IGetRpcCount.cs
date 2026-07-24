namespace Unity.Multiplayer.Tools.Adapters
{
	internal interface IGetRpcCount : global::Unity.Multiplayer.Tools.Adapters.IAdapterComponent
	{
		event global::System.Action OnRpcCountUpdated;

		int GetRpcCount(global::Unity.Multiplayer.Tools.Adapters.ObjectId objectId);
	}
}
