namespace WebSocketSharp.Net
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

		private static string urlDecode(string s, global::System.Text.Encoding encoding)
		{
			return (s.IndexOfAny(new char[2] { '%', '+' }) > -1) ? global::WebSocketSharp.Net.HttpUtility.UrlDecode(s, encoding) : s;
		}

		public static global::WebSocketSharp.Net.QueryStringCollection Parse(string query)
		{
			return Parse(query, global::System.Text.Encoding.UTF8);
		}

		public static global::WebSocketSharp.Net.QueryStringCollection Parse(string query, global::System.Text.Encoding encoding)
		{
			if (query == null)
			{
				return new global::WebSocketSharp.Net.QueryStringCollection(1);
			}
			if (query.Length == 0)
			{
				return new global::WebSocketSharp.Net.QueryStringCollection(1);
			}
			if (query == "?")
			{
				return new global::WebSocketSharp.Net.QueryStringCollection(1);
			}
			if (query[0] == '?')
			{
				query = query.Substring(1);
			}
			if (encoding == null)
			{
				encoding = global::System.Text.Encoding.UTF8;
			}
			global::WebSocketSharp.Net.QueryStringCollection queryStringCollection = new global::WebSocketSharp.Net.QueryStringCollection();
			string[] array = query.Split(new char[1] { '&' });
			string[] array2 = array;
			foreach (string text in array2)
			{
				int length = text.Length;
				if (length != 0 && !(text == "="))
				{
					int num = text.IndexOf('=');
					if (num < 0)
					{
						queryStringCollection.Add(null, urlDecode(text, encoding));
						continue;
					}
					if (num == 0)
					{
						queryStringCollection.Add(null, urlDecode(text.Substring(1), encoding));
						continue;
					}
					string name = urlDecode(text.Substring(0, num), encoding);
					int num2 = num + 1;
					string value = ((num2 < length) ? urlDecode(text.Substring(num2), encoding) : string.Empty);
					queryStringCollection.Add(name, value);
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
