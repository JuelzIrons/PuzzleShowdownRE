namespace UnityWebSocketSharp.Net
{
	internal sealed class QueryStringCollection : global::System.Collections.Specialized.NameValueCollection
	{
		public QueryStringCollection()
		{
		}

		public QueryStringCollection(int capacity)
			: base(capacity)
		{
		}

		public static global::UnityWebSocketSharp.Net.QueryStringCollection Parse(string query)
		{
			return Parse(query, global::System.Text.Encoding.UTF8);
		}

		public static global::UnityWebSocketSharp.Net.QueryStringCollection Parse(string query, global::System.Text.Encoding encoding)
		{
			if (query == null)
			{
				return new global::UnityWebSocketSharp.Net.QueryStringCollection(1);
			}
			if (query.Length == 0)
			{
				return new global::UnityWebSocketSharp.Net.QueryStringCollection(1);
			}
			if (query == "?")
			{
				return new global::UnityWebSocketSharp.Net.QueryStringCollection(1);
			}
			if (query[0] == '?')
			{
				query = query.Substring(1);
			}
			if (encoding == null)
			{
				encoding = global::System.Text.Encoding.UTF8;
			}
			global::UnityWebSocketSharp.Net.QueryStringCollection queryStringCollection = new global::UnityWebSocketSharp.Net.QueryStringCollection();
			string[] array = query.Split('&');
			foreach (string text in array)
			{
				int length = text.Length;
				if (length != 0 && !(text == "="))
				{
					string name = null;
					string text2 = null;
					int num = text.IndexOf('=');
					if (num < 0)
					{
						text2 = text.UrlDecode(encoding);
					}
					else if (num == 0)
					{
						text2 = text.Substring(1).UrlDecode(encoding);
					}
					else
					{
						name = text.Substring(0, num).UrlDecode(encoding);
						int num2 = num + 1;
						text2 = ((num2 < length) ? text.Substring(num2).UrlDecode(encoding) : string.Empty);
					}
					queryStringCollection.Add(name, text2);
				}
			}
			return queryStringCollection;
		}

		public override string ToString()
		{
			global::System.Text.StringBuilder stringBuilder = new global::System.Text.StringBuilder();
			string[] allKeys = AllKeys;
			foreach (string text in allKeys)
			{
				stringBuilder.AppendFormat("{0}={1}&", text, base[text]);
			}
			if (stringBuilder.Length > 0)
			{
				stringBuilder.Length--;
			}
			return stringBuilder.ToString();
		}
	}
}
