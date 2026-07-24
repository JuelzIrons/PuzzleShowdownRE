namespace Unity.VisualScripting
{
	public class StaticFieldAccessor<TField> : global::Unity.VisualScripting.IOptimizedAccessor
	{
		private readonly global::System.Reflection.FieldInfo fieldInfo;

		private global::System.Func<TField> getter;

		private global::System.Action<TField> setter;

		private global::System.Type targetType;

		public StaticFieldAccessor(global::System.Reflection.FieldInfo fieldInfo)
		{
			if (global::Unity.VisualScripting.OptimizedReflection.safeMode)
			{
				if (fieldInfo == null)
				{
					throw new global::System.ArgumentNullException("fieldInfo");
				}
				if (fieldInfo.FieldType != typeof(TField))
				{
					throw new global::System.ArgumentException("Field type of field info doesn't match generic type.", "fieldInfo");
				}
				if (!fieldInfo.IsStatic)
				{
					throw new global::System.ArgumentException("The field isn't static.", "fieldInfo");
				}
			}
			this.fieldInfo = fieldInfo;
			targetType = fieldInfo.DeclaringType;
		}

		public void Compile()
		{
			if (fieldInfo.IsLiteral)
			{
				TField constant = (TField)fieldInfo.GetValue(null);
				getter = () => constant;
				return;
			}
			if (global::Unity.VisualScripting.OptimizedReflection.useJit)
			{
				global::System.Linq.Expressions.MemberExpression memberExpression = global::System.Linq.Expressions.Expression.Field(null, fieldInfo);
				getter = global::System.Linq.Expressions.Expression.Lambda<global::System.Func<TField>>(memberExpression, global::System.Array.Empty<global::System.Linq.Expressions.ParameterExpression>()).Compile();
				if (fieldInfo.CanWrite())
				{
					global::System.Linq.Expressions.ParameterExpression parameterExpression = global::System.Linq.Expressions.Expression.Parameter(typeof(TField));
					global::System.Linq.Expressions.BinaryExpression body = global::System.Linq.Expressions.Expression.Assign(memberExpression, parameterExpression);
					setter = global::System.Linq.Expressions.Expression.Lambda<global::System.Action<TField>>(body, new global::System.Linq.Expressions.ParameterExpression[1] { parameterExpression }).Compile();
				}
				return;
			}
			getter = () => (TField)fieldInfo.GetValue(null);
			if (fieldInfo.CanWrite())
			{
				setter = delegate(TField value)
				{
					fieldInfo.SetValue(null, value);
				};
			}
		}

		public object GetValue(object target)
		{
			if (global::Unity.VisualScripting.OptimizedReflection.safeMode)
			{
				global::Unity.VisualScripting.OptimizedReflection.VerifyStaticTarget(targetType, target);
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
					throw new global::System.Reflection.TargetException($"The field '{targetType}.{fieldInfo.Name}' cannot be assigned.");
				}
				if (!typeof(TField).IsAssignableFrom(value))
				{
					throw new global::System.ArgumentException(string.Format("The provided value for '{0}.{1}' does not match the field type.\nProvided: {2}\nExpected: {3}", targetType, fieldInfo.Name, value?.GetType()?.ToString() ?? "null", typeof(TField)));
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
			setter((TField)value);
		}
	}
}
