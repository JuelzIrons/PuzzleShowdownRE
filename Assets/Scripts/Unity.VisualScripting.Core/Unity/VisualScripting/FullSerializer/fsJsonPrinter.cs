namespace Unity.VisualScripting.FullSerializer
{
	public static class fsJsonPrinter
	{
		private static void InsertSpacing(global::System.IO.TextWriter stream, int count)
		{
			for (int i = 0; i < count; i++)
			{
				stream.Write("    ");
			}
		}

		private static string EscapeString(string str)
		{
			bool flag = false;
			foreach (char c in str)
			{
				int num = global::System.Convert.ToInt32(c);
				if (num < 0 || num > 127)
				{
					flag = true;
					break;
				}
				switch (c)
				{
				case '\0':
				case '\a':
				case '\b':
				case '\t':
				case '\n':
				case '\f':
				case '\r':
				case '"':
				case '\\':
					flag = true;
					break;
				}
				if (flag)
				{
					break;
				}
			}
			if (!flag)
			{
				return str;
			}
			global::System.Text.StringBuilder stringBuilder = new global::System.Text.StringBuilder();
			foreach (char c2 in str)
			{
				int num2 = global::System.Convert.ToInt32(c2);
				if (num2 < 0 || num2 > 127)
				{
					stringBuilder.Append($"\\u{num2:x4} ".Trim());
					continue;
				}
				switch (c2)
				{
				case '"':
					stringBuilder.Append("\\\"");
					break;
				case '\\':
					stringBuilder.Append("\\\\");
					break;
				case '\a':
					stringBuilder.Append("\\a");
					break;
				case '\b':
					stringBuilder.Append("\\b");
					break;
				case '\f':
					stringBuilder.Append("\\f");
					break;
				case '\n':
					stringBuilder.Append("\\n");
					break;
				case '\r':
					stringBuilder.Append("\\r");
					break;
				case '\t':
					stringBuilder.Append("\\t");
					break;
				case '\0':
					stringBuilder.Append("\\0");
					break;
				default:
					stringBuilder.Append(c2);
					break;
				}
			}
			return stringBuilder.ToString();
		}

		private static void BuildCompressedString(global::Unity.VisualScripting.FullSerializer.fsData data, global::System.IO.TextWriter stream)
		{
			switch (data.Type)
			{
			case global::Unity.VisualScripting.FullSerializer.fsDataType.Null:
				stream.Write("null");
				break;
			case global::Unity.VisualScripting.FullSerializer.fsDataType.Boolean:
				if (data.AsBool)
				{
					stream.Write("true");
				}
				else
				{
					stream.Write("false");
				}
				break;
			case global::Unity.VisualScripting.FullSerializer.fsDataType.Double:
				stream.Write(ConvertDoubleToString(data.AsDouble));
				break;
			case global::Unity.VisualScripting.FullSerializer.fsDataType.Int64:
				stream.Write(data.AsInt64);
				break;
			case global::Unity.VisualScripting.FullSerializer.fsDataType.String:
				stream.Write('"');
				stream.Write(EscapeString(data.AsString));
				stream.Write('"');
				break;
			case global::Unity.VisualScripting.FullSerializer.fsDataType.Object:
			{
				stream.Write('{');
				bool flag2 = false;
				foreach (global::System.Collections.Generic.KeyValuePair<string, global::Unity.VisualScripting.FullSerializer.fsData> item in data.AsDictionary)
				{
					if (flag2)
					{
						stream.Write(',');
					}
					flag2 = true;
					stream.Write('"');
					stream.Write(item.Key);
					stream.Write('"');
					stream.Write(":");
					BuildCompressedString(item.Value, stream);
				}
				stream.Write('}');
				break;
			}
			case global::Unity.VisualScripting.FullSerializer.fsDataType.Array:
			{
				stream.Write('[');
				bool flag = false;
				foreach (global::Unity.VisualScripting.FullSerializer.fsData @as in data.AsList)
				{
					if (flag)
					{
						stream.Write(',');
					}
					flag = true;
					BuildCompressedString(@as, stream);
				}
				stream.Write(']');
				break;
			}
			}
		}

		private static void BuildPrettyString(global::Unity.VisualScripting.FullSerializer.fsData data, global::System.IO.TextWriter stream, int depth)
		{
			switch (data.Type)
			{
			case global::Unity.VisualScripting.FullSerializer.fsDataType.Null:
				stream.Write("null");
				break;
			case global::Unity.VisualScripting.FullSerializer.fsDataType.Boolean:
				if (data.AsBool)
				{
					stream.Write("true");
				}
				else
				{
					stream.Write("false");
				}
				break;
			case global::Unity.VisualScripting.FullSerializer.fsDataType.Double:
				stream.Write(ConvertDoubleToString(data.AsDouble));
				break;
			case global::Unity.VisualScripting.FullSerializer.fsDataType.Int64:
				stream.Write(data.AsInt64);
				break;
			case global::Unity.VisualScripting.FullSerializer.fsDataType.String:
				stream.Write('"');
				stream.Write(EscapeString(data.AsString));
				stream.Write('"');
				break;
			case global::Unity.VisualScripting.FullSerializer.fsDataType.Object:
			{
				stream.Write('{');
				stream.WriteLine();
				bool flag2 = false;
				foreach (global::System.Collections.Generic.KeyValuePair<string, global::Unity.VisualScripting.FullSerializer.fsData> item in data.AsDictionary)
				{
					if (flag2)
					{
						stream.Write(',');
						stream.WriteLine();
					}
					flag2 = true;
					InsertSpacing(stream, depth + 1);
					stream.Write('"');
					stream.Write(item.Key);
					stream.Write('"');
					stream.Write(": ");
					BuildPrettyString(item.Value, stream, depth + 1);
				}
				stream.WriteLine();
				InsertSpacing(stream, depth);
				stream.Write('}');
				break;
			}
			case global::Unity.VisualScripting.FullSerializer.fsDataType.Array:
			{
				if (data.AsList.Count == 0)
				{
					stream.Write("[]");
					break;
				}
				bool flag = false;
				stream.Write('[');
				stream.WriteLine();
				foreach (global::Unity.VisualScripting.FullSerializer.fsData @as in data.AsList)
				{
					if (flag)
					{
						stream.Write(',');
						stream.WriteLine();
					}
					flag = true;
					InsertSpacing(stream, depth + 1);
					BuildPrettyString(@as, stream, depth + 1);
				}
				stream.WriteLine();
				InsertSpacing(stream, depth);
				stream.Write(']');
				break;
			}
			}
		}

		public static void PrettyJson(global::Unity.VisualScripting.FullSerializer.fsData data, global::System.IO.TextWriter outputStream)
		{
			BuildPrettyString(data, outputStream, 0);
		}

		public static string PrettyJson(global::Unity.VisualScripting.FullSerializer.fsData data)
		{
			global::System.Text.StringBuilder stringBuilder = new global::System.Text.StringBuilder();
			using global::System.IO.StringWriter stream = new global::System.IO.StringWriter(stringBuilder);
			BuildPrettyString(data, stream, 0);
			return stringBuilder.ToString();
		}

		public static void CompressedJson(global::Unity.VisualScripting.FullSerializer.fsData data, global::System.IO.StreamWriter outputStream)
		{
			BuildCompressedString(data, outputStream);
		}

		public static string CompressedJson(global::Unity.VisualScripting.FullSerializer.fsData data)
		{
			global::System.Text.StringBuilder stringBuilder = new global::System.Text.StringBuilder();
			using global::System.IO.StringWriter stream = new global::System.IO.StringWriter(stringBuilder);
			BuildCompressedString(data, stream);
			return stringBuilder.ToString();
		}

		private static string ConvertDoubleToString(double d)
		{
			if (double.IsInfinity(d) || double.IsNaN(d))
			{
				return d.ToString(global::System.Globalization.CultureInfo.InvariantCulture);
			}
			string text = d.ToString(global::System.Globalization.CultureInfo.InvariantCulture);
			if (!text.Contains(".") && !text.Contains("e") && !text.Contains("E"))
			{
				text += ".0";
			}
			return text;
		}
	}
}
