namespace Unity.Networking.Transport
{
	public interface INetworkPipelineStage
	{
		int StaticSize { get; }

		unsafe global::Unity.Networking.Transport.NetworkPipelineStage StaticInitialize(byte* staticInstanceBuffer, int staticInstanceBufferLength, global::Unity.Networking.Transport.NetworkSettings settings);
	}
}
