namespace Unity.VisualScripting.Dependencies.NCalc
{
	public class EvaluationVisitor : global::Unity.VisualScripting.Dependencies.NCalc.LogicalExpressionVisitor
	{
		private delegate T Func<T>();

		private readonly global::Unity.VisualScripting.Flow flow;

		private readonly global::Unity.VisualScripting.Dependencies.NCalc.EvaluateOptions options;

		private bool IgnoreCase => options.HasFlag(global::Unity.VisualScripting.Dependencies.NCalc.EvaluateOptions.IgnoreCase);

		public object Result { get; private set; }

		public global::System.Collections.Generic.Dictionary<string, object> Parameters { get; set; }

		public event global::Unity.VisualScripting.Dependencies.NCalc.EvaluateFunctionHandler EvaluateFunction;

		public event global::Unity.VisualScripting.Dependencies.NCalc.EvaluateParameterHandler EvaluateParameter;

		public EvaluationVisitor(global::Unity.VisualScripting.Flow flow, global::Unity.VisualScripting.Dependencies.NCalc.EvaluateOptions options)
		{
			this.flow = flow;
			this.options = options;
		}

		private object Evaluate(global::Unity.VisualScripting.Dependencies.NCalc.LogicalExpression expression)
		{
			expression.Accept(this);
			return Result;
		}

		public override void Visit(global::Unity.VisualScripting.Dependencies.NCalc.TernaryExpression ternary)
		{
			ternary.LeftExpression.Accept(this);
			if (global::Unity.VisualScripting.ConversionUtility.Convert<bool>(Result))
			{
				ternary.MiddleExpression.Accept(this);
			}
			else
			{
				ternary.RightExpression.Accept(this);
			}
		}

		public override void Visit(global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpression binary)
		{
			object leftValue = null;
			global::Unity.VisualScripting.Dependencies.NCalc.EvaluationVisitor.Func<object> func = delegate
			{
				if (leftValue == null)
				{
					binary.LeftExpression.Accept(this);
					leftValue = Result;
				}
				return leftValue;
			};
			object rightValue = null;
			global::Unity.VisualScripting.Dependencies.NCalc.EvaluationVisitor.Func<object> func2 = delegate
			{
				if (rightValue == null)
				{
					binary.RightExpression.Accept(this);
					rightValue = Result;
				}
				return rightValue;
			};
			switch (binary.Type)
			{
			case global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpressionType.And:
				Result = global::Unity.VisualScripting.ConversionUtility.Convert<bool>(func()) && global::Unity.VisualScripting.ConversionUtility.Convert<bool>(func2());
				break;
			case global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpressionType.Or:
				Result = global::Unity.VisualScripting.ConversionUtility.Convert<bool>(func()) || global::Unity.VisualScripting.ConversionUtility.Convert<bool>(func2());
				break;
			case global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpressionType.Div:
				Result = global::Unity.VisualScripting.OperatorUtility.Divide(func(), func2());
				break;
			case global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpressionType.Equal:
				Result = global::Unity.VisualScripting.OperatorUtility.Equal(func(), func2());
				break;
			case global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpressionType.Greater:
				Result = global::Unity.VisualScripting.OperatorUtility.GreaterThan(func(), func2());
				break;
			case global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpressionType.GreaterOrEqual:
				Result = global::Unity.VisualScripting.OperatorUtility.GreaterThanOrEqual(func(), func2());
				break;
			case global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpressionType.Lesser:
				Result = global::Unity.VisualScripting.OperatorUtility.LessThan(func(), func2());
				break;
			case global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpressionType.LesserOrEqual:
				Result = global::Unity.VisualScripting.OperatorUtility.LessThanOrEqual(func(), func2());
				break;
			case global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpressionType.Minus:
				Result = global::Unity.VisualScripting.OperatorUtility.Subtract(func(), func2());
				break;
			case global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpressionType.Modulo:
				Result = global::Unity.VisualScripting.OperatorUtility.Modulo(func(), func2());
				break;
			case global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpressionType.NotEqual:
				Result = global::Unity.VisualScripting.OperatorUtility.NotEqual(func(), func2());
				break;
			case global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpressionType.Plus:
				Result = global::Unity.VisualScripting.OperatorUtility.Add(func(), func2());
				break;
			case global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpressionType.Times:
				Result = global::Unity.VisualScripting.OperatorUtility.Multiply(func(), func2());
				break;
			case global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpressionType.BitwiseAnd:
				Result = global::Unity.VisualScripting.OperatorUtility.And(func(), func2());
				break;
			case global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpressionType.BitwiseOr:
				Result = global::Unity.VisualScripting.OperatorUtility.Or(func(), func2());
				break;
			case global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpressionType.BitwiseXOr:
				Result = global::Unity.VisualScripting.OperatorUtility.ExclusiveOr(func(), func2());
				break;
			case global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpressionType.LeftShift:
				Result = global::Unity.VisualScripting.OperatorUtility.LeftShift(func(), func2());
				break;
			case global::Unity.VisualScripting.Dependencies.NCalc.BinaryExpressionType.RightShift:
				Result = global::Unity.VisualScripting.OperatorUtility.RightShift(func(), func2());
				break;
			}
		}

