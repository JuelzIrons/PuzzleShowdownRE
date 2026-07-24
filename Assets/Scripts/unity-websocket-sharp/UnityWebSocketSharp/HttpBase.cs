namespace UnityWebSocketSharp
{
	internal abstract class HttpBase
	{
		private global::System.Collections.Specialized.NameValueCollection _headers;

		private static readonly int _maxMessageHeaderLength;

		private string _messageBody;

		private byte[] _messageBodyData;

		private global::System.Version _version;

		protected static readonly string CrLf;

		protected static readonly string CrLfHt;

		protected static readonly string CrLfSp;

		internal byte[] MessageBodyData => _messageBodyData;

		protected string HeaderSection
		{
			get
			{
				global::System.Text.StringBuilder stringBuilder = new global::System.Text.StringBuilder(64);
				string[] allKeys = _headers.AllKeys;
				foreach (string text in allKeys)
				{
					stringBuilder.AppendFormat("{0}: {1}{2}", text, _headers[text], CrLf);
				}
				stringBuilder.Append(CrLf);
				return stringBuilder.ToString();
			}
		}

		public bool HasMessageBody => _messageBodyData != null;

		public global::System.Collections.Specialized.NameValueCollection Headers => _headers;

		public string MessageBody
		{
			get
			{
				if (_messageBody == null)
				{
					_messageBody = getMessageBody();
				}
				return _messageBody;
			}
		}

		public abstract string MessageHeader { get; }

		public global::System.Version ProtocolVersion => _version;

		static HttpBase()
		{
			_maxMessageHeaderLength = 8192;
			CrLf = "\r\n";
			CrLfHt = "\r\n\t";
			CrLfSp = "\r\n ";
		}

		protected HttpBase(global::System.Version version, global::System.Collections.Specialized.NameValueCollection headers)
		{
			_version = version;
			_headers = headers;
		}

		private string getMessageBody()
		{
			if (_messageBodyData == null || _messageBodyData.LongLength == 0L)
			{
				return string.Empty;
			}
			string text = _headers["Content-Type"];
			return ((text != null && text.Length > 0) ? global::UnityWebSocketSharp.Net.HttpUtility.GetEncoding(text) : global::System.Text.Encoding.UTF8).GetString(_messageBodyData);
		}

		private static byte[] readMessageBodyFrom(global::System.IO.Stream stream, string length)
		{
			if (!long.TryParse(length, out var result))
			{
				throw new global::System.ArgumentException("It cannot be parsed.", "length");
			}
			if (result < 0)
			{
				string message = "It is less than zero.";
				throw new global::System.ArgumentOutOfRangeException("length", message);
			}
			if (result <= 1024)
			{
				if (result <= 0)
				{
					return null;
				}
				return stream.ReadBytes((int)result);
			}
			return stream.ReadBytes(result, 1024);
		}

		private static string[] readMessageHeaderFrom(global::System.IO.Stream stream)
		{
			global::System.Collections.Generic.List<byte> buff = new global::System.Collections.Generic.List<byte>();
			int cnt = 0;
			global::System.Action<int> beforeComparing = delegate(int i)
			{
				if (i == -1)
				{
					throw new global::System.IO.EndOfStreamException("The header could not be read from the data stream.");
				}
				buff.Add((byte)i);
				cnt++;
			};
			bool flag = false;
			do
			{
				flag = stream.ReadByte().IsEqualTo('\r', beforeComparing) && stream.ReadByte().IsEqualTo('\n', beforeComparing) && stream.ReadByte().IsEqualTo('\r', beforeComparing) && stream.ReadByte().IsEqualTo('\n', beforeComparing);
				if (cnt > _maxMessageHeaderLength)
				{
					throw new global::System.InvalidOperationException("The length of the header is greater than the max length.");
				}
			}
			while (!flag);
			byte[] bytes = buff.ToArray();
			return global::System.Text.Encoding.UTF8.GetString(bytes).Replace(CrLfSp, " ").Replace(CrLfHt, " ")
				.Split(new string[1] { CrLf }, global::System.StringSplitOptions.RemoveEmptyEntries);
		}

		internal void WriteTo(global::System.IO.Stream stream)
		{
			byte[] array = ToByteArray();
			stream.Write(array, 0, array.Length);
		}

		protected static T Read<T>(global::System.IO.Stream stream, global::System.Func<string[], T> parser, int millisecondsTimeout) where T : global::UnityWebSocketSharp.HttpBase
		{
			T val = null;
			bool timeout = false;
			global::System.Threading.Timer timer = new global::System.Threading.Timer(delegate
			{
				timeout = true;
				stream.Close();
			}, null, millisecondsTimeout, -1);
			global::System.Exception ex = null;
			try
			{
				string[] arg = readMessageHeaderFrom(stream);
				val = parser(arg);
				string text = val.Headers["Content-Length"];
				if (text != null && text.Length > 0)
				{
					val._messageBodyData = readMessageBodyFrom(stream, text);
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
			if (timeout)
			{
				throw new global::UnityWebSocketSharp.WebSocketException("A timeout has occurred.");
			}
			if (ex != null)
			{
				throw new global::UnityWebSocketSharp.WebSocketException("An exception has occurred.", ex);
			}
			return val;
		}

		public byte[] ToByteArray()
		{
			byte[] bytes = global::System.Text.Encoding.UTF8.GetBytes(MessageHeader);
			if (_messageBodyData == null)
			{
				return bytes;
			}
			return global::System.Linq.Enumerable.ToArray(global::System.Linq.Enumerable.Concat(bytes, _messageBodyData));
		}

		public override string ToString()
		{
			if (_messageBodyData == null)
			{
				return MessageHeader;
			}
			return MessageHeader + MessageBody;
		}
	}
}
