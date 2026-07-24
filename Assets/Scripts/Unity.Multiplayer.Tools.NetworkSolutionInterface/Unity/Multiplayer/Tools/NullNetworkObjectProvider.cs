namespace Unity.Multiplayer.Tools
{
	internal class NullNetworkObjectProvider : global::Unity.Multiplayer.Tools.INetworkObjectProvider
	{
		global::UnityEngine.Object global::Unity.Multiplayer.Tools.INetworkObjectProvider.GetNetworkObject(ulong networkObjectId)
		{
			return null;
		}
	}
}
