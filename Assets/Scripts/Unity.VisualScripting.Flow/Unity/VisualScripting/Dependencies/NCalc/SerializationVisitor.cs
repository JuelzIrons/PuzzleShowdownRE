namespace Unity.VisualScripting.Dependencies.NCalc
{
	public class SerializationVisitor : global::Unity.VisualScripting.Dependencies.NCalc.LogicalExpressionVisitor
	{
		private readonly global::System.Globalization.NumberFormatInfo _numberFormatInfo;

		public global::System.Text.StringBuilder Result { get; protected set; }

		public SerializationVisitor()
		{
			Result = new global::System.Text.StringBuilder();
			_numberFormatInfo = new global::System.Globalization.NumberFormatInfo
			{
				NumberDecimalSeparator = "."
			};
		}

		public override void Visit(global::Unity.VisualScripting.Dependencies.NCalc.TernaryExpression ternary)
		{
			EncapsulateNoValue(ternary.LeftExpression);
			Result.Append("? ");
			EncapsulateNoValue(ternary.MiddleExpression);
			Result.Append(": ");
			EncapsulateNoValue(ternary.RightExpression);
		}

		public override void Visit(global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpression binary)
		{
			EncapsulateNoValue(binary.LeftExpression);
			switch (binary.Type)
			{
			case global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpressionType.And:
				Result.Append("and ");
				break;
			case global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpressionType.Or:
				Result.Append("or ");
				break;
			case global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpressionType.Div:
				Result.Append("/ ");
				break;
			case global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpressionType.Equal:
				Result.Append("= ");
				break;
			case global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpressionType.Greater:
				Result.Append("> ");
				break;
			case global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpressionType.GreaterOrEqual:
				Result.Append(">= ");
				break;
			case global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpressionType.Lesser:
				Result.Append("< ");
				break;
			case global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpressionType.LesserOrEqual:
				Result.Append("<= ");
				break;
			case global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpressionType.Minus:
				Result.Append("- ");
				break;
			case global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpressionType.Modulo:
				Result.Append("% ");
				break;
			case global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpressionType.NotEqual:
				Result.Append("!= ");
				break;
			case global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpressionType.Plus:
				Result.Append("+ ");
				break;
			case global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpressionType.Times:
				Result.Append("* ");
				break;
			case global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpressionType.BitwiseAnd:
				Result.Append("& ");
				break;
			case global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpressionType.BitwiseOr:
				Result.Append("| ");
				break;
			case global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpressionType.BitwiseXOr:
				Result.Append("~ ");
				break;
			case global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpressionType.LeftShift:
				Result.Append("<< ");
				break;
			case global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpressionType.RightShift:
				Result.Append(">> ");
				break;
			}
			EncapsulateNoValue(binary.RightExpression);
		}

		public override void Visit(global::Unity.VisualScripting.Dependencies.NCalc.UnaryExpression unary)
		{
			switch (unary.Type)
			{
			case global::Unity.VisualScripting.Dependencies.NCalc.UnaryExpressionType.Not:
				Result.Append("!");
				break;
			case global::Unity.VisualScripting.Dependencies.NCalc.UnaryExpressionType.Negate:
				Result.Append("-");
				break;
			case global::Unity.VisualScripting.Dependencies.NCalc.UnaryExpressionType.BitwiseNot:
				Result.Append("~");
				break;
			}
			EncapsulateNoValue(unary.Expression);
		}

		public override void Visit(global::Unity.VisualScripting.Dependencies.NCalc.ValueExpression value)
		{
			switch (value.Type)
			{
			case global::Unity.VisualScripting.Dependencies.NCalc.ValueType.Boolean:
				Result.Append(value.Value).Append(" ");
				break;
			case global::Unity.VisualScripting.Dependencies.NCalc.ValueType.DateTime:
				Result.Append("#").Append(value.Value).Append("#")
					.Append(" ");
				break;
			case global::Unity.VisualScripting.Dependencies.NCalc.ValueType.Float:
				Result.Append(decimal.Parse(value.Value.ToString()).ToString(_numberFormatInfo)).Append(" ");
				break;
			case global::Unity.VisualScripting.Dependencies.NCalc.ValueType.Integer:
				Result.Append(value.Value).Append(" ");
				break;
			case global::Unity.VisualScripting.Dependencies.NCalc.ValueType.String:
				Result.Append("'").Append(value.Value).Append("'")
					.Append(" ");
				break;
			}
		}

		public override void Visit(global::Unity.VisualScripting.Dependencies.NCalc.FunctionExpression function)
		{
			Result.Append(function.Identifier.Name);
			Result.Append("(");
			for (int i = 0; i < function.Expressions.Length; i++)
			{
				function.Expressions[i].Accept(this);
				if (i < function.Expressions.Length - 1)
				{
					Result.Remove(Result.Length - 1, 1);
					Result.Append(", ");
				}
			}
			while (Result[Result.Length - 1] == ' ')
			{
				Result.Remove(Result.Length - 1, 1);
			}
			Result.Append(") ");
		}

		public override void Visit(global::Unity.VisualScripting.Dependencies.NCalc.IdentifierExpression identifier)
		{
			Result.Append("[").Append(identifier.Name).Append("] ");
		}

		protected void EncapsulateNoValue(global::Unity.VisualScripting.Dependencies.NCalc.LogicalExpression expression)
		{
			if (expression is global::Unity.VisualScripting.Dependencies.NCalc.ValueExpression)
			{
				expression.Accept(this);
				return;
			}
			Result.Append("(");
			expression.Accept(this);
			while (Result[Result.Length - 1] == ' ')
			{
				Result.Remove(Result.Length - 1, 1);
			}
			Result.Append(") ");
		}
	}
}
