namespace Unity.Services.Relay
{
	internal static class ApiErrorExtender
	{
		public static global::Unity.Services.Relay.RelayExceptionReason GetExceptionReason(this global::Unity.Services.Relay.Models.ErrorResponseBody error)
		{
			global::Unity.Services.Relay.RelayExceptionReason result = global::Unity.Services.Relay.RelayExceptionReason.Unknown;
			if (error.Code != 15000)
			{
				if (global::System.Enum.IsDefined(typeof(global::Unity.Services.Relay.RelayExceptionReason), error.Code))
				{
					result = (global::Unity.Services.Relay.RelayExceptionReason)error.Code;
				}
			}
			else if (global::System.Enum.IsDefined(typeof(global::Unity.Services.Relay.RelayExceptionReason), error.Status))
			{
				result = (global::Unity.Services.Relay.RelayExceptionReason)error.Status;
			}
			return result;
		}

		public static global::Unity.Services.Relay.RelayExceptionReason GetExceptionReason(this global::Unity.Services.Relay.Http.HttpClientResponse error)
		{
			global::Unity.Services.Relay.RelayExceptionReason result = global::Unity.Services.Relay.RelayExceptionReason.Unknown;
			if (error.IsHttpError)
			{
				int num = (int)error.StatusCode + 15000;
				if (global::System.Enum.IsDefined(typeof(global::Unity.Services.Relay.RelayExceptionReason), num))
				{
					result = (global::Unity.Services.Relay.RelayExceptionReason)num;
				}
			}
			else if (error.IsNetworkError)
			{
				result = global::Unity.Services.Relay.RelayExceptionReason.NetworkError;
			}
			return result;
		}

		public static string GetExceptionMessage(this global::Unity.Services.Relay.Models.ErrorResponseBody error)
		{
			string text = error.Title + ": " + error.Detail;
			foreach (global::Unity.Services.Relay.Models.KeyValuePair item in error.Details ?? new global::System.Collections.Generic.List<global::Unity.Services.Relay.Models.KeyValuePair>())
			{
				text = text + "\n" + item.Key + ": " + item.Value;
			}
			return text;
		}
	}
}
