namespace Unity.Netcode
{
	internal class DeferredMessageManager : global::Unity.Netcode.IDeferredNetworkMessageManager
	{
		protected struct TriggerData
		{
			public global::Unity.Netcode.FastBufferReader Reader;

			public global::Unity.Netcode.NetworkMessageHeader Header;

			public ulong SenderId;

			public float Timestamp;

			public int SerializedHeaderSize;
		}

		protected struct TriggerInfo
		{
			public string MessageType;

			public float Expiry;

			public global::Unity.Collections.NativeList<global::Unity.Netcode.DeferredMessageManager.TriggerData> TriggerData;
		}

		protected readonly global::System.Collections.Generic.Dictionary<global::Unity.Netcode.IDeferredNetworkMessageManager.TriggerType, global::System.Collections.Generic.Dictionary<ulong, global::Unity.Netcode.DeferredMessageManager.TriggerInfo>> m_Triggers = new global::System.Collections.Generic.Dictionary<global::Unity.Netcode.IDeferredNetworkMessageManager.TriggerType, global::System.Collections.Generic.Dictionary<ulong, global::Unity.Netcode.DeferredMessageManager.TriggerInfo>>();

		private readonly global::Unity.Netcode.NetworkManager m_NetworkManager;

		internal static bool IncludeMessageType = true;

		internal DeferredMessageManager(global::Unity.Netcode.NetworkManager networkManager)
		{
			m_NetworkManager = networkManager;
		}

		public unsafe virtual void DeferMessage(global::Unity.Netcode.IDeferredNetworkMessageManager.TriggerType trigger, ulong key, global::Unity.Netcode.FastBufferReader reader, ref global::Unity.Netcode.NetworkContext context, string messageType)
		{
			if (!m_Triggers.TryGetValue(trigger, out var value))
			{
				value = new global::System.Collections.Generic.Dictionary<ulong, global::Unity.Netcode.DeferredMessageManager.TriggerInfo>();
				m_Triggers[trigger] = value;
			}
			if (!value.TryGetValue(key, out var value2))
			{
				global::Unity.Netcode.DeferredMessageManager.TriggerInfo triggerInfo = new global::Unity.Netcode.DeferredMessageManager.TriggerInfo
				{
					MessageType = messageType,
					Expiry = m_NetworkManager.RealTimeProvider.RealTimeSinceStartup + m_NetworkManager.NetworkConfig.SpawnTimeout,
					TriggerData = new global::Unity.Collections.NativeList<global::Unity.Netcode.DeferredMessageManager.TriggerData>(global::Unity.Collections.Allocator.Persistent)
				};
				value2 = (value[key] = triggerInfo);
			}
			value2.TriggerData.Add(new global::Unity.Netcode.DeferredMessageManager.TriggerData
			{
				Reader = new global::Unity.Netcode.FastBufferReader(reader.GetUnsafePtr(), global::Unity.Collections.Allocator.Persistent, reader.Length),
				Header = context.Header,
				Timestamp = context.Timestamp,
				SenderId = context.SenderId,
				SerializedHeaderSize = context.SerializedHeaderSize
			});
		}

		public unsafe virtual void CleanupStaleTriggers()
		{
			foreach (global::System.Collections.Generic.KeyValuePair<global::Unity.Netcode.IDeferredNetworkMessageManager.TriggerType, global::System.Collections.Generic.Dictionary<ulong, global::Unity.Netcode.DeferredMessageManager.TriggerInfo>> trigger in m_Triggers)
			{
				ulong* ptr = stackalloc ulong[trigger.Value.Count];
				int num = 0;
				foreach (global::System.Collections.Generic.KeyValuePair<ulong, global::Unity.Netcode.DeferredMessageManager.TriggerInfo> item in trigger.Value)
				{
					if (item.Value.Expiry < m_NetworkManager.RealTimeProvider.RealTimeSinceStartup)
					{
						ptr[num++] = item.Key;
						PurgeTrigger(trigger.Key, item.Key, item.Value);
					}
				}
				for (int i = 0; i < num; i++)
				{
					trigger.Value.Remove(ptr[i]);
				}
			}
		}

		private string GetWarningMessage(global::Unity.Netcode.IDeferredNetworkMessageManager.TriggerType triggerType, ulong key, global::Unity.Netcode.DeferredMessageManager.TriggerInfo triggerInfo, float spawnTimeout)
		{
			if (IncludeMessageType)
			{
				return string.Format("[Deferred {0}] Messages were received for a trigger of type {1} associated with id ({2}), but the {3} was not received within the timeout period {4} second(s).", triggerType, triggerInfo.MessageType, key, "NetworkObject", spawnTimeout);
			}
			return string.Format("Deferred messages were received for a trigger of type {0} associated with id ({1}), but the {2} was not received within the timeout period {3} second(s).", triggerType, key, "NetworkObject", spawnTimeout);
		}

		protected virtual void PurgeTrigger(global::Unity.Netcode.IDeferredNetworkMessageManager.TriggerType triggerType, ulong key, global::Unity.Netcode.DeferredMessageManager.TriggerInfo triggerInfo)
		{
			global::Unity.Netcode.LogLevel logLevel = ((!m_NetworkManager.DistributedAuthorityMode) ? global::Unity.Netcode.LogLevel.Normal : global::Unity.Netcode.LogLevel.Developer);
			if (global::Unity.Netcode.NetworkLog.CurrentLogLevel <= logLevel)
			{
				global::Unity.Netcode.NetworkLog.LogWarning(GetWarningMessage(triggerType, key, triggerInfo, m_NetworkManager.NetworkConfig.SpawnTimeout));
			}
			foreach (global::Unity.Netcode.DeferredMessageManager.TriggerData triggerDatum in triggerInfo.TriggerData)
			{
				triggerDatum.Reader.Dispose();
			}
			triggerInfo.TriggerData.Dispose();
		}

		public virtual void ProcessTriggers(global::Unity.Netcode.IDeferredNetworkMessageManager.TriggerType trigger, ulong key)
		{
			if (!m_Triggers.TryGetValue(trigger, out var value) || !value.TryGetValue(key, out var value2))
			{
				return;
			}
			value.Remove(key);
			foreach (global::Unity.Netcode.DeferredMessageManager.TriggerData triggerDatum in value2.TriggerData)
			{
				global::Unity.Netcode.DeferredMessageManager.TriggerData current = triggerDatum;
				m_NetworkManager.ConnectionManager.MessageManager.HandleMessage(in current.Header, current.Reader, current.SenderId, current.Timestamp, current.SerializedHeaderSize);
			}
			value2.TriggerData.Dispose();
		}

		public virtual void CleanupAllTriggers()
		{
			foreach (global::System.Collections.Generic.KeyValuePair<global::Unity.Netcode.IDeferredNetworkMessageManager.TriggerType, global::System.Collections.Generic.Dictionary<ulong, global::Unity.Netcode.DeferredMessageManager.TriggerInfo>> trigger in m_Triggers)
			{
				foreach (global::System.Collections.Generic.KeyValuePair<ulong, global::Unity.Netcode.DeferredMessageManager.TriggerInfo> item in trigger.Value)
				{
					foreach (global::Unity.Netcode.DeferredMessageManager.TriggerData triggerDatum in item.Value.TriggerData)
					{
						triggerDatum.Reader.Dispose();
					}
					item.Value.TriggerData.Dispose();
				}
			}
			m_Triggers.Clear();
		}
	}
}
