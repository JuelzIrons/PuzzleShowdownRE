namespace Unity.Services.Lobbies.Http
{
	internal static class CommonErrors
	{
		private const string ErrorPrefix = "com.unity.services.lobby";

		public static global::Unity.Services.Lobbies.Http.IError RequestOnSuccessNull => new global::Unity.Services.Lobbies.Http.BasicError("com.unity.services.lobbyonsuccessnullerror", "Request must have an onSuccess callback", null, 0, "");

		public static global::Unity.Services.Lobbies.Http.IError HttpNetworkError => new global::Unity.Services.Lobbies.Http.BasicError("com.unity.services.lobbyhttpclient.networkerror", "Network Error", null, 0, "");

		public static global::Unity.Services.Lobbies.Http.IError CreateUnspecifiedHttpError(string details)
		{
			return new global::Unity.Services.Lobbies.Http.BasicError("com.unity.services.lobbyhttp.httperror", "Unspecified HTTP error", null, 0, details);
		}
	}
}
