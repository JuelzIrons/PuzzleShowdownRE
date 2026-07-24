namespace Newtonsoft.Json.Serialization
{
	public class JsonDictionaryContract : global::Newtonsoft.Json.Serialization.JsonContainerContract
	{
		private readonly global::System.Type? _genericCollectionDefinitionType;

		private global::System.Type? _genericWrapperType;

		private global::Newtonsoft.Json.Serialization.ObjectConstructor<object>? _genericWrapperCreator;

		private global::System.Func<object>? _genericTemporaryDictionaryCreator;

		private readonly global::System.Reflection.ConstructorInfo? _parameterizedConstructor;

		private global::Newtonsoft.Json.Serialization.ObjectConstructor<object>? _overrideCreator;

		private global::Newtonsoft.Json.Serialization.ObjectConstructor<object>? _parameterizedCreator;

		public global::System.Func<string, string>? DictionaryKeyResolver { get; set; }

		public global::System.Type? DictionaryKeyType { get; }

		public global::System.Type? DictionaryValueType { get; }

		internal global::Newtonsoft.Json.Serialization.JsonContract? KeyContract { get; set; }

		internal bool ShouldCreateWrapper { get; }

		internal global::Newtonsoft.Json.Serialization.ObjectConstructor<object>? ParameterizedCreator
		{
			get
			{
				if (_parameterizedCreator == null && _parameterizedConstructor != null)
				{
					_parameterizedCreator = global::Newtonsoft.Json.Serialization.JsonTypeReflector.ReflectionDelegateFactory.CreateParameterizedConstructor(_parameterizedConstructor);
				}
				return _parameterizedCreator;
			}
		}

		public global::Newtonsoft.Json.Serialization.ObjectConstructor<object>? OverrideCreator
		{
			get
			{
				return _overrideCreator;
			}
			set
			{
				_overrideCreator = value;
			}
		}

		public bool HasParameterizedCreator { get; set; }

		internal bool HasParameterizedCreatorInternal
		{
			get
			{
				if (!HasParameterizedCreator && _parameterizedCreator == null)
				{
					return _parameterizedConstructor != null;
				}
				return true;
			}
		}

		public JsonDictionaryContract(global::System.Type underlyingType)
			: base(underlyingType)
		{
			ContractType = global::Newtonsoft.Json.Serialization.JsonContractType.Dictionary;
			global::System.Type keyType;
			global::System.Type valueType;
			if (global::Newtonsoft.Json.Utilities.ReflectionUtils.ImplementsGenericDefinition(NonNullableUnderlyingType, typeof(global::System.Collections.Generic.IDictionary<, >), out _genericCollectionDefinitionType))
			{
				keyType = _genericCollectionDefinitionType.GetGenericArguments()[0];
				valueType = _genericCollectionDefinitionType.GetGenericArguments()[1];
				if (global::Newtonsoft.Json.Utilities.ReflectionUtils.IsGenericDefinition(NonNullableUnderlyingType, typeof(global::System.Collections.Generic.IDictionary<, >)))
				{
					base.CreatedType = typeof(global::System.Collections.Generic.Dictionary<, >).MakeGenericType(keyType, valueType);
				}
				else if (global::Newtonsoft.Json.Utilities.TypeExtensions.IsGenericType(NonNullableUnderlyingType) && NonNullableUnderlyingType.GetGenericTypeDefinition().FullName == "System.Collections.Concurrent.ConcurrentDictionary`2")
				{
					ShouldCreateWrapper = true;
				}
				IsReadOnlyOrFixedSize = global::Newtonsoft.Json.Utilities.ReflectionUtils.InheritsGenericDefinition(NonNullableUnderlyingType, typeof(global::System.Collections.ObjectModel.ReadOnlyDictionary<, >));
			}
			else if (global::Newtonsoft.Json.Utilities.ReflectionUtils.ImplementsGenericDefinition(NonNullableUnderlyingType, typeof(global::System.Collections.Generic.IReadOnlyDictionary<, >), out _genericCollectionDefinitionType))
			{
				keyType = _genericCollectionDefinitionType.GetGenericArguments()[0];
				valueType = _genericCollectionDefinitionType.GetGenericArguments()[1];
				if (global::Newtonsoft.Json.Utilities.ReflectionUtils.IsGenericDefinition(NonNullableUnderlyingType, typeof(global::System.Collections.Generic.IReadOnlyDictionary<, >)))
				{
					base.CreatedType = typeof(global::System.Collections.ObjectModel.ReadOnlyDictionary<, >).MakeGenericType(keyType, valueType);
				}
				IsReadOnlyOrFixedSize = true;
			}
			else
			{
				global::Newtonsoft.Json.Utilities.ReflectionUtils.GetDictionaryKeyValueTypes(NonNullableUnderlyingType, out keyType, out valueType);
				if (NonNullableUnderlyingType == typeof(global::System.Collections.IDictionary))
				{
					base.CreatedType = typeof(global::System.Collections.Generic.Dictionary<object, object>);
				}
			}
			if (keyType != null && valueType != null)
			{
				_parameterizedConstructor = global::Newtonsoft.Json.Utilities.CollectionUtils.ResolveEnumerableCollectionConstructor(base.CreatedType, typeof(global::System.Collections.Generic.KeyValuePair<, >).MakeGenericType(keyType, valueType), typeof(global::System.Collections.Generic.IDictionary<, >).MakeGenericType(keyType, valueType));
				if (!HasParameterizedCreatorInternal && NonNullableUnderlyingType.Name == "FSharpMap`2")
				{
					global::Newtonsoft.Json.Utilities.FSharpUtils.EnsureInitialized(global::Newtonsoft.Json.Utilities.TypeExtensions.Assembly(NonNullableUnderlyingType));
					_parameterizedCreator = global::Newtonsoft.Json.Utilities.FSharpUtils.Instance.CreateMap(keyType, valueType);
				}
			}
			if (!typeof(global::System.Collections.IDictionary).IsAssignableFrom(base.CreatedType))
			{
				ShouldCreateWrapper = true;
			}
			DictionaryKeyType = keyType;
			DictionaryValueType = valueType;
			if (DictionaryKeyType != null && DictionaryValueType != null && global::Newtonsoft.Json.Utilities.ImmutableCollectionsUtils.TryBuildImmutableForDictionaryContract(NonNullableUnderlyingType, DictionaryKeyType, DictionaryValueType, out global::System.Type createdType, out global::Newtonsoft.Json.Serialization.ObjectConstructor<object> parameterizedCreator))
			{
				base.CreatedType = createdType;
				_parameterizedCreator = parameterizedCreator;
				IsReadOnlyOrFixedSize = true;
			}
		}

		internal global::Newtonsoft.Json.Utilities.IWrappedDictionary CreateWrapper(object dictionary)
		{
			if (_genericWrapperCreator == null)
			{
				_genericWrapperType = typeof(global::Newtonsoft.Json.Utilities.DictionaryWrapper<, >).MakeGenericType(DictionaryKeyType, DictionaryValueType);
				global::System.Reflection.ConstructorInfo constructor = _genericWrapperType.GetConstructor(new global::System.Type[1] { _genericCollectionDefinitionType });
				_genericWrapperCreator = global::Newtonsoft.Json.Serialization.JsonTypeReflector.ReflectionDelegateFactory.CreateParameterizedConstructor(constructor);
			}
			return (global::Newtonsoft.Json.Utilities.IWrappedDictionary)_genericWrapperCreator(dictionary);
		}

		internal global::System.Collections.IDictionary CreateTemporaryDictionary()
		{
			if (_genericTemporaryDictionaryCreator == null)
			{
				global::System.Type type = typeof(global::System.Collections.Generic.Dictionary<, >).MakeGenericType(DictionaryKeyType ?? typeof(object), DictionaryValueType ?? typeof(object));
				_genericTemporaryDictionaryCreator = global::Newtonsoft.Json.Serialization.JsonTypeReflector.ReflectionDelegateFactory.CreateDefaultConstructor<object>(type);
			}
			return (global::System.Collections.IDictionary)_genericTemporaryDictionaryCreator();
		}
	}
}
