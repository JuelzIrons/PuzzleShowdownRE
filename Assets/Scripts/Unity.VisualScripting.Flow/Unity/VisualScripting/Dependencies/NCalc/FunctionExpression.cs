namespace Unity.VisualScripting.Dependencies.NCalc
{
	public class FunctionExpression : global::Unity.VisualScripting.Dependencies.NCalc.LogicalExpression
	{
		public global::Unity.VisualScripting.Dependencies.NCalc.IdentifierExpression Identifier { get; set; }

		public global::Unity.VisualScripting.Dependencies.NCalc.LogicalExpression[] Expressions { get; set; }

		public FunctionExpression(global::Unity.VisualScripting.Dependencies.NCalc.IdentifierExpression identifier, global::Unity.VisualScripting.Dependencies.NCalc.LogicalExpression[] expressions)
		{
			Identifier = identifier;
			Expressions = expressions;
		}

		public override void Accept(global::Unity.VisualScripting.Dependencies.NCalc.LogicalExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}
	}
}
