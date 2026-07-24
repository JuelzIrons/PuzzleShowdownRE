namespace Unity.Services.Core.Networking.Internal
{
	internal struct ReadOnlyHttpRequest
	{
		private global::Unity.Services.Core.Networking.Internal.HttpRequest m_Request;

		public string Method => m_Request.Method;

		public string Url => m_Request.Url;

		public global::System.Collections.Generic.IReadOnlyDictionary<string, string> Headers => m_Request.Headers;

		public byte[] Body => m_Request.Body;

		public ReadOnlyHttpRequest(global::Unity.Services.Core.Networking.Internal.HttpRequest request)
		{
			m_Request = request;
		}
	}
}
