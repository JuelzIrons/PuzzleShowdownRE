namespace WebSocketSharp.Net
{
	[global::System.Serializable]
	public class CookieCollection : global::System.Collections.Generic.ICollection<global::WebSocketSharp.Net.Cookie>, global::System.Collections.Generic.IEnumerable<global::WebSocketSharp.Net.Cookie>, global::System.Collections.IEnumerable
	{
		private global::System.Collections.Generic.List<global::WebSocketSharp.Net.Cookie> _list;

		private bool _readOnly;

		private object _sync;

		internal global::System.Collections.Generic.IList<global::WebSocketSharp.Net.Cookie> List => _list;

		internal global::System.Collections.Generic.IEnumerable<global::WebSocketSharp.Net.Cookie> Sorted
		{
			get
			{
				global::System.Collections.Generic.List<global::WebSocketSharp.Net.Cookie> list = new global::System.Collections.Generic.List<global::WebSocketSharp.Net.Cookie>(_list);
				if (list.Count > 1)
				{
					list.Sort(compareForSorted);
				}
				return list;
			}
		}

		public int Count => _list.Count;

		public bool IsReadOnly
		{
			get
			{
				return _readOnly;
			}
			internal set
			{
				_readOnly = value;
			}
		}

		public bool IsSynchronized => false;

		public global::WebSocketSharp.Net.Cookie this[int index]
		{
			get
			{
				if (index < 0 || index >= _list.Count)
				{
					throw new global::System.ArgumentOutOfRangeException("index");
				}
				return _list[index];
			}
		}

		public global::WebSocketSharp.Net.Cookie this[string name]
		{
			get
			{
				if (name == null)
				{
					throw new global::System.ArgumentNullException("name");
				}
				global::System.StringComparison comparisonType = global::System.StringComparison.InvariantCultureIgnoreCase;
				foreach (global::WebSocketSharp.Net.Cookie item in Sorted)
				{
					if (item.Name.Equals(name, comparisonType))
					{
						return item;
					}
				}
				return null;
			}
		}

		public object SyncRoot => _sync;

		public CookieCollection()
		{
			_list = new global::System.Collections.Generic.List<global::WebSocketSharp.Net.Cookie>();
			_sync = ((global::System.Collections.ICollection)_list).SyncRoot;
		}

		private void add(global::WebSocketSharp.Net.Cookie cookie)
		{
			int num = search(cookie);
			if (num == -1)
			{
				_list.Add(cookie);
			}
			else
			{
				_list[num] = cookie;
			}
		}

		private static int compareForSort(global::WebSocketSharp.Net.Cookie x, global::WebSocketSharp.Net.Cookie y)
		{
			return x.Name.Length + x.Value.Length - (y.Name.Length + y.Value.Length);
		}

		private static int compareForSorted(global::WebSocketSharp.Net.Cookie x, global::WebSocketSharp.Net.Cookie y)
		{
			int num = x.Version - y.Version;
			return (num != 0) ? num : (((num = x.Name.CompareTo(y.Name)) != 0) ? num : (y.Path.Length - x.Path.Length));
		}

		private static global::WebSocketSharp.Net.CookieCollection parseRequest(string value)
		{
			global::WebSocketSharp.Net.CookieCollection cookieCollection = new global::WebSocketSharp.Net.CookieCollection();
			global::WebSocketSharp.Net.Cookie result = null;
			int num = 0;
			global::System.StringComparison comparisonType = global::System.StringComparison.InvariantCultureIgnoreCase;
			global::System.Collections.Generic.List<string> list = value.SplitHeaderValue(',', ';').ToList();
			for (int i = 0; i < list.Count; i++)
			{
				string text = list[i].Trim();
				if (text.Length == 0)
				{
					continue;
				}
				int num2 = text.IndexOf('=');
				switch (num2)
				{
				case -1:
					if (result != null && text.Equals("$port", comparisonType))
					{
						result.Port = "\"\"";
					}
					continue;
				case 0:
					if (result != null)
					{
						cookieCollection.add(result);
						result = null;
					}
					continue;
				}
				string text2 = text.Substring(0, num2).TrimEnd(new char[1] { ' ' });
				string text3 = ((num2 < text.Length - 1) ? text.Substring(num2 + 1).TrimStart(new char[1] { ' ' }) : string.Empty);
				if (text2.Equals("$version", comparisonType))
				{
					if (text3.Length != 0 && int.TryParse(text3.Unquote(), out var result2))
					{
						num = result2;
					}
					continue;
				}
				if (text2.Equals("$path", comparisonType))
				{
					if (result != null && text3.Length != 0)
					{
						result.Path = text3;
					}
					continue;
				}
				if (text2.Equals("$domain", comparisonType))
				{
					if (result != null && text3.Length != 0)
					{
						result.Domain = text3;
					}
					continue;
				}
				if (text2.Equals("$port", comparisonType))
				{
					if (result != null && text3.Length != 0)
					{
						result.Port = text3;
					}
					continue;
				}
				if (result != null)
				{
					cookieCollection.add(result);
				}
				if (global::WebSocketSharp.Net.Cookie.TryCreate(text2, text3, out result) && num != 0)
				{
					result.Version = num;
				}
			}
			if (result != null)
			{
				cookieCollection.add(result);
			}
			return cookieCollection;
		}

		private static global::WebSocketSharp.Net.CookieCollection parseResponse(string value)
		{
			global::WebSocketSharp.Net.CookieCollection cookieCollection = new global::WebSocketSharp.Net.CookieCollection();
			global::WebSocketSharp.Net.Cookie result = null;
			global::System.StringComparison comparisonType = global::System.StringComparison.InvariantCultureIgnoreCase;
			global::System.Collections.Generic.List<string> list = value.SplitHeaderValue(',', ';').ToList();
			for (int i = 0; i < list.Count; i++)
			{
				string text = list[i].Trim();
				if (text.Length == 0)
				{
					continue;
				}
				int num = text.IndexOf('=');
				switch (num)
				{
				case -1:
					if (result != null)
					{
						if (text.Equals("port", comparisonType))
						{
							result.Port = "\"\"";
						}
						else if (text.Equals("discard", comparisonType))
						{
							result.Discard = true;
						}
						else if (text.Equals("secure", comparisonType))
						{
							result.Secure = true;
						}
						else if (text.Equals("httponly", comparisonType))
						{
							result.HttpOnly = true;
						}
					}
					continue;
				case 0:
					if (result != null)
					{
						cookieCollection.add(result);
						result = null;
					}
					continue;
				}
				string text2 = text.Substring(0, num).TrimEnd(new char[1] { ' ' });
				string text3 = ((num < text.Length - 1) ? text.Substring(num + 1).TrimStart(new char[1] { ' ' }) : string.Empty);
				if (text2.Equals("version", comparisonType))
				{
					if (result != null && text3.Length != 0 && int.TryParse(text3.Unquote(), out var result2))
					{
						result.Version = result2;
					}
				}
				else if (text2.Equals("expires", comparisonType))
				{
					if (text3.Length == 0)
					{
						continue;
					}
					if (i == list.Count - 1)
					{
						break;
					}
					i++;
					if (result != null && !(result.Expires != global::System.DateTime.MinValue))
					{
						global::System.Text.StringBuilder stringBuilder = new global::System.Text.StringBuilder(text3, 32);
						stringBuilder.AppendFormat(", {0}", list[i].Trim());
						if (global::System.DateTime.TryParseExact(stringBuilder.ToString(), new string[2] { "ddd, dd'-'MMM'-'yyyy HH':'mm':'ss 'GMT'", "r" }, global::System.Globalization.CultureInfo.CreateSpecificCulture("en-US"), global::System.Globalization.DateTimeStyles.AdjustToUniversal | global::System.Globalization.DateTimeStyles.AssumeUniversal, out var result3))
						{
							result.Expires = result3.ToLocalTime();
						}
					}
				}
				else if (text2.Equals("max-age", comparisonType))
				{
					if (result != null && text3.Length != 0 && int.TryParse(text3.Unquote(), out var result4))
					{
						result.MaxAge = result4;
					}
				}
				else if (text2.Equals("path", comparisonType))
				{
					if (result != null && text3.Length != 0)
					{
						result.Path = text3;
					}
				}
				else if (text2.Equals("domain", comparisonType))
				{
					if (result != null && text3.Length != 0)
					{
						result.Domain = text3;
					}
				}
				else if (text2.Equals("port", comparisonType))
				{
					if (result != null && text3.Length != 0)
					{
						result.Port = text3;
					}
				}
				else if (text2.Equals("comment", comparisonType))
				{
					if (result != null && text3.Length != 0)
					{
						result.Comment = urlDecode(text3, global::System.Text.Encoding.UTF8);
					}
				}
				else if (text2.Equals("commenturl", comparisonType))
				{
					if (result != null && text3.Length != 0)
					{
						result.CommentUri = text3.Unquote().ToUri();
					}
				}
				else if (text2.Equals("samesite", comparisonType))
				{
					if (result != null && text3.Length != 0)
					{
						result.SameSite = text3.Unquote();
					}
				}
				else
				{
					if (result != null)
					{
						cookieCollection.add(result);
					}
					global::WebSocketSharp.Net.Cookie.TryCreate(text2, text3, out result);
				}
			}
			if (result != null)
			{
				cookieCollection.add(result);
			}
			return cookieCollection;
		}

		private int search(global::WebSocketSharp.Net.Cookie cookie)
		{
			for (int num = _list.Count - 1; num >= 0; num--)
			{
				if (_list[num].EqualsWithoutValue(cookie))
				{
					return num;
				}
			}
			return -1;
		}

		private static string urlDecode(string s, global::System.Text.Encoding encoding)
		{
			if (s.IndexOfAny(new char[2] { '%', '+' }) == -1)
			{
				return s;
			}
			try
			{
				return global::WebSocketSharp.Net.HttpUtility.UrlDecode(s, encoding);
			}
			catch
			{
				return null;
			}
		}

		internal static global::WebSocketSharp.Net.CookieCollection Parse(string value, bool response)
		{
			try
			{
				return response ? parseResponse(value) : parseRequest(value);
			}
			catch (global::System.Exception innerException)
			{
				throw new global::WebSocketSharp.Net.CookieException("It could not be parsed.", innerException);
			}
		}

		internal void SetOrRemove(global::WebSocketSharp.Net.Cookie cookie)
		{
			int num = search(cookie);
			if (num == -1)
			{
				if (!cookie.Expired)
				{
					_list.Add(cookie);
				}
			}
			else if (cookie.Expired)
			{
				_list.RemoveAt(num);
			}
			else
			{
				_list[num] = cookie;
			}
		}

		internal void SetOrRemove(global::WebSocketSharp.Net.CookieCollection cookies)
		{
			foreach (global::WebSocketSharp.Net.Cookie item in cookies._list)
			{
				SetOrRemove(item);
			}
		}

		internal void Sort()
		{
			if (_list.Count > 1)
			{
				_list.Sort(compareForSort);
			}
		}

		public void Add(global::WebSocketSharp.Net.Cookie cookie)
		{
			if (_readOnly)
			{
				string message = "The collection is read-only.";
				throw new global::System.InvalidOperationException(message);
			}
			if (cookie == null)
			{
				throw new global::System.ArgumentNullException("cookie");
			}
			add(cookie);
		}

		public void Add(global::WebSocketSharp.Net.CookieCollection cookies)
		{
			if (_readOnly)
			{
				string message = "The collection is read-only.";
				throw new global::System.InvalidOperationException(message);
			}
			if (cookies == null)
			{
				throw new global::System.ArgumentNullException("cookies");
			}
			foreach (global::WebSocketSharp.Net.Cookie item in cookies._list)
			{
				add(item);
			}
		}

		public void Clear()
		{
			if (_readOnly)
			{
				string message = "The collection is read-only.";
				throw new global::System.InvalidOperationException(message);
			}
			_list.Clear();
		}

		public bool Contains(global::WebSocketSharp.Net.Cookie cookie)
		{
			if (cookie == null)
			{
				throw new global::System.ArgumentNullException("cookie");
			}
			return search(cookie) > -1;
		}

		public void CopyTo(global::WebSocketSharp.Net.Cookie[] array, int index)
		{
			if (array == null)
			{
				throw new global::System.ArgumentNullException("array");
			}
			if (index < 0)
			{
				throw new global::System.ArgumentOutOfRangeException("index", "Less than zero.");
			}
			if (array.Length - index < _list.Count)
			{
				string message = "The available space of the array is not enough to copy to.";
				throw new global::System.ArgumentException(message);
			}
			_list.CopyTo(array, index);
		}

		public global::System.Collections.Generic.IEnumerator<global::WebSocketSharp.Net.Cookie> GetEnumerator()
		{
			return _list.GetEnumerator();
		}

		public bool Remove(global::WebSocketSharp.Net.Cookie cookie)
		{
			if (_readOnly)
			{
				string message = "The collection is read-only.";
				throw new global::System.InvalidOperationException(message);
			}
			if (cookie == null)
			{
				throw new global::System.ArgumentNullException("cookie");
			}
			int num = search(cookie);
			if (num == -1)
			{
				return false;
			}
			_list.RemoveAt(num);
			return true;
		}

		global::System.Collections.IEnumerator global::System.Collections.IEnumerable.GetEnumerator()
		{
			return _list.GetEnumerator();
		}
	}
}
