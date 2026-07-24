namespace Newtonsoft.Json.Utilities
{
	internal static class JavaScriptUtils
	{
		internal static readonly bool[] SingleQuoteCharEscapeFlags;

		internal static readonly bool[] DoubleQuoteCharEscapeFlags;

		internal static readonly bool[] HtmlCharEscapeFlags;

		private const int UnicodeTextLength = 6;

		private const string EscapedUnicodeText = "!";

		static JavaScriptUtils()
		{
			SingleQuoteCharEscapeFlags = new bool[128];
			DoubleQuoteCharEscapeFlags = new bool[128];
			HtmlCharEscapeFlags = new bool[128];
			global::System.Collections.Generic.IList<char> list = new global::System.Collections.Generic.List<char> { '\n', '\r', '\t', '\\', '\f', '\b' };
			for (int i = 0; i < 32; i++)
			{
				list.Add((char)i);
			}
			foreach (char item in global::System.Linq.Enumerable.Union(list, new char[1] { '\'' }))
			{
				SingleQuoteCharEscapeFlags[(uint)item] = true;
			}
			foreach (char item2 in global::System.Linq.Enumerable.Union(list, new char[1] { '"' }))
			{
				DoubleQuoteCharEscapeFlags[(uint)item2] = true;
			}
			foreach (char item3 in global::System.Linq.Enumerable.Union(list, new char[5] { '"', '\'', '<', '>', '&' }))
			{
				HtmlCharEscapeFlags[(uint)item3] = true;
			}
		}

		public static bool[] GetCharEscapeFlags(global::Newtonsoft.Json.StringEscapeHandling stringEscapeHandling, char quoteChar)
		{
			if (stringEscapeHandling == global::Newtonsoft.Json.StringEscapeHandling.EscapeHtml)
			{
				return HtmlCharEscapeFlags;
			}
			if (quoteChar == '"')
			{
				return DoubleQuoteCharEscapeFlags;
			}
			return SingleQuoteCharEscapeFlags;
		}

		public static bool ShouldEscapeJavaScriptString(string? s, bool[] charEscapeFlags)
		{
			if (s == null)
			{
				return false;
			}
			foreach (char c in s)
			{
				if (c >= charEscapeFlags.Length || charEscapeFlags[(uint)c])
				{
					return true;
				}
			}
			return false;
		}

		public static void WriteEscapedJavaScriptString(global::System.IO.TextWriter writer, string? s, char delimiter, bool appendDelimiters, bool[] charEscapeFlags, global::Newtonsoft.Json.StringEscapeHandling stringEscapeHandling, global::Newtonsoft.Json.IArrayPool<char>? bufferPool, ref char[]? writeBuffer)
		{
			if (appendDelimiters)
			{
				writer.Write(delimiter);
			}
			if (!global::Newtonsoft.Json.Utilities.StringUtils.IsNullOrEmpty(s))
			{
				int num = FirstCharToEscape(s, charEscapeFlags, stringEscapeHandling);
				if (num == -1)
				{
					writer.Write(s);
				}
				else
				{
					if (num != 0)
					{
						if (writeBuffer == null || writeBuffer.Length < num)
						{
							writeBuffer = global::Newtonsoft.Json.Utilities.BufferUtils.EnsureBufferSize(bufferPool, num, writeBuffer);
						}
						s.CopyTo(0, writeBuffer, 0, num);
						writer.Write(writeBuffer, 0, num);
					}
					int num2;
					for (int i = num; i < s.Length; i++)
					{
						char c = s[i];
						if (c < charEscapeFlags.Length && !charEscapeFlags[(uint)c])
						{
							continue;
						}
						string text;
						switch (c)
						{
						case '\t':
							text = "\\t";
							break;
						case '\n':
							text = "\\n";
							break;
						case '\r':
							text = "\\r";
							break;
						case '\f':
							text = "\\f";
							break;
						case '\b':
							text = "\\b";
							break;
						case '\\':
							text = "\\\\";
							break;
						case '\u0085':
							text = "\\u0085";
							break;
						case '\u2028':
							text = "\\u2028";
							break;
						case '\u2029':
							text = "\\u2029";
							break;
						default:
							if (c < charEscapeFlags.Length || stringEscapeHandling == global::Newtonsoft.Json.StringEscapeHandling.EscapeNonAscii)
							{
								if (c == '\'' && stringEscapeHandling != global::Newtonsoft.Json.StringEscapeHandling.EscapeHtml)
								{
									text = "\\'";
									break;
								}
								if (c == '"' && stringEscapeHandling != global::Newtonsoft.Json.StringEscapeHandling.EscapeHtml)
								{
									text = "\\\"";
									break;
								}
								if (writeBuffer == null || writeBuffer.Length < 6)
								{
									writeBuffer = global::Newtonsoft.Json.Utilities.BufferUtils.EnsureBufferSize(bufferPool, 6, writeBuffer);
								}
								global::Newtonsoft.Json.Utilities.StringUtils.ToCharAsUnicode(c, writeBuffer);
								text = "!";
							}
							else
							{
								text = null;
							}
							break;
						}
						if (text == null)
						{
							continue;
						}
						bool flag = string.Equals(text, "!", global::System.StringComparison.Ordinal);
						if (i > num)
						{
							num2 = i - num + (flag ? 6 : 0);
							int num3 = (flag ? 6 : 0);
							if (writeBuffer == null || writeBuffer.Length < num2)
							{
								char[] array = global::Newtonsoft.Json.Utilities.BufferUtils.RentBuffer(bufferPool, num2);
								if (flag)
								{
									global::System.Array.Copy(writeBuffer, array, 6);
								}
								global::Newtonsoft.Json.Utilities.BufferUtils.ReturnBuffer(bufferPool, writeBuffer);
								writeBuffer = array;
							}
							s.CopyTo(num, writeBuffer, num3, num2 - num3);
							writer.Write(writeBuffer, num3, num2 - num3);
						}
						num = i + 1;
						if (!flag)
						{
							writer.Write(text);
						}
						else
						{
							writer.Write(writeBuffer, 0, 6);
						}
					}
					num2 = s.Length - num;
					if (num2 > 0)
					{
						if (writeBuffer == null || writeBuffer.Length < num2)
						{
							writeBuffer = global::Newtonsoft.Json.Utilities.BufferUtils.EnsureBufferSize(bufferPool, num2, writeBuffer);
						}
						s.CopyTo(num, writeBuffer, 0, num2);
						writer.Write(writeBuffer, 0, num2);
					}
				}
			}
			if (appendDelimiters)
			{
				writer.Write(delimiter);
			}
		}

		public static string ToEscapedJavaScriptString(string? value, char delimiter, bool appendDelimiters, global::Newtonsoft.Json.StringEscapeHandling stringEscapeHandling)
		{
			bool[] charEscapeFlags = GetCharEscapeFlags(stringEscapeHandling, delimiter);
			using global::System.IO.StringWriter stringWriter = global::Newtonsoft.Json.Utilities.StringUtils.CreateStringWriter(value?.Length ?? 16);
			char[] writeBuffer = null;
			WriteEscapedJavaScriptString(stringWriter, value, delimiter, appendDelimiters, charEscapeFlags, stringEscapeHandling, null, ref writeBuffer);
			return stringWriter.ToString();
		}

		private static int FirstCharToEscape(string s, bool[] charEscapeFlags, global::Newtonsoft.Json.StringEscapeHandling stringEscapeHandling)
		{
			for (int i = 0; i != s.Length; i++)
			{
				char c = s[i];
				if (c < charEscapeFlags.Length)
				{
					if (charEscapeFlags[(uint)c])
					{
						return i;
					}
					continue;
				}
				if (stringEscapeHandling == global::Newtonsoft.Json.StringEscapeHandling.EscapeNonAscii)
				{
					return i;
				}
				if (c == '\u0085' || c == '\u2028' || c == '\u2029')
				{
					return i;
				}
			}
			return -1;
		}

		public static global::System.Threading.Tasks.Task WriteEscapedJavaScriptStringAsync(global::System.IO.TextWriter writer, string s, char delimiter, bool appendDelimiters, bool[] charEscapeFlags, global::Newtonsoft.Json.StringEscapeHandling stringEscapeHandling, global::Newtonsoft.Json.JsonTextWriter client, char[] writeBuffer, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			if (appendDelimiters)
			{
				return WriteEscapedJavaScriptStringWithDelimitersAsync(writer, s, delimiter, charEscapeFlags, stringEscapeHandling, client, writeBuffer, cancellationToken);
			}
			if (global::Newtonsoft.Json.Utilities.StringUtils.IsNullOrEmpty(s))
			{
				return cancellationToken.CancelIfRequestedAsync() ?? global::Newtonsoft.Json.Utilities.AsyncUtils.CompletedTask;
			}
			return WriteEscapedJavaScriptStringWithoutDelimitersAsync(writer, s, charEscapeFlags, stringEscapeHandling, client, writeBuffer, cancellationToken);
		}

		private static global::System.Threading.Tasks.Task WriteEscapedJavaScriptStringWithDelimitersAsync(global::System.IO.TextWriter writer, string s, char delimiter, bool[] charEscapeFlags, global::Newtonsoft.Json.StringEscapeHandling stringEscapeHandling, global::Newtonsoft.Json.JsonTextWriter client, char[] writeBuffer, global::System.Threading.CancellationToken cancellationToken)
		{
			global::System.Threading.Tasks.Task task = writer.WriteAsync(delimiter, cancellationToken);
			if (!task.IsCompletedSuccessfully())
			{
				return WriteEscapedJavaScriptStringWithDelimitersAsync(task, writer, s, delimiter, charEscapeFlags, stringEscapeHandling, client, writeBuffer, cancellationToken);
			}
			if (!global::Newtonsoft.Json.Utilities.StringUtils.IsNullOrEmpty(s))
			{
				task = WriteEscapedJavaScriptStringWithoutDelimitersAsync(writer, s, charEscapeFlags, stringEscapeHandling, client, writeBuffer, cancellationToken);
				if (task.IsCompletedSuccessfully())
				{
					return writer.WriteAsync(delimiter, cancellationToken);
				}
			}
			return WriteCharAsync(task, writer, delimiter, cancellationToken);
		}

		private static async global::System.Threading.Tasks.Task WriteEscapedJavaScriptStringWithDelimitersAsync(global::System.Threading.Tasks.Task task, global::System.IO.TextWriter writer, string s, char delimiter, bool[] charEscapeFlags, global::Newtonsoft.Json.StringEscapeHandling stringEscapeHandling, global::Newtonsoft.Json.JsonTextWriter client, char[] writeBuffer, global::System.Threading.CancellationToken cancellationToken)
		{
			await task.ConfigureAwait(continueOnCapturedContext: false);
			if (!global::Newtonsoft.Json.Utilities.StringUtils.IsNullOrEmpty(s))
			{
				await WriteEscapedJavaScriptStringWithoutDelimitersAsync(writer, s, charEscapeFlags, stringEscapeHandling, client, writeBuffer, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			}
			await writer.WriteAsync(delimiter).ConfigureAwait(continueOnCapturedContext: false);
		}

		public static async global::System.Threading.Tasks.Task WriteCharAsync(global::System.Threading.Tasks.Task task, global::System.IO.TextWriter writer, char c, global::System.Threading.CancellationToken cancellationToken)
		{
			await task.ConfigureAwait(continueOnCapturedContext: false);
			await writer.WriteAsync(c, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
		}

		private static global::System.Threading.Tasks.Task WriteEscapedJavaScriptStringWithoutDelimitersAsync(global::System.IO.TextWriter writer, string s, bool[] charEscapeFlags, global::Newtonsoft.Json.StringEscapeHandling stringEscapeHandling, global::Newtonsoft.Json.JsonTextWriter client, char[] writeBuffer, global::System.Threading.CancellationToken cancellationToken)
		{
			int num = FirstCharToEscape(s, charEscapeFlags, stringEscapeHandling);
			if (num != -1)
			{
				return WriteDefinitelyEscapedJavaScriptStringWithoutDelimitersAsync(writer, s, num, charEscapeFlags, stringEscapeHandling, client, writeBuffer, cancellationToken);
			}
			return writer.WriteAsync(s, cancellationToken);
		}

		private static async global::System.Threading.Tasks.Task WriteDefinitelyEscapedJavaScriptStringWithoutDelimitersAsync(global::System.IO.TextWriter writer, string s, int lastWritePosition, bool[] charEscapeFlags, global::Newtonsoft.Json.StringEscapeHandling stringEscapeHandling, global::Newtonsoft.Json.JsonTextWriter client, char[] writeBuffer, global::System.Threading.CancellationToken cancellationToken)
		{
			if (writeBuffer == null || writeBuffer.Length < lastWritePosition)
			{
				writeBuffer = client.EnsureWriteBuffer(lastWritePosition, 6);
			}
			if (lastWritePosition != 0)
			{
				s.CopyTo(0, writeBuffer, 0, lastWritePosition);
				await writer.WriteAsync(writeBuffer, 0, lastWritePosition, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			}
			bool isEscapedUnicodeText = false;
			string escapedValue = null;
			int num;
			for (int i = lastWritePosition; i < s.Length; i++)
			{
				char c = s[i];
				if (c < charEscapeFlags.Length && !charEscapeFlags[(uint)c])
				{
					continue;
				}
				switch (c)
				{
				case '\t':
					escapedValue = "\\t";
					break;
				case '\n':
					escapedValue = "\\n";
					break;
				case '\r':
					escapedValue = "\\r";
					break;
				case '\f':
					escapedValue = "\\f";
					break;
				case '\b':
					escapedValue = "\\b";
					break;
				case '\\':
					escapedValue = "\\\\";
					break;
				case '\u0085':
					escapedValue = "\\u0085";
					break;
				case '\u2028':
					escapedValue = "\\u2028";
					break;
				case '\u2029':
					escapedValue = "\\u2029";
					break;
				default:
					if (c >= charEscapeFlags.Length && stringEscapeHandling != global::Newtonsoft.Json.StringEscapeHandling.EscapeNonAscii)
					{
						continue;
					}
					if (c == '\'' && stringEscapeHandling != global::Newtonsoft.Json.StringEscapeHandling.EscapeHtml)
					{
						escapedValue = "\\'";
						break;
					}
					if (c == '"' && stringEscapeHandling != global::Newtonsoft.Json.StringEscapeHandling.EscapeHtml)
					{
						escapedValue = "\\\"";
						break;
					}
					if (writeBuffer.Length < 6)
					{
						writeBuffer = client.EnsureWriteBuffer(6, 0);
					}
					global::Newtonsoft.Json.Utilities.StringUtils.ToCharAsUnicode(c, writeBuffer);
					isEscapedUnicodeText = true;
					break;
				}
				if (i > lastWritePosition)
				{
					num = i - lastWritePosition + (isEscapedUnicodeText ? 6 : 0);
					int num2 = (isEscapedUnicodeText ? 6 : 0);
					if (writeBuffer.Length < num)
					{
						writeBuffer = client.EnsureWriteBuffer(num, 6);
					}
					s.CopyTo(lastWritePosition, writeBuffer, num2, num - num2);
					await writer.WriteAsync(writeBuffer, num2, num - num2, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
				}
				lastWritePosition = i + 1;
				if (!isEscapedUnicodeText)
				{
					await writer.WriteAsync(escapedValue, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
					continue;
				}
				await writer.WriteAsync(writeBuffer, 0, 6, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
				isEscapedUnicodeText = false;
			}
			num = s.Length - lastWritePosition;
			if (num != 0)
			{
				if (writeBuffer.Length < num)
				{
					writeBuffer = client.EnsureWriteBuffer(num, 0);
				}
				s.CopyTo(lastWritePosition, writeBuffer, 0, num);
				await writer.WriteAsync(writeBuffer, 0, num, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			}
		}

		public static bool TryGetDateFromConstructorJson(global::Newtonsoft.Json.JsonReader reader, out global::System.DateTime dateTime, [global::System.Diagnostics.CodeAnalysis.NotNullWhen(false)] out string? errorMessage)
		{
			dateTime = default(global::System.DateTime);
			errorMessage = null;
			if (!TryGetDateConstructorValue(reader, out var integer, out errorMessage) || !integer.HasValue)
			{
				errorMessage = errorMessage ?? "Date constructor has no arguments.";
				return false;
			}
			if (!TryGetDateConstructorValue(reader, out var integer2, out errorMessage))
			{
				return false;
			}
			if (integer2.HasValue)
			{
				global::System.Collections.Generic.List<long> list = new global::System.Collections.Generic.List<long> { integer.Value, integer2.Value };
				while (true)
				{
					if (!TryGetDateConstructorValue(reader, out var integer3, out errorMessage))
					{
						return false;
					}
					if (!integer3.HasValue)
					{
						break;
					}
					list.Add(integer3.Value);
				}
				if (list.Count > 7)
				{
					errorMessage = "Unexpected number of arguments when reading date constructor.";
					return false;
				}
				while (list.Count < 7)
				{
					list.Add(0L);
				}
				dateTime = new global::System.DateTime((int)list[0], (int)list[1] + 1, (int)((list[2] == 0L) ? 1 : list[2]), (int)list[3], (int)list[4], (int)list[5], (int)list[6]);
			}
			else
			{
				dateTime = global::Newtonsoft.Json.Utilities.DateTimeUtils.ConvertJavaScriptTicksToDateTime(integer.Value);
			}
			return true;
		}

		private static bool TryGetDateConstructorValue(global::Newtonsoft.Json.JsonReader reader, out long? integer, [global::System.Diagnostics.CodeAnalysis.NotNullWhen(false)] out string? errorMessage)
		{
			integer = null;
			errorMessage = null;
			if (!reader.Read())
			{
				errorMessage = "Unexpected end when reading date constructor.";
				return false;
			}
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.EndConstructor)
			{
				return true;
			}
			if (reader.TokenType != global::Newtonsoft.Json.JsonToken.Integer)
			{
				errorMessage = "Unexpected token when reading date constructor. Expected Integer, got " + reader.TokenType;
				return false;
			}
			integer = (long)reader.Value;
			return true;
		}
	}
}
