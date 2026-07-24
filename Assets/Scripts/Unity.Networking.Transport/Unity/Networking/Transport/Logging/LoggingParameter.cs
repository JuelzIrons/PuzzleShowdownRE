namespace Unity.Networking.Transport.Logging
{
	[global::System.Serializable]
	public struct LoggingParameter : global::Unity.Networking.Transport.INetworkParameter
	{
		public global::Unity.Collections.FixedString32Bytes DriverName;

		public bool Validate()
		{
			if (DriverName.IsEmpty)
			{
				global::UnityEngine.Debug.LogError("The driver name must not be empty.");
				return false;
			}
			return true;
		}
	}
}
