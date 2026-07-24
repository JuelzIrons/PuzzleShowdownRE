namespace Unity.Multiplayer.Tools.NetStats
{
	internal interface INetworkSerializable
	{
		void NetworkSerialize<T>(global::Unity.Multiplayer.Tools.NetStats.BufferSerializer<T> serializer) where T : global::Unity.Multiplayer.Tools.NetStats.IReaderWriter;
	}
}
