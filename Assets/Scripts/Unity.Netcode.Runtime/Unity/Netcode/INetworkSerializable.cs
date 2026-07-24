namespace Unity.Netcode
{
	public interface INetworkSerializable
	{
		void NetworkSerialize<T>(global::Unity.Netcode.BufferSerializer<T> serializer) where T : global::Unity.Netcode.IReaderWriter;
	}
}
