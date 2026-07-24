namespace Newtonsoft.Json.Utilities
{
	internal static class ValidationUtils
	{
		public static void ArgumentNotNull([global::System.Diagnostics.CodeAnalysis.NotNull] object? value, string parameterName)
		{
			if (value == null)
			{
				throw new global::System.ArgumentNullException(parameterName);
			}
		}
	}
}
