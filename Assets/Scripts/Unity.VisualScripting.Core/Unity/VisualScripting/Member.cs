namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.SerializationVersion("A", new global::System.Type[] { })]
	public sealed class Member : global::UnityEngine.ISerializationCallbackReceiver
	{
		public enum Source
		{
			Unknown = 0,
			Field = 1,
			Property = 2,
			Method = 3,
			Constructor = 4
		}

		[global::Unity.VisualScripting.SerializeAs("name")]
		private string _name;

		[global::Unity.VisualScripting.SerializeAs("parameterTypes")]
		private global::System.Type[] _parameterTypes;

		[global::Unity.VisualScripting.SerializeAs("targetType")]
		private global::System.Type _targetType;

		[global::Unity.VisualScripting.SerializeAs("targetTypeName")]
		private string _targetTypeName;

		[global::Unity.VisualScripting.DoNotSerialize]
		private global::Unity.VisualScripting.Member.Source _source;

		[global::Unity.VisualScripting.DoNotSerialize]
		private global::System.Reflection.FieldInfo _fieldInfo;

		[global::Unity.VisualScripting.DoNotSerialize]
		private global::System.Reflection.PropertyInfo _propertyInfo;

		[global::Unity.VisualScripting.DoNotSerialize]
		private global::System.Reflection.MethodInfo _methodInfo;

		[global::Unity.VisualScripting.DoNotSerialize]
		private global::System.Reflection.ConstructorInfo _constructorInfo;

		[global::Unity.VisualScripting.DoNotSerialize]
		private bool _isExtension;

		[global::Unity.VisualScripting.DoNotSerialize]
		private bool _isInvokedAsExtension;

		[global::Unity.VisualScripting.DoNotSerialize]
		private global::Unity.VisualScripting.IOptimizedAccessor fieldAccessor;

		[global::Unity.VisualScripting.DoNotSerialize]
		private global::Unity.VisualScripting.IOptimizedAccessor propertyAccessor;

		[global::Unity.VisualScripting.DoNotSerialize]
		private global::Unity.VisualScripting.IOptimizedInvoker methodInvoker;

		public const global::System.Reflection.MemberTypes SupportedMemberTypes = global::System.Reflection.MemberTypes.Constructor | global::System.Reflection.MemberTypes.Field | global::System.Reflection.MemberTypes.Method | global::System.Reflection.MemberTypes.Property;

		public const global::System.Reflection.BindingFlags SupportedBindingFlags = global::System.Reflection.BindingFlags.Instance | global::System.Reflection.BindingFlags.Static | global::System.Reflection.BindingFlags.Public | global::System.Reflection.BindingFlags.NonPublic | global::System.Reflection.BindingFlags.FlattenHierarchy;

		private static readonly object[] EmptyObjects = new object[0];

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::System.Type targetType
		{
			get
			{
				return _targetType;
			}
			private set
			{
				if (!(value == targetType))
				{
					isReflected = false;
					_targetType = value;
					if (value == null)
					{
						_targetTypeName = null;
					}
					else
					{
						_targetTypeName = global::Unity.VisualScripting.RuntimeCodebase.SerializeType(value);
					}
				}
			}
		}

		[global::Unity.VisualScripting.DoNotSerialize]
		public string targetTypeName => _targetTypeName;

		[global::Unity.VisualScripting.DoNotSerialize]
		public string name
		{
			get
			{
				return _name;
			}
			private set
			{
				if (value != name)
				{
					isReflected = false;
				}
				_name = value;
			}
		}

		[global::Unity.VisualScripting.DoNotSerialize]
		public bool isReflected { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.Member.Source source
		{
			get
			{
				EnsureReflected();
				return _source;
			}
			private set
			{
				_source = value;
			}
		}

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::System.Reflection.FieldInfo fieldInfo
		{
			get
			{
				EnsureReflected();
				return _fieldInfo;
			}
			private set
			{
				_fieldInfo = value;
			}
		}

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::System.Reflection.PropertyInfo propertyInfo
		{
			get
			{
				EnsureReflected();
				return _propertyInfo;
			}
			private set
			{
				_propertyInfo = value;
			}
		}

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::System.Reflection.MethodInfo methodInfo
		{
			get
			{
				EnsureReflected();
				return _methodInfo;
			}
			private set
			{
				_methodInfo = value;
			}
		}

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::System.Reflection.ConstructorInfo constructorInfo
		{
			get
			{
				EnsureReflected();
				return _constructorInfo;
			}
			private set
			{
				_constructorInfo = value;
			}
		}

		[global::Unity.VisualScripting.DoNotSerialize]
		public bool isExtension
		{
			get
			{
				EnsureReflected();
				return _isExtension;
			}
			private set
			{
				_isExtension = value;
			}
		}

		[global::Unity.VisualScripting.DoNotSerialize]
		public bool isInvokedAsExtension
		{
			get
			{
				EnsureReflected();
				return _isInvokedAsExtension;
			}
			private set
			{
				_isInvokedAsExtension = value;
			}
		}

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::System.Type[] parameterTypes
		{
			get
			{
				return _parameterTypes;
			}
			private set
			{
				_parameterTypes = value;
				isReflected = false;
			}
		}

		public global::System.Reflection.MethodBase methodBase => source switch
		{
			global::Unity.VisualScripting.Member.Source.Method => methodInfo, 
			global::Unity.VisualScripting.Member.Source.Constructor => constructorInfo, 
			_ => null, 
		};

		private global::System.Reflection.MemberInfo _info => source switch
		{
			global::Unity.VisualScripting.Member.Source.Field => _fieldInfo, 
			global::Unity.VisualScripting.Member.Source.Property => _propertyInfo, 
			global::Unity.VisualScripting.Member.Source.Method => _methodInfo, 
			global::Unity.VisualScripting.Member.Source.Constructor => _constructorInfo, 
			_ => throw new global::Unity.VisualScripting.UnexpectedEnumValueException<global::Unity.VisualScripting.Member.Source>(source), 
		};

		public global::System.Reflection.MemberInfo info => source switch
		{
			global::Unity.VisualScripting.Member.Source.Field => fieldInfo, 
			global::Unity.VisualScripting.Member.Source.Property => propertyInfo, 
			global::Unity.VisualScripting.Member.Source.Method => methodInfo, 
			global::Unity.VisualScripting.Member.Source.Constructor => constructorInfo, 
			_ => throw new global::Unity.VisualScripting.UnexpectedEnumValueException<global::Unity.VisualScripting.Member.Source>(source), 
		};

		public global::System.Type type => source switch
		{
			global::Unity.VisualScripting.Member.Source.Field => fieldInfo.FieldType, 
			global::Unity.VisualScripting.Member.Source.Property => propertyInfo.PropertyType, 
			global::Unity.VisualScripting.Member.Source.Method => methodInfo.ReturnType, 
			global::Unity.VisualScripting.Member.Source.Constructor => constructorInfo.DeclaringType, 
			_ => throw new global::Unity.VisualScripting.UnexpectedEnumValueException<global::Unity.VisualScripting.Member.Source>(source), 
		};

		public bool isCoroutine
		{
			get
			{
				if (!isGettable)
				{
					return false;
				}
				return type == typeof(global::System.Collections.IEnumerator);
			}
		}

		public bool isYieldInstruction
		{
			get
			{
				if (!isGettable)
				{
					return false;
				}
				return typeof(global::UnityEngine.YieldInstruction).IsAssignableFrom(type);
			}
		}

		public bool isGettable => IsGettable(nonPublic: true);

		public bool isPubliclyGettable => IsGettable(nonPublic: false);

		public bool isSettable => IsSettable(nonPublic: true);

		public bool isPubliclySettable => IsSettable(nonPublic: false);

		public bool isInvocable => IsInvocable(nonPublic: true);

		public bool isPubliclyInvocable => IsInvocable(nonPublic: false);

		public bool isAccessor => source switch
		{
			global::Unity.VisualScripting.Member.Source.Field => true, 
			global::Unity.VisualScripting.Member.Source.Property => true, 
			global::Unity.VisualScripting.Member.Source.Method => false, 
			global::Unity.VisualScripting.Member.Source.Constructor => false, 
			_ => throw new global::Unity.VisualScripting.UnexpectedEnumValueException<global::Unity.VisualScripting.Member.Source>(source), 
		};

		public bool isField => source == global::Unity.VisualScripting.Member.Source.Field;

		public bool isProperty => source == global::Unity.VisualScripting.Member.Source.Property;

		public bool isMethod => source == global::Unity.VisualScripting.Member.Source.Method;

		public bool isConstructor => source == global::Unity.VisualScripting.Member.Source.Constructor;

		public bool requiresTarget
		{
			get
			{
				switch (source)
				{
				case global::Unity.VisualScripting.Member.Source.Field:
					return !fieldInfo.IsStatic;
				case global::Unity.VisualScripting.Member.Source.Property:
					return !(propertyInfo.GetGetMethod(nonPublic: true) ?? propertyInfo.GetSetMethod(nonPublic: true)).IsStatic;
				case global::Unity.VisualScripting.Member.Source.Method:
					if (methodInfo.IsStatic)
					{
						return isInvokedAsExtension;
					}
					return true;
				case global::Unity.VisualScripting.Member.Source.Constructor:
					return false;
				default:
					throw new global::Unity.VisualScripting.UnexpectedEnumValueException<global::Unity.VisualScripting.Member.Source>(source);
				}
			}
		}

		public bool isOperator
		{
			get
			{
				if (isMethod)
				{
					return methodInfo.IsOperator();
				}
				return false;
			}
		}

		public bool isConversion
		{
			get
			{
				if (isMethod)
				{
					return methodInfo.IsUserDefinedConversion();
				}
				return false;
			}
		}

		public int order => info.MetadataToken;

		public global::System.Type declaringType => info.ExtendedDeclaringType(isInvokedAsExtension);

		public bool isInherited => targetType != declaringType;

		public global::System.Type pseudoDeclaringType
		{
			get
			{
				global::System.Type type = declaringType;
				if (typeof(global::UnityEngine.Object).IsAssignableFrom(targetType))
				{
					if (targetType == typeof(global::UnityEngine.GameObject) || targetType == typeof(global::UnityEngine.Component) || targetType == typeof(global::UnityEngine.ScriptableObject))
					{
						return targetType;
					}
					if (type != typeof(global::UnityEngine.Object) && type != typeof(global::UnityEngine.GameObject) && type != typeof(global::UnityEngine.Component) && type != typeof(global::UnityEngine.MonoBehaviour) && type != typeof(global::UnityEngine.ScriptableObject) && type != typeof(object))
					{
						return targetType;
					}
				}
				return type;
			}
		}

		public bool isPseudoInherited
		{
			get
			{
				if (!(targetType != pseudoDeclaringType))
				{
					if (isMethod)
					{
						return methodInfo.IsGenericExtension();
					}
					return false;
				}
				return true;
			}
		}

		public bool isIndexer
		{
			get
			{
				if (isProperty)
				{
					return propertyInfo.GetIndexParameters().Length != 0;
				}
				return false;
			}
		}

		public bool isPredictable
		{
			get
			{
				if (!isField)
				{
					return info.HasAttribute<global::Unity.VisualScripting.PredictableAttribute>();
				}
				return true;
			}
		}

		public bool allowsNull
		{
			get
			{
				if (isSettable)
				{
					if (!type.IsReferenceType() || !info.HasAttribute<global::Unity.VisualScripting.AllowsNullAttribute>())
					{
						return global::System.Nullable.GetUnderlyingType(type) != null;
					}
					return true;
				}
				return false;
			}
		}

		[global::System.Obsolete("This parameterless constructor is only made public for serialization. Use another constructor instead.")]
		public Member()
		{
		}

		public Member(global::System.Type targetType, string name, global::System.Type[] parameterTypes = null)
		{
			global::Unity.VisualScripting.Ensure.That("targetType").IsNotNull(targetType);
			global::Unity.VisualScripting.Ensure.That("name").IsNotNull(name);
			if (parameterTypes != null)
			{
				for (int i = 0; i < parameterTypes.Length; i++)
				{
					if (parameterTypes[i] == null)
					{
						throw new global::System.ArgumentNullException("parameterTypes" + $"[{i}]");
					}
				}
			}
			this.targetType = targetType;
			this.name = name;
			this.parameterTypes = parameterTypes;
		}

		public Member(global::System.Type targetType, global::System.Reflection.FieldInfo fieldInfo)
		{
			global::Unity.VisualScripting.Ensure.That("targetType").IsNotNull(targetType);
			global::Unity.VisualScripting.Ensure.That("fieldInfo").IsNotNull(fieldInfo);
			source = global::Unity.VisualScripting.Member.Source.Field;
			this.fieldInfo = fieldInfo;
			this.targetType = targetType;
			name = fieldInfo.Name;
			parameterTypes = null;
			isReflected = true;
		}

		public Member(global::System.Type targetType, global::System.Reflection.PropertyInfo propertyInfo)
		{
			global::Unity.VisualScripting.Ensure.That("targetType").IsNotNull(targetType);
			global::Unity.VisualScripting.Ensure.That("propertyInfo").IsNotNull(propertyInfo);
			source = global::Unity.VisualScripting.Member.Source.Property;
			this.propertyInfo = propertyInfo;
			this.targetType = targetType;
			name = propertyInfo.Name;
			parameterTypes = null;
			isReflected = true;
		}

		public Member(global::System.Type targetType, global::System.Reflection.MethodInfo methodInfo)
		{
			global::Unity.VisualScripting.Ensure.That("targetType").IsNotNull(targetType);
			global::Unity.VisualScripting.Ensure.That("methodInfo").IsNotNull(methodInfo);
			source = global::Unity.VisualScripting.Member.Source.Method;
			this.methodInfo = methodInfo;
			this.targetType = targetType;
			name = methodInfo.Name;
			isExtension = methodInfo.IsExtension();
			isInvokedAsExtension = methodInfo.IsInvokedAsExtension(targetType);
			parameterTypes = global::System.Linq.Enumerable.ToArray(global::System.Linq.Enumerable.Select(methodInfo.GetInvocationParameters(_isInvokedAsExtension), (global::System.Reflection.ParameterInfo pi) => pi.ParameterType));
			isReflected = true;
		}

		public Member(global::System.Type targetType, global::System.Reflection.ConstructorInfo constructorInfo)
		{
			global::Unity.VisualScripting.Ensure.That("targetType").IsNotNull(targetType);
			global::Unity.VisualScripting.Ensure.That("constructorInfo").IsNotNull(constructorInfo);
			source = global::Unity.VisualScripting.Member.Source.Constructor;
			this.constructorInfo = constructorInfo;
			this.targetType = targetType;
			name = constructorInfo.Name;
			parameterTypes = global::System.Linq.Enumerable.ToArray(global::System.Linq.Enumerable.Select(constructorInfo.GetParameters(), (global::System.Reflection.ParameterInfo pi) => pi.ParameterType));
			isReflected = true;
		}

		void global::UnityEngine.ISerializationCallbackReceiver.OnBeforeSerialize()
		{
		}

		void global::UnityEngine.ISerializationCallbackReceiver.OnAfterDeserialize()
		{
			if (targetType != null)
			{
				_targetTypeName = global::Unity.VisualScripting.RuntimeCodebase.SerializeType(targetType);
			}
			else if (_targetTypeName != null)
			{
				try
				{
					targetType = global::Unity.VisualScripting.RuntimeCodebase.DeserializeType(_targetTypeName);
				}
				catch
				{
				}
			}
		}

		public bool IsGettable(bool nonPublic)
		{
			switch (source)
			{
			case global::Unity.VisualScripting.Member.Source.Field:
				if (!nonPublic)
				{
					return fieldInfo.IsPublic;
				}
				return true;
			case global::Unity.VisualScripting.Member.Source.Property:
				if (propertyInfo.CanRead)
				{
					if (!nonPublic)
					{
						return propertyInfo.GetGetMethod(nonPublic: false) != null;
					}
					return true;
				}
				return false;
			case global::Unity.VisualScripting.Member.Source.Method:
				if (methodInfo.ReturnType != typeof(void))
				{
					if (!nonPublic)
					{
						return methodInfo.IsPublic;
					}
					return true;
				}
				return false;
			case global::Unity.VisualScripting.Member.Source.Constructor:
				if (!nonPublic)
				{
					return constructorInfo.IsPublic;
				}
				return true;
			default:
				throw new global::Unity.VisualScripting.UnexpectedEnumValueException<global::Unity.VisualScripting.Member.Source>(source);
			}
		}

		public bool IsSettable(bool nonPublic)
		{
			switch (source)
			{
			case global::Unity.VisualScripting.Member.Source.Field:
				if (!fieldInfo.IsLiteral && !fieldInfo.IsInitOnly)
				{
					if (!nonPublic)
					{
						return fieldInfo.IsPublic;
					}
					return true;
				}
				return false;
			case global::Unity.VisualScripting.Member.Source.Property:
				if (propertyInfo.CanWrite)
				{
					if (!nonPublic)
					{
						return propertyInfo.GetSetMethod(nonPublic: false) != null;
					}
					return true;
				}
				return false;
			case global::Unity.VisualScripting.Member.Source.Method:
				return false;
			case global::Unity.VisualScripting.Member.Source.Constructor:
				return false;
			default:
				throw new global::Unity.VisualScripting.UnexpectedEnumValueException<global::Unity.VisualScripting.Member.Source>(source);
			}
		}

		public bool IsInvocable(bool nonPublic)
		{
			switch (source)
			{
			case global::Unity.VisualScripting.Member.Source.Field:
				return false;
			case global::Unity.VisualScripting.Member.Source.Property:
				return false;
			case global::Unity.VisualScripting.Member.Source.Method:
				if (!nonPublic)
				{
					return methodInfo.IsPublic;
				}
				return true;
			case global::Unity.VisualScripting.Member.Source.Constructor:
				if (!nonPublic)
				{
					return constructorInfo.IsPublic;
				}
				return true;
			default:
				throw new global::Unity.VisualScripting.UnexpectedEnumValueException<global::Unity.VisualScripting.Member.Source>(source);
			}
		}

		private void EnsureExplicitParameterTypes()
		{
			if (parameterTypes == null)
			{
				throw new global::System.InvalidOperationException("Missing parameter types.");
			}
		}

		public void Reflect()
		{
			if (targetType == null)
			{
				if (targetTypeName != null)
				{
					throw new global::System.MissingMemberException(targetTypeName, name);
				}
				throw new global::System.MissingMemberException("Target type not found.");
			}
			_source = global::Unity.VisualScripting.Member.Source.Unknown;
			_fieldInfo = null;
			_propertyInfo = null;
			_methodInfo = null;
			_constructorInfo = null;
			fieldAccessor = null;
			propertyAccessor = null;
			methodInvoker = null;
			global::System.Reflection.MemberInfo[] extendedMember;
			try
			{
				extendedMember = targetType.GetExtendedMember(name, global::System.Reflection.MemberTypes.Constructor | global::System.Reflection.MemberTypes.Field | global::System.Reflection.MemberTypes.Method | global::System.Reflection.MemberTypes.Property, global::System.Reflection.BindingFlags.Instance | global::System.Reflection.BindingFlags.Static | global::System.Reflection.BindingFlags.Public | global::System.Reflection.BindingFlags.NonPublic | global::System.Reflection.BindingFlags.FlattenHierarchy);
			}
			catch (global::System.NotSupportedException innerException)
			{
				throw new global::System.InvalidOperationException($"An error occured when trying to reflect the member '{name}' of the type '{targetType.FullName}' in a '{GetType().Name}' unit. Supported member types: {(global::System.Reflection.MemberTypes.Constructor | global::System.Reflection.MemberTypes.Field | global::System.Reflection.MemberTypes.Method | global::System.Reflection.MemberTypes.Property)}, supported binding flags: {(global::System.Reflection.BindingFlags.Instance | global::System.Reflection.BindingFlags.Static | global::System.Reflection.BindingFlags.Public | global::System.Reflection.BindingFlags.NonPublic | global::System.Reflection.BindingFlags.FlattenHierarchy)}", innerException);
			}
			if (extendedMember.Length == 0 && global::Unity.VisualScripting.RuntimeCodebase.RenamedMembers(targetType).TryGetValue(name, out var value))
			{
				name = value;
				try
				{
					extendedMember = targetType.GetExtendedMember(name, global::System.Reflection.MemberTypes.Constructor | global::System.Reflection.MemberTypes.Field | global::System.Reflection.MemberTypes.Method | global::System.Reflection.MemberTypes.Property, global::System.Reflection.BindingFlags.Instance | global::System.Reflection.BindingFlags.Static | global::System.Reflection.BindingFlags.Public | global::System.Reflection.BindingFlags.NonPublic | global::System.Reflection.BindingFlags.FlattenHierarchy);
				}
				catch (global::System.NotSupportedException innerException2)
				{
					throw new global::System.InvalidOperationException($"An error occured when trying to reflect the renamed member '{name}' of the type '{targetType.FullName}' in a '{GetType().Name}' unit. Supported member types: {(global::System.Reflection.MemberTypes.Constructor | global::System.Reflection.MemberTypes.Field | global::System.Reflection.MemberTypes.Method | global::System.Reflection.MemberTypes.Property)}, supported binding flags: {(global::System.Reflection.BindingFlags.Instance | global::System.Reflection.BindingFlags.Static | global::System.Reflection.BindingFlags.Public | global::System.Reflection.BindingFlags.NonPublic | global::System.Reflection.BindingFlags.FlattenHierarchy)}", innerException2);
				}
			}
			if (extendedMember.Length == 0)
			{
				throw new global::System.MissingMemberException("No matching member found: '" + targetType.Name + "." + name + "'");
			}
			global::System.Reflection.MemberTypes? memberTypes = null;
			global::System.Reflection.MemberInfo[] array = extendedMember;
			foreach (global::System.Reflection.MemberInfo memberInfo in array)
			{
				if (!memberTypes.HasValue)
				{
					memberTypes = memberInfo.MemberType;
				}
				else if (memberInfo.MemberType != memberTypes && !memberInfo.IsExtensionMethod())
				{
					global::UnityEngine.Debug.LogWarning("Multiple members with the same name are of a different type: '" + targetType.Name + "." + name + "'");
					break;
				}
			}
			switch (memberTypes)
			{
			case global::System.Reflection.MemberTypes.Field:
				ReflectField(extendedMember);
				break;
			case global::System.Reflection.MemberTypes.Property:
				ReflectProperty(extendedMember);
				break;
			case global::System.Reflection.MemberTypes.Method:
				ReflectMethod(extendedMember);
				break;
			case global::System.Reflection.MemberTypes.Constructor:
				ReflectConstructor(extendedMember);
				break;
			default:
				throw new global::Unity.VisualScripting.UnexpectedEnumValueException<global::System.Reflection.MemberTypes>(memberTypes.Value);
			}
			isReflected = true;
		}

		private void ReflectField(global::System.Collections.Generic.IEnumerable<global::System.Reflection.MemberInfo> candidates)
		{
			_source = global::Unity.VisualScripting.Member.Source.Field;
			_fieldInfo = global::System.Linq.Enumerable.OfType<global::System.Reflection.FieldInfo>(candidates).Disambiguate(targetType);
			if (_fieldInfo == null)
			{
				throw new global::System.MissingMemberException("No matching field found: '" + targetType.Name + "." + name + "'");
			}
		}

		private void ReflectProperty(global::System.Collections.Generic.IEnumerable<global::System.Reflection.MemberInfo> candidates)
		{
			_source = global::Unity.VisualScripting.Member.Source.Property;
			_propertyInfo = global::System.Linq.Enumerable.OfType<global::System.Reflection.PropertyInfo>(candidates).Disambiguate(targetType);
			if (_propertyInfo == null)
			{
				throw new global::System.MissingMemberException("No matching property found: '" + targetType.Name + "." + name + "'");
			}
		}

		private void ReflectConstructor(global::System.Collections.Generic.IEnumerable<global::System.Reflection.MemberInfo> candidates)
		{
			_source = global::Unity.VisualScripting.Member.Source.Constructor;
			EnsureExplicitParameterTypes();
			_constructorInfo = global::System.Linq.Enumerable.Where(global::System.Linq.Enumerable.OfType<global::System.Reflection.ConstructorInfo>(candidates), (global::System.Reflection.ConstructorInfo c) => !c.IsStatic).Disambiguate(targetType, parameterTypes);
			if (_constructorInfo == null)
			{
				throw new global::System.MissingMemberException("No matching constructor found: '" + targetType.Name + " (" + global::System.Linq.Enumerable.Select(parameterTypes, (global::System.Type t) => t.Name).ToCommaSeparatedString() + ")'");
			}
		}

		private void ReflectMethod(global::System.Collections.Generic.IEnumerable<global::System.Reflection.MemberInfo> candidates)
		{
			_source = global::Unity.VisualScripting.Member.Source.Method;
			EnsureExplicitParameterTypes();
			_methodInfo = global::System.Linq.Enumerable.OfType<global::System.Reflection.MethodInfo>(candidates).Disambiguate(targetType, parameterTypes);
			if (_methodInfo == null)
			{
				throw new global::System.MissingMemberException("No matching method found: '" + targetType.Name + "." + name + " (" + global::System.Linq.Enumerable.Select(parameterTypes, (global::System.Type t) => t.Name).ToCommaSeparatedString() + ")'\nCandidates:\n" + candidates.ToLineSeparatedString());
			}
			_isExtension = _methodInfo.IsExtension();
			_isInvokedAsExtension = _methodInfo.IsInvokedAsExtension(targetType);
		}

		public void Prewarm()
		{
			if (fieldAccessor == null)
			{
				fieldAccessor = fieldInfo?.Prewarm();
			}
			if (propertyAccessor == null)
			{
				propertyAccessor = propertyInfo?.Prewarm();
			}
			if (methodInvoker == null)
			{
				methodInvoker = methodInfo?.Prewarm();
			}
		}

		public void EnsureReflected()
		{
			if (!isReflected)
			{
				Reflect();
			}
		}

		public void EnsureReady(object target)
		{
			EnsureReflected();
			if (target == null && requiresTarget)
			{
				throw new global::System.InvalidOperationException($"Missing target object for '{targetType}.{name}'.");
			}
			if (target != null && !requiresTarget)
			{
				throw new global::System.InvalidOperationException($"Superfluous target object for '{targetType}.{name}'.");
			}
		}

		public object Get(object target)
		{
			EnsureReady(target);
			switch (source)
			{
			case global::Unity.VisualScripting.Member.Source.Field:
				if (fieldAccessor == null)
				{
					fieldAccessor = fieldInfo.Prewarm();
				}
				return fieldAccessor.GetValue(target);
			case global::Unity.VisualScripting.Member.Source.Property:
				if (propertyAccessor == null)
				{
					propertyAccessor = propertyInfo.Prewarm();
				}
				return propertyAccessor.GetValue(target);
			case global::Unity.VisualScripting.Member.Source.Method:
				throw new global::System.NotSupportedException("Member is a method. Consider using 'Invoke' instead.");
			case global::Unity.VisualScripting.Member.Source.Constructor:
				throw new global::System.NotSupportedException("Member is a constructor. Consider using 'Invoke' instead.");
			default:
				throw new global::Unity.VisualScripting.UnexpectedEnumValueException<global::Unity.VisualScripting.Member.Source>(source);
			}
		}

		public T Get<T>(object target)
		{
			return (T)Get(target);
		}

		public object Set(object target, object value)
		{
			EnsureReady(target);
			switch (source)
			{
			case global::Unity.VisualScripting.Member.Source.Field:
				if (fieldAccessor == null)
				{
					fieldAccessor = fieldInfo.Prewarm();
				}
				fieldAccessor.SetValue(target, value);
				return value;
			case global::Unity.VisualScripting.Member.Source.Property:
				if (propertyAccessor == null)
				{
					propertyAccessor = propertyInfo.Prewarm();
				}
				propertyAccessor.SetValue(target, value);
				return value;
			case global::Unity.VisualScripting.Member.Source.Method:
				throw new global::System.NotSupportedException("Member is a method.");
			case global::Unity.VisualScripting.Member.Source.Constructor:
				throw new global::System.NotSupportedException("Member is a constructor.");
			default:
				throw new global::Unity.VisualScripting.UnexpectedEnumValueException<global::Unity.VisualScripting.Member.Source>(source);
			}
		}

		private void EnsureInvocable(object target)
		{
			EnsureReady(target);
			if (source == global::Unity.VisualScripting.Member.Source.Field || source == global::Unity.VisualScripting.Member.Source.Property)
			{
				throw new global::System.NotSupportedException("Member is a field or property.");
			}
			if (source == global::Unity.VisualScripting.Member.Source.Method)
			{
				if (methodInfo.ContainsGenericParameters)
				{
					throw new global::System.NotSupportedException($"Trying to invoke an open-constructed generic method: '{methodInfo}'.");
				}
				if (methodInvoker == null)
				{
					methodInvoker = methodInfo.Prewarm();
				}
			}
			else
			{
				if (source != global::Unity.VisualScripting.Member.Source.Constructor)
				{
					throw new global::Unity.VisualScripting.UnexpectedEnumValueException<global::Unity.VisualScripting.Member.Source>(source);
				}
				if (constructorInfo.ContainsGenericParameters)
				{
					throw new global::System.NotSupportedException($"Trying to invoke an open-constructed generic constructor: '{constructorInfo}'.");
				}
			}
		}

		public global::System.Collections.Generic.IEnumerable<global::System.Reflection.ParameterInfo> GetParameterInfos()
		{
			EnsureReflected();
			return methodBase.GetInvocationParameters(isInvokedAsExtension);
		}

		public object Invoke(object target)
		{
			EnsureInvocable(target);
			if (source == global::Unity.VisualScripting.Member.Source.Method)
			{
				if (isInvokedAsExtension)
				{
					return methodInvoker.Invoke(null, target);
				}
				return methodInvoker.Invoke(target);
			}
			return constructorInfo.Invoke(EmptyObjects);
		}

		public object Invoke(object target, object arg0)
		{
			EnsureInvocable(target);
			if (source == global::Unity.VisualScripting.Member.Source.Method)
			{
				if (isInvokedAsExtension)
				{
					return methodInvoker.Invoke(null, target, arg0);
				}
				return methodInvoker.Invoke(target, arg0);
			}
			return constructorInfo.Invoke(new object[1] { arg0 });
		}

		public object Invoke(object target, object arg0, object arg1)
		{
			EnsureInvocable(target);
			if (source == global::Unity.VisualScripting.Member.Source.Method)
			{
				if (isInvokedAsExtension)
				{
					return methodInvoker.Invoke(null, target, arg0, arg1);
				}
				return methodInvoker.Invoke(target, arg0, arg1);
			}
			return constructorInfo.Invoke(new object[2] { arg0, arg1 });
		}

		public object Invoke(object target, object arg0, object arg1, object arg2)
		{
			EnsureInvocable(target);
			if (source == global::Unity.VisualScripting.Member.Source.Method)
			{
				if (isInvokedAsExtension)
				{
					return methodInvoker.Invoke(null, target, arg0, arg1, arg2);
				}
				return methodInvoker.Invoke(target, arg0, arg1, arg2);
			}
			return constructorInfo.Invoke(new object[3] { arg0, arg1, arg2 });
		}

		public object Invoke(object target, object arg0, object arg1, object arg2, object arg3)
		{
			EnsureInvocable(target);
			if (source == global::Unity.VisualScripting.Member.Source.Method)
			{
				if (isInvokedAsExtension)
				{
					return methodInvoker.Invoke(null, target, arg0, arg1, arg2, arg3);
				}
				return methodInvoker.Invoke(target, arg0, arg1, arg2, arg3);
			}
			return constructorInfo.Invoke(new object[4] { arg0, arg1, arg2, arg3 });
		}

		public object Invoke(object target, object arg0, object arg1, object arg2, object arg3, object arg4)
		{
			EnsureInvocable(target);
			if (source == global::Unity.VisualScripting.Member.Source.Method)
			{
				if (isInvokedAsExtension)
				{
					return methodInvoker.Invoke(null, target, arg0, arg1, arg2, arg3, arg4);
				}
				return methodInvoker.Invoke(target, arg0, arg1, arg2, arg3, arg4);
			}
			return constructorInfo.Invoke(new object[5] { arg0, arg1, arg2, arg3, arg4 });
		}

		public object Invoke(object target, params object[] arguments)
		{
			EnsureInvocable(target);
			if (source == global::Unity.VisualScripting.Member.Source.Method)
			{
				if (isInvokedAsExtension)
				{
					object[] array = new object[arguments.Length + 1];
					array[0] = target;
					global::System.Array.Copy(arguments, 0, array, 1, arguments.Length);
					return methodInvoker.Invoke(null, array);
				}
				return methodInvoker.Invoke(target, arguments);
			}
			return constructorInfo.Invoke(arguments);
		}

		public T Invoke<T>(object target)
		{
			return (T)Invoke(target);
		}

		public T Invoke<T>(object target, object arg0)
		{
			return (T)Invoke(target, arg0);
		}

		public T Invoke<T>(object target, object arg0, object arg1)
		{
			return (T)Invoke(target, arg0, arg1);
		}

		public T Invoke<T>(object target, object arg0, object arg1, object arg2)
		{
			return (T)Invoke(target, arg0, arg1, arg2);
		}

		public T Invoke<T>(object target, object arg0, object arg1, object arg2, object arg3)
		{
			return (T)Invoke(target, arg0, arg1, arg2, arg3);
		}

		public T Invoke<T>(object target, object arg0, object arg1, object arg2, object arg3, object arg4)
		{
			return (T)Invoke(target, arg0, arg1, arg2, arg3, arg4);
		}

		public T Invoke<T>(object target, params object[] arguments)
		{
			return (T)Invoke(target, arguments);
		}

		public override bool Equals(object obj)
		{
			global::Unity.VisualScripting.Member member = obj as global::Unity.VisualScripting.Member;
			if (!(member != null) || !(targetType == member.targetType) || !(name == member.name))
			{
				return false;
			}
			bool flag = parameterTypes != null;
			bool flag2 = member.parameterTypes != null;
			if (flag != flag2)
			{
				return false;
			}
			if (flag)
			{
				int num = parameterTypes.Length;
				int num2 = member.parameterTypes.Length;
				if (num != num2)
				{
					return false;
				}
				for (int i = 0; i < num; i++)
				{
					if (parameterTypes[i] != member.parameterTypes[i])
					{
						return false;
					}
				}
			}
			return true;
		}

		public override int GetHashCode()
		{
			int num = 17;
			num = num * 23 + (targetType?.GetHashCode() ?? 0);
			num = num * 23 + (name?.GetHashCode() ?? 0);
			if (parameterTypes != null)
			{
				global::System.Type[] array = parameterTypes;
				foreach (global::System.Type type in array)
				{
					num = num * 23 + type.GetHashCode();
				}
			}
			else
			{
				num *= 23;
			}
			return num;
		}

		public static bool operator ==(global::Unity.VisualScripting.Member a, global::Unity.VisualScripting.Member b)
		{
			if ((object)a == b)
			{
				return true;
			}
			if ((object)a == null || (object)b == null)
			{
				return false;
			}
			return a.Equals(b);
		}

		public static bool operator !=(global::Unity.VisualScripting.Member a, global::Unity.VisualScripting.Member b)
		{
			return !(a == b);
		}

		public string ToUniqueString()
		{
			string text = targetType.FullName + "." + name;
			if (parameterTypes != null)
			{
				text += "(";
				global::System.Type[] array = parameterTypes;
				foreach (global::System.Type type in array)
				{
					text += type.FullName;
				}
				text += ")";
			}
			return text;
		}

		public override string ToString()
		{
			return targetType.CSharpName() + "." + name;
		}

		public global::Unity.VisualScripting.Member ToDeclarer()
		{
			return new global::Unity.VisualScripting.Member(declaringType, name, parameterTypes);
		}

		public global::Unity.VisualScripting.Member ToPseudoDeclarer()
		{
			return new global::Unity.VisualScripting.Member(pseudoDeclaringType, name, parameterTypes);
		}
	}
}
