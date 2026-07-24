namespace Unity.Netcode
{
	internal class ProxyRpcTarget : global::Unity.Netcode.ProxyRpcTargetGroup, global::Unity.Netcode.IIndividualRpcTarget
	{
		internal ProxyRpcTarget(ulong clientId, global::Unity.Netcode.NetworkManager manager)
			: base(manager)
		{
			Add(clientId);
		}

		public void SetClientId(ulong clientId)
		{
			Clear();
			Add(clientId);
		}
	}
}
