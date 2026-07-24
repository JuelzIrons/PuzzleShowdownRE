namespace Newtonsoft.Json.Utilities
{
	internal static class DateTimeUtils
	{
		internal static readonly long InitialJavaScriptDateTicks;

		private const string IsoDateFormat = "yyyy-MM-ddTHH:mm:ss.FFFFFFFK";

		private const int DaysPer100Years = 36524;

		private const int DaysPer400Years = 146097;

		private const int DaysPer4Years = 1461;

		private const int DaysPerYear = 365;

		private const long TicksPerDay = 864000000000L;

		private static readonly int[] DaysToMonth365;

		private static readonly int[] DaysToMonth366;

		static DateTimeUtils()
		{
			InitialJavaScriptDateTicks = 621355968000000000L;
			DaysToMonth365 = new int[13]
			{
				0, 31, 59, 90, 120, 151, 181, 212, 243, 273,
				304, 334, 365
			};
			DaysToMonth366 = new int[13]
			{
				0, 31, 60, 91, 121, 152, 182, 213, 244, 274,
				305, 335, 366
			};
		}

		public static global::System.TimeSpan GetUtcOffset(this global::System.DateTime d)
		{
			return global::System.TimeZoneInfo.Local.GetUtcOffset(d);
		}

		public static global::System.Xml.XmlDateTimeSerializationMode ToSerializationMode(global::System.DateTimeKind kind)
		{
			return kind switch
			{
				global::System.DateTimeKind.Local => global::System.Xml.XmlDateTimeSerializationMode.Local, 
				global::System.DateTimeKind.Unspecified => global::System.Xml.XmlDateTimeSerializationMode.Unspecified, 
				global::System.DateTimeKind.Utc => global::System.Xml.XmlDateTimeSerializationMode.Utc, 
				_ => throw global::Newtonsoft.Json.Utilities.MiscellaneousUtils.CreateArgumentOutOfRangeException("kind", kind, "Unexpected DateTimeKind value."), 
			};
		}

		internal static global::System.DateTime EnsureDateTime(global::System.DateTime value, global::Newtonsoft.Json.DateTimeZoneHandling timeZone)
		{
			switch (timeZone)
			{
			case global::Newtonsoft.Json.DateTimeZoneHandling.Local:
				value = SwitchToLocalTime(value);
				break;
			case global::Newtonsoft.Json.DateTimeZoneHandling.Utc:
				value = SwitchToUtcTime(value);
				break;
			case global::Newtonsoft.Json.DateTimeZoneHandling.Unspecified:
				value = new global::System.DateTime(value.Ticks, global::System.DateTimeKind.Unspecified);
				break;
			default:
				throw new global::System.ArgumentException("Invalid date time handling value.");
			case global::Newtonsoft.Json.DateTimeZoneHandling.RoundtripKind:
				break;
			}
			return value;
		}

		private static global::System.DateTime SwitchToLocalTime(global::System.DateTime value)
		{
			return value.Kind switch
			{
				global::System.DateTimeKind.Unspecified => new global::System.DateTime(value.Ticks, global::System.DateTimeKind.Local), 
				global::System.DateTimeKind.Utc => value.ToLocalTime(), 
				global::System.DateTimeKind.Local => value, 
				_ => value, 
			};
		}

		private static global::System.DateTime SwitchToUtcTime(global::System.DateTime value)
		{
			return value.Kind switch
			{
				global::System.DateTimeKind.Unspecified => new global::System.DateTime(value.Ticks, global::System.DateTimeKind.Utc), 
				global::System.DateTimeKind.Utc => value, 
				global::System.DateTimeKind.Local => value.ToUniversalTime(), 
				_ => value, 
			};
		}

		private static long ToUniversalTicks(global::System.DateTime dateTime)
		{
			if (dateTime.Kind == global::System.DateTimeKind.Utc)
			{
				return dateTime.Ticks;
			}
			return ToUniversalTicks(dateTime, dateTime.GetUtcOffset());
		}

		private static long ToUniversalTicks(global::System.DateTime dateTime, global::System.TimeSpan offset)
		{
			if (dateTime.Kind == global::System.DateTimeKind.Utc || dateTime == global::System.DateTime.MaxValue || dateTime == global::System.DateTime.MinValue)
			{
				return dateTime.Ticks;
			}
			long num = dateTime.Ticks - offset.Ticks;
			if (num > 3155378975999999999L)
			{
				return 3155378975999999999L;
			}
			if (num < 0)
			{
				return 0L;
			}
			return num;
		}

		internal static long ConvertDateTimeToJavaScriptTicks(global::System.DateTime dateTime, global::System.TimeSpan offset)
		{
			return UniversalTicksToJavaScriptTicks(ToUniversalTicks(dateTime, offset));
		}

		internal static long ConvertDateTimeToJavaScriptTicks(global::System.DateTime dateTime)
		{
			return ConvertDateTimeToJavaScriptTicks(dateTime, convertToUtc: true);
		}

		internal static long ConvertDateTimeToJavaScriptTicks(global::System.DateTime dateTime, bool convertToUtc)
		{
			return UniversalTicksToJavaScriptTicks(convertToUtc ? ToUniversalTicks(dateTime) : dateTime.Ticks);
		}

		private static long UniversalTicksToJavaScriptTicks(long universalTicks)
		{
			return (universalTicks - InitialJavaScriptDateTicks) / 10000;
		}

		internal static global::System.DateTime ConvertJavaScriptTicksToDateTime(long javaScriptTicks)
		{
			return new global::System.DateTime(javaScriptTicks * 10000 + InitialJavaScriptDateTicks, global::System.DateTimeKind.Utc);
		}

		internal static bool TryParseDateTimeIso(global::Newtonsoft.Json.Utilities.StringReference text, global::Newtonsoft.Json.DateTimeZoneHandling dateTimeZoneHandling, out global::System.DateTime dt)
		{
			global::Newtonsoft.Json.Utilities.DateTimeParser dateTimeParser = default(global::Newtonsoft.Json.Utilities.DateTimeParser);
			if (!dateTimeParser.Parse(text.Chars, text.StartIndex, text.Length))
			{
				dt = default(global::System.DateTime);
				return false;
			}
			global::System.DateTime dateTime = CreateDateTime(dateTimeParser);
			switch (dateTimeParser.Zone)
			{
			case global::Newtonsoft.Json.Utilities.ParserTimeZone.Utc:
				dateTime = new global::System.DateTime(dateTime.Ticks, global::System.DateTimeKind.Utc);
				break;
			case global::Newtonsoft.Json.Utilities.ParserTimeZone.LocalWestOfUtc:
			{
				global::System.TimeSpan timeSpan2 = new global::System.TimeSpan(dateTimeParser.ZoneHour, dateTimeParser.ZoneMinute, 0);
				long num = dateTime.Ticks + timeSpan2.Ticks;
				long num4 = num;
				global::System.DateTime minValue = global::System.DateTime.MaxValue;
				if (num4 <= minValue.Ticks)
				{
					dateTime = new global::System.DateTime(num, global::System.DateTimeKind.Utc).ToLocalTime();
					break;
				}
				num += dateTime.GetUtcOffset().Ticks;
				long num5 = num;
				minValue = global::System.DateTime.MaxValue;
				if (num5 > minValue.Ticks)
				{
					minValue = global::System.DateTime.MaxValue;
					num = minValue.Ticks;
				}
				dateTime = new global::System.DateTime(num, global::System.DateTimeKind.Local);
				break;
			}
			case global::Newtonsoft.Json.Utilities.ParserTimeZone.LocalEastOfUtc:
			{
				global::System.TimeSpan timeSpan = new global::System.TimeSpan(dateTimeParser.ZoneHour, dateTimeParser.ZoneMinute, 0);
				long num = dateTime.Ticks - timeSpan.Ticks;
				long num2 = num;
				global::System.DateTime minValue = global::System.DateTime.MinValue;
				if (num2 >= minValue.Ticks)
				{
					dateTime = new global::System.DateTime(num, global::System.DateTimeKind.Utc).ToLocalTime();
					break;
				}
				num += dateTime.GetUtcOffset().Ticks;
				long num3 = num;
				minValue = global::System.DateTime.MinValue;
				if (num3 < minValue.Ticks)
				{
					minValue = global::System.DateTime.MinValue;
					num = minValue.Ticks;
				}
				dateTime = new global::System.DateTime(num, global::System.DateTimeKind.Local);
				break;
			}
			}
			dt = EnsureDateTime(dateTime, dateTimeZoneHandling);
			return true;
		}

		internal static bool TryParseDateTimeOffsetIso(global::Newtonsoft.Json.Utilities.StringReference text, out global::System.DateTimeOffset dt)
		{
			global::Newtonsoft.Json.Utilities.DateTimeParser dateTimeParser = default(global::Newtonsoft.Json.Utilities.DateTimeParser);
			if (!dateTimeParser.Parse(text.Chars, text.StartIndex, text.Length))
			{
				dt = default(global::System.DateTimeOffset);
				return false;
			}
			global::System.DateTime dateTime = CreateDateTime(dateTimeParser);
			global::System.TimeSpan offset = dateTimeParser.Zone switch
			{
				global::Newtonsoft.Json.Utilities.ParserTimeZone.Utc => new global::System.TimeSpan(0L), 
				global::Newtonsoft.Json.Utilities.ParserTimeZone.LocalWestOfUtc => new global::System.TimeSpan(-dateTimeParser.ZoneHour, -dateTimeParser.ZoneMinute, 0), 
				global::Newtonsoft.Json.Utilities.ParserTimeZone.LocalEastOfUtc => new global::System.TimeSpan(dateTimeParser.ZoneHour, dateTimeParser.ZoneMinute, 0), 
				_ => global::System.TimeZoneInfo.Local.GetUtcOffset(dateTime), 
			};
			long num = dateTime.Ticks - offset.Ticks;
			if (num < 0 || num > 3155378975999999999L)
			{
				dt = default(global::System.DateTimeOffset);
				return false;
			}
			dt = new global::System.DateTimeOffset(dateTime, offset);
			return true;
		}

		private static global::System.DateTime CreateDateTime(global::Newtonsoft.Json.Utilities.DateTimeParser dateTimeParser)
		{
			bool flag;
			if (dateTimeParser.Hour == 24)
			{
				flag = true;
				dateTimeParser.Hour = 0;
			}
			else
			{
				flag = false;
			}
			global::System.DateTime result = new global::System.DateTime(dateTimeParser.Year, dateTimeParser.Month, dateTimeParser.Day, dateTimeParser.Hour, dateTimeParser.Minute, dateTimeParser.Second).AddTicks(dateTimeParser.Fraction);
			if (flag)
			{
				result = result.AddDays(1.0);
			}
			return result;
		}

		internal static bool TryParseDateTime(global::Newtonsoft.Json.Utilities.StringReference s, global::Newtonsoft.Json.DateTimeZoneHandling dateTimeZoneHandling, string? dateFormatString, global::System.Globalization.CultureInfo culture, out global::System.DateTime dt)
		{
			if (s.Length > 0)
			{
				int startIndex = s.StartIndex;
				if (s[startIndex] == '/')
				{
					if (s.Length >= 9 && s.StartsWith("/Date(") && s.EndsWith(")/") && TryParseDateTimeMicrosoft(s, dateTimeZoneHandling, out dt))
					{
						return true;
					}
				}
				else if (s.Length >= 19 && s.Length <= 40 && char.IsDigit(s[startIndex]) && s[startIndex + 10] == 'T' && TryParseDateTimeIso(s, dateTimeZoneHandling, out dt))
				{
					return true;
				}
				if (!global::Newtonsoft.Json.Utilities.StringUtils.IsNullOrEmpty(dateFormatString) && TryParseDateTimeExact(s.ToString(), dateTimeZoneHandling, dateFormatString, culture, out dt))
				{
					return true;
				}
			}
			dt = default(global::System.DateTime);
			return false;
		}

		internal static bool TryParseDateTime(string s, global::Newtonsoft.Json.DateTimeZoneHandling dateTimeZoneHandling, string? dateFormatString, global::System.Globalization.CultureInfo culture, out global::System.DateTime dt)
		{
			if (s.Length > 0)
			{
				if (s[0] == '/')
				{
					if (s.Length >= 9 && s.StartsWith("/Date(", global::System.StringComparison.Ordinal) && s.EndsWith(")/", global::System.StringComparison.Ordinal) && TryParseDateTimeMicrosoft(new global::Newtonsoft.Json.Utilities.StringReference(s.ToCharArray(), 0, s.Length), dateTimeZoneHandling, out dt))
					{
						return true;
					}
				}
				else if (s.Length >= 19 && s.Length <= 40 && char.IsDigit(s[0]) && s[10] == 'T' && global::System.DateTime.TryParseExact(s, "yyyy-MM-ddTHH:mm:ss.FFFFFFFK", global::System.Globalization.CultureInfo.InvariantCulture, global::System.Globalization.DateTimeStyles.RoundtripKind, out dt))
				{
					dt = EnsureDateTime(dt, dateTimeZoneHandling);
					return true;
				}
				if (!global::Newtonsoft.Json.Utilities.StringUtils.IsNullOrEmpty(dateFormatString) && TryParseDateTimeExact(s, dateTimeZoneHandling, dateFormatString, culture, out dt))
				{
					return true;
				}
			}
			dt = default(global::System.DateTime);
			return false;
		}

		internal static bool TryParseDateTimeOffset(global::Newtonsoft.Json.Utilities.StringReference s, string? dateFormatString, global::System.Globalization.CultureInfo culture, out global::System.DateTimeOffset dt)
		{
			if (s.Length > 0)
			{
				int startIndex = s.StartIndex;
				if (s[startIndex] == '/')
				{
					if (s.Length >= 9 && s.StartsWith("/Date(") && s.EndsWith(")/") && TryParseDateTimeOffsetMicrosoft(s, out dt))
					{
						return true;
					}
				}
				else if (s.Length >= 19 && s.Length <= 40 && char.IsDigit(s[startIndex]) && s[startIndex + 10] == 'T' && TryParseDateTimeOffsetIso(s, out dt))
				{
					return true;
				}
				if (!global::Newtonsoft.Json.Utilities.StringUtils.IsNullOrEmpty(dateFormatString) && TryParseDateTimeOffsetExact(s.ToString(), dateFormatString, culture, out dt))
				{
					return true;
				}
			}
			dt = default(global::System.DateTimeOffset);
			return false;
		}

		internal static bool TryParseDateTimeOffset(string s, string? dateFormatString, global::System.Globalization.CultureInfo culture, out global::System.DateTimeOffset dt)
		{
			if (s.Length > 0)
			{
				if (s[0] == '/')
				{
					if (s.Length >= 9 && s.StartsWith("/Date(", global::System.StringComparison.Ordinal) && s.EndsWith(")/", global::System.StringComparison.Ordinal) && TryParseDateTimeOffsetMicrosoft(new global::Newtonsoft.Json.Utilities.StringReference(s.ToCharArray(), 0, s.Length), out dt))
					{
						return true;
					}
				}
				else if (s.Length >= 19 && s.Length <= 40 && char.IsDigit(s[0]) && s[10] == 'T' && global::System.DateTimeOffset.TryParseExact(s, "yyyy-MM-ddTHH:mm:ss.FFFFFFFK", global::System.Globalization.CultureInfo.InvariantCulture, global::System.Globalization.DateTimeStyles.RoundtripKind, out dt) && TryParseDateTimeOffsetIso(new global::Newtonsoft.Json.Utilities.StringReference(s.ToCharArray(), 0, s.Length), out dt))
				{
					return true;
				}
				if (!global::Newtonsoft.Json.Utilities.StringUtils.IsNullOrEmpty(dateFormatString) && TryParseDateTimeOffsetExact(s, dateFormatString, culture, out dt))
				{
					return true;
				}
			}
			dt = default(global::System.DateTimeOffset);
			return false;
		}

		private static bool TryParseMicrosoftDate(global::Newtonsoft.Json.Utilities.StringReference text, out long ticks, out global::System.TimeSpan offset, out global::System.DateTimeKind kind)
		{
			kind = global::System.DateTimeKind.Utc;
			int num = text.IndexOf('+', 7, text.Length - 8);
			if (num == -1)
			{
				num = text.IndexOf('-', 7, text.Length - 8);
			}
			if (num != -1)
			{
				kind = global::System.DateTimeKind.Local;
				if (!TryReadOffset(text, num + text.StartIndex, out offset))
				{
					ticks = 0L;
					return false;
				}
			}
			else
			{
				offset = global::System.TimeSpan.Zero;
				num = text.Length - 2;
			}
			return global::Newtonsoft.Json.Utilities.ConvertUtils.Int64TryParse(text.Chars, 6 + text.StartIndex, num - 6, out ticks) == global::Newtonsoft.Json.Utilities.ParseResult.Success;
		}

		private static bool TryParseDateTimeMicrosoft(global::Newtonsoft.Json.Utilities.StringReference text, global::Newtonsoft.Json.DateTimeZoneHandling dateTimeZoneHandling, out global::System.DateTime dt)
		{
			if (!TryParseMicrosoftDate(text, out var ticks, out var _, out var kind))
			{
				dt = default(global::System.DateTime);
				return false;
			}
			global::System.DateTime dateTime = ConvertJavaScriptTicksToDateTime(ticks);
			switch (kind)
			{
			case global::System.DateTimeKind.Unspecified:
				dt = global::System.DateTime.SpecifyKind(dateTime.ToLocalTime(), global::System.DateTimeKind.Unspecified);
				break;
			case global::System.DateTimeKind.Local:
				dt = dateTime.ToLocalTime();
				break;
			default:
				dt = dateTime;
				break;
			}
			dt = EnsureDateTime(dt, dateTimeZoneHandling);
			return true;
		}

		private static bool TryParseDateTimeExact(string text, global::Newtonsoft.Json.DateTimeZoneHandling dateTimeZoneHandling, string dateFormatString, global::System.Globalization.CultureInfo culture, out global::System.DateTime dt)
		{
			if (global::System.DateTime.TryParseExact(text, dateFormatString, culture, global::System.Globalization.DateTimeStyles.RoundtripKind, out var result))
			{
				result = EnsureDateTime(result, dateTimeZoneHandling);
				dt = result;
				return true;
			}
			dt = default(global::System.DateTime);
			return false;
		}

		private static bool TryParseDateTimeOffsetMicrosoft(global::Newtonsoft.Json.Utilities.StringReference text, out global::System.DateTimeOffset dt)
		{
			if (!TryParseMicrosoftDate(text, out var ticks, out var offset, out var _))
			{
				dt = default(global::System.DateTime);
				return false;
			}
			dt = new global::System.DateTimeOffset(ConvertJavaScriptTicksToDateTime(ticks).Add(offset).Ticks, offset);
			return true;
		}

		private static bool TryParseDateTimeOffsetExact(string text, string dateFormatString, global::System.Globalization.CultureInfo culture, out global::System.DateTimeOffset dt)
		{
			if (global::System.DateTimeOffset.TryParseExact(text, dateFormatString, culture, global::System.Globalization.DateTimeStyles.RoundtripKind, out var result))
			{
				dt = result;
				return true;
			}
			dt = default(global::System.DateTimeOffset);
			return false;
		}

		private static bool TryReadOffset(global::Newtonsoft.Json.Utilities.StringReference offsetText, int startIndex, out global::System.TimeSpan offset)
		{
			bool flag = offsetText[startIndex] == '-';
			if (global::Newtonsoft.Json.Utilities.ConvertUtils.Int32TryParse(offsetText.Chars, startIndex + 1, 2, out var value) != global::Newtonsoft.Json.Utilities.ParseResult.Success)
			{
				offset = default(global::System.TimeSpan);
				return false;
			}
			int value2 = 0;
			if (offsetText.Length - startIndex > 5 && global::Newtonsoft.Json.Utilities.ConvertUtils.Int32TryParse(offsetText.Chars, startIndex + 3, 2, out value2) != global::Newtonsoft.Json.Utilities.ParseResult.Success)
			{
				offset = default(global::System.TimeSpan);
				return false;
			}
			offset = global::System.TimeSpan.FromHours(value) + global::System.TimeSpan.FromMinutes(value2);
			if (flag)
			{
				offset = offset.Negate();
			}
			return true;
		}

		internal static void WriteDateTimeString(global::System.IO.TextWriter writer, global::System.DateTime value, global::Newtonsoft.Json.DateFormatHandling format, string? formatString, global::System.Globalization.CultureInfo culture)
		{
			if (global::Newtonsoft.Json.Utilities.StringUtils.IsNullOrEmpty(formatString))
			{
				char[] array = new char[64];
				int count = WriteDateTimeString(array, 0, value, null, value.Kind, format);
				writer.Write(array, 0, count);
			}
			else
			{
				writer.Write(value.ToString(formatString, culture));
			}
		}

		internal static int WriteDateTimeString(char[] chars, int start, global::System.DateTime value, global::System.TimeSpan? offset, global::System.DateTimeKind kind, global::Newtonsoft.Json.DateFormatHandling format)
		{
			int num = start;
			if (format == global::Newtonsoft.Json.DateFormatHandling.MicrosoftDateFormat)
			{
				global::System.TimeSpan offset2 = offset ?? value.GetUtcOffset();
				long num2 = ConvertDateTimeToJavaScriptTicks(value, offset2);
				"\\/Date(".CopyTo(0, chars, num, 7);
				num += 7;
				string text = num2.ToString(global::System.Globalization.CultureInfo.InvariantCulture);
				text.CopyTo(0, chars, num, text.Length);
				num += text.Length;
				switch (kind)
				{
				case global::System.DateTimeKind.Unspecified:
					if (value != global::System.DateTime.MaxValue && value != global::System.DateTime.MinValue)
					{
						num = WriteDateTimeOffset(chars, num, offset2, format);
					}
					break;
				case global::System.DateTimeKind.Local:
					num = WriteDateTimeOffset(chars, num, offset2, format);
					break;
				}
				")\\/".CopyTo(0, chars, num, 3);
				num += 3;
			}
			else
			{
				num = WriteDefaultIsoDate(chars, num, value);
				switch (kind)
				{
				case global::System.DateTimeKind.Local:
					num = WriteDateTimeOffset(chars, num, offset ?? value.GetUtcOffset(), format);
					break;
				case global::System.DateTimeKind.Utc:
					chars[num++] = 'Z';
					break;
				}
			}
			return num;
		}

		internal static int WriteDefaultIsoDate(char[] chars, int start, global::System.DateTime dt)
		{
			int num = 19;
			GetDateValues(dt, out var year, out var month, out var day);
			CopyIntToCharArray(chars, start, year, 4);
			chars[start + 4] = '-';
			CopyIntToCharArray(chars, start + 5, month, 2);
			chars[start + 7] = '-';
			CopyIntToCharArray(chars, start + 8, day, 2);
			chars[start + 10] = 'T';
			CopyIntToCharArray(chars, start + 11, dt.Hour, 2);
			chars[start + 13] = ':';
			CopyIntToCharArray(chars, start + 14, dt.Minute, 2);
			chars[start + 16] = ':';
			CopyIntToCharArray(chars, start + 17, dt.Second, 2);
			int num2 = (int)(dt.Ticks % 10000000);
			if (num2 != 0)
			{
				int num3 = 7;
				while (num2 % 10 == 0)
				{
					num3--;
					num2 /= 10;
				}
				chars[start + 19] = '.';
				CopyIntToCharArray(chars, start + 20, num2, num3);
				num += num3 + 1;
			}
			return start + num;
		}

		private static void CopyIntToCharArray(char[] chars, int start, int value, int digits)
		{
			while (digits-- != 0)
			{
				chars[start + digits] = (char)(value % 10 + 48);
				value /= 10;
			}
		}

		internal static int WriteDateTimeOffset(char[] chars, int start, global::System.TimeSpan offset, global::Newtonsoft.Json.DateFormatHandling format)
		{
			chars[start++] = ((offset.Ticks >= 0) ? '+' : '-');
			int value = global::System.Math.Abs(offset.Hours);
			CopyIntToCharArray(chars, start, value, 2);
			start += 2;
			if (format == global::Newtonsoft.Json.DateFormatHandling.IsoDateFormat)
			{
				chars[start++] = ':';
			}
			int value2 = global::System.Math.Abs(offset.Minutes);
			CopyIntToCharArray(chars, start, value2, 2);
			start += 2;
			return start;
		}

		internal static void WriteDateTimeOffsetString(global::System.IO.TextWriter writer, global::System.DateTimeOffset value, global::Newtonsoft.Json.DateFormatHandling format, string? formatString, global::System.Globalization.CultureInfo culture)
		{
			if (global::Newtonsoft.Json.Utilities.StringUtils.IsNullOrEmpty(formatString))
			{
				char[] array = new char[64];
				int count = WriteDateTimeString(array, 0, (format == global::Newtonsoft.Json.DateFormatHandling.IsoDateFormat) ? value.DateTime : value.UtcDateTime, value.Offset, global::System.DateTimeKind.Local, format);
				writer.Write(array, 0, count);
			}
			else
			{
				writer.Write(value.ToString(formatString, culture));
			}
		}

		private static void GetDateValues(global::System.DateTime td, out int year, out int month, out int day)
		{
			int num = (int)(td.Ticks / 864000000000L);
			int num2 = num / 146097;
			num -= num2 * 146097;
			int num3 = num / 36524;
			if (num3 == 4)
			{
				num3 = 3;
			}
			num -= num3 * 36524;
			int num4 = num / 1461;
			num -= num4 * 1461;
			int num5 = num / 365;
			if (num5 == 4)
			{
				num5 = 3;
			}
			year = num2 * 400 + num3 * 100 + num4 * 4 + num5 + 1;
			num -= num5 * 365;
			int[] array = ((num5 == 3 && (num4 != 24 || num3 == 3)) ? DaysToMonth366 : DaysToMonth365);
			int i;
			for (i = num >> 6; num >= array[i]; i++)
			{
			}
			month = i;
			day = num - array[i - 1] + 1;
		}
	}
}
