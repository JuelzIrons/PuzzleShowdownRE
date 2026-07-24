namespace Unity.Netcode
{
	public static class NetworkLog
	{
		internal enum LogType : byte
		{
			Info = 0,
			Warning = 1,
			Error = 2,
			None = 3
		}

		internal static global::Unity.Netcode.NetworkManager NetworkManagerOverride;

		public static global::Unity.Netcode.LogLevel CurrentLogLevel
		{
			get
			{
				if (!(global::Unity.Netcode.NetworkManager.Singleton == null))
				{
					return global::Unity.Netcode.NetworkManager.Singleton.LogLevel;
				}
				return global::Unity.Netcode.LogLevel.Normal;
			}
		}

		public static void LogInfo(string message)
		{
			global::UnityEngine.Debug.Log("[Netcode] " + message);
		}

		public static void LogWarning(string message)
		{
			global::UnityEngine.Debug.LogWarning("[Netcode] " + message);
		}

		public static void LogError(string message)
		{
			global::UnityEngine.Debug.LogError("[Netcode] " + message);
		}

		public static void LogInfoServer(string message)
		{
			LogServer(message, global::Unity.Netcode.NetworkLog.LogType.Info);
		}

		public static void LogInfoSessionOwner(string message)
		{
			LogServer(message, global::Unity.Netcode.NetworkLog.LogType.Info);
		}

		public static void LogWarningServer(string message)
		{
			LogServer(message, global::Unity.Netcode.NetworkLog.LogType.Warning);
		}

		public static void LogErrorServer(string message)
		{
			LogServer(message, global::Unity.Netcode.NetworkLog.LogType.Error);
		}

		private static void LogServer(string message, global::Unity.Netcode.NetworkLog.LogType logType)
		{
			global::Unity.Netcode.NetworkManager networkManager = NetworkManagerOverride ?? (NetworkManagerOverride = global::Unity.Netcode.NetworkManager.Singleton);
			ulong num = networkManager?.LocalClientId ?? 0;
			bool flag = (((bool)networkManager && networkManager.DistributedAuthorityMode) ? networkManager.LocalClient.IsSessionOwner : (!networkManager || networkManager.DistributedAuthorityMode || networkManager.IsServer));
			switch (logType)
			{
			case global::Unity.Netcode.NetworkLog.LogType.Info:
				if (flag)
				{
					LogInfoServerLocal(message, num);
				}
				else
				{
					LogInfo(message);
				}
				break;
			case global::Unity.Netcode.NetworkLog.LogType.Warning:
				if (flag)
				{
					LogWarningServerLocal(message, num);
				}
				else
				{
					LogWarning(message);
				}
				break;
			case global::Unity.Netcode.NetworkLog.LogType.Error:
				if (flag)
				{
					LogErrorServerLocal(message, num);
				}
				else
				{
					LogError(message);
				}
				break;
			}
			if (!flag && networkManager.NetworkConfig.EnableNetworkLogs)
			{
				global::Unity.Netcode.ServerLogMessage message2 = new global::Unity.Netcode.ServerLogMessage
				{
					LogType = logType,
					Message = message,
					SenderId = num
				};
				int num2 = networkManager.ConnectionManager.SendMessage(ref message2, global::Unity.Netcode.MessageDeliveryType<global::Unity.Netcode.ServerLogMessage>.DefaultDelivery, 0uL);
				networkManager.NetworkMetrics.TrackServerLogSent(0uL, (uint)logType, num2);
			}
		}

		private static string Header()
		{
			if ((NetworkManagerOverride ?? (NetworkManagerOverride = global::Unity.Netcode.NetworkManager.Singleton)).DistributedAuthorityMode)
			{
				return "Session-Owner";
			}
			return "Netcode-Server";
		}

		internal static void LogInfoServerLocal(string message, ulong sender)
		{
			global::UnityEngine.Debug.Log($"[{Header()} Sender={sender}] {message}");
		}

		internal static void LogWarningServerLocal(string message, ulong sender)
		{
			global::UnityEngine.Debug.LogWarning($"[{Header()} Sender={sender}] {message}");
		}

		internal static void LogErrorServerLocal(string message, ulong sender)
		{
			global::UnityEngine.Debug.LogError($"[{Header()} Sender={sender}] {message}");
		}
	}
}
