namespace Unity.Services.Core.Networking.Internal
{
	internal struct ReadOnlyHttpResponse
	{
		private global::Unity.Services.Core.Networking.Internal.HttpResponse m_Response;

		public global::Unity.Services.Core.Networking.Internal.ReadOnlyHttpRequest Request => m_Response.Request;

		public global::System.Collections.Generic.IReadOnlyDictionary<string, string> Headers => m_Response.Headers;

		public byte[] Data => m_Response.Data;

		public long StatusCode => m_Response.StatusCode;

		public string ErrorMessage => m_Response.ErrorMessage;

		public bool IsHttpError => m_Response.IsHttpError;

		public bool IsNetworkError => m_Response.IsNetworkError;

		public ReadOnlyHttpResponse(global::Unity.Services.Core.Networking.Internal.HttpResponse response)
		{
			m_Response = response;
		}
	}
}
