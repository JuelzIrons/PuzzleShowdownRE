namespace Unity.Networking.Transport
{
	[global::System.Serializable]
	public struct WebSocketParameter : global::Unity.Networking.Transport.INetworkParameter
	{
		public global::Unity.Collections.FixedString128Bytes Path;

		public bool Validate()
		{
			bool result = true;
			if (Path.Length == 0 || Path[0] != 47)
			{
				result = false;
				global::UnityEngine.Debug.LogError($"WebSocket path \"{Path}\" is invalid");
			}
			return result;
		}
	}
}
