namespace Unity.Services.Core.Networking.Internal
{
	internal interface IHttpClient : global::Unity.Services.Core.Internal.IServiceComponent
	{
		string GetBaseUrlFor(string serviceId);

		global::Unity.Services.Core.Networking.Internal.HttpOptions GetDefaultOptionsFor(string serviceId);

		global::Unity.Services.Core.Networking.Internal.HttpRequest CreateRequestForService(string serviceId, string resourcePath);

		global::Unity.Services.Core.Internal.IAsyncOperation<global::Unity.Services.Core.Networking.Internal.ReadOnlyHttpResponse> Send(global::Unity.Services.Core.Networking.Internal.HttpRequest request);
	}
}
