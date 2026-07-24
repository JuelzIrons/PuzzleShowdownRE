namespace Unity.Networking.Transport
{
	[global::System.Serializable]
	public struct NetworkConfigParameter : global::Unity.Networking.Transport.INetworkParameter
	{
		public int connectTimeoutMS;

		public int maxConnectAttempts;

		public int disconnectTimeoutMS;

		public int heartbeatTimeoutMS;

		public int reconnectionTimeoutMS;

		public int maxFrameTimeMS;

		public int fixedFrameTimeMS;

		public int receiveQueueCapacity;

		public int sendQueueCapacity;

		public int maxMessageSize;

		public bool performPathMtuDiscovery;

		public bool Validate()
		{
			bool flag = true;
			if (connectTimeoutMS < 0)
			{
				flag = false;
				global::UnityEngine.Debug.LogError($"connectTimeoutMS value ({connectTimeoutMS}) must be greater than or equal to 0");
			}
			if (maxConnectAttempts < 0)
			{
				flag = false;
				global::UnityEngine.Debug.LogError($"maxConnectAttempts value ({maxConnectAttempts}) must be greater than or equal to 0");
			}
			if (disconnectTimeoutMS < 0)
			{
				flag = false;
				global::UnityEngine.Debug.LogError($"disconnectTimeoutMS value ({disconnectTimeoutMS}) must be greater than or equal to 0");
			}
			if (heartbeatTimeoutMS < 0)
			{
				flag = false;
				global::UnityEngine.Debug.LogError($"heartbeatTimeoutMS value ({heartbeatTimeoutMS}) must be greater than or equal to 0");
			}
			if (reconnectionTimeoutMS < 0)
			{
				flag = false;
				global::UnityEngine.Debug.LogError($"reconnectionTimeoutMS value ({reconnectionTimeoutMS}) must be greater than or equal to 0");
			}
			if (maxFrameTimeMS < 0)
			{
				flag = false;
				global::UnityEngine.Debug.LogError($"maxFrameTimeMS value ({maxFrameTimeMS}) must be greater than or equal to 0");
			}
			if (fixedFrameTimeMS < 0)
			{
				flag = false;
				global::UnityEngine.Debug.LogError($"fixedFrameTimeMS value ({fixedFrameTimeMS}) must be greater than or equal to 0");
			}
			if (receiveQueueCapacity <= 0)
			{
				flag = false;
				global::UnityEngine.Debug.LogError($"receiveQueueCapacity value ({receiveQueueCapacity}) must be greater than 0");
			}
			if (sendQueueCapacity <= 0)
			{
				flag = false;
				global::UnityEngine.Debug.LogError($"sendQueueCapacity value ({sendQueueCapacity}) must be greater than 0");
			}
			if (maxMessageSize <= 0 || maxMessageSize > 1472)
			{
				flag = false;
				global::UnityEngine.Debug.LogError($"maxMessageSize value ({maxMessageSize}) must be greater than 0 and less than or equal to {1472}");
			}
			if (flag && maxMessageSize < 548)
			{
				global::UnityEngine.Debug.LogWarning($"maxMessageSize value ({maxMessageSize}) is unnecessarily low. 548 should be safe in all circumstances.");
			}
			return flag;
		}
	}
}
