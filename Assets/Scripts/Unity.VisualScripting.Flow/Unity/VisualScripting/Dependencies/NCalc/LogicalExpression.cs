namespace Unity.VisualScripting.Dependencies.NCalc
{
	public abstract class LogicalExpression
	{
		private const char BS = '\\';

		public global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpression And(global::Unity.VisualScripting.Dependencies.NCalc.LogicalExpression operand)
		{
			return new global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpression(global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpressionType.And, this, operand);
		}

		public global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpression And(object operand)
		{
			return new global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpression(global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpressionType.And, this, new global::Unity.VisualScripting.Dependencies.NCalc.ValueExpression(operand));
		}

		public global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpression DividedBy(global::Unity.VisualScripting.Dependencies.NCalc.LogicalExpression operand)
		{
			return new global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpression(global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpressionType.Div, this, operand);
		}

		public global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpression DividedBy(object operand)
		{
			return new global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpression(global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpressionType.Div, this, new global::Unity.VisualScripting.Dependencies.NCalc.ValueExpression(operand));
		}

		public global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpression EqualsTo(global::Unity.VisualScripting.Dependencies.NCalc.LogicalExpression operand)
		{
			return new global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpression(global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpressionType.Equal, this, operand);
		}

		public global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpression EqualsTo(object operand)
		{
			return new global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpression(global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpressionType.Equal, this, new global::Unity.VisualScripting.Dependencies.NCalc.ValueExpression(operand));
		}

		public global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpression GreaterThan(global::Unity.VisualScripting.Dependencies.NCalc.LogicalExpression operand)
		{
			return new global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpression(global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpressionType.Greater, this, operand);
		}

		public global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpression GreaterThan(object operand)
		{
			return new global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpression(global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpressionType.Greater, this, new global::Unity.VisualScripting.Dependencies.NCalc.ValueExpression(operand));
		}

		public global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpression GreaterOrEqualThan(global::Unity.VisualScripting.Dependencies.NCalc.LogicalExpression operand)
		{
			return new global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpression(global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpressionType.GreaterOrEqual, this, operand);
		}

		public global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpression GreaterOrEqualThan(object operand)
		{
			return new global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpression(global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpressionType.GreaterOrEqual, this, new global::Unity.VisualScripting.Dependencies.NCalc.ValueExpression(operand));
		}

		public global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpression LesserThan(global::Unity.VisualScripting.Dependencies.NCalc.LogicalExpression operand)
		{
			return new global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpression(global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpressionType.Lesser, this, operand);
		}

		public global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpression LesserThan(object operand)
		{
			return new global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpression(global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpressionType.Lesser, this, new global::Unity.VisualScripting.Dependencies.NCalc.ValueExpression(operand));
		}

		public global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpression LesserOrEqualThan(global::Unity.VisualScripting.Dependencies.NCalc.LogicalExpression operand)
		{
			return new global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpression(global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpressionType.LesserOrEqual, this, operand);
		}

		public global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpression LesserOrEqualThan(object operand)
		{
			return new global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpression(global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpressionType.LesserOrEqual, this, new global::Unity.VisualScripting.Dependencies.NCalc.ValueExpression(operand));
		}

		public global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpression Minus(global::Unity.VisualScripting.Dependencies.NCalc.LogicalExpression operand)
		{
			return new global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpression(global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpressionType.Minus, this, operand);
		}

		public global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpression Minus(object operand)
		{
			return new global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpression(global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpressionType.Minus, this, new global::Unity.VisualScripting.Dependencies.NCalc.ValueExpression(operand));
		}

		public global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpression Modulo(global::Unity.VisualScripting.Dependencies.NCalc.LogicalExpression operand)
		{
			return new global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpression(global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpressionType.Modulo, this, operand);
		}

		public global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpression Modulo(object operand)
		{
			return new global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpression(global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpressionType.Modulo, this, new global::Unity.VisualScripting.Dependencies.NCalc.ValueExpression(operand));
		}

		public global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpression NotEqual(global::Unity.VisualScripting.Dependencies.NCalc.LogicalExpression operand)
		{
			return new global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpression(global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpressionType.NotEqual, this, operand);
		}

		public global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpression NotEqual(object operand)
		{
			return new global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpression(global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpressionType.NotEqual, this, new global::Unity.VisualScripting.Dependencies.NCalc.ValueExpression(operand));
		}

		public global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpression Or(global::Unity.VisualScripting.Dependencies.NCalc.LogicalExpression operand)
		{
			return new global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpression(global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpressionType.Or, this, operand);
		}

		public global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpression Or(object operand)
		{
			return new global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpression(global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpressionType.Or, this, new global::Unity.VisualScripting.Dependencies.NCalc.ValueExpression(operand));
		}

		public global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpression Plus(global::Unity.VisualScripting.Dependencies.NCalc.LogicalExpression operand)
		{
			return new global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpression(global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpressionType.Plus, this, operand);
		}

		public global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpression Plus(object operand)
		{
			return new global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpression(global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpressionType.Plus, this, new global::Unity.VisualScripting.Dependencies.NCalc.ValueExpression(operand));
		}

		public global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpression Mult(global::Unity.VisualScripting.Dependencies.NCalc.LogicalExpression operand)
		{
			return new global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpression(global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpressionType.Times, this, operand);
		}

		public global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpression Mult(object operand)
		{
			return new global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpression(global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpressionType.Times, this, new global::Unity.VisualScripting.Dependencies.NCalc.ValueExpression(operand));
		}

		public global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpression BitwiseOr(global::Unity.VisualScripting.Dependencies.NCalc.LogicalExpression operand)
		{
			return new global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpression(global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpressionType.BitwiseOr, this, operand);
		}

		public global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpression BitwiseOr(object operand)
		{
			return new global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpression(global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpressionType.BitwiseOr, this, new global::Unity.VisualScripting.Dependencies.NCalc.ValueExpression(operand));
		}

		public global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpression BitwiseAnd(global::Unity.VisualScripting.Dependencies.NCalc.LogicalExpression operand)
		{
			return new global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpression(global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpressionType.BitwiseAnd, this, operand);
		}

		public global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpression BitwiseAnd(object operand)
		{
			return new global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpression(global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpressionType.BitwiseAnd, this, new global::Unity.VisualScripting.Dependencies.NCalc.ValueExpression(operand));
		}

		public global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpression BitwiseXOr(global::Unity.VisualScripting.Dependencies.NCalc.LogicalExpression operand)
		{
			return new global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpression(global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpressionType.BitwiseXOr, this, operand);
		}

		public global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpression BitwiseXOr(object operand)
		{
			return new global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpression(global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpressionType.BitwiseXOr, this, new global::Unity.VisualScripting.Dependencies.NCalc.ValueExpression(operand));
		}

		public global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpression LeftShift(global::Unity.VisualScripting.Dependencies.NCalc.LogicalExpression operand)
		{
			return new global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpression(global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpressionType.LeftShift, this, operand);
		}

		public global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpression LeftShift(object operand)
		{
			return new global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpression(global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpressionType.LeftShift, this, new global::Unity.VisualScripting.Dependencies.NCalc.ValueExpression(operand));
		}

		public global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpression RightShift(global::Unity.VisualScripting.Dependencies.NCalc.LogicalExpression operand)
		{
			return new global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpression(global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpressionType.RightShift, this, operand);
		}

		public global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpression RightShift(object operand)
		{
			return new global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpression(global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpressionType.RightShift, this, new global::Unity.VisualScripting.Dependencies.NCalc.ValueExpression(operand));
		}

		public override string ToString()
		{
			global::Unity.VisualScripting.Dependencies.NCalc.SerializationVisitor serializationVisitor = new global::Unity.VisualScripting.Dependencies.NCalc.SerializationVisitor();
			Accept(serializationVisitor);
			return serializationVisitor.Result.ToString().TrimEnd(' ');
		}

		public virtual void Accept(global::Unity.VisualScripting.Dependencies.NCalc.LogicalExpressionVisitor visitor)
		{
			throw new global::System.NotImplementedException();
		}

		private static string ExtractString(string text)
		{
			global::System.Text.StringBuilder stringBuilder = new global::System.Text.StringBuilder(text);
			int startIndex = 1;
			int num = -1;
			while ((num = stringBuilder.ToString().IndexOf('\\', startIndex)) != -1)
			{
				char c = stringBuilder[num + 1];
				switch (c)
				{
				case 'u':
				{
					string value = string.Concat(stringBuilder[num + 4], stringBuilder[num + 5]);
					string value2 = string.Concat(stringBuilder[num + 2], stringBuilder[num + 3]);
					char value3 = global::System.Text.Encoding.Unicode.GetChars(new byte[2]
					{
						global::System.Convert.ToByte(value, 16),
						global::System.Convert.ToByte(value2, 16)
					})[0];
					stringBuilder.Remove(num, 6).Insert(num, value3);
					break;
				}
				case 'n':
					stringBuilder.Remove(num, 2).Insert(num, '\n');
					break;
				case 'r':
					stringBuilder.Remove(num, 2).Insert(num, '\r');
					break;
				case 't':
					stringBuilder.Remove(num, 2).Insert(num, '\t');
					break;
				case '\'':
					stringBuilder.Remove(num, 2).Insert(num, '\'');
					break;
				case '\\':
					stringBuilder.Remove(num, 2).Insert(num, '\\');
					break;
				default:
					throw new global::System.ApplicationException("Unvalid escape sequence: \\" + c);
				}
				startIndex = num + 1;
			}
			stringBuilder.Remove(0, 1);
			stringBuilder.Remove(stringBuilder.Length - 1, 1);
			return stringBuilder.ToString();
		}
	}
}
