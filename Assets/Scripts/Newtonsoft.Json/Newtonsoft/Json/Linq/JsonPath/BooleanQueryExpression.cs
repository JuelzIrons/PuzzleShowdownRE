namespace Newtonsoft.Json.Linq.JsonPath
{
	internal class BooleanQueryExpression : global::Newtonsoft.Json.Linq.JsonPath.QueryExpression
	{
		public readonly object Left;

		public readonly object? Right;

		public BooleanQueryExpression(global::Newtonsoft.Json.Linq.JsonPath.QueryOperator @operator, object left, object? right)
			: base(@operator)
		{
			Left = left;
			Right = right;
		}

		private global::System.Collections.Generic.IEnumerable<global::Newtonsoft.Json.Linq.JToken> GetResult(global::Newtonsoft.Json.Linq.JToken root, global::Newtonsoft.Json.Linq.JToken t, object? o)
		{
			if (o is global::Newtonsoft.Json.Linq.JToken jToken)
			{
				return new global::Newtonsoft.Json.Linq.JToken[1] { jToken };
			}
			if (o is global::System.Collections.Generic.List<global::Newtonsoft.Json.Linq.JsonPath.PathFilter> filters)
			{
				return global::Newtonsoft.Json.Linq.JsonPath.JPath.Evaluate(filters, root, t, null);
			}
			return global::Newtonsoft.Json.Utilities.CollectionUtils.ArrayEmpty<global::Newtonsoft.Json.Linq.JToken>();
		}

		public override bool IsMatch(global::Newtonsoft.Json.Linq.JToken root, global::Newtonsoft.Json.Linq.JToken t, global::Newtonsoft.Json.Linq.JsonSelectSettings? settings)
		{
			if (Operator == global::Newtonsoft.Json.Linq.JsonPath.QueryOperator.Exists)
			{
				return global::System.Linq.Enumerable.Any(GetResult(root, t, Left));
			}
			using (global::System.Collections.Generic.IEnumerator<global::Newtonsoft.Json.Linq.JToken> enumerator = GetResult(root, t, Left).GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					global::System.Collections.Generic.IEnumerable<global::Newtonsoft.Json.Linq.JToken> result = GetResult(root, t, Right);
					global::System.Collections.Generic.ICollection<global::Newtonsoft.Json.Linq.JToken> collection = (result as global::System.Collections.Generic.ICollection<global::Newtonsoft.Json.Linq.JToken>) ?? global::System.Linq.Enumerable.ToList(result);
					do
					{
						global::Newtonsoft.Json.Linq.JToken current = enumerator.Current;
						foreach (global::Newtonsoft.Json.Linq.JToken item in collection)
						{
							if (MatchTokens(current, item, settings))
							{
								return true;
							}
						}
					}
					while (enumerator.MoveNext());
				}
			}
			return false;
		}

		private bool MatchTokens(global::Newtonsoft.Json.Linq.JToken leftResult, global::Newtonsoft.Json.Linq.JToken rightResult, global::Newtonsoft.Json.Linq.JsonSelectSettings? settings)
		{
			if (leftResult is global::Newtonsoft.Json.Linq.JValue jValue && rightResult is global::Newtonsoft.Json.Linq.JValue jValue2)
			{
				switch (Operator)
				{
				case global::Newtonsoft.Json.Linq.JsonPath.QueryOperator.RegexEquals:
					if (RegexEquals(jValue, jValue2, settings))
					{
						return true;
					}
					break;
				case global::Newtonsoft.Json.Linq.JsonPath.QueryOperator.Equals:
					if (EqualsWithStringCoercion(jValue, jValue2))
					{
						return true;
					}
					break;
				case global::Newtonsoft.Json.Linq.JsonPath.QueryOperator.StrictEquals:
					if (EqualsWithStrictMatch(jValue, jValue2))
					{
						return true;
					}
					break;
				case global::Newtonsoft.Json.Linq.JsonPath.QueryOperator.NotEquals:
					if (!EqualsWithStringCoercion(jValue, jValue2))
					{
						return true;
					}
					break;
				case global::Newtonsoft.Json.Linq.JsonPath.QueryOperator.StrictNotEquals:
					if (!EqualsWithStrictMatch(jValue, jValue2))
					{
						return true;
					}
					break;
				case global::Newtonsoft.Json.Linq.JsonPath.QueryOperator.GreaterThan:
					if (jValue.CompareTo(jValue2) > 0)
					{
						return true;
					}
					break;
				case global::Newtonsoft.Json.Linq.JsonPath.QueryOperator.GreaterThanOrEquals:
					if (jValue.CompareTo(jValue2) >= 0)
					{
						return true;
					}
					break;
				case global::Newtonsoft.Json.Linq.JsonPath.QueryOperator.LessThan:
					if (jValue.CompareTo(jValue2) < 0)
					{
						return true;
					}
					break;
				case global::Newtonsoft.Json.Linq.JsonPath.QueryOperator.LessThanOrEquals:
					if (jValue.CompareTo(jValue2) <= 0)
					{
						return true;
					}
					break;
				case global::Newtonsoft.Json.Linq.JsonPath.QueryOperator.Exists:
					return true;
				}
			}
			else
			{
				global::Newtonsoft.Json.Linq.JsonPath.QueryOperator queryOperator = Operator;
				if ((uint)(queryOperator - 2) <= 1u)
				{
					return true;
				}
			}
			return false;
		}

		private static bool RegexEquals(global::Newtonsoft.Json.Linq.JValue input, global::Newtonsoft.Json.Linq.JValue pattern, global::Newtonsoft.Json.Linq.JsonSelectSettings? settings)
		{
			if (input.Type != global::Newtonsoft.Json.Linq.JTokenType.String || pattern.Type != global::Newtonsoft.Json.Linq.JTokenType.String)
			{
				return false;
			}
			string obj = (string)pattern.Value;
			int num = obj.LastIndexOf('/');
			string pattern2 = obj.Substring(1, num - 1);
			string optionsText = obj.Substring(num + 1);
			global::System.TimeSpan matchTimeout = settings?.RegexMatchTimeout ?? global::System.Text.RegularExpressions.Regex.InfiniteMatchTimeout;
			return global::System.Text.RegularExpressions.Regex.IsMatch((string)input.Value, pattern2, global::Newtonsoft.Json.Utilities.MiscellaneousUtils.GetRegexOptions(optionsText), matchTimeout);
		}

		internal static bool EqualsWithStringCoercion(global::Newtonsoft.Json.Linq.JValue value, global::Newtonsoft.Json.Linq.JValue queryValue)
		{
			if (value.Equals(queryValue))
			{
				return true;
			}
			if ((value.Type == global::Newtonsoft.Json.Linq.JTokenType.Integer && queryValue.Type == global::Newtonsoft.Json.Linq.JTokenType.Float) || (value.Type == global::Newtonsoft.Json.Linq.JTokenType.Float && queryValue.Type == global::Newtonsoft.Json.Linq.JTokenType.Integer))
			{
				return global::Newtonsoft.Json.Linq.JValue.Compare(value.Type, value.Value, queryValue.Value) == 0;
			}
			if (queryValue.Type != global::Newtonsoft.Json.Linq.JTokenType.String)
			{
				return false;
			}
			string b = (string)queryValue.Value;
			string a;
			switch (value.Type)
			{
			case global::Newtonsoft.Json.Linq.JTokenType.Date:
			{
				using (global::System.IO.StringWriter stringWriter = global::Newtonsoft.Json.Utilities.StringUtils.CreateStringWriter(64))
				{
					if (value.Value is global::System.DateTimeOffset value2)
					{
						global::Newtonsoft.Json.Utilities.DateTimeUtils.WriteDateTimeOffsetString(stringWriter, value2, global::Newtonsoft.Json.DateFormatHandling.IsoDateFormat, null, global::System.Globalization.CultureInfo.InvariantCulture);
					}
					else
					{
						global::Newtonsoft.Json.Utilities.DateTimeUtils.WriteDateTimeString(stringWriter, (global::System.DateTime)value.Value, global::Newtonsoft.Json.DateFormatHandling.IsoDateFormat, null, global::System.Globalization.CultureInfo.InvariantCulture);
					}
					a = stringWriter.ToString();
				}
				break;
			}
			case global::Newtonsoft.Json.Linq.JTokenType.Bytes:
				a = global::System.Convert.ToBase64String((byte[])value.Value);
				break;
			case global::Newtonsoft.Json.Linq.JTokenType.Guid:
			case global::Newtonsoft.Json.Linq.JTokenType.TimeSpan:
				a = value.Value.ToString();
				break;
			case global::Newtonsoft.Json.Linq.JTokenType.Uri:
				a = ((global::System.Uri)value.Value).OriginalString;
				break;
			default:
				return false;
			}
			return string.Equals(a, b, global::System.StringComparison.Ordinal);
		}

		internal static bool EqualsWithStrictMatch(global::Newtonsoft.Json.Linq.JValue value, global::Newtonsoft.Json.Linq.JValue queryValue)
		{
			if ((value.Type == global::Newtonsoft.Json.Linq.JTokenType.Integer && queryValue.Type == global::Newtonsoft.Json.Linq.JTokenType.Float) || (value.Type == global::Newtonsoft.Json.Linq.JTokenType.Float && queryValue.Type == global::Newtonsoft.Json.Linq.JTokenType.Integer))
			{
				return global::Newtonsoft.Json.Linq.JValue.Compare(value.Type, value.Value, queryValue.Value) == 0;
			}
			if (value.Type != queryValue.Type)
			{
				return false;
			}
			return value.Equals(queryValue);
		}
	}
}
