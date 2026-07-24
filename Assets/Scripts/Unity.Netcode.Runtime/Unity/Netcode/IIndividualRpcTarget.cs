namespace Unity.Netcode
{
	internal interface IIndividualRpcTarget
	{
		global::Unity.Netcode.BaseRpcTarget Target { get; }

		void SetClientId(ulong clientId);
	}
}
