namespace Newtonsoft.Json.Serialization
{
	public class SnakeCaseNamingStrategy : global::Newtonsoft.Json.Serialization.NamingStrategy
	{
		public SnakeCaseNamingStrategy(bool processDictionaryKeys, bool overrideSpecifiedNames)
		{
			base.ProcessDictionaryKeys = processDictionaryKeys;
			base.OverrideSpecifiedNames = overrideSpecifiedNames;
		}

		public SnakeCaseNamingStrategy(bool processDictionaryKeys, bool overrideSpecifiedNames, bool processExtensionDataNames)
			: this(processDictionaryKeys, overrideSpecifiedNames)
		{
			base.ProcessExtensionDataNames = processExtensionDataNames;
		}

		public SnakeCaseNamingStrategy()
		{
		}

		protected override string ResolvePropertyName(string name)
		{
			return global::Newtonsoft.Json.Utilities.StringUtils.ToSnakeCase(name);
		}
	}
}
