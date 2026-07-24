namespace Unity.VisualScripting.Dependencies.NCalc
{
	public abstract class LogicalExpressionVisitor
	{
		public abstract void Visit(global::Unity.VisualScripting.Dependencies.NCalc.TernaryExpression ternary);

		public abstract void Visit(global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpression binary);

		public abstract void Visit(global::Unity.VisualScripting.Dependencies.NCalc.UnaryExpression unary);

		public abstract void Visit(global::Unity.VisualScripting.Dependencies.NCalc.ValueExpression value);

		public abstract void Visit(global::Unity.VisualScripting.Dependencies.NCalc.FunctionExpression function);

		public abstract void Visit(global::Unity.VisualScripting.Dependencies.NCalc.IdentifierExpression identifier);
	}
}
