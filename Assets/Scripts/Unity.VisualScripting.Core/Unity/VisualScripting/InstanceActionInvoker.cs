namespace Unity.VisualScripting
{
	public sealed class InstanceActionInvoker<TTarget> : global::Unity.VisualScripting.InstanceActionInvokerBase<TTarget>
	{
		private global::System.Action<TTarget> invoke;

		public InstanceActionInvoker(global::System.Reflection.MethodInfo methodInfo)
			: base(methodInfo)
		{
		}

		public override object Invoke(object target, params object[] args)
		{
			if (args.Length != 0)
			{
				throw new global::System.Reflection.TargetParameterCountException();
			}
			return Invoke(target);
		}

		public override object Invoke(object target)
		{
			if (global::Unity.VisualScripting.OptimizedReflection.safeMode)
			{
				VerifyTarget(target);
				try
				{
					return InvokeUnsafe(target);
				}
				catch (global::System.Reflection.TargetInvocationException)
				{
					throw;
				}
				catch (global::System.Exception inner)
				{
					throw new global::System.Reflection.TargetInvocationException(inner);
				}
			}
			return InvokeUnsafe(target);
		}

		private object InvokeUnsafe(object target)
		{
			invoke((TTarget)target);
			return null;
		}

		protected override global::System.Type[] GetParameterTypes()
		{
			return global::System.Type.EmptyTypes;
		}

		protected override void CompileExpression(global::System.Linq.Expressions.MethodCallExpression callExpression, global::System.Linq.Expressions.ParameterExpression[] parameterExpressions)
		{
			invoke = global::System.Linq.Expressions.Expression.Lambda<global::System.Action<TTarget>>(callExpression, parameterExpressions).Compile();
		}

		protected override void CreateDelegate()
		{
			invoke = (global::System.Action<TTarget>)methodInfo.CreateDelegate(typeof(global::System.Action<TTarget>));
		}
	}
	public sealed class InstanceActionInvoker<TTarget, TParam0> : global::Unity.VisualScripting.InstanceActionInvokerBase<TTarget>
	{
		private global::System.Action<TTarget, TParam0> invoke;

		public InstanceActionInvoker(global::System.Reflection.MethodInfo methodInfo)
			: base(methodInfo)
		{
		}

		public override object Invoke(object target, params object[] args)
		{
			if (args.Length != 1)
			{
				throw new global::System.Reflection.TargetParameterCountException();
			}
			return Invoke(target, args[0]);
		}

		public override object Invoke(object target, object arg0)
		{
			if (global::Unity.VisualScripting.OptimizedReflection.safeMode)
			{
				VerifyTarget(target);
				VerifyArgument<TParam0>(methodInfo, 0, arg0);
				try
				{
					return InvokeUnsafe(target, arg0);
				}
				catch (global::System.Reflection.TargetInvocationException)
				{
					throw;
				}
				catch (global::System.Exception inner)
				{
					throw new global::System.Reflection.TargetInvocationException(inner);
				}
			}
			return InvokeUnsafe(target, arg0);
		}

		private object InvokeUnsafe(object target, object arg0)
		{
			invoke((TTarget)target, (TParam0)arg0);
			return null;
		}

		protected override global::System.Type[] GetParameterTypes()
		{
			return new global::System.Type[1] { typeof(TParam0) };
		}

		protected override void CompileExpression(global::System.Linq.Expressions.MethodCallExpression callExpression, global::System.Linq.Expressions.ParameterExpression[] parameterExpressions)
		{
			invoke = global::System.Linq.Expressions.Expression.Lambda<global::System.Action<TTarget, TParam0>>(callExpression, parameterExpressions).Compile();
		}

		protected override void CreateDelegate()
		{
			invoke = (global::System.Action<TTarget, TParam0>)methodInfo.CreateDelegate(typeof(global::System.Action<TTarget, TParam0>));
		}
	}
	public sealed class InstanceActionInvoker<TTarget, TParam0, TParam1> : global::Unity.VisualScripting.InstanceActionInvokerBase<TTarget>
	{
		private global::System.Action<TTarget, TParam0, TParam1> invoke;

		public InstanceActionInvoker(global::System.Reflection.MethodInfo methodInfo)
			: base(methodInfo)
		{
		}

		public override object Invoke(object target, params object[] args)
		{
			if (args.Length != 2)
			{
				throw new global::System.Reflection.TargetParameterCountException();
			}
			return Invoke(target, args[0], args[1]);
		}

		public override object Invoke(object target, object arg0, object arg1)
		{
			if (global::Unity.VisualScripting.OptimizedReflection.safeMode)
			{
				VerifyTarget(target);
				VerifyArgument<TParam0>(methodInfo, 0, arg0);
				VerifyArgument<TParam1>(methodInfo, 0, arg1);
				try
				{
					return InvokeUnsafe(target, arg0, arg1);
				}
				catch (global::System.Reflection.TargetInvocationException)
				{
					throw;
				}
				catch (global::System.Exception inner)
				{
					throw new global::System.Reflection.TargetInvocationException(inner);
				}
			}
			return InvokeUnsafe(target, arg0, arg1);
		}

		public object InvokeUnsafe(object target, object arg0, object arg1)
		{
			invoke((TTarget)target, (TParam0)arg0, (TParam1)arg1);
			return null;
		}

		protected override global::System.Type[] GetParameterTypes()
		{
			return new global::System.Type[2]
			{
				typeof(TParam0),
				typeof(TParam1)
			};
		}

		protected override void CompileExpression(global::System.Linq.Expressions.MethodCallExpression callExpression, global::System.Linq.Expressions.ParameterExpression[] parameterExpressions)
		{
			invoke = global::System.Linq.Expressions.Expression.Lambda<global::System.Action<TTarget, TParam0, TParam1>>(callExpression, parameterExpressions).Compile();
		}

		protected override void CreateDelegate()
		{
			invoke = (global::System.Action<TTarget, TParam0, TParam1>)methodInfo.CreateDelegate(typeof(global::System.Action<TTarget, TParam0, TParam1>));
		}
	}
	public sealed class InstanceActionInvoker<TTarget, TParam0, TParam1, TParam2> : global::Unity.VisualScripting.InstanceActionInvokerBase<TTarget>
	{
		private global::System.Action<TTarget, TParam0, TParam1, TParam2> invoke;

		public InstanceActionInvoker(global::System.Reflection.MethodInfo methodInfo)
			: base(methodInfo)
		{
		}

		public override object Invoke(object target, params object[] args)
		{
			if (args.Length != 3)
			{
				throw new global::System.Reflection.TargetParameterCountException();
			}
			return Invoke(target, args[0], args[1], args[2]);
		}

		public override object Invoke(object target, object arg0, object arg1, object arg2)
		{
			if (global::Unity.VisualScripting.OptimizedReflection.safeMode)
			{
				VerifyTarget(target);
				VerifyArgument<TParam0>(methodInfo, 0, arg0);
				VerifyArgument<TParam1>(methodInfo, 1, arg1);
				VerifyArgument<TParam2>(methodInfo, 2, arg2);
				try
				{
					return InvokeUnsafe(target, arg0, arg1, arg2);
				}
				catch (global::System.Reflection.TargetInvocationException)
				{
					throw;
				}
				catch (global::System.Exception inner)
				{
					throw new global::System.Reflection.TargetInvocationException(inner);
				}
			}
			return InvokeUnsafe(target, arg0, arg1, arg2);
		}

		public object InvokeUnsafe(object target, object arg0, object arg1, object arg2)
		{
			invoke((TTarget)target, (TParam0)arg0, (TParam1)arg1, (TParam2)arg2);
			return null;
		}

		protected override global::System.Type[] GetParameterTypes()
		{
			return new global::System.Type[3]
			{
				typeof(TParam0),
				typeof(TParam1),
				typeof(TParam2)
			};
		}

		protected override void CompileExpression(global::System.Linq.Expressions.MethodCallExpression callExpression, global::System.Linq.Expressions.ParameterExpression[] parameterExpressions)
		{
			invoke = global::System.Linq.Expressions.Expression.Lambda<global::System.Action<TTarget, TParam0, TParam1, TParam2>>(callExpression, parameterExpressions).Compile();
		}

		protected override void CreateDelegate()
		{
			invoke = (global::System.Action<TTarget, TParam0, TParam1, TParam2>)methodInfo.CreateDelegate(typeof(global::System.Action<TTarget, TParam0, TParam1, TParam2>));
		}
	}
	public sealed class InstanceActionInvoker<TTarget, TParam0, TParam1, TParam2, TParam3> : global::Unity.VisualScripting.InstanceActionInvokerBase<TTarget>
	{
		private global::Unity.VisualScripting.Action<TTarget, TParam0, TParam1, TParam2, TParam3> invoke;

		public InstanceActionInvoker(global::System.Reflection.MethodInfo methodInfo)
			: base(methodInfo)
		{
		}

		public override object Invoke(object target, params object[] args)
		{
			if (args.Length != 4)
			{
				throw new global::System.Reflection.TargetParameterCountException();
			}
			return Invoke(target, args[0], args[1], args[2], args[3]);
		}

		public override object Invoke(object target, object arg0, object arg1, object arg2, object arg3)
		{
			if (global::Unity.VisualScripting.OptimizedReflection.safeMode)
			{
				VerifyTarget(target);
				VerifyArgument<TParam0>(methodInfo, 0, arg0);
				VerifyArgument<TParam1>(methodInfo, 1, arg1);
				VerifyArgument<TParam2>(methodInfo, 2, arg2);
				VerifyArgument<TParam3>(methodInfo, 3, arg3);
				try
				{
					return InvokeUnsafe(target, arg0, arg1, arg2, arg3);
				}
				catch (global::System.Reflection.TargetInvocationException)
				{
					throw;
				}
				catch (global::System.Exception inner)
				{
					throw new global::System.Reflection.TargetInvocationException(inner);
				}
			}
			return InvokeUnsafe(target, arg0, arg1, arg2, arg3);
		}

		public object InvokeUnsafe(object target, object arg0, object arg1, object arg2, object arg3)
		{
			invoke((TTarget)target, (TParam0)arg0, (TParam1)arg1, (TParam2)arg2, (TParam3)arg3);
			return null;
		}

		protected override global::System.Type[] GetParameterTypes()
		{
			return new global::System.Type[4]
			{
				typeof(TParam0),
				typeof(TParam1),
				typeof(TParam2),
				typeof(TParam3)
			};
		}

		protected override void CompileExpression(global::System.Linq.Expressions.MethodCallExpression callExpression, global::System.Linq.Expressions.ParameterExpression[] parameterExpressions)
		{
			invoke = global::System.Linq.Expressions.Expression.Lambda<global::Unity.VisualScripting.Action<TTarget, TParam0, TParam1, TParam2, TParam3>>(callExpression, parameterExpressions).Compile();
		}

		protected override void CreateDelegate()
		{
			invoke = (global::Unity.VisualScripting.Action<TTarget, TParam0, TParam1, TParam2, TParam3>)methodInfo.CreateDelegate(typeof(global::Unity.VisualScripting.Action<TTarget, TParam0, TParam1, TParam2, TParam3>));
		}
	}
	public sealed class InstanceActionInvoker<TTarget, TParam0, TParam1, TParam2, TParam3, TParam4> : global::Unity.VisualScripting.InstanceActionInvokerBase<TTarget>
	{
		private global::Unity.VisualScripting.Action<TTarget, TParam0, TParam1, TParam2, TParam3, TParam4> invoke;

		public InstanceActionInvoker(global::System.Reflection.MethodInfo methodInfo)
			: base(methodInfo)
		{
		}

		public override object Invoke(object target, params object[] args)
		{
			if (args.Length != 5)
			{
				throw new global::System.Reflection.TargetParameterCountException();
			}
			return Invoke(target, args[0], args[1], args[2], args[3], args[4]);
		}

		public override object Invoke(object target, object arg0, object arg1, object arg2, object arg3, object arg4)
		{
			if (global::Unity.VisualScripting.OptimizedReflection.safeMode)
			{
				VerifyTarget(target);
				VerifyArgument<TParam0>(methodInfo, 0, arg0);
				VerifyArgument<TParam1>(methodInfo, 1, arg1);
				VerifyArgument<TParam2>(methodInfo, 2, arg2);
				VerifyArgument<TParam3>(methodInfo, 3, arg3);
				VerifyArgument<TParam4>(methodInfo, 4, arg4);
				try
				{
					return InvokeUnsafe(target, arg0, arg1, arg2, arg3, arg4);
				}
				catch (global::System.Reflection.TargetInvocationException)
				{
					throw;
				}
				catch (global::System.Exception inner)
				{
					throw new global::System.Reflection.TargetInvocationException(inner);
				}
			}
			return InvokeUnsafe(target, arg0, arg1, arg2, arg3, arg4);
		}

		public object InvokeUnsafe(object target, object arg0, object arg1, object arg2, object arg3, object arg4)
		{
			invoke((TTarget)target, (TParam0)arg0, (TParam1)arg1, (TParam2)arg2, (TParam3)arg3, (TParam4)arg4);
			return null;
		}

		protected override global::System.Type[] GetParameterTypes()
		{
			return new global::System.Type[5]
			{
				typeof(TParam0),
				typeof(TParam1),
				typeof(TParam2),
				typeof(TParam3),
				typeof(TParam4)
			};
		}

		protected override void CompileExpression(global::System.Linq.Expressions.MethodCallExpression callExpression, global::System.Linq.Expressions.ParameterExpression[] parameterExpressions)
		{
			invoke = global::System.Linq.Expressions.Expression.Lambda<global::Unity.VisualScripting.Action<TTarget, TParam0, TParam1, TParam2, TParam3, TParam4>>(callExpression, parameterExpressions).Compile();
		}

		protected override void CreateDelegate()
		{
			invoke = (global::Unity.VisualScripting.Action<TTarget, TParam0, TParam1, TParam2, TParam3, TParam4>)methodInfo.CreateDelegate(typeof(global::Unity.VisualScripting.Action<TTarget, TParam0, TParam1, TParam2, TParam3, TParam4>));
		}
	}
}
