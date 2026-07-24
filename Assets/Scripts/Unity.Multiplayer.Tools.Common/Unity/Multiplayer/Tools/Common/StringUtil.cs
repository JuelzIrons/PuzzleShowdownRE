namespace Unity.Multiplayer.Tools.Common
{
	internal static class StringUtil
	{
		internal static string AddSpacesToCamelCase(string s)
		{
			return string.Concat(global::System.Linq.Enumerable.Select(s, (char x) => (!char.IsUpper(x)) ? x.ToString() : (" " + x))).TrimStart(' ');
		}

		internal static string RemoveSpaces(string s)
		{
			return s.Replace(" ", "");
		}
	}
}
