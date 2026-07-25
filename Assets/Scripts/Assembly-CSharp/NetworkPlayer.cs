public class NetworkPlayer : global::Unity.Netcode.NetworkBehaviour
{
	public override void OnNetworkSpawn()
	{
		base.OnNetworkSpawn();
	}

	public override void OnNetworkDespawn()
	{
		base.OnNetworkDespawn();
		RelayManager.Instance.LocalDisconnect();
	}
}
