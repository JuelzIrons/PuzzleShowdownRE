namespace Newtonsoft.Json.Serialization
{
	public class CamelCaseNamingStrategy : global::Newtonsoft.Json.Serialization.NamingStrategy
	{
		public CamelCaseNamingStrategy(bool processDictionaryKeys, bool overrideSpecifiedNames)
		{
			base.ProcessDictionaryKeys = processDictionaryKeys;
			base.OverrideSpecifiedNames = overrideSpecifiedNames;
		}

		public CamelCaseNamingStrategy(bool processDictionaryKeys, bool overrideSpecifiedNames, bool processExtensionDataNames)
			: this(processDictionaryKeys, overrideSpecifiedNames)
		{
			base.ProcessExtensionDataNames = processExtensionDataNames;
		}

		public CamelCaseNamingStrategy()
		{
		}

		protected override string ResolvePropertyName(string name)
		{
			return global::Newtonsoft.Json.Utilities.StringUtils.ToCamelCase(name);
		}
	}
}
