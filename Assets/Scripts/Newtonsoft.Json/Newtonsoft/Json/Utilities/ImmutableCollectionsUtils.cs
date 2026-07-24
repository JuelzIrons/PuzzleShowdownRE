namespace Newtonsoft.Json.Utilities
{
	internal static class ImmutableCollectionsUtils
	{
		internal class ImmutableCollectionTypeInfo
		{
			public string ContractTypeName { get; set; }

			public string CreatedTypeName { get; set; }

			public string BuilderTypeName { get; set; }

			public ImmutableCollectionTypeInfo(string contractTypeName, string createdTypeName, string builderTypeName)
			{
				ContractTypeName = contractTypeName;
				CreatedTypeName = createdTypeName;
				BuilderTypeName = builderTypeName;
			}
		}

		private const string ImmutableListGenericInterfaceTypeName = "System.Collections.Immutable.IImmutableList`1";

		private const string ImmutableQueueGenericInterfaceTypeName = "System.Collections.Immutable.IImmutableQueue`1";

		private const string ImmutableStackGenericInterfaceTypeName = "System.Collections.Immutable.IImmutableStack`1";

		private const string ImmutableSetGenericInterfaceTypeName = "System.Collections.Immutable.IImmutableSet`1";

		private const string ImmutableArrayTypeName = "System.Collections.Immutable.ImmutableArray";

		private const string ImmutableArrayGenericTypeName = "System.Collections.Immutable.ImmutableArray`1";

		private const string ImmutableListTypeName = "System.Collections.Immutable.ImmutableList";

		private const string ImmutableListGenericTypeName = "System.Collections.Immutable.ImmutableList`1";

		private const string ImmutableQueueTypeName = "System.Collections.Immutable.ImmutableQueue";

		private const string ImmutableQueueGenericTypeName = "System.Collections.Immutable.ImmutableQueue`1";

		private const string ImmutableStackTypeName = "System.Collections.Immutable.ImmutableStack";

		private const string ImmutableStackGenericTypeName = "System.Collections.Immutable.ImmutableStack`1";

		private const string ImmutableSortedSetTypeName = "System.Collections.Immutable.ImmutableSortedSet";

		private const string ImmutableSortedSetGenericTypeName = "System.Collections.Immutable.ImmutableSortedSet`1";

		private const string ImmutableHashSetTypeName = "System.Collections.Immutable.ImmutableHashSet";

		private const string ImmutableHashSetGenericTypeName = "System.Collections.Immutable.ImmutableHashSet`1";

		private static readonly global::System.Collections.Generic.IList<global::Newtonsoft.Json.Utilities.ImmutableCollectionsUtils.ImmutableCollectionTypeInfo> ArrayContractImmutableCollectionDefinitions = new global::System.Collections.Generic.List<global::Newtonsoft.Json.Utilities.ImmutableCollectionsUtils.ImmutableCollectionTypeInfo>
		{
			new global::Newtonsoft.Json.Utilities.ImmutableCollectionsUtils.ImmutableCollectionTypeInfo("System.Collections.Immutable.IImmutableList`1", "System.Collections.Immutable.ImmutableList`1", "System.Collections.Immutable.ImmutableList"),
			new global::Newtonsoft.Json.Utilities.ImmutableCollectionsUtils.ImmutableCollectionTypeInfo("System.Collections.Immutable.ImmutableList`1", "System.Collections.Immutable.ImmutableList`1", "System.Collections.Immutable.ImmutableList"),
			new global::Newtonsoft.Json.Utilities.ImmutableCollectionsUtils.ImmutableCollectionTypeInfo("System.Collections.Immutable.IImmutableQueue`1", "System.Collections.Immutable.ImmutableQueue`1", "System.Collections.Immutable.ImmutableQueue"),
			new global::Newtonsoft.Json.Utilities.ImmutableCollectionsUtils.ImmutableCollectionTypeInfo("System.Collections.Immutable.ImmutableQueue`1", "System.Collections.Immutable.ImmutableQueue`1", "System.Collections.Immutable.ImmutableQueue"),
			new global::Newtonsoft.Json.Utilities.ImmutableCollectionsUtils.ImmutableCollectionTypeInfo("System.Collections.Immutable.IImmutableStack`1", "System.Collections.Immutable.ImmutableStack`1", "System.Collections.Immutable.ImmutableStack"),
			new global::Newtonsoft.Json.Utilities.ImmutableCollectionsUtils.ImmutableCollectionTypeInfo("System.Collections.Immutable.ImmutableStack`1", "System.Collections.Immutable.ImmutableStack`1", "System.Collections.Immutable.ImmutableStack"),
			new global::Newtonsoft.Json.Utilities.ImmutableCollectionsUtils.ImmutableCollectionTypeInfo("System.Collections.Immutable.IImmutableSet`1", "System.Collections.Immutable.ImmutableHashSet`1", "System.Collections.Immutable.ImmutableHashSet"),
			new global::Newtonsoft.Json.Utilities.ImmutableCollectionsUtils.ImmutableCollectionTypeInfo("System.Collections.Immutable.ImmutableSortedSet`1", "System.Collections.Immutable.ImmutableSortedSet`1", "System.Collections.Immutable.ImmutableSortedSet"),
			new global::Newtonsoft.Json.Utilities.ImmutableCollectionsUtils.ImmutableCollectionTypeInfo("System.Collections.Immutable.ImmutableHashSet`1", "System.Collections.Immutable.ImmutableHashSet`1", "System.Collections.Immutable.ImmutableHashSet"),
			new global::Newtonsoft.Json.Utilities.ImmutableCollectionsUtils.ImmutableCollectionTypeInfo("System.Collections.Immutable.ImmutableArray`1", "System.Collections.Immutable.ImmutableArray`1", "System.Collections.Immutable.ImmutableArray")
		};

		private const string ImmutableDictionaryGenericInterfaceTypeName = "System.Collections.Immutable.IImmutableDictionary`2";

		private const string ImmutableDictionaryTypeName = "System.Collections.Immutable.ImmutableDictionary";

		private const string ImmutableDictionaryGenericTypeName = "System.Collections.Immutable.ImmutableDictionary`2";

		private const string ImmutableSortedDictionaryTypeName = "System.Collections.Immutable.ImmutableSortedDictionary";

		private const string ImmutableSortedDictionaryGenericTypeName = "System.Collections.Immutable.ImmutableSortedDictionary`2";

		private static readonly global::System.Collections.Generic.IList<global::Newtonsoft.Json.Utilities.ImmutableCollectionsUtils.ImmutableCollectionTypeInfo> DictionaryContractImmutableCollectionDefinitions = new global::System.Collections.Generic.List<global::Newtonsoft.Json.Utilities.ImmutableCollectionsUtils.ImmutableCollectionTypeInfo>
		{
			new global::Newtonsoft.Json.Utilities.ImmutableCollectionsUtils.ImmutableCollectionTypeInfo("System.Collections.Immutable.IImmutableDictionary`2", "System.Collections.Immutable.ImmutableDictionary`2", "System.Collections.Immutable.ImmutableDictionary"),
			new global::Newtonsoft.Json.Utilities.ImmutableCollectionsUtils.ImmutableCollectionTypeInfo("System.Collections.Immutable.ImmutableSortedDictionary`2", "System.Collections.Immutable.ImmutableSortedDictionary`2", "System.Collections.Immutable.ImmutableSortedDictionary"),
			new global::Newtonsoft.Json.Utilities.ImmutableCollectionsUtils.ImmutableCollectionTypeInfo("System.Collections.Immutable.ImmutableDictionary`2", "System.Collections.Immutable.ImmutableDictionary`2", "System.Collections.Immutable.ImmutableDictionary")
		};

		internal static bool TryBuildImmutableForArrayContract(global::System.Type underlyingType, global::System.Type collectionItemType, [global::System.Diagnostics.CodeAnalysis.NotNullWhen(true)] out global::System.Type? createdType, [global::System.Diagnostics.CodeAnalysis.NotNullWhen(true)] out global::Newtonsoft.Json.Serialization.ObjectConstructor<object>? parameterizedCreator)
		{
			if (underlyingType.IsGenericType())
			{
				global::System.Type genericTypeDefinition = underlyingType.GetGenericTypeDefinition();
				string name = genericTypeDefinition.FullName;
				global::Newtonsoft.Json.Utilities.ImmutableCollectionsUtils.ImmutableCollectionTypeInfo immutableCollectionTypeInfo = global::System.Linq.Enumerable.FirstOrDefault(ArrayContractImmutableCollectionDefinitions, (global::Newtonsoft.Json.Utilities.ImmutableCollectionsUtils.ImmutableCollectionTypeInfo d) => d.ContractTypeName == name);
				if (immutableCollectionTypeInfo != null)
				{
					global::System.Type type = genericTypeDefinition.Assembly().GetType(immutableCollectionTypeInfo.CreatedTypeName);
					global::System.Type type2 = genericTypeDefinition.Assembly().GetType(immutableCollectionTypeInfo.BuilderTypeName);
					if (type != null && type2 != null)
					{
						global::System.Reflection.MethodInfo methodInfo = global::System.Linq.Enumerable.FirstOrDefault(type2.GetMethods(), (global::System.Reflection.MethodInfo m) => m.Name == "CreateRange" && m.GetParameters().Length == 1);
						if (methodInfo != null)
						{
							createdType = type.MakeGenericType(collectionItemType);
							global::System.Reflection.MethodInfo method = methodInfo.MakeGenericMethod(collectionItemType);
							parameterizedCreator = global::Newtonsoft.Json.Serialization.JsonTypeReflector.ReflectionDelegateFactory.CreateParameterizedConstructor(method);
							return true;
						}
					}
				}
			}
			createdType = null;
			parameterizedCreator = null;
			return false;
		}

		internal static bool TryBuildImmutableForDictionaryContract(global::System.Type underlyingType, global::System.Type keyItemType, global::System.Type valueItemType, [global::System.Diagnostics.CodeAnalysis.NotNullWhen(true)] out global::System.Type? createdType, [global::System.Diagnostics.CodeAnalysis.NotNullWhen(true)] out global::Newtonsoft.Json.Serialization.ObjectConstructor<object>? parameterizedCreator)
		{
			if (underlyingType.IsGenericType())
			{
				global::System.Type genericTypeDefinition = underlyingType.GetGenericTypeDefinition();
				string name = genericTypeDefinition.FullName;
				global::Newtonsoft.Json.Utilities.ImmutableCollectionsUtils.ImmutableCollectionTypeInfo immutableCollectionTypeInfo = global::System.Linq.Enumerable.FirstOrDefault(DictionaryContractImmutableCollectionDefinitions, (global::Newtonsoft.Json.Utilities.ImmutableCollectionsUtils.ImmutableCollectionTypeInfo d) => d.ContractTypeName == name);
				if (immutableCollectionTypeInfo != null)
				{
					global::System.Type type = genericTypeDefinition.Assembly().GetType(immutableCollectionTypeInfo.CreatedTypeName);
					global::System.Type type2 = genericTypeDefinition.Assembly().GetType(immutableCollectionTypeInfo.BuilderTypeName);
					if (type != null && type2 != null)
					{
						global::System.Reflection.MethodInfo methodInfo = global::System.Linq.Enumerable.FirstOrDefault(type2.GetMethods(), delegate(global::System.Reflection.MethodInfo m)
						{
							global::System.Reflection.ParameterInfo[] parameters = m.GetParameters();
							return m.Name == "CreateRange" && parameters.Length == 1 && parameters[0].ParameterType.IsGenericType() && parameters[0].ParameterType.GetGenericTypeDefinition() == typeof(global::System.Collections.Generic.IEnumerable<>);
						});
						if (methodInfo != null)
						{
							createdType = type.MakeGenericType(keyItemType, valueItemType);
							global::System.Reflection.MethodInfo method = methodInfo.MakeGenericMethod(keyItemType, valueItemType);
							parameterizedCreator = global::Newtonsoft.Json.Serialization.JsonTypeReflector.ReflectionDelegateFactory.CreateParameterizedConstructor(method);
							return true;
						}
					}
				}
			}
			createdType = null;
			parameterizedCreator = null;
			return false;
		}
	}
}
