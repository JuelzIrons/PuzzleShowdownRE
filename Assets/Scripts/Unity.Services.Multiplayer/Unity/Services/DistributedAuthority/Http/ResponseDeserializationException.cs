namespace Unity.Services.DistributedAuthority.Http
{
	[global::System.Serializable]
	internal class ResponseDeserializationException : global::System.Exception
	{
		public global::Unity.Services.DistributedAuthority.Http.HttpClientResponse response;

		public ResponseDeserializationException()
		{
		}

		public ResponseDeserializationException(string message)
			: base(message)
		{
		}

		private ResponseDeserializationException(global::System.Exception inner, string message)
			: base(message, inner)
		{
		}

		public ResponseDeserializationException(global::Unity.Services.DistributedAuthority.Http.HttpClientResponse httpClientResponse)
			: base("Unable to Deserialize Http Client Response")
		{
			response = httpClientResponse;
		}

		public ResponseDeserializationException(global::Unity.Services.DistributedAuthority.Http.HttpClientResponse httpClientResponse, string message)
			: base(message)
		{
			response = httpClientResponse;
		}

		public ResponseDeserializationException(global::Unity.Services.DistributedAuthority.Http.HttpClientResponse httpClientResponse, global::System.Exception inner, string message)
			: base(message, inner)
		{
			response = httpClientResponse;
		}
	}
}
