namespace Newtonsoft.Json.Utilities
{
	internal class NoThrowExpressionVisitor : global::System.Linq.Expressions.ExpressionVisitor
	{
		internal static readonly object ErrorResult = new object();

		protected override global::System.Linq.Expressions.Expression VisitConditional(global::System.Linq.Expressions.ConditionalExpression node)
		{
			if (node.IfFalse.NodeType == global::System.Linq.Expressions.ExpressionType.Throw)
			{
				return global::System.Linq.Expressions.Expression.Condition(node.Test, node.IfTrue, global::System.Linq.Expressions.Expression.Constant(ErrorResult));
			}
			return base.VisitConditional(node);
		}
	}
}
