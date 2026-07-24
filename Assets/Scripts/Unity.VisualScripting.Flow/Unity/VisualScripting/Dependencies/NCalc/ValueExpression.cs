namespace Unity.VisualScripting.Dependencies.NCalc
{
	public class ValueExpression : global::Unity.VisualScripting.Dependencies.NCalc.LogicalExpression
	{
		public object Value { get; set; }

		public global::Unity.VisualScripting.Dependencies.NCalc.ValueType Type { get; set; }

		public ValueExpression(object value, global::Unity.VisualScripting.Dependencies.NCalc.ValueType type)
		{
			Value = value;
			Type = type;
		}

		public ValueExpression(object value)
		{
			switch (global::System.Type.GetTypeCode(value.GetType()))
			{
			case global::System.TypeCode.Boolean:
				Type = global::Unity.VisualScripting.Dependencies.NCalc.ValueType.Boolean;
				break;
			case global::System.TypeCode.DateTime:
				Type = global::Unity.VisualScripting.Dependencies.NCalc.ValueType.DateTime;
				break;
			case global::System.TypeCode.Single:
			case global::System.TypeCode.Double:
			case global::System.TypeCode.Decimal:
				Type = global::Unity.VisualScripting.Dependencies.NCalc.ValueType.Float;
				break;
			case global::System.TypeCode.SByte:
			case global::System.TypeCode.Byte:
			case global::System.TypeCode.Int16:
			case global::System.TypeCode.UInt16:
			case global::System.TypeCode.Int32:
			case global::System.TypeCode.UInt32:
			case global::System.TypeCode.Int64:
			case global::System.TypeCode.UInt64:
				Type = global::Unity.VisualScripting.Dependencies.NCalc.ValueType.Integer;
				break;
			case global::System.TypeCode.String:
				Type = global::Unity.VisualScripting.Dependencies.NCalc.ValueType.String;
				break;
			default:
				throw new global::Unity.VisualScripting.Dependencies.NCalc.EvaluationException("This value could not be handled: " + value);
			}
			Value = value;
		}

		public ValueExpression(string value)
		{
			Value = value;
			Type = global::Unity.VisualScripting.Dependencies.NCalc.ValueType.String;
		}

		public ValueExpression(int value)
		{
			Value = value;
			Type = global::Unity.VisualScripting.Dependencies.NCalc.ValueType.Integer;
		}

		public ValueExpression(float value)
		{
			Value = value;
			Type = global::Unity.VisualScripting.Dependencies.NCalc.ValueType.Float;
		}

		public ValueExpression(global::System.DateTime value)
		{
			Value = value;
			Type = global::Unity.VisualScripting.Dependencies.NCalc.ValueType.DateTime;
		}

		public ValueExpression(bool value)
		{
			Value = value;
			Type = global::Unity.VisualScripting.Dependencies.NCalc.ValueType.Boolean;
		}

		public override void Accept(global::Unity.VisualScripting.Dependencies.NCalc.LogicalExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}
	}
}
