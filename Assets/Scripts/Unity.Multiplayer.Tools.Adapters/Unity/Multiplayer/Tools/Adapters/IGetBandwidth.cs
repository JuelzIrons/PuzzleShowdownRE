namespace Unity.Multiplayer.Tools.Adapters
{
	internal interface IGetBandwidth : global::Unity.Multiplayer.Tools.Adapters.IAdapterComponent
	{
		global::Unity.Multiplayer.Tools.Common.BandwidthTypes SupportedBandwidthTypes { get; }

		bool IsCacheEmpty { get; }

		event global::System.Action OnBandwidthUpdated;

		float GetBandwidthBytes(global::Unity.Multiplayer.Tools.Adapters.ObjectId objectId, global::Unity.Multiplayer.Tools.Common.BandwidthTypes bandwidthTypes = global::Unity.Multiplayer.Tools.Common.BandwidthTypes.All, global::Unity.Multiplayer.Tools.Common.NetworkDirection networkDirection = global::Unity.Multiplayer.Tools.Common.NetworkDirection.SentAndReceived);
	}
}
