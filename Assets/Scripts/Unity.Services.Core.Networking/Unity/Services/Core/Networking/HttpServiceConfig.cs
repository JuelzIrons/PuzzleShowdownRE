namespace Unity.Services.Core.Networking
{
	[global::System.Serializable]
	internal struct HttpServiceConfig
	{
		public string ServiceId;

		public string BaseUrl;

		public global::Unity.Services.Core.Networking.Internal.HttpOptions DefaultOptions;
	}
}
