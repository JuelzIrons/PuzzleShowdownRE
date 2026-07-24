namespace WebSocketSharp
{
	public static class Ext
	{
		private static readonly byte[] _last = new byte[1];

		private static readonly int _retry = 5;

		private const string _tspecials = "()<>@,;:\\\"/[]?={} \t";

		private static byte[] compress(this byte[] data)
		{
			if (data.LongLength == 0)
			{
				return data;
			}
			using global::System.IO.MemoryStream stream = new global::System.IO.MemoryStream(data);
			return stream.compressToArray();
		}

		private static global::System.IO.MemoryStream compress(this global::System.IO.Stream stream)
		{
			global::System.IO.MemoryStream memoryStream = new global::System.IO.MemoryStream();
			if (stream.Length == 0)
			{
				return memoryStream;
			}
			stream.Position = 0L;
			using global::System.IO.Compression.DeflateStream deflateStream = new global::System.IO.Compression.DeflateStream(memoryStream, global::System.IO.Compression.CompressionMode.Compress, leaveOpen: true);
			stream.CopyTo(deflateStream, 1024);
			deflateStream.Close();
			memoryStream.Write(_last, 0, 1);
			memoryStream.Position = 0L;
			return memoryStream;
		}

		private static byte[] compressToArray(this global::System.IO.Stream stream)
		{
			using global::System.IO.MemoryStream memoryStream = stream.compress();
			memoryStream.Close();
			return memoryStream.ToArray();
		}

		private static byte[] decompress(this byte[] data)
		{
			if (data.LongLength == 0)
			{
				return data;
			}
			using global::System.IO.MemoryStream stream = new global::System.IO.MemoryStream(data);
			return stream.decompressToArray();
		}

		private static global::System.IO.MemoryStream decompress(this global::System.IO.Stream stream)
		{
			global::System.IO.MemoryStream memoryStream = new global::System.IO.MemoryStream();
			if (stream.Length == 0)
			{
				return memoryStream;
			}
			stream.Position = 0L;
			using global::System.IO.Compression.DeflateStream deflateStream = new global::System.IO.Compression.DeflateStream(stream, global::System.IO.Compression.CompressionMode.Decompress, leaveOpen: true);
			deflateStream.CopyTo(memoryStream, 1024);
			memoryStream.Position = 0L;
			return memoryStream;
		}

		private static byte[] decompressToArray(this global::System.IO.Stream stream)
		{
			using global::System.IO.MemoryStream memoryStream = stream.decompress();
			memoryStream.Close();
			return memoryStream.ToArray();
		}

		private static bool isHttpMethod(this string value)
		{
			int result;
			switch (value)
			{
			default:
				result = ((value == "TRACE") ? 1 : 0);
				break;
			case "GET":
			case "HEAD":
			case "POST":
			case "PUT":
			case "DELETE":
			case "CONNECT":
			case "OPTIONS":
				result = 1;
				break;
			}
			return (byte)result != 0;
		}

		private static bool isHttpMethod10(this string value)
		{
			return value == "GET" || value == "HEAD" || value == "POST";
		}

		internal static byte[] Append(this ushort code, string reason)
		{
			byte[] array = code.InternalToByteArray(global::WebSocketSharp.ByteOrder.Big);
			if (reason == null || reason.Length == 0)
			{
				return array;
			}
			global::System.Collections.Generic.List<byte> list = new global::System.Collections.Generic.List<byte>(array);
			list.AddRange(global::System.Text.Encoding.UTF8.GetBytes(reason));
			return list.ToArray();
		}

		internal static byte[] Compress(this byte[] data, global::WebSocketSharp.CompressionMethod method)
		{
			return (method == global::WebSocketSharp.CompressionMethod.Deflate) ? data.compress() : data;
		}

		internal static global::System.IO.Stream Compress(this global::System.IO.Stream stream, global::WebSocketSharp.CompressionMethod method)
		{
			return (method == global::WebSocketSharp.CompressionMethod.Deflate) ? stream.compress() : stream;
		}

		internal static byte[] CompressToArray(this global::System.IO.Stream stream, global::WebSocketSharp.CompressionMethod method)
		{
			return (method == global::WebSocketSharp.CompressionMethod.Deflate) ? stream.compressToArray() : stream.ToByteArray();
		}

		internal static bool Contains(this string value, params char[] anyOf)
		{
			return anyOf != null && anyOf.Length != 0 && value.IndexOfAny(anyOf) > -1;
		}

		internal static bool Contains(this global::System.Collections.Specialized.NameValueCollection collection, string name)
		{
			return collection[name] != null;
		}

		internal static bool Contains(this global::System.Collections.Specialized.NameValueCollection collection, string name, string value, global::System.StringComparison comparisonTypeForValue)
		{
			string text = collection[name];
			if (text == null)
			{
				return false;
			}
			string[] array = text.Split(new char[1] { ',' });
			foreach (string text2 in array)
			{
				if (text2.Trim().Equals(value, comparisonTypeForValue))
				{
					return true;
				}
			}
			return false;
		}

		internal static bool Contains<T>(this global::System.Collections.Generic.IEnumerable<T> source, global::System.Func<T, bool> condition)
		{
			foreach (T item in source)
			{
				if (condition(item))
				{
					return true;
				}
			}
			return false;
		}

		internal static bool ContainsTwice(this string[] values)
		{
			int len = values.Length;
			int end = len - 1;
			global::System.Func<int, bool> seek = null;
			seek = delegate(int idx)
			{
				if (idx == end)
				{
					return false;
				}
				string text = values[idx];
				for (int i = idx + 1; i < len; i++)
				{
					if (values[i] == text)
					{
						return true;
					}
				}
				return seek(++idx);
			};
			return seek(0);
		}

		internal static T[] Copy<T>(this T[] source, int length)
		{
			T[] array = new T[length];
			global::System.Array.Copy(source, 0, array, 0, length);
			return array;
		}

		internal static T[] Copy<T>(this T[] source, long length)
		{
			T[] array = new T[length];
			global::System.Array.Copy(source, 0L, array, 0L, length);
			return array;
		}

		internal static void CopyTo(this global::System.IO.Stream source, global::System.IO.Stream destination, int bufferLength)
		{
			byte[] buffer = new byte[bufferLength];
			int num = 0;
			while (true)
			{
				num = source.Read(buffer, 0, bufferLength);
				if (num <= 0)
				{
					break;
				}
				destination.Write(buffer, 0, num);
			}
		}

		internal static void CopyToAsync(this global::System.IO.Stream source, global::System.IO.Stream destination, int bufferLength, global::System.Action completed, global::System.Action<global::System.Exception> error)
		{
			byte[] buff = new byte[bufferLength];
			global::System.AsyncCallback callback = null;
			callback = delegate(global::System.IAsyncResult ar)
			{
				try
				{
					int num = source.EndRead(ar);
					if (num <= 0)
					{
						if (completed != null)
						{
							completed();
						}
					}
					else
					{
						destination.Write(buff, 0, num);
						source.BeginRead(buff, 0, bufferLength, callback, null);
					}
				}
				catch (global::System.Exception obj2)
				{
					if (error != null)
					{
						error(obj2);
					}
				}
			};
			try
			{
				source.BeginRead(buff, 0, bufferLength, callback, null);
			}
			catch (global::System.Exception obj)
			{
				if (error != null)
				{
					error(obj);
				}
			}
		}

		internal static byte[] Decompress(this byte[] data, global::WebSocketSharp.CompressionMethod method)
		{
			return (method == global::WebSocketSharp.CompressionMethod.Deflate) ? data.decompress() : data;
		}

		internal static global::System.IO.Stream Decompress(this global::System.IO.Stream stream, global::WebSocketSharp.CompressionMethod method)
		{
			return (method == global::WebSocketSharp.CompressionMethod.Deflate) ? stream.decompress() : stream;
		}

		internal static byte[] DecompressToArray(this global::System.IO.Stream stream, global::WebSocketSharp.CompressionMethod method)
		{
			return (method == global::WebSocketSharp.CompressionMethod.Deflate) ? stream.decompressToArray() : stream.ToByteArray();
		}

		internal static void Emit(this global::System.EventHandler eventHandler, object sender, global::System.EventArgs e)
		{
			eventHandler?.Invoke(sender, e);
		}

		internal static void Emit<TEventArgs>(this global::System.EventHandler<TEventArgs> eventHandler, object sender, TEventArgs e) where TEventArgs : global::System.EventArgs
		{
			eventHandler?.Invoke(sender, e);
		}

		internal static bool EqualsWith(this int value, char c, global::System.Action<int> action)
		{
			action(value);
			return value == c;
		}

		internal static string GetAbsolutePath(this global::System.Uri uri)
		{
			if (uri.IsAbsoluteUri)
			{
				return uri.AbsolutePath;
			}
			string originalString = uri.OriginalString;
			if (originalString[0] != '/')
			{
				return null;
			}
			int num = originalString.IndexOfAny(new char[2] { '?', '#' });
			return (num > 0) ? originalString.Substring(0, num) : originalString;
		}

		internal static global::WebSocketSharp.Net.CookieCollection GetCookies(this global::System.Collections.Specialized.NameValueCollection headers, bool response)
		{
			string text = headers[response ? "Set-Cookie" : "Cookie"];
			return (text != null) ? global::WebSocketSharp.Net.CookieCollection.Parse(text, response) : new global::WebSocketSharp.Net.CookieCollection();
		}

		internal static string GetDnsSafeHost(this global::System.Uri uri, bool bracketIPv6)
		{
			return (bracketIPv6 && uri.HostNameType == global::System.UriHostNameType.IPv6) ? uri.Host : uri.DnsSafeHost;
		}

		internal static string GetMessage(this global::WebSocketSharp.CloseStatusCode code)
		{
			return code switch
			{
				global::WebSocketSharp.CloseStatusCode.TlsHandshakeFailure => "An error has occurred during a TLS handshake.", 
				global::WebSocketSharp.CloseStatusCode.ServerError => "WebSocket server got an internal error.", 
				global::WebSocketSharp.CloseStatusCode.MandatoryExtension => "WebSocket client didn't receive expected extension(s).", 
				global::WebSocketSharp.CloseStatusCode.TooBig => "A too big message has been received.", 
				global::WebSocketSharp.CloseStatusCode.PolicyViolation => "A policy violation has occurred.", 
				global::WebSocketSharp.CloseStatusCode.InvalidData => "Invalid data has been received.", 
				global::WebSocketSharp.CloseStatusCode.Abnormal => "An exception has occurred.", 
				global::WebSocketSharp.CloseStatusCode.UnsupportedData => "Unsupported data has been received.", 
				global::WebSocketSharp.CloseStatusCode.ProtocolError => "A WebSocket protocol error has occurred.", 
				_ => string.Empty, 
			};
		}

		internal static string GetName(this string nameAndValue, char separator)
		{
			int num = nameAndValue.IndexOf(separator);
			return (num > 0) ? nameAndValue.Substring(0, num).Trim() : null;
		}

		internal static string GetUTF8DecodedString(this byte[] bytes)
		{
			return global::System.Text.Encoding.UTF8.GetString(bytes);
		}

		internal static byte[] GetUTF8EncodedBytes(this string s)
		{
			return global::System.Text.Encoding.UTF8.GetBytes(s);
		}

		internal static string GetValue(this string nameAndValue, char separator)
		{
			return nameAndValue.GetValue(separator, unquote: false);
		}

		internal static string GetValue(this string nameAndValue, char separator, bool unquote)
		{
			int num = nameAndValue.IndexOf(separator);
			if (num < 0 || num == nameAndValue.Length - 1)
			{
				return null;
			}
			string text = nameAndValue.Substring(num + 1).Trim();
			return unquote ? text.Unquote() : text;
		}

		internal static byte[] InternalToByteArray(this ushort value, global::WebSocketSharp.ByteOrder order)
		{
			byte[] bytes = global::System.BitConverter.GetBytes(value);
			if (!order.IsHostOrder())
			{
				global::System.Array.Reverse((global::System.Array)bytes);
			}
			return bytes;
		}

		internal static byte[] InternalToByteArray(this ulong value, global::WebSocketSharp.ByteOrder order)
		{
			byte[] bytes = global::System.BitConverter.GetBytes(value);
			if (!order.IsHostOrder())
			{
				global::System.Array.Reverse((global::System.Array)bytes);
			}
			return bytes;
		}

		internal static bool IsCompressionExtension(this string value, global::WebSocketSharp.CompressionMethod method)
		{
			return value.StartsWith(method.ToExtensionString());
		}

		internal static bool IsControl(this byte opcode)
		{
			return opcode > 7 && opcode < 16;
		}

		internal static bool IsControl(this global::WebSocketSharp.Opcode opcode)
		{
			return (int)opcode >= 8;
		}

		internal static bool IsData(this byte opcode)
		{
			return opcode == 1 || opcode == 2;
		}

		internal static bool IsData(this global::WebSocketSharp.Opcode opcode)
		{
			return opcode == global::WebSocketSharp.Opcode.Text || opcode == global::WebSocketSharp.Opcode.Binary;
		}

		internal static bool IsHttpMethod(this string value, global::System.Version version)
		{
			return (version == global::WebSocketSharp.Net.HttpVersion.Version10) ? value.isHttpMethod10() : value.isHttpMethod();
		}

		internal static bool IsPortNumber(this int value)
		{
			return value > 0 && value < 65536;
		}

		internal static bool IsReserved(this ushort code)
		{
			return code == 1004 || code == 1005 || code == 1006 || code == 1015;
		}

		internal static bool IsReserved(this global::WebSocketSharp.CloseStatusCode code)
		{
			return code == global::WebSocketSharp.CloseStatusCode.Undefined || code == global::WebSocketSharp.CloseStatusCode.NoStatus || code == global::WebSocketSharp.CloseStatusCode.Abnormal || code == global::WebSocketSharp.CloseStatusCode.TlsHandshakeFailure;
		}

		internal static bool IsSupported(this byte opcode)
		{
			return global::System.Enum.IsDefined(typeof(global::WebSocketSharp.Opcode), opcode);
		}

		internal static bool IsText(this string value)
		{
			int length = value.Length;
			for (int i = 0; i < length; i++)
			{
				char c = value[i];
				if (c < ' ')
				{
					if ("\r\n\t".IndexOf(c) == -1)
					{
						return false;
					}
					if (c == '\n')
					{
						i++;
						if (i == length)
						{
							break;
						}
						c = value[i];
						if (" \t".IndexOf(c) == -1)
						{
							return false;
						}
					}
				}
				else if (c == '\u007f')
				{
					return false;
				}
			}
			return true;
		}

		internal static bool IsToken(this string value)
		{
			foreach (char c in value)
			{
				if (c < ' ')
				{
					return false;
				}
				if (c > '~')
				{
					return false;
				}
				if ("()<>@,;:\\\"/[]?={} \t".IndexOf(c) > -1)
				{
					return false;
				}
			}
			return true;
		}

		internal static bool KeepsAlive(this global::System.Collections.Specialized.NameValueCollection headers, global::System.Version version)
		{
			global::System.StringComparison comparisonTypeForValue = global::System.StringComparison.OrdinalIgnoreCase;
			return (version < global::WebSocketSharp.Net.HttpVersion.Version11) ? headers.Contains("Connection", "keep-alive", comparisonTypeForValue) : (!headers.Contains("Connection", "close", comparisonTypeForValue));
		}

		internal static string Quote(this string value)
		{
			return string.Format("\"{0}\"", value.Replace("\"", "\\\""));
		}

		internal static byte[] ReadBytes(this global::System.IO.Stream stream, int length)
		{
			byte[] array = new byte[length];
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			while (length > 0)
			{
				num3 = stream.Read(array, num, length);
				if (num3 <= 0)
				{
					if (num2 >= _retry)
					{
						return array.SubArray(0, num);
					}
					num2++;
				}
				else
				{
					num2 = 0;
					num += num3;
					length -= num3;
				}
			}
			return array;
		}

		internal static byte[] ReadBytes(this global::System.IO.Stream stream, long length, int bufferLength)
		{
			using global::System.IO.MemoryStream memoryStream = new global::System.IO.MemoryStream();
			byte[] buffer = new byte[bufferLength];
			int num = 0;
			int num2 = 0;
			while (length > 0)
			{
				if (length < bufferLength)
				{
					bufferLength = (int)length;
				}
				num2 = stream.Read(buffer, 0, bufferLength);
				if (num2 <= 0)
				{
					if (num >= _retry)
					{
						break;
					}
					num++;
				}
				else
				{
					num = 0;
					memoryStream.Write(buffer, 0, num2);
					length -= num2;
				}
			}
			memoryStream.Close();
			return memoryStream.ToArray();
		}

		internal static void ReadBytesAsync(this global::System.IO.Stream stream, int length, global::System.Action<byte[]> completed, global::System.Action<global::System.Exception> error)
		{
			byte[] buff = new byte[length];
			int offset = 0;
			int retry = 0;
			global::System.AsyncCallback callback = null;
			callback = delegate(global::System.IAsyncResult ar)
			{
				try
				{
					int num = stream.EndRead(ar);
					if (num <= 0)
					{
						if (retry < _retry)
						{
							retry++;
							stream.BeginRead(buff, offset, length, callback, null);
						}
						else if (completed != null)
						{
							completed(buff.SubArray(0, offset));
						}
					}
					else if (num == length)
					{
						if (completed != null)
						{
							completed(buff);
						}
					}
					else
					{
						retry = 0;
						offset += num;
						length -= num;
						stream.BeginRead(buff, offset, length, callback, null);
					}
				}
				catch (global::System.Exception obj2)
				{
					if (error != null)
					{
						error(obj2);
					}
				}
			};
			try
			{
				stream.BeginRead(buff, offset, length, callback, null);
			}
			catch (global::System.Exception obj)
			{
				if (error != null)
				{
					error(obj);
				}
			}
		}

		internal static void ReadBytesAsync(this global::System.IO.Stream stream, long length, int bufferLength, global::System.Action<byte[]> completed, global::System.Action<global::System.Exception> error)
		{
			global::System.IO.MemoryStream dest = new global::System.IO.MemoryStream();
			byte[] buff = new byte[bufferLength];
			int retry = 0;
			global::System.Action<long> read = null;
			read = delegate(long len)
			{
				if (len < bufferLength)
				{
					bufferLength = (int)len;
				}
				stream.BeginRead(buff, 0, bufferLength, delegate(global::System.IAsyncResult ar)
				{
					try
					{
						int num = stream.EndRead(ar);
						if (num <= 0)
						{
							if (retry < _retry)
							{
								int num2 = retry;
								retry = num2 + 1;
								read(len);
							}
							else
							{
								if (completed != null)
								{
									dest.Close();
									completed(dest.ToArray());
								}
								dest.Dispose();
							}
						}
						else
						{
							dest.Write(buff, 0, num);
							if (num == len)
							{
								if (completed != null)
								{
									dest.Close();
									completed(dest.ToArray());
								}
								dest.Dispose();
							}
							else
							{
								retry = 0;
								read(len - num);
							}
						}
					}
					catch (global::System.Exception obj2)
					{
						dest.Dispose();
						if (error != null)
						{
							error(obj2);
						}
					}
				}, null);
			};
			try
			{
				read(length);
			}
			catch (global::System.Exception obj)
			{
				dest.Dispose();
				if (error != null)
				{
					error(obj);
				}
			}
		}

		internal static T[] Reverse<T>(this T[] array)
		{
			int num = array.Length;
			T[] array2 = new T[num];
			int num2 = num - 1;
			for (int i = 0; i <= num2; i++)
			{
				array2[i] = array[num2 - i];
			}
			return array2;
		}

		internal static global::System.Collections.Generic.IEnumerable<string> SplitHeaderValue(this string value, params char[] separators)
		{
			int len = value.Length;
			int end = len - 1;
			global::System.Text.StringBuilder buff = new global::System.Text.StringBuilder(32);
			bool escaped = false;
			bool quoted = false;
			for (int i = 0; i <= end; i++)
			{
				char c = value[i];
				buff.Append(c);
				switch (c)
				{
				case '"':
					if (escaped)
					{
						escaped = false;
					}
					else
					{
						quoted = !quoted;
					}
					continue;
				case '\\':
					if (i == end)
					{
						break;
					}
					if (value[i + 1] == '"')
					{
						escaped = true;
					}
					continue;
				default:
					if (global::System.Array.IndexOf(separators, c) > -1 && !quoted)
					{
						buff.Length--;
						yield return buff.ToString();
						buff.Length = 0;
					}
					continue;
				}
				break;
			}
			yield return buff.ToString();
		}

		internal static byte[] ToByteArray(this global::System.IO.Stream stream)
		{
			using global::System.IO.MemoryStream memoryStream = new global::System.IO.MemoryStream();
			stream.Position = 0L;
			stream.CopyTo(memoryStream, 1024);
			memoryStream.Close();
			return memoryStream.ToArray();
		}

		internal static global::WebSocketSharp.CompressionMethod ToCompressionMethod(this string value)
		{
			global::System.Array values = global::System.Enum.GetValues(typeof(global::WebSocketSharp.CompressionMethod));
			foreach (global::WebSocketSharp.CompressionMethod item in values)
			{
				if (item.ToExtensionString() == value)
				{
					return item;
				}
			}
			return global::WebSocketSharp.CompressionMethod.None;
		}

		internal static string ToExtensionString(this global::WebSocketSharp.CompressionMethod method, params string[] parameters)
		{
			if (method == global::WebSocketSharp.CompressionMethod.None)
			{
				return string.Empty;
			}
			string text = $"permessage-{method.ToString().ToLower()}";
			return (parameters != null && parameters.Length != 0) ? string.Format("{0}; {1}", text, parameters.ToString("; ")) : text;
		}

		internal static global::System.Net.IPAddress ToIPAddress(this string value)
		{
			if (value == null || value.Length == 0)
			{
				return null;
			}
			if (global::System.Net.IPAddress.TryParse(value, out var address))
			{
				return address;
			}
			try
			{
				global::System.Net.IPAddress[] hostAddresses = global::System.Net.Dns.GetHostAddresses(value);
				return hostAddresses[0];
			}
			catch
			{
				return null;
			}
		}

		internal static global::System.Collections.Generic.List<TSource> ToList<TSource>(this global::System.Collections.Generic.IEnumerable<TSource> source)
		{
			return new global::System.Collections.Generic.List<TSource>(source);
		}

		internal static string ToString(this global::System.Net.IPAddress address, bool bracketIPv6)
		{
			return (bracketIPv6 && address.AddressFamily == global::System.Net.Sockets.AddressFamily.InterNetworkV6) ? $"[{address.ToString()}]" : address.ToString();
		}

		internal static ushort ToUInt16(this byte[] source, global::WebSocketSharp.ByteOrder sourceOrder)
		{
			return global::System.BitConverter.ToUInt16(source.ToHostOrder(sourceOrder), 0);
		}

		internal static ulong ToUInt64(this byte[] source, global::WebSocketSharp.ByteOrder sourceOrder)
		{
			return global::System.BitConverter.ToUInt64(source.ToHostOrder(sourceOrder), 0);
		}

		internal static global::System.Collections.Generic.IEnumerable<string> TrimEach(this global::System.Collections.Generic.IEnumerable<string> source)
		{
			foreach (string elm in source)
			{
				yield return elm.Trim();
			}
		}

		internal static string TrimSlashFromEnd(this string value)
		{
			string text = value.TrimEnd(new char[1] { '/' });
			return (text.Length > 0) ? text : "/";
		}

		internal static string TrimSlashOrBackslashFromEnd(this string value)
		{
			string text = value.TrimEnd('/', '\\');
			return (text.Length > 0) ? text : value[0].ToString();
		}

		internal static bool TryCreateVersion(this string versionString, out global::System.Version result)
		{
			result = null;
			try
			{
				result = new global::System.Version(versionString);
			}
			catch
			{
				return false;
			}
			return true;
		}

		internal static bool TryCreateWebSocketUri(this string uriString, out global::System.Uri result, out string message)
		{
			result = null;
			message = null;
			global::System.Uri uri = uriString.ToUri();
			if (uri == null)
			{
				message = "An invalid URI string.";
				return false;
			}
			if (!uri.IsAbsoluteUri)
			{
				message = "A relative URI.";
				return false;
			}
			string scheme = uri.Scheme;
			if (!(scheme == "ws") && !(scheme == "wss"))
			{
				message = "The scheme part is not 'ws' or 'wss'.";
				return false;
			}
			int port = uri.Port;
			if (port == 0)
			{
				message = "The port part is zero.";
				return false;
			}
			if (uri.Fragment.Length > 0)
			{
				message = "It includes the fragment component.";
				return false;
			}
			result = ((port != -1) ? uri : new global::System.Uri(string.Format("{0}://{1}:{2}{3}", scheme, uri.Host, (scheme == "ws") ? 80 : 443, uri.PathAndQuery)));
			return true;
		}

		internal static bool TryGetUTF8DecodedString(this byte[] bytes, out string s)
		{
			s = null;
			try
			{
				s = global::System.Text.Encoding.UTF8.GetString(bytes);
			}
			catch
			{
				return false;
			}
			return true;
		}

		internal static bool TryGetUTF8EncodedBytes(this string s, out byte[] bytes)
		{
			bytes = null;
			try
			{
				bytes = global::System.Text.Encoding.UTF8.GetBytes(s);
			}
			catch
			{
				return false;
			}
			return true;
		}

		internal static bool TryOpenRead(this global::System.IO.FileInfo fileInfo, out global::System.IO.FileStream fileStream)
		{
			fileStream = null;
			try
			{
				fileStream = fileInfo.OpenRead();
			}
			catch
			{
				return false;
			}
			return true;
		}

		internal static string Unquote(this string value)
		{
			int num = value.IndexOf('"');
			if (num == -1)
			{
				return value;
			}
			int num2 = value.LastIndexOf('"');
			if (num2 == num)
			{
				return value;
			}
			int num3 = num2 - num - 1;
			return (num3 > 0) ? value.Substring(num + 1, num3).Replace("\\\"", "\"") : string.Empty;
		}

		internal static bool Upgrades(this global::System.Collections.Specialized.NameValueCollection headers, string protocol)
		{
			global::System.StringComparison comparisonTypeForValue = global::System.StringComparison.OrdinalIgnoreCase;
			return headers.Contains("Upgrade", protocol, comparisonTypeForValue) && headers.Contains("Connection", "Upgrade", comparisonTypeForValue);
		}

		internal static string UrlDecode(this string value, global::System.Text.Encoding encoding)
		{
			return global::WebSocketSharp.Net.HttpUtility.UrlDecode(value, encoding);
		}

		internal static string UrlEncode(this string value, global::System.Text.Encoding encoding)
		{
			return global::WebSocketSharp.Net.HttpUtility.UrlEncode(value, encoding);
		}

		internal static void WriteBytes(this global::System.IO.Stream stream, byte[] bytes, int bufferLength)
		{
			using global::System.IO.MemoryStream memoryStream = new global::System.IO.MemoryStream(bytes);
			memoryStream.CopyTo(stream, bufferLength);
		}

		internal static void WriteBytesAsync(this global::System.IO.Stream stream, byte[] bytes, int bufferLength, global::System.Action completed, global::System.Action<global::System.Exception> error)
		{
			global::System.IO.MemoryStream src = new global::System.IO.MemoryStream(bytes);
			src.CopyToAsync(stream, bufferLength, delegate
			{
				if (completed != null)
				{
					completed();
				}
				src.Dispose();
			}, delegate(global::System.Exception ex)
			{
				src.Dispose();
				if (error != null)
				{
					error(ex);
				}
			});
		}

		public static string GetDescription(this global::WebSocketSharp.Net.HttpStatusCode code)
		{
			return ((int)code).GetStatusDescription();
		}

		public static string GetStatusDescription(this int code)
		{
			return code switch
			{
				100 => "Continue", 
				101 => "Switching Protocols", 
				102 => "Processing", 
				200 => "OK", 
				201 => "Created", 
				202 => "Accepted", 
				203 => "Non-Authoritative Information", 
				204 => "No Content", 
				205 => "Reset Content", 
				206 => "Partial Content", 
				207 => "Multi-Status", 
				300 => "Multiple Choices", 
				301 => "Moved Permanently", 
				302 => "Found", 
				303 => "See Other", 
				304 => "Not Modified", 
				305 => "Use Proxy", 
				307 => "Temporary Redirect", 
				400 => "Bad Request", 
				401 => "Unauthorized", 
				402 => "Payment Required", 
				403 => "Forbidden", 
				404 => "Not Found", 
				405 => "Method Not Allowed", 
				406 => "Not Acceptable", 
				407 => "Proxy Authentication Required", 
				408 => "Request Timeout", 
				409 => "Conflict", 
				410 => "Gone", 
				411 => "Length Required", 
				412 => "Precondition Failed", 
				413 => "Request Entity Too Large", 
				414 => "Request-Uri Too Long", 
				415 => "Unsupported Media Type", 
				416 => "Requested Range Not Satisfiable", 
				417 => "Expectation Failed", 
				422 => "Unprocessable Entity", 
				423 => "Locked", 
				424 => "Failed Dependency", 
				500 => "Internal Server Error", 
				501 => "Not Implemented", 
				502 => "Bad Gateway", 
				503 => "Service Unavailable", 
				504 => "Gateway Timeout", 
				505 => "Http Version Not Supported", 
				507 => "Insufficient Storage", 
				_ => string.Empty, 
			};
		}

		public static bool IsCloseStatusCode(this ushort value)
		{
			return value > 999 && value < 5000;
		}

		public static bool IsEnclosedIn(this string value, char c)
		{
			if (value == null)
			{
				return false;
			}
			int length = value.Length;
			if (length < 2)
			{
				return false;
			}
			return value[0] == c && value[length - 1] == c;
		}

		public static bool IsHostOrder(this global::WebSocketSharp.ByteOrder order)
		{
			return global::System.BitConverter.IsLittleEndian == (order == global::WebSocketSharp.ByteOrder.Little);
		}

		public static bool IsLocal(this global::System.Net.IPAddress address)
		{
			if (address == null)
			{
				throw new global::System.ArgumentNullException("address");
			}
			if (address.Equals(global::System.Net.IPAddress.Any))
			{
				return true;
			}
			if (address.Equals(global::System.Net.IPAddress.Loopback))
			{
				return true;
			}
			if (global::System.Net.Sockets.Socket.OSSupportsIPv6)
			{
				if (address.Equals(global::System.Net.IPAddress.IPv6Any))
				{
					return true;
				}
				if (address.Equals(global::System.Net.IPAddress.IPv6Loopback))
				{
					return true;
				}
			}
			string hostName = global::System.Net.Dns.GetHostName();
			global::System.Net.IPAddress[] hostAddresses = global::System.Net.Dns.GetHostAddresses(hostName);
			global::System.Net.IPAddress[] array = hostAddresses;
			foreach (global::System.Net.IPAddress obj in array)
			{
				if (address.Equals(obj))
				{
					return true;
				}
			}
			return false;
		}

		public static bool IsNullOrEmpty(this string value)
		{
			return value == null || value.Length == 0;
		}

		public static bool IsPredefinedScheme(this string value)
		{
			if (value == null || value.Length < 2)
			{
				return false;
			}
			switch (value[0])
			{
			case 'h':
				return value == "http" || value == "https";
			case 'w':
				return value == "ws" || value == "wss";
			case 'f':
				return value == "file" || value == "ftp";
			case 'g':
				return value == "gopher";
			case 'm':
				return value == "mailto";
			case 'n':
			{
				char c = value[1];
				return (c != 'e') ? (value == "nntp") : (value == "news" || value == "net.pipe" || value == "net.tcp");
			}
			default:
				return false;
			}
		}

		public static bool MaybeUri(this string value)
		{
			if (value == null)
			{
				return false;
			}
			if (value.Length == 0)
			{
				return false;
			}
			int num = value.IndexOf(':');
			if (num == -1)
			{
				return false;
			}
			if (num >= 10)
			{
				return false;
			}
			string value2 = value.Substring(0, num);
			return value2.IsPredefinedScheme();
		}

		public static T[] SubArray<T>(this T[] array, int startIndex, int length)
		{
			if (array == null)
			{
				throw new global::System.ArgumentNullException("array");
			}
			int num = array.Length;
			if (num == 0)
			{
				if (startIndex != 0)
				{
					throw new global::System.ArgumentOutOfRangeException("startIndex");
				}
				if (length != 0)
				{
					throw new global::System.ArgumentOutOfRangeException("length");
				}
				return array;
			}
			if (startIndex < 0 || startIndex >= num)
			{
				throw new global::System.ArgumentOutOfRangeException("startIndex");
			}
			if (length < 0 || length > num - startIndex)
			{
				throw new global::System.ArgumentOutOfRangeException("length");
			}
			if (length == 0)
			{
				return new T[0];
			}
			if (length == num)
			{
				return array;
			}
			T[] array2 = new T[length];
			global::System.Array.Copy(array, startIndex, array2, 0, length);
			return array2;
		}

		public static T[] SubArray<T>(this T[] array, long startIndex, long length)
		{
			if (array == null)
			{
				throw new global::System.ArgumentNullException("array");
			}
			long num = array.LongLength;
			if (num == 0)
			{
				if (startIndex != 0)
				{
					throw new global::System.ArgumentOutOfRangeException("startIndex");
				}
				if (length != 0)
				{
					throw new global::System.ArgumentOutOfRangeException("length");
				}
				return array;
			}
			if (startIndex < 0 || startIndex >= num)
			{
				throw new global::System.ArgumentOutOfRangeException("startIndex");
			}
			if (length < 0 || length > num - startIndex)
			{
				throw new global::System.ArgumentOutOfRangeException("length");
			}
			if (length == 0)
			{
				return new T[0];
			}
			if (length == num)
			{
				return array;
			}
			T[] array2 = new T[length];
			global::System.Array.Copy(array, startIndex, array2, 0L, length);
			return array2;
		}

		public static void Times(this int n, global::System.Action action)
		{
			if (n > 0 && action != null)
			{
				for (int i = 0; i < n; i++)
				{
					action();
				}
			}
		}

		public static void Times(this long n, global::System.Action action)
		{
			if (n > 0 && action != null)
			{
				for (long num = 0L; num < n; num++)
				{
					action();
				}
			}
		}

		public static void Times(this uint n, global::System.Action action)
		{
			if (n != 0 && action != null)
			{
				for (uint num = 0u; num < n; num++)
				{
					action();
				}
			}
		}

		public static void Times(this ulong n, global::System.Action action)
		{
			if (n != 0 && action != null)
			{
				for (ulong num = 0uL; num < n; num++)
				{
					action();
				}
			}
		}

		public static void Times(this int n, global::System.Action<int> action)
		{
			if (n > 0 && action != null)
			{
				for (int i = 0; i < n; i++)
				{
					action(i);
				}
			}
		}

		public static void Times(this long n, global::System.Action<long> action)
		{
			if (n > 0 && action != null)
			{
				for (long num = 0L; num < n; num++)
				{
					action(num);
				}
			}
		}

		public static void Times(this uint n, global::System.Action<uint> action)
		{
			if (n != 0 && action != null)
			{
				for (uint num = 0u; num < n; num++)
				{
					action(num);
				}
			}
		}

		public static void Times(this ulong n, global::System.Action<ulong> action)
		{
			if (n != 0 && action != null)
			{
				for (ulong num = 0uL; num < n; num++)
				{
					action(num);
				}
			}
		}

		[global::System.Obsolete("This method will be removed.")]
		public static T To<T>(this byte[] source, global::WebSocketSharp.ByteOrder sourceOrder) where T : struct
		{
			if (source == null)
			{
				throw new global::System.ArgumentNullException("source");
			}
			if (source.Length == 0)
			{
				return default(T);
			}
			global::System.Type typeFromHandle = typeof(T);
			byte[] value = source.ToHostOrder(sourceOrder);
			return (typeFromHandle == typeof(bool)) ? ((T)(object)global::System.BitConverter.ToBoolean(value, 0)) : ((typeFromHandle == typeof(char)) ? ((T)(object)global::System.BitConverter.ToChar(value, 0)) : ((typeFromHandle == typeof(double)) ? ((T)(object)global::System.BitConverter.ToDouble(value, 0)) : ((typeFromHandle == typeof(short)) ? ((T)(object)global::System.BitConverter.ToInt16(value, 0)) : ((typeFromHandle == typeof(int)) ? ((T)(object)global::System.BitConverter.ToInt32(value, 0)) : ((typeFromHandle == typeof(long)) ? ((T)(object)global::System.BitConverter.ToInt64(value, 0)) : ((typeFromHandle == typeof(float)) ? ((T)(object)global::System.BitConverter.ToSingle(value, 0)) : ((typeFromHandle == typeof(ushort)) ? ((T)(object)global::System.BitConverter.ToUInt16(value, 0)) : ((typeFromHandle == typeof(uint)) ? ((T)(object)global::System.BitConverter.ToUInt32(value, 0)) : ((typeFromHandle == typeof(ulong)) ? ((T)(object)global::System.BitConverter.ToUInt64(value, 0)) : default(T))))))))));
		}

		[global::System.Obsolete("This method will be removed.")]
		public static byte[] ToByteArray<T>(this T value, global::WebSocketSharp.ByteOrder order) where T : struct
		{
			global::System.Type typeFromHandle = typeof(T);
			byte[] array = ((typeFromHandle == typeof(bool)) ? global::System.BitConverter.GetBytes((bool)(object)value) : ((!(typeFromHandle == typeof(byte))) ? ((typeFromHandle == typeof(char)) ? global::System.BitConverter.GetBytes((char)(object)value) : ((typeFromHandle == typeof(double)) ? global::System.BitConverter.GetBytes((double)(object)value) : ((typeFromHandle == typeof(short)) ? global::System.BitConverter.GetBytes((short)(object)value) : ((typeFromHandle == typeof(int)) ? global::System.BitConverter.GetBytes((int)(object)value) : ((typeFromHandle == typeof(long)) ? global::System.BitConverter.GetBytes((long)(object)value) : ((typeFromHandle == typeof(float)) ? global::System.BitConverter.GetBytes((float)(object)value) : ((typeFromHandle == typeof(ushort)) ? global::System.BitConverter.GetBytes((ushort)(object)value) : ((typeFromHandle == typeof(uint)) ? global::System.BitConverter.GetBytes((uint)(object)value) : ((typeFromHandle == typeof(ulong)) ? global::System.BitConverter.GetBytes((ulong)(object)value) : global::WebSocketSharp.WebSocket.EmptyBytes))))))))) : new byte[1] { (byte)(object)value }));
			if (array.Length > 1 && !order.IsHostOrder())
			{
				global::System.Array.Reverse((global::System.Array)array);
			}
			return array;
		}

		public static byte[] ToHostOrder(this byte[] source, global::WebSocketSharp.ByteOrder sourceOrder)
		{
			if (source == null)
			{
				throw new global::System.ArgumentNullException("source");
			}
			if (source.Length < 2)
			{
				return source;
			}
			if (sourceOrder.IsHostOrder())
			{
				return source;
			}
			return source.Reverse();
		}

		public static string ToString<T>(this T[] array, string separator)
		{
			if (array == null)
			{
				throw new global::System.ArgumentNullException("array");
			}
			int num = array.Length;
			if (num == 0)
			{
				return string.Empty;
			}
			if (separator == null)
			{
				separator = string.Empty;
			}
			global::System.Text.StringBuilder stringBuilder = new global::System.Text.StringBuilder(64);
			int num2 = num - 1;
			for (int i = 0; i < num2; i++)
			{
				stringBuilder.AppendFormat("{0}{1}", array[i], separator);
			}
			stringBuilder.Append(array[num2].ToString());
			return stringBuilder.ToString();
		}

		public static global::System.Uri ToUri(this string value)
		{
			global::System.Uri.TryCreate(value, value.MaybeUri() ? global::System.UriKind.Absolute : global::System.UriKind.Relative, out var result);
			return result;
		}

		[global::System.Obsolete("This method will be removed.")]
		public static void WriteContent(this global::WebSocketSharp.Net.HttpListenerResponse response, byte[] content)
		{
			if (response == null)
			{
				throw new global::System.ArgumentNullException("response");
			}
			if (content == null)
			{
				throw new global::System.ArgumentNullException("content");
			}
			long num = content.LongLength;
			if (num == 0)
			{
				response.Close();
				return;
			}
			response.ContentLength64 = num;
			global::System.IO.Stream outputStream = response.OutputStream;
			if (num <= int.MaxValue)
			{
				outputStream.Write(content, 0, (int)num);
			}
			else
			{
				outputStream.WriteBytes(content, 1024);
			}
			outputStream.Close();
		}
	}
}
