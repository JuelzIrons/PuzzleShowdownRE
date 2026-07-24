namespace Unity.Netcode
{
	internal interface IDeferredNetworkMessageManager
	{
		internal enum TriggerType
		{
			OnSpawn = 0,
			OnAddPrefab = 1,
			OnNextFrame = 2
		}

		void DeferMessage(global::Unity.Netcode.IDeferredNetworkMessageManager.TriggerType trigger, ulong key, global::Unity.Netcode.FastBufferReader reader, ref global::Unity.Netcode.NetworkContext context, string messageType = null);

		void CleanupStaleTriggers();

		void ProcessTriggers(global::Unity.Netcode.IDeferredNetworkMessageManager.TriggerType trigger, ulong key);

		void CleanupAllTriggers();
	}
}
