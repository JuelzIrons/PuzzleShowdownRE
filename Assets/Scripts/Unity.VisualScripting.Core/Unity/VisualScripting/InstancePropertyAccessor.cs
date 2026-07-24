namespace Unity.VisualScripting
{
	public class InstancePropertyAccessor<TTarget, TProperty> : global::Unity.VisualScripting.IOptimizedAccessor
	{
		private readonly global::System.Reflection.PropertyInfo propertyInfo;

		private global::System.Func<TTarget, TProperty> getter;

		private global::System.Action<TTarget, TProperty> setter;

		public InstancePropertyAccessor(global::System.Reflection.PropertyInfo propertyInfo)
		{
			if (global::Unity.VisualScripting.OptimizedReflection.safeMode)
			{
				global::Unity.VisualScripting.Ensure.That("propertyInfo").IsNotNull(propertyInfo);
				if (propertyInfo.DeclaringType != typeof(TTarget))
				{
					throw new global::System.ArgumentException("The declaring type of the property info doesn't match the generic type.", "propertyInfo");
				}
				if (propertyInfo.PropertyType != typeof(TProperty))
				{
					throw new global::System.ArgumentException("The property type of the property info doesn't match the generic type.", "propertyInfo");
				}
				if (propertyInfo.IsStatic())
				{
					throw new global::System.ArgumentException("The property is static.", "propertyInfo");
				}
			}
			this.propertyInfo = propertyInfo;
		}

		public void Compile()
		{
			global::System.Reflection.MethodInfo getMethod = propertyInfo.GetGetMethod(nonPublic: true);
			global::System.Reflection.MethodInfo setMethod = propertyInfo.GetSetMethod(nonPublic: true);
			if (global::Unity.VisualScripting.OptimizedReflection.useJit)
			{
				global::System.Linq.Expressions.ParameterExpression parameterExpression = global::System.Linq.Expressions.Expression.Parameter(typeof(TTarget), "target");
				if (getMethod != null)
				{
					global::System.Linq.Expressions.MemberExpression body = global::System.Linq.Expressions.Expression.Property(parameterExpression, propertyInfo);
					getter = global::System.Linq.Expressions.Expression.Lambda<global::System.Func<TTarget, TProperty>>(body, new global::System.Linq.Expressions.ParameterExpression[1] { parameterExpression }).Compile();
				}
				if (setMethod != null)
				{
					setter = (global::System.Action<TTarget, TProperty>)setMethod.CreateDelegate(typeof(global::System.Action<TTarget, TProperty>));
				}
			}
			else
			{
				if (getMethod != null)
				{
					getter = (global::System.Func<TTarget, TProperty>)getMethod.CreateDelegate(typeof(global::System.Func<TTarget, TProperty>));
				}
				if (setMethod != null)
				{
					setter = (global::System.Action<TTarget, TProperty>)setMethod.CreateDelegate(typeof(global::System.Action<TTarget, TProperty>));
				}
			}
		}

		public object GetValue(object target)
		{
			if (global::Unity.VisualScripting.OptimizedReflection.safeMode)
			{
				global::Unity.VisualScripting.OptimizedReflection.VerifyInstanceTarget<TTarget>(target);
				if (getter == null)
				{
					throw new global::System.Reflection.TargetException($"The property '{typeof(TTarget)}.{propertyInfo.Name}' has no get accessor.");
				}
				try
				{
					return GetValueUnsafe(target);
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
			return GetValueUnsafe(target);
		}

		private object GetValueUnsafe(object target)
		{
			return getter((TTarget)target);
		}

		public void SetValue(object target, object value)
		{
			if (global::Unity.VisualScripting.OptimizedReflection.safeMode)
			{
				global::Unity.VisualScripting.OptimizedReflection.VerifyInstanceTarget<TTarget>(target);
				if (setter == null)
				{
					throw new global::System.Reflection.TargetException($"The property '{typeof(TTarget)}.{propertyInfo.Name}' has no set accessor.");
				}
				if (!typeof(TProperty).IsAssignableFrom(value))
				{
					throw new global::System.ArgumentException(string.Format("The provided value for '{0}.{1}' does not match the property type.\nProvided: {2}\nExpected: {3}", typeof(TTarget), propertyInfo.Name, value?.GetType()?.ToString() ?? "null", typeof(TProperty)));
				}
				try
				{
					SetValueUnsafe(target, value);
					return;
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
			SetValueUnsafe(target, value);
		}

		private void SetValueUnsafe(object target, object value)
		{
			setter((TTarget)target, (TProperty)value);
		}
	}
}
