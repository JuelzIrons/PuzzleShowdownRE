namespace WebSocketSharp
{
	internal abstract class HttpBase
	{
		private global::System.Collections.Specialized.NameValueCollection _headers;

		private const int _headersMaxLength = 8192;

		private global::System.Version _version;

		internal byte[] EntityBodyData;

		protected const string CrLf = "\r\n";

		public string EntityBody
		{
			get
			{
				if (EntityBodyData == null || EntityBodyData.LongLength == 0)
				{
					return string.Empty;
				}
				global::System.Text.Encoding encoding = null;
				string text = _headers["Content-Type"];
				if (text != null && text.Length > 0)
				{
					encoding = global::WebSocketSharp.Net.HttpUtility.GetEncoding(text);
				}
				return (encoding ?? global::System.Text.Encoding.UTF8).GetString(EntityBodyData);
			}
		}

		public global::System.Collections.Specialized.NameValueCollection Headers => _headers;

		public global::System.Version ProtocolVersion => _version;

		protected HttpBase(global::System.Version version, global::System.Collections.Specialized.NameValueCollection headers)
		{
			_version = version;
			_headers = headers;
		}

		private static byte[] readEntityBody(global::System.IO.Stream stream, string length)
		{
			if (!long.TryParse(length, out var result))
			{
				throw new global::System.ArgumentException("Cannot be parsed.", "length");
			}
			if (result < 0)
			{
				throw new global::System.ArgumentOutOfRangeException("length", "Less than zero.");
			}
			return (result > 1024) ? stream.ReadBytes(result, 1024) : ((result > 0) ? stream.ReadBytes((int)result) : null);
		}

		private static string[] readHeaders(global::System.IO.Stream stream, int maxLength)
		{
			global::System.Collections.Generic.List<byte> buff = new global::System.Collections.Generic.List<byte>();
			int cnt = 0;
			global::System.Action<int> action = delegate(int i)
			{
				if (i == -1)
				{
					throw new global::System.IO.EndOfStreamException("The header cannot be read from the data source.");
				}
				buff.Add((byte)i);
				cnt++;
			};
			bool flag = false;
			while (cnt < maxLength)
			{
				if (stream.ReadByte().EqualsWith('\r', action) && stream.ReadByte().EqualsWith('\n', action) && stream.ReadByte().EqualsWith('\r', action) && stream.ReadByte().EqualsWith('\n', action))
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				throw new global::WebSocketSharp.WebSocketException("The length of header part is greater than the max length.");
			}
			return global::System.Text.Encoding.UTF8.GetString(buff.ToArray()).Replace("\r\n ", " ").Replace("\r\n\t", " ")
				.Split(new string[1] { "\r\n" }, global::System.StringSplitOptions.RemoveEmptyEntries);
		}

		protected static T Read<T>(global::System.IO.Stream stream, global::System.Func<string[], T> parser, int millisecondsTimeout) where T : global::WebSocketSharp.HttpBase
		{
			bool timeout = false;
			global::System.Threading.Timer timer = new global::System.Threading.Timer(delegate
			{
				timeout = true;
				stream.Close();
			}, null, millisecondsTimeout, -1);
			T val = null;
			global::System.Exception ex = null;
			try
			{
				val = parser(readHeaders(stream, 8192));
				string text = val.Headers["Content-Length"];
				if (text != null && text.Length > 0)
				{
					val.EntityBodyData = readEntityBody(stream, text);
				}
			}
			catch (global::System.Exception ex2)
			{
				ex = ex2;
			}
			finally
			{
				timer.Change(-1, -1);
				timer.Dispose();
			}
			string text2 = (timeout ? "A timeout has occurred while reading an HTTP request/response." : ((ex != null) ? "An exception has occurred while reading an HTTP request/response." : null));
			if (text2 != null)
			{
				throw new global::WebSocketSharp.WebSocketException(text2, ex);
			}
			return val;
		}

		public byte[] ToByteArray()
		{
			return global::System.Text.Encoding.UTF8.GetBytes(ToString());
		}
	}
}
