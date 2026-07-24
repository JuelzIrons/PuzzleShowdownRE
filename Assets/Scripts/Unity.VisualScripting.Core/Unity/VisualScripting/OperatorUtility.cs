namespace Unity.VisualScripting
{
	public static class OperatorUtility
	{
		public static readonly global::System.Collections.Generic.Dictionary<string, string> operatorNames;

		public static readonly global::System.Collections.Generic.Dictionary<string, int> operatorRanks;

		private static readonly global::System.Collections.Generic.Dictionary<global::Unity.VisualScripting.UnaryOperator, global::Unity.VisualScripting.UnaryOperatorHandler> unaryOperatorHandlers;

		private static readonly global::System.Collections.Generic.Dictionary<global::Unity.VisualScripting.BinaryOperator, global::Unity.VisualScripting.BinaryOperatorHandler> binaryOpeatorHandlers;

		private static readonly global::Unity.VisualScripting.LogicalNegationHandler logicalNegationHandler;

		private static readonly global::Unity.VisualScripting.NumericNegationHandler numericNegationHandler;

		private static readonly global::Unity.VisualScripting.IncrementHandler incrementHandler;

		private static readonly global::Unity.VisualScripting.DecrementHandler decrementHandler;

		private static readonly global::Unity.VisualScripting.PlusHandler plusHandler;

		private static readonly global::Unity.VisualScripting.AdditionHandler additionHandler;

		private static readonly global::Unity.VisualScripting.SubtractionHandler subtractionHandler;

		private static readonly global::Unity.VisualScripting.MultiplicationHandler multiplicationHandler;

		private static readonly global::Unity.VisualScripting.DivisionHandler divisionHandler;

		private static readonly global::Unity.VisualScripting.ModuloHandler moduloHandler;

		private static readonly global::Unity.VisualScripting.AndHandler andHandler;

		private static readonly global::Unity.VisualScripting.OrHandler orHandler;

		private static readonly global::Unity.VisualScripting.ExclusiveOrHandler exclusiveOrHandler;

		private static readonly global::Unity.VisualScripting.EqualityHandler equalityHandler;

		private static readonly global::Unity.VisualScripting.InequalityHandler inequalityHandler;

		private static readonly global::Unity.VisualScripting.GreaterThanHandler greaterThanHandler;

		private static readonly global::Unity.VisualScripting.LessThanHandler lessThanHandler;

		private static readonly global::Unity.VisualScripting.GreaterThanOrEqualHandler greaterThanOrEqualHandler;

		private static readonly global::Unity.VisualScripting.LessThanOrEqualHandler lessThanOrEqualHandler;

		private static readonly global::Unity.VisualScripting.LeftShiftHandler leftShiftHandler;

		private static readonly global::Unity.VisualScripting.RightShiftHandler rightShiftHandler;

		static OperatorUtility()
		{
			operatorNames = new global::System.Collections.Generic.Dictionary<string, string>
			{
				{ "op_Addition", "+" },
				{ "op_Subtraction", "-" },
				{ "op_Multiply", "*" },
				{ "op_Division", "/" },
				{ "op_Modulus", "%" },
				{ "op_ExclusiveOr", "^" },
				{ "op_BitwiseAnd", "&" },
				{ "op_BitwiseOr", "|" },
				{ "op_LogicalAnd", "&&" },
				{ "op_LogicalOr", "||" },
				{ "op_Assign", "=" },
				{ "op_LeftShift", "<<" },
				{ "op_RightShift", ">>" },
				{ "op_Equality", "==" },
				{ "op_GreaterThan", ">" },
				{ "op_LessThan", "<" },
				{ "op_Inequality", "!=" },
				{ "op_GreaterThanOrEqual", ">=" },
				{ "op_LessThanOrEqual", "<=" },
				{ "op_MultiplicationAssignment", "*=" },
				{ "op_SubtractionAssignment", "-=" },
				{ "op_ExclusiveOrAssignment", "^=" },
				{ "op_LeftShiftAssignment", "<<=" },
				{ "op_ModulusAssignment", "%=" },
				{ "op_AdditionAssignment", "+=" },
				{ "op_BitwiseAndAssignment", "&=" },
				{ "op_BitwiseOrAssignment", "|=" },
				{ "op_Comma", "," },
				{ "op_DivisionAssignment", "/=" },
				{ "op_Decrement", "--" },
				{ "op_Increment", "++" },
				{ "op_UnaryNegation", "-" },
				{ "op_UnaryPlus", "+" },
				{ "op_OnesComplement", "~" }
			};
			operatorRanks = new global::System.Collections.Generic.Dictionary<string, int>
			{
				{ "op_Addition", 2 },
				{ "op_Subtraction", 2 },
				{ "op_Multiply", 2 },
				{ "op_Division", 2 },
				{ "op_Modulus", 2 },
				{ "op_ExclusiveOr", 2 },
				{ "op_BitwiseAnd", 2 },
				{ "op_BitwiseOr", 2 },
				{ "op_LogicalAnd", 2 },
				{ "op_LogicalOr", 2 },
				{ "op_Assign", 2 },
				{ "op_LeftShift", 2 },
				{ "op_RightShift", 2 },
				{ "op_Equality", 2 },
				{ "op_GreaterThan", 2 },
				{ "op_LessThan", 2 },
				{ "op_Inequality", 2 },
				{ "op_GreaterThanOrEqual", 2 },
				{ "op_LessThanOrEqual", 2 },
				{ "op_MultiplicationAssignment", 2 },
				{ "op_SubtractionAssignment", 2 },
				{ "op_ExclusiveOrAssignment", 2 },
				{ "op_LeftShiftAssignment", 2 },
				{ "op_ModulusAssignment", 2 },
				{ "op_AdditionAssignment", 2 },
				{ "op_BitwiseAndAssignment", 2 },
				{ "op_BitwiseOrAssignment", 2 },
				{ "op_Comma", 2 },
				{ "op_DivisionAssignment", 2 },
				{ "op_Decrement", 1 },
				{ "op_Increment", 1 },
				{ "op_UnaryNegation", 1 },
				{ "op_UnaryPlus", 1 },
				{ "op_OnesComplement", 1 }
			};
			unaryOperatorHandlers = new global::System.Collections.Generic.Dictionary<global::Unity.VisualScripting.UnaryOperator, global::Unity.VisualScripting.UnaryOperatorHandler>();
			binaryOpeatorHandlers = new global::System.Collections.Generic.Dictionary<global::Unity.VisualScripting.BinaryOperator, global::Unity.VisualScripting.BinaryOperatorHandler>();
			logicalNegationHandler = new global::Unity.VisualScripting.LogicalNegationHandler();
			numericNegationHandler = new global::Unity.VisualScripting.NumericNegationHandler();
			incrementHandler = new global::Unity.VisualScripting.IncrementHandler();
			decrementHandler = new global::Unity.VisualScripting.DecrementHandler();
			plusHandler = new global::Unity.VisualScripting.PlusHandler();
			additionHandler = new global::Unity.VisualScripting.AdditionHandler();
			subtractionHandler = new global::Unity.VisualScripting.SubtractionHandler();
			multiplicationHandler = new global::Unity.VisualScripting.MultiplicationHandler();
			divisionHandler = new global::Unity.VisualScripting.DivisionHandler();
			moduloHandler = new global::Unity.VisualScripting.ModuloHandler();
			andHandler = new global::Unity.VisualScripting.AndHandler();
			orHandler = new global::Unity.VisualScripting.OrHandler();
			exclusiveOrHandler = new global::Unity.VisualScripting.ExclusiveOrHandler();
			equalityHandler = new global::Unity.VisualScripting.EqualityHandler();
			inequalityHandler = new global::Unity.VisualScripting.InequalityHandler();
			greaterThanHandler = new global::Unity.VisualScripting.GreaterThanHandler();
			lessThanHandler = new global::Unity.VisualScripting.LessThanHandler();
			greaterThanOrEqualHandler = new global::Unity.VisualScripting.GreaterThanOrEqualHandler();
			lessThanOrEqualHandler = new global::Unity.VisualScripting.LessThanOrEqualHandler();
			leftShiftHandler = new global::Unity.VisualScripting.LeftShiftHandler();
			rightShiftHandler = new global::Unity.VisualScripting.RightShiftHandler();
			unaryOperatorHandlers.Add(global::Unity.VisualScripting.UnaryOperator.LogicalNegation, logicalNegationHandler);
			unaryOperatorHandlers.Add(global::Unity.VisualScripting.UnaryOperator.NumericNegation, numericNegationHandler);
			unaryOperatorHandlers.Add(global::Unity.VisualScripting.UnaryOperator.Increment, incrementHandler);
			unaryOperatorHandlers.Add(global::Unity.VisualScripting.UnaryOperator.Decrement, decrementHandler);
			unaryOperatorHandlers.Add(global::Unity.VisualScripting.UnaryOperator.Plus, plusHandler);
			binaryOpeatorHandlers.Add(global::Unity.VisualScripting.BinaryOperator.Addition, additionHandler);
			binaryOpeatorHandlers.Add(global::Unity.VisualScripting.BinaryOperator.Subtraction, subtractionHandler);
			binaryOpeatorHandlers.Add(global::Unity.VisualScripting.BinaryOperator.Multiplication, multiplicationHandler);
			binaryOpeatorHandlers.Add(global::Unity.VisualScripting.BinaryOperator.Division, divisionHandler);
			binaryOpeatorHandlers.Add(global::Unity.VisualScripting.BinaryOperator.Modulo, moduloHandler);
			binaryOpeatorHandlers.Add(global::Unity.VisualScripting.BinaryOperator.And, andHandler);
			binaryOpeatorHandlers.Add(global::Unity.VisualScripting.BinaryOperator.Or, orHandler);
			binaryOpeatorHandlers.Add(global::Unity.VisualScripting.BinaryOperator.ExclusiveOr, exclusiveOrHandler);
			binaryOpeatorHandlers.Add(global::Unity.VisualScripting.BinaryOperator.Equality, equalityHandler);
			binaryOpeatorHandlers.Add(global::Unity.VisualScripting.BinaryOperator.Inequality, inequalityHandler);
			binaryOpeatorHandlers.Add(global::Unity.VisualScripting.BinaryOperator.GreaterThan, greaterThanHandler);
			binaryOpeatorHandlers.Add(global::Unity.VisualScripting.BinaryOperator.LessThan, lessThanHandler);
			binaryOpeatorHandlers.Add(global::Unity.VisualScripting.BinaryOperator.GreaterThanOrEqual, greaterThanOrEqualHandler);
			binaryOpeatorHandlers.Add(global::Unity.VisualScripting.BinaryOperator.LessThanOrEqual, lessThanOrEqualHandler);
			binaryOpeatorHandlers.Add(global::Unity.VisualScripting.BinaryOperator.LeftShift, leftShiftHandler);
			binaryOpeatorHandlers.Add(global::Unity.VisualScripting.BinaryOperator.RightShift, rightShiftHandler);
		}

		public static global::Unity.VisualScripting.UnaryOperatorHandler GetHandler(global::Unity.VisualScripting.UnaryOperator @operator)
		{
			if (unaryOperatorHandlers.ContainsKey(@operator))
			{
				return unaryOperatorHandlers[@operator];
			}
			throw new global::Unity.VisualScripting.UnexpectedEnumValueException<global::Unity.VisualScripting.UnaryOperator>(@operator);
		}

		public static global::Unity.VisualScripting.BinaryOperatorHandler GetHandler(global::Unity.VisualScripting.BinaryOperator @operator)
		{
			if (binaryOpeatorHandlers.ContainsKey(@operator))
			{
				return binaryOpeatorHandlers[@operator];
			}
			throw new global::Unity.VisualScripting.UnexpectedEnumValueException<global::Unity.VisualScripting.BinaryOperator>(@operator);
		}

		public static string Symbol(this global::Unity.VisualScripting.UnaryOperator @operator)
		{
			return GetHandler(@operator).symbol;
		}

		public static string Symbol(this global::Unity.VisualScripting.BinaryOperator @operator)
		{
			return GetHandler(@operator).symbol;
		}

		public static string Name(this global::Unity.VisualScripting.UnaryOperator @operator)
		{
			return GetHandler(@operator).name;
		}

		public static string Name(this global::Unity.VisualScripting.BinaryOperator @operator)
		{
			return GetHandler(@operator).name;
		}

		public static string Verb(this global::Unity.VisualScripting.UnaryOperator @operator)
		{
			return GetHandler(@operator).verb;
		}

		public static string Verb(this global::Unity.VisualScripting.BinaryOperator @operator)
		{
			return GetHandler(@operator).verb;
		}

		public static object Operate(global::Unity.VisualScripting.UnaryOperator @operator, object x)
		{
			if (!unaryOperatorHandlers.ContainsKey(@operator))
			{
				throw new global::Unity.VisualScripting.UnexpectedEnumValueException<global::Unity.VisualScripting.UnaryOperator>(@operator);
			}
			return unaryOperatorHandlers[@operator].Operate(x);
		}

		public static object Operate(global::Unity.VisualScripting.BinaryOperator @operator, object a, object b)
		{
			if (!binaryOpeatorHandlers.ContainsKey(@operator))
			{
				throw new global::Unity.VisualScripting.UnexpectedEnumValueException<global::Unity.VisualScripting.BinaryOperator>(@operator);
			}
			return binaryOpeatorHandlers[@operator].Operate(a, b);
		}

		public static object Negate(object x)
		{
			return numericNegationHandler.Operate(x);
		}

		public static object Not(object x)
		{
			return logicalNegationHandler.Operate(x);
		}

		public static object UnaryPlus(object x)
		{
			return plusHandler.Operate(x);
		}

		public static object Increment(object x)
		{
			return incrementHandler.Operate(x);
		}

		public static object Decrement(object x)
		{
			return decrementHandler.Operate(x);
		}

		public static object And(object a, object b)
		{
			return andHandler.Operate(a, b);
		}

		public static object Or(object a, object b)
		{
			return orHandler.Operate(a, b);
		}

		public static object ExclusiveOr(object a, object b)
		{
			return exclusiveOrHandler.Operate(a, b);
		}

		public static object Add(object a, object b)
		{
			return additionHandler.Operate(a, b);
		}

		public static object Subtract(object a, object b)
		{
			return subtractionHandler.Operate(a, b);
		}

		public static object Multiply(object a, object b)
		{
			return multiplicationHandler.Operate(a, b);
		}

		public static object Divide(object a, object b)
		{
			return divisionHandler.Operate(a, b);
		}

		public static object Modulo(object a, object b)
		{
			return moduloHandler.Operate(a, b);
		}

		public static bool Equal(object a, object b)
		{
			return (bool)equalityHandler.Operate(a, b);
		}

		public static bool NotEqual(object a, object b)
		{
			return (bool)inequalityHandler.Operate(a, b);
		}

		public static bool GreaterThan(object a, object b)
		{
			return (bool)greaterThanHandler.Operate(a, b);
		}

		public static bool LessThan(object a, object b)
		{
			return (bool)lessThanHandler.Operate(a, b);
		}

		public static bool GreaterThanOrEqual(object a, object b)
		{
			return (bool)greaterThanOrEqualHandler.Operate(a, b);
		}

		public static bool LessThanOrEqual(object a, object b)
		{
			return (bool)lessThanOrEqualHandler.Operate(a, b);
		}

		public static object LeftShift(object a, object b)
		{
			return leftShiftHandler.Operate(a, b);
		}

		public static object RightShift(object a, object b)
		{
			return rightShiftHandler.Operate(a, b);
		}
	}
}
