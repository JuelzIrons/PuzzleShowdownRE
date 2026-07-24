namespace Unity.VisualScripting
{
	public sealed class StaticActionInvoker : global::Unity.VisualScripting.StaticActionInvokerBase
	{
		private global::System.Action invoke;

		public StaticActionInvoker(global::System.Reflection.MethodInfo methodInfo)
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
			invoke();
			return null;
		}

		protected override global::System.Type[] GetParameterTypes()
		{
			return global::System.Type.EmptyTypes;
		}

		protected override void CompileExpression(global::System.Linq.Expressions.MethodCallExpression callExpression, global::System.Linq.Expressions.ParameterExpression[] parameterExpressions)
		{
			invoke = global::System.Linq.Expressions.Expression.Lambda<global::System.Action>(callExpression, parameterExpressions).Compile();
		}

		protected override void CreateDelegate()
		{
			invoke = delegate
			{
				((global::System.Action)methodInfo.CreateDelegate(typeof(global::System.Action)))();
			};
		}
	}
	public sealed class StaticActionInvoker<TParam0> : global::Unity.VisualScripting.StaticActionInvokerBase
	{
		private global::System.Action<TParam0> invoke;

		public StaticActionInvoker(global::System.Reflection.MethodInfo methodInfo)
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
			invoke((TParam0)arg0);
			return null;
		}

		protected override global::System.Type[] GetParameterTypes()
		{
			return new global::System.Type[1] { typeof(TParam0) };
		}

		protected override void CompileExpression(global::System.Linq.Expressions.MethodCallExpression callExpression, global::System.Linq.Expressions.ParameterExpression[] parameterExpressions)
		{
			invoke = global::System.Linq.Expressions.Expression.Lambda<global::System.Action<TParam0>>(callExpression, parameterExpressions).Compile();
		}

		protected override void CreateDelegate()
		{
			invoke = delegate(TParam0 param0)
			{
				((global::System.Action<TParam0>)methodInfo.CreateDelegate(typeof(global::System.Action<TParam0>)))(param0);
			};
		}
	}
	public sealed class StaticActionInvoker<TParam0, TParam1> : global::Unity.VisualScripting.StaticActionInvokerBase
	{
		private global::System.Action<TParam0, TParam1> invoke;

		public StaticActionInvoker(global::System.Reflection.MethodInfo methodInfo)
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
			invoke((TParam0)arg0, (TParam1)arg1);
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
			invoke = global::System.Linq.Expressions.Expression.Lambda<global::System.Action<TParam0, TParam1>>(callExpression, parameterExpressions).Compile();
		}

		protected override void CreateDelegate()
		{
			invoke = delegate(TParam0 param0, TParam1 param1)
			{
				((global::System.Action<TParam0, TParam1>)methodInfo.CreateDelegate(typeof(global::System.Action<TParam0, TParam1>)))(param0, param1);
			};
		}
	}
	public sealed class StaticActionInvoker<TParam0, TParam1, TParam2> : global::Unity.VisualScripting.StaticActionInvokerBase
	{
		private global::System.Action<TParam0, TParam1, TParam2> invoke;

		public StaticActionInvoker(global::System.Reflection.MethodInfo methodInfo)
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
			invoke((TParam0)arg0, (TParam1)arg1, (TParam2)arg2);
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
			invoke = global::System.Linq.Expressions.Expression.Lambda<global::System.Action<TParam0, TParam1, TParam2>>(callExpression, parameterExpressions).Compile();
		}

		protected override void CreateDelegate()
		{
			invoke = delegate(TParam0 param0, TParam1 param1, TParam2 param2)
			{
				((global::System.Action<TParam0, TParam1, TParam2>)methodInfo.CreateDelegate(typeof(global::System.Action<TParam0, TParam1, TParam2>)))(param0, param1, param2);
			};
		}
	}
	public sealed class StaticActionInvoker<TParam0, TParam1, TParam2, TParam3> : global::Unity.VisualScripting.StaticActionInvokerBase
	{
		private global::System.Action<TParam0, TParam1, TParam2, TParam3> invoke;

		public StaticActionInvoker(global::System.Reflection.MethodInfo methodInfo)
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
			invoke((TParam0)arg0, (TParam1)arg1, (TParam2)arg2, (TParam3)arg3);
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
			invoke = global::System.Linq.Expressions.Expression.Lambda<global::System.Action<TParam0, TParam1, TParam2, TParam3>>(callExpression, parameterExpressions).Compile();
		}

		protected override void CreateDelegate()
		{
			invoke = delegate(TParam0 param0, TParam1 param1, TParam2 param2, TParam3 param3)
			{
				((global::System.Action<TParam0, TParam1, TParam2, TParam3>)methodInfo.CreateDelegate(typeof(global::System.Action<TParam0, TParam1, TParam2, TParam3>)))(param0, param1, param2, param3);
			};
		}
	}
	public sealed class StaticActionInvoker<TParam0, TParam1, TParam2, TParam3, TParam4> : global::Unity.VisualScripting.StaticActionInvokerBase
	{
		private global::Unity.VisualScripting.Action<TParam0, TParam1, TParam2, TParam3, TParam4> invoke;

		public StaticActionInvoker(global::System.Reflection.MethodInfo methodInfo)
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
			invoke((TParam0)arg0, (TParam1)arg1, (TParam2)arg2, (TParam3)arg3, (TParam4)arg4);
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
			invoke = global::System.Linq.Expressions.Expression.Lambda<global::Unity.VisualScripting.Action<TParam0, TParam1, TParam2, TParam3, TParam4>>(callExpression, parameterExpressions).Compile();
		}

		protected override void CreateDelegate()
		{
			invoke = delegate(TParam0 param0, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4)
			{
				((global::Unity.VisualScripting.Action<TParam0, TParam1, TParam2, TParam3, TParam4>)methodInfo.CreateDelegate(typeof(global::Unity.VisualScripting.Action<TParam0, TParam1, TParam2, TParam3, TParam4>)))(param0, param1, param2, param3, param4);
			};
		}
	}
}
