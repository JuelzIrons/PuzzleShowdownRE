namespace Newtonsoft.Json.Serialization
{
	public class KebabCaseNamingStrategy : global::Newtonsoft.Json.Serialization.NamingStrategy
	{
		public KebabCaseNamingStrategy(bool processDictionaryKeys, bool overrideSpecifiedNames)
		{
			base.ProcessDictionaryKeys = processDictionaryKeys;
			base.OverrideSpecifiedNames = overrideSpecifiedNames;
		}

		public KebabCaseNamingStrategy(bool processDictionaryKeys, bool overrideSpecifiedNames, bool processExtensionDataNames)
			: this(processDictionaryKeys, overrideSpecifiedNames)
		{
			base.ProcessExtensionDataNames = processExtensionDataNames;
		}

		public KebabCaseNamingStrategy()
		{
		}

		protected override string ResolvePropertyName(string name)
		{
			return global::Newtonsoft.Json.Utilities.StringUtils.ToKebabCase(name);
		}
	}
}
