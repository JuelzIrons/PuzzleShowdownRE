namespace Newtonsoft.Json.Linq.JsonPath
{
	internal class FieldFilter : global::Newtonsoft.Json.Linq.JsonPath.PathFilter
	{
		internal string? Name;

		public FieldFilter(string? name)
		{
			Name = name;
		}

		public override global::System.Collections.Generic.IEnumerable<global::Newtonsoft.Json.Linq.JToken> ExecuteFilter(global::Newtonsoft.Json.Linq.JToken root, global::System.Collections.Generic.IEnumerable<global::Newtonsoft.Json.Linq.JToken> current, global::Newtonsoft.Json.Linq.JsonSelectSettings? settings)
		{
			foreach (global::Newtonsoft.Json.Linq.JToken item in current)
			{
				if (item is global::Newtonsoft.Json.Linq.JObject jObject)
				{
					if (Name != null)
					{
						global::Newtonsoft.Json.Linq.JToken jToken = jObject[Name];
						if (jToken != null)
						{
							yield return jToken;
						}
						else if (settings?.ErrorWhenNoMatch ?? false)
						{
							throw new global::Newtonsoft.Json.JsonException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Property '{0}' does not exist on JObject.", global::System.Globalization.CultureInfo.InvariantCulture, Name));
						}
						continue;
					}
					foreach (global::System.Collections.Generic.KeyValuePair<string, global::Newtonsoft.Json.Linq.JToken> item2 in jObject)
					{
						yield return item2.Value;
					}
				}
				else if (settings?.ErrorWhenNoMatch ?? false)
				{
					throw new global::Newtonsoft.Json.JsonException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Property '{0}' not valid on {1}.", global::System.Globalization.CultureInfo.InvariantCulture, Name ?? "*", item.GetType().Name));
				}
			}
		}
	}
}
