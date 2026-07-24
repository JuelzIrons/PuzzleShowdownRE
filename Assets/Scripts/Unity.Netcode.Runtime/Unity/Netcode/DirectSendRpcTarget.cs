namespace Unity.Netcode
{
	internal class DirectSendRpcTarget : global::Unity.Netcode.BaseRpcTarget, global::Unity.Netcode.IIndividualRpcTarget
	{
		internal ulong ClientId;

		public global::Unity.Netcode.BaseRpcTarget Target => this;

		public override void Dispose()
		{
			CheckLockBeforeDispose();
		}

		internal override void Send(global::Unity.Netcode.NetworkBehaviour behaviour, ref global::Unity.Netcode.RpcMessage message, global::Unity.Netcode.NetworkDelivery delivery, global::Unity.Netcode.RpcParams rpcParams)
		{
			SendMessageToClient(behaviour, ClientId, ref message, delivery);
		}

		public void SetClientId(ulong clientId)
		{
			ClientId = clientId;
		}

		internal DirectSendRpcTarget(global::Unity.Netcode.NetworkManager manager)
			: base(manager)
		{
		}

		internal DirectSendRpcTarget(ulong clientId, global::Unity.Netcode.NetworkManager manager)
			: base(manager)
		{
			ClientId = clientId;
		}
	}
}
