namespace Newtonsoft.Json.Linq.JsonPath
{
	internal class ArrayIndexFilter : global::Newtonsoft.Json.Linq.JsonPath.PathFilter
	{
		public int? Index { get; set; }

		public override global::System.Collections.Generic.IEnumerable<global::Newtonsoft.Json.Linq.JToken> ExecuteFilter(global::Newtonsoft.Json.Linq.JToken root, global::System.Collections.Generic.IEnumerable<global::Newtonsoft.Json.Linq.JToken> current, global::Newtonsoft.Json.Linq.JsonSelectSettings? settings)
		{
			foreach (global::Newtonsoft.Json.Linq.JToken item in current)
			{
				if (Index.HasValue)
				{
					global::Newtonsoft.Json.Linq.JToken tokenIndex = global::Newtonsoft.Json.Linq.JsonPath.PathFilter.GetTokenIndex(item, settings, Index.GetValueOrDefault());
					if (tokenIndex != null)
					{
						yield return tokenIndex;
					}
				}
				else if (item is global::Newtonsoft.Json.Linq.JArray || item is global::Newtonsoft.Json.Linq.JConstructor)
				{
					foreach (global::Newtonsoft.Json.Linq.JToken item2 in (global::System.Collections.Generic.IEnumerable<global::Newtonsoft.Json.Linq.JToken>)item)
					{
						yield return item2;
					}
				}
				else if (settings?.ErrorWhenNoMatch ?? false)
				{
					throw new global::Newtonsoft.Json.JsonException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Index * not valid on {0}.", global::System.Globalization.CultureInfo.InvariantCulture, item.GetType().Name));
				}
			}
		}
	}
}
