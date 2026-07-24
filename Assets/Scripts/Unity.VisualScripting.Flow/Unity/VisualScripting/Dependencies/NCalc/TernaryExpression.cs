namespace Unity.VisualScripting.Dependencies.NCalc
{
	public class TernaryExpression : global::Unity.VisualScripting.Dependencies.NCalc.LogicalExpression
	{
		public global::Unity.VisualScripting.Dependencies.NCalc.LogicalExpression LeftExpression { get; set; }

		public global::Unity.VisualScripting.Dependencies.NCalc.LogicalExpression MiddleExpression { get; set; }

		public global::Unity.VisualScripting.Dependencies.NCalc.LogicalExpression RightExpression { get; set; }

		public TernaryExpression(global::Unity.VisualScripting.Dependencies.NCalc.LogicalExpression leftExpression, global::Unity.VisualScripting.Dependencies.NCalc.LogicalExpression middleExpression, global::Unity.VisualScripting.Dependencies.NCalc.LogicalExpression rightExpression)
		{
			LeftExpression = leftExpression;
			MiddleExpression = middleExpression;
			RightExpression = rightExpression;
		}

		public override void Accept(global::Unity.VisualScripting.Dependencies.NCalc.LogicalExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}
	}
}
