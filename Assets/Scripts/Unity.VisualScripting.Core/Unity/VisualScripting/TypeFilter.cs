namespace Unity.VisualScripting
{
	[global::System.AttributeUsage(global::System.AttributeTargets.Property | global::System.AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
	public sealed class TypeFilter : global::System.Attribute, global::System.ICloneable
	{
		private readonly global::System.Collections.Generic.HashSet<global::System.Type> types;

		public global::Unity.VisualScripting.TypesMatching Matching { get; set; }

		public global::System.Collections.Generic.HashSet<global::System.Type> Types => types;

		public bool Value { get; set; }

		public bool Reference { get; set; }

		public bool Classes { get; set; }

		public bool Interfaces { get; set; }

		public bool Structs { get; set; }

		public bool Enums { get; set; }

		public bool Public { get; set; }

		public bool NonPublic { get; set; }

		public bool Abstract { get; set; }

		public bool Generic { get; set; }

		public bool OpenConstructedGeneric { get; set; }

		public bool Static { get; set; }

		public bool Sealed { get; set; }

		public bool Nested { get; set; }

		public bool Primitives { get; set; }

		public bool Object { get; set; }

		public bool NonSerializable { get; set; }

		public bool Obsolete { get; set; }

		public bool ExpectsBoolean
		{
			get
			{
				if (Types.Count == 1)
				{
					return global::System.Linq.Enumerable.Single(Types) == typeof(bool);
				}
				return false;
			}
		}

		public static global::Unity.VisualScripting.TypeFilter Any => new global::Unity.VisualScripting.TypeFilter();

		public TypeFilter(global::Unity.VisualScripting.TypesMatching matching, global::System.Collections.Generic.IEnumerable<global::System.Type> types)
		{
			global::Unity.VisualScripting.Ensure.That("types").IsNotNull(types);
			Matching = matching;
			this.types = new global::System.Collections.Generic.HashSet<global::System.Type>(types);
			Value = true;
			Reference = true;
			Classes = true;
			Interfaces = true;
			Structs = true;
			Enums = true;
			Public = true;
			NonPublic = false;
			Abstract = true;
			Generic = true;
			OpenConstructedGeneric = false;
			Static = true;
			Sealed = true;
			Nested = true;
			Primitives = true;
			Object = true;
			NonSerializable = true;
			Obsolete = false;
		}

		public TypeFilter(global::Unity.VisualScripting.TypesMatching matching, params global::System.Type[] types)
			: this(matching, (global::System.Collections.Generic.IEnumerable<global::System.Type>)types)
		{
		}

		public TypeFilter(global::System.Collections.Generic.IEnumerable<global::System.Type> types)
			: this(global::Unity.VisualScripting.TypesMatching.ConvertibleToAny, types)
		{
		}

		public TypeFilter(params global::System.Type[] types)
			: this(global::Unity.VisualScripting.TypesMatching.ConvertibleToAny, types)
		{
		}

		object global::System.ICloneable.Clone()
		{
			return Clone();
		}

		public global::Unity.VisualScripting.TypeFilter Clone()
		{
			return new global::Unity.VisualScripting.TypeFilter(Matching, global::System.Linq.Enumerable.ToArray(Types))
			{
				Value = Value,
				Reference = Reference,
				Classes = Classes,
				Interfaces = Interfaces,
				Structs = Structs,
				Enums = Enums,
				Public = Public,
				NonPublic = NonPublic,
				Abstract = Abstract,
				Generic = Generic,
				OpenConstructedGeneric = OpenConstructedGeneric,
				Static = Static,
				Sealed = Sealed,
				Nested = Nested,
				Primitives = Primitives,
				Object = Object,
				NonSerializable = NonSerializable,
				Obsolete = Obsolete
			};
		}

		public override bool Equals(object obj)
		{
			if (!(obj is global::Unity.VisualScripting.TypeFilter typeFilter))
			{
				return false;
			}
			if (Matching == typeFilter.Matching && types.SetEquals(typeFilter.types) && Value == typeFilter.Value && Reference == typeFilter.Reference && Classes == typeFilter.Classes && Interfaces == typeFilter.Interfaces && Structs == typeFilter.Structs && Enums == typeFilter.Enums && Public == typeFilter.Public && NonPublic == typeFilter.NonPublic && Abstract == typeFilter.Abstract && Generic == typeFilter.Generic && OpenConstructedGeneric == typeFilter.OpenConstructedGeneric && Static == typeFilter.Static && Sealed == typeFilter.Sealed && Nested == typeFilter.Nested && Primitives == typeFilter.Primitives && Object == typeFilter.Object && NonSerializable == typeFilter.NonSerializable)
			{
				return Obsolete == typeFilter.Obsolete;
			}
			return false;
		}

		public override int GetHashCode()
		{
			int num = 17;
			num = num * 23 + Matching.GetHashCode();
			foreach (global::System.Type type in types)
			{
				if (type != null)
				{
					num = num * 23 + type.GetHashCode();
				}
			}
			num = num * 23 + Value.GetHashCode();
			num = num * 23 + Reference.GetHashCode();
			num = num * 23 + Classes.GetHashCode();
			num = num * 23 + Interfaces.GetHashCode();
			num = num * 23 + Structs.GetHashCode();
			num = num * 23 + Enums.GetHashCode();
			num = num * 23 + Public.GetHashCode();
			num = num * 23 + NonPublic.GetHashCode();
			num = num * 23 + Abstract.GetHashCode();
			num = num * 23 + Generic.GetHashCode();
			num = num * 23 + OpenConstructedGeneric.GetHashCode();
			num = num * 23 + Static.GetHashCode();
			num = num * 23 + Sealed.GetHashCode();
			num = num * 23 + Nested.GetHashCode();
			num = num * 23 + Primitives.GetHashCode();
			num = num * 23 + Object.GetHashCode();
			num = num * 23 + NonSerializable.GetHashCode();
			return num * 23 + Obsolete.GetHashCode();
		}

		public bool ValidateType(global::System.Type type)
		{
			global::Unity.VisualScripting.Ensure.That("type").IsNotNull(type);
			if (!Generic && type.IsGenericType)
			{
				return false;
			}
			if (!OpenConstructedGeneric && type.ContainsGenericParameters)
			{
				return false;
			}
			if (!Value && type.IsValueType)
			{
				return false;
			}
			if (!Reference && !type.IsValueType)
			{
				return false;
			}
			if (!Classes && type.IsClass)
			{
				return false;
			}
			if (!Interfaces && type.IsInterface)
			{
				return false;
			}
			if (!Structs && type.IsValueType && !type.IsEnum && !type.IsPrimitive)
			{
				return false;
			}
			if (!Enums && type.IsEnum)
			{
				return false;
			}
			if (!Public && type.IsVisible)
			{
				return false;
			}
			if (!NonPublic && !type.IsVisible)
			{
				return false;
			}
			if (!Abstract && type.IsAbstract())
			{
				return false;
			}
			if (!Static && type.IsStatic())
			{
				return false;
			}
			if (!Sealed && type.IsSealed)
			{
				return false;
			}
			if (!Nested && type.IsNested)
			{
				return false;
			}
			if (!Primitives && type.IsPrimitive)
			{
				return false;
			}
			if (!Object && type == typeof(object))
			{
				return false;
			}
			if (!NonSerializable && !type.IsSerializable)
			{
				return false;
			}
			if (type.IsSpecialName || type.HasAttribute<global::System.Runtime.CompilerServices.CompilerGeneratedAttribute>())
			{
				return false;
			}
			if (!Obsolete && type.HasAttribute<global::System.ObsoleteAttribute>())
			{
				return false;
			}
			bool flag = true;
			if (Types.Count > 0)
			{
				flag = Matching == global::Unity.VisualScripting.TypesMatching.AssignableToAll;
				foreach (global::System.Type type2 in Types)
				{
					if (Matching == global::Unity.VisualScripting.TypesMatching.Any)
					{
						if (type == type2)
						{
							flag = true;
							break;
						}
						continue;
					}
					if (Matching == global::Unity.VisualScripting.TypesMatching.ConvertibleToAny)
					{
						if (type.IsConvertibleTo(type2, guaranteed: true))
						{
							flag = true;
							break;
						}
						continue;
					}
					if (Matching == global::Unity.VisualScripting.TypesMatching.AssignableToAll)
					{
						flag &= type.IsSubclassOf(type2);
						if (!flag)
						{
							break;
						}
						continue;
					}
					throw new global::Unity.VisualScripting.UnexpectedEnumValueException<global::Unity.VisualScripting.TypesMatching>(Matching);
				}
			}
			return flag;
		}

		public override string ToString()
		{
			global::System.Text.StringBuilder stringBuilder = new global::System.Text.StringBuilder();
			stringBuilder.AppendLine($"Matching: {Matching}");
			stringBuilder.AppendLine("Types: " + types.ToCommaSeparatedString());
			stringBuilder.AppendLine();
			stringBuilder.AppendLine($"Value: {Value}");
			stringBuilder.AppendLine($"Reference: {Reference}");
			stringBuilder.AppendLine($"Classes: {Classes}");
			stringBuilder.AppendLine($"Interfaces: {Interfaces}");
			stringBuilder.AppendLine($"Structs: {Structs}");
			stringBuilder.AppendLine($"Enums: {Enums}");
			stringBuilder.AppendLine($"Public: {Public}");
			stringBuilder.AppendLine($"NonPublic: {NonPublic}");
			stringBuilder.AppendLine($"Abstract: {Abstract}");
			stringBuilder.AppendLine($"Generic: {Generic}");
			stringBuilder.AppendLine($"OpenConstructedGeneric: {OpenConstructedGeneric}");
			stringBuilder.AppendLine($"Static: {Static}");
			stringBuilder.AppendLine($"Sealed: {Sealed}");
			stringBuilder.AppendLine($"Nested: {Nested}");
			stringBuilder.AppendLine($"Primitives: {Primitives}");
			stringBuilder.AppendLine($"Object: {Object}");
			stringBuilder.AppendLine($"NonSerializable: {NonSerializable}");
			stringBuilder.AppendLine($"Obsolete: {Obsolete}");
			return stringBuilder.ToString();
		}
	}
}