		public override void Visit(global::Unity.VisualScripting.Dependencies.NCalc.UnaryExpression unary)
		{
			unary.Expression.Accept(this);
			switch (unary.Type)
			{
			case global::Unity.VisualScripting.Dependencies.NCalc.UnaryExpressionType.Not:
				Result = !global::Unity.VisualScripting.ConversionUtility.Convert<bool>(Result);
				break;
			case global::Unity.VisualScripting.Dependencies.NCalc.UnaryExpressionType.Negate:
				Result = global::Unity.VisualScripting.OperatorUtility.Negate(Result);
				break;
			case global::Unity.VisualScripting.Dependencies.NCalc.UnaryExpressionType.BitwiseNot:
				Result = global::Unity.VisualScripting.OperatorUtility.Not(Result);
				break;
			}
		}

		public override void Visit(global::Unity.VisualScripting.Dependencies.NCalc.ValueExpression value)
		{
			Result = value.Value;
		}

		public override void Visit(global::Unity.VisualScripting.Dependencies.NCalc.FunctionExpression function)
		{
			global::Unity.VisualScripting.Dependencies.NCalc.FunctionArgs functionArgs = new global::Unity.VisualScripting.Dependencies.NCalc.FunctionArgs
			{
				Parameters = new global::Unity.VisualScripting.Dependencies.NCalc.Expression[function.Expressions.Length]
			};
			for (int i = 0; i < function.Expressions.Length; i++)
			{
				functionArgs.Parameters[i] = new global::Unity.VisualScripting.Dependencies.NCalc.Expression(function.Expressions[i], options);
				functionArgs.Parameters[i].EvaluateFunction += this.EvaluateFunction;
				functionArgs.Parameters[i].EvaluateParameter += this.EvaluateParameter;
				functionArgs.Parameters[i].Parameters = Parameters;
			}
			OnEvaluateFunction(IgnoreCase ? function.Identifier.Name.ToLower() : function.Identifier.Name, functionArgs);
			if (functionArgs.HasResult)
			{
				Result = functionArgs.Result;
				return;
			}
			switch (function.Identifier.Name.ToLower(global::System.Globalization.CultureInfo.InvariantCulture))
			{
			case "abs":
				CheckCase(function, "Abs");
				CheckExactArgumentCount(function, 1);
				Result = global::UnityEngine.Mathf.Abs(global::Unity.VisualScripting.ConversionUtility.Convert<float>(Evaluate(function.Expressions[0])));
				break;
			case "acos":
				CheckCase(function, "Acos");
				CheckExactArgumentCount(function, 1);
				Result = global::UnityEngine.Mathf.Acos(global::Unity.VisualScripting.ConversionUtility.Convert<float>(Evaluate(function.Expressions[0])));
				break;
			case "asin":
				CheckCase(function, "Asin");
				CheckExactArgumentCount(function, 1);
				Result = global::UnityEngine.Mathf.Asin(global::Unity.VisualScripting.ConversionUtility.Convert<float>(Evaluate(function.Expressions[0])));
				break;
			case "atan":
				CheckCase(function, "Atan");
				CheckExactArgumentCount(function, 1);
				Result = global::UnityEngine.Mathf.Atan(global::Unity.VisualScripting.ConversionUtility.Convert<float>(Evaluate(function.Expressions[0])));
				break;
			case "ceil":
				CheckCase(function, "Ceil");
				CheckExactArgumentCount(function, 1);
				Result = global::UnityEngine.Mathf.Ceil(global::Unity.VisualScripting.ConversionUtility.Convert<float>(Evaluate(function.Expressions[0])));
				break;
			case "cos":
				CheckCase(function, "Cos");
				CheckExactArgumentCount(function, 1);
				Result = global::UnityEngine.Mathf.Cos(global::Unity.VisualScripting.ConversionUtility.Convert<float>(Evaluate(function.Expressions[0])));
				break;
			case "exp":
				CheckCase(function, "Exp");
				CheckExactArgumentCount(function, 1);
				Result = global::UnityEngine.Mathf.Exp(global::Unity.VisualScripting.ConversionUtility.Convert<float>(Evaluate(function.Expressions[0])));
				break;
			case "floor":
				CheckCase(function, "Floor");
				CheckExactArgumentCount(function, 1);
				Result = global::UnityEngine.Mathf.Floor(global::Unity.VisualScripting.ConversionUtility.Convert<float>(Evaluate(function.Expressions[0])));
				break;
			case "truncate":
				CheckCase(function, "Truncate");
				CheckExactArgumentCount(function, 1);
				Result = global::UnityEngine.Mathf.FloorToInt(global::Unity.VisualScripting.ConversionUtility.Convert<float>(Evaluate(function.Expressions[0])));
				break;
			case "log":
				CheckCase(function, "Log");
				CheckExactArgumentCount(function, 2);
				Result = global::UnityEngine.Mathf.Log(global::Unity.VisualScripting.ConversionUtility.Convert<float>(Evaluate(function.Expressions[0])), global::Unity.VisualScripting.ConversionUtility.Convert<float>(Evaluate(function.Expressions[1])));
				break;
			case "log10":
				CheckCase(function, "Log10");
				CheckExactArgumentCount(function, 1);
				Result = global::UnityEngine.Mathf.Log10(global::Unity.VisualScripting.ConversionUtility.Convert<float>(Evaluate(function.Expressions[0])));
				break;
			case "pow":
				CheckCase(function, "Pow");
				CheckExactArgumentCount(function, 2);
				Result = global::UnityEngine.Mathf.Pow(global::Unity.VisualScripting.ConversionUtility.Convert<float>(Evaluate(function.Expressions[0])), global::Unity.VisualScripting.ConversionUtility.Convert<float>(Evaluate(function.Expressions[1])));
				break;
			case "round":
				CheckCase(function, "Round");
				CheckExactArgumentCount(function, 1);
				Result = global::UnityEngine.Mathf.Round(global::Unity.VisualScripting.ConversionUtility.Convert<float>(Evaluate(function.Expressions[0])));
				break;
			case "sign":
				CheckCase(function, "Sign");
				CheckExactArgumentCount(function, 1);
				Result = global::UnityEngine.Mathf.Sign(global::Unity.VisualScripting.ConversionUtility.Convert<float>(Evaluate(function.Expressions[0])));
				break;
			case "sin":
				CheckCase(function, "Sin");
				CheckExactArgumentCount(function, 1);
				Result = global::UnityEngine.Mathf.Sin(global::Unity.VisualScripting.ConversionUtility.Convert<float>(Evaluate(function.Expressions[0])));
				break;
			case "sqrt":
				CheckCase(function, "Sqrt");
				CheckExactArgumentCount(function, 1);
				Result = global::UnityEngine.Mathf.Sqrt(global::Unity.VisualScripting.ConversionUtility.Convert<float>(Evaluate(function.Expressions[0])));
				break;
			case "tan":
				CheckCase(function, "Tan");
				CheckExactArgumentCount(function, 1);
				Result = global::UnityEngine.Mathf.Tan(global::Unity.VisualScripting.ConversionUtility.Convert<float>(Evaluate(function.Expressions[0])));
				break;
			case "max":
				CheckCase(function, "Max");
				CheckExactArgumentCount(function, 2);
				Result = global::UnityEngine.Mathf.Max(global::Unity.VisualScripting.ConversionUtility.Convert<float>(Evaluate(function.Expressions[0])), global::Unity.VisualScripting.ConversionUtility.Convert<float>(Evaluate(function.Expressions[1])));
				break;
			case "min":
				CheckCase(function, "Min");
				CheckExactArgumentCount(function, 2);
				Result = global::UnityEngine.Mathf.Min(global::Unity.VisualScripting.ConversionUtility.Convert<float>(Evaluate(function.Expressions[0])), global::Unity.VisualScripting.ConversionUtility.Convert<float>(Evaluate(function.Expressions[1])));
				break;
			case "in":
			{
				CheckCase(function, "In");
				CheckExactArgumentCount(function, 2);
				object objA = Evaluate(function.Expressions[0]);
				bool flag = false;
				for (int j = 1; j < function.Expressions.Length; j++)
				{
					object objB = Evaluate(function.Expressions[j]);
					if (object.Equals(objA, objB))
					{
						flag = true;
						break;
					}
				}
				Result = flag;
				break;
			}
			default:
				throw new global::System.ArgumentException("Function not found", function.Identifier.Name);
			}
		}

