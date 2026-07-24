namespace Unity.Netcode
{
	internal interface INetworkMessage
	{
		int Version { get; }

		void Serialize(global::Unity.Netcode.FastBufferWriter writer, int targetVersion);

		bool Deserialize(global::Unity.Netcode.FastBufferReader reader, ref global::Unity.Netcode.NetworkContext context, int receivedMessageVersion);

		void Handle(ref global::Unity.Netcode.NetworkContext context);
	}
}
