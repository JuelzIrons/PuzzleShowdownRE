namespace Newtonsoft.Json.Utilities
{
	internal sealed class DynamicProxyMetaObject<T> : global::System.Dynamic.DynamicMetaObject
	{
		private delegate global::System.Dynamic.DynamicMetaObject Fallback(global::System.Dynamic.DynamicMetaObject? errorSuggestion);

		private sealed class GetBinderAdapter : global::System.Dynamic.GetMemberBinder
		{
			internal GetBinderAdapter(global::System.Dynamic.InvokeMemberBinder binder)
				: base(binder.Name, binder.IgnoreCase)
			{
			}

			public override global::System.Dynamic.DynamicMetaObject FallbackGetMember(global::System.Dynamic.DynamicMetaObject target, global::System.Dynamic.DynamicMetaObject? errorSuggestion)
			{
				throw new global::System.NotSupportedException();
			}
		}

		private readonly global::Newtonsoft.Json.Utilities.DynamicProxy<T> _proxy;

		private static global::System.Linq.Expressions.Expression[] NoArgs => global::Newtonsoft.Json.Utilities.CollectionUtils.ArrayEmpty<global::System.Linq.Expressions.Expression>();

		internal DynamicProxyMetaObject(global::System.Linq.Expressions.Expression expression, T value, global::Newtonsoft.Json.Utilities.DynamicProxy<T> proxy)
			: base(expression, global::System.Dynamic.BindingRestrictions.Empty, value)
		{
			_proxy = proxy;
		}

		private bool IsOverridden(string method)
		{
			return global::Newtonsoft.Json.Utilities.ReflectionUtils.IsMethodOverridden(_proxy.GetType(), typeof(global::Newtonsoft.Json.Utilities.DynamicProxy<T>), method);
		}

		public override global::System.Dynamic.DynamicMetaObject BindGetMember(global::System.Dynamic.GetMemberBinder binder)
		{
			if (!IsOverridden("TryGetMember"))
			{
				return base.BindGetMember(binder);
			}
			return CallMethodWithResult("TryGetMember", binder, NoArgs, (global::System.Dynamic.DynamicMetaObject? e) => binder.FallbackGetMember(this, e));
		}

		public override global::System.Dynamic.DynamicMetaObject BindSetMember(global::System.Dynamic.SetMemberBinder binder, global::System.Dynamic.DynamicMetaObject value)
		{
			if (!IsOverridden("TrySetMember"))
			{
				return base.BindSetMember(binder, value);
			}
			return CallMethodReturnLast("TrySetMember", binder, GetArgs(value), (global::System.Dynamic.DynamicMetaObject? e) => binder.FallbackSetMember(this, value, e));
		}

		public override global::System.Dynamic.DynamicMetaObject BindDeleteMember(global::System.Dynamic.DeleteMemberBinder binder)
		{
			if (!IsOverridden("TryDeleteMember"))
			{
				return base.BindDeleteMember(binder);
			}
			return CallMethodNoResult("TryDeleteMember", binder, NoArgs, (global::System.Dynamic.DynamicMetaObject? e) => binder.FallbackDeleteMember(this, e));
		}

		public override global::System.Dynamic.DynamicMetaObject BindConvert(global::System.Dynamic.ConvertBinder binder)
		{
			if (!IsOverridden("TryConvert"))
			{
				return base.BindConvert(binder);
			}
			return CallMethodWithResult("TryConvert", binder, NoArgs, (global::System.Dynamic.DynamicMetaObject? e) => binder.FallbackConvert(this, e));
		}

		public override global::System.Dynamic.DynamicMetaObject BindInvokeMember(global::System.Dynamic.InvokeMemberBinder binder, global::System.Dynamic.DynamicMetaObject[] args)
		{
			if (!IsOverridden("TryInvokeMember"))
			{
				return base.BindInvokeMember(binder, args);
			}
			global::Newtonsoft.Json.Utilities.DynamicProxyMetaObject<T>.Fallback fallback = (global::System.Dynamic.DynamicMetaObject? e) => binder.FallbackInvokeMember(this, args, e);
			return BuildCallMethodWithResult("TryInvokeMember", binder, GetArgArray(args), BuildCallMethodWithResult("TryGetMember", new global::Newtonsoft.Json.Utilities.DynamicProxyMetaObject<T>.GetBinderAdapter(binder), NoArgs, fallback(null), (global::System.Dynamic.DynamicMetaObject? e) => binder.FallbackInvoke(e, args, null)), null);
		}

		public override global::System.Dynamic.DynamicMetaObject BindCreateInstance(global::System.Dynamic.CreateInstanceBinder binder, global::System.Dynamic.DynamicMetaObject[] args)
		{
			if (!IsOverridden("TryCreateInstance"))
			{
				return base.BindCreateInstance(binder, args);
			}
			return CallMethodWithResult("TryCreateInstance", binder, GetArgArray(args), (global::System.Dynamic.DynamicMetaObject? e) => binder.FallbackCreateInstance(this, args, e));
		}

		public override global::System.Dynamic.DynamicMetaObject BindInvoke(global::System.Dynamic.InvokeBinder binder, global::System.Dynamic.DynamicMetaObject[] args)
		{
			if (!IsOverridden("TryInvoke"))
			{
				return base.BindInvoke(binder, args);
			}
			return CallMethodWithResult("TryInvoke", binder, GetArgArray(args), (global::System.Dynamic.DynamicMetaObject? e) => binder.FallbackInvoke(this, args, e));
		}

		public override global::System.Dynamic.DynamicMetaObject BindBinaryOperation(global::System.Dynamic.BinaryOperationBinder binder, global::System.Dynamic.DynamicMetaObject arg)
		{
			if (!IsOverridden("TryBinaryOperation"))
			{
				return base.BindBinaryOperation(binder, arg);
			}
			return CallMethodWithResult("TryBinaryOperation", binder, GetArgs(arg), (global::System.Dynamic.DynamicMetaObject? e) => binder.FallbackBinaryOperation(this, arg, e));
		}

		public override global::System.Dynamic.DynamicMetaObject BindUnaryOperation(global::System.Dynamic.UnaryOperationBinder binder)
		{
			if (!IsOverridden("TryUnaryOperation"))
			{
				return base.BindUnaryOperation(binder);
			}
			return CallMethodWithResult("TryUnaryOperation", binder, NoArgs, (global::System.Dynamic.DynamicMetaObject? e) => binder.FallbackUnaryOperation(this, e));
		}

		public override global::System.Dynamic.DynamicMetaObject BindGetIndex(global::System.Dynamic.GetIndexBinder binder, global::System.Dynamic.DynamicMetaObject[] indexes)
		{
			if (!IsOverridden("TryGetIndex"))
			{
				return base.BindGetIndex(binder, indexes);
			}
			return CallMethodWithResult("TryGetIndex", binder, GetArgArray(indexes), (global::System.Dynamic.DynamicMetaObject? e) => binder.FallbackGetIndex(this, indexes, e));
		}

		public override global::System.Dynamic.DynamicMetaObject BindSetIndex(global::System.Dynamic.SetIndexBinder binder, global::System.Dynamic.DynamicMetaObject[] indexes, global::System.Dynamic.DynamicMetaObject value)
		{
			if (!IsOverridden("TrySetIndex"))
			{
				return base.BindSetIndex(binder, indexes, value);
			}
			return CallMethodReturnLast("TrySetIndex", binder, GetArgArray(indexes, value), (global::System.Dynamic.DynamicMetaObject? e) => binder.FallbackSetIndex(this, indexes, value, e));
		}

		public override global::System.Dynamic.DynamicMetaObject BindDeleteIndex(global::System.Dynamic.DeleteIndexBinder binder, global::System.Dynamic.DynamicMetaObject[] indexes)
		{
			if (!IsOverridden("TryDeleteIndex"))
			{
				return base.BindDeleteIndex(binder, indexes);
			}
			return CallMethodNoResult("TryDeleteIndex", binder, GetArgArray(indexes), (global::System.Dynamic.DynamicMetaObject? e) => binder.FallbackDeleteIndex(this, indexes, e));
		}

		private static global::System.Collections.Generic.IEnumerable<global::System.Linq.Expressions.Expression> GetArgs(params global::System.Dynamic.DynamicMetaObject[] args)
		{
			return global::System.Linq.Enumerable.Select(args, delegate(global::System.Dynamic.DynamicMetaObject arg)
			{
				global::System.Linq.Expressions.Expression expression = arg.Expression;
				return (!expression.Type.IsValueType()) ? expression : global::System.Linq.Expressions.Expression.Convert(expression, typeof(object));
			});
		}

		private static global::System.Linq.Expressions.Expression[] GetArgArray(global::System.Dynamic.DynamicMetaObject[] args)
		{
			return new global::System.Linq.Expressions.NewArrayExpression[1] { global::System.Linq.Expressions.Expression.NewArrayInit(typeof(object), GetArgs(args)) };
		}

		private static global::System.Linq.Expressions.Expression[] GetArgArray(global::System.Dynamic.DynamicMetaObject[] args, global::System.Dynamic.DynamicMetaObject value)
		{
			global::System.Linq.Expressions.Expression expression = value.Expression;
			return new global::System.Linq.Expressions.Expression[2]
			{
				global::System.Linq.Expressions.Expression.NewArrayInit(typeof(object), GetArgs(args)),
				expression.Type.IsValueType() ? global::System.Linq.Expressions.Expression.Convert(expression, typeof(object)) : expression
			};
		}

		private static global::System.Linq.Expressions.ConstantExpression Constant(global::System.Dynamic.DynamicMetaObjectBinder binder)
		{
			global::System.Type type = binder.GetType();
			while (!type.IsVisible())
			{
				type = type.BaseType();
			}
			return global::System.Linq.Expressions.Expression.Constant(binder, type);
		}

		private global::System.Dynamic.DynamicMetaObject CallMethodWithResult(string methodName, global::System.Dynamic.DynamicMetaObjectBinder binder, global::System.Collections.Generic.IEnumerable<global::System.Linq.Expressions.Expression> args, global::Newtonsoft.Json.Utilities.DynamicProxyMetaObject<T>.Fallback fallback, global::Newtonsoft.Json.Utilities.DynamicProxyMetaObject<T>.Fallback? fallbackInvoke = null)
		{
			global::System.Dynamic.DynamicMetaObject fallbackResult = fallback(null);
			return BuildCallMethodWithResult(methodName, binder, args, fallbackResult, fallbackInvoke);
		}

		private global::System.Dynamic.DynamicMetaObject BuildCallMethodWithResult(string methodName, global::System.Dynamic.DynamicMetaObjectBinder binder, global::System.Collections.Generic.IEnumerable<global::System.Linq.Expressions.Expression> args, global::System.Dynamic.DynamicMetaObject fallbackResult, global::Newtonsoft.Json.Utilities.DynamicProxyMetaObject<T>.Fallback? fallbackInvoke)
		{
			global::System.Linq.Expressions.ParameterExpression parameterExpression = global::System.Linq.Expressions.Expression.Parameter(typeof(object), null);
			global::System.Collections.Generic.IList<global::System.Linq.Expressions.Expression> list = new global::System.Collections.Generic.List<global::System.Linq.Expressions.Expression>();
			list.Add(global::System.Linq.Expressions.Expression.Convert(base.Expression, typeof(T)));
			list.Add(Constant(binder));
			list.AddRange(args);
			list.Add(parameterExpression);
			global::System.Dynamic.DynamicMetaObject dynamicMetaObject = new global::System.Dynamic.DynamicMetaObject(parameterExpression, global::System.Dynamic.BindingRestrictions.Empty);
			if (binder.ReturnType != typeof(object))
			{
				dynamicMetaObject = new global::System.Dynamic.DynamicMetaObject(global::System.Linq.Expressions.Expression.Convert(dynamicMetaObject.Expression, binder.ReturnType), dynamicMetaObject.Restrictions);
			}
			if (fallbackInvoke != null)
			{
				dynamicMetaObject = fallbackInvoke(dynamicMetaObject);
			}
			return new global::System.Dynamic.DynamicMetaObject(global::System.Linq.Expressions.Expression.Block(new global::System.Linq.Expressions.ParameterExpression[1] { parameterExpression }, global::System.Linq.Expressions.Expression.Condition(global::System.Linq.Expressions.Expression.Call(global::System.Linq.Expressions.Expression.Constant(_proxy), typeof(global::Newtonsoft.Json.Utilities.DynamicProxy<T>).GetMethod(methodName), list), dynamicMetaObject.Expression, fallbackResult.Expression, binder.ReturnType)), GetRestrictions().Merge(dynamicMetaObject.Restrictions).Merge(fallbackResult.Restrictions));
		}

		private global::System.Dynamic.DynamicMetaObject CallMethodReturnLast(string methodName, global::System.Dynamic.DynamicMetaObjectBinder binder, global::System.Collections.Generic.IEnumerable<global::System.Linq.Expressions.Expression> args, global::Newtonsoft.Json.Utilities.DynamicProxyMetaObject<T>.Fallback fallback)
		{
			global::System.Dynamic.DynamicMetaObject dynamicMetaObject = fallback(null);
			global::System.Linq.Expressions.ParameterExpression parameterExpression = global::System.Linq.Expressions.Expression.Parameter(typeof(object), null);
			global::System.Collections.Generic.IList<global::System.Linq.Expressions.Expression> list = new global::System.Collections.Generic.List<global::System.Linq.Expressions.Expression>();
			list.Add(global::System.Linq.Expressions.Expression.Convert(base.Expression, typeof(T)));
			list.Add(Constant(binder));
			list.AddRange(args);
			list[list.Count - 1] = global::System.Linq.Expressions.Expression.Assign(parameterExpression, list[list.Count - 1]);
			return new global::System.Dynamic.DynamicMetaObject(global::System.Linq.Expressions.Expression.Block(new global::System.Linq.Expressions.ParameterExpression[1] { parameterExpression }, global::System.Linq.Expressions.Expression.Condition(global::System.Linq.Expressions.Expression.Call(global::System.Linq.Expressions.Expression.Constant(_proxy), typeof(global::Newtonsoft.Json.Utilities.DynamicProxy<T>).GetMethod(methodName), list), parameterExpression, dynamicMetaObject.Expression, typeof(object))), GetRestrictions().Merge(dynamicMetaObject.Restrictions));
		}

		private global::System.Dynamic.DynamicMetaObject CallMethodNoResult(string methodName, global::System.Dynamic.DynamicMetaObjectBinder binder, global::System.Linq.Expressions.Expression[] args, global::Newtonsoft.Json.Utilities.DynamicProxyMetaObject<T>.Fallback fallback)
		{
			global::System.Dynamic.DynamicMetaObject dynamicMetaObject = fallback(null);
			global::System.Collections.Generic.IList<global::System.Linq.Expressions.Expression> list = new global::System.Collections.Generic.List<global::System.Linq.Expressions.Expression>();
			list.Add(global::System.Linq.Expressions.Expression.Convert(base.Expression, typeof(T)));
			list.Add(Constant(binder));
			list.AddRange(args);
			return new global::System.Dynamic.DynamicMetaObject(global::System.Linq.Expressions.Expression.Condition(global::System.Linq.Expressions.Expression.Call(global::System.Linq.Expressions.Expression.Constant(_proxy), typeof(global::Newtonsoft.Json.Utilities.DynamicProxy<T>).GetMethod(methodName), list), global::System.Linq.Expressions.Expression.Empty(), dynamicMetaObject.Expression, typeof(void)), GetRestrictions().Merge(dynamicMetaObject.Restrictions));
		}

		private global::System.Dynamic.BindingRestrictions GetRestrictions()
		{
			if (base.Value != null || !base.HasValue)
			{
				return global::System.Dynamic.BindingRestrictions.GetTypeRestriction(base.Expression, base.LimitType);
			}
			return global::System.Dynamic.BindingRestrictions.GetInstanceRestriction(base.Expression, null);
		}

		public override global::System.Collections.Generic.IEnumerable<string> GetDynamicMemberNames()
		{
			return _proxy.GetDynamicMemberNames((T)base.Value);
		}
	}
}
