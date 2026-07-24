namespace Unity.VisualScripting.Dependencies.NCalc
{
	public class IdentifierExpression : global::Unity.VisualScripting.Dependencies.NCalc.LogicalExpression
	{
		public string Name { get; set; }

		public IdentifierExpression(string name)
		{
			Name = name;
		}

		public override void Accept(global::Unity.VisualScripting.Dependencies.NCalc.LogicalExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}
	}
}
