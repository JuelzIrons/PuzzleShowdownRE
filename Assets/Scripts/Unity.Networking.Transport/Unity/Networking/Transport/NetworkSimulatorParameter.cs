namespace Unity.Networking.Transport
{
	[global::System.Serializable]
	public struct NetworkSimulatorParameter : global::Unity.Networking.Transport.INetworkParameter
	{
		public float ReceivePacketLossPercent;

		public float SendPacketLossPercent;

		public uint SendDelayMS;

		public uint SendJitterMS;

		public float SendDuplicatePercent;

		public float ReceiveMtu;

		internal uint RandomSeed;

		public bool Validate()
		{
			if (ReceivePacketLossPercent < 0f || ReceivePacketLossPercent > 100f)
			{
				global::UnityEngine.Debug.LogError(string.Format("{0} value ({1}) must be between 0 and 100.", "ReceivePacketLossPercent", ReceivePacketLossPercent));
				return false;
			}
			if (SendPacketLossPercent < 0f || SendPacketLossPercent > 100f)
			{
				global::UnityEngine.Debug.LogError(string.Format("{0} value ({1}) must be between 0 and 100.", "SendPacketLossPercent", SendPacketLossPercent));
				return false;
			}
			if (SendDuplicatePercent < 0f || SendDuplicatePercent > 100f)
			{
				global::UnityEngine.Debug.LogError(string.Format("{0} value ({1}) must be between 0 and 100.", "SendDuplicatePercent", SendDuplicatePercent));
				return false;
			}
			return true;
		}
	}
}
