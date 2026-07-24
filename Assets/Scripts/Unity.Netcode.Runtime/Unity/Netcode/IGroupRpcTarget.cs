namespace Unity.Netcode
{
	internal interface IGroupRpcTarget
	{
		global::Unity.Netcode.BaseRpcTarget Target { get; }

		void Add(ulong clientId);

		void Clear();
	}
}
