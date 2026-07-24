namespace UnityWebSocketSharp
{
	internal static class Ext
	{
		private static readonly byte[] _last = new byte[1];

		private static readonly int _maxRetry = 5;

		private const string _tspecials = "()<>@,;:\\\"/[]?={} \t";

		private static byte[] compress(this byte[] data)
		{
			if (data.LongLength == 0L)
			{
				return data;
			}
			using global::System.IO.MemoryStream stream = new global::System.IO.MemoryStream(data);
			return stream.compressToArray();
		}

		private static global::System.IO.MemoryStream compress(this global::System.IO.Stream stream)
		{
			global::System.IO.MemoryStream memoryStream = new global::System.IO.MemoryStream();
			if (stream.Length == 0L)
			{
				return memoryStream;
			}
			stream.Position = 0L;
			global::System.IO.Compression.CompressionMode mode = global::System.IO.Compression.CompressionMode.Compress;
			using global::System.IO.Compression.DeflateStream deflateStream = new global::System.IO.Compression.DeflateStream(memoryStream, mode, leaveOpen: true);
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
			if (data.LongLength == 0L)
			{
				return data;
			}
			using global::System.IO.MemoryStream stream = new global::System.IO.MemoryStream(data);
			return stream.decompressToArray();
		}

		private static global::System.IO.MemoryStream decompress(this global::System.IO.Stream stream)
		{
			global::System.IO.MemoryStream memoryStream = new global::System.IO.MemoryStream();
			if (stream.Length == 0L)
			{
				return memoryStream;
			}
			stream.Position = 0L;
			global::System.IO.Compression.CompressionMode mode = global::System.IO.Compression.CompressionMode.Decompress;
			using global::System.IO.Compression.DeflateStream deflateStream = new global::System.IO.Compression.DeflateStream(stream, mode, leaveOpen: true);
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

		private static bool isPredefinedScheme(this string value)
		{
			switch (value[0])
			{
			case 'h':
				if (!(value == "http"))
				{
					return value == "https";
				}
				return true;
			case 'w':
				if (!(value == "ws"))
				{
					return value == "wss";
				}
				return true;
			case 'f':
				if (!(value == "file"))
				{
					return value == "ftp";
				}
				return true;
			case 'g':
				return value == "gopher";
			case 'm':
				return value == "mailto";
			case 'n':
			{
				char c = value[1];
				if (c != 'e')
				{
					return value == "nntp";
				}
				if (!(value == "news") && !(value == "net.pipe"))
				{
					return value == "net.tcp";
				}
				return true;
			}
			default:
				return false;
			}
		}

		internal static byte[] Append(this ushort code, string reason)
		{
			byte[] array = code.ToByteArray(global::UnityWebSocketSharp.ByteOrder.Big);
			if (reason == null || reason.Length == 0)
			{
				return array;
			}
			global::System.Collections.Generic.List<byte> list = new global::System.Collections.Generic.List<byte>(array);
			byte[] bytes = global::System.Text.Encoding.UTF8.GetBytes(reason);
			list.AddRange(bytes);
			return list.ToArray();
		}

		internal static byte[] Compress(this byte[] data, global::UnityWebSocketSharp.CompressionMethod method)
		{
			if (method != global::UnityWebSocketSharp.CompressionMethod.Deflate)
			{
				return data;
			}
			return data.compress();
		}

		internal static global::System.IO.Stream Compress(this global::System.IO.Stream stream, global::UnityWebSocketSharp.CompressionMethod method)
		{
			if (method != global::UnityWebSocketSharp.CompressionMethod.Deflate)
			{
				return stream;
			}
			return stream.compress();
		}

		internal static bool Contains(this string value, params char[] anyOf)
		{
			if (anyOf == null || anyOf.Length == 0)
			{
				return false;
			}
			return value.IndexOfAny(anyOf) > -1;
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
			string[] array = text.Split(',');
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].Trim().Equals(value, comparisonTypeForValue))
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

		internal static T[] Copy<T>(this T[] sourceArray, int length)
		{
			T[] array = new T[length];
			global::System.Array.Copy(sourceArray, 0, array, 0, length);
			return array;
		}

		internal static T[] Copy<T>(this T[] sourceArray, long length)
		{
			T[] array = new T[length];
			global::System.Array.Copy(sourceArray, 0L, array, 0L, length);
			return array;
		}

		internal static void CopyTo(this global::System.IO.Stream sourceStream, global::System.IO.Stream destinationStream, int bufferLength)
		{
			byte[] buffer = new byte[bufferLength];
			while (true)
			{
				int num = sourceStream.Read(buffer, 0, bufferLength);
				if (num > 0)
				{
					destinationStream.Write(buffer, 0, num);
					continue;
				}
				break;
			}
		}

		internal static void CopyToAsync(this global::System.IO.Stream sourceStream, global::System.IO.Stream destinationStream, int bufferLength, global::System.Action completed, global::System.Action<global::System.Exception> error)
		{
			byte[] buff = new byte[bufferLength];
			global::System.AsyncCallback callback = null;
			callback = delegate(global::System.IAsyncResult ar)
			{
				try
				{
					int num = sourceStream.EndRead(ar);
					if (num <= 0)
					{
						if (completed != null)
						{
							completed();
						}
					}
					else
					{
						destinationStream.Write(buff, 0, num);
						sourceStream.BeginRead(buff, 0, bufferLength, callback, null);
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
				sourceStream.BeginRead(buff, 0, bufferLength, callback, null);
			}
			catch (global::System.Exception obj)
			{
				if (error != null)
				{
					error(obj);
				}
			}
		}

		internal static byte[] Decompress(this byte[] data, global::UnityWebSocketSharp.CompressionMethod method)
		{
			if (method != global::UnityWebSocketSharp.CompressionMethod.Deflate)
			{
				return data;
			}
			return data.decompress();
		}

		internal static global::System.IO.Stream Decompress(this global::System.IO.Stream stream, global::UnityWebSocketSharp.CompressionMethod method)
		{
			if (method != global::UnityWebSocketSharp.CompressionMethod.Deflate)
			{
				return stream;
			}
			return stream.decompress();
		}

		internal static byte[] DecompressToArray(this global::System.IO.Stream stream, global::UnityWebSocketSharp.CompressionMethod method)
		{
			if (method != global::UnityWebSocketSharp.CompressionMethod.Deflate)
			{
				return stream.ToByteArray();
			}
			return stream.decompressToArray();
		}

		internal static void Emit(this global::System.EventHandler eventHandler, object sender, global::System.EventArgs e)
		{
			eventHandler?.Invoke(sender, e);
		}

		internal static void Emit<TEventArgs>(this global::System.EventHandler<TEventArgs> eventHandler, object sender, TEventArgs e) where TEventArgs : global::System.EventArgs
		{
			eventHandler?.Invoke(sender, e);
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
			if (num <= 0)
			{
				return originalString;
			}
			return originalString.Substring(0, num);
		}

		internal static global::UnityWebSocketSharp.Net.CookieCollection GetCookies(this global::System.Collections.Specialized.NameValueCollection headers, bool response)
		{
			string text = headers[response ? "Set-Cookie" : "Cookie"];
			if (text == null)
			{
				return new global::UnityWebSocketSharp.Net.CookieCollection();
			}
			return global::UnityWebSocketSharp.Net.CookieCollection.Parse(text, response);
		}

		internal static string GetDnsSafeHost(this global::System.Uri uri, bool bracketIPv6)
		{
			if (!bracketIPv6 || uri.HostNameType != global::System.UriHostNameType.IPv6)
			{
				return uri.DnsSafeHost;
			}
			return uri.Host;
		}

		internal static string GetErrorMessage(this ushort code)
		{
			return code switch
			{
				1002 => "A protocol error has occurred.", 
				1003 => "Unsupported data has been received.", 
				1006 => "An abnormal error has occurred.", 
				1007 => "Invalid data has been received.", 
				1008 => "A policy violation has occurred.", 
				1009 => "A too big message has been received.", 
				1010 => "The client did not receive expected extension(s).", 
				1011 => "The server got an internal error.", 
				1015 => "An error has occurred during a TLS handshake.", 
				_ => string.Empty, 
			};
		}

		internal static string GetErrorMessage(this global::UnityWebSocketSharp.CloseStatusCode code)
		{
			return ((ushort)code).GetErrorMessage();
		}

		internal static string GetName(this string nameAndValue, char separator)
		{
			int num = nameAndValue.IndexOf(separator);
			if (num <= 0)
			{
				return null;
			}
			return nameAndValue.Substring(0, num).Trim();
		}

		internal static string GetUTF8DecodedString(this byte[] bytes)
		{
			try
			{
				return global::System.Text.Encoding.UTF8.GetString(bytes);
			}
			catch
			{
				return null;
			}
		}

		internal static byte[] GetUTF8EncodedBytes(this string s)
		{
			try
			{
				return global::System.Text.Encoding.UTF8.GetBytes(s);
			}
			catch
			{
				return null;
			}
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
			if (!unquote)
			{
				return text;
			}
			return text.Unquote();
		}

		internal static bool IsCompressionExtension(this string value, global::UnityWebSocketSharp.CompressionMethod method)
		{
			string value2 = method.ToExtensionString();
			global::System.StringComparison comparisonType = global::System.StringComparison.Ordinal;
			return value.StartsWith(value2, comparisonType);
		}

		internal static bool IsEqualTo(this int value, char c, global::System.Action<int> beforeComparing)
		{
			beforeComparing(value);
			return value == c;
		}

		internal static bool IsHttpMethod(this string value)
		{
			switch (value)
			{
			default:
				return value == "TRACE";
			case "GET":
			case "HEAD":
			case "POST":
			case "PUT":
			case "DELETE":
			case "CONNECT":
			case "OPTIONS":
				return true;
			}
		}

		internal static bool IsPortNumber(this int value)
		{
			if (value > 0)
			{
				return value < 65536;
			}
			return false;
		}

		internal static bool IsReserved(this global::UnityWebSocketSharp.CloseStatusCode code)
		{
			return ((ushort)code).IsReservedStatusCode();
		}

		internal static bool IsReservedStatusCode(this ushort code)
		{
			if (code != 1004 && code != 1005 && code != 1006)
			{
				return code == 1015;
			}
			return true;
		}

		internal static bool IsSupportedOpcode(this byte opcode)
		{
			return global::System.Enum.IsDefined(typeof(global::UnityWebSocketSharp.Opcode), opcode);
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
			if (!(version < global::UnityWebSocketSharp.Net.HttpVersion.Version11))
			{
				return !headers.Contains("Connection", "close", comparisonTypeForValue);
			}
			return headers.Contains("Connection", "keep-alive", comparisonTypeForValue);
		}

		internal static bool MaybeUri(this string value)
		{
			int num = value.IndexOf(':');
			if (num < 2 || num > 9)
			{
				return false;
			}
			return value.Substring(0, num).isPredefinedScheme();
		}

		internal static string Quote(this string value)
		{
			string arg = value.Replace("\"", "\\\"");
			return $"\"{arg}\"";
		}

		internal static byte[] ReadBytes(this global::System.IO.Stream stream, int length)
		{
			byte[] array = new byte[length];
			int num = 0;
			int num2 = 0;
			while (length > 0)
			{
				int num3 = stream.Read(array, num, length);
				if (num3 <= 0)
				{
					if (num2 >= _maxRetry)
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
			while (length > 0)
			{
				if (length < bufferLength)
				{
					bufferLength = (int)length;
				}
				int num2 = stream.Read(buffer, 0, bufferLength);
				if (num2 <= 0)
				{
					if (num >= _maxRetry)
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
			byte[] ret = new byte[length];
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
						if (retry < _maxRetry)
						{
							retry++;
							stream.BeginRead(ret, offset, length, callback, null);
						}
						else if (completed != null)
						{
							completed(ret.SubArray(0, offset));
						}
					}
					else if (num == length)
					{
						if (completed != null)
						{
							completed(ret);
						}
					}
					else
					{
						retry = 0;
						offset += num;
						length -= num;
						stream.BeginRead(ret, offset, length, callback, null);
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
				stream.BeginRead(ret, offset, length, callback, null);
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
							if (retry < _maxRetry)
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
									byte[] obj2 = dest.ToArray();
									completed(obj2);
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
									byte[] obj3 = dest.ToArray();
									completed(obj3);
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
					catch (global::System.Exception obj4)
					{
						dest.Dispose();
						if (error != null)
						{
							error(obj4);
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
			long num = array.LongLength;
			T[] array2 = new T[num];
			long num2 = num - 1;
			for (long num3 = 0L; num3 <= num2; num3++)
			{
				array2[num3] = array[num2 - num3];
			}
			return array2;
		}

		internal static global::System.Collections.Generic.IEnumerable<string> SplitHeaderValue(this string value, params char[] separators)
		{
			int length = value.Length;
			int end = length - 1;
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
					if (i != end)
					{
						if (value[i + 1] == '"')
						{
							escaped = true;
						}
						continue;
					}
					break;
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
			stream.Position = 0L;
			using global::System.IO.MemoryStream memoryStream = new global::System.IO.MemoryStream();
			stream.CopyTo(memoryStream, 1024);
			memoryStream.Close();
			return memoryStream.ToArray();
		}

		internal static byte[] ToByteArray(this ushort value, global::UnityWebSocketSharp.ByteOrder order)
		{
			byte[] bytes = global::System.BitConverter.GetBytes(value);
			if (!order.IsHostOrder())
			{
				global::System.Array.Reverse(bytes);
			}
			return bytes;
		}

		internal static byte[] ToByteArray(this ulong value, global::UnityWebSocketSharp.ByteOrder order)
		{
			byte[] bytes = global::System.BitConverter.GetBytes(value);
			if (!order.IsHostOrder())
			{
				global::System.Array.Reverse(bytes);
			}
			return bytes;
		}

		internal static global::UnityWebSocketSharp.CompressionMethod ToCompressionMethod(this string value)
		{
			foreach (global::UnityWebSocketSharp.CompressionMethod value2 in global::System.Enum.GetValues(typeof(global::UnityWebSocketSharp.CompressionMethod)))
			{
				if (value2.ToExtensionString() == value)
				{
					return value2;
				}
			}
			return global::UnityWebSocketSharp.CompressionMethod.None;
		}

		internal static string ToExtensionString(this global::UnityWebSocketSharp.CompressionMethod method, params string[] parameters)
		{
			if (method == global::UnityWebSocketSharp.CompressionMethod.None)
			{
				return string.Empty;
			}
			string arg = method.ToString().ToLower();
			string text = $"permessage-{arg}";
			if (parameters == null || parameters.Length == 0)
			{
				return text;
			}
			string arg2 = parameters.ToString("; ");
			return $"{text}; {arg2}";
		}

		internal static int ToInt32(this string numericString)
		{
			return int.Parse(numericString);
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
				return global::System.Net.Dns.GetHostAddresses(value)[0];
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
			if (!bracketIPv6 || address.AddressFamily != global::System.Net.Sockets.AddressFamily.InterNetworkV6)
			{
				return address.ToString();
			}
			return $"[{address}]";
		}

		internal static ushort ToUInt16(this byte[] source, global::UnityWebSocketSharp.ByteOrder sourceOrder)
		{
			return global::System.BitConverter.ToUInt16(source.ToHostOrder(sourceOrder), 0);
		}

		internal static ulong ToUInt64(this byte[] source, global::UnityWebSocketSharp.ByteOrder sourceOrder)
		{
			return global::System.BitConverter.ToUInt64(source.ToHostOrder(sourceOrder), 0);
		}

		internal static global::System.Version ToVersion(this string versionString)
		{
			return new global::System.Version(versionString);
		}

		internal static global::System.Collections.Generic.IEnumerable<string> TrimEach(this global::System.Collections.Generic.IEnumerable<string> source)
		{
			foreach (string item in source)
			{
				yield return item.Trim();
			}
		}

		internal static string TrimSlashFromEnd(this string value)
		{
			string text = value.TrimEnd('/');
			if (text.Length <= 0)
			{
				return "/";
			}
			return text;
		}

		internal static string TrimSlashOrBackslashFromEnd(this string value)
		{
			string text = value.TrimEnd('/', '\\');
			if (text.Length <= 0)
			{
				return value[0].ToString();
			}
			return text;
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
			if (port == -1)
			{
				port = ((scheme == "ws") ? 80 : 443);
				uriString = $"{scheme}://{uri.Host}:{port}{uri.PathAndQuery}";
				result = new global::System.Uri(uriString);
			}
			else
			{
				result = uri;
			}
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
			if (num3 <= 0)
			{
				return string.Empty;
			}
			return value.Substring(num + 1, num3).Replace("\\\"", "\"");
		}

		internal static bool Upgrades(this global::System.Collections.Specialized.NameValueCollection headers, string protocol)
		{
			global::System.StringComparison comparisonTypeForValue = global::System.StringComparison.OrdinalIgnoreCase;
			if (headers.Contains("Upgrade", protocol, comparisonTypeForValue))
			{
				return headers.Contains("Connection", "Upgrade", comparisonTypeForValue);
			}
			return false;
		}

		internal static string UrlDecode(this string value, global::System.Text.Encoding encoding)
		{
			if (value.IndexOfAny(new char[2] { '%', '+' }) <= -1)
			{
				return value;
			}
			return global::UnityWebSocketSharp.Net.HttpUtility.UrlDecode(value, encoding);
		}

		internal static string UrlEncode(this string value, global::System.Text.Encoding encoding)
		{
			return global::UnityWebSocketSharp.Net.HttpUtility.UrlEncode(value, encoding);
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

		public static string GetDescription(this global::UnityWebSocketSharp.Net.HttpStatusCode code)
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
			if (value > 999)
			{
				return value < 5000;
			}
			return false;
		}

		public static bool IsEnclosedIn(this string value, char c)
		{
			if (value == null)
			{
				return false;
			}
			int length = value.Length;
			if (length <= 1)
			{
				return false;
			}
			if (value[0] == c)
			{
				return value[length - 1] == c;
			}
			return false;
		}

		public static bool IsHostOrder(this global::UnityWebSocketSharp.ByteOrder order)
		{
			return global::System.BitConverter.IsLittleEndian == (order == global::UnityWebSocketSharp.ByteOrder.Little);
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
			global::System.Net.IPAddress[] hostAddresses = global::System.Net.Dns.GetHostAddresses(global::System.Net.Dns.GetHostName());
			foreach (global::System.Net.IPAddress obj in hostAddresses)
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
			if (value != null)
			{
				return value.Length == 0;
			}
			return true;
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
			if (num == 0L)
			{
				if (startIndex != 0L)
				{
					throw new global::System.ArgumentOutOfRangeException("startIndex");
				}
				if (length != 0L)
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
			if (length == 0L)
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

		public static byte[] ToHostOrder(this byte[] source, global::UnityWebSocketSharp.ByteOrder sourceOrder)
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
			global::System.Text.StringBuilder stringBuilder = new global::System.Text.StringBuilder(64);
			int num2 = num - 1;
			for (int i = 0; i < num2; i++)
			{
				stringBuilder.AppendFormat("{0}{1}", array[i], separator);
			}
			stringBuilder.AppendFormat("{0}", array[num2]);
			return stringBuilder.ToString();
		}

		public static global::System.Uri ToUri(this string value)
		{
			if (value == null || value.Length == 0)
			{
				return null;
			}
			global::System.UriKind uriKind = (value.MaybeUri() ? global::System.UriKind.Absolute : global::System.UriKind.Relative);
			global::System.Uri.TryCreate(value, uriKind, out var result);
			return result;
		}
	}
}
