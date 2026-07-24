namespace Newtonsoft.Json
{
	public class JsonTextReader : global::Newtonsoft.Json.JsonReader, global::Newtonsoft.Json.IJsonLineInfo
	{
		private readonly bool _safeAsync;

		private const char UnicodeReplacementChar = '\ufffd';

		private const int MaximumJavascriptIntegerCharacterLength = 380;

		private const int LargeBufferLength = 1073741823;

		private readonly global::System.IO.TextReader _reader;

		private char[]? _chars;

		private int _charsUsed;

		private int _charPos;

		private int _lineStartPos;

		private int _lineNumber;

		private bool _isEndOfFile;

		private global::Newtonsoft.Json.Utilities.StringBuffer _stringBuffer;

		private global::Newtonsoft.Json.Utilities.StringReference _stringReference;

		private global::Newtonsoft.Json.IArrayPool<char>? _arrayPool;

		public global::Newtonsoft.Json.JsonNameTable? PropertyNameTable { get; set; }

		public global::Newtonsoft.Json.IArrayPool<char>? ArrayPool
		{
			get
			{
				return _arrayPool;
			}
			set
			{
				if (value == null)
				{
					throw new global::System.ArgumentNullException("value");
				}
				_arrayPool = value;
			}
		}

		public int LineNumber
		{
			get
			{
				if (base.CurrentState == global::Newtonsoft.Json.JsonReader.State.Start && LinePosition == 0 && TokenType != global::Newtonsoft.Json.JsonToken.Comment)
				{
					return 0;
				}
				return _lineNumber;
			}
		}

		public int LinePosition => _charPos - _lineStartPos;

		public override global::System.Threading.Tasks.Task<bool> ReadAsync(global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.ReadAsync(cancellationToken);
			}
			return DoReadAsync(cancellationToken);
		}

		internal global::System.Threading.Tasks.Task<bool> DoReadAsync(global::System.Threading.CancellationToken cancellationToken)
		{
			EnsureBuffer();
			global::System.Threading.Tasks.Task<bool> task;
			do
			{
				switch (_currentState)
				{
				case global::Newtonsoft.Json.JsonReader.State.Start:
				case global::Newtonsoft.Json.JsonReader.State.Property:
				case global::Newtonsoft.Json.JsonReader.State.ArrayStart:
				case global::Newtonsoft.Json.JsonReader.State.Array:
				case global::Newtonsoft.Json.JsonReader.State.ConstructorStart:
				case global::Newtonsoft.Json.JsonReader.State.Constructor:
					return ParseValueAsync(cancellationToken);
				case global::Newtonsoft.Json.JsonReader.State.ObjectStart:
				case global::Newtonsoft.Json.JsonReader.State.Object:
					return ParseObjectAsync(cancellationToken);
				case global::Newtonsoft.Json.JsonReader.State.PostValue:
					task = ParsePostValueAsync(ignoreComments: false, cancellationToken);
					if (!global::Newtonsoft.Json.Utilities.AsyncUtils.IsCompletedSuccessfully(task))
					{
						return DoReadAsync(task, cancellationToken);
					}
					break;
				case global::Newtonsoft.Json.JsonReader.State.Finished:
					return ReadFromFinishedAsync(cancellationToken);
				default:
					throw global::Newtonsoft.Json.JsonReaderException.Create(this, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Unexpected state: {0}.", global::System.Globalization.CultureInfo.InvariantCulture, base.CurrentState));
				}
			}
			while (!task.Result);
			return global::Newtonsoft.Json.Utilities.AsyncUtils.True;
		}

		private async global::System.Threading.Tasks.Task<bool> DoReadAsync(global::System.Threading.Tasks.Task<bool> task, global::System.Threading.CancellationToken cancellationToken)
		{
			if (await task.ConfigureAwait(continueOnCapturedContext: false))
			{
				return true;
			}
			return await DoReadAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
		}

		private async global::System.Threading.Tasks.Task<bool> ParsePostValueAsync(bool ignoreComments, global::System.Threading.CancellationToken cancellationToken)
		{
			while (true)
			{
				char c = _chars[_charPos];
				switch (c)
				{
				case '\0':
					if (_charsUsed == _charPos)
					{
						if (await ReadDataAsync(append: false, cancellationToken).ConfigureAwait(continueOnCapturedContext: false) == 0)
						{
							_currentState = global::Newtonsoft.Json.JsonReader.State.Finished;
							return false;
						}
					}
					else
					{
						_charPos++;
					}
					continue;
				case '}':
					_charPos++;
					SetToken(global::Newtonsoft.Json.JsonToken.EndObject);
					return true;
				case ']':
					_charPos++;
					SetToken(global::Newtonsoft.Json.JsonToken.EndArray);
					return true;
				case ')':
					_charPos++;
					SetToken(global::Newtonsoft.Json.JsonToken.EndConstructor);
					return true;
				case '/':
					await ParseCommentAsync(!ignoreComments, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
					if (!ignoreComments)
					{
						return true;
					}
					continue;
				case ',':
					_charPos++;
					SetStateBasedOnCurrent();
					return false;
				case '\t':
				case ' ':
					_charPos++;
					continue;
				case '\r':
					await ProcessCarriageReturnAsync(append: false, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
					continue;
				case '\n':
					ProcessLineFeed();
					continue;
				}
				if (char.IsWhiteSpace(c))
				{
					_charPos++;
					continue;
				}
				if (base.SupportMultipleContent && Depth == 0)
				{
					SetStateBasedOnCurrent();
					return false;
				}
				throw global::Newtonsoft.Json.JsonReaderException.Create(this, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("After parsing a value an unexpected character was encountered: {0}.", global::System.Globalization.CultureInfo.InvariantCulture, c));
			}
		}

		private async global::System.Threading.Tasks.Task<bool> ReadFromFinishedAsync(global::System.Threading.CancellationToken cancellationToken)
		{
			if (await EnsureCharsAsync(0, append: false, cancellationToken).ConfigureAwait(continueOnCapturedContext: false))
			{
				await EatWhitespaceAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
				if (_isEndOfFile)
				{
					SetToken(global::Newtonsoft.Json.JsonToken.None);
					return false;
				}
				if (_chars[_charPos] == '/')
				{
					await ParseCommentAsync(setToken: true, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
					return true;
				}
				throw global::Newtonsoft.Json.JsonReaderException.Create(this, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Additional text encountered after finished reading JSON content: {0}.", global::System.Globalization.CultureInfo.InvariantCulture, _chars[_charPos]));
			}
			SetToken(global::Newtonsoft.Json.JsonToken.None);
			return false;
		}

		private global::System.Threading.Tasks.Task<int> ReadDataAsync(bool append, global::System.Threading.CancellationToken cancellationToken)
		{
			return ReadDataAsync(append, 0, cancellationToken);
		}

		private async global::System.Threading.Tasks.Task<int> ReadDataAsync(bool append, int charsRequired, global::System.Threading.CancellationToken cancellationToken)
		{
			if (_isEndOfFile)
			{
				return 0;
			}
			PrepareBufferForReadData(append, charsRequired);
			int num = await global::Newtonsoft.Json.Utilities.AsyncUtils.ReadAsync(_reader, _chars, _charsUsed, _chars.Length - _charsUsed - 1, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			_charsUsed += num;
			if (num == 0)
			{
				_isEndOfFile = true;
			}
			_chars[_charsUsed] = '\0';
			return num;
		}

		private async global::System.Threading.Tasks.Task<bool> ParseValueAsync(global::System.Threading.CancellationToken cancellationToken)
		{
			while (true)
			{
				char c = _chars[_charPos];
				switch (c)
				{
				case '\0':
					if (_charsUsed == _charPos)
					{
						if (await ReadDataAsync(append: false, cancellationToken).ConfigureAwait(continueOnCapturedContext: false) == 0)
						{
							return false;
						}
					}
					else
					{
						_charPos++;
					}
					break;
				case '"':
				case '\'':
					await ParseStringAsync(c, global::Newtonsoft.Json.ReadType.Read, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
					return true;
				case 't':
					await ParseTrueAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
					return true;
				case 'f':
					await ParseFalseAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
					return true;
				case 'n':
					if (await EnsureCharsAsync(1, append: true, cancellationToken).ConfigureAwait(continueOnCapturedContext: false))
					{
						switch (_chars[_charPos + 1])
						{
						case 'u':
							await ParseNullAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
							break;
						case 'e':
							await ParseConstructorAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
							break;
						default:
							throw CreateUnexpectedCharacterException(_chars[_charPos]);
						}
						return true;
					}
					_charPos++;
					throw CreateUnexpectedEndException();
				case 'N':
					await ParseNumberNaNAsync(global::Newtonsoft.Json.ReadType.Read, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
					return true;
				case 'I':
					await ParseNumberPositiveInfinityAsync(global::Newtonsoft.Json.ReadType.Read, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
					return true;
				case '-':
					if (!(await EnsureCharsAsync(1, append: true, cancellationToken).ConfigureAwait(continueOnCapturedContext: false)) || _chars[_charPos + 1] != 'I')
					{
						await ParseNumberAsync(global::Newtonsoft.Json.ReadType.Read, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
					}
					else
					{
						await ParseNumberNegativeInfinityAsync(global::Newtonsoft.Json.ReadType.Read, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
					}
					return true;
				case '/':
					await ParseCommentAsync(setToken: true, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
					return true;
				case 'u':
					await ParseUndefinedAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
					return true;
				case '{':
					_charPos++;
					SetToken(global::Newtonsoft.Json.JsonToken.StartObject);
					return true;
				case '[':
					_charPos++;
					SetToken(global::Newtonsoft.Json.JsonToken.StartArray);
					return true;
				case ']':
					_charPos++;
					SetToken(global::Newtonsoft.Json.JsonToken.EndArray);
					return true;
				case ',':
					SetToken(global::Newtonsoft.Json.JsonToken.Undefined);
					return true;
				case ')':
					_charPos++;
					SetToken(global::Newtonsoft.Json.JsonToken.EndConstructor);
					return true;
				case '\r':
					await ProcessCarriageReturnAsync(append: false, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
					break;
				case '\n':
					ProcessLineFeed();
					break;
				case '\t':
				case ' ':
					_charPos++;
					break;
				default:
					if (char.IsWhiteSpace(c))
					{
						_charPos++;
						break;
					}
					if (char.IsNumber(c) || c == '-' || c == '.')
					{
						await ParseNumberAsync(global::Newtonsoft.Json.ReadType.Read, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
						return true;
					}
					throw CreateUnexpectedCharacterException(c);
				}
			}
		}

		private async global::System.Threading.Tasks.Task ReadStringIntoBufferAsync(char quote, global::System.Threading.CancellationToken cancellationToken)
		{
			int charPos = _charPos;
			int initialPosition = _charPos;
			int lastWritePosition = _charPos;
			_stringBuffer.Position = 0;
			while (true)
			{
				switch (_chars[charPos++])
				{
				case '\0':
					if (_charsUsed == charPos - 1)
					{
						charPos--;
						if (await ReadDataAsync(append: true, cancellationToken).ConfigureAwait(continueOnCapturedContext: false) == 0)
						{
							_charPos = charPos;
							throw global::Newtonsoft.Json.JsonReaderException.Create(this, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Unterminated string. Expected delimiter: {0}.", global::System.Globalization.CultureInfo.InvariantCulture, quote));
						}
					}
					break;
				case '\\':
				{
					_charPos = charPos;
					if (!(await EnsureCharsAsync(0, append: true, cancellationToken).ConfigureAwait(continueOnCapturedContext: false)))
					{
						throw global::Newtonsoft.Json.JsonReaderException.Create(this, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Unterminated string. Expected delimiter: {0}.", global::System.Globalization.CultureInfo.InvariantCulture, quote));
					}
					int escapeStartPos = charPos - 1;
					char c = _chars[charPos];
					charPos++;
					char writeChar;
					switch (c)
					{
					case 'b':
						writeChar = '\b';
						break;
					case 't':
						writeChar = '\t';
						break;
					case 'n':
						writeChar = '\n';
						break;
					case 'f':
						writeChar = '\f';
						break;
					case 'r':
						writeChar = '\r';
						break;
					case '\\':
						writeChar = '\\';
						break;
					case '"':
					case '\'':
					case '/':
						writeChar = c;
						break;
					case 'u':
						_charPos = charPos;
						writeChar = await ParseUnicodeAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
						if (global::Newtonsoft.Json.Utilities.StringUtils.IsLowSurrogate(writeChar))
						{
							writeChar = '\ufffd';
						}
						else if (global::Newtonsoft.Json.Utilities.StringUtils.IsHighSurrogate(writeChar))
						{
							bool anotherHighSurrogate;
							do
							{
								anotherHighSurrogate = false;
								if (await EnsureCharsAsync(2, append: true, cancellationToken).ConfigureAwait(continueOnCapturedContext: false) && _chars[_charPos] == '\\' && _chars[_charPos + 1] == 'u')
								{
									char highSurrogate = writeChar;
									_charPos += 2;
									writeChar = await ParseUnicodeAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
									if (!global::Newtonsoft.Json.Utilities.StringUtils.IsLowSurrogate(writeChar))
									{
										if (global::Newtonsoft.Json.Utilities.StringUtils.IsHighSurrogate(writeChar))
										{
											highSurrogate = '\ufffd';
											anotherHighSurrogate = true;
										}
										else
										{
											highSurrogate = '\ufffd';
										}
									}
									EnsureBufferNotEmpty();
									WriteCharToBuffer(highSurrogate, lastWritePosition, escapeStartPos);
									lastWritePosition = _charPos;
								}
								else
								{
									writeChar = '\ufffd';
								}
							}
							while (anotherHighSurrogate);
						}
						charPos = _charPos;
						break;
					default:
						_charPos = charPos;
						throw global::Newtonsoft.Json.JsonReaderException.Create(this, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Bad JSON escape sequence: {0}.", global::System.Globalization.CultureInfo.InvariantCulture, "\\" + c));
					}
					EnsureBufferNotEmpty();
					WriteCharToBuffer(writeChar, lastWritePosition, escapeStartPos);
					lastWritePosition = charPos;
					break;
				}
				case '\r':
					_charPos = charPos - 1;
					await ProcessCarriageReturnAsync(append: true, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
					charPos = _charPos;
					break;
				case '\n':
					_charPos = charPos - 1;
					ProcessLineFeed();
					charPos = _charPos;
					break;
				case '"':
				case '\'':
					if (_chars[charPos - 1] == quote)
					{
						FinishReadStringIntoBuffer(charPos - 1, initialPosition, lastWritePosition);
						return;
					}
					break;
				}
			}
		}

		private global::System.Threading.Tasks.Task ProcessCarriageReturnAsync(bool append, global::System.Threading.CancellationToken cancellationToken)
		{
			_charPos++;
			global::System.Threading.Tasks.Task<bool> task = EnsureCharsAsync(1, append, cancellationToken);
			if (global::Newtonsoft.Json.Utilities.AsyncUtils.IsCompletedSuccessfully(task))
			{
				SetNewLine(task.Result);
				return global::Newtonsoft.Json.Utilities.AsyncUtils.CompletedTask;
			}
			return ProcessCarriageReturnAsync(task);
		}

		private async global::System.Threading.Tasks.Task ProcessCarriageReturnAsync(global::System.Threading.Tasks.Task<bool> task)
		{
			SetNewLine(await task.ConfigureAwait(continueOnCapturedContext: false));
		}

		private async global::System.Threading.Tasks.Task<char> ParseUnicodeAsync(global::System.Threading.CancellationToken cancellationToken)
		{
			return ConvertUnicode(await EnsureCharsAsync(4, append: true, cancellationToken).ConfigureAwait(continueOnCapturedContext: false));
		}

		private global::System.Threading.Tasks.Task<bool> EnsureCharsAsync(int relativePosition, bool append, global::System.Threading.CancellationToken cancellationToken)
		{
			if (_charPos + relativePosition < _charsUsed)
			{
				return global::Newtonsoft.Json.Utilities.AsyncUtils.True;
			}
			if (_isEndOfFile)
			{
				return global::Newtonsoft.Json.Utilities.AsyncUtils.False;
			}
			return ReadCharsAsync(relativePosition, append, cancellationToken);
		}

		private async global::System.Threading.Tasks.Task<bool> ReadCharsAsync(int relativePosition, bool append, global::System.Threading.CancellationToken cancellationToken)
		{
			int charsRequired = _charPos + relativePosition - _charsUsed + 1;
			do
			{
				int num = await ReadDataAsync(append, charsRequired, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
				if (num == 0)
				{
					return false;
				}
				charsRequired -= num;
			}
			while (charsRequired > 0);
			return true;
		}

		private async global::System.Threading.Tasks.Task<bool> ParseObjectAsync(global::System.Threading.CancellationToken cancellationToken)
		{
			while (true)
			{
				char c = _chars[_charPos];
				switch (c)
				{
				case '\0':
					if (_charsUsed == _charPos)
					{
						if (await ReadDataAsync(append: false, cancellationToken).ConfigureAwait(continueOnCapturedContext: false) == 0)
						{
							return false;
						}
					}
					else
					{
						_charPos++;
					}
					break;
				case '}':
					SetToken(global::Newtonsoft.Json.JsonToken.EndObject);
					_charPos++;
					return true;
				case '/':
					await ParseCommentAsync(setToken: true, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
					return true;
				case '\r':
					await ProcessCarriageReturnAsync(append: false, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
					break;
				case '\n':
					ProcessLineFeed();
					break;
				case '\t':
				case ' ':
					_charPos++;
					break;
				default:
					if (char.IsWhiteSpace(c))
					{
						_charPos++;
						break;
					}
					return await ParsePropertyAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
				}
			}
		}

		private async global::System.Threading.Tasks.Task ParseCommentAsync(bool setToken, global::System.Threading.CancellationToken cancellationToken)
		{
			_charPos++;
			if (!(await EnsureCharsAsync(1, append: false, cancellationToken).ConfigureAwait(continueOnCapturedContext: false)))
			{
				throw global::Newtonsoft.Json.JsonReaderException.Create(this, "Unexpected end while parsing comment.");
			}
			bool singlelineComment;
			if (_chars[_charPos] == '*')
			{
				singlelineComment = false;
			}
			else
			{
				if (_chars[_charPos] != '/')
				{
					throw global::Newtonsoft.Json.JsonReaderException.Create(this, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Error parsing comment. Expected: *, got {0}.", global::System.Globalization.CultureInfo.InvariantCulture, _chars[_charPos]));
				}
				singlelineComment = true;
			}
			_charPos++;
			int initialPosition = _charPos;
			while (true)
			{
				switch (_chars[_charPos])
				{
				case '\0':
					if (_charsUsed == _charPos)
					{
						if (await ReadDataAsync(append: true, cancellationToken).ConfigureAwait(continueOnCapturedContext: false) == 0)
						{
							if (!singlelineComment)
							{
								throw global::Newtonsoft.Json.JsonReaderException.Create(this, "Unexpected end while parsing comment.");
							}
							EndComment(setToken, initialPosition, _charPos);
							return;
						}
					}
					else
					{
						_charPos++;
					}
					break;
				case '*':
					_charPos++;
					if (!singlelineComment && await EnsureCharsAsync(0, append: true, cancellationToken).ConfigureAwait(continueOnCapturedContext: false) && _chars[_charPos] == '/')
					{
						EndComment(setToken, initialPosition, _charPos - 1);
						_charPos++;
						return;
					}
					break;
				case '\r':
					if (singlelineComment)
					{
						EndComment(setToken, initialPosition, _charPos);
						return;
					}
					await ProcessCarriageReturnAsync(append: true, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
					break;
				case '\n':
					if (singlelineComment)
					{
						EndComment(setToken, initialPosition, _charPos);
						return;
					}
					ProcessLineFeed();
					break;
				default:
					_charPos++;
					break;
				}
			}
		}

		private async global::System.Threading.Tasks.Task EatWhitespaceAsync(global::System.Threading.CancellationToken cancellationToken)
		{
			while (true)
			{
				char c = _chars[_charPos];
				switch (c)
				{
				case '\0':
					if (_charsUsed == _charPos)
					{
						if (await ReadDataAsync(append: false, cancellationToken).ConfigureAwait(continueOnCapturedContext: false) == 0)
						{
							return;
						}
					}
					else
					{
						_charPos++;
					}
					break;
				case '\r':
					await ProcessCarriageReturnAsync(append: false, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
					break;
				case '\n':
					ProcessLineFeed();
					break;
				default:
					if (!char.IsWhiteSpace(c))
					{
						return;
					}
					goto case ' ';
				case ' ':
					_charPos++;
					break;
				}
			}
		}

		private async global::System.Threading.Tasks.Task ParseStringAsync(char quote, global::Newtonsoft.Json.ReadType readType, global::System.Threading.CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			_charPos++;
			ShiftBufferIfNeeded();
			await ReadStringIntoBufferAsync(quote, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			ParseReadString(quote, readType);
		}

		private async global::System.Threading.Tasks.Task<bool> MatchValueAsync(string value, global::System.Threading.CancellationToken cancellationToken)
		{
			return MatchValue(await EnsureCharsAsync(value.Length - 1, append: true, cancellationToken).ConfigureAwait(continueOnCapturedContext: false), value);
		}

		private async global::System.Threading.Tasks.Task<bool> MatchValueWithTrailingSeparatorAsync(string value, global::System.Threading.CancellationToken cancellationToken)
		{
			if (!(await MatchValueAsync(value, cancellationToken).ConfigureAwait(continueOnCapturedContext: false)))
			{
				return false;
			}
			if (!(await EnsureCharsAsync(0, append: false, cancellationToken).ConfigureAwait(continueOnCapturedContext: false)))
			{
				return true;
			}
			return IsSeparator(_chars[_charPos]) || _chars[_charPos] == '\0';
		}

		private async global::System.Threading.Tasks.Task MatchAndSetAsync(string value, global::Newtonsoft.Json.JsonToken newToken, object? tokenValue, global::System.Threading.CancellationToken cancellationToken)
		{
			if (await MatchValueWithTrailingSeparatorAsync(value, cancellationToken).ConfigureAwait(continueOnCapturedContext: false))
			{
				SetToken(newToken, tokenValue);
				return;
			}
			throw global::Newtonsoft.Json.JsonReaderException.Create(this, "Error parsing " + newToken.ToString().ToLowerInvariant() + " value.");
		}

		private global::System.Threading.Tasks.Task ParseTrueAsync(global::System.Threading.CancellationToken cancellationToken)
		{
			return MatchAndSetAsync(global::Newtonsoft.Json.JsonConvert.True, global::Newtonsoft.Json.JsonToken.Boolean, true, cancellationToken);
		}

		private global::System.Threading.Tasks.Task ParseFalseAsync(global::System.Threading.CancellationToken cancellationToken)
		{
			return MatchAndSetAsync(global::Newtonsoft.Json.JsonConvert.False, global::Newtonsoft.Json.JsonToken.Boolean, false, cancellationToken);
		}

		private global::System.Threading.Tasks.Task ParseNullAsync(global::System.Threading.CancellationToken cancellationToken)
		{
			return MatchAndSetAsync(global::Newtonsoft.Json.JsonConvert.Null, global::Newtonsoft.Json.JsonToken.Null, null, cancellationToken);
		}

		private async global::System.Threading.Tasks.Task ParseConstructorAsync(global::System.Threading.CancellationToken cancellationToken)
		{
			if (await MatchValueWithTrailingSeparatorAsync("new", cancellationToken).ConfigureAwait(continueOnCapturedContext: false))
			{
				await EatWhitespaceAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
				int initialPosition = _charPos;
				int endPosition;
				while (true)
				{
					char c = _chars[_charPos];
					if (c == '\0')
					{
						if (_charsUsed == _charPos)
						{
							if (await ReadDataAsync(append: true, cancellationToken).ConfigureAwait(continueOnCapturedContext: false) == 0)
							{
								throw global::Newtonsoft.Json.JsonReaderException.Create(this, "Unexpected end while parsing constructor.");
							}
							continue;
						}
						endPosition = _charPos;
						_charPos++;
						break;
					}
					if (char.IsLetterOrDigit(c))
					{
						_charPos++;
						continue;
					}
					switch (c)
					{
					case '\r':
						endPosition = _charPos;
						await ProcessCarriageReturnAsync(append: true, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
						break;
					case '\n':
						endPosition = _charPos;
						ProcessLineFeed();
						break;
					default:
						if (char.IsWhiteSpace(c))
						{
							endPosition = _charPos;
							_charPos++;
							break;
						}
						if (c == '(')
						{
							endPosition = _charPos;
							break;
						}
						throw global::Newtonsoft.Json.JsonReaderException.Create(this, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Unexpected character while parsing constructor: {0}.", global::System.Globalization.CultureInfo.InvariantCulture, c));
					}
					break;
				}
				_stringReference = new global::Newtonsoft.Json.Utilities.StringReference(_chars, initialPosition, endPosition - initialPosition);
				string constructorName = _stringReference.ToString();
				await EatWhitespaceAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
				if (_chars[_charPos] != '(')
				{
					throw global::Newtonsoft.Json.JsonReaderException.Create(this, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Unexpected character while parsing constructor: {0}.", global::System.Globalization.CultureInfo.InvariantCulture, _chars[_charPos]));
				}
				_charPos++;
				ClearRecentString();
				SetToken(global::Newtonsoft.Json.JsonToken.StartConstructor, constructorName);
				return;
			}
			throw global::Newtonsoft.Json.JsonReaderException.Create(this, "Unexpected content while parsing JSON.");
		}

		private async global::System.Threading.Tasks.Task<object> ParseNumberNaNAsync(global::Newtonsoft.Json.ReadType readType, global::System.Threading.CancellationToken cancellationToken)
		{
			return ParseNumberNaN(readType, await MatchValueWithTrailingSeparatorAsync(global::Newtonsoft.Json.JsonConvert.NaN, cancellationToken).ConfigureAwait(continueOnCapturedContext: false));
		}

		private async global::System.Threading.Tasks.Task<object> ParseNumberPositiveInfinityAsync(global::Newtonsoft.Json.ReadType readType, global::System.Threading.CancellationToken cancellationToken)
		{
			return ParseNumberPositiveInfinity(readType, await MatchValueWithTrailingSeparatorAsync(global::Newtonsoft.Json.JsonConvert.PositiveInfinity, cancellationToken).ConfigureAwait(continueOnCapturedContext: false));
		}

		private async global::System.Threading.Tasks.Task<object> ParseNumberNegativeInfinityAsync(global::Newtonsoft.Json.ReadType readType, global::System.Threading.CancellationToken cancellationToken)
		{
			return ParseNumberNegativeInfinity(readType, await MatchValueWithTrailingSeparatorAsync(global::Newtonsoft.Json.JsonConvert.NegativeInfinity, cancellationToken).ConfigureAwait(continueOnCapturedContext: false));
		}

		private async global::System.Threading.Tasks.Task ParseNumberAsync(global::Newtonsoft.Json.ReadType readType, global::System.Threading.CancellationToken cancellationToken)
		{
			ShiftBufferIfNeeded();
			char firstChar = _chars[_charPos];
			int initialPosition = _charPos;
			await ReadNumberIntoBufferAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			ParseReadNumber(readType, firstChar, initialPosition);
		}

		private global::System.Threading.Tasks.Task ParseUndefinedAsync(global::System.Threading.CancellationToken cancellationToken)
		{
			return MatchAndSetAsync(global::Newtonsoft.Json.JsonConvert.Undefined, global::Newtonsoft.Json.JsonToken.Undefined, null, cancellationToken);
		}

		private async global::System.Threading.Tasks.Task<bool> ParsePropertyAsync(global::System.Threading.CancellationToken cancellationToken)
		{
			char c = _chars[_charPos];
			char quoteChar;
			if (c == '"' || c == '\'')
			{
				_charPos++;
				quoteChar = c;
				ShiftBufferIfNeeded();
				await ReadStringIntoBufferAsync(quoteChar, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			}
			else
			{
				if (!ValidIdentifierChar(c))
				{
					throw global::Newtonsoft.Json.JsonReaderException.Create(this, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Invalid property identifier character: {0}.", global::System.Globalization.CultureInfo.InvariantCulture, _chars[_charPos]));
				}
				quoteChar = '\0';
				ShiftBufferIfNeeded();
				await ParseUnquotedPropertyAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			}
			string propertyName = ((PropertyNameTable == null) ? _stringReference.ToString() : (PropertyNameTable.Get(_stringReference.Chars, _stringReference.StartIndex, _stringReference.Length) ?? _stringReference.ToString()));
			await EatWhitespaceAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			if (_chars[_charPos] != ':')
			{
				throw global::Newtonsoft.Json.JsonReaderException.Create(this, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Invalid character after parsing property name. Expected ':' but got: {0}.", global::System.Globalization.CultureInfo.InvariantCulture, _chars[_charPos]));
			}
			_charPos++;
			SetToken(global::Newtonsoft.Json.JsonToken.PropertyName, propertyName);
			_quoteChar = quoteChar;
			ClearRecentString();
			return true;
		}

		private async global::System.Threading.Tasks.Task ReadNumberIntoBufferAsync(global::System.Threading.CancellationToken cancellationToken)
		{
			int charPos = _charPos;
			while (true)
			{
				char c = _chars[charPos];
				if (c == '\0')
				{
					_charPos = charPos;
					if (_charsUsed != charPos || await ReadDataAsync(append: true, cancellationToken).ConfigureAwait(continueOnCapturedContext: false) == 0)
					{
						break;
					}
				}
				else
				{
					if (ReadNumberCharIntoBuffer(c, charPos))
					{
						break;
					}
					charPos++;
				}
			}
		}

		private async global::System.Threading.Tasks.Task ParseUnquotedPropertyAsync(global::System.Threading.CancellationToken cancellationToken)
		{
			int initialPosition = _charPos;
			while (true)
			{
				char c = _chars[_charPos];
				if (c == '\0')
				{
					if (_charsUsed != _charPos)
					{
						_stringReference = new global::Newtonsoft.Json.Utilities.StringReference(_chars, initialPosition, _charPos - initialPosition);
						break;
					}
					if (await ReadDataAsync(append: true, cancellationToken).ConfigureAwait(continueOnCapturedContext: false) == 0)
					{
						throw global::Newtonsoft.Json.JsonReaderException.Create(this, "Unexpected end while parsing unquoted property name.");
					}
				}
				else if (ReadUnquotedPropertyReportIfDone(c, initialPosition))
				{
					break;
				}
			}
		}

		private async global::System.Threading.Tasks.Task<bool> ReadNullCharAsync(global::System.Threading.CancellationToken cancellationToken)
		{
			if (_charsUsed == _charPos)
			{
				if (await ReadDataAsync(append: false, cancellationToken).ConfigureAwait(continueOnCapturedContext: false) == 0)
				{
					_isEndOfFile = true;
					return true;
				}
			}
			else
			{
				_charPos++;
			}
			return false;
		}

		private async global::System.Threading.Tasks.Task HandleNullAsync(global::System.Threading.CancellationToken cancellationToken)
		{
			if (await EnsureCharsAsync(1, append: true, cancellationToken).ConfigureAwait(continueOnCapturedContext: false))
			{
				if (_chars[_charPos + 1] == 'u')
				{
					await ParseNullAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
					return;
				}
				_charPos += 2;
				throw CreateUnexpectedCharacterException(_chars[_charPos - 1]);
			}
			_charPos = _charsUsed;
			throw CreateUnexpectedEndException();
		}

		private async global::System.Threading.Tasks.Task ReadFinishedAsync(global::System.Threading.CancellationToken cancellationToken)
		{
			if (await EnsureCharsAsync(0, append: false, cancellationToken).ConfigureAwait(continueOnCapturedContext: false))
			{
				await EatWhitespaceAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
				if (_isEndOfFile)
				{
					SetToken(global::Newtonsoft.Json.JsonToken.None);
					return;
				}
				if (_chars[_charPos] != '/')
				{
					throw global::Newtonsoft.Json.JsonReaderException.Create(this, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Additional text encountered after finished reading JSON content: {0}.", global::System.Globalization.CultureInfo.InvariantCulture, _chars[_charPos]));
				}
				await ParseCommentAsync(setToken: false, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			}
			SetToken(global::Newtonsoft.Json.JsonToken.None);
		}

		private async global::System.Threading.Tasks.Task<object?> ReadStringValueAsync(global::Newtonsoft.Json.ReadType readType, global::System.Threading.CancellationToken cancellationToken)
		{
			EnsureBuffer();
			switch (_currentState)
			{
			case global::Newtonsoft.Json.JsonReader.State.PostValue:
				if (await ParsePostValueAsync(ignoreComments: true, cancellationToken).ConfigureAwait(continueOnCapturedContext: false))
				{
					return null;
				}
				goto case global::Newtonsoft.Json.JsonReader.State.Start;
			case global::Newtonsoft.Json.JsonReader.State.Start:
			case global::Newtonsoft.Json.JsonReader.State.Property:
			case global::Newtonsoft.Json.JsonReader.State.ArrayStart:
			case global::Newtonsoft.Json.JsonReader.State.Array:
			case global::Newtonsoft.Json.JsonReader.State.ConstructorStart:
			case global::Newtonsoft.Json.JsonReader.State.Constructor:
				while (true)
				{
					char c = _chars[_charPos];
					switch (c)
					{
					case '\0':
						if (await ReadNullCharAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false))
						{
							SetToken(global::Newtonsoft.Json.JsonToken.None, null, updateIndex: false);
							return null;
						}
						break;
					case '"':
					case '\'':
						await ParseStringAsync(c, readType, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
						return FinishReadQuotedStringValue(readType);
					case '-':
						if (await EnsureCharsAsync(1, append: true, cancellationToken).ConfigureAwait(continueOnCapturedContext: false) && _chars[_charPos + 1] == 'I')
						{
							return ParseNumberNegativeInfinity(readType);
						}
						await ParseNumberAsync(readType, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
						return Value;
					case '.':
					case '0':
					case '1':
					case '2':
					case '3':
					case '4':
					case '5':
					case '6':
					case '7':
					case '8':
					case '9':
						if (readType != global::Newtonsoft.Json.ReadType.ReadAsString)
						{
							_charPos++;
							throw CreateUnexpectedCharacterException(c);
						}
						await ParseNumberAsync(global::Newtonsoft.Json.ReadType.ReadAsString, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
						return Value;
					case 'f':
					case 't':
					{
						if (readType != global::Newtonsoft.Json.ReadType.ReadAsString)
						{
							_charPos++;
							throw CreateUnexpectedCharacterException(c);
						}
						string expected = ((c == 't') ? global::Newtonsoft.Json.JsonConvert.True : global::Newtonsoft.Json.JsonConvert.False);
						if (!(await MatchValueWithTrailingSeparatorAsync(expected, cancellationToken).ConfigureAwait(continueOnCapturedContext: false)))
						{
							throw CreateUnexpectedCharacterException(_chars[_charPos]);
						}
						SetToken(global::Newtonsoft.Json.JsonToken.String, expected);
						return expected;
					}
					case 'I':
						return await ParseNumberPositiveInfinityAsync(readType, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
					case 'N':
						return await ParseNumberNaNAsync(readType, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
					case 'n':
						await HandleNullAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
						return null;
					case '/':
						await ParseCommentAsync(setToken: false, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
						break;
					case ',':
						ProcessValueComma();
						break;
					case ']':
						_charPos++;
						if (_currentState == global::Newtonsoft.Json.JsonReader.State.Array || _currentState == global::Newtonsoft.Json.JsonReader.State.ArrayStart || _currentState == global::Newtonsoft.Json.JsonReader.State.PostValue)
						{
							SetToken(global::Newtonsoft.Json.JsonToken.EndArray);
							return null;
						}
						throw CreateUnexpectedCharacterException(c);
					case '\r':
						await ProcessCarriageReturnAsync(append: false, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
						break;
					case '\n':
						ProcessLineFeed();
						break;
					case '\t':
					case ' ':
						_charPos++;
						break;
					default:
						_charPos++;
						if (!char.IsWhiteSpace(c))
						{
							throw CreateUnexpectedCharacterException(c);
						}
						break;
					}
				}
			case global::Newtonsoft.Json.JsonReader.State.Finished:
				await ReadFinishedAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
				return null;
			default:
				throw global::Newtonsoft.Json.JsonReaderException.Create(this, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Unexpected state: {0}.", global::System.Globalization.CultureInfo.InvariantCulture, base.CurrentState));
			}
		}

		private async global::System.Threading.Tasks.Task<object?> ReadNumberValueAsync(global::Newtonsoft.Json.ReadType readType, global::System.Threading.CancellationToken cancellationToken)
		{
			EnsureBuffer();
			switch (_currentState)
			{
			case global::Newtonsoft.Json.JsonReader.State.PostValue:
				if (await ParsePostValueAsync(ignoreComments: true, cancellationToken).ConfigureAwait(continueOnCapturedContext: false))
				{
					return null;
				}
				goto case global::Newtonsoft.Json.JsonReader.State.Start;
			case global::Newtonsoft.Json.JsonReader.State.Start:
			case global::Newtonsoft.Json.JsonReader.State.Property:
			case global::Newtonsoft.Json.JsonReader.State.ArrayStart:
			case global::Newtonsoft.Json.JsonReader.State.Array:
			case global::Newtonsoft.Json.JsonReader.State.ConstructorStart:
			case global::Newtonsoft.Json.JsonReader.State.Constructor:
				while (true)
				{
					char c = _chars[_charPos];
					switch (c)
					{
					case '\0':
						if (await ReadNullCharAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false))
						{
							SetToken(global::Newtonsoft.Json.JsonToken.None, null, updateIndex: false);
							return null;
						}
						break;
					case '"':
					case '\'':
						await ParseStringAsync(c, readType, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
						return FinishReadQuotedNumber(readType);
					case 'n':
						await HandleNullAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
						return null;
					case 'N':
						return await ParseNumberNaNAsync(readType, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
					case 'I':
						return await ParseNumberPositiveInfinityAsync(readType, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
					case '-':
						if (await EnsureCharsAsync(1, append: true, cancellationToken).ConfigureAwait(continueOnCapturedContext: false) && _chars[_charPos + 1] == 'I')
						{
							return await ParseNumberNegativeInfinityAsync(readType, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
						}
						await ParseNumberAsync(readType, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
						return Value;
					case '.':
					case '0':
					case '1':
					case '2':
					case '3':
					case '4':
					case '5':
					case '6':
					case '7':
					case '8':
					case '9':
						await ParseNumberAsync(readType, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
						return Value;
					case '/':
						await ParseCommentAsync(setToken: false, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
						break;
					case ',':
						ProcessValueComma();
						break;
					case ']':
						_charPos++;
						if (_currentState == global::Newtonsoft.Json.JsonReader.State.Array || _currentState == global::Newtonsoft.Json.JsonReader.State.ArrayStart || _currentState == global::Newtonsoft.Json.JsonReader.State.PostValue)
						{
							SetToken(global::Newtonsoft.Json.JsonToken.EndArray);
							return null;
						}
						throw CreateUnexpectedCharacterException(c);
					case '\r':
						await ProcessCarriageReturnAsync(append: false, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
						break;
					case '\n':
						ProcessLineFeed();
						break;
					case '\t':
					case ' ':
						_charPos++;
						break;
					default:
						_charPos++;
						if (!char.IsWhiteSpace(c))
						{
							throw CreateUnexpectedCharacterException(c);
						}
						break;
					}
				}
			case global::Newtonsoft.Json.JsonReader.State.Finished:
				await ReadFinishedAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
				return null;
			default:
				throw global::Newtonsoft.Json.JsonReaderException.Create(this, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Unexpected state: {0}.", global::System.Globalization.CultureInfo.InvariantCulture, base.CurrentState));
			}
		}

		public override global::System.Threading.Tasks.Task<bool?> ReadAsBooleanAsync(global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.ReadAsBooleanAsync(cancellationToken);
			}
			return DoReadAsBooleanAsync(cancellationToken);
		}

		internal async global::System.Threading.Tasks.Task<bool?> DoReadAsBooleanAsync(global::System.Threading.CancellationToken cancellationToken)
		{
			EnsureBuffer();
			switch (_currentState)
			{
			case global::Newtonsoft.Json.JsonReader.State.PostValue:
				if (await ParsePostValueAsync(ignoreComments: true, cancellationToken).ConfigureAwait(continueOnCapturedContext: false))
				{
					return null;
				}
				goto case global::Newtonsoft.Json.JsonReader.State.Start;
			case global::Newtonsoft.Json.JsonReader.State.Start:
			case global::Newtonsoft.Json.JsonReader.State.Property:
			case global::Newtonsoft.Json.JsonReader.State.ArrayStart:
			case global::Newtonsoft.Json.JsonReader.State.Array:
			case global::Newtonsoft.Json.JsonReader.State.ConstructorStart:
			case global::Newtonsoft.Json.JsonReader.State.Constructor:
				while (true)
				{
					char c = _chars[_charPos];
					switch (c)
					{
					case '\0':
						if (await ReadNullCharAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false))
						{
							SetToken(global::Newtonsoft.Json.JsonToken.None, null, updateIndex: false);
							return null;
						}
						break;
					case '"':
					case '\'':
						await ParseStringAsync(c, global::Newtonsoft.Json.ReadType.Read, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
						return ReadBooleanString(_stringReference.ToString());
					case 'n':
						await HandleNullAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
						return null;
					case '-':
					case '.':
					case '0':
					case '1':
					case '2':
					case '3':
					case '4':
					case '5':
					case '6':
					case '7':
					case '8':
					case '9':
					{
						await ParseNumberAsync(global::Newtonsoft.Json.ReadType.Read, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
						bool flag = ((!(Value is global::System.Numerics.BigInteger i)) ? global::System.Convert.ToBoolean(Value, global::System.Globalization.CultureInfo.InvariantCulture) : (i != 0L));
						SetToken(global::Newtonsoft.Json.JsonToken.Boolean, flag, updateIndex: false);
						return flag;
					}
					case 'f':
					case 't':
					{
						bool isTrue = c == 't';
						if (!(await MatchValueWithTrailingSeparatorAsync(isTrue ? global::Newtonsoft.Json.JsonConvert.True : global::Newtonsoft.Json.JsonConvert.False, cancellationToken).ConfigureAwait(continueOnCapturedContext: false)))
						{
							throw CreateUnexpectedCharacterException(_chars[_charPos]);
						}
						SetToken(global::Newtonsoft.Json.JsonToken.Boolean, isTrue);
						return isTrue;
					}
					case '/':
						await ParseCommentAsync(setToken: false, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
						break;
					case ',':
						ProcessValueComma();
						break;
					case ']':
						_charPos++;
						if (_currentState == global::Newtonsoft.Json.JsonReader.State.Array || _currentState == global::Newtonsoft.Json.JsonReader.State.ArrayStart || _currentState == global::Newtonsoft.Json.JsonReader.State.PostValue)
						{
							SetToken(global::Newtonsoft.Json.JsonToken.EndArray);
							return null;
						}
						throw CreateUnexpectedCharacterException(c);
					case '\r':
						await ProcessCarriageReturnAsync(append: false, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
						break;
					case '\n':
						ProcessLineFeed();
						break;
					case '\t':
					case ' ':
						_charPos++;
						break;
					default:
						_charPos++;
						if (!char.IsWhiteSpace(c))
						{
							throw CreateUnexpectedCharacterException(c);
						}
						break;
					}
				}
			case global::Newtonsoft.Json.JsonReader.State.Finished:
				await ReadFinishedAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
				return null;
			default:
				throw global::Newtonsoft.Json.JsonReaderException.Create(this, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Unexpected state: {0}.", global::System.Globalization.CultureInfo.InvariantCulture, base.CurrentState));
			}
		}

		public override global::System.Threading.Tasks.Task<byte[]?> ReadAsBytesAsync(global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.ReadAsBytesAsync(cancellationToken);
			}
			return DoReadAsBytesAsync(cancellationToken);
		}

		internal async global::System.Threading.Tasks.Task<byte[]?> DoReadAsBytesAsync(global::System.Threading.CancellationToken cancellationToken)
		{
			EnsureBuffer();
			bool isWrapped = false;
			switch (_currentState)
			{
			case global::Newtonsoft.Json.JsonReader.State.PostValue:
				if (await ParsePostValueAsync(ignoreComments: true, cancellationToken).ConfigureAwait(continueOnCapturedContext: false))
				{
					return null;
				}
				goto case global::Newtonsoft.Json.JsonReader.State.Start;
			case global::Newtonsoft.Json.JsonReader.State.Start:
			case global::Newtonsoft.Json.JsonReader.State.Property:
			case global::Newtonsoft.Json.JsonReader.State.ArrayStart:
			case global::Newtonsoft.Json.JsonReader.State.Array:
			case global::Newtonsoft.Json.JsonReader.State.ConstructorStart:
			case global::Newtonsoft.Json.JsonReader.State.Constructor:
				while (true)
				{
					char c = _chars[_charPos];
					switch (c)
					{
					case '\0':
						if (await ReadNullCharAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false))
						{
							SetToken(global::Newtonsoft.Json.JsonToken.None, null, updateIndex: false);
							return null;
						}
						break;
					case '"':
					case '\'':
					{
						await ParseStringAsync(c, global::Newtonsoft.Json.ReadType.ReadAsBytes, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
						byte[] data = (byte[])Value;
						if (isWrapped)
						{
							await ReaderReadAndAssertAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
							if (TokenType != global::Newtonsoft.Json.JsonToken.EndObject)
							{
								throw global::Newtonsoft.Json.JsonReaderException.Create(this, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Error reading bytes. Unexpected token: {0}.", global::System.Globalization.CultureInfo.InvariantCulture, TokenType));
							}
							SetToken(global::Newtonsoft.Json.JsonToken.Bytes, data, updateIndex: false);
						}
						return data;
					}
					case '{':
						_charPos++;
						SetToken(global::Newtonsoft.Json.JsonToken.StartObject);
						await ReadIntoWrappedTypeObjectAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
						isWrapped = true;
						break;
					case '[':
						_charPos++;
						SetToken(global::Newtonsoft.Json.JsonToken.StartArray);
						return await ReadArrayIntoByteArrayAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
					case 'n':
						await HandleNullAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
						return null;
					case '/':
						await ParseCommentAsync(setToken: false, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
						break;
					case ',':
						ProcessValueComma();
						break;
					case ']':
						_charPos++;
						if (_currentState == global::Newtonsoft.Json.JsonReader.State.Array || _currentState == global::Newtonsoft.Json.JsonReader.State.ArrayStart || _currentState == global::Newtonsoft.Json.JsonReader.State.PostValue)
						{
							SetToken(global::Newtonsoft.Json.JsonToken.EndArray);
							return null;
						}
						throw CreateUnexpectedCharacterException(c);
					case '\r':
						await ProcessCarriageReturnAsync(append: false, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
						break;
					case '\n':
						ProcessLineFeed();
						break;
					case '\t':
					case ' ':
						_charPos++;
						break;
					default:
						_charPos++;
						if (!char.IsWhiteSpace(c))
						{
							throw CreateUnexpectedCharacterException(c);
						}
						break;
					}
				}
			case global::Newtonsoft.Json.JsonReader.State.Finished:
				await ReadFinishedAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
				return null;
			default:
				throw global::Newtonsoft.Json.JsonReaderException.Create(this, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Unexpected state: {0}.", global::System.Globalization.CultureInfo.InvariantCulture, base.CurrentState));
			}
		}

		private async global::System.Threading.Tasks.Task ReadIntoWrappedTypeObjectAsync(global::System.Threading.CancellationToken cancellationToken)
		{
			await ReaderReadAndAssertAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			if (Value != null && Value.ToString() == "$type")
			{
				await ReaderReadAndAssertAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
				if (Value != null && Value.ToString().StartsWith("System.Byte[]", global::System.StringComparison.Ordinal))
				{
					await ReaderReadAndAssertAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
					if (Value.ToString() == "$value")
					{
						return;
					}
				}
			}
			throw global::Newtonsoft.Json.JsonReaderException.Create(this, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Error reading bytes. Unexpected token: {0}.", global::System.Globalization.CultureInfo.InvariantCulture, global::Newtonsoft.Json.JsonToken.StartObject));
		}

		public override global::System.Threading.Tasks.Task<global::System.DateTime?> ReadAsDateTimeAsync(global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.ReadAsDateTimeAsync(cancellationToken);
			}
			return DoReadAsDateTimeAsync(cancellationToken);
		}

		internal async global::System.Threading.Tasks.Task<global::System.DateTime?> DoReadAsDateTimeAsync(global::System.Threading.CancellationToken cancellationToken)
		{
			return (global::System.DateTime?)(await ReadStringValueAsync(global::Newtonsoft.Json.ReadType.ReadAsDateTime, cancellationToken).ConfigureAwait(continueOnCapturedContext: false));
		}

		public override global::System.Threading.Tasks.Task<global::System.DateTimeOffset?> ReadAsDateTimeOffsetAsync(global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.ReadAsDateTimeOffsetAsync(cancellationToken);
			}
			return DoReadAsDateTimeOffsetAsync(cancellationToken);
		}

		internal async global::System.Threading.Tasks.Task<global::System.DateTimeOffset?> DoReadAsDateTimeOffsetAsync(global::System.Threading.CancellationToken cancellationToken)
		{
			return (global::System.DateTimeOffset?)(await ReadStringValueAsync(global::Newtonsoft.Json.ReadType.ReadAsDateTimeOffset, cancellationToken).ConfigureAwait(continueOnCapturedContext: false));
		}

		public override global::System.Threading.Tasks.Task<decimal?> ReadAsDecimalAsync(global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.ReadAsDecimalAsync(cancellationToken);
			}
			return DoReadAsDecimalAsync(cancellationToken);
		}

		internal async global::System.Threading.Tasks.Task<decimal?> DoReadAsDecimalAsync(global::System.Threading.CancellationToken cancellationToken)
		{
			return (decimal?)(await ReadNumberValueAsync(global::Newtonsoft.Json.ReadType.ReadAsDecimal, cancellationToken).ConfigureAwait(continueOnCapturedContext: false));
		}

		public override global::System.Threading.Tasks.Task<double?> ReadAsDoubleAsync(global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.ReadAsDoubleAsync(cancellationToken);
			}
			return DoReadAsDoubleAsync(cancellationToken);
		}

		internal async global::System.Threading.Tasks.Task<double?> DoReadAsDoubleAsync(global::System.Threading.CancellationToken cancellationToken)
		{
			return (double?)(await ReadNumberValueAsync(global::Newtonsoft.Json.ReadType.ReadAsDouble, cancellationToken).ConfigureAwait(continueOnCapturedContext: false));
		}

		public override global::System.Threading.Tasks.Task<int?> ReadAsInt32Async(global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.ReadAsInt32Async(cancellationToken);
			}
			return DoReadAsInt32Async(cancellationToken);
		}

		internal async global::System.Threading.Tasks.Task<int?> DoReadAsInt32Async(global::System.Threading.CancellationToken cancellationToken)
		{
			return (int?)(await ReadNumberValueAsync(global::Newtonsoft.Json.ReadType.ReadAsInt32, cancellationToken).ConfigureAwait(continueOnCapturedContext: false));
		}

		public override global::System.Threading.Tasks.Task<string?> ReadAsStringAsync(global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.ReadAsStringAsync(cancellationToken);
			}
			return DoReadAsStringAsync(cancellationToken);
		}

		internal async global::System.Threading.Tasks.Task<string?> DoReadAsStringAsync(global::System.Threading.CancellationToken cancellationToken)
		{
			return (string)(await ReadStringValueAsync(global::Newtonsoft.Json.ReadType.ReadAsString, cancellationToken).ConfigureAwait(continueOnCapturedContext: false));
		}

		public JsonTextReader(global::System.IO.TextReader reader)
		{
			if (reader == null)
			{
				throw new global::System.ArgumentNullException("reader");
			}
			_reader = reader;
			_lineNumber = 1;
			_safeAsync = GetType() == typeof(global::Newtonsoft.Json.JsonTextReader);
		}

		private void EnsureBufferNotEmpty()
		{
			if (_stringBuffer.IsEmpty)
			{
				_stringBuffer = new global::Newtonsoft.Json.Utilities.StringBuffer(_arrayPool, 1024);
			}
		}

		private void SetNewLine(bool hasNextChar)
		{
			if (hasNextChar && _chars[_charPos] == '\n')
			{
				_charPos++;
			}
			OnNewLine(_charPos);
		}

		private void OnNewLine(int pos)
		{
			_lineNumber++;
			_lineStartPos = pos;
		}

		private void ParseString(char quote, global::Newtonsoft.Json.ReadType readType)
		{
			_charPos++;
			ShiftBufferIfNeeded();
			ReadStringIntoBuffer(quote);
			ParseReadString(quote, readType);
		}

		private void ParseReadString(char quote, global::Newtonsoft.Json.ReadType readType)
		{
			SetPostValueState(updateIndex: true);
			switch (readType)
			{
			case global::Newtonsoft.Json.ReadType.ReadAsBytes:
			{
				global::System.Guid g;
				byte[] value2 = ((_stringReference.Length == 0) ? global::Newtonsoft.Json.Utilities.CollectionUtils.ArrayEmpty<byte>() : ((_stringReference.Length != 36 || !global::Newtonsoft.Json.Utilities.ConvertUtils.TryConvertGuid(_stringReference.ToString(), out g)) ? global::System.Convert.FromBase64CharArray(_stringReference.Chars, _stringReference.StartIndex, _stringReference.Length) : g.ToByteArray()));
				SetToken(global::Newtonsoft.Json.JsonToken.Bytes, value2, updateIndex: false);
				return;
			}
			case global::Newtonsoft.Json.ReadType.ReadAsString:
			{
				string value = _stringReference.ToString();
				SetToken(global::Newtonsoft.Json.JsonToken.String, value, updateIndex: false);
				_quoteChar = quote;
				return;
			}
			case global::Newtonsoft.Json.ReadType.ReadAsInt32:
			case global::Newtonsoft.Json.ReadType.ReadAsDecimal:
			case global::Newtonsoft.Json.ReadType.ReadAsBoolean:
				return;
			}
			if (_dateParseHandling != global::Newtonsoft.Json.DateParseHandling.None)
			{
				global::System.DateTimeOffset dt2;
				if (readType switch
				{
					global::Newtonsoft.Json.ReadType.ReadAsDateTime => 1, 
					global::Newtonsoft.Json.ReadType.ReadAsDateTimeOffset => 2, 
					_ => (int)_dateParseHandling, 
				} == 1)
				{
					if (global::Newtonsoft.Json.Utilities.DateTimeUtils.TryParseDateTime(_stringReference, base.DateTimeZoneHandling, base.DateFormatString, base.Culture, out var dt))
					{
						SetToken(global::Newtonsoft.Json.JsonToken.Date, dt, updateIndex: false);
						return;
					}
				}
				else if (global::Newtonsoft.Json.Utilities.DateTimeUtils.TryParseDateTimeOffset(_stringReference, base.DateFormatString, base.Culture, out dt2))
				{
					SetToken(global::Newtonsoft.Json.JsonToken.Date, dt2, updateIndex: false);
					return;
				}
			}
			SetToken(global::Newtonsoft.Json.JsonToken.String, _stringReference.ToString(), updateIndex: false);
			_quoteChar = quote;
		}

		private static void BlockCopyChars(char[] src, int srcOffset, char[] dst, int dstOffset, int count)
		{
			global::System.Buffer.BlockCopy(src, srcOffset * 2, dst, dstOffset * 2, count * 2);
		}

		private void ShiftBufferIfNeeded()
		{
			int num = _chars.Length;
			if ((double)(num - _charPos) <= (double)num * 0.1 || num >= 1073741823)
			{
				int num2 = _charsUsed - _charPos;
				if (num2 > 0)
				{
					BlockCopyChars(_chars, _charPos, _chars, 0, num2);
				}
				_lineStartPos -= _charPos;
				_charPos = 0;
				_charsUsed = num2;
				_chars[_charsUsed] = '\0';
			}
		}

		private int ReadData(bool append)
		{
			return ReadData(append, 0);
		}

		private void PrepareBufferForReadData(bool append, int charsRequired)
		{
			if (_charsUsed + charsRequired < _chars.Length - 1)
			{
				return;
			}
			if (append)
			{
				int num = _chars.Length * 2;
				int minSize = global::System.Math.Max((num < 0) ? int.MaxValue : num, _charsUsed + charsRequired + 1);
				char[] array = global::Newtonsoft.Json.Utilities.BufferUtils.RentBuffer(_arrayPool, minSize);
				BlockCopyChars(_chars, 0, array, 0, _chars.Length);
				global::Newtonsoft.Json.Utilities.BufferUtils.ReturnBuffer(_arrayPool, _chars);
				_chars = array;
				return;
			}
			int num2 = _charsUsed - _charPos;
			if (num2 + charsRequired + 1 >= _chars.Length)
			{
				char[] array2 = global::Newtonsoft.Json.Utilities.BufferUtils.RentBuffer(_arrayPool, num2 + charsRequired + 1);
				if (num2 > 0)
				{
					BlockCopyChars(_chars, _charPos, array2, 0, num2);
				}
				global::Newtonsoft.Json.Utilities.BufferUtils.ReturnBuffer(_arrayPool, _chars);
				_chars = array2;
			}
			else if (num2 > 0)
			{
				BlockCopyChars(_chars, _charPos, _chars, 0, num2);
			}
			_lineStartPos -= _charPos;
			_charPos = 0;
			_charsUsed = num2;
		}

		private int ReadData(bool append, int charsRequired)
		{
			if (_isEndOfFile)
			{
				return 0;
			}
			PrepareBufferForReadData(append, charsRequired);
			int count = _chars.Length - _charsUsed - 1;
			int num = _reader.Read(_chars, _charsUsed, count);
			_charsUsed += num;
			if (num == 0)
			{
				_isEndOfFile = true;
			}
			_chars[_charsUsed] = '\0';
			return num;
		}

		private bool EnsureChars(int relativePosition, bool append)
		{
			if (_charPos + relativePosition >= _charsUsed)
			{
				return ReadChars(relativePosition, append);
			}
			return true;
		}

		private bool ReadChars(int relativePosition, bool append)
		{
			if (_isEndOfFile)
			{
				return false;
			}
			int num = _charPos + relativePosition - _charsUsed + 1;
			int num2 = 0;
			do
			{
				int num3 = ReadData(append, num - num2);
				if (num3 == 0)
				{
					break;
				}
				num2 += num3;
			}
			while (num2 < num);
			if (num2 < num)
			{
				return false;
			}
			return true;
		}

		public override bool Read()
		{
			EnsureBuffer();
			do
			{
				switch (_currentState)
				{
				case global::Newtonsoft.Json.JsonReader.State.Start:
				case global::Newtonsoft.Json.JsonReader.State.Property:
				case global::Newtonsoft.Json.JsonReader.State.ArrayStart:
				case global::Newtonsoft.Json.JsonReader.State.Array:
				case global::Newtonsoft.Json.JsonReader.State.ConstructorStart:
				case global::Newtonsoft.Json.JsonReader.State.Constructor:
					return ParseValue();
				case global::Newtonsoft.Json.JsonReader.State.ObjectStart:
				case global::Newtonsoft.Json.JsonReader.State.Object:
					return ParseObject();
				case global::Newtonsoft.Json.JsonReader.State.PostValue:
					break;
				case global::Newtonsoft.Json.JsonReader.State.Finished:
					if (EnsureChars(0, append: false))
					{
						EatWhitespace();
						if (_isEndOfFile)
						{
							SetToken(global::Newtonsoft.Json.JsonToken.None);
							return false;
						}
						if (_chars[_charPos] == '/')
						{
							ParseComment(setToken: true);
							return true;
						}
						throw global::Newtonsoft.Json.JsonReaderException.Create(this, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Additional text encountered after finished reading JSON content: {0}.", global::System.Globalization.CultureInfo.InvariantCulture, _chars[_charPos]));
					}
					SetToken(global::Newtonsoft.Json.JsonToken.None);
					return false;
				default:
					throw global::Newtonsoft.Json.JsonReaderException.Create(this, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Unexpected state: {0}.", global::System.Globalization.CultureInfo.InvariantCulture, base.CurrentState));
				}
			}
			while (!ParsePostValue(ignoreComments: false));
			return true;
		}

		public override int? ReadAsInt32()
		{
			return (int?)ReadNumberValue(global::Newtonsoft.Json.ReadType.ReadAsInt32);
		}

		public override global::System.DateTime? ReadAsDateTime()
		{
			return (global::System.DateTime?)ReadStringValue(global::Newtonsoft.Json.ReadType.ReadAsDateTime);
		}

		public override string? ReadAsString()
		{
			return (string)ReadStringValue(global::Newtonsoft.Json.ReadType.ReadAsString);
		}

		public override byte[]? ReadAsBytes()
		{
			EnsureBuffer();
			bool flag = false;
			switch (_currentState)
			{
			case global::Newtonsoft.Json.JsonReader.State.PostValue:
				if (ParsePostValue(ignoreComments: true))
				{
					return null;
				}
				goto case global::Newtonsoft.Json.JsonReader.State.Start;
			case global::Newtonsoft.Json.JsonReader.State.Start:
			case global::Newtonsoft.Json.JsonReader.State.Property:
			case global::Newtonsoft.Json.JsonReader.State.ArrayStart:
			case global::Newtonsoft.Json.JsonReader.State.Array:
			case global::Newtonsoft.Json.JsonReader.State.ConstructorStart:
			case global::Newtonsoft.Json.JsonReader.State.Constructor:
				while (true)
				{
					char c = _chars[_charPos];
					switch (c)
					{
					case '\0':
						if (ReadNullChar())
						{
							SetToken(global::Newtonsoft.Json.JsonToken.None, null, updateIndex: false);
							return null;
						}
						break;
					case '"':
					case '\'':
					{
						ParseString(c, global::Newtonsoft.Json.ReadType.ReadAsBytes);
						byte[] array = (byte[])Value;
						if (flag)
						{
							ReaderReadAndAssert();
							if (TokenType != global::Newtonsoft.Json.JsonToken.EndObject)
							{
								throw global::Newtonsoft.Json.JsonReaderException.Create(this, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Error reading bytes. Unexpected token: {0}.", global::System.Globalization.CultureInfo.InvariantCulture, TokenType));
							}
							SetToken(global::Newtonsoft.Json.JsonToken.Bytes, array, updateIndex: false);
						}
						return array;
					}
					case '{':
						_charPos++;
						SetToken(global::Newtonsoft.Json.JsonToken.StartObject);
						ReadIntoWrappedTypeObject();
						flag = true;
						break;
					case '[':
						_charPos++;
						SetToken(global::Newtonsoft.Json.JsonToken.StartArray);
						return ReadArrayIntoByteArray();
					case 'n':
						HandleNull();
						return null;
					case '/':
						ParseComment(setToken: false);
						break;
					case ',':
						ProcessValueComma();
						break;
					case ']':
						_charPos++;
						if (_currentState == global::Newtonsoft.Json.JsonReader.State.Array || _currentState == global::Newtonsoft.Json.JsonReader.State.ArrayStart || _currentState == global::Newtonsoft.Json.JsonReader.State.PostValue)
						{
							SetToken(global::Newtonsoft.Json.JsonToken.EndArray);
							return null;
						}
						throw CreateUnexpectedCharacterException(c);
					case '\r':
						ProcessCarriageReturn(append: false);
						break;
					case '\n':
						ProcessLineFeed();
						break;
					case '\t':
					case ' ':
						_charPos++;
						break;
					default:
						_charPos++;
						if (!char.IsWhiteSpace(c))
						{
							throw CreateUnexpectedCharacterException(c);
						}
						break;
					}
				}
			case global::Newtonsoft.Json.JsonReader.State.Finished:
				ReadFinished();
				return null;
			default:
				throw global::Newtonsoft.Json.JsonReaderException.Create(this, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Unexpected state: {0}.", global::System.Globalization.CultureInfo.InvariantCulture, base.CurrentState));
			}
		}

		private object? ReadStringValue(global::Newtonsoft.Json.ReadType readType)
		{
			EnsureBuffer();
			switch (_currentState)
			{
			case global::Newtonsoft.Json.JsonReader.State.PostValue:
				if (ParsePostValue(ignoreComments: true))
				{
					return null;
				}
				goto case global::Newtonsoft.Json.JsonReader.State.Start;
			case global::Newtonsoft.Json.JsonReader.State.Start:
			case global::Newtonsoft.Json.JsonReader.State.Property:
			case global::Newtonsoft.Json.JsonReader.State.ArrayStart:
			case global::Newtonsoft.Json.JsonReader.State.Array:
			case global::Newtonsoft.Json.JsonReader.State.ConstructorStart:
			case global::Newtonsoft.Json.JsonReader.State.Constructor:
				while (true)
				{
					char c = _chars[_charPos];
					switch (c)
					{
					case '\0':
						if (ReadNullChar())
						{
							SetToken(global::Newtonsoft.Json.JsonToken.None, null, updateIndex: false);
							return null;
						}
						break;
					case '"':
					case '\'':
						ParseString(c, readType);
						return FinishReadQuotedStringValue(readType);
					case '-':
						if (EnsureChars(1, append: true) && _chars[_charPos + 1] == 'I')
						{
							return ParseNumberNegativeInfinity(readType);
						}
						ParseNumber(readType);
						return Value;
					case '.':
					case '0':
					case '1':
					case '2':
					case '3':
					case '4':
					case '5':
					case '6':
					case '7':
					case '8':
					case '9':
						if (readType != global::Newtonsoft.Json.ReadType.ReadAsString)
						{
							_charPos++;
							throw CreateUnexpectedCharacterException(c);
						}
						ParseNumber(global::Newtonsoft.Json.ReadType.ReadAsString);
						return Value;
					case 'f':
					case 't':
					{
						if (readType != global::Newtonsoft.Json.ReadType.ReadAsString)
						{
							_charPos++;
							throw CreateUnexpectedCharacterException(c);
						}
						string text = ((c == 't') ? global::Newtonsoft.Json.JsonConvert.True : global::Newtonsoft.Json.JsonConvert.False);
						if (!MatchValueWithTrailingSeparator(text))
						{
							throw CreateUnexpectedCharacterException(_chars[_charPos]);
						}
						SetToken(global::Newtonsoft.Json.JsonToken.String, text);
						return text;
					}
					case 'I':
						return ParseNumberPositiveInfinity(readType);
					case 'N':
						return ParseNumberNaN(readType);
					case 'n':
						HandleNull();
						return null;
					case '/':
						ParseComment(setToken: false);
						break;
					case ',':
						ProcessValueComma();
						break;
					case ']':
						_charPos++;
						if (_currentState == global::Newtonsoft.Json.JsonReader.State.Array || _currentState == global::Newtonsoft.Json.JsonReader.State.ArrayStart || _currentState == global::Newtonsoft.Json.JsonReader.State.PostValue)
						{
							SetToken(global::Newtonsoft.Json.JsonToken.EndArray);
							return null;
						}
						throw CreateUnexpectedCharacterException(c);
					case '\r':
						ProcessCarriageReturn(append: false);
						break;
					case '\n':
						ProcessLineFeed();
						break;
					case '\t':
					case ' ':
						_charPos++;
						break;
					default:
						_charPos++;
						if (!char.IsWhiteSpace(c))
						{
							throw CreateUnexpectedCharacterException(c);
						}
						break;
					}
				}
			case global::Newtonsoft.Json.JsonReader.State.Finished:
				ReadFinished();
				return null;
			default:
				throw global::Newtonsoft.Json.JsonReaderException.Create(this, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Unexpected state: {0}.", global::System.Globalization.CultureInfo.InvariantCulture, base.CurrentState));
			}
		}

		private object? FinishReadQuotedStringValue(global::Newtonsoft.Json.ReadType readType)
		{
			switch (readType)
			{
			case global::Newtonsoft.Json.ReadType.ReadAsBytes:
			case global::Newtonsoft.Json.ReadType.ReadAsString:
				return Value;
			case global::Newtonsoft.Json.ReadType.ReadAsDateTime:
				if (Value is global::System.DateTime dateTime)
				{
					return dateTime;
				}
				return ReadDateTimeString((string)Value);
			case global::Newtonsoft.Json.ReadType.ReadAsDateTimeOffset:
				if (Value is global::System.DateTimeOffset dateTimeOffset)
				{
					return dateTimeOffset;
				}
				return ReadDateTimeOffsetString((string)Value);
			default:
				throw new global::System.ArgumentOutOfRangeException("readType");
			}
		}

		private global::Newtonsoft.Json.JsonReaderException CreateUnexpectedCharacterException(char c)
		{
			return global::Newtonsoft.Json.JsonReaderException.Create(this, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Unexpected character encountered while parsing value: {0}.", global::System.Globalization.CultureInfo.InvariantCulture, c));
		}

		public override bool? ReadAsBoolean()
		{
			EnsureBuffer();
			switch (_currentState)
			{
			case global::Newtonsoft.Json.JsonReader.State.PostValue:
				if (ParsePostValue(ignoreComments: true))
				{
					return null;
				}
				goto case global::Newtonsoft.Json.JsonReader.State.Start;
			case global::Newtonsoft.Json.JsonReader.State.Start:
			case global::Newtonsoft.Json.JsonReader.State.Property:
			case global::Newtonsoft.Json.JsonReader.State.ArrayStart:
			case global::Newtonsoft.Json.JsonReader.State.Array:
			case global::Newtonsoft.Json.JsonReader.State.ConstructorStart:
			case global::Newtonsoft.Json.JsonReader.State.Constructor:
				while (true)
				{
					char c = _chars[_charPos];
					switch (c)
					{
					case '\0':
						if (ReadNullChar())
						{
							SetToken(global::Newtonsoft.Json.JsonToken.None, null, updateIndex: false);
							return null;
						}
						break;
					case '"':
					case '\'':
						ParseString(c, global::Newtonsoft.Json.ReadType.Read);
						return ReadBooleanString(_stringReference.ToString());
					case 'n':
						HandleNull();
						return null;
					case '-':
					case '.':
					case '0':
					case '1':
					case '2':
					case '3':
					case '4':
					case '5':
					case '6':
					case '7':
					case '8':
					case '9':
					{
						ParseNumber(global::Newtonsoft.Json.ReadType.Read);
						bool flag2 = ((!(Value is global::System.Numerics.BigInteger bigInteger)) ? global::System.Convert.ToBoolean(Value, global::System.Globalization.CultureInfo.InvariantCulture) : (bigInteger != 0L));
						SetToken(global::Newtonsoft.Json.JsonToken.Boolean, flag2, updateIndex: false);
						return flag2;
					}
					case 'f':
					case 't':
					{
						bool flag = c == 't';
						string value = (flag ? global::Newtonsoft.Json.JsonConvert.True : global::Newtonsoft.Json.JsonConvert.False);
						if (!MatchValueWithTrailingSeparator(value))
						{
							throw CreateUnexpectedCharacterException(_chars[_charPos]);
						}
						SetToken(global::Newtonsoft.Json.JsonToken.Boolean, global::Newtonsoft.Json.Utilities.BoxedPrimitives.Get(flag));
						return flag;
					}
					case '/':
						ParseComment(setToken: false);
						break;
					case ',':
						ProcessValueComma();
						break;
					case ']':
						_charPos++;
						if (_currentState == global::Newtonsoft.Json.JsonReader.State.Array || _currentState == global::Newtonsoft.Json.JsonReader.State.ArrayStart || _currentState == global::Newtonsoft.Json.JsonReader.State.PostValue)
						{
							SetToken(global::Newtonsoft.Json.JsonToken.EndArray);
							return null;
						}
						throw CreateUnexpectedCharacterException(c);
					case '\r':
						ProcessCarriageReturn(append: false);
						break;
					case '\n':
						ProcessLineFeed();
						break;
					case '\t':
					case ' ':
						_charPos++;
						break;
					default:
						_charPos++;
						if (!char.IsWhiteSpace(c))
						{
							throw CreateUnexpectedCharacterException(c);
						}
						break;
					}
				}
			case global::Newtonsoft.Json.JsonReader.State.Finished:
				ReadFinished();
				return null;
			default:
				throw global::Newtonsoft.Json.JsonReaderException.Create(this, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Unexpected state: {0}.", global::System.Globalization.CultureInfo.InvariantCulture, base.CurrentState));
			}
		}

		private void ProcessValueComma()
		{
			_charPos++;
			if (_currentState != global::Newtonsoft.Json.JsonReader.State.PostValue)
			{
				SetToken(global::Newtonsoft.Json.JsonToken.Undefined);
				global::Newtonsoft.Json.JsonReaderException ex = CreateUnexpectedCharacterException(',');
				_charPos--;
				throw ex;
			}
			SetStateBasedOnCurrent();
		}

		private object? ReadNumberValue(global::Newtonsoft.Json.ReadType readType)
		{
			EnsureBuffer();
			switch (_currentState)
			{
			case global::Newtonsoft.Json.JsonReader.State.PostValue:
				if (ParsePostValue(ignoreComments: true))
				{
					return null;
				}
				goto case global::Newtonsoft.Json.JsonReader.State.Start;
			case global::Newtonsoft.Json.JsonReader.State.Start:
			case global::Newtonsoft.Json.JsonReader.State.Property:
			case global::Newtonsoft.Json.JsonReader.State.ArrayStart:
			case global::Newtonsoft.Json.JsonReader.State.Array:
			case global::Newtonsoft.Json.JsonReader.State.ConstructorStart:
			case global::Newtonsoft.Json.JsonReader.State.Constructor:
				while (true)
				{
					char c = _chars[_charPos];
					switch (c)
					{
					case '\0':
						if (ReadNullChar())
						{
							SetToken(global::Newtonsoft.Json.JsonToken.None, null, updateIndex: false);
							return null;
						}
						break;
					case '"':
					case '\'':
						ParseString(c, readType);
						return FinishReadQuotedNumber(readType);
					case 'n':
						HandleNull();
						return null;
					case 'N':
						return ParseNumberNaN(readType);
					case 'I':
						return ParseNumberPositiveInfinity(readType);
					case '-':
						if (EnsureChars(1, append: true) && _chars[_charPos + 1] == 'I')
						{
							return ParseNumberNegativeInfinity(readType);
						}
						ParseNumber(readType);
						return Value;
					case '.':
					case '0':
					case '1':
					case '2':
					case '3':
					case '4':
					case '5':
					case '6':
					case '7':
					case '8':
					case '9':
						ParseNumber(readType);
						return Value;
					case '/':
						ParseComment(setToken: false);
						break;
					case ',':
						ProcessValueComma();
						break;
					case ']':
						_charPos++;
						if (_currentState == global::Newtonsoft.Json.JsonReader.State.Array || _currentState == global::Newtonsoft.Json.JsonReader.State.ArrayStart || _currentState == global::Newtonsoft.Json.JsonReader.State.PostValue)
						{
							SetToken(global::Newtonsoft.Json.JsonToken.EndArray);
							return null;
						}
						throw CreateUnexpectedCharacterException(c);
					case '\r':
						ProcessCarriageReturn(append: false);
						break;
					case '\n':
						ProcessLineFeed();
						break;
					case '\t':
					case ' ':
						_charPos++;
						break;
					default:
						_charPos++;
						if (!char.IsWhiteSpace(c))
						{
							throw CreateUnexpectedCharacterException(c);
						}
						break;
					}
				}
			case global::Newtonsoft.Json.JsonReader.State.Finished:
				ReadFinished();
				return null;
			default:
				throw global::Newtonsoft.Json.JsonReaderException.Create(this, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Unexpected state: {0}.", global::System.Globalization.CultureInfo.InvariantCulture, base.CurrentState));
			}
		}

		private object? FinishReadQuotedNumber(global::Newtonsoft.Json.ReadType readType)
		{
			return readType switch
			{
				global::Newtonsoft.Json.ReadType.ReadAsInt32 => ReadInt32String(_stringReference.ToString()), 
				global::Newtonsoft.Json.ReadType.ReadAsDecimal => ReadDecimalString(_stringReference.ToString()), 
				global::Newtonsoft.Json.ReadType.ReadAsDouble => ReadDoubleString(_stringReference.ToString()), 
				_ => throw new global::System.ArgumentOutOfRangeException("readType"), 
			};
		}

		public override global::System.DateTimeOffset? ReadAsDateTimeOffset()
		{
			return (global::System.DateTimeOffset?)ReadStringValue(global::Newtonsoft.Json.ReadType.ReadAsDateTimeOffset);
		}

		public override decimal? ReadAsDecimal()
		{
			return (decimal?)ReadNumberValue(global::Newtonsoft.Json.ReadType.ReadAsDecimal);
		}

		public override double? ReadAsDouble()
		{
			return (double?)ReadNumberValue(global::Newtonsoft.Json.ReadType.ReadAsDouble);
		}

		private void HandleNull()
		{
			if (EnsureChars(1, append: true))
			{
				if (_chars[_charPos + 1] == 'u')
				{
					ParseNull();
					return;
				}
				_charPos += 2;
				throw CreateUnexpectedCharacterException(_chars[_charPos - 1]);
			}
			_charPos = _charsUsed;
			throw CreateUnexpectedEndException();
		}

		private void ReadFinished()
		{
			if (EnsureChars(0, append: false))
			{
				EatWhitespace();
				if (_isEndOfFile)
				{
					return;
				}
				if (_chars[_charPos] != '/')
				{
					throw global::Newtonsoft.Json.JsonReaderException.Create(this, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Additional text encountered after finished reading JSON content: {0}.", global::System.Globalization.CultureInfo.InvariantCulture, _chars[_charPos]));
				}
				ParseComment(setToken: false);
			}
			SetToken(global::Newtonsoft.Json.JsonToken.None);
		}

		private bool ReadNullChar()
		{
			if (_charsUsed == _charPos)
			{
				if (ReadData(append: false) == 0)
				{
					_isEndOfFile = true;
					return true;
				}
			}
			else
			{
				_charPos++;
			}
			return false;
		}

		private void EnsureBuffer()
		{
			if (_chars == null)
			{
				_chars = global::Newtonsoft.Json.Utilities.BufferUtils.RentBuffer(_arrayPool, 1024);
				_chars[0] = '\0';
			}
		}

		private void ReadStringIntoBuffer(char quote)
		{
			int num = _charPos;
			int charPos = _charPos;
			int lastWritePosition = _charPos;
			_stringBuffer.Position = 0;
			while (true)
			{
				switch (_chars[num++])
				{
				case '\0':
					if (_charsUsed == num - 1)
					{
						num--;
						if (ReadData(append: true) == 0)
						{
							_charPos = num;
							throw global::Newtonsoft.Json.JsonReaderException.Create(this, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Unterminated string. Expected delimiter: {0}.", global::System.Globalization.CultureInfo.InvariantCulture, quote));
						}
					}
					break;
				case '\\':
				{
					_charPos = num;
					if (!EnsureChars(0, append: true))
					{
						throw global::Newtonsoft.Json.JsonReaderException.Create(this, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Unterminated string. Expected delimiter: {0}.", global::System.Globalization.CultureInfo.InvariantCulture, quote));
					}
					int writeToPosition = num - 1;
					char c = _chars[num];
					num++;
					char c2;
					switch (c)
					{
					case 'b':
						c2 = '\b';
						break;
					case 't':
						c2 = '\t';
						break;
					case 'n':
						c2 = '\n';
						break;
					case 'f':
						c2 = '\f';
						break;
					case 'r':
						c2 = '\r';
						break;
					case '\\':
						c2 = '\\';
						break;
					case '"':
					case '\'':
					case '/':
						c2 = c;
						break;
					case 'u':
						_charPos = num;
						c2 = ParseUnicode();
						if (global::Newtonsoft.Json.Utilities.StringUtils.IsLowSurrogate(c2))
						{
							c2 = '\ufffd';
						}
						else if (global::Newtonsoft.Json.Utilities.StringUtils.IsHighSurrogate(c2))
						{
							bool flag;
							do
							{
								flag = false;
								if (EnsureChars(2, append: true) && _chars[_charPos] == '\\' && _chars[_charPos + 1] == 'u')
								{
									char writeChar = c2;
									_charPos += 2;
									c2 = ParseUnicode();
									if (!global::Newtonsoft.Json.Utilities.StringUtils.IsLowSurrogate(c2))
									{
										if (global::Newtonsoft.Json.Utilities.StringUtils.IsHighSurrogate(c2))
										{
											writeChar = '\ufffd';
											flag = true;
										}
										else
										{
											writeChar = '\ufffd';
										}
									}
									EnsureBufferNotEmpty();
									WriteCharToBuffer(writeChar, lastWritePosition, writeToPosition);
									lastWritePosition = _charPos;
								}
								else
								{
									c2 = '\ufffd';
								}
							}
							while (flag);
						}
						num = _charPos;
						break;
					default:
						_charPos = num;
						throw global::Newtonsoft.Json.JsonReaderException.Create(this, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Bad JSON escape sequence: {0}.", global::System.Globalization.CultureInfo.InvariantCulture, "\\" + c));
					}
					EnsureBufferNotEmpty();
					WriteCharToBuffer(c2, lastWritePosition, writeToPosition);
					lastWritePosition = num;
					break;
				}
				case '\r':
					_charPos = num - 1;
					ProcessCarriageReturn(append: true);
					num = _charPos;
					break;
				case '\n':
					_charPos = num - 1;
					ProcessLineFeed();
					num = _charPos;
					break;
				case '"':
				case '\'':
					if (_chars[num - 1] == quote)
					{
						FinishReadStringIntoBuffer(num - 1, charPos, lastWritePosition);
						return;
					}
					break;
				}
			}
		}

		private void FinishReadStringIntoBuffer(int charPos, int initialPosition, int lastWritePosition)
		{
			if (initialPosition == lastWritePosition)
			{
				_stringReference = new global::Newtonsoft.Json.Utilities.StringReference(_chars, initialPosition, charPos - initialPosition);
			}
			else
			{
				EnsureBufferNotEmpty();
				if (charPos > lastWritePosition)
				{
					_stringBuffer.Append(_arrayPool, _chars, lastWritePosition, charPos - lastWritePosition);
				}
				_stringReference = new global::Newtonsoft.Json.Utilities.StringReference(_stringBuffer.InternalBuffer, 0, _stringBuffer.Position);
			}
			_charPos = charPos + 1;
		}

		private void WriteCharToBuffer(char writeChar, int lastWritePosition, int writeToPosition)
		{
			if (writeToPosition > lastWritePosition)
			{
				_stringBuffer.Append(_arrayPool, _chars, lastWritePosition, writeToPosition - lastWritePosition);
			}
			_stringBuffer.Append(_arrayPool, writeChar);
		}

		private char ConvertUnicode(bool enoughChars)
		{
			if (enoughChars)
			{
				if (global::Newtonsoft.Json.Utilities.ConvertUtils.TryHexTextToInt(_chars, _charPos, _charPos + 4, out var value))
				{
					char result = global::System.Convert.ToChar(value);
					_charPos += 4;
					return result;
				}
				throw global::Newtonsoft.Json.JsonReaderException.Create(this, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Invalid Unicode escape sequence: \\u{0}.", global::System.Globalization.CultureInfo.InvariantCulture, new string(_chars, _charPos, 4)));
			}
			throw global::Newtonsoft.Json.JsonReaderException.Create(this, "Unexpected end while parsing Unicode escape sequence.");
		}

		private char ParseUnicode()
		{
			return ConvertUnicode(EnsureChars(4, append: true));
		}

		private void ReadNumberIntoBuffer()
		{
			int num = _charPos;
			while (true)
			{
				char c = _chars[num];
				if (c == '\0')
				{
					_charPos = num;
					if (_charsUsed != num || ReadData(append: true) == 0)
					{
						break;
					}
				}
				else
				{
					if (ReadNumberCharIntoBuffer(c, num))
					{
						break;
					}
					num++;
				}
			}
		}

		private bool ReadNumberCharIntoBuffer(char currentChar, int charPos)
		{
			switch (currentChar)
			{
			case '+':
			case '-':
			case '.':
			case '0':
			case '1':
			case '2':
			case '3':
			case '4':
			case '5':
			case '6':
			case '7':
			case '8':
			case '9':
			case 'A':
			case 'B':
			case 'C':
			case 'D':
			case 'E':
			case 'F':
			case 'X':
			case 'a':
			case 'b':
			case 'c':
			case 'd':
			case 'e':
			case 'f':
			case 'x':
				return false;
			default:
				_charPos = charPos;
				if (char.IsWhiteSpace(currentChar) || currentChar == ',' || currentChar == '}' || currentChar == ']' || currentChar == ')' || currentChar == '/')
				{
					return true;
				}
				throw global::Newtonsoft.Json.JsonReaderException.Create(this, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Unexpected character encountered while parsing number: {0}.", global::System.Globalization.CultureInfo.InvariantCulture, currentChar));
			}
		}

		private void ClearRecentString()
		{
			_stringBuffer.Position = 0;
			_stringReference = default(global::Newtonsoft.Json.Utilities.StringReference);
		}

		private bool ParsePostValue(bool ignoreComments)
		{
			while (true)
			{
				char c = _chars[_charPos];
				switch (c)
				{
				case '\0':
					if (_charsUsed == _charPos)
					{
						if (ReadData(append: false) == 0)
						{
							_currentState = global::Newtonsoft.Json.JsonReader.State.Finished;
							return false;
						}
					}
					else
					{
						_charPos++;
					}
					continue;
				case '}':
					_charPos++;
					SetToken(global::Newtonsoft.Json.JsonToken.EndObject);
					return true;
				case ']':
					_charPos++;
					SetToken(global::Newtonsoft.Json.JsonToken.EndArray);
					return true;
				case ')':
					_charPos++;
					SetToken(global::Newtonsoft.Json.JsonToken.EndConstructor);
					return true;
				case '/':
					ParseComment(!ignoreComments);
					if (!ignoreComments)
					{
						return true;
					}
					continue;
				case ',':
					_charPos++;
					SetStateBasedOnCurrent();
					return false;
				case '\t':
				case ' ':
					_charPos++;
					continue;
				case '\r':
					ProcessCarriageReturn(append: false);
					continue;
				case '\n':
					ProcessLineFeed();
					continue;
				}
				if (char.IsWhiteSpace(c))
				{
					_charPos++;
					continue;
				}
				if (base.SupportMultipleContent && Depth == 0)
				{
					SetStateBasedOnCurrent();
					return false;
				}
				throw global::Newtonsoft.Json.JsonReaderException.Create(this, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("After parsing a value an unexpected character was encountered: {0}.", global::System.Globalization.CultureInfo.InvariantCulture, c));
			}
		}

		private bool ParseObject()
		{
			while (true)
			{
				char c = _chars[_charPos];
				switch (c)
				{
				case '\0':
					if (_charsUsed == _charPos)
					{
						if (ReadData(append: false) == 0)
						{
							return false;
						}
					}
					else
					{
						_charPos++;
					}
					break;
				case '}':
					SetToken(global::Newtonsoft.Json.JsonToken.EndObject);
					_charPos++;
					return true;
				case '/':
					ParseComment(setToken: true);
					return true;
				case '\r':
					ProcessCarriageReturn(append: false);
					break;
				case '\n':
					ProcessLineFeed();
					break;
				case '\t':
				case ' ':
					_charPos++;
					break;
				default:
					if (char.IsWhiteSpace(c))
					{
						_charPos++;
						break;
					}
					return ParseProperty();
				}
			}
		}

		private bool ParseProperty()
		{
			char c = _chars[_charPos];
			char c2;
			if (c == '"' || c == '\'')
			{
				_charPos++;
				c2 = c;
				ShiftBufferIfNeeded();
				ReadStringIntoBuffer(c2);
			}
			else
			{
				if (!ValidIdentifierChar(c))
				{
					throw global::Newtonsoft.Json.JsonReaderException.Create(this, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Invalid property identifier character: {0}.", global::System.Globalization.CultureInfo.InvariantCulture, _chars[_charPos]));
				}
				c2 = '\0';
				ShiftBufferIfNeeded();
				ParseUnquotedProperty();
			}
			string text;
			if (PropertyNameTable != null)
			{
				text = PropertyNameTable.Get(_stringReference.Chars, _stringReference.StartIndex, _stringReference.Length);
				if (text == null)
				{
					text = _stringReference.ToString();
				}
			}
			else
			{
				text = _stringReference.ToString();
			}
			EatWhitespace();
			if (_chars[_charPos] != ':')
			{
				throw global::Newtonsoft.Json.JsonReaderException.Create(this, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Invalid character after parsing property name. Expected ':' but got: {0}.", global::System.Globalization.CultureInfo.InvariantCulture, _chars[_charPos]));
			}
			_charPos++;
			SetToken(global::Newtonsoft.Json.JsonToken.PropertyName, text);
			_quoteChar = c2;
			ClearRecentString();
			return true;
		}

		private bool ValidIdentifierChar(char value)
		{
			if (!char.IsLetterOrDigit(value) && value != '_')
			{
				return value == '$';
			}
			return true;
		}

		private void ParseUnquotedProperty()
		{
			int charPos = _charPos;
			while (true)
			{
				char c = _chars[_charPos];
				if (c == '\0')
				{
					if (_charsUsed != _charPos)
					{
						_stringReference = new global::Newtonsoft.Json.Utilities.StringReference(_chars, charPos, _charPos - charPos);
						break;
					}
					if (ReadData(append: true) == 0)
					{
						throw global::Newtonsoft.Json.JsonReaderException.Create(this, "Unexpected end while parsing unquoted property name.");
					}
				}
				else if (ReadUnquotedPropertyReportIfDone(c, charPos))
				{
					break;
				}
			}
		}

		private bool ReadUnquotedPropertyReportIfDone(char currentChar, int initialPosition)
		{
			if (ValidIdentifierChar(currentChar))
			{
				_charPos++;
				return false;
			}
			if (char.IsWhiteSpace(currentChar) || currentChar == ':')
			{
				_stringReference = new global::Newtonsoft.Json.Utilities.StringReference(_chars, initialPosition, _charPos - initialPosition);
				return true;
			}
			throw global::Newtonsoft.Json.JsonReaderException.Create(this, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Invalid JavaScript property identifier character: {0}.", global::System.Globalization.CultureInfo.InvariantCulture, currentChar));
		}

		private bool ParseValue()
		{
			while (true)
			{
				char c = _chars[_charPos];
				switch (c)
				{
				case '\0':
					if (_charsUsed == _charPos)
					{
						if (ReadData(append: false) == 0)
						{
							return false;
						}
					}
					else
					{
						_charPos++;
					}
					break;
				case '"':
				case '\'':
					ParseString(c, global::Newtonsoft.Json.ReadType.Read);
					return true;
				case 't':
					ParseTrue();
					return true;
				case 'f':
					ParseFalse();
					return true;
				case 'n':
					if (EnsureChars(1, append: true))
					{
						switch (_chars[_charPos + 1])
						{
						case 'u':
							ParseNull();
							break;
						case 'e':
							ParseConstructor();
							break;
						default:
							throw CreateUnexpectedCharacterException(_chars[_charPos]);
						}
						return true;
					}
					_charPos++;
					throw CreateUnexpectedEndException();
				case 'N':
					ParseNumberNaN(global::Newtonsoft.Json.ReadType.Read);
					return true;
				case 'I':
					ParseNumberPositiveInfinity(global::Newtonsoft.Json.ReadType.Read);
					return true;
				case '-':
					if (EnsureChars(1, append: true) && _chars[_charPos + 1] == 'I')
					{
						ParseNumberNegativeInfinity(global::Newtonsoft.Json.ReadType.Read);
					}
					else
					{
						ParseNumber(global::Newtonsoft.Json.ReadType.Read);
					}
					return true;
				case '/':
					ParseComment(setToken: true);
					return true;
				case 'u':
					ParseUndefined();
					return true;
				case '{':
					_charPos++;
					SetToken(global::Newtonsoft.Json.JsonToken.StartObject);
					return true;
				case '[':
					_charPos++;
					SetToken(global::Newtonsoft.Json.JsonToken.StartArray);
					return true;
				case ']':
					_charPos++;
					SetToken(global::Newtonsoft.Json.JsonToken.EndArray);
					return true;
				case ',':
					SetToken(global::Newtonsoft.Json.JsonToken.Undefined);
					return true;
				case ')':
					_charPos++;
					SetToken(global::Newtonsoft.Json.JsonToken.EndConstructor);
					return true;
				case '\r':
					ProcessCarriageReturn(append: false);
					break;
				case '\n':
					ProcessLineFeed();
					break;
				case '\t':
				case ' ':
					_charPos++;
					break;
				default:
					if (char.IsWhiteSpace(c))
					{
						_charPos++;
						break;
					}
					if (char.IsNumber(c) || c == '-' || c == '.')
					{
						ParseNumber(global::Newtonsoft.Json.ReadType.Read);
						return true;
					}
					throw CreateUnexpectedCharacterException(c);
				}
			}
		}

		private void ProcessLineFeed()
		{
			_charPos++;
			OnNewLine(_charPos);
		}

		private void ProcessCarriageReturn(bool append)
		{
			_charPos++;
			SetNewLine(EnsureChars(1, append));
		}

		private void EatWhitespace()
		{
			while (true)
			{
				char c = _chars[_charPos];
				switch (c)
				{
				case '\0':
					if (_charsUsed == _charPos)
					{
						if (ReadData(append: false) == 0)
						{
							return;
						}
					}
					else
					{
						_charPos++;
					}
					break;
				case '\r':
					ProcessCarriageReturn(append: false);
					break;
				case '\n':
					ProcessLineFeed();
					break;
				default:
					if (!char.IsWhiteSpace(c))
					{
						return;
					}
					goto case ' ';
				case ' ':
					_charPos++;
					break;
				}
			}
		}

		private void ParseConstructor()
		{
			if (MatchValueWithTrailingSeparator("new"))
			{
				EatWhitespace();
				int charPos = _charPos;
				int charPos2;
				while (true)
				{
					char c = _chars[_charPos];
					if (c == '\0')
					{
						if (_charsUsed == _charPos)
						{
							if (ReadData(append: true) == 0)
							{
								throw global::Newtonsoft.Json.JsonReaderException.Create(this, "Unexpected end while parsing constructor.");
							}
							continue;
						}
						charPos2 = _charPos;
						_charPos++;
						break;
					}
					if (char.IsLetterOrDigit(c))
					{
						_charPos++;
						continue;
					}
					switch (c)
					{
					case '\r':
						charPos2 = _charPos;
						ProcessCarriageReturn(append: true);
						break;
					case '\n':
						charPos2 = _charPos;
						ProcessLineFeed();
						break;
					default:
						if (char.IsWhiteSpace(c))
						{
							charPos2 = _charPos;
							_charPos++;
							break;
						}
						if (c == '(')
						{
							charPos2 = _charPos;
							break;
						}
						throw global::Newtonsoft.Json.JsonReaderException.Create(this, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Unexpected character while parsing constructor: {0}.", global::System.Globalization.CultureInfo.InvariantCulture, c));
					}
					break;
				}
				_stringReference = new global::Newtonsoft.Json.Utilities.StringReference(_chars, charPos, charPos2 - charPos);
				string value = _stringReference.ToString();
				EatWhitespace();
				if (_chars[_charPos] != '(')
				{
					throw global::Newtonsoft.Json.JsonReaderException.Create(this, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Unexpected character while parsing constructor: {0}.", global::System.Globalization.CultureInfo.InvariantCulture, _chars[_charPos]));
				}
				_charPos++;
				ClearRecentString();
				SetToken(global::Newtonsoft.Json.JsonToken.StartConstructor, value);
				return;
			}
			throw global::Newtonsoft.Json.JsonReaderException.Create(this, "Unexpected content while parsing JSON.");
		}

		private void ParseNumber(global::Newtonsoft.Json.ReadType readType)
		{
			ShiftBufferIfNeeded();
			char firstChar = _chars[_charPos];
			int charPos = _charPos;
			ReadNumberIntoBuffer();
			ParseReadNumber(readType, firstChar, charPos);
		}

		private void ParseReadNumber(global::Newtonsoft.Json.ReadType readType, char firstChar, int initialPosition)
		{
			SetPostValueState(updateIndex: true);
			_stringReference = new global::Newtonsoft.Json.Utilities.StringReference(_chars, initialPosition, _charPos - initialPosition);
			bool flag = char.IsDigit(firstChar) && _stringReference.Length == 1;
			bool flag2 = firstChar == '0' && _stringReference.Length > 1 && _stringReference.Chars[_stringReference.StartIndex + 1] != '.' && _stringReference.Chars[_stringReference.StartIndex + 1] != 'e' && _stringReference.Chars[_stringReference.StartIndex + 1] != 'E';
			global::Newtonsoft.Json.JsonToken newToken;
			object value;
			switch (readType)
			{
			case global::Newtonsoft.Json.ReadType.ReadAsString:
			{
				string text5 = _stringReference.ToString();
				double result3;
				if (flag2)
				{
					try
					{
						if (text5.StartsWith("0x", global::System.StringComparison.OrdinalIgnoreCase))
						{
							global::System.Convert.ToInt64(text5, 16);
						}
						else
						{
							global::System.Convert.ToInt64(text5, 8);
						}
					}
					catch (global::System.Exception ex4)
					{
						throw ThrowReaderError(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Input string '{0}' is not a valid number.", global::System.Globalization.CultureInfo.InvariantCulture, text5), ex4);
					}
				}
				else if (!double.TryParse(text5, global::System.Globalization.NumberStyles.Float, global::System.Globalization.CultureInfo.InvariantCulture, out result3))
				{
					throw ThrowReaderError(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Input string '{0}' is not a valid number.", global::System.Globalization.CultureInfo.InvariantCulture, _stringReference.ToString()));
				}
				newToken = global::Newtonsoft.Json.JsonToken.String;
				value = text5;
				break;
			}
			case global::Newtonsoft.Json.ReadType.ReadAsInt32:
				if (flag)
				{
					value = global::Newtonsoft.Json.Utilities.BoxedPrimitives.Get(firstChar - 48);
				}
				else if (flag2)
				{
					string text6 = _stringReference.ToString();
					try
					{
						value = global::Newtonsoft.Json.Utilities.BoxedPrimitives.Get(text6.StartsWith("0x", global::System.StringComparison.OrdinalIgnoreCase) ? global::System.Convert.ToInt32(text6, 16) : global::System.Convert.ToInt32(text6, 8));
					}
					catch (global::System.Exception ex5)
					{
						throw ThrowReaderError(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Input string '{0}' is not a valid integer.", global::System.Globalization.CultureInfo.InvariantCulture, text6), ex5);
					}
				}
				else
				{
					int value5;
					switch (global::Newtonsoft.Json.Utilities.ConvertUtils.Int32TryParse(_stringReference.Chars, _stringReference.StartIndex, _stringReference.Length, out value5))
					{
					case global::Newtonsoft.Json.Utilities.ParseResult.Success:
						break;
					case global::Newtonsoft.Json.Utilities.ParseResult.Overflow:
						throw ThrowReaderError(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("JSON integer {0} is too large or small for an Int32.", global::System.Globalization.CultureInfo.InvariantCulture, _stringReference.ToString()));
					default:
						throw ThrowReaderError(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Input string '{0}' is not a valid integer.", global::System.Globalization.CultureInfo.InvariantCulture, _stringReference.ToString()));
					}
					value = global::Newtonsoft.Json.Utilities.BoxedPrimitives.Get(value5);
				}
				newToken = global::Newtonsoft.Json.JsonToken.Integer;
				break;
			case global::Newtonsoft.Json.ReadType.ReadAsDecimal:
				if (flag)
				{
					value = global::Newtonsoft.Json.Utilities.BoxedPrimitives.Get((decimal)firstChar - 48m);
				}
				else if (flag2)
				{
					string text3 = _stringReference.ToString();
					try
					{
						value = global::Newtonsoft.Json.Utilities.BoxedPrimitives.Get(global::System.Convert.ToDecimal(text3.StartsWith("0x", global::System.StringComparison.OrdinalIgnoreCase) ? global::System.Convert.ToInt64(text3, 16) : global::System.Convert.ToInt64(text3, 8)));
					}
					catch (global::System.Exception ex2)
					{
						throw ThrowReaderError(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Input string '{0}' is not a valid decimal.", global::System.Globalization.CultureInfo.InvariantCulture, text3), ex2);
					}
				}
				else
				{
					if (global::Newtonsoft.Json.Utilities.ConvertUtils.DecimalTryParse(_stringReference.Chars, _stringReference.StartIndex, _stringReference.Length, out var value4) != global::Newtonsoft.Json.Utilities.ParseResult.Success)
					{
						throw ThrowReaderError(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Input string '{0}' is not a valid decimal.", global::System.Globalization.CultureInfo.InvariantCulture, _stringReference.ToString()));
					}
					value = global::Newtonsoft.Json.Utilities.BoxedPrimitives.Get(value4);
				}
				newToken = global::Newtonsoft.Json.JsonToken.Float;
				break;
			case global::Newtonsoft.Json.ReadType.ReadAsDouble:
				if (flag)
				{
					value = global::Newtonsoft.Json.Utilities.BoxedPrimitives.Get((double)(int)firstChar - 48.0);
				}
				else if (flag2)
				{
					string text4 = _stringReference.ToString();
					try
					{
						value = global::Newtonsoft.Json.Utilities.BoxedPrimitives.Get(global::System.Convert.ToDouble(text4.StartsWith("0x", global::System.StringComparison.OrdinalIgnoreCase) ? global::System.Convert.ToInt64(text4, 16) : global::System.Convert.ToInt64(text4, 8)));
					}
					catch (global::System.Exception ex3)
					{
						throw ThrowReaderError(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Input string '{0}' is not a valid double.", global::System.Globalization.CultureInfo.InvariantCulture, text4), ex3);
					}
				}
				else
				{
					if (!double.TryParse(_stringReference.ToString(), global::System.Globalization.NumberStyles.Float, global::System.Globalization.CultureInfo.InvariantCulture, out var result2))
					{
						throw ThrowReaderError(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Input string '{0}' is not a valid double.", global::System.Globalization.CultureInfo.InvariantCulture, _stringReference.ToString()));
					}
					value = global::Newtonsoft.Json.Utilities.BoxedPrimitives.Get(result2);
				}
				newToken = global::Newtonsoft.Json.JsonToken.Float;
				break;
			case global::Newtonsoft.Json.ReadType.Read:
			case global::Newtonsoft.Json.ReadType.ReadAsInt64:
			{
				if (flag)
				{
					value = global::Newtonsoft.Json.Utilities.BoxedPrimitives.Get((long)firstChar - 48L);
					newToken = global::Newtonsoft.Json.JsonToken.Integer;
					break;
				}
				if (flag2)
				{
					string text = _stringReference.ToString();
					try
					{
						value = global::Newtonsoft.Json.Utilities.BoxedPrimitives.Get(text.StartsWith("0x", global::System.StringComparison.OrdinalIgnoreCase) ? global::System.Convert.ToInt64(text, 16) : global::System.Convert.ToInt64(text, 8));
					}
					catch (global::System.Exception ex)
					{
						throw ThrowReaderError(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Input string '{0}' is not a valid number.", global::System.Globalization.CultureInfo.InvariantCulture, text), ex);
					}
					newToken = global::Newtonsoft.Json.JsonToken.Integer;
					break;
				}
				long value2;
				switch (global::Newtonsoft.Json.Utilities.ConvertUtils.Int64TryParse(_stringReference.Chars, _stringReference.StartIndex, _stringReference.Length, out value2))
				{
				case global::Newtonsoft.Json.Utilities.ParseResult.Success:
					value = global::Newtonsoft.Json.Utilities.BoxedPrimitives.Get(value2);
					newToken = global::Newtonsoft.Json.JsonToken.Integer;
					break;
				case global::Newtonsoft.Json.Utilities.ParseResult.Overflow:
				{
					string text2 = _stringReference.ToString();
					if (text2.Length > 380)
					{
						throw ThrowReaderError(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("JSON integer {0} is too large to parse.", global::System.Globalization.CultureInfo.InvariantCulture, _stringReference.ToString()));
					}
					value = BigIntegerParse(text2, global::System.Globalization.CultureInfo.InvariantCulture);
					newToken = global::Newtonsoft.Json.JsonToken.Integer;
					break;
				}
				default:
					if (_floatParseHandling == global::Newtonsoft.Json.FloatParseHandling.Decimal)
					{
						decimal value3;
						global::Newtonsoft.Json.Utilities.ParseResult parseResult = global::Newtonsoft.Json.Utilities.ConvertUtils.DecimalTryParse(_stringReference.Chars, _stringReference.StartIndex, _stringReference.Length, out value3);
						if (parseResult != global::Newtonsoft.Json.Utilities.ParseResult.Success)
						{
							throw ThrowReaderError(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Input string '{0}' is not a valid decimal.", global::System.Globalization.CultureInfo.InvariantCulture, _stringReference.ToString()));
						}
						value = global::Newtonsoft.Json.Utilities.BoxedPrimitives.Get(value3);
					}
					else
					{
						if (!double.TryParse(_stringReference.ToString(), global::System.Globalization.NumberStyles.Float, global::System.Globalization.CultureInfo.InvariantCulture, out var result))
						{
							throw ThrowReaderError(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Input string '{0}' is not a valid number.", global::System.Globalization.CultureInfo.InvariantCulture, _stringReference.ToString()));
						}
						value = global::Newtonsoft.Json.Utilities.BoxedPrimitives.Get(result);
					}
					newToken = global::Newtonsoft.Json.JsonToken.Float;
					break;
				}
				break;
			}
			default:
				throw global::Newtonsoft.Json.JsonReaderException.Create(this, "Cannot read number value as type.");
			}
			ClearRecentString();
			SetToken(newToken, value, updateIndex: false);
		}

		private global::Newtonsoft.Json.JsonReaderException ThrowReaderError(string message, global::System.Exception? ex = null)
		{
			SetToken(global::Newtonsoft.Json.JsonToken.Undefined, null, updateIndex: false);
			return global::Newtonsoft.Json.JsonReaderException.Create(this, message, ex);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
		private static object BigIntegerParse(string number, global::System.Globalization.CultureInfo culture)
		{
			return global::System.Numerics.BigInteger.Parse(number, culture);
		}

		private void ParseComment(bool setToken)
		{
			_charPos++;
			if (!EnsureChars(1, append: false))
			{
				throw global::Newtonsoft.Json.JsonReaderException.Create(this, "Unexpected end while parsing comment.");
			}
			bool flag;
			if (_chars[_charPos] == '*')
			{
				flag = false;
			}
			else
			{
				if (_chars[_charPos] != '/')
				{
					throw global::Newtonsoft.Json.JsonReaderException.Create(this, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Error parsing comment. Expected: *, got {0}.", global::System.Globalization.CultureInfo.InvariantCulture, _chars[_charPos]));
				}
				flag = true;
			}
			_charPos++;
			int charPos = _charPos;
			while (true)
			{
				switch (_chars[_charPos])
				{
				case '\0':
					if (_charsUsed == _charPos)
					{
						if (ReadData(append: true) == 0)
						{
							if (!flag)
							{
								throw global::Newtonsoft.Json.JsonReaderException.Create(this, "Unexpected end while parsing comment.");
							}
							EndComment(setToken, charPos, _charPos);
							return;
						}
					}
					else
					{
						_charPos++;
					}
					break;
				case '*':
					_charPos++;
					if (!flag && EnsureChars(0, append: true) && _chars[_charPos] == '/')
					{
						EndComment(setToken, charPos, _charPos - 1);
						_charPos++;
						return;
					}
					break;
				case '\r':
					if (flag)
					{
						EndComment(setToken, charPos, _charPos);
						return;
					}
					ProcessCarriageReturn(append: true);
					break;
				case '\n':
					if (flag)
					{
						EndComment(setToken, charPos, _charPos);
						return;
					}
					ProcessLineFeed();
					break;
				default:
					_charPos++;
					break;
				}
			}
		}

		private void EndComment(bool setToken, int initialPosition, int endPosition)
		{
			if (setToken)
			{
				SetToken(global::Newtonsoft.Json.JsonToken.Comment, new string(_chars, initialPosition, endPosition - initialPosition));
			}
		}

		private bool MatchValue(string value)
		{
			return MatchValue(EnsureChars(value.Length - 1, append: true), value);
		}

		private bool MatchValue(bool enoughChars, string value)
		{
			if (!enoughChars)
			{
				_charPos = _charsUsed;
				throw CreateUnexpectedEndException();
			}
			for (int i = 0; i < value.Length; i++)
			{
				if (_chars[_charPos + i] != value[i])
				{
					_charPos += i;
					return false;
				}
			}
			_charPos += value.Length;
			return true;
		}

		private bool MatchValueWithTrailingSeparator(string value)
		{
			if (!MatchValue(value))
			{
				return false;
			}
			if (!EnsureChars(0, append: false))
			{
				return true;
			}
			if (!IsSeparator(_chars[_charPos]))
			{
				return _chars[_charPos] == '\0';
			}
			return true;
		}

		private bool IsSeparator(char c)
		{
			switch (c)
			{
			case ',':
			case ']':
			case '}':
				return true;
			case '/':
			{
				if (!EnsureChars(1, append: false))
				{
					return false;
				}
				char c2 = _chars[_charPos + 1];
				if (c2 != '*')
				{
					return c2 == '/';
				}
				return true;
			}
			case ')':
				if (base.CurrentState == global::Newtonsoft.Json.JsonReader.State.Constructor || base.CurrentState == global::Newtonsoft.Json.JsonReader.State.ConstructorStart)
				{
					return true;
				}
				break;
			case '\t':
			case '\n':
			case '\r':
			case ' ':
				return true;
			default:
				if (char.IsWhiteSpace(c))
				{
					return true;
				}
				break;
			}
			return false;
		}

		private void ParseTrue()
		{
			if (MatchValueWithTrailingSeparator(global::Newtonsoft.Json.JsonConvert.True))
			{
				SetToken(global::Newtonsoft.Json.JsonToken.Boolean, global::Newtonsoft.Json.Utilities.BoxedPrimitives.BooleanTrue);
				return;
			}
			throw global::Newtonsoft.Json.JsonReaderException.Create(this, "Error parsing boolean value.");
		}

		private void ParseNull()
		{
			if (MatchValueWithTrailingSeparator(global::Newtonsoft.Json.JsonConvert.Null))
			{
				SetToken(global::Newtonsoft.Json.JsonToken.Null);
				return;
			}
			throw global::Newtonsoft.Json.JsonReaderException.Create(this, "Error parsing null value.");
		}

		private void ParseUndefined()
		{
			if (MatchValueWithTrailingSeparator(global::Newtonsoft.Json.JsonConvert.Undefined))
			{
				SetToken(global::Newtonsoft.Json.JsonToken.Undefined);
				return;
			}
			throw global::Newtonsoft.Json.JsonReaderException.Create(this, "Error parsing undefined value.");
		}

		private void ParseFalse()
		{
			if (MatchValueWithTrailingSeparator(global::Newtonsoft.Json.JsonConvert.False))
			{
				SetToken(global::Newtonsoft.Json.JsonToken.Boolean, global::Newtonsoft.Json.Utilities.BoxedPrimitives.BooleanFalse);
				return;
			}
			throw global::Newtonsoft.Json.JsonReaderException.Create(this, "Error parsing boolean value.");
		}

		private object ParseNumberNegativeInfinity(global::Newtonsoft.Json.ReadType readType)
		{
			return ParseNumberNegativeInfinity(readType, MatchValueWithTrailingSeparator(global::Newtonsoft.Json.JsonConvert.NegativeInfinity));
		}

		private object ParseNumberNegativeInfinity(global::Newtonsoft.Json.ReadType readType, bool matched)
		{
			if (matched)
			{
				switch (readType)
				{
				case global::Newtonsoft.Json.ReadType.Read:
				case global::Newtonsoft.Json.ReadType.ReadAsDouble:
					if (_floatParseHandling == global::Newtonsoft.Json.FloatParseHandling.Double)
					{
						SetToken(global::Newtonsoft.Json.JsonToken.Float, global::Newtonsoft.Json.Utilities.BoxedPrimitives.DoubleNegativeInfinity);
						return double.NegativeInfinity;
					}
					break;
				case global::Newtonsoft.Json.ReadType.ReadAsString:
					SetToken(global::Newtonsoft.Json.JsonToken.String, global::Newtonsoft.Json.JsonConvert.NegativeInfinity);
					return global::Newtonsoft.Json.JsonConvert.NegativeInfinity;
				}
				throw global::Newtonsoft.Json.JsonReaderException.Create(this, "Cannot read -Infinity value.");
			}
			throw global::Newtonsoft.Json.JsonReaderException.Create(this, "Error parsing -Infinity value.");
		}

		private object ParseNumberPositiveInfinity(global::Newtonsoft.Json.ReadType readType)
		{
			return ParseNumberPositiveInfinity(readType, MatchValueWithTrailingSeparator(global::Newtonsoft.Json.JsonConvert.PositiveInfinity));
		}

		private object ParseNumberPositiveInfinity(global::Newtonsoft.Json.ReadType readType, bool matched)
		{
			if (matched)
			{
				switch (readType)
				{
				case global::Newtonsoft.Json.ReadType.Read:
				case global::Newtonsoft.Json.ReadType.ReadAsDouble:
					if (_floatParseHandling == global::Newtonsoft.Json.FloatParseHandling.Double)
					{
						SetToken(global::Newtonsoft.Json.JsonToken.Float, global::Newtonsoft.Json.Utilities.BoxedPrimitives.DoublePositiveInfinity);
						return double.PositiveInfinity;
					}
					break;
				case global::Newtonsoft.Json.ReadType.ReadAsString:
					SetToken(global::Newtonsoft.Json.JsonToken.String, global::Newtonsoft.Json.JsonConvert.PositiveInfinity);
					return global::Newtonsoft.Json.JsonConvert.PositiveInfinity;
				}
				throw global::Newtonsoft.Json.JsonReaderException.Create(this, "Cannot read Infinity value.");
			}
			throw global::Newtonsoft.Json.JsonReaderException.Create(this, "Error parsing Infinity value.");
		}

		private object ParseNumberNaN(global::Newtonsoft.Json.ReadType readType)
		{
			return ParseNumberNaN(readType, MatchValueWithTrailingSeparator(global::Newtonsoft.Json.JsonConvert.NaN));
		}

		private object ParseNumberNaN(global::Newtonsoft.Json.ReadType readType, bool matched)
		{
			if (matched)
			{
				switch (readType)
				{
				case global::Newtonsoft.Json.ReadType.Read:
				case global::Newtonsoft.Json.ReadType.ReadAsDouble:
					if (_floatParseHandling == global::Newtonsoft.Json.FloatParseHandling.Double)
					{
						SetToken(global::Newtonsoft.Json.JsonToken.Float, global::Newtonsoft.Json.Utilities.BoxedPrimitives.DoubleNaN);
						return double.NaN;
					}
					break;
				case global::Newtonsoft.Json.ReadType.ReadAsString:
					SetToken(global::Newtonsoft.Json.JsonToken.String, global::Newtonsoft.Json.JsonConvert.NaN);
					return global::Newtonsoft.Json.JsonConvert.NaN;
				}
				throw global::Newtonsoft.Json.JsonReaderException.Create(this, "Cannot read NaN value.");
			}
			throw global::Newtonsoft.Json.JsonReaderException.Create(this, "Error parsing NaN value.");
		}

		public override void Close()
		{
			base.Close();
			if (_chars != null)
			{
				global::Newtonsoft.Json.Utilities.BufferUtils.ReturnBuffer(_arrayPool, _chars);
				_chars = null;
			}
			if (base.CloseInput)
			{
				_reader?.Close();
			}
			_stringBuffer.Clear(_arrayPool);
		}

		public bool HasLineInfo()
		{
			return true;
		}
	}
}
