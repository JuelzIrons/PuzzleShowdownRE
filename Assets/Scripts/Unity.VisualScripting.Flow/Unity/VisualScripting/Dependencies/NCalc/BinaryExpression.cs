namespace Unity.VisualScripting.Dependencies.NCalc
{
	public class BinaryExpression : global::Unity.VisualScripting.Dependencies.NCalc.LogicalExpression
	{
		public global::Unity.VisualScripting.Dependencies.NCalc.LogicalExpression LeftExpression { get; set; }

		public global::Unity.VisualScripting.Dependencies.NCalc.LogicalExpression RightExpression { get; set; }

		public global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpressionType Type { get; set; }

		public BinaryExpression(global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpressionType type, global::Unity.VisualScripting.Dependencies.NCalc.LogicalExpression leftExpression, global::Unity.VisualScripting.Dependencies.NCalc.LogicalExpression rightExpression)
		{
			Type = type;
			LeftExpression = leftExpression;
			RightExpression = rightExpression;
		}

		public override void Accept(global::Unity.VisualScripting.Dependencies.NCalc.LogicalExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}
	}
}
