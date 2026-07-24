namespace Unity.Networking.Transport
{
	public static class BaselibNetworkParameterExtensions
	{
		[global::System.Obsolete("To set receiveQueueCapacity and sendQueueCapacity parameters use WithNetworkConfigParameters()", false)]
		public static ref global::Unity.Networking.Transport.NetworkSettings WithBaselibNetworkInterfaceParameters(this ref global::Unity.Networking.Transport.NetworkSettings settings, int receiveQueueCapacity = 0, int sendQueueCapacity = 0, uint maximumPayloadSize = 0u)
		{
			global::Unity.Networking.Transport.BaselibNetworkParameter parameter = new global::Unity.Networking.Transport.BaselibNetworkParameter
			{
				receiveQueueCapacity = receiveQueueCapacity,
				sendQueueCapacity = sendQueueCapacity,
				maximumPayloadSize = maximumPayloadSize
			};
			settings.AddRawParameterStruct(ref parameter);
			return ref settings;
		}
	}
}
