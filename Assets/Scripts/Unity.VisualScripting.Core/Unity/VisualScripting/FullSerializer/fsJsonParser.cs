namespace Unity.VisualScripting.FullSerializer
{
	public class fsJsonParser
	{
		private readonly global::System.Text.StringBuilder _cachedStringBuilder = new global::System.Text.StringBuilder(256);

		private int _start;

		private string _input;

		private fsJsonParser(string input)
		{
			_input = input;
			_start = 0;
		}

		private global::Unity.VisualScripting.FullSerializer.fsResult MakeFailure(string message)
		{
			int num = global::System.Math.Max(0, _start - 20);
			int length = global::System.Math.Min(50, _input.Length - num);
			return global::Unity.VisualScripting.FullSerializer.fsResult.Fail("Error while parsing: " + message + "; context = <" + _input.Substring(num, length) + ">");
		}

		private bool TryMoveNext()
		{
			if (_start < _input.Length)
			{
				_start++;
				return true;
			}
			return false;
		}

		private bool HasValue()
		{
			return HasValue(0);
		}

		private bool HasValue(int offset)
		{
			if (_start + offset >= 0)
			{
				return _start + offset < _input.Length;
			}
			return false;
		}

		private char Character()
		{
			return Character(0);
		}

		private char Character(int offset)
		{
			return _input[_start + offset];
		}

		private void SkipSpace()
		{
			while (HasValue())
			{
				if (char.IsWhiteSpace(Character()))
				{
					TryMoveNext();
					continue;
				}
				if (!HasValue(1) || Character(0) != '/')
				{
					break;
				}
				if (Character(1) == '/')
				{
					while (HasValue() && !global::System.Environment.NewLine.Contains(Character().ToString() ?? ""))
					{
						TryMoveNext();
					}
				}
				else
				{
					if (Character(1) != '*')
					{
						continue;
					}
					TryMoveNext();
					TryMoveNext();
					while (HasValue(1))
					{
						if (Character(0) == '*' && Character(1) == '/')
						{
							TryMoveNext();
							TryMoveNext();
							TryMoveNext();
							break;
						}
						TryMoveNext();
					}
				}
			}
		}

		private global::Unity.VisualScripting.FullSerializer.fsResult TryParseExact(string content)
		{
			for (int i = 0; i < content.Length; i++)
			{
				if (Character() != content[i])
				{
					return MakeFailure("Expected " + content[i]);
				}
				if (!TryMoveNext())
				{
					return MakeFailure("Unexpected end of content when parsing " + content);
				}
			}
			return global::Unity.VisualScripting.FullSerializer.fsResult.Success;
		}

		private global::Unity.VisualScripting.FullSerializer.fsResult TryParseTrue(out global::Unity.VisualScripting.FullSerializer.fsData data)
		{
			global::Unity.VisualScripting.FullSerializer.fsResult result = TryParseExact("true");
			if (result.Succeeded)
			{
				data = new global::Unity.VisualScripting.FullSerializer.fsData(boolean: true);
				return global::Unity.VisualScripting.FullSerializer.fsResult.Success;
			}
			data = null;
			return result;
		}

		private global::Unity.VisualScripting.FullSerializer.fsResult TryParseFalse(out global::Unity.VisualScripting.FullSerializer.fsData data)
		{
			global::Unity.VisualScripting.FullSerializer.fsResult result = TryParseExact("false");
			if (result.Succeeded)
			{
				data = new global::Unity.VisualScripting.FullSerializer.fsData(boolean: false);
				return global::Unity.VisualScripting.FullSerializer.fsResult.Success;
			}
			data = null;
			return result;
		}

		private global::Unity.VisualScripting.FullSerializer.fsResult TryParseNull(out global::Unity.VisualScripting.FullSerializer.fsData data)
		{
			global::Unity.VisualScripting.FullSerializer.fsResult result = TryParseExact("null");
			if (result.Succeeded)
			{
				data = new global::Unity.VisualScripting.FullSerializer.fsData();
				return global::Unity.VisualScripting.FullSerializer.fsResult.Success;
			}
			data = null;
			return result;
		}

		private bool IsSeparator(char c)
		{
			if (!char.IsWhiteSpace(c) && c != ',' && c != '}')
			{
				return c == ']';
			}
			return true;
		}

		private global::Unity.VisualScripting.FullSerializer.fsResult TryParseNumber(out global::Unity.VisualScripting.FullSerializer.fsData data)
		{
			int start = _start;
			while (TryMoveNext() && HasValue() && !IsSeparator(Character()))
			{
			}
			string text = _input.Substring(start, _start - start);
			if (!text.Contains(".") && !text.Contains("e") && !text.Contains("E"))
			{
				switch (text)
				{
				case "Infinity":
				case "-Infinity":
				case "NaN":
					break;
				default:
				{
					if (!long.TryParse(text, global::System.Globalization.NumberStyles.Any, global::System.Globalization.CultureInfo.InvariantCulture, out var result))
					{
						data = null;
						return MakeFailure("Bad Int64 format with " + text);
					}
					data = new global::Unity.VisualScripting.FullSerializer.fsData(result);
					return global::Unity.VisualScripting.FullSerializer.fsResult.Success;
				}
				}
			}
			if (!double.TryParse(text, global::System.Globalization.NumberStyles.Any, global::System.Globalization.CultureInfo.InvariantCulture, out var result2))
			{
				data = null;
				return MakeFailure("Bad double format with " + text);
			}
			data = new global::Unity.VisualScripting.FullSerializer.fsData(result2);
			return global::Unity.VisualScripting.FullSerializer.fsResult.Success;
		}

		private global::Unity.VisualScripting.FullSerializer.fsResult TryParseString(out string str)
		{
			_cachedStringBuilder.Length = 0;
			if (Character() != '"' || !TryMoveNext())
			{
				str = string.Empty;
				return MakeFailure("Expected initial \" when parsing a string");
			}
			while (HasValue() && Character() != '"')
			{
				char c = Character();
				if (c == '\\')
				{
					char escaped;
					global::Unity.VisualScripting.FullSerializer.fsResult result = TryUnescapeChar(out escaped);
					if (result.Failed)
					{
						str = string.Empty;
						return result;
					}
					_cachedStringBuilder.Append(escaped);
				}
				else
				{
					_cachedStringBuilder.Append(c);
					if (!TryMoveNext())
					{
						str = string.Empty;
						return MakeFailure("Unexpected end of input when reading a string");
					}
				}
			}
			if (!HasValue() || Character() != '"' || !TryMoveNext())
			{
				str = string.Empty;
				return MakeFailure("No closing \" when parsing a string");
			}
			str = _cachedStringBuilder.ToString();
			return global::Unity.VisualScripting.FullSerializer.fsResult.Success;
		}

		private global::Unity.VisualScripting.FullSerializer.fsResult TryParseArray(out global::Unity.VisualScripting.FullSerializer.fsData arr)
		{
			if (Character() != '[')
			{
				arr = null;
				return MakeFailure("Expected initial [ when parsing an array");
			}
			if (!TryMoveNext())
			{
				arr = null;
				return MakeFailure("Unexpected end of input when parsing an array");
			}
			SkipSpace();
			global::System.Collections.Generic.List<global::Unity.VisualScripting.FullSerializer.fsData> list = new global::System.Collections.Generic.List<global::Unity.VisualScripting.FullSerializer.fsData>();
			while (HasValue() && Character() != ']')
			{
				global::Unity.VisualScripting.FullSerializer.fsData data;
				global::Unity.VisualScripting.FullSerializer.fsResult result = RunParse(out data);
				if (result.Failed)
				{
					arr = null;
					return result;
				}
				list.Add(data);
				SkipSpace();
				if (HasValue() && Character() == ',')
				{
					if (!TryMoveNext())
					{
						break;
					}
					SkipSpace();
				}
			}
			if (!HasValue() || Character() != ']' || !TryMoveNext())
			{
				arr = null;
				return MakeFailure("No closing ] for array");
			}
			arr = new global::Unity.VisualScripting.FullSerializer.fsData(list);
			return global::Unity.VisualScripting.FullSerializer.fsResult.Success;
		}

		private global::Unity.VisualScripting.FullSerializer.fsResult TryParseObject(out global::Unity.VisualScripting.FullSerializer.fsData obj)
		{
			if (Character() != '{')
			{
				obj = null;
				return MakeFailure("Expected initial { when parsing an object");
			}
			if (!TryMoveNext())
			{
				obj = null;
				return MakeFailure("Unexpected end of input when parsing an object");
			}
			SkipSpace();
			global::System.Collections.Generic.Dictionary<string, global::Unity.VisualScripting.FullSerializer.fsData> dictionary = new global::System.Collections.Generic.Dictionary<string, global::Unity.VisualScripting.FullSerializer.fsData>(global::Unity.VisualScripting.FullSerializer.fsGlobalConfig.IsCaseSensitive ? global::System.StringComparer.Ordinal : global::System.StringComparer.OrdinalIgnoreCase);
			while (HasValue() && Character() != '}')
			{
				SkipSpace();
				global::Unity.VisualScripting.FullSerializer.fsResult result = TryParseString(out var str);
				if (result.Failed)
				{
					obj = null;
					return result;
				}
				SkipSpace();
				if (!HasValue() || Character() != ':' || !TryMoveNext())
				{
					obj = null;
					return MakeFailure("Expected : after key \"" + str + "\"");
				}
				SkipSpace();
				result = RunParse(out var data);
				if (result.Failed)
				{
					obj = null;
					return result;
				}
				dictionary.Add(str, data);
				SkipSpace();
				if (HasValue() && Character() == ',')
				{
					if (!TryMoveNext())
					{
						break;
					}
					SkipSpace();
				}
			}
			if (!HasValue() || Character() != '}' || !TryMoveNext())
			{
				obj = null;
				return MakeFailure("No closing } for object");
			}
			obj = new global::Unity.VisualScripting.FullSerializer.fsData(dictionary);
			return global::Unity.VisualScripting.FullSerializer.fsResult.Success;
		}

		private global::Unity.VisualScripting.FullSerializer.fsResult RunParse(out global::Unity.VisualScripting.FullSerializer.fsData data)
		{
			SkipSpace();
			if (!HasValue())
			{
				data = null;
				return MakeFailure("Unexpected end of input");
			}
			switch (Character())
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
			case 'I':
			case 'N':
				return TryParseNumber(out data);
			case '"':
			{
				string str;
				global::Unity.VisualScripting.FullSerializer.fsResult result = TryParseString(out str);
				if (result.Failed)
				{
					data = null;
					return result;
				}
				data = new global::Unity.VisualScripting.FullSerializer.fsData(str);
				return global::Unity.VisualScripting.FullSerializer.fsResult.Success;
			}
			case '[':
				return TryParseArray(out data);
			case '{':
				return TryParseObject(out data);
			case 't':
				return TryParseTrue(out data);
			case 'f':
				return TryParseFalse(out data);
			case 'n':
				return TryParseNull(out data);
			default:
				data = null;
				return MakeFailure("unable to parse; invalid token \"" + Character() + "\"");
			}
		}

		public static global::Unity.VisualScripting.FullSerializer.fsResult Parse(string input, out global::Unity.VisualScripting.FullSerializer.fsData data)
		{
			if (string.IsNullOrEmpty(input))
			{
				data = null;
				return global::Unity.VisualScripting.FullSerializer.fsResult.Fail("No input");
			}
			return new global::Unity.VisualScripting.FullSerializer.fsJsonParser(input).RunParse(out data);
		}

		public static global::Unity.VisualScripting.FullSerializer.fsData Parse(string input)
		{
			Parse(input, out var data).AssertSuccess();
			return data;
		}

		private bool IsHex(char c)
		{
			if ((c < '0' || c > '9') && (c < 'a' || c > 'f'))
			{
				if (c >= 'A')
				{
					return c <= 'F';
				}
				return false;
			}
			return true;
		}

		private uint ParseSingleChar(char c1, uint multipliyer)
		{
			uint result = 0u;
			if (c1 >= '0' && c1 <= '9')
			{
				result = (uint)(c1 - 48) * multipliyer;
			}
			else if (c1 >= 'A' && c1 <= 'F')
			{
				result = (uint)(c1 - 65 + 10) * multipliyer;
			}
			else if (c1 >= 'a' && c1 <= 'f')
			{
				result = (uint)(c1 - 97 + 10) * multipliyer;
			}
			return result;
		}

		private uint ParseUnicode(char c1, char c2, char c3, char c4)
		{
			uint num = ParseSingleChar(c1, 4096u);
			uint num2 = ParseSingleChar(c2, 256u);
			uint num3 = ParseSingleChar(c3, 16u);
			uint num4 = ParseSingleChar(c4, 1u);
			return num + num2 + num3 + num4;
		}

		private global::Unity.VisualScripting.FullSerializer.fsResult TryUnescapeChar(out char escaped)
		{
			TryMoveNext();
			if (!HasValue())
			{
				escaped = ' ';
				return MakeFailure("Unexpected end of input after \\");
			}
			switch (Character())
			{
			case '\\':
				TryMoveNext();
				escaped = '\\';
				return global::Unity.VisualScripting.FullSerializer.fsResult.Success;
			case '/':
				TryMoveNext();
				escaped = '/';
				return global::Unity.VisualScripting.FullSerializer.fsResult.Success;
			case '"':
				TryMoveNext();
				escaped = '"';
				return global::Unity.VisualScripting.FullSerializer.fsResult.Success;
			case 'a':
				TryMoveNext();
				escaped = '\a';
				return global::Unity.VisualScripting.FullSerializer.fsResult.Success;
			case 'b':
				TryMoveNext();
				escaped = '\b';
				return global::Unity.VisualScripting.FullSerializer.fsResult.Success;
			case 'f':
				TryMoveNext();
				escaped = '\f';
				return global::Unity.VisualScripting.FullSerializer.fsResult.Success;
			case 'n':
				TryMoveNext();
				escaped = '\n';
				return global::Unity.VisualScripting.FullSerializer.fsResult.Success;
			case 'r':
				TryMoveNext();
				escaped = '\r';
				return global::Unity.VisualScripting.FullSerializer.fsResult.Success;
			case 't':
				TryMoveNext();
				escaped = '\t';
				return global::Unity.VisualScripting.FullSerializer.fsResult.Success;
			case '0':
				TryMoveNext();
				escaped = '\0';
				return global::Unity.VisualScripting.FullSerializer.fsResult.Success;
			case 'u':
				TryMoveNext();
				if (IsHex(Character(0)) && IsHex(Character(1)) && IsHex(Character(2)) && IsHex(Character(3)))
				{
					uint num = ParseUnicode(Character(0), Character(1), Character(2), Character(3));
					TryMoveNext();
					TryMoveNext();
					TryMoveNext();
					TryMoveNext();
					escaped = (char)num;
					return global::Unity.VisualScripting.FullSerializer.fsResult.Success;
				}
				escaped = '\0';
				return MakeFailure($"invalid escape sequence '\\u{Character(0)}{Character(1)}{Character(2)}{Character(3)}'\n");
			default:
				escaped = '\0';
				return MakeFailure($"Invalid escape sequence \\{Character()}");
			}
		}
	}
}
