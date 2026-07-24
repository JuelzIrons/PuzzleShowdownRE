namespace Unity.Netcode.Transports.UTP
{
	public interface INetworkStreamDriverConstructor
	{
		void CreateDriver(global::Unity.Netcode.Transports.UTP.UnityTransport transport, out global::Unity.Networking.Transport.NetworkDriver driver, out global::Unity.Networking.Transport.NetworkPipeline unreliableFragmentedPipeline, out global::Unity.Networking.Transport.NetworkPipeline unreliableSequencedFragmentedPipeline, out global::Unity.Networking.Transport.NetworkPipeline reliableSequencedPipeline);
	}
}
