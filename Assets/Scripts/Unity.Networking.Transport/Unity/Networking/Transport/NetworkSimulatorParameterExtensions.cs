namespace Unity.Networking.Transport
{
	public static class NetworkSimulatorParameterExtensions
	{
		public static ref global::Unity.Networking.Transport.NetworkSettings WithNetworkSimulatorParameters(this ref global::Unity.Networking.Transport.NetworkSettings settings, float receivePacketLossPercent = 0f, float sendPacketLossPercent = 0f, uint sendDelayMS = 0u, uint sendJitterMS = 0u, float sendDuplicatePercent = 0f, int receiveMtu = 0)
		{
			global::Unity.Networking.Transport.NetworkSimulatorParameter parameter = new global::Unity.Networking.Transport.NetworkSimulatorParameter
			{
				ReceivePacketLossPercent = receivePacketLossPercent,
				SendPacketLossPercent = sendPacketLossPercent,
				SendDelayMS = sendDelayMS,
				SendJitterMS = sendJitterMS,
				SendDuplicatePercent = sendDuplicatePercent,
				ReceiveMtu = receiveMtu
			};
			settings.AddRawParameterStruct(ref parameter);
			return ref settings;
		}

		public static void ModifyNetworkSimulatorParameters(this global::Unity.Networking.Transport.NetworkDriver driver, global::Unity.Networking.Transport.NetworkSimulatorParameter newParams)
		{
			if (!driver.m_NetworkStack.TryGetLayer<global::Unity.Networking.Transport.SimulatorLayer>(out var layer))
			{
				global::UnityEngine.Debug.LogError("Network simulator not available. Driver must have been configured with NetworkSettings.WithNetworkSimulatorParameters for network simulator to be available.");
				return;
			}
			if (!newParams.Validate())
			{
				global::UnityEngine.Debug.LogError("Modified network simulator parameters are invalid and were not applied.");
				return;
			}
			layer.Parameters = newParams;
			driver.m_NetworkSettings.AddRawParameterStruct(ref newParams);
		}
	}
}
