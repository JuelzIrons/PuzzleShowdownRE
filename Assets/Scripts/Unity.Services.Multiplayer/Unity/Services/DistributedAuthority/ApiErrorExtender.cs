namespace Unity.Services.DistributedAuthority
{
	internal static class ApiErrorExtender
	{
		public static global::Unity.Services.DistributedAuthority.DistributedAuthorityExceptionReason GetExceptionReason(this global::Unity.Services.DistributedAuthority.Models.ErrorResponseBody error)
		{
			global::Unity.Services.DistributedAuthority.DistributedAuthorityExceptionReason result = global::Unity.Services.DistributedAuthority.DistributedAuthorityExceptionReason.Unknown;
			if (error.Code != 45000)
			{
				if (global::System.Enum.IsDefined(typeof(global::Unity.Services.DistributedAuthority.DistributedAuthorityExceptionReason), error.Code))
				{
					result = (global::Unity.Services.DistributedAuthority.DistributedAuthorityExceptionReason)error.Code;
				}
			}
			else if (global::System.Enum.IsDefined(typeof(global::Unity.Services.DistributedAuthority.DistributedAuthorityExceptionReason), error.Status))
			{
				result = (global::Unity.Services.DistributedAuthority.DistributedAuthorityExceptionReason)error.Status;
			}
			return result;
		}

		public static global::Unity.Services.DistributedAuthority.DistributedAuthorityExceptionReason GetExceptionReason(this global::Unity.Services.DistributedAuthority.Http.HttpClientResponse error)
		{
			global::Unity.Services.DistributedAuthority.DistributedAuthorityExceptionReason result = global::Unity.Services.DistributedAuthority.DistributedAuthorityExceptionReason.Unknown;
			if (error.IsHttpError)
			{
				int num = (int)error.StatusCode + 45000;
				if (global::System.Enum.IsDefined(typeof(global::Unity.Services.DistributedAuthority.DistributedAuthorityExceptionReason), num))
				{
					result = (global::Unity.Services.DistributedAuthority.DistributedAuthorityExceptionReason)num;
				}
			}
			else if (error.IsNetworkError)
			{
				result = global::Unity.Services.DistributedAuthority.DistributedAuthorityExceptionReason.NetworkError;
			}
			return result;
		}

		public static string GetExceptionMessage(this global::Unity.Services.DistributedAuthority.Models.ErrorResponseBody error)
		{
			string text = error.Title + ": " + error.Detail;
			if (error.Details == null)
			{
				return text;
			}
			foreach (global::Unity.Services.DistributedAuthority.Models.ErrorDetail detail in error.Details)
			{
				text = text + "\n" + detail.Error + ": " + detail.Message;
			}
			return text;
		}
	}
}
