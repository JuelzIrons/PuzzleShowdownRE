namespace Newtonsoft.Json.Linq.JsonPath
{
	internal class FieldMultipleFilter : global::Newtonsoft.Json.Linq.JsonPath.PathFilter
	{
		internal global::System.Collections.Generic.List<string> Names;

		public FieldMultipleFilter(global::System.Collections.Generic.List<string> names)
		{
			Names = names;
		}

		public override global::System.Collections.Generic.IEnumerable<global::Newtonsoft.Json.Linq.JToken> ExecuteFilter(global::Newtonsoft.Json.Linq.JToken root, global::System.Collections.Generic.IEnumerable<global::Newtonsoft.Json.Linq.JToken> current, global::Newtonsoft.Json.Linq.JsonSelectSettings? settings)
		{
			foreach (global::Newtonsoft.Json.Linq.JToken item in current)
			{
				if (item is global::Newtonsoft.Json.Linq.JObject o)
				{
					foreach (string name in Names)
					{
						global::Newtonsoft.Json.Linq.JToken jToken = o[name];
						if (jToken != null)
						{
							yield return jToken;
						}
						if (settings?.ErrorWhenNoMatch ?? false)
						{
							throw new global::Newtonsoft.Json.JsonException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Property '{0}' does not exist on JObject.", global::System.Globalization.CultureInfo.InvariantCulture, name));
						}
					}
				}
				else if (settings?.ErrorWhenNoMatch ?? false)
				{
					throw new global::Newtonsoft.Json.JsonException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Properties {0} not valid on {1}.", global::System.Globalization.CultureInfo.InvariantCulture, string.Join(", ", global::System.Linq.Enumerable.Select(Names, (string n) => "'" + n + "'")), item.GetType().Name));
				}
			}
		}
	}
}
