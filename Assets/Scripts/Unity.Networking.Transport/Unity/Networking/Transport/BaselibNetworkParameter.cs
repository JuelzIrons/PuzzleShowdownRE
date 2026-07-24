namespace Unity.Networking.Transport
{
	[global::System.Obsolete("To set receiveQueueCapacity and sendQueueCapacity parameters use NetworkConfigParameter", false)]
	public struct BaselibNetworkParameter : global::Unity.Networking.Transport.INetworkParameter
	{
		public int receiveQueueCapacity;

		public int sendQueueCapacity;

		public uint maximumPayloadSize;

		public bool Validate()
		{
			bool result = true;
			if (receiveQueueCapacity <= 0)
			{
				result = false;
				global::UnityEngine.Debug.LogError($"ReceiveQueueCapacity value ({receiveQueueCapacity}) must be greater than 0");
			}
			if (sendQueueCapacity <= 0)
			{
				result = false;
				global::UnityEngine.Debug.LogError($"SendQueueCapacity value ({sendQueueCapacity}) must be greater than 0");
			}
			return result;
		}
	}
}
