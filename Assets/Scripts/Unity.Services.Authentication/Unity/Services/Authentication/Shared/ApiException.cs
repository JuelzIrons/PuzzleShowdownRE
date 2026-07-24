namespace Unity.Services.Authentication.Shared
{
	internal class ApiException : global::System.Exception
	{
		public global::Unity.Services.Authentication.Shared.ApiExceptionType Type { get; private set; }

		public global::Unity.Services.Authentication.Shared.IApiResponse Response { get; private set; }

		public ApiException(global::Unity.Services.Authentication.Shared.ApiExceptionType type, string message, global::Unity.Services.Authentication.Shared.IApiResponse response = null)
			: base(message)
		{
			Type = type;
			Response = response;
		}
	}
}
