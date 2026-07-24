namespace Unity.Multiplayer.Tools.NetStats
{
	internal interface IMetricDispatcher
	{
		void RegisterObserver(global::Unity.Multiplayer.Tools.NetStats.IMetricObserver observer);

		void SetConnectionId(ulong connectionId);

		void Dispatch();
	}
}
