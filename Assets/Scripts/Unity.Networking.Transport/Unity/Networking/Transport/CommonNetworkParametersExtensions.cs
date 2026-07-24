namespace Unity.Networking.Transport
{
	public static class CommonNetworkParametersExtensions
	{
		public static ref global::Unity.Networking.Transport.NetworkSettings WithNetworkConfigParameters(this ref global::Unity.Networking.Transport.NetworkSettings settings, int connectTimeoutMS = 1000, int maxConnectAttempts = 60, int disconnectTimeoutMS = 30000, int heartbeatTimeoutMS = 500, int reconnectionTimeoutMS = 2000, int maxFrameTimeMS = 0, int fixedFrameTimeMS = 0, int receiveQueueCapacity = 512, int sendQueueCapacity = 512, int maxMessageSize = 1400, bool performPathMtuDiscovery = false)
		{
			global::Unity.Networking.Transport.NetworkConfigParameter parameter = new global::Unity.Networking.Transport.NetworkConfigParameter
			{
				connectTimeoutMS = connectTimeoutMS,
				maxConnectAttempts = maxConnectAttempts,
				disconnectTimeoutMS = disconnectTimeoutMS,
				heartbeatTimeoutMS = heartbeatTimeoutMS,
				reconnectionTimeoutMS = reconnectionTimeoutMS,
				maxFrameTimeMS = maxFrameTimeMS,
				fixedFrameTimeMS = fixedFrameTimeMS,
				receiveQueueCapacity = receiveQueueCapacity,
				sendQueueCapacity = sendQueueCapacity,
				maxMessageSize = maxMessageSize,
				performPathMtuDiscovery = performPathMtuDiscovery
			};
			settings.AddRawParameterStruct(ref parameter);
			return ref settings;
		}

		public static ref global::Unity.Networking.Transport.NetworkSettings WithNetworkConfigParameters(this ref global::Unity.Networking.Transport.NetworkSettings settings, int connectTimeoutMS, int maxConnectAttempts, int disconnectTimeoutMS, int heartbeatTimeoutMS, int reconnectionTimeoutMS, int maxFrameTimeMS, int fixedFrameTimeMS, int receiveQueueCapacity, int sendQueueCapacity, bool performPathMtuDiscovery)
		{
			global::Unity.Networking.Transport.NetworkConfigParameter parameter = new global::Unity.Networking.Transport.NetworkConfigParameter
			{
				connectTimeoutMS = connectTimeoutMS,
				maxConnectAttempts = maxConnectAttempts,
				disconnectTimeoutMS = disconnectTimeoutMS,
				heartbeatTimeoutMS = heartbeatTimeoutMS,
				reconnectionTimeoutMS = reconnectionTimeoutMS,
				maxFrameTimeMS = maxFrameTimeMS,
				fixedFrameTimeMS = fixedFrameTimeMS,
				receiveQueueCapacity = receiveQueueCapacity,
				sendQueueCapacity = sendQueueCapacity,
				maxMessageSize = 1400,
				performPathMtuDiscovery = performPathMtuDiscovery
			};
			settings.AddRawParameterStruct(ref parameter);
			return ref settings;
		}

		public static global::Unity.Networking.Transport.NetworkConfigParameter GetNetworkConfigParameters(this ref global::Unity.Networking.Transport.NetworkSettings settings)
		{
			if (!settings.TryGet<global::Unity.Networking.Transport.NetworkConfigParameter>(out var parameter))
			{
				parameter.connectTimeoutMS = 1000;
				parameter.maxConnectAttempts = 60;
				parameter.disconnectTimeoutMS = 30000;
				parameter.heartbeatTimeoutMS = 500;
				parameter.reconnectionTimeoutMS = 2000;
				parameter.receiveQueueCapacity = 512;
				parameter.sendQueueCapacity = 512;
				parameter.maxFrameTimeMS = 0;
				parameter.fixedFrameTimeMS = 0;
				parameter.maxMessageSize = 1400;
				parameter.performPathMtuDiscovery = false;
			}
			return parameter;
		}
	}
}
