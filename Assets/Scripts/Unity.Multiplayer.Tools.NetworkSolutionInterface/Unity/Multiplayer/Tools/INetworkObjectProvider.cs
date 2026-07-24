namespace Unity.Multiplayer.Tools
{
	internal interface INetworkObjectProvider
	{
		global::UnityEngine.Object GetNetworkObject(ulong networkObjectId);
	}
}
