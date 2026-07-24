namespace Unity.Netcode.Components
{
	internal interface INetworkTransformLogStateEntry
	{
		void AddLogEntry(global::Unity.Netcode.Components.NetworkTransform.NetworkTransformState networkTransformState, ulong targetClient, bool preUpdate = false);
	}
}
