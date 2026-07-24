namespace Unity.Services.Authentication.PlayerAccounts
{
	internal static class HttpUtilities
	{
		private const int k_MinPort = 49215;

		private const int k_MaxPort = 65535;

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

		public static bool TryBindListenerOnFreePort(out global::System.Net.HttpListener httpListener, out int port)
		{
			for (port = 49215; port < 65535; port++)
			{
				httpListener = new global::System.Net.HttpListener();
				httpListener.Prefixes.Add($"http://localhost:{port}/");
				try
				{
					httpListener.Start();
					return true;
				}
				catch
				{
				}
			}
			port = 0;
			httpListener = null;
			return false;
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
