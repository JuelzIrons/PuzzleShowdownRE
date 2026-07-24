namespace Unity.VisualScripting
{
	public abstract class InvokerBase : global::Unity.VisualScripting.IOptimizedInvoker
	{
		protected readonly global::System.Type targetType;

		protected readonly global::System.Reflection.MethodInfo methodInfo;

		protected InvokerBase(global::System.Reflection.MethodInfo methodInfo)
		{
			if (global::Unity.VisualScripting.OptimizedReflection.safeMode && methodInfo == null)
			{
				throw new global::System.ArgumentNullException("methodInfo");
			}
			this.methodInfo = methodInfo;
			targetType = methodInfo.DeclaringType;
		}

		protected void VerifyArgument<TParam>(global::System.Reflection.MethodInfo methodInfo, int argIndex, object arg)
		{
			if (!typeof(TParam).IsAssignableFrom(arg))
			{
				throw new global::System.ArgumentException(string.Format("The provided argument value for '{0}.{1}' does not match the parameter type.\nProvided: {2}\nExpected: {3}", targetType, methodInfo.Name, arg?.GetType().ToString() ?? "null", typeof(TParam)), methodInfo.GetParameters()[argIndex].Name);
			}
		}

		public void Compile()
		{
			if (global::Unity.VisualScripting.OptimizedReflection.useJit)
			{
				CompileExpression();
			}
			else
			{
				CreateDelegate();
			}
		}

		protected global::System.Linq.Expressions.ParameterExpression[] GetParameterExpressions()
		{
			global::System.Reflection.ParameterInfo[] parameters = methodInfo.GetParameters();
			global::System.Type[] parameterTypes = GetParameterTypes();
			if (parameters.Length != parameterTypes.Length)
			{
				throw new global::System.ArgumentException("Parameter count of method info doesn't match generic argument count.", "methodInfo");
			}
			for (int i = 0; i < parameterTypes.Length; i++)
			{
				if (parameterTypes[i] != parameters[i].ParameterType)
				{
					throw new global::System.ArgumentException("Parameter type of method info doesn't match generic argument.", "methodInfo");
				}
			}
			global::System.Linq.Expressions.ParameterExpression[] array = new global::System.Linq.Expressions.ParameterExpression[parameterTypes.Length];
			for (int j = 0; j < parameterTypes.Length; j++)
			{
				array[j] = global::System.Linq.Expressions.Expression.Parameter(parameterTypes[j], "parameter" + j);
			}
			return array;
		}

		protected abstract global::System.Type[] GetParameterTypes();

		public abstract object Invoke(object target, params object[] args);

		public virtual object Invoke(object target)
		{
			throw new global::System.Reflection.TargetParameterCountException();
		}

		public virtual object Invoke(object target, object arg0)
		{
			throw new global::System.Reflection.TargetParameterCountException();
		}

		public virtual object Invoke(object target, object arg0, object arg1)
		{
			throw new global::System.Reflection.TargetParameterCountException();
		}

		public virtual object Invoke(object target, object arg0, object arg1, object arg2)
		{
			throw new global::System.Reflection.TargetParameterCountException();
		}

		public virtual object Invoke(object target, object arg0, object arg1, object arg2, object arg3)
		{
			throw new global::System.Reflection.TargetParameterCountException();
		}

		public virtual object Invoke(object target, object arg0, object arg1, object arg2, object arg3, object arg4)
		{
			throw new global::System.Reflection.TargetParameterCountException();
		}

		protected abstract void CompileExpression();

		protected abstract void CreateDelegate();

		protected abstract void VerifyTarget(object target);
	}
}
