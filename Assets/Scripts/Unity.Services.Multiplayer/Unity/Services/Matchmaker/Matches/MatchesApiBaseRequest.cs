namespace Unity.Services.Matchmaker.Matches
{
	[global::UnityEngine.Scripting.Preserve]
	internal class MatchesApiBaseRequest
	{
		private static readonly global::System.Text.RegularExpressions.Regex JsonRegex = new global::System.Text.RegularExpressions.Regex("application\\/json(;\\s)?((charset=utf8|q=[0-1]\\.\\d)(\\s)?)*");

		[global::UnityEngine.Scripting.Preserve]
		public global::System.Collections.Generic.List<string> AddParamsToQueryParams(global::System.Collections.Generic.List<string> queryParams, string key, string value)
		{
			key = global::UnityEngine.Networking.UnityWebRequest.EscapeURL(key);
			value = global::UnityEngine.Networking.UnityWebRequest.EscapeURL(value);
			queryParams.Add(key + "=" + value);
			return queryParams;
		}

		[global::UnityEngine.Scripting.Preserve]
		public global::System.Collections.Generic.List<string> AddParamsToQueryParams(global::System.Collections.Generic.List<string> queryParams, string key, global::System.Collections.Generic.List<string> values, string style, bool explode)
		{
			if (explode)
			{
				foreach (string value in values)
				{
					string text = global::UnityEngine.Networking.UnityWebRequest.EscapeURL(value);
					queryParams.Add(global::UnityEngine.Networking.UnityWebRequest.EscapeURL(key) + "=" + text);
				}
			}
			else
			{
				string text2 = global::UnityEngine.Networking.UnityWebRequest.EscapeURL(key) + "=";
				foreach (string value2 in values)
				{
					text2 = text2 + global::UnityEngine.Networking.UnityWebRequest.EscapeURL(value2) + ",";
				}
				text2 = text2.Remove(text2.Length - 1);
				queryParams.Add(text2);
			}
			return queryParams;
		}

		[global::UnityEngine.Scripting.Preserve]
		public global::System.Collections.Generic.List<string> AddParamsToQueryParams(global::System.Collections.Generic.List<string> queryParams, global::System.Collections.Generic.Dictionary<string, string> modelVars)
		{
			foreach (string key in modelVars.Keys)
			{
				string text = global::UnityEngine.Networking.UnityWebRequest.EscapeURL(modelVars[key]);
				queryParams.Add(global::UnityEngine.Networking.UnityWebRequest.EscapeURL(key) + "=" + text);
			}
			return queryParams;
		}

		[global::UnityEngine.Scripting.Preserve]
		public global::System.Collections.Generic.List<string> AddParamsToQueryParams<T>(global::System.Collections.Generic.List<string> queryParams, string key, T value)
		{
			if (queryParams == null)
			{
				queryParams = new global::System.Collections.Generic.List<string>();
			}
			key = global::UnityEngine.Networking.UnityWebRequest.EscapeURL(key);
			string text = global::UnityEngine.Networking.UnityWebRequest.EscapeURL(value.ToString());
			queryParams.Add(key + "=" + text);
			return queryParams;
		}

		[global::UnityEngine.Scripting.Preserve]
		public string GetPathParamString(global::System.Collections.Generic.List<string> pathParam)
		{
			string text = "";
			foreach (string item in pathParam)
			{
				text = text + global::UnityEngine.Networking.UnityWebRequest.EscapeURL(item) + ",";
			}
			return text.Remove(text.Length - 1);
		}

		public byte[] ConstructBody(global::System.IO.Stream stream)
		{
			if (stream != null)
			{
				using (global::System.IO.MemoryStream memoryStream = new global::System.IO.MemoryStream())
				{
					stream.CopyTo(memoryStream);
					return memoryStream.ToArray();
				}
			}
			return null;
		}

		public byte[] ConstructBody(string s)
		{
			return global::System.Text.Encoding.UTF8.GetBytes(s);
		}

		public byte[] ConstructBody(object o)
		{
			return global::Unity.Services.Matchmaker.Matches.JsonSerialization.Serialize(o);
		}

		public string GenerateAcceptHeader(string[] accepts)
		{
			if (accepts.Length == 0)
			{
				return null;
			}
			for (int i = 0; i < accepts.Length; i++)
			{
				if (string.Equals(accepts[i], "application/json", global::System.StringComparison.OrdinalIgnoreCase))
				{
					return "application/json";
				}
			}
			return string.Join(", ", accepts);
		}

		public string GenerateContentTypeHeader(string[] contentTypes)
		{
			if (contentTypes.Length == 0)
			{
				return null;
			}
			for (int i = 0; i < contentTypes.Length; i++)
			{
				if (!string.IsNullOrWhiteSpace(contentTypes[i]) && JsonRegex.IsMatch(contentTypes[i]))
				{
					return contentTypes[i];
				}
			}
			return contentTypes[0];
		}

		public global::UnityEngine.Networking.IMultipartFormSection GenerateMultipartFormFileSection(string paramName, global::System.IO.FileStream stream, string contentType)
		{
			return new global::UnityEngine.Networking.MultipartFormFileSection(paramName, ConstructBody(stream), GetFileName(stream.Name), contentType);
		}

		public global::UnityEngine.Networking.IMultipartFormSection GenerateMultipartFormFileSection(string paramName, global::System.IO.Stream stream, string contentType)
		{
			return new global::UnityEngine.Networking.MultipartFormFileSection(paramName, ConstructBody(stream), global::System.Guid.NewGuid().ToString(), contentType);
		}

		private string GetFileName(string filePath)
		{
			return global::System.IO.Path.GetFileName(filePath);
		}
	}
}
