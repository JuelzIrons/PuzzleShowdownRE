namespace Unity.Services.Authentication.PlayerAccounts
{
	internal static class UriHelper
	{
		public static global::System.Collections.Generic.Dictionary<string, string> ParseQueryString(string queryString)
		{
			if (queryString == null)
			{
				throw global::Unity.Services.Authentication.PlayerAccounts.PlayerAccountsExceptionHandler.HandleError("queryString", "Query string cannot be null.");
			}
			global::System.Collections.Generic.Dictionary<string, string> dictionary = new global::System.Collections.Generic.Dictionary<string, string>();
			string[] array = queryString.TrimStart('?', '#').Split('&');
			for (int i = 0; i < array.Length; i++)
			{
				string[] array2 = array[i].Split('=');
				if (array2.Length == 2)
				{
					dictionary[array2[0]] = global::System.Uri.UnescapeDataString(array2[1]);
				}
			}
			return dictionary;
		}
	}
}
