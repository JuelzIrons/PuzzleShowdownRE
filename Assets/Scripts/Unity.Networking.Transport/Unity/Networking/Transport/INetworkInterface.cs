namespace Unity.Networking.Transport
{
	public interface INetworkInterface : global::System.IDisposable
	{
		global::Unity.Networking.Transport.NetworkEndpoint LocalEndpoint { get; }

		int Initialize(ref global::Unity.Networking.Transport.NetworkSettings settings, ref int packetPadding);

		global::Unity.Jobs.JobHandle ScheduleReceive(ref global::Unity.Networking.Transport.ReceiveJobArguments arguments, global::Unity.Jobs.JobHandle dep);

		global::Unity.Jobs.JobHandle ScheduleSend(ref global::Unity.Networking.Transport.SendJobArguments arguments, global::Unity.Jobs.JobHandle dep);

		int Bind(global::Unity.Networking.Transport.NetworkEndpoint endpoint);

		int Listen();
	}
}
