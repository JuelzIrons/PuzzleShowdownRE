namespace UnityEngine.InputSystem.Utilities
{
	internal struct JsonParser
	{
		public enum JsonValueType
		{
			None = 0,
			Bool = 1,
			Real = 2,
			Integer = 3,
			String = 4,
			Array = 5,
			Object = 6,
			Any = 7
		}

		public struct JsonString : global::System.IEquatable<global::UnityEngine.InputSystem.Utilities.JsonParser.JsonString>
		{
			public global::UnityEngine.InputSystem.Utilities.Substring text;

			public bool hasEscapes;

			public override string ToString()
			{
				if (!hasEscapes)
				{
					return text.ToString();
				}
				global::System.Text.StringBuilder stringBuilder = new global::System.Text.StringBuilder();
				int length = text.length;
				for (int i = 0; i < length; i++)
				{
					char c = text[i];
					if (c == '\\')
					{
						i++;
						if (i == length)
						{
							break;
						}
						c = text[i];
					}
					stringBuilder.Append(c);
				}
				return stringBuilder.ToString();
			}

			public bool Equals(global::UnityEngine.InputSystem.Utilities.JsonParser.JsonString other)
			{
				if (hasEscapes == other.hasEscapes)
				{
					return global::UnityEngine.InputSystem.Utilities.Substring.Compare(text, other.text, global::System.StringComparison.InvariantCultureIgnoreCase) == 0;
				}
				int length = text.length;
				int length2 = other.text.length;
				int num = 0;
				int num2 = 0;
				while (num < length && num2 < length2)
				{
					char c = text[num];
					char c2 = other.text[num2];
					if (c == '\\')
					{
						num++;
						if (num == length)
						{
							return false;
						}
						c = text[num];
					}
					if (c2 == '\\')
					{
						num2++;
						if (num2 == length2)
						{
							return false;
						}
						c2 = other.text[num2];
					}
					if (char.ToUpperInvariant(c) != char.ToUpperInvariant(c2))
					{
						return false;
					}
					num++;
					num2++;
				}
				if (num == length)
				{
					return num2 == length2;
				}
				return false;
			}

			public override bool Equals(object obj)
			{
				if (obj is global::UnityEngine.InputSystem.Utilities.JsonParser.JsonString other)
				{
					return Equals(other);
				}
				return false;
			}

			public override int GetHashCode()
			{
				return (text.GetHashCode() * 397) ^ hasEscapes.GetHashCode();
			}

			public static bool operator ==(global::UnityEngine.InputSystem.Utilities.JsonParser.JsonString left, global::UnityEngine.InputSystem.Utilities.JsonParser.JsonString right)
			{
				return left.Equals(right);
			}

			public static bool operator !=(global::UnityEngine.InputSystem.Utilities.JsonParser.JsonString left, global::UnityEngine.InputSystem.Utilities.JsonParser.JsonString right)
			{
				return !left.Equals(right);
			}

			public static implicit operator global::UnityEngine.InputSystem.Utilities.JsonParser.JsonString(string str)
			{
				return new global::UnityEngine.InputSystem.Utilities.JsonParser.JsonString
				{
					text = str
				};
			}
		}

		public struct JsonValue : global::System.IEquatable<global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValue>
		{
			public global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValueType type;

			public bool boolValue;

			public double realValue;

			public long integerValue;

			public global::UnityEngine.InputSystem.Utilities.JsonParser.JsonString stringValue;

			public global::System.Collections.Generic.List<global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValue> arrayValue;

			public global::System.Collections.Generic.Dictionary<string, global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValue> objectValue;

			public object anyValue;

			public bool ToBoolean()
			{
				return type switch
				{
					global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValueType.Bool => boolValue, 
					global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValueType.Integer => integerValue != 0, 
					global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValueType.Real => global::UnityEngine.InputSystem.Utilities.NumberHelpers.Approximately(0.0, realValue), 
					global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValueType.String => global::System.Convert.ToBoolean(ToString()), 
					_ => false, 
				};
			}

			public long ToInteger()
			{
				return type switch
				{
					global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValueType.Bool => boolValue ? 1 : 0, 
					global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValueType.Integer => integerValue, 
					global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValueType.Real => (long)realValue, 
					global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValueType.String => global::System.Convert.ToInt64(ToString()), 
					_ => 0L, 
				};
			}

			public double ToDouble()
			{
				return type switch
				{
					global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValueType.Bool => boolValue ? 1 : 0, 
					global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValueType.Integer => integerValue, 
					global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValueType.Real => realValue, 
					global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValueType.String => global::System.Convert.ToSingle(ToString()), 
					_ => 0.0, 
				};
			}

			public override string ToString()
			{
				switch (type)
				{
				case global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValueType.None:
					return "null";
				case global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValueType.Bool:
					return boolValue.ToString();
				case global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValueType.Integer:
					return integerValue.ToString(global::System.Globalization.CultureInfo.InvariantCulture);
				case global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValueType.Real:
					return realValue.ToString(global::System.Globalization.CultureInfo.InvariantCulture);
				case global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValueType.String:
					return stringValue.ToString();
				case global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValueType.Array:
					if (arrayValue == null)
					{
						return "[]";
					}
					return "[" + string.Join(",", global::System.Linq.Enumerable.Select(arrayValue, (global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValue x) => x.ToString())) + "]";
				case global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValueType.Object:
				{
					if (objectValue == null)
					{
						return "{}";
					}
					global::System.Collections.Generic.IEnumerable<string> values = global::System.Linq.Enumerable.Select(objectValue, (global::System.Collections.Generic.KeyValuePair<string, global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValue> pair) => $"\"{pair.Key}\" : \"{pair.Value}\"");
					return "{" + string.Join(",", values) + "}";
				}
				case global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValueType.Any:
					return anyValue.ToString();
				default:
					return base.ToString();
				}
			}

			public static implicit operator global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValue(bool val)
			{
				return new global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValue
				{
					type = global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValueType.Bool,
					boolValue = val
				};
			}

			public static implicit operator global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValue(long val)
			{
				return new global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValue
				{
					type = global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValueType.Integer,
					integerValue = val
				};
			}

			public static implicit operator global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValue(double val)
			{
				return new global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValue
				{
					type = global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValueType.Real,
					realValue = val
				};
			}

			public static implicit operator global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValue(string str)
			{
				return new global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValue
				{
					type = global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValueType.String,
					stringValue = new global::UnityEngine.InputSystem.Utilities.JsonParser.JsonString
					{
						text = str
					}
				};
			}

			public static implicit operator global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValue(global::UnityEngine.InputSystem.Utilities.JsonParser.JsonString str)
			{
				return new global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValue
				{
					type = global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValueType.String,
					stringValue = str
				};
			}

			public static implicit operator global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValue(global::System.Collections.Generic.List<global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValue> array)
			{
				return new global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValue
				{
					type = global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValueType.Array,
					arrayValue = array
				};
			}

			public static implicit operator global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValue(global::System.Collections.Generic.Dictionary<string, global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValue> obj)
			{
				return new global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValue
				{
					type = global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValueType.Object,
					objectValue = obj
				};
			}

			public static implicit operator global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValue(global::System.Enum val)
			{
				return new global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValue
				{
					type = global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValueType.Any,
					anyValue = val
				};
			}

			public bool Equals(global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValue other)
			{
				if (type == other.type)
				{
					return type switch
					{
						global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValueType.None => true, 
						global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValueType.Bool => boolValue == other.boolValue, 
						global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValueType.Integer => integerValue == other.integerValue, 
						global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValueType.Real => global::UnityEngine.InputSystem.Utilities.NumberHelpers.Approximately(realValue, other.realValue), 
						global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValueType.String => stringValue == other.stringValue, 
						global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValueType.Object => throw new global::System.NotImplementedException(), 
						global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValueType.Array => throw new global::System.NotImplementedException(), 
						global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValueType.Any => anyValue.Equals(other.anyValue), 
						_ => false, 
					};
				}
				if (anyValue != null)
				{
					return Equals(anyValue, other);
				}
				if (other.anyValue != null)
				{
					return Equals(other.anyValue, this);
				}
				return false;
			}

			private static bool Equals(object obj, global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValue value)
			{
				if (obj == null)
				{
					return false;
				}
				if (obj is global::System.Text.RegularExpressions.Regex regex)
				{
					return regex.IsMatch(value.ToString());
				}
				if (obj is string text)
				{
					switch (value.type)
					{
					case global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValueType.String:
						return value.stringValue == text;
					case global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValueType.Integer:
					{
						if (long.TryParse(text, out var result))
						{
							return result == value.integerValue;
						}
						return false;
					}
					case global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValueType.Real:
					{
						if (double.TryParse(text, out var result2))
						{
							return global::UnityEngine.InputSystem.Utilities.NumberHelpers.Approximately(result2, value.realValue);
						}
						return false;
					}
					case global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValueType.Bool:
						if (value.boolValue)
						{
							if (!(text == "True") && !(text == "true"))
							{
								return text == "1";
							}
							return true;
						}
						if (!(text == "False") && !(text == "false"))
						{
							return text == "0";
						}
						return true;
					}
				}
				if (obj is float num)
				{
					if (value.type == global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValueType.Real)
					{
						return global::UnityEngine.InputSystem.Utilities.NumberHelpers.Approximately(num, value.realValue);
					}
					if (value.type == global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValueType.String)
					{
						if (float.TryParse(value.ToString(), out var result3))
						{
							return global::UnityEngine.Mathf.Approximately(num, result3);
						}
						return false;
					}
				}
				if (obj is double a)
				{
					if (value.type == global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValueType.Real)
					{
						return global::UnityEngine.InputSystem.Utilities.NumberHelpers.Approximately(a, value.realValue);
					}
					if (value.type == global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValueType.String)
					{
						if (double.TryParse(value.ToString(), out var result4))
						{
							return global::UnityEngine.InputSystem.Utilities.NumberHelpers.Approximately(a, result4);
						}
						return false;
					}
				}
				if (obj is int num2)
				{
					if (value.type == global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValueType.Integer)
					{
						return num2 == value.integerValue;
					}
					if (value.type == global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValueType.String)
					{
						if (int.TryParse(value.ToString(), out var result5))
						{
							return num2 == result5;
						}
						return false;
					}
				}
				if (obj is long num3)
				{
					if (value.type == global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValueType.Integer)
					{
						return num3 == value.integerValue;
					}
					if (value.type == global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValueType.String)
					{
						if (long.TryParse(value.ToString(), out var result6))
						{
							return num3 == result6;
						}
						return false;
					}
				}
				if (obj is bool flag)
				{
					if (value.type == global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValueType.Bool)
					{
						return flag == value.boolValue;
					}
					if (value.type == global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValueType.String)
					{
						if (flag)
						{
							if (!(value.stringValue == "true") && !(value.stringValue == "True"))
							{
								return value.stringValue == "1";
							}
							return true;
						}
						if (!(value.stringValue == "false") && !(value.stringValue == "False"))
						{
							return value.stringValue == "0";
						}
						return true;
					}
				}
				if (obj is global::System.Enum)
				{
					if (value.type == global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValueType.Integer)
					{
						return global::System.Convert.ToInt64(obj) == value.integerValue;
					}
					if (value.type == global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValueType.String)
					{
						return value.stringValue == global::System.Enum.GetName(obj.GetType(), obj);
					}
				}
				return false;
			}

			public override bool Equals(object obj)
			{
				if (obj is global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValue other)
				{
					return Equals(other);
				}
				return false;
			}

			public override int GetHashCode()
			{
				return ((((((((((((((int)type * 397) ^ boolValue.GetHashCode()) * 397) ^ realValue.GetHashCode()) * 397) ^ integerValue.GetHashCode()) * 397) ^ stringValue.GetHashCode()) * 397) ^ ((arrayValue != null) ? arrayValue.GetHashCode() : 0)) * 397) ^ ((objectValue != null) ? objectValue.GetHashCode() : 0)) * 397) ^ ((anyValue != null) ? anyValue.GetHashCode() : 0);
			}

			public static bool operator ==(global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValue left, global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValue right)
			{
				return left.Equals(right);
			}

			public static bool operator !=(global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValue left, global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValue right)
			{
				return !left.Equals(right);
			}
		}

		private readonly string m_Text;

		private readonly int m_Length;

		private int m_Position;

		private bool m_MatchAnyElementInArray;

		private bool m_DryRun;

		public bool isAtEnd => m_Position >= m_Length;

		public JsonParser(string json)
		{
			this = default(global::UnityEngine.InputSystem.Utilities.JsonParser);
			if (json == null)
			{
				throw new global::System.ArgumentNullException("json");
			}
			m_Text = json;
			m_Length = json.Length;
		}

		public void Reset()
		{
			m_Position = 0;
			m_MatchAnyElementInArray = false;
			m_DryRun = false;
		}

		public override string ToString()
		{
			if (m_Text != null)
			{
				return $"{m_Position}: {m_Text.Substring(m_Position)}";
			}
			return base.ToString();
		}

		public bool NavigateToProperty(string path)
		{
			if (string.IsNullOrEmpty(path))
			{
				throw new global::System.ArgumentNullException("path");
			}
			int length = path.Length;
			int i = 0;
			m_DryRun = true;
			if (!ParseToken('{'))
			{
				return false;
			}
			while (m_Position < m_Length && i < length)
			{
				SkipWhitespace();
				if (m_Position == m_Length)
				{
					return false;
				}
				if (m_Text[m_Position] != '"')
				{
					return false;
				}
				m_Position++;
				int num = i;
				for (; i < length; i++)
				{
					char c = path[i];
					if (c == '/' || c == '[' || m_Text[m_Position] != c)
					{
						break;
					}
					m_Position++;
				}
				if (m_Position < m_Length && m_Text[m_Position] == '"' && (i >= length || path[i] == '/' || path[i] == '['))
				{
					m_Position++;
					if (!SkipToValue())
					{
						return false;
					}
					if (i >= length)
					{
						return true;
					}
					if (path[i] == '/')
					{
						i++;
						if (!ParseToken('{'))
						{
							return false;
						}
					}
					else if (path[i] == '[')
					{
						i++;
						if (i == length)
						{
							throw new global::System.ArgumentException("Malformed JSON property path: " + path, "path");
						}
						if (path[i] != ']')
						{
							throw new global::System.NotImplementedException("Navigating to specific array element");
						}
						m_MatchAnyElementInArray = true;
						i++;
						if (i == length)
						{
							return true;
						}
					}
				}
				else
				{
					i = num;
					while (m_Position < m_Length && m_Text[m_Position] != '"')
					{
						m_Position++;
					}
					if (m_Position == m_Length || m_Text[m_Position] != '"')
					{
						return false;
					}
					m_Position++;
					if (!SkipToValue() || !ParseValue())
					{
						return false;
					}
					SkipWhitespace();
					if (m_Position == m_Length || m_Text[m_Position] == '}' || m_Text[m_Position] != ',')
					{
						return false;
					}
					m_Position++;
				}
			}
			return false;
		}

		public bool CurrentPropertyHasValueEqualTo(global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValue expectedValue)
		{
			int position = m_Position;
			m_DryRun = false;
			if (!ParseValue(out var result))
			{
				m_Position = position;
				return false;
			}
			m_Position = position;
			bool flag = false;
			if (result.type == global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValueType.Array && m_MatchAnyElementInArray)
			{
				global::System.Collections.Generic.List<global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValue> arrayValue = result.arrayValue;
				int num = 0;
				while (!flag && num < arrayValue.Count)
				{
					flag = arrayValue[num] == expectedValue;
					num++;
				}
			}
			else
			{
				flag = result == expectedValue;
			}
			return flag;
		}

		public bool ParseToken(char token)
		{
			SkipWhitespace();
			if (m_Position == m_Length)
			{
				return false;
			}
			if (m_Text[m_Position] != token)
			{
				return false;
			}
			m_Position++;
			SkipWhitespace();
			return m_Position < m_Length;
		}

		public bool ParseValue()
		{
			global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValue result;
			return ParseValue(out result);
		}

		public bool ParseValue(out global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValue result)
		{
			result = default(global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValue);
			SkipWhitespace();
			if (m_Position == m_Length)
			{
				return false;
			}
			switch (m_Text[m_Position])
			{
			case '"':
				if (ParseStringValue(out result))
				{
					return true;
				}
				break;
			case '[':
				if (ParseArrayValue(out result))
				{
					return true;
				}
				break;
			case '{':
				if (ParseObjectValue(out result))
				{
					return true;
				}
				break;
			case 'f':
			case 't':
				if (ParseBooleanValue(out result))
				{
					return true;
				}
				break;
			case 'n':
				if (ParseNullValue(out result))
				{
					return true;
				}
				break;
			default:
				if (ParseNumber(out result))
				{
					return true;
				}
				break;
			}
			return false;
		}

		public bool ParseStringValue(out global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValue result)
		{
			result = default(global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValue);
			SkipWhitespace();
			if (m_Position == m_Length || m_Text[m_Position] != '"')
			{
				return false;
			}
			m_Position++;
			int position = m_Position;
			bool hasEscapes = false;
			for (; m_Position < m_Length; m_Position++)
			{
				switch (m_Text[m_Position])
				{
				case '\\':
					m_Position++;
					if (m_Position != m_Length)
					{
						hasEscapes = true;
						continue;
					}
					break;
				case '"':
					m_Position++;
					result = new global::UnityEngine.InputSystem.Utilities.JsonParser.JsonString
					{
						text = new global::UnityEngine.InputSystem.Utilities.Substring(m_Text, position, m_Position - position - 1),
						hasEscapes = hasEscapes
					};
					return true;
				default:
					continue;
				}
				break;
			}
			return false;
		}

		public bool ParseArrayValue(out global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValue result)
		{
			result = default(global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValue);
			SkipWhitespace();
			if (m_Position == m_Length || m_Text[m_Position] != '[')
			{
				return false;
			}
			m_Position++;
			if (m_Position == m_Length)
			{
				return false;
			}
			if (m_Text[m_Position] == ']')
			{
				result = new global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValue
				{
					type = global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValueType.Array
				};
				m_Position++;
				return true;
			}
			global::System.Collections.Generic.List<global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValue> list = null;
			if (!m_DryRun)
			{
				list = new global::System.Collections.Generic.List<global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValue>();
			}
			while (m_Position < m_Length)
			{
				if (!ParseValue(out var result2))
				{
					return false;
				}
				if (!m_DryRun)
				{
					list.Add(result2);
				}
				SkipWhitespace();
				if (m_Position == m_Length)
				{
					return false;
				}
				switch (m_Text[m_Position])
				{
				case ']':
					m_Position++;
					if (!m_DryRun)
					{
						result = list;
					}
					return true;
				case ',':
					m_Position++;
					break;
				}
			}
			return false;
		}

		public bool ParseObjectValue(out global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValue result)
		{
			result = default(global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValue);
			if (!ParseToken('{'))
			{
				return false;
			}
			if (m_Position < m_Length && m_Text[m_Position] == '}')
			{
				result = new global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValue
				{
					type = global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValueType.Object
				};
				m_Position++;
				return true;
			}
			while (m_Position < m_Length)
			{
				if (!ParseStringValue(out var _))
				{
					return false;
				}
				if (!SkipToValue())
				{
					return false;
				}
				if (!ParseValue(out var _))
				{
					return false;
				}
				if (!m_DryRun)
				{
					throw new global::System.NotImplementedException();
				}
				SkipWhitespace();
				if (m_Position < m_Length && m_Text[m_Position] == '}')
				{
					if (!m_DryRun)
					{
						throw new global::System.NotImplementedException();
					}
					m_Position++;
					return true;
				}
			}
			return false;
		}

		public bool ParseNumber(out global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValue result)
		{
			result = default(global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValue);
			SkipWhitespace();
			if (m_Position == m_Length)
			{
				return false;
			}
			bool flag = false;
			bool flag2 = false;
			long num = 0L;
			double num2 = 0.0;
			double num3 = 10.0;
			int num4 = 0;
			if (m_Text[m_Position] == '-')
			{
				flag = true;
				m_Position++;
			}
			if (m_Position == m_Length || !char.IsDigit(m_Text[m_Position]))
			{
				return false;
			}
			while (m_Position < m_Length)
			{
				char c = m_Text[m_Position];
				if (c == '.' || c < '0' || c > '9')
				{
					break;
				}
				num = num * 10 + c - 48;
				m_Position++;
			}
			if (m_Position < m_Length && m_Text[m_Position] == '.')
			{
				flag2 = true;
				m_Position++;
				if (m_Position == m_Length || !char.IsDigit(m_Text[m_Position]))
				{
					return false;
				}
				while (m_Position < m_Length)
				{
					char c2 = m_Text[m_Position];
					if (c2 < '0' || c2 > '9')
					{
						break;
					}
					num2 = (double)(c2 - 48) / num3 + num2;
					num3 *= 10.0;
					m_Position++;
				}
			}
			if (m_Position < m_Length && (m_Text[m_Position] == 'e' || m_Text[m_Position] == 'E'))
			{
				m_Position++;
				bool flag3 = false;
				if (m_Position < m_Length && m_Text[m_Position] == '-')
				{
					flag3 = true;
					m_Position++;
				}
				else if (m_Position < m_Length && m_Text[m_Position] == '+')
				{
					m_Position++;
				}
				int num5 = 1;
				while (m_Position < m_Length && char.IsDigit(m_Text[m_Position]))
				{
					int num6 = m_Text[m_Position] - 48;
					num4 *= num5;
					num4 += num6;
					num5 *= 10;
					m_Position++;
				}
				if (flag3)
				{
					num4 *= -1;
				}
			}
			if (!m_DryRun)
			{
				if (!flag2 && num4 == 0)
				{
					if (flag)
					{
						result = -num;
					}
					else
					{
						result = num;
					}
				}
				else
				{
					float num7 = ((!flag) ? ((float)((double)num + num2)) : ((float)(0.0 - ((double)num + num2))));
					if (num4 != 0)
					{
						num7 *= global::UnityEngine.Mathf.Pow(10f, num4);
					}
					result = num7;
				}
			}
			return true;
		}

		public bool ParseBooleanValue(out global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValue result)
		{
			SkipWhitespace();
			if (SkipString("true"))
			{
				result = true;
				return true;
			}
			if (SkipString("false"))
			{
				result = false;
				return true;
			}
			result = default(global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValue);
			return false;
		}

		public bool ParseNullValue(out global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValue result)
		{
			result = default(global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValue);
			return SkipString("null");
		}

		public bool SkipToValue()
		{
			SkipWhitespace();
			if (m_Position == m_Length || m_Text[m_Position] != ':')
			{
				return false;
			}
			m_Position++;
			SkipWhitespace();
			return true;
		}

		private bool SkipString(string text)
		{
			SkipWhitespace();
			int length = text.Length;
			if (m_Position + length >= m_Length)
			{
				return false;
			}
			for (int i = 0; i < length; i++)
			{
				if (m_Text[m_Position + i] != text[i])
				{
					return false;
				}
			}
			m_Position += length;
			return true;
		}

		private void SkipWhitespace()
		{
			while (m_Position < m_Length && char.IsWhiteSpace(m_Text[m_Position]))
			{
				m_Position++;
			}
		}
	}
}
