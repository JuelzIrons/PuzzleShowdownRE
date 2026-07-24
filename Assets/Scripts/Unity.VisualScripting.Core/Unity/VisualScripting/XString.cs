namespace Unity.VisualScripting
{
	internal static class XString
	{
		internal static string Inject(this string format, params object[] formattingArgs)
		{
			return string.Format(format, formattingArgs);
		}

		internal static string Inject(this string format, params string[] formattingArgs)
		{
			return string.Format(format, global::System.Linq.Enumerable.ToArray(global::System.Linq.Enumerable.Select((global::System.Collections.Generic.IEnumerable<string>)formattingArgs, (global::System.Func<string, object>)((string a) => a))));
		}
	}
}
