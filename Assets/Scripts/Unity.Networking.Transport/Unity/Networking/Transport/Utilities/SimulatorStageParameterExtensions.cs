namespace Unity.Networking.Transport.Utilities
{
	public static class SimulatorStageParameterExtensions
	{
		public static ref global::Unity.Networking.Transport.NetworkSettings WithSimulatorStageParameters(this ref global::Unity.Networking.Transport.NetworkSettings settings, int maxPacketCount, int maxPacketSize = 1472, global::Unity.Networking.Transport.Utilities.ApplyMode mode = global::Unity.Networking.Transport.Utilities.ApplyMode.AllPackets, int packetDelayMs = 0, int packetJitterMs = 0, int packetDropInterval = 0, int packetDropPercentage = 0, int packetDuplicationPercentage = 0, int fuzzFactor = 0, int fuzzOffset = 0, uint randomSeed = 0u)
		{
			global::Unity.Networking.Transport.Utilities.SimulatorUtility.Parameters parameter = new global::Unity.Networking.Transport.Utilities.SimulatorUtility.Parameters
			{
				MaxPacketCount = maxPacketCount,
				MaxPacketSize = maxPacketSize,
				Mode = mode,
				PacketDelayMs = packetDelayMs,
				PacketJitterMs = packetJitterMs,
				PacketDropInterval = packetDropInterval,
				PacketDropPercentage = packetDropPercentage,
				PacketDuplicationPercentage = packetDuplicationPercentage,
				FuzzFactor = fuzzFactor,
				FuzzOffset = fuzzOffset,
				RandomSeed = randomSeed
			};
			settings.AddRawParameterStruct(ref parameter);
			return ref settings;
		}

		public static global::Unity.Networking.Transport.Utilities.SimulatorUtility.Parameters GetSimulatorStageParameters(this ref global::Unity.Networking.Transport.NetworkSettings settings)
		{
			settings.TryGet<global::Unity.Networking.Transport.Utilities.SimulatorUtility.Parameters>(out var parameter);
			return parameter;
		}

		public unsafe static void ModifySimulatorStageParameters(this global::Unity.Networking.Transport.NetworkDriver driver, global::Unity.Networking.Transport.Utilities.SimulatorUtility.Parameters newParams)
		{
			global::Unity.Networking.Transport.NetworkPipelineStageId stageId = global::Unity.Networking.Transport.NetworkPipelineStageId.Get<global::Unity.Networking.Transport.SimulatorPipelineStage>();
			global::Unity.Networking.Transport.Utilities.SimulatorUtility.Parameters* writeablePipelineParameter = driver.GetWriteablePipelineParameter<global::Unity.Networking.Transport.Utilities.SimulatorUtility.Parameters>(default(global::Unity.Networking.Transport.NetworkPipeline), stageId);
			if (writeablePipelineParameter->MaxPacketCount != newParams.MaxPacketCount)
			{
				global::UnityEngine.Debug.LogError("Simulator stage maximum packet count can't be modified.");
				return;
			}
			if (writeablePipelineParameter->MaxPacketSize != newParams.MaxPacketSize)
			{
				global::UnityEngine.Debug.LogError("Simulator stage maximum packet size can't be modified.");
				return;
			}
			*writeablePipelineParameter = newParams;
			driver.m_NetworkSettings.AddRawParameterStruct(ref newParams);
		}
	}
}
