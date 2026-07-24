namespace Unity.Services.Lobbies
{
	internal class Response
	{
		public global::System.Collections.Generic.Dictionary<string, string> Headers { get; }

		public long Status { get; set; }

		public Response(global::Unity.Services.Lobbies.Http.HttpClientResponse httpResponse)
		{
			Headers = httpResponse.Headers;
			Status = httpResponse.StatusCode;
		}
	}
	internal class Response<T> : global::Unity.Services.Lobbies.Response
	{
		public T Result { get; }

		public Response(global::Unity.Services.Lobbies.Http.HttpClientResponse httpResponse, T result)
			: base(httpResponse)
		{
			Result = result;
		}
	}
}
