namespace Unity.VisualScripting
{
	public abstract class BinaryOperatorHandler : global::Unity.VisualScripting.OperatorHandler
	{
		private struct OperatorQuery : global::System.IEquatable<global::Unity.VisualScripting.BinaryOperatorHandler.OperatorQuery>
		{
			public readonly global::System.Type leftType;

			public readonly global::System.Type rightType;

			public OperatorQuery(global::System.Type leftType, global::System.Type rightType)
			{
				this.leftType = leftType;
				this.rightType = rightType;
			}

			public bool Equals(global::Unity.VisualScripting.BinaryOperatorHandler.OperatorQuery other)
			{
				if (leftType == other.leftType)
				{
					return rightType == other.rightType;
				}
				return false;
			}

			public override bool Equals(object obj)
			{
				if (!(obj is global::Unity.VisualScripting.BinaryOperatorHandler.OperatorQuery))
				{
					return false;
				}
				return Equals((global::Unity.VisualScripting.BinaryOperatorHandler.OperatorQuery)obj);
			}

			public override int GetHashCode()
			{
				return global::Unity.VisualScripting.HashUtility.GetHashCode(leftType, rightType);
			}
		}

		private readonly global::System.Collections.Generic.Dictionary<global::Unity.VisualScripting.BinaryOperatorHandler.OperatorQuery, global::System.Func<object, object, object>> handlers = new global::System.Collections.Generic.Dictionary<global::Unity.VisualScripting.BinaryOperatorHandler.OperatorQuery, global::System.Func<object, object, object>>();

		private readonly global::System.Collections.Generic.Dictionary<global::Unity.VisualScripting.BinaryOperatorHandler.OperatorQuery, global::Unity.VisualScripting.IOptimizedInvoker> userDefinedOperators = new global::System.Collections.Generic.Dictionary<global::Unity.VisualScripting.BinaryOperatorHandler.OperatorQuery, global::Unity.VisualScripting.IOptimizedInvoker>();

		private readonly global::System.Collections.Generic.Dictionary<global::Unity.VisualScripting.BinaryOperatorHandler.OperatorQuery, global::Unity.VisualScripting.BinaryOperatorHandler.OperatorQuery> userDefinedOperandTypes = new global::System.Collections.Generic.Dictionary<global::Unity.VisualScripting.BinaryOperatorHandler.OperatorQuery, global::Unity.VisualScripting.BinaryOperatorHandler.OperatorQuery>();

		protected BinaryOperatorHandler(string name, string verb, string symbol, string customMethodName)
			: base(name, verb, symbol, customMethodName)
		{
		}

		public virtual object Operate(object leftOperand, object rightOperand)
		{
			global::System.Type type = leftOperand?.GetType();
			global::System.Type type2 = rightOperand?.GetType();
			global::Unity.VisualScripting.BinaryOperatorHandler.OperatorQuery key;
			if (type != null && type2 != null)
			{
				key = new global::Unity.VisualScripting.BinaryOperatorHandler.OperatorQuery(type, type2);
			}
			else if (type != null && type.IsNullable())
			{
				key = new global::Unity.VisualScripting.BinaryOperatorHandler.OperatorQuery(type, type);
			}
			else
			{
				if (!(type2 != null) || !type2.IsNullable())
				{
					if (type == null && type2 == null)
					{
						return BothNullHandling();
					}
					return SingleNullHandling();
				}
				key = new global::Unity.VisualScripting.BinaryOperatorHandler.OperatorQuery(type2, type2);
			}
			if (handlers.ContainsKey(key))
			{
				return handlers[key](leftOperand, rightOperand);
			}
			if (base.customMethodName != null)
			{
				if (!userDefinedOperators.ContainsKey(key))
				{
					global::System.Reflection.MethodInfo method = key.leftType.GetMethod(base.customMethodName, global::System.Reflection.BindingFlags.Static | global::System.Reflection.BindingFlags.Public, null, new global::System.Type[2] { key.leftType, key.rightType }, null);
					if (key.leftType != key.rightType)
					{
						global::System.Reflection.MethodInfo method2 = key.rightType.GetMethod(base.customMethodName, global::System.Reflection.BindingFlags.Static | global::System.Reflection.BindingFlags.Public, null, new global::System.Type[2] { key.leftType, key.rightType }, null);
						if (method != null && method2 != null)
						{
							throw new global::Unity.VisualScripting.AmbiguousOperatorException(base.symbol, key.leftType, key.rightType);
						}
						global::System.Reflection.MethodInfo methodInfo = method ?? method2;
						if (methodInfo != null)
						{
							userDefinedOperandTypes.Add(key, ResolveUserDefinedOperandTypes(methodInfo));
						}
						userDefinedOperators.Add(key, methodInfo?.Prewarm());
					}
					else
					{
						if (method != null)
						{
							userDefinedOperandTypes.Add(key, ResolveUserDefinedOperandTypes(method));
						}
						userDefinedOperators.Add(key, method?.Prewarm());
					}
				}
				if (userDefinedOperators[key] != null)
				{
					leftOperand = global::Unity.VisualScripting.ConversionUtility.Convert(leftOperand, userDefinedOperandTypes[key].leftType);
					rightOperand = global::Unity.VisualScripting.ConversionUtility.Convert(rightOperand, userDefinedOperandTypes[key].rightType);
					return userDefinedOperators[key].Invoke(null, leftOperand, rightOperand);
				}
			}
			return CustomHandling(leftOperand, rightOperand);
		}

		protected virtual object CustomHandling(object leftOperand, object rightOperand)
		{
			throw new global::Unity.VisualScripting.InvalidOperatorException(base.symbol, leftOperand?.GetType(), rightOperand?.GetType());
		}

		protected virtual object BothNullHandling()
		{
			throw new global::Unity.VisualScripting.InvalidOperatorException(base.symbol, null, null);
		}

		protected virtual object SingleNullHandling()
		{
			throw new global::Unity.VisualScripting.InvalidOperatorException(base.symbol, null, null);
		}

		protected void Handle<TLeft, TRight>(global::System.Func<TLeft, TRight, object> handler, bool reverse = false)
		{
			global::Unity.VisualScripting.BinaryOperatorHandler.OperatorQuery key = new global::Unity.VisualScripting.BinaryOperatorHandler.OperatorQuery(typeof(TLeft), typeof(TRight));
			if (handlers.ContainsKey(key))
			{
				throw new global::System.ArgumentException($"A handler is already registered for '{typeof(TLeft)} {base.symbol} {typeof(TRight)}'.");
			}
			handlers.Add(key, (object left, object right) => handler((TLeft)left, (TRight)right));
			if (!reverse || !(typeof(TLeft) != typeof(TRight)))
			{
				return;
			}
			global::Unity.VisualScripting.BinaryOperatorHandler.OperatorQuery key2 = new global::Unity.VisualScripting.BinaryOperatorHandler.OperatorQuery(typeof(TRight), typeof(TLeft));
			if (!handlers.ContainsKey(key2))
			{
				handlers.Add(key2, (object left, object right) => handler((TLeft)left, (TRight)right));
			}
		}

		private static global::Unity.VisualScripting.BinaryOperatorHandler.OperatorQuery ResolveUserDefinedOperandTypes(global::System.Reflection.MethodInfo userDefinedOperator)
		{
			global::System.Reflection.ParameterInfo[] parameters = userDefinedOperator.GetParameters();
			return new global::Unity.VisualScripting.BinaryOperatorHandler.OperatorQuery(parameters[0].ParameterType, parameters[1].ParameterType);
		}
	}
}
