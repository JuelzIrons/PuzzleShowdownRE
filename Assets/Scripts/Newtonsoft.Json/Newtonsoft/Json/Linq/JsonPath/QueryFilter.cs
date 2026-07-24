namespace Newtonsoft.Json.Linq.JsonPath
{
	internal class QueryFilter : global::Newtonsoft.Json.Linq.JsonPath.PathFilter
	{
		internal global::Newtonsoft.Json.Linq.JsonPath.QueryExpression Expression;

		public QueryFilter(global::Newtonsoft.Json.Linq.JsonPath.QueryExpression expression)
		{
			Expression = expression;
		}

		public override global::System.Collections.Generic.IEnumerable<global::Newtonsoft.Json.Linq.JToken> ExecuteFilter(global::Newtonsoft.Json.Linq.JToken root, global::System.Collections.Generic.IEnumerable<global::Newtonsoft.Json.Linq.JToken> current, global::Newtonsoft.Json.Linq.JsonSelectSettings? settings)
		{
			foreach (global::Newtonsoft.Json.Linq.JToken item in current)
			{
				foreach (global::Newtonsoft.Json.Linq.JToken item2 in (global::System.Collections.Generic.IEnumerable<global::Newtonsoft.Json.Linq.JToken>)item)
				{
					if (Expression.IsMatch(root, item2, settings))
					{
						yield return item2;
					}
				}
			}
		}
	}
}
