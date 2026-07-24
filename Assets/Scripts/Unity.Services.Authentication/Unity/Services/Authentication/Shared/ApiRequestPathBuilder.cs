namespace Unity.Services.Authentication.Shared
{
	internal class ApiRequestPathBuilder
	{
		private string _baseUrl;

		private string _path;

		private string _query = "?";

		public ApiRequestPathBuilder(string baseUrl, string path)
		{
			_baseUrl = baseUrl;
			_path = path;
		}

		public void AddPathParameters(global::System.Collections.Generic.Dictionary<string, string> parameters)
		{
			foreach (global::System.Collections.Generic.KeyValuePair<string, string> parameter in parameters)
			{
				_path = _path.Replace("{" + parameter.Key + "}", global::System.Uri.EscapeDataString(parameter.Value));
			}
		}

		public void AddQueryParameters(global::Unity.Services.Authentication.Shared.Multimap<string, string> parameters)
		{
			foreach (global::System.Collections.Generic.KeyValuePair<string, global::System.Collections.Generic.IList<string>> parameter in parameters)
			{
				foreach (string item in parameter.Value)
				{
					_query = _query + parameter.Key + "=" + global::System.Uri.EscapeDataString(item) + "&";
				}
			}
		}

		public string GetFullUri()
		{
			return _baseUrl + _path + _query.Substring(0, _query.Length - 1);
		}
	}
}
