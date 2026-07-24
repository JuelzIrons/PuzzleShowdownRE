namespace UnityEngine.Rendering
{
	public static class DocumentationUtils
	{
		public static string GetHelpURL<TEnum>(TEnum mask = default(TEnum)) where TEnum : struct, global::System.IConvertible
		{
			global::UnityEngine.HelpURLAttribute helpURLAttribute = (global::UnityEngine.HelpURLAttribute)global::System.Linq.Enumerable.FirstOrDefault(mask.GetType().GetCustomAttributes(typeof(global::UnityEngine.HelpURLAttribute), inherit: false));
			if (helpURLAttribute != null)
			{
				return $"{helpURLAttribute.URL}#{mask}";
			}
			return string.Empty;
		}

		public static bool TryGetHelpURL(global::System.Type type, out string url)
		{
			global::UnityEngine.HelpURLAttribute customAttribute = global::System.Reflection.CustomAttributeExtensions.GetCustomAttribute<global::UnityEngine.HelpURLAttribute>(type, inherit: false);
			url = customAttribute?.URL;
			return customAttribute != null;
		}
	}
}
