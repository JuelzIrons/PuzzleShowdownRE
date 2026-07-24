namespace Unity.Services.Qos.Http
{
	internal interface IHttpClient
	{
		global::System.Threading.Tasks.Task<global::Unity.Services.Qos.Http.HttpClientResponse> MakeRequestAsync(string method, string url, byte[] body, global::System.Collections.Generic.Dictionary<string, string> headers, int requestTimeout);

		global::System.Threading.Tasks.Task<global::Unity.Services.Qos.Http.HttpClientResponse> MakeRequestAsync(string method, string url, global::System.Collections.Generic.List<global::UnityEngine.Networking.IMultipartFormSection> body, global::System.Collections.Generic.Dictionary<string, string> headers, int requestTimeout, string boundary = null);
	}
}
