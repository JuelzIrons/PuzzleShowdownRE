namespace Unity.Multiplayer.Tools.Adapters
{
	internal class NetworkParameters
	{
		public int PacketDelayMilliseconds { get; set; }

		public int PacketDelayRangeMilliseconds { get; set; }

		public int PacketLossIntervalMilliseconds { get; set; }

		public int PacketLossPercent { get; set; }
	}
}
