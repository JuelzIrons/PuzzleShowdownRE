namespace Newtonsoft.Json.Linq.JsonPath
{
	internal class QueryScanFilter : global::Newtonsoft.Json.Linq.JsonPath.PathFilter
	{
		internal global::Newtonsoft.Json.Linq.JsonPath.QueryExpression Expression;

		public QueryScanFilter(global::Newtonsoft.Json.Linq.JsonPath.QueryExpression expression)
		{
			Expression = expression;
		}

		public override global::System.Collections.Generic.IEnumerable<global::Newtonsoft.Json.Linq.JToken> ExecuteFilter(global::Newtonsoft.Json.Linq.JToken root, global::System.Collections.Generic.IEnumerable<global::Newtonsoft.Json.Linq.JToken> current, global::Newtonsoft.Json.Linq.JsonSelectSettings? settings)
		{
			foreach (global::Newtonsoft.Json.Linq.JToken item in current)
			{
				if (item is global::Newtonsoft.Json.Linq.JContainer jContainer)
				{
					foreach (global::Newtonsoft.Json.Linq.JToken item2 in jContainer.DescendantsAndSelf())
					{
						if (Expression.IsMatch(root, item2, settings))
						{
							yield return item2;
						}
					}
				}
				else if (Expression.IsMatch(root, item, settings))
				{
					yield return item;
				}
			}
		}
	}
}
