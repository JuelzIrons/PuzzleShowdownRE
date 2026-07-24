namespace Unity.VisualScripting.FullSerializer
{
	public static class fsReflectionUtility
	{
		public static global::System.Type GetInterface(global::System.Type type, global::System.Type interfaceType)
		{
			if (global::Unity.VisualScripting.FullSerializer.Internal.fsPortableReflection.Resolve(interfaceType).IsGenericType && !global::Unity.VisualScripting.FullSerializer.Internal.fsPortableReflection.Resolve(interfaceType).IsGenericTypeDefinition)
			{
				throw new global::System.ArgumentException("GetInterface requires that if the interface type is generic, then it must be the generic type definition, not a specific generic type instantiation");
			}
			while (type != null)
			{
				global::System.Type[] interfaces = type.GetInterfaces();
				foreach (global::System.Type type2 in interfaces)
				{
					if (global::Unity.VisualScripting.FullSerializer.Internal.fsPortableReflection.Resolve(type2).IsGenericType)
					{
						if (interfaceType == type2.GetGenericTypeDefinition())
						{
							return type2;
						}
					}
					else if (interfaceType == type2)
					{
						return type2;
					}
				}
				type = global::Unity.VisualScripting.FullSerializer.Internal.fsPortableReflection.Resolve(type).BaseType;
			}
			return null;
		}
	}
}
