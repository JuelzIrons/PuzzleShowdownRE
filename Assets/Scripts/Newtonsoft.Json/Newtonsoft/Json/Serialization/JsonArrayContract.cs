namespace Newtonsoft.Json.Serialization
{
	public class JsonArrayContract : global::Newtonsoft.Json.Serialization.JsonContainerContract
	{
		private readonly global::System.Type? _genericCollectionDefinitionType;

		private global::System.Type? _genericWrapperType;

		private global::Newtonsoft.Json.Serialization.ObjectConstructor<object>? _genericWrapperCreator;

		private global::System.Func<object>? _genericTemporaryCollectionCreator;

		private readonly global::System.Reflection.ConstructorInfo? _parameterizedConstructor;

		private global::Newtonsoft.Json.Serialization.ObjectConstructor<object>? _parameterizedCreator;

		private global::Newtonsoft.Json.Serialization.ObjectConstructor<object>? _overrideCreator;

		public global::System.Type? CollectionItemType { get; }

		public bool IsMultidimensionalArray { get; }

		internal bool IsArray { get; }

		internal bool ShouldCreateWrapper { get; }

		internal bool CanDeserialize { get; private set; }

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
				CanDeserialize = true;
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

		public JsonArrayContract(global::System.Type underlyingType)
			: base(underlyingType)
		{
			ContractType = global::Newtonsoft.Json.Serialization.JsonContractType.Array;
			IsArray = base.CreatedType.IsArray || (global::Newtonsoft.Json.Utilities.TypeExtensions.IsGenericType(NonNullableUnderlyingType) && NonNullableUnderlyingType.GetGenericTypeDefinition().FullName == "System.Linq.EmptyPartition`1");
			bool canDeserialize;
			global::System.Type implementingType;
			if (IsArray)
			{
				CollectionItemType = global::Newtonsoft.Json.Utilities.ReflectionUtils.GetCollectionItemType(base.UnderlyingType);
				IsReadOnlyOrFixedSize = true;
				_genericCollectionDefinitionType = typeof(global::System.Collections.Generic.List<>).MakeGenericType(CollectionItemType);
				canDeserialize = true;
				IsMultidimensionalArray = base.CreatedType.IsArray && base.UnderlyingType.GetArrayRank() > 1;
			}
			else if (typeof(global::System.Collections.IList).IsAssignableFrom(NonNullableUnderlyingType))
			{
				if (global::Newtonsoft.Json.Utilities.ReflectionUtils.ImplementsGenericDefinition(NonNullableUnderlyingType, typeof(global::System.Collections.Generic.ICollection<>), out _genericCollectionDefinitionType))
				{
					CollectionItemType = _genericCollectionDefinitionType.GetGenericArguments()[0];
				}
				else
				{
					CollectionItemType = global::Newtonsoft.Json.Utilities.ReflectionUtils.GetCollectionItemType(NonNullableUnderlyingType);
				}
				if (NonNullableUnderlyingType == typeof(global::System.Collections.IList))
				{
					base.CreatedType = typeof(global::System.Collections.Generic.List<object>);
				}
				if (CollectionItemType != null)
				{
					_parameterizedConstructor = global::Newtonsoft.Json.Utilities.CollectionUtils.ResolveEnumerableCollectionConstructor(NonNullableUnderlyingType, CollectionItemType);
				}
				IsReadOnlyOrFixedSize = global::Newtonsoft.Json.Utilities.ReflectionUtils.InheritsGenericDefinition(NonNullableUnderlyingType, typeof(global::System.Collections.ObjectModel.ReadOnlyCollection<>));
				canDeserialize = true;
			}
			else if (global::Newtonsoft.Json.Utilities.ReflectionUtils.ImplementsGenericDefinition(NonNullableUnderlyingType, typeof(global::System.Collections.Generic.ICollection<>), out _genericCollectionDefinitionType))
			{
				CollectionItemType = _genericCollectionDefinitionType.GetGenericArguments()[0];
				if (global::Newtonsoft.Json.Utilities.ReflectionUtils.IsGenericDefinition(NonNullableUnderlyingType, typeof(global::System.Collections.Generic.ICollection<>)) || global::Newtonsoft.Json.Utilities.ReflectionUtils.IsGenericDefinition(NonNullableUnderlyingType, typeof(global::System.Collections.Generic.IList<>)))
				{
					base.CreatedType = typeof(global::System.Collections.Generic.List<>).MakeGenericType(CollectionItemType);
				}
				if (global::Newtonsoft.Json.Utilities.ReflectionUtils.IsGenericDefinition(NonNullableUnderlyingType, typeof(global::System.Collections.Generic.ISet<>)))
				{
					base.CreatedType = typeof(global::System.Collections.Generic.HashSet<>).MakeGenericType(CollectionItemType);
				}
				_parameterizedConstructor = global::Newtonsoft.Json.Utilities.CollectionUtils.ResolveEnumerableCollectionConstructor(NonNullableUnderlyingType, CollectionItemType);
				canDeserialize = true;
				ShouldCreateWrapper = true;
			}
			else if (global::Newtonsoft.Json.Utilities.ReflectionUtils.ImplementsGenericDefinition(NonNullableUnderlyingType, typeof(global::System.Collections.Generic.IReadOnlyCollection<>), out implementingType))
			{
				CollectionItemType = implementingType.GetGenericArguments()[0];
				if (global::Newtonsoft.Json.Utilities.ReflectionUtils.IsGenericDefinition(NonNullableUnderlyingType, typeof(global::System.Collections.Generic.IReadOnlyCollection<>)) || global::Newtonsoft.Json.Utilities.ReflectionUtils.IsGenericDefinition(NonNullableUnderlyingType, typeof(global::System.Collections.Generic.IReadOnlyList<>)))
				{
					base.CreatedType = typeof(global::System.Collections.ObjectModel.ReadOnlyCollection<>).MakeGenericType(CollectionItemType);
				}
				_genericCollectionDefinitionType = typeof(global::System.Collections.Generic.List<>).MakeGenericType(CollectionItemType);
				_parameterizedConstructor = global::Newtonsoft.Json.Utilities.CollectionUtils.ResolveEnumerableCollectionConstructor(base.CreatedType, CollectionItemType);
				StoreFSharpListCreatorIfNecessary(NonNullableUnderlyingType);
				IsReadOnlyOrFixedSize = true;
				canDeserialize = HasParameterizedCreatorInternal;
			}
			else if (global::Newtonsoft.Json.Utilities.ReflectionUtils.ImplementsGenericDefinition(NonNullableUnderlyingType, typeof(global::System.Collections.Generic.IEnumerable<>), out implementingType))
			{
				CollectionItemType = implementingType.GetGenericArguments()[0];
				if (global::Newtonsoft.Json.Utilities.ReflectionUtils.IsGenericDefinition(base.UnderlyingType, typeof(global::System.Collections.Generic.IEnumerable<>)))
				{
					base.CreatedType = typeof(global::System.Collections.Generic.List<>).MakeGenericType(CollectionItemType);
				}
				_parameterizedConstructor = global::Newtonsoft.Json.Utilities.CollectionUtils.ResolveEnumerableCollectionConstructor(NonNullableUnderlyingType, CollectionItemType);
				StoreFSharpListCreatorIfNecessary(NonNullableUnderlyingType);
				if (global::Newtonsoft.Json.Utilities.TypeExtensions.IsGenericType(NonNullableUnderlyingType) && NonNullableUnderlyingType.GetGenericTypeDefinition() == typeof(global::System.Collections.Generic.IEnumerable<>))
				{
					_genericCollectionDefinitionType = implementingType;
					IsReadOnlyOrFixedSize = false;
					ShouldCreateWrapper = false;
					canDeserialize = true;
				}
				else
				{
					_genericCollectionDefinitionType = typeof(global::System.Collections.Generic.List<>).MakeGenericType(CollectionItemType);
					IsReadOnlyOrFixedSize = true;
					ShouldCreateWrapper = true;
					canDeserialize = HasParameterizedCreatorInternal;
				}
			}
			else
			{
				canDeserialize = false;
				ShouldCreateWrapper = true;
			}
			CanDeserialize = canDeserialize;
			if (CollectionItemType != null && global::Newtonsoft.Json.Utilities.ImmutableCollectionsUtils.TryBuildImmutableForArrayContract(NonNullableUnderlyingType, CollectionItemType, out global::System.Type createdType, out global::Newtonsoft.Json.Serialization.ObjectConstructor<object> parameterizedCreator))
			{
				base.CreatedType = createdType;
				_parameterizedCreator = parameterizedCreator;
				IsReadOnlyOrFixedSize = true;
				CanDeserialize = true;
			}
		}

		internal global::Newtonsoft.Json.Utilities.IWrappedCollection CreateWrapper(object list)
		{
			if (_genericWrapperCreator == null)
			{
				_genericWrapperType = typeof(global::Newtonsoft.Json.Utilities.CollectionWrapper<>).MakeGenericType(CollectionItemType);
				global::System.Type type = ((!global::Newtonsoft.Json.Utilities.ReflectionUtils.InheritsGenericDefinition(_genericCollectionDefinitionType, typeof(global::System.Collections.Generic.List<>)) && !(_genericCollectionDefinitionType.GetGenericTypeDefinition() == typeof(global::System.Collections.Generic.IEnumerable<>))) ? _genericCollectionDefinitionType : typeof(global::System.Collections.Generic.ICollection<>).MakeGenericType(CollectionItemType));
				global::System.Reflection.ConstructorInfo constructor = _genericWrapperType.GetConstructor(new global::System.Type[1] { type });
				_genericWrapperCreator = global::Newtonsoft.Json.Serialization.JsonTypeReflector.ReflectionDelegateFactory.CreateParameterizedConstructor(constructor);
			}
			return (global::Newtonsoft.Json.Utilities.IWrappedCollection)_genericWrapperCreator(list);
		}

		internal global::System.Collections.IList CreateTemporaryCollection()
		{
			if (_genericTemporaryCollectionCreator == null)
			{
				global::System.Type type = ((IsMultidimensionalArray || CollectionItemType == null) ? typeof(object) : CollectionItemType);
				global::System.Type type2 = typeof(global::System.Collections.Generic.List<>).MakeGenericType(type);
				_genericTemporaryCollectionCreator = global::Newtonsoft.Json.Serialization.JsonTypeReflector.ReflectionDelegateFactory.CreateDefaultConstructor<object>(type2);
			}
			return (global::System.Collections.IList)_genericTemporaryCollectionCreator();
		}

		private void StoreFSharpListCreatorIfNecessary(global::System.Type underlyingType)
		{
			if (!HasParameterizedCreatorInternal && underlyingType.Name == "FSharpList`1")
			{
				global::Newtonsoft.Json.Utilities.FSharpUtils.EnsureInitialized(global::Newtonsoft.Json.Utilities.TypeExtensions.Assembly(underlyingType));
				_parameterizedCreator = global::Newtonsoft.Json.Utilities.FSharpUtils.Instance.CreateSeq(CollectionItemType);
			}
		}
	}
}
