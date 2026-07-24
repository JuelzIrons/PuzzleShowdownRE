namespace Newtonsoft.Json.Linq.JsonPath
{
	internal class JPath
	{
		private static readonly char[] FloatCharacters = new char[3] { '.', 'E', 'e' };

		private readonly string _expression;

		private int _currentIndex;

		public global::System.Collections.Generic.List<global::Newtonsoft.Json.Linq.JsonPath.PathFilter> Filters { get; }

		public JPath(string expression)
		{
			global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(expression, "expression");
			_expression = expression;
			Filters = new global::System.Collections.Generic.List<global::Newtonsoft.Json.Linq.JsonPath.PathFilter>();
			ParseMain();
		}

		private void ParseMain()
		{
			int currentIndex = _currentIndex;
			EatWhitespace();
			if (_expression.Length == _currentIndex)
			{
				return;
			}
			if (_expression[_currentIndex] == '$')
			{
				if (_expression.Length == 1)
				{
					return;
				}
				char c = _expression[_currentIndex + 1];
				if (c == '.' || c == '[')
				{
					_currentIndex++;
					currentIndex = _currentIndex;
				}
			}
			if (!ParsePath(Filters, currentIndex, query: false))
			{
				int currentIndex2 = _currentIndex;
				EatWhitespace();
				if (_currentIndex < _expression.Length)
				{
					throw new global::Newtonsoft.Json.JsonException("Unexpected character while parsing path: " + _expression[currentIndex2]);
				}
			}
		}

		private bool ParsePath(global::System.Collections.Generic.List<global::Newtonsoft.Json.Linq.JsonPath.PathFilter> filters, int currentPartStartIndex, bool query)
		{
			bool scan = false;
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			while (_currentIndex < _expression.Length && !flag3)
			{
				char c = _expression[_currentIndex];
				switch (c)
				{
				case '(':
				case '[':
					if (_currentIndex > currentPartStartIndex)
					{
						string text = _expression.Substring(currentPartStartIndex, _currentIndex - currentPartStartIndex);
						if (text == "*")
						{
							text = null;
						}
						filters.Add(CreatePathFilter(text, scan));
						scan = false;
					}
					filters.Add(ParseIndexer(c, scan));
					scan = false;
					_currentIndex++;
					currentPartStartIndex = _currentIndex;
					flag = true;
					flag2 = false;
					break;
				case ')':
				case ']':
					flag3 = true;
					break;
				case ' ':
					if (_currentIndex < _expression.Length)
					{
						flag3 = true;
					}
					break;
				case '.':
					if (_currentIndex > currentPartStartIndex)
					{
						string text2 = _expression.Substring(currentPartStartIndex, _currentIndex - currentPartStartIndex);
						if (text2 == "*")
						{
							text2 = null;
						}
						filters.Add(CreatePathFilter(text2, scan));
						scan = false;
					}
					if (_currentIndex + 1 < _expression.Length && _expression[_currentIndex + 1] == '.')
					{
						scan = true;
						_currentIndex++;
					}
					_currentIndex++;
					currentPartStartIndex = _currentIndex;
					flag = false;
					flag2 = true;
					break;
				default:
					if (query && (c == '=' || c == '<' || c == '!' || c == '>' || c == '|' || c == '&'))
					{
						flag3 = true;
						break;
					}
					if (flag)
					{
						throw new global::Newtonsoft.Json.JsonException("Unexpected character following indexer: " + c);
					}
					_currentIndex++;
					break;
				}
			}
			bool flag4 = _currentIndex == _expression.Length;
			if (_currentIndex > currentPartStartIndex)
			{
				string text3 = _expression.Substring(currentPartStartIndex, _currentIndex - currentPartStartIndex).TrimEnd(global::System.Array.Empty<char>());
				if (text3 == "*")
				{
					text3 = null;
				}
				filters.Add(CreatePathFilter(text3, scan));
			}
			else if (flag2 && (flag4 || query))
			{
				throw new global::Newtonsoft.Json.JsonException("Unexpected end while parsing path.");
			}
			return flag4;
		}

		private static global::Newtonsoft.Json.Linq.JsonPath.PathFilter CreatePathFilter(string? member, bool scan)
		{
			if (!scan)
			{
				return new global::Newtonsoft.Json.Linq.JsonPath.FieldFilter(member);
			}
			return new global::Newtonsoft.Json.Linq.JsonPath.ScanFilter(member);
		}

		private global::Newtonsoft.Json.Linq.JsonPath.PathFilter ParseIndexer(char indexerOpenChar, bool scan)
		{
			_currentIndex++;
			char indexerCloseChar = ((indexerOpenChar == '[') ? ']' : ')');
			EnsureLength("Path ended with open indexer.");
			EatWhitespace();
			if (_expression[_currentIndex] == '\'')
			{
				return ParseQuotedField(indexerCloseChar, scan);
			}
			if (_expression[_currentIndex] == '?')
			{
				return ParseQuery(indexerCloseChar, scan);
			}
			return ParseArrayIndexer(indexerCloseChar);
		}

		private global::Newtonsoft.Json.Linq.JsonPath.PathFilter ParseArrayIndexer(char indexerCloseChar)
		{
			int currentIndex = _currentIndex;
			int? num = null;
			global::System.Collections.Generic.List<int> list = null;
			int num2 = 0;
			int? start = null;
			int? end = null;
			int? step = null;
			while (_currentIndex < _expression.Length)
			{
				char c = _expression[_currentIndex];
				if (c == ' ')
				{
					num = _currentIndex;
					EatWhitespace();
					continue;
				}
				if (c == indexerCloseChar)
				{
					int num3 = (num ?? _currentIndex) - currentIndex;
					if (list != null)
					{
						if (num3 == 0)
						{
							throw new global::Newtonsoft.Json.JsonException("Array index expected.");
						}
						int item = global::System.Convert.ToInt32(_expression.Substring(currentIndex, num3), global::System.Globalization.CultureInfo.InvariantCulture);
						list.Add(item);
						return new global::Newtonsoft.Json.Linq.JsonPath.ArrayMultipleIndexFilter(list);
					}
					if (num2 > 0)
					{
						if (num3 > 0)
						{
							int value = global::System.Convert.ToInt32(_expression.Substring(currentIndex, num3), global::System.Globalization.CultureInfo.InvariantCulture);
							if (num2 == 1)
							{
								end = value;
							}
							else
							{
								step = value;
							}
						}
						return new global::Newtonsoft.Json.Linq.JsonPath.ArraySliceFilter
						{
							Start = start,
							End = end,
							Step = step
						};
					}
					if (num3 == 0)
					{
						throw new global::Newtonsoft.Json.JsonException("Array index expected.");
					}
					int value2 = global::System.Convert.ToInt32(_expression.Substring(currentIndex, num3), global::System.Globalization.CultureInfo.InvariantCulture);
					return new global::Newtonsoft.Json.Linq.JsonPath.ArrayIndexFilter
					{
						Index = value2
					};
				}
				switch (c)
				{
				case ',':
				{
					int num5 = (num ?? _currentIndex) - currentIndex;
					if (num5 == 0)
					{
						throw new global::Newtonsoft.Json.JsonException("Array index expected.");
					}
					if (list == null)
					{
						list = new global::System.Collections.Generic.List<int>();
					}
					string value4 = _expression.Substring(currentIndex, num5);
					list.Add(global::System.Convert.ToInt32(value4, global::System.Globalization.CultureInfo.InvariantCulture));
					_currentIndex++;
					EatWhitespace();
					currentIndex = _currentIndex;
					num = null;
					break;
				}
				case '*':
					_currentIndex++;
					EnsureLength("Path ended with open indexer.");
					EatWhitespace();
					if (_expression[_currentIndex] != indexerCloseChar)
					{
						throw new global::Newtonsoft.Json.JsonException("Unexpected character while parsing path indexer: " + c);
					}
					return new global::Newtonsoft.Json.Linq.JsonPath.ArrayIndexFilter();
				case ':':
				{
					int num4 = (num ?? _currentIndex) - currentIndex;
					if (num4 > 0)
					{
						int value3 = global::System.Convert.ToInt32(_expression.Substring(currentIndex, num4), global::System.Globalization.CultureInfo.InvariantCulture);
						switch (num2)
						{
						case 0:
							start = value3;
							break;
						case 1:
							end = value3;
							break;
						default:
							step = value3;
							break;
						}
					}
					num2++;
					_currentIndex++;
					EatWhitespace();
					currentIndex = _currentIndex;
					num = null;
					break;
				}
				default:
					if (!char.IsDigit(c) && c != '-')
					{
						throw new global::Newtonsoft.Json.JsonException("Unexpected character while parsing path indexer: " + c);
					}
					if (num.HasValue)
					{
						throw new global::Newtonsoft.Json.JsonException("Unexpected character while parsing path indexer: " + c);
					}
					_currentIndex++;
					break;
				}
			}
			throw new global::Newtonsoft.Json.JsonException("Path ended with open indexer.");
		}

		private void EatWhitespace()
		{
			while (_currentIndex < _expression.Length && _expression[_currentIndex] == ' ')
			{
				_currentIndex++;
			}
		}

		private global::Newtonsoft.Json.Linq.JsonPath.PathFilter ParseQuery(char indexerCloseChar, bool scan)
		{
			_currentIndex++;
			EnsureLength("Path ended with open indexer.");
			if (_expression[_currentIndex] != '(')
			{
				throw new global::Newtonsoft.Json.JsonException("Unexpected character while parsing path indexer: " + _expression[_currentIndex]);
			}
			_currentIndex++;
			global::Newtonsoft.Json.Linq.JsonPath.QueryExpression expression = ParseExpression();
			_currentIndex++;
			EnsureLength("Path ended with open indexer.");
			EatWhitespace();
			if (_expression[_currentIndex] != indexerCloseChar)
			{
				throw new global::Newtonsoft.Json.JsonException("Unexpected character while parsing path indexer: " + _expression[_currentIndex]);
			}
			if (!scan)
			{
				return new global::Newtonsoft.Json.Linq.JsonPath.QueryFilter(expression);
			}
			return new global::Newtonsoft.Json.Linq.JsonPath.QueryScanFilter(expression);
		}

		private bool TryParseExpression(out global::System.Collections.Generic.List<global::Newtonsoft.Json.Linq.JsonPath.PathFilter>? expressionPath)
		{
			if (_expression[_currentIndex] == '$')
			{
				expressionPath = new global::System.Collections.Generic.List<global::Newtonsoft.Json.Linq.JsonPath.PathFilter> { global::Newtonsoft.Json.Linq.JsonPath.RootFilter.Instance };
			}
			else
			{
				if (_expression[_currentIndex] != '@')
				{
					expressionPath = null;
					return false;
				}
				expressionPath = new global::System.Collections.Generic.List<global::Newtonsoft.Json.Linq.JsonPath.PathFilter>();
			}
			_currentIndex++;
			if (ParsePath(expressionPath, _currentIndex, query: true))
			{
				throw new global::Newtonsoft.Json.JsonException("Path ended with open query.");
			}
			return true;
		}

		private global::Newtonsoft.Json.JsonException CreateUnexpectedCharacterException()
		{
			return new global::Newtonsoft.Json.JsonException("Unexpected character while parsing path query: " + _expression[_currentIndex]);
		}

		private object ParseSide()
		{
			EatWhitespace();
			if (TryParseExpression(out global::System.Collections.Generic.List<global::Newtonsoft.Json.Linq.JsonPath.PathFilter> expressionPath))
			{
				EatWhitespace();
				EnsureLength("Path ended with open query.");
				return expressionPath;
			}
			if (TryParseValue(out object value))
			{
				EatWhitespace();
				EnsureLength("Path ended with open query.");
				return new global::Newtonsoft.Json.Linq.JValue(value);
			}
			throw CreateUnexpectedCharacterException();
		}

		private global::Newtonsoft.Json.Linq.JsonPath.QueryExpression ParseExpression()
		{
			global::Newtonsoft.Json.Linq.JsonPath.QueryExpression queryExpression = null;
			global::Newtonsoft.Json.Linq.JsonPath.CompositeExpression compositeExpression = null;
			while (_currentIndex < _expression.Length)
			{
				object left = ParseSide();
				object right = null;
				global::Newtonsoft.Json.Linq.JsonPath.QueryOperator queryOperator;
				if (_expression[_currentIndex] == ')' || _expression[_currentIndex] == '|' || _expression[_currentIndex] == '&')
				{
					queryOperator = global::Newtonsoft.Json.Linq.JsonPath.QueryOperator.Exists;
				}
				else
				{
					queryOperator = ParseOperator();
					right = ParseSide();
				}
				global::Newtonsoft.Json.Linq.JsonPath.BooleanQueryExpression booleanQueryExpression = new global::Newtonsoft.Json.Linq.JsonPath.BooleanQueryExpression(queryOperator, left, right);
				if (_expression[_currentIndex] == ')')
				{
					if (compositeExpression != null)
					{
						compositeExpression.Expressions.Add(booleanQueryExpression);
						return queryExpression;
					}
					return booleanQueryExpression;
				}
				if (_expression[_currentIndex] == '&')
				{
					if (!Match("&&"))
					{
						throw CreateUnexpectedCharacterException();
					}
					if (compositeExpression == null || compositeExpression.Operator != global::Newtonsoft.Json.Linq.JsonPath.QueryOperator.And)
					{
						global::Newtonsoft.Json.Linq.JsonPath.CompositeExpression compositeExpression2 = new global::Newtonsoft.Json.Linq.JsonPath.CompositeExpression(global::Newtonsoft.Json.Linq.JsonPath.QueryOperator.And);
						compositeExpression?.Expressions.Add(compositeExpression2);
						compositeExpression = compositeExpression2;
						if (queryExpression == null)
						{
							queryExpression = compositeExpression;
						}
					}
					compositeExpression.Expressions.Add(booleanQueryExpression);
				}
				if (_expression[_currentIndex] != '|')
				{
					continue;
				}
				if (!Match("||"))
				{
					throw CreateUnexpectedCharacterException();
				}
				if (compositeExpression == null || compositeExpression.Operator != global::Newtonsoft.Json.Linq.JsonPath.QueryOperator.Or)
				{
					global::Newtonsoft.Json.Linq.JsonPath.CompositeExpression compositeExpression3 = new global::Newtonsoft.Json.Linq.JsonPath.CompositeExpression(global::Newtonsoft.Json.Linq.JsonPath.QueryOperator.Or);
					compositeExpression?.Expressions.Add(compositeExpression3);
					compositeExpression = compositeExpression3;
					if (queryExpression == null)
					{
						queryExpression = compositeExpression;
					}
				}
				compositeExpression.Expressions.Add(booleanQueryExpression);
			}
			throw new global::Newtonsoft.Json.JsonException("Path ended with open query.");
		}

		private bool TryParseValue(out object? value)
		{
			char c = _expression[_currentIndex];
			if (c == '\'')
			{
				value = ReadQuotedString();
				return true;
			}
			if (char.IsDigit(c) || c == '-')
			{
				global::System.Text.StringBuilder stringBuilder = new global::System.Text.StringBuilder();
				stringBuilder.Append(c);
				_currentIndex++;
				while (_currentIndex < _expression.Length)
				{
					c = _expression[_currentIndex];
					if (c == ' ' || c == ')')
					{
						string text = stringBuilder.ToString();
						if (text.IndexOfAny(FloatCharacters) != -1)
						{
							double result2;
							bool result = double.TryParse(text, global::System.Globalization.NumberStyles.Float | global::System.Globalization.NumberStyles.AllowThousands, global::System.Globalization.CultureInfo.InvariantCulture, out result2);
							value = result2;
							return result;
						}
						long result4;
						bool result3 = long.TryParse(text, global::System.Globalization.NumberStyles.Integer, global::System.Globalization.CultureInfo.InvariantCulture, out result4);
						value = result4;
						return result3;
					}
					stringBuilder.Append(c);
					_currentIndex++;
				}
			}
			else
			{
				switch (c)
				{
				case 't':
					if (Match("true"))
					{
						value = true;
						return true;
					}
					break;
				case 'f':
					if (Match("false"))
					{
						value = false;
						return true;
					}
					break;
				case 'n':
					if (Match("null"))
					{
						value = null;
						return true;
					}
					break;
				case '/':
					value = ReadRegexString();
					return true;
				}
			}
			value = null;
			return false;
		}

		private string ReadQuotedString()
		{
			global::System.Text.StringBuilder stringBuilder = new global::System.Text.StringBuilder();
			_currentIndex++;
			while (_currentIndex < _expression.Length)
			{
				char c = _expression[_currentIndex];
				if (c == '\\' && _currentIndex + 1 < _expression.Length)
				{
					_currentIndex++;
					c = _expression[_currentIndex];
					char value;
					switch (c)
					{
					case 'b':
						value = '\b';
						break;
					case 't':
						value = '\t';
						break;
					case 'n':
						value = '\n';
						break;
					case 'f':
						value = '\f';
						break;
					case 'r':
						value = '\r';
						break;
					case '"':
					case '\'':
					case '/':
					case '\\':
						value = c;
						break;
					default:
						throw new global::Newtonsoft.Json.JsonException("Unknown escape character: \\" + c);
					}
					stringBuilder.Append(value);
					_currentIndex++;
				}
				else
				{
					if (c == '\'')
					{
						_currentIndex++;
						return stringBuilder.ToString();
					}
					_currentIndex++;
					stringBuilder.Append(c);
				}
			}
			throw new global::Newtonsoft.Json.JsonException("Path ended with an open string.");
		}

		private string ReadRegexString()
		{
			int currentIndex = _currentIndex;
			_currentIndex++;
			while (_currentIndex < _expression.Length)
			{
				char c = _expression[_currentIndex];
				if (c == '\\' && _currentIndex + 1 < _expression.Length)
				{
					_currentIndex += 2;
					continue;
				}
				if (c == '/')
				{
					_currentIndex++;
					while (_currentIndex < _expression.Length)
					{
						c = _expression[_currentIndex];
						if (!char.IsLetter(c))
						{
							break;
						}
						_currentIndex++;
					}
					return _expression.Substring(currentIndex, _currentIndex - currentIndex);
				}
				_currentIndex++;
			}
			throw new global::Newtonsoft.Json.JsonException("Path ended with an open regex.");
		}

		private bool Match(string s)
		{
			int num = _currentIndex;
			for (int i = 0; i < s.Length; i++)
			{
				if (num < _expression.Length && _expression[num] == s[i])
				{
					num++;
					continue;
				}
				return false;
			}
			_currentIndex = num;
			return true;
		}

		private global::Newtonsoft.Json.Linq.JsonPath.QueryOperator ParseOperator()
		{
			if (_currentIndex + 1 >= _expression.Length)
			{
				throw new global::Newtonsoft.Json.JsonException("Path ended with open query.");
			}
			if (Match("==="))
			{
				return global::Newtonsoft.Json.Linq.JsonPath.QueryOperator.StrictEquals;
			}
			if (Match("=="))
			{
				return global::Newtonsoft.Json.Linq.JsonPath.QueryOperator.Equals;
			}
			if (Match("=~"))
			{
				return global::Newtonsoft.Json.Linq.JsonPath.QueryOperator.RegexEquals;
			}
			if (Match("!=="))
			{
				return global::Newtonsoft.Json.Linq.JsonPath.QueryOperator.StrictNotEquals;
			}
			if (Match("!=") || Match("<>"))
			{
				return global::Newtonsoft.Json.Linq.JsonPath.QueryOperator.NotEquals;
			}
			if (Match("<="))
			{
				return global::Newtonsoft.Json.Linq.JsonPath.QueryOperator.LessThanOrEquals;
			}
			if (Match("<"))
			{
				return global::Newtonsoft.Json.Linq.JsonPath.QueryOperator.LessThan;
			}
			if (Match(">="))
			{
				return global::Newtonsoft.Json.Linq.JsonPath.QueryOperator.GreaterThanOrEquals;
			}
			if (Match(">"))
			{
				return global::Newtonsoft.Json.Linq.JsonPath.QueryOperator.GreaterThan;
			}
			throw new global::Newtonsoft.Json.JsonException("Could not read query operator.");
		}

		private global::Newtonsoft.Json.Linq.JsonPath.PathFilter ParseQuotedField(char indexerCloseChar, bool scan)
		{
			global::System.Collections.Generic.List<string> list = null;
			while (_currentIndex < _expression.Length)
			{
				string text = ReadQuotedString();
				EatWhitespace();
				EnsureLength("Path ended with open indexer.");
				if (_expression[_currentIndex] == indexerCloseChar)
				{
					if (list != null)
					{
						list.Add(text);
						if (!scan)
						{
							return new global::Newtonsoft.Json.Linq.JsonPath.FieldMultipleFilter(list);
						}
						return new global::Newtonsoft.Json.Linq.JsonPath.ScanMultipleFilter(list);
					}
					return CreatePathFilter(text, scan);
				}
				if (_expression[_currentIndex] == ',')
				{
					_currentIndex++;
					EatWhitespace();
					if (list == null)
					{
						list = new global::System.Collections.Generic.List<string>();
					}
					list.Add(text);
					continue;
				}
				throw new global::Newtonsoft.Json.JsonException("Unexpected character while parsing path indexer: " + _expression[_currentIndex]);
			}
			throw new global::Newtonsoft.Json.JsonException("Path ended with open indexer.");
		}

		private void EnsureLength(string message)
		{
			if (_currentIndex >= _expression.Length)
			{
				throw new global::Newtonsoft.Json.JsonException(message);
			}
		}

		internal global::System.Collections.Generic.IEnumerable<global::Newtonsoft.Json.Linq.JToken> Evaluate(global::Newtonsoft.Json.Linq.JToken root, global::Newtonsoft.Json.Linq.JToken t, global::Newtonsoft.Json.Linq.JsonSelectSettings? settings)
		{
			return Evaluate(Filters, root, t, settings);
		}

		internal static global::System.Collections.Generic.IEnumerable<global::Newtonsoft.Json.Linq.JToken> Evaluate(global::System.Collections.Generic.List<global::Newtonsoft.Json.Linq.JsonPath.PathFilter> filters, global::Newtonsoft.Json.Linq.JToken root, global::Newtonsoft.Json.Linq.JToken t, global::Newtonsoft.Json.Linq.JsonSelectSettings? settings)
		{
			global::System.Collections.Generic.IEnumerable<global::Newtonsoft.Json.Linq.JToken> enumerable = new global::Newtonsoft.Json.Linq.JToken[1] { t };
			foreach (global::Newtonsoft.Json.Linq.JsonPath.PathFilter filter in filters)
			{
				enumerable = filter.ExecuteFilter(root, enumerable, settings);
			}
			return enumerable;
		}
	}
}
