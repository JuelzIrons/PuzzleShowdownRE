namespace Newtonsoft.Json.Linq.JsonPath
{
	internal class ScanMultipleFilter : global::Newtonsoft.Json.Linq.JsonPath.PathFilter
	{
		private global::System.Collections.Generic.List<string> _names;

		public ScanMultipleFilter(global::System.Collections.Generic.List<string> names)
		{
			_names = names;
		}

		public override global::System.Collections.Generic.IEnumerable<global::Newtonsoft.Json.Linq.JToken> ExecuteFilter(global::Newtonsoft.Json.Linq.JToken root, global::System.Collections.Generic.IEnumerable<global::Newtonsoft.Json.Linq.JToken> current, global::Newtonsoft.Json.Linq.JsonSelectSettings? settings)
		{
			foreach (global::Newtonsoft.Json.Linq.JToken c in current)
			{
				global::Newtonsoft.Json.Linq.JToken value = c;
				while (true)
				{
					global::Newtonsoft.Json.Linq.JContainer container = value as global::Newtonsoft.Json.Linq.JContainer;
					value = global::Newtonsoft.Json.Linq.JsonPath.PathFilter.GetNextScanValue(c, container, value);
					if (value == null)
					{
						break;
					}
					if (!(value is global::Newtonsoft.Json.Linq.JProperty property))
					{
						continue;
					}
					foreach (string name in _names)
					{
						if (property.Name == name)
						{
							yield return property.Value;
						}
					}
				}
			}
		}
	}
}
