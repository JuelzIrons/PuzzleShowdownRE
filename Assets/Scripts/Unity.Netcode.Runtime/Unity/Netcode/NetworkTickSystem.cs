namespace Unity.Netcode
{
	public class NetworkTickSystem
	{
		public const int NoTick = int.MinValue;

		public uint TickRate { get; internal set; }

		public global::Unity.Netcode.NetworkTime LocalTime { get; internal set; }

		public global::Unity.Netcode.NetworkTime ServerTime { get; internal set; }

		public event global::System.Action Tick;

		public NetworkTickSystem(uint tickRate, double localTimeSec, double serverTimeSec)
		{
			if (tickRate == 0)
			{
				throw new global::System.ArgumentException("Tick rate must be a positive value.", "tickRate");
			}
			TickRate = tickRate;
			this.Tick = null;
			LocalTime = new global::Unity.Netcode.NetworkTime(tickRate, localTimeSec);
			ServerTime = new global::Unity.Netcode.NetworkTime(tickRate, serverTimeSec);
		}

		public void Reset(double localTimeSec, double serverTimeSec)
		{
			LocalTime = new global::Unity.Netcode.NetworkTime(TickRate, localTimeSec);
			ServerTime = new global::Unity.Netcode.NetworkTime(TickRate, serverTimeSec);
		}

		public void UpdateTick(double localTimeSec, double serverTimeSec)
		{
			int tick = LocalTime.Tick;
			LocalTime = new global::Unity.Netcode.NetworkTime(TickRate, localTimeSec);
			ServerTime = new global::Unity.Netcode.NetworkTime(TickRate, serverTimeSec);
			global::Unity.Netcode.NetworkTime localTime = LocalTime;
			global::Unity.Netcode.NetworkTime serverTime = ServerTime;
			int tick2 = LocalTime.Tick;
			int num = tick2 - ServerTime.Tick;
			for (int i = tick + 1; i <= tick2; i++)
			{
				LocalTime = new global::Unity.Netcode.NetworkTime(TickRate, i);
				ServerTime = new global::Unity.Netcode.NetworkTime(TickRate, i - num);
				this.Tick?.Invoke();
			}
			LocalTime = localTime;
			ServerTime = serverTime;
		}
	}
}
