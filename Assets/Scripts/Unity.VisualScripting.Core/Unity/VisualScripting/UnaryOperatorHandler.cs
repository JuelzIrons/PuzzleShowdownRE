namespace Unity.VisualScripting
{
	public abstract class UnaryOperatorHandler : global::Unity.VisualScripting.OperatorHandler
	{
		private readonly global::System.Collections.Generic.Dictionary<global::System.Type, global::System.Func<object, object>> manualHandlers = new global::System.Collections.Generic.Dictionary<global::System.Type, global::System.Func<object, object>>();

		private readonly global::System.Collections.Generic.Dictionary<global::System.Type, global::Unity.VisualScripting.IOptimizedInvoker> userDefinedOperators = new global::System.Collections.Generic.Dictionary<global::System.Type, global::Unity.VisualScripting.IOptimizedInvoker>();

		private readonly global::System.Collections.Generic.Dictionary<global::System.Type, global::System.Type> userDefinedOperandTypes = new global::System.Collections.Generic.Dictionary<global::System.Type, global::System.Type>();

		protected UnaryOperatorHandler(string name, string verb, string symbol, string customMethodName)
			: base(name, verb, symbol, customMethodName)
		{
		}

		public object Operate(object operand)
		{
			global::Unity.VisualScripting.Ensure.That("operand").IsNotNull(operand);
			global::System.Type type = operand.GetType();
			if (manualHandlers.ContainsKey(type))
			{
				return manualHandlers[type](operand);
			}
			if (base.customMethodName != null)
			{
				if (!userDefinedOperators.ContainsKey(type))
				{
					global::System.Reflection.MethodInfo method = type.GetMethod(base.customMethodName, global::System.Reflection.BindingFlags.Static | global::System.Reflection.BindingFlags.Public);
					if (method != null)
					{
						userDefinedOperandTypes.Add(type, ResolveUserDefinedOperandType(method));
					}
					userDefinedOperators.Add(type, method?.Prewarm());
				}
				if (userDefinedOperators[type] != null)
				{
					operand = global::Unity.VisualScripting.ConversionUtility.Convert(operand, userDefinedOperandTypes[type]);
					return userDefinedOperators[type].Invoke(null, operand);
				}
			}
			return CustomHandling(operand);
		}

		protected virtual object CustomHandling(object operand)
		{
			throw new global::Unity.VisualScripting.InvalidOperatorException(base.symbol, operand.GetType());
		}

		protected void Handle<T>(global::System.Func<T, object> handler)
		{
			manualHandlers.Add(typeof(T), (object operand) => handler((T)operand));
		}

		private static global::System.Type ResolveUserDefinedOperandType(global::System.Reflection.MethodInfo userDefinedOperator)
		{
			return userDefinedOperator.GetParameters()[0].ParameterType;
		}
	}
}
