namespace Unity.Services.Qos.V2
{
	internal class Response
	{
		public global::System.Collections.Generic.Dictionary<string, string> Headers { get; }

		public long Status { get; set; }

		public Response(global::Unity.Services.Qos.V2.Http.HttpClientResponse httpResponse)
		{
			Headers = httpResponse.Headers;
			Status = httpResponse.StatusCode;
		}
	}
	internal class Response<T> : global::Unity.Services.Qos.V2.Response
	{
		public T Result { get; }

		public Response(global::Unity.Services.Qos.V2.Http.HttpClientResponse httpResponse, T result)
			: base(httpResponse)
		{
			Result = result;
		}
	}
}
