namespace Unity.Multiplayer.Tools.Adapters
{
	internal interface IGetConnectionStatus : global::Unity.Multiplayer.Tools.Adapters.IAdapterComponent
	{
		event global::System.Action ServerOrClientStarted;

		event global::System.Action ServerOrClientStopped;
	}
}
