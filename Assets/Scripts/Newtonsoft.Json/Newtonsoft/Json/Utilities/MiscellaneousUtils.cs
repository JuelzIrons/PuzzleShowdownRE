namespace Newtonsoft.Json.Utilities
{
	internal static class MiscellaneousUtils
	{
		[global::System.Diagnostics.Conditional("DEBUG")]
		public static void Assert([global::System.Diagnostics.CodeAnalysis.DoesNotReturnIf(false)] bool condition, string? message = null)
		{
		}

		public static bool ValueEquals(object? objA, object? objB)
		{
			if (objA == objB)
			{
				return true;
			}
			if (objA == null || objB == null)
			{
				return false;
			}
			if (objA.GetType() != objB.GetType())
			{
				if (global::Newtonsoft.Json.Utilities.ConvertUtils.IsInteger(objA) && global::Newtonsoft.Json.Utilities.ConvertUtils.IsInteger(objB))
				{
					return global::System.Convert.ToDecimal(objA, global::System.Globalization.CultureInfo.CurrentCulture).Equals(global::System.Convert.ToDecimal(objB, global::System.Globalization.CultureInfo.CurrentCulture));
				}
				if ((objA is double || objA is float || objA is decimal) && (objB is double || objB is float || objB is decimal))
				{
					return global::Newtonsoft.Json.Utilities.MathUtils.ApproxEquals(global::System.Convert.ToDouble(objA, global::System.Globalization.CultureInfo.CurrentCulture), global::System.Convert.ToDouble(objB, global::System.Globalization.CultureInfo.CurrentCulture));
				}
				return false;
			}
			return objA.Equals(objB);
		}

		public static global::System.ArgumentOutOfRangeException CreateArgumentOutOfRangeException(string paramName, object actualValue, string message)
		{
			string message2 = message + global::System.Environment.NewLine + "Actual value was {0}.".FormatWith(global::System.Globalization.CultureInfo.InvariantCulture, actualValue);
			return new global::System.ArgumentOutOfRangeException(paramName, message2);
		}

		public static string ToString(object? value)
		{
			if (value == null)
			{
				return "{null}";
			}
			if (!(value is string text))
			{
				return value.ToString();
			}
			return "\"" + text + "\"";
		}

		public static int ByteArrayCompare(byte[] a1, byte[] a2)
		{
			int num = a1.Length.CompareTo(a2.Length);
			if (num != 0)
			{
				return num;
			}
			for (int i = 0; i < a1.Length; i++)
			{
				int num2 = a1[i].CompareTo(a2[i]);
				if (num2 != 0)
				{
					return num2;
				}
			}
			return 0;
		}

		public static string? GetPrefix(string qualifiedName)
		{
			GetQualifiedNameParts(qualifiedName, out string prefix, out string _);
			return prefix;
		}

		public static string GetLocalName(string qualifiedName)
		{
			GetQualifiedNameParts(qualifiedName, out string _, out string localName);
			return localName;
		}

		public static void GetQualifiedNameParts(string qualifiedName, out string? prefix, out string localName)
		{
			int num = global::Newtonsoft.Json.Utilities.StringUtils.IndexOf(qualifiedName, ':');
			if (num == -1 || num == 0 || qualifiedName.Length - 1 == num)
			{
				prefix = null;
				localName = qualifiedName;
			}
			else
			{
				prefix = qualifiedName.Substring(0, num);
				localName = qualifiedName.Substring(num + 1);
			}
		}

		internal static global::System.Text.RegularExpressions.RegexOptions GetRegexOptions(string optionsText)
		{
			global::System.Text.RegularExpressions.RegexOptions regexOptions = global::System.Text.RegularExpressions.RegexOptions.None;
			for (int i = 0; i < optionsText.Length; i++)
			{
				switch (optionsText[i])
				{
				case 'i':
					regexOptions |= global::System.Text.RegularExpressions.RegexOptions.IgnoreCase;
					break;
				case 'm':
					regexOptions |= global::System.Text.RegularExpressions.RegexOptions.Multiline;
					break;
				case 's':
					regexOptions |= global::System.Text.RegularExpressions.RegexOptions.Singleline;
					break;
				case 'x':
					regexOptions |= global::System.Text.RegularExpressions.RegexOptions.ExplicitCapture;
					break;
				}
			}
			return regexOptions;
		}
	}
}
