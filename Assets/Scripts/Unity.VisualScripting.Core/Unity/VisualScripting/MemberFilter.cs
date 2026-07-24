namespace Unity.VisualScripting
{
	[global::System.AttributeUsage(global::System.AttributeTargets.Property | global::System.AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
	public sealed class MemberFilter : global::System.Attribute, global::System.ICloneable
	{
		public bool Fields { get; set; }

		public bool Properties { get; set; }

		public bool Methods { get; set; }

		public bool Constructors { get; set; }

		public bool Gettable { get; set; }

		public bool Settable { get; set; }

		public bool Inherited { get; set; }

		public bool Targeted { get; set; }

		public bool NonTargeted { get; set; }

		public bool Public { get; set; }

		public bool NonPublic { get; set; }

		public bool ReadOnly { get; set; }

		public bool WriteOnly { get; set; }

		public bool Extensions { get; set; }

		public bool Operators { get; set; }

		public bool Conversions { get; set; }

		public bool Setters { get; set; }

		public bool Parameters { get; set; }

		public bool Obsolete { get; set; }

		public bool OpenConstructedGeneric { get; set; }

		public bool TypeInitializers { get; set; }

		public bool ClsNonCompliant { get; set; }

		public global::System.Reflection.BindingFlags validBindingFlags
		{
			get
			{
				global::System.Reflection.BindingFlags bindingFlags = global::System.Reflection.BindingFlags.Default;
				if (Public)
				{
					bindingFlags |= global::System.Reflection.BindingFlags.Public;
				}
				if (NonPublic)
				{
					bindingFlags |= global::System.Reflection.BindingFlags.NonPublic;
				}
				if (Targeted || Constructors)
				{
					bindingFlags |= global::System.Reflection.BindingFlags.Instance;
				}
				if (NonTargeted)
				{
					bindingFlags |= global::System.Reflection.BindingFlags.Static;
				}
				if (!Inherited)
				{
					bindingFlags |= global::System.Reflection.BindingFlags.DeclaredOnly;
				}
				if (NonTargeted && Inherited)
				{
					bindingFlags |= global::System.Reflection.BindingFlags.FlattenHierarchy;
				}
				return bindingFlags;
			}
		}

		public global::System.Reflection.MemberTypes validMemberTypes
		{
			get
			{
				global::System.Reflection.MemberTypes memberTypes = (global::System.Reflection.MemberTypes)0;
				if (Fields || Gettable || Settable)
				{
					memberTypes |= global::System.Reflection.MemberTypes.Field;
				}
				if (Properties || Gettable || Settable)
				{
					memberTypes |= global::System.Reflection.MemberTypes.Property;
				}
				if (Methods || Gettable)
				{
					memberTypes |= global::System.Reflection.MemberTypes.Method;
				}
				if (Constructors || Gettable)
				{
					memberTypes |= global::System.Reflection.MemberTypes.Constructor;
				}
				return memberTypes;
			}
		}

		public static global::Unity.VisualScripting.MemberFilter Any => new global::Unity.VisualScripting.MemberFilter
		{
			Fields = true,
			Properties = true,
			Methods = true,
			Constructors = true
		};

		public MemberFilter()
		{
			Fields = false;
			Properties = false;
			Methods = false;
			Constructors = false;
			Gettable = false;
			Settable = false;
			Inherited = true;
			Targeted = true;
			NonTargeted = true;
			Public = true;
			NonPublic = false;
			ReadOnly = true;
			WriteOnly = true;
			Extensions = true;
			Operators = true;
			Conversions = true;
			Parameters = true;
			Obsolete = false;
			OpenConstructedGeneric = false;
			TypeInitializers = true;
			ClsNonCompliant = true;
		}

		object global::System.ICloneable.Clone()
		{
			return Clone();
		}

		public global::Unity.VisualScripting.MemberFilter Clone()
		{
			return new global::Unity.VisualScripting.MemberFilter
			{
				Fields = Fields,
				Properties = Properties,
				Methods = Methods,
				Constructors = Constructors,
				Gettable = Gettable,
				Settable = Settable,
				Inherited = Inherited,
				Targeted = Targeted,
				NonTargeted = NonTargeted,
				Public = Public,
				NonPublic = NonPublic,
				ReadOnly = ReadOnly,
				WriteOnly = WriteOnly,
				Extensions = Extensions,
				Operators = Operators,
				Conversions = Conversions,
				Parameters = Parameters,
				Obsolete = Obsolete,
				OpenConstructedGeneric = OpenConstructedGeneric,
				TypeInitializers = TypeInitializers,
				ClsNonCompliant = ClsNonCompliant
			};
		}

		public override bool Equals(object obj)
		{
			if (!(obj is global::Unity.VisualScripting.MemberFilter memberFilter))
			{
				return false;
			}
			if (Fields == memberFilter.Fields && Properties == memberFilter.Properties && Methods == memberFilter.Methods && Constructors == memberFilter.Constructors && Gettable == memberFilter.Gettable && Settable == memberFilter.Settable && Inherited == memberFilter.Inherited && Targeted == memberFilter.Targeted && NonTargeted == memberFilter.NonTargeted && Public == memberFilter.Public && NonPublic == memberFilter.NonPublic && ReadOnly == memberFilter.ReadOnly && WriteOnly == memberFilter.WriteOnly && Extensions == memberFilter.Extensions && Operators == memberFilter.Operators && Conversions == memberFilter.Conversions && Parameters == memberFilter.Parameters && Obsolete == memberFilter.Obsolete && OpenConstructedGeneric == memberFilter.OpenConstructedGeneric && TypeInitializers == memberFilter.TypeInitializers)
			{
				return ClsNonCompliant == memberFilter.ClsNonCompliant;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return ((((((((((((((((((((17 * 23 + Fields.GetHashCode()) * 23 + Properties.GetHashCode()) * 23 + Methods.GetHashCode()) * 23 + Constructors.GetHashCode()) * 23 + Gettable.GetHashCode()) * 23 + Settable.GetHashCode()) * 23 + Inherited.GetHashCode()) * 23 + Targeted.GetHashCode()) * 23 + NonTargeted.GetHashCode()) * 23 + Public.GetHashCode()) * 23 + NonPublic.GetHashCode()) * 23 + ReadOnly.GetHashCode()) * 23 + WriteOnly.GetHashCode()) * 23 + Extensions.GetHashCode()) * 23 + Operators.GetHashCode()) * 23 + Conversions.GetHashCode()) * 23 + Parameters.GetHashCode()) * 23 + Obsolete.GetHashCode()) * 23 + OpenConstructedGeneric.GetHashCode()) * 23 + TypeInitializers.GetHashCode()) * 23 + ClsNonCompliant.GetHashCode();
		}

		public bool ValidateMember(global::System.Reflection.MemberInfo member, global::Unity.VisualScripting.TypeFilter typeFilter = null)
		{
			if (member is global::System.Reflection.FieldInfo)
			{
				global::System.Reflection.FieldInfo fieldInfo = (global::System.Reflection.FieldInfo)member;
				bool flag = true;
				bool flag2 = !fieldInfo.IsLiteral && !fieldInfo.IsInitOnly;
				if (!Fields && !(Gettable && flag) && !(Settable && flag2))
				{
					return false;
				}
				bool flag3 = !fieldInfo.IsStatic;
				if (!Targeted && flag3)
				{
					return false;
				}
				if (!NonTargeted && !flag3)
				{
					return false;
				}
				if (!WriteOnly && !flag)
				{
					return false;
				}
				if (!ReadOnly && !flag2)
				{
					return false;
				}
				if (!Public && fieldInfo.IsPublic)
				{
					return false;
				}
				if (!NonPublic && !fieldInfo.IsPublic)
				{
					return false;
				}
				if (typeFilter != null && !typeFilter.ValidateType(fieldInfo.FieldType))
				{
					return false;
				}
				if (fieldInfo.IsSpecialName)
				{
					return false;
				}
			}
			else if (member is global::System.Reflection.PropertyInfo)
			{
				global::System.Reflection.PropertyInfo propertyInfo = (global::System.Reflection.PropertyInfo)member;
				global::System.Reflection.MethodInfo getMethod = propertyInfo.GetGetMethod(nonPublic: true);
				global::System.Reflection.MethodInfo setMethod = propertyInfo.GetSetMethod(nonPublic: true);
				bool canRead = propertyInfo.CanRead;
				bool canWrite = propertyInfo.CanWrite;
				if (!Properties && !(Gettable && canRead) && !(Settable && canWrite))
				{
					return false;
				}
				bool num = !WriteOnly || (!Properties && Gettable);
				bool flag4 = !ReadOnly || (!Properties && Settable);
				bool flag5 = propertyInfo.CanRead && (NonPublic || getMethod.IsPublic);
				bool flag6 = propertyInfo.CanWrite && (NonPublic || setMethod.IsPublic);
				if (num && !flag5)
				{
					return false;
				}
				if (flag4 && !flag6)
				{
					return false;
				}
				bool flag7 = !(getMethod ?? setMethod).IsStatic;
				if (!Targeted && flag7)
				{
					return false;
				}
				if (!NonTargeted && !flag7)
				{
					return false;
				}
				if (typeFilter != null && !typeFilter.ValidateType(propertyInfo.PropertyType))
				{
					return false;
				}
				if (propertyInfo.IsSpecialName)
				{
					return false;
				}
				if (global::System.Linq.Enumerable.Any(propertyInfo.GetIndexParameters()))
				{
					return false;
				}
			}
			else if (member is global::System.Reflection.MethodBase)
			{
				global::System.Reflection.MethodBase methodBase = (global::System.Reflection.MethodBase)member;
				bool flag8 = methodBase.IsExtensionMethod();
				bool flag9 = !methodBase.IsStatic || flag8;
				if (!Public && methodBase.IsPublic)
				{
					return false;
				}
				if (!NonPublic && !methodBase.IsPublic)
				{
					return false;
				}
				if (!Parameters && methodBase.GetParameters().Length > (flag8 ? 1 : 0))
				{
					return false;
				}
				if (!OpenConstructedGeneric && methodBase.ContainsGenericParameters)
				{
					return false;
				}
				if (member is global::System.Reflection.MethodInfo)
				{
					global::System.Reflection.MethodInfo methodInfo = (global::System.Reflection.MethodInfo)member;
					bool flag10 = methodInfo.IsOperator();
					bool flag11 = methodInfo.IsUserDefinedConversion();
					bool flag12 = methodInfo.ReturnType != typeof(void);
					bool flag13 = false;
					if (!Methods && !(Gettable && flag12) && !(Settable && flag13))
					{
						return false;
					}
					if (!Targeted && flag9)
					{
						return false;
					}
					if (!NonTargeted && !flag9)
					{
						return false;
					}
					if (!Operators && flag10)
					{
						return false;
					}
					if (!Extensions && flag8)
					{
						return false;
					}
					if (typeFilter != null && !typeFilter.ValidateType(methodInfo.ReturnType))
					{
						return false;
					}
					if (methodInfo.IsSpecialName && !(flag10 || flag11))
					{
						return false;
					}
					if (flag12 && methodInfo.ReturnType.IsByRefLike)
					{
						return false;
					}
				}
				else if (member is global::System.Reflection.ConstructorInfo)
				{
					global::System.Reflection.ConstructorInfo constructorInfo = (global::System.Reflection.ConstructorInfo)member;
					bool flag14 = true;
					bool flag15 = false;
					if (!Constructors && !(Gettable && flag14) && !(Settable && flag15))
					{
						return false;
					}
					if (typeFilter != null && !typeFilter.ValidateType(constructorInfo.DeclaringType))
					{
						return false;
					}
					if (constructorInfo.IsStatic && !TypeInitializers)
					{
						return false;
					}
					if (typeof(global::UnityEngine.Component).IsAssignableFrom(member.DeclaringType) || typeof(global::UnityEngine.ScriptableObject).IsAssignableFrom(member.DeclaringType))
					{
						return false;
					}
				}
			}
			if (!Obsolete && member.HasAttribute<global::System.ObsoleteAttribute>(inherit: false))
			{
				return false;
			}
			if (!ClsNonCompliant)
			{
				global::System.CLSCompliantAttribute attribute = member.GetAttribute<global::System.CLSCompliantAttribute>();
				if (attribute != null && !attribute.IsCompliant)
				{
					return false;
				}
			}
			return true;
		}

		public override string ToString()
		{
			global::System.Text.StringBuilder stringBuilder = new global::System.Text.StringBuilder();
			stringBuilder.AppendLine($"Fields: {Fields}");
			stringBuilder.AppendLine($"Properties: {Properties}");
			stringBuilder.AppendLine($"Methods: {Methods}");
			stringBuilder.AppendLine($"Constructors: {Constructors}");
			stringBuilder.AppendLine($"Gettable: {Gettable}");
			stringBuilder.AppendLine($"Settable: {Settable}");
			stringBuilder.AppendLine();
			stringBuilder.AppendLine($"Inherited: {Inherited}");
			stringBuilder.AppendLine($"Instance: {Targeted}");
			stringBuilder.AppendLine($"Static: {NonTargeted}");
			stringBuilder.AppendLine($"Public: {Public}");
			stringBuilder.AppendLine($"NonPublic: {NonPublic}");
			stringBuilder.AppendLine($"ReadOnly: {ReadOnly}");
			stringBuilder.AppendLine($"WriteOnly: {WriteOnly}");
			stringBuilder.AppendLine($"Extensions: {Extensions}");
			stringBuilder.AppendLine($"Operators: {Operators}");
			stringBuilder.AppendLine($"Conversions: {Conversions}");
			stringBuilder.AppendLine($"Parameters: {Parameters}");
			stringBuilder.AppendLine($"Obsolete: {Obsolete}");
			stringBuilder.AppendLine($"OpenConstructedGeneric: {OpenConstructedGeneric}");
			stringBuilder.AppendLine($"TypeInitializers: {TypeInitializers}");
			stringBuilder.AppendLine($"ClsNonCompliant: {ClsNonCompliant}");
			return stringBuilder.ToString();
		}
	}
}
