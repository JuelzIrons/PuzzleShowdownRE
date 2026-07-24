namespace Unity.VisualScripting
{
	public class StaticPropertyAccessor<TProperty> : global::Unity.VisualScripting.IOptimizedAccessor
	{
		private readonly global::System.Reflection.PropertyInfo propertyInfo;

		private global::System.Func<TProperty> getter;

		private global::System.Action<TProperty> setter;

		private global::System.Type targetType;

		public StaticPropertyAccessor(global::System.Reflection.PropertyInfo propertyInfo)
		{
			if (global::Unity.VisualScripting.OptimizedReflection.safeMode)
			{
				if (propertyInfo == null)
				{
					throw new global::System.ArgumentNullException("propertyInfo");
				}
				if (propertyInfo.PropertyType != typeof(TProperty))
				{
					throw new global::System.ArgumentException("The property type of the property info doesn't match the generic type.", "propertyInfo");
				}
				if (!propertyInfo.IsStatic())
				{
					throw new global::System.ArgumentException("The property isn't static.", "propertyInfo");
				}
			}
			this.propertyInfo = propertyInfo;
			targetType = propertyInfo.DeclaringType;
		}

		public void Compile()
		{
			global::System.Reflection.MethodInfo getMethod = propertyInfo.GetGetMethod(nonPublic: true);
			global::System.Reflection.MethodInfo setMethod = propertyInfo.GetSetMethod(nonPublic: true);
			if (global::Unity.VisualScripting.OptimizedReflection.useJit)
			{
				if (getMethod != null)
				{
					global::System.Linq.Expressions.MemberExpression body = global::System.Linq.Expressions.Expression.Property(null, propertyInfo);
					getter = global::System.Linq.Expressions.Expression.Lambda<global::System.Func<TProperty>>(body, global::System.Array.Empty<global::System.Linq.Expressions.ParameterExpression>()).Compile();
				}
				if (setMethod != null)
				{
					setter = (global::System.Action<TProperty>)setMethod.CreateDelegate(typeof(global::System.Action<TProperty>));
				}
			}
			else
			{
				if (getMethod != null)
				{
					getter = (global::System.Func<TProperty>)getMethod.CreateDelegate(typeof(global::System.Func<TProperty>));
				}
				if (setMethod != null)
				{
					setter = (global::System.Action<TProperty>)setMethod.CreateDelegate(typeof(global::System.Action<TProperty>));
				}
			}
		}

		public object GetValue(object target)
		{
			if (global::Unity.VisualScripting.OptimizedReflection.safeMode)
			{
				global::Unity.VisualScripting.OptimizedReflection.VerifyStaticTarget(targetType, target);
				if (getter == null)
				{
					throw new global::System.Reflection.TargetException($"The property '{targetType}.{propertyInfo.Name}' has no get accessor.");
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
			return getter();
		}

		public void SetValue(object target, object value)
		{
			if (global::Unity.VisualScripting.OptimizedReflection.safeMode)
			{
				global::Unity.VisualScripting.OptimizedReflection.VerifyStaticTarget(targetType, target);
				if (setter == null)
				{
					throw new global::System.Reflection.TargetException($"The property '{targetType}.{propertyInfo.Name}' has no set accessor.");
				}
				if (!typeof(TProperty).IsAssignableFrom(value))
				{
					throw new global::System.ArgumentException(string.Format("The provided value for '{0}.{1}' does not match the property type.\nProvided: {2}\nExpected: {3}", targetType, propertyInfo.Name, value?.GetType()?.ToString() ?? "null", typeof(TProperty)));
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
			setter((TProperty)value);
		}
	}
}
