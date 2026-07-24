namespace Unity.Netcode
{
	internal interface INetworkMessageProvider
	{
		global::System.Collections.Generic.List<global::Unity.Netcode.NetworkMessageManager.MessageWithHandler> GetMessages();
	}
}
