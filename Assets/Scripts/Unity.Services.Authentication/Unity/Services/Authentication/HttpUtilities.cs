namespace Unity.Services.Authentication
{
	internal static class HttpUtilities
	{
		public static global::System.Collections.Generic.IDictionary<string, string> ParseQueryString(string queryString)
		{
			global::System.Collections.Generic.Dictionary<string, string> dictionary = new global::System.Collections.Generic.Dictionary<string, string>();
			string[] array = queryString.Split('?', '&');
			foreach (string text in array)
			{
				int num = text.IndexOf('=');
				if (num >= 0)
				{
					string key = UnescapeUrlString(text.Substring(0, num));
					string value = UnescapeUrlString(text.Substring(num + 1));
					dictionary[key] = value;
				}
			}
			return dictionary;
		}

		public static string EncodeQueryString(global::System.Collections.Generic.IDictionary<string, string> queryParams)
		{
			global::System.Text.StringBuilder stringBuilder = new global::System.Text.StringBuilder();
			bool flag = true;
			foreach (global::System.Collections.Generic.KeyValuePair<string, string> queryParam in queryParams)
			{
				if (!flag)
				{
					stringBuilder.Append('&');
				}
				else
				{
					flag = false;
				}
				stringBuilder.Append(EscapeUrlString(queryParam.Key)).Append('=').Append(EscapeUrlString(queryParam.Value));
			}
			return stringBuilder.ToString();
		}

		private static string EscapeUrlString(string rawString)
		{
			return global::System.Uri.EscapeDataString(rawString);
		}

		private static string UnescapeUrlString(string urlString)
		{
			return global::System.Uri.UnescapeDataString(urlString);
		}
	}
}
