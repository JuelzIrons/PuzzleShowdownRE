namespace Newtonsoft.Json.Linq.JsonPath
{
	internal abstract class QueryExpression
	{
		internal global::Newtonsoft.Json.Linq.JsonPath.QueryOperator Operator;

		public QueryExpression(global::Newtonsoft.Json.Linq.JsonPath.QueryOperator @operator)
		{
			Operator = @operator;
		}

		public bool IsMatch(global::Newtonsoft.Json.Linq.JToken root, global::Newtonsoft.Json.Linq.JToken t)
		{
			return IsMatch(root, t, null);
		}

		public abstract bool IsMatch(global::Newtonsoft.Json.Linq.JToken root, global::Newtonsoft.Json.Linq.JToken t, global::Newtonsoft.Json.Linq.JsonSelectSettings? settings);
	}
}
