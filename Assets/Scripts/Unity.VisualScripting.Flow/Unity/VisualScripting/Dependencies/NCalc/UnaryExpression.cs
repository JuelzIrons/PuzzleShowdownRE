namespace Unity.VisualScripting.Dependencies.NCalc
{
	public class UnaryExpression : global::Unity.VisualScripting.Dependencies.NCalc.LogicalExpression
	{
		public global::Unity.VisualScripting.Dependencies.NCalc.LogicalExpression Expression { get; set; }

		public global::Unity.VisualScripting.Dependencies.NCalc.UnaryExpressionType Type { get; set; }

		public UnaryExpression(global::Unity.VisualScripting.Dependencies.NCalc.UnaryExpressionType type, global::Unity.VisualScripting.Dependencies.NCalc.LogicalExpression expression)
		{
			Type = type;
			Expression = expression;
		}

		public override void Accept(global::Unity.VisualScripting.Dependencies.NCalc.LogicalExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}
	}
}
