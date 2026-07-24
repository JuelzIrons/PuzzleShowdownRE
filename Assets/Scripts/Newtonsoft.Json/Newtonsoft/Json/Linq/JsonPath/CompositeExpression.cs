namespace Newtonsoft.Json.Linq.JsonPath
{
	internal class CompositeExpression : global::Newtonsoft.Json.Linq.JsonPath.QueryExpression
	{
		public global::System.Collections.Generic.List<global::Newtonsoft.Json.Linq.JsonPath.QueryExpression> Expressions { get; set; }

		public CompositeExpression(global::Newtonsoft.Json.Linq.JsonPath.QueryOperator @operator)
			: base(@operator)
		{
			Expressions = new global::System.Collections.Generic.List<global::Newtonsoft.Json.Linq.JsonPath.QueryExpression>();
		}

		public override bool IsMatch(global::Newtonsoft.Json.Linq.JToken root, global::Newtonsoft.Json.Linq.JToken t, global::Newtonsoft.Json.Linq.JsonSelectSettings? settings)
		{
			switch (Operator)
			{
			case global::Newtonsoft.Json.Linq.JsonPath.QueryOperator.And:
				foreach (global::Newtonsoft.Json.Linq.JsonPath.QueryExpression expression in Expressions)
				{
					if (!expression.IsMatch(root, t, settings))
					{
						return false;
					}
				}
				return true;
			case global::Newtonsoft.Json.Linq.JsonPath.QueryOperator.Or:
				foreach (global::Newtonsoft.Json.Linq.JsonPath.QueryExpression expression2 in Expressions)
				{
					if (expression2.IsMatch(root, t, settings))
					{
						return true;
					}
				}
				return false;
			default:
				throw new global::System.ArgumentOutOfRangeException();
			}
		}
	}
}
