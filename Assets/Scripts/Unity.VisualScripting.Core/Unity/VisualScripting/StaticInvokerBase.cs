namespace Unity.VisualScripting
{
	public abstract class StaticInvokerBase : global::Unity.VisualScripting.InvokerBase
	{
		protected StaticInvokerBase(global::System.Reflection.MethodInfo methodInfo)
			: base(methodInfo)
		{
			if (global::Unity.VisualScripting.OptimizedReflection.safeMode && !methodInfo.IsStatic)
			{
				throw new global::System.ArgumentException("The method isn't static.", "methodInfo");
			}
		}

		protected sealed override void CompileExpression()
		{
			global::System.Linq.Expressions.ParameterExpression[] parameterExpressions = GetParameterExpressions();
			global::System.Reflection.MethodInfo method = methodInfo;
			global::System.Linq.Expressions.Expression[] arguments = parameterExpressions;
			global::System.Linq.Expressions.MethodCallExpression callExpression = global::System.Linq.Expressions.Expression.Call(method, arguments);
			CompileExpression(callExpression, parameterExpressions);
		}

		protected abstract void CompileExpression(global::System.Linq.Expressions.MethodCallExpression callExpression, global::System.Linq.Expressions.ParameterExpression[] parameterExpressions);

		protected override void VerifyTarget(object target)
		{
			global::Unity.VisualScripting.OptimizedReflection.VerifyStaticTarget(targetType, target);
		}
	}
}