		private void CheckCase(global::Unity.VisualScripting.Dependencies.NCalc.FunctionExpression function, string reference)
		{
			string name = function.Identifier.Name;
			if (IgnoreCase)
			{
				if (!string.Equals(name, reference, global::System.StringComparison.InvariantCultureIgnoreCase))
				{
					throw new global::System.ArgumentException("Function not found.", name);
				}
			}
			else if (name != reference)
			{
				throw new global::System.ArgumentException("Function not found: '" + name + "'. Try '" + reference + "' instead.");
			}
		}

		private void OnEvaluateFunction(string name, global::Unity.VisualScripting.Dependencies.NCalc.FunctionArgs args)
		{
			this.EvaluateFunction?.Invoke(flow, name, args);
		}

		public override void Visit(global::Unity.VisualScripting.Dependencies.NCalc.IdentifierExpression identifier)
		{
			if (Parameters.ContainsKey(identifier.Name))
			{
				if (Parameters[identifier.Name] is global::Unity.VisualScripting.Dependencies.NCalc.Expression)
				{
					global::Unity.VisualScripting.Dependencies.NCalc.Expression expression = (global::Unity.VisualScripting.Dependencies.NCalc.Expression)Parameters[identifier.Name];
					foreach (global::System.Collections.Generic.KeyValuePair<string, object> parameter in Parameters)
					{
						expression.Parameters[parameter.Key] = parameter.Value;
					}
					expression.EvaluateFunction += this.EvaluateFunction;
					expression.EvaluateParameter += this.EvaluateParameter;
					Result = ((global::Unity.VisualScripting.Dependencies.NCalc.Expression)Parameters[identifier.Name]).Evaluate(flow);
				}
				else
				{
					Result = Parameters[identifier.Name];
				}
			}
			else
			{
				global::Unity.VisualScripting.Dependencies.NCalc.ParameterArgs parameterArgs = new global::Unity.VisualScripting.Dependencies.NCalc.ParameterArgs();
				OnEvaluateParameter(identifier.Name, parameterArgs);
				if (!parameterArgs.HasResult)
				{
					throw new global::System.ArgumentException("Parameter was not defined", identifier.Name);
				}
				Result = parameterArgs.Result;
			}
		}

		private void OnEvaluateParameter(string name, global::Unity.VisualScripting.Dependencies.NCalc.ParameterArgs args)
		{
			this.EvaluateParameter?.Invoke(flow, name, args);
		}

		public static void CheckExactArgumentCount(global::Unity.VisualScripting.Dependencies.NCalc.FunctionExpression function, int count)
		{
			if (function.Expressions.Length != count)
			{
				throw new global::System.ArgumentException($"{function.Identifier.Name}() takes at exactly {count} arguments. {function.Expressions.Length} provided.");
			}
		}

		public static void CheckMinArgumentCount(global::Unity.VisualScripting.Dependencies.NCalc.FunctionExpression function, int count)
		{
			if (function.Expressions.Length < count)
			{
				throw new global::System.ArgumentException($"{function.Identifier.Name}() takes at at least {count} arguments. {function.Expressions.Length} provided.");
			}
		}
	}
}
