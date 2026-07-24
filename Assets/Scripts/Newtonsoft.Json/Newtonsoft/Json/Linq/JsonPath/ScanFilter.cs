namespace Newtonsoft.Json.Linq.JsonPath
{
	internal class ScanFilter : global::Newtonsoft.Json.Linq.JsonPath.PathFilter
	{
		internal string? Name;

		public ScanFilter(string? name)
		{
			Name = name;
		}

		public override global::System.Collections.Generic.IEnumerable<global::Newtonsoft.Json.Linq.JToken> ExecuteFilter(global::Newtonsoft.Json.Linq.JToken root, global::System.Collections.Generic.IEnumerable<global::Newtonsoft.Json.Linq.JToken> current, global::Newtonsoft.Json.Linq.JsonSelectSettings? settings)
		{
			foreach (global::Newtonsoft.Json.Linq.JToken c in current)
			{
				if (Name == null)
				{
					yield return c;
				}
				global::Newtonsoft.Json.Linq.JToken value = c;
				while (true)
				{
					global::Newtonsoft.Json.Linq.JContainer container = value as global::Newtonsoft.Json.Linq.JContainer;
					value = global::Newtonsoft.Json.Linq.JsonPath.PathFilter.GetNextScanValue(c, container, value);
					if (value == null)
					{
						break;
					}
					if (value is global::Newtonsoft.Json.Linq.JProperty jProperty)
					{
						if (jProperty.Name == Name)
						{
							yield return jProperty.Value;
						}
					}
					else if (Name == null)
					{
						yield return value;
					}
				}
			}
		}
	}
}
