namespace Newtonsoft.Json.Utilities
{
	internal abstract class ReflectionDelegateFactory
	{
		public global::System.Func<T, object?> CreateGet<T>(global::System.Reflection.MemberInfo memberInfo)
		{
			if (memberInfo is global::System.Reflection.PropertyInfo propertyInfo)
			{
				if (propertyInfo.PropertyType.IsByRef)
				{
					throw new global::System.InvalidOperationException("Could not create getter for {0}. ByRef return values are not supported.".FormatWith(global::System.Globalization.CultureInfo.InvariantCulture, propertyInfo));
				}
				return CreateGet<T>(propertyInfo);
			}
			if (memberInfo is global::System.Reflection.FieldInfo fieldInfo)
			{
				return CreateGet<T>(fieldInfo);
			}
			throw new global::System.Exception("Could not create getter for {0}.".FormatWith(global::System.Globalization.CultureInfo.InvariantCulture, memberInfo));
		}

		public global::System.Action<T, object?> CreateSet<T>(global::System.Reflection.MemberInfo memberInfo)
		{
			if (memberInfo is global::System.Reflection.PropertyInfo propertyInfo)
			{
				return CreateSet<T>(propertyInfo);
			}
			if (memberInfo is global::System.Reflection.FieldInfo fieldInfo)
			{
				return CreateSet<T>(fieldInfo);
			}
			throw new global::System.Exception("Could not create setter for {0}.".FormatWith(global::System.Globalization.CultureInfo.InvariantCulture, memberInfo));
		}

		public abstract global::Newtonsoft.Json.Utilities.MethodCall<T, object?> CreateMethodCall<T>(global::System.Reflection.MethodBase method);

		public abstract global::Newtonsoft.Json.Serialization.ObjectConstructor<object> CreateParameterizedConstructor(global::System.Reflection.MethodBase method);

		public abstract global::System.Func<T> CreateDefaultConstructor<T>(global::System.Type type);

		public abstract global::System.Func<T, object?> CreateGet<T>(global::System.Reflection.PropertyInfo propertyInfo);

		public abstract global::System.Func<T, object?> CreateGet<T>(global::System.Reflection.FieldInfo fieldInfo);

		public abstract global::System.Action<T, object?> CreateSet<T>(global::System.Reflection.FieldInfo fieldInfo);

		public abstract global::System.Action<T, object?> CreateSet<T>(global::System.Reflection.PropertyInfo propertyInfo);
	}
}
