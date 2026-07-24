namespace Unity.Services.Authentication.Shared
{
	internal interface IApiResponse
	{
		object Content { get; }

		int StatusCode { get; }

		global::Unity.Services.Authentication.Shared.Multimap<string, string> Headers { get; }

		string ErrorText { get; }

		string RawContent { get; }

		bool IsSuccessful { get; }

		bool IsRedirection { get; }

		bool IsClientError { get; }

		bool IsServerError { get; }
	}
}
