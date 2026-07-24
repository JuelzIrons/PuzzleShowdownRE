namespace Unity.VisualScripting
{
	public class InstanceFieldAccessor<TTarget, TField> : global::Unity.VisualScripting.IOptimizedAccessor
	{
		private readonly global::System.Reflection.FieldInfo fieldInfo;

		private global::System.Func<TTarget, TField> getter;

		private global::System.Action<TTarget, TField> setter;

		public InstanceFieldAccessor(global::System.Reflection.FieldInfo fieldInfo)
		{
			if (global::Unity.VisualScripting.OptimizedReflection.safeMode)
			{
				global::Unity.VisualScripting.Ensure.That("fieldInfo").IsNotNull(fieldInfo);
				if (fieldInfo.DeclaringType != typeof(TTarget))
				{
					throw new global::System.ArgumentException("Declaring type of field info doesn't match generic type.", "fieldInfo");
				}
				if (fieldInfo.FieldType != typeof(TField))
				{
					throw new global::System.ArgumentException("Field type of field info doesn't match generic type.", "fieldInfo");
				}
				if (fieldInfo.IsStatic)
				{
					throw new global::System.ArgumentException("The field is static.", "fieldInfo");
				}
			}
			this.fieldInfo = fieldInfo;
		}

		public void Compile()
		{
			if (global::Unity.VisualScripting.OptimizedReflection.useJit)
			{
				global::System.Linq.Expressions.ParameterExpression parameterExpression = global::System.Linq.Expressions.Expression.Parameter(typeof(TTarget), "target");
				global::System.Linq.Expressions.MemberExpression memberExpression = global::System.Linq.Expressions.Expression.Field(parameterExpression, fieldInfo);
				getter = global::System.Linq.Expressions.Expression.Lambda<global::System.Func<TTarget, TField>>(memberExpression, new global::System.Linq.Expressions.ParameterExpression[1] { parameterExpression }).Compile();
				if (fieldInfo.CanWrite())
				{
					try
					{
						global::System.Linq.Expressions.ParameterExpression parameterExpression2 = global::System.Linq.Expressions.Expression.Parameter(typeof(TField));
						global::System.Linq.Expressions.BinaryExpression body = global::System.Linq.Expressions.Expression.Assign(memberExpression, parameterExpression2);
						setter = global::System.Linq.Expressions.Expression.Lambda<global::System.Action<TTarget, TField>>(body, new global::System.Linq.Expressions.ParameterExpression[2] { parameterExpression, parameterExpression2 }).Compile();
						return;
					}
					catch
					{
						global::UnityEngine.Debug.Log("Failed instance field: " + fieldInfo);
						throw;
					}
				}
				return;
			}
			getter = (TTarget instance) => (TField)fieldInfo.GetValue(instance);
			if (fieldInfo.CanWrite())
			{
				setter = delegate(TTarget instance, TField value)
				{
					fieldInfo.SetValue(instance, value);
				};
			}
		}

		public object GetValue(object target)
		{
			if (global::Unity.VisualScripting.OptimizedReflection.safeMode)
			{
				global::Unity.VisualScripting.OptimizedReflection.VerifyInstanceTarget<TTarget>(target);
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
					throw new global::System.Reflection.TargetException($"The field '{typeof(TTarget)}.{fieldInfo.Name}' cannot be assigned.");
				}
				if (!typeof(TField).IsAssignableFrom(value))
				{
					throw new global::System.ArgumentException(string.Format("The provided value for '{0}.{1}' does not match the field type.\nProvided: {2}\nExpected: {3}", typeof(TTarget), fieldInfo.Name, value?.GetType()?.ToString() ?? "null", typeof(TField)));
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
			setter((TTarget)target, (TField)value);
		}
	}
}
