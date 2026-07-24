namespace Unity.VisualScripting
{
	public abstract class InstanceInvokerBase<TTarget> : global::Unity.VisualScripting.InvokerBase
	{
		protected InstanceInvokerBase(global::System.Reflection.MethodInfo methodInfo)
			: base(methodInfo)
		{
			if (global::Unity.VisualScripting.OptimizedReflection.safeMode)
			{
				if (methodInfo.DeclaringType != typeof(TTarget))
				{
					throw new global::System.ArgumentException("Declaring type of method info doesn't match generic type.", "methodInfo");
				}
				if (methodInfo.IsStatic)
				{
					throw new global::System.ArgumentException("The method is static.", "methodInfo");
				}
			}
		}

		protected sealed override void CompileExpression()
		{
			global::System.Linq.Expressions.ParameterExpression parameterExpression = global::System.Linq.Expressions.Expression.Parameter(typeof(TTarget), "target");
			global::System.Linq.Expressions.ParameterExpression[] parameterExpressions = GetParameterExpressions();
			global::System.Linq.Expressions.ParameterExpression[] array = new global::System.Linq.Expressions.ParameterExpression[1 + parameterExpressions.Length];
			array[0] = parameterExpression;
			global::System.Array.Copy(parameterExpressions, 0, array, 1, parameterExpressions.Length);
			global::System.Reflection.MethodInfo method = methodInfo;
			global::System.Linq.Expressions.Expression[] arguments = parameterExpressions;
			global::System.Linq.Expressions.MethodCallExpression callExpression = global::System.Linq.Expressions.Expression.Call(parameterExpression, method, arguments);
			CompileExpression(callExpression, array);
		}

		protected abstract void CompileExpression(global::System.Linq.Expressions.MethodCallExpression callExpression, global::System.Linq.Expressions.ParameterExpression[] parameterExpressions);

		protected override void VerifyTarget(object target)
		{
			global::Unity.VisualScripting.OptimizedReflection.VerifyInstanceTarget<TTarget>(target);
		}
	}
}
