namespace Newtonsoft.Json
{
	internal struct JsonPosition
	{
		private static readonly char[] SpecialCharacters = new char[18]
		{
			'.', ' ', '\'', '/', '"', '[', ']', '(', ')', '\t',
			'\n', '\r', '\f', '\b', '\\', '\u0085', '\u2028', '\u2029'
		};

		internal global::Newtonsoft.Json.JsonContainerType Type;

		internal int Position;

		internal string? PropertyName;

		internal bool HasIndex;

		public JsonPosition(global::Newtonsoft.Json.JsonContainerType type)
		{
			Type = type;
			HasIndex = TypeHasIndex(type);
			Position = -1;
			PropertyName = null;
		}

		internal int CalculateLength()
		{
			switch (Type)
			{
			case global::Newtonsoft.Json.JsonContainerType.Object:
				return PropertyName.Length + 5;
			case global::Newtonsoft.Json.JsonContainerType.Array:
			case global::Newtonsoft.Json.JsonContainerType.Constructor:
				return global::Newtonsoft.Json.Utilities.MathUtils.IntLength((ulong)Position) + 2;
			default:
				throw new global::System.ArgumentOutOfRangeException("Type");
			}
		}

		internal void WriteTo(global::System.Text.StringBuilder sb, ref global::System.IO.StringWriter? writer, ref char[]? buffer)
		{
			switch (Type)
			{
			case global::Newtonsoft.Json.JsonContainerType.Object:
			{
				string propertyName = PropertyName;
				if (propertyName.IndexOfAny(SpecialCharacters) != -1)
				{
					sb.Append("['");
					if (writer == null)
					{
						writer = new global::System.IO.StringWriter(sb);
					}
					global::Newtonsoft.Json.Utilities.JavaScriptUtils.WriteEscapedJavaScriptString(writer, propertyName, '\'', appendDelimiters: false, global::Newtonsoft.Json.Utilities.JavaScriptUtils.SingleQuoteCharEscapeFlags, global::Newtonsoft.Json.StringEscapeHandling.Default, null, ref buffer);
					sb.Append("']");
				}
				else
				{
					if (sb.Length > 0)
					{
						sb.Append('.');
					}
					sb.Append(propertyName);
				}
				break;
			}
			case global::Newtonsoft.Json.JsonContainerType.Array:
			case global::Newtonsoft.Json.JsonContainerType.Constructor:
				sb.Append('[');
				sb.Append(Position);
				sb.Append(']');
				break;
			}
		}

		internal static bool TypeHasIndex(global::Newtonsoft.Json.JsonContainerType type)
		{
			if (type != global::Newtonsoft.Json.JsonContainerType.Array)
			{
				return type == global::Newtonsoft.Json.JsonContainerType.Constructor;
			}
			return true;
		}

		internal static string BuildPath(global::System.Collections.Generic.List<global::Newtonsoft.Json.JsonPosition> positions, global::Newtonsoft.Json.JsonPosition? currentPosition)
		{
			int num = 0;
			if (positions != null)
			{
				for (int i = 0; i < positions.Count; i++)
				{
					num += positions[i].CalculateLength();
				}
			}
			if (currentPosition.HasValue)
			{
				num += currentPosition.GetValueOrDefault().CalculateLength();
			}
			global::System.Text.StringBuilder stringBuilder = new global::System.Text.StringBuilder(num);
			global::System.IO.StringWriter writer = null;
			char[] buffer = null;
			if (positions != null)
			{
				foreach (global::Newtonsoft.Json.JsonPosition position in positions)
				{
					position.WriteTo(stringBuilder, ref writer, ref buffer);
				}
			}
			currentPosition?.WriteTo(stringBuilder, ref writer, ref buffer);
			return stringBuilder.ToString();
		}

		internal static string FormatMessage(global::Newtonsoft.Json.IJsonLineInfo? lineInfo, string path, string message)
		{
			if (!message.EndsWith(global::System.Environment.NewLine, global::System.StringComparison.Ordinal))
			{
				message = message.Trim();
				if (!global::Newtonsoft.Json.Utilities.StringUtils.EndsWith(message, '.'))
				{
					message += ".";
				}
				message += " ";
			}
			message += global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Path '{0}'", global::System.Globalization.CultureInfo.InvariantCulture, path);
			if (lineInfo != null && lineInfo.HasLineInfo())
			{
				message += global::Newtonsoft.Json.Utilities.StringUtils.FormatWith(", line {0}, position {1}", global::System.Globalization.CultureInfo.InvariantCulture, lineInfo.LineNumber, lineInfo.LinePosition);
			}
			message += ".";
			return message;
		}
	}
}
