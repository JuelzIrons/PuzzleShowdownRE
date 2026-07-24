namespace Unity.Netcode
{
	internal enum NetworkMessageTypes : uint
	{
		ConnectionApproved = 0u,
		ConnectionRequest = 1u,
		ChangeOwnership = 2u,
		ClientConnected = 3u,
		ClientDisconnected = 4u,
		ClientRpc = 5u,
		CreateObject = 6u,
		DestroyObject = 7u,
		DisconnectReason = 8u,
		ForwardClientRpc = 9u,
		ForwardServerRpc = 10u,
		NamedMessage = 11u,
		NetworkTransformMessage = 12u,
		NetworkVariableDelta = 13u,
		ParentSync = 14u,
		Proxy = 15u,
		Rpc = 16u,
		SceneEvent = 17u,
		ServerLog = 18u,
		ServerRpc = 19u,
		SessionOwner = 20u,
		TimeSync = 21u,
		Unnamed = 22u,
		AnticipationCounterSyncPingMessage = 23u,
		AnticipationCounterSyncPongMessage = 24u
	}
}
