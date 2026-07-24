namespace Newtonsoft.Json.Schema
{
	[global::System.Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
	public class JsonSchemaGenerator
	{
		private class TypeSchema
		{
			public global::System.Type Type { get; }

			public global::Newtonsoft.Json.Schema.JsonSchema Schema { get; }

			public TypeSchema(global::System.Type type, global::Newtonsoft.Json.Schema.JsonSchema schema)
			{
				global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(type, "type");
				global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(schema, "schema");
				Type = type;
				Schema = schema;
			}
		}

		private global::Newtonsoft.Json.Serialization.IContractResolver _contractResolver;

		private global::Newtonsoft.Json.Schema.JsonSchemaResolver _resolver;

		private readonly global::System.Collections.Generic.IList<global::Newtonsoft.Json.Schema.JsonSchemaGenerator.TypeSchema> _stack = new global::System.Collections.Generic.List<global::Newtonsoft.Json.Schema.JsonSchemaGenerator.TypeSchema>();

		private global::Newtonsoft.Json.Schema.JsonSchema _currentSchema;

		public global::Newtonsoft.Json.Schema.UndefinedSchemaIdHandling UndefinedSchemaIdHandling { get; set; }

		public global::Newtonsoft.Json.Serialization.IContractResolver ContractResolver
		{
			get
			{
				if (_contractResolver == null)
				{
					return global::Newtonsoft.Json.Serialization.DefaultContractResolver.Instance;
				}
				return _contractResolver;
			}
			set
			{
				_contractResolver = value;
			}
		}

		private global::Newtonsoft.Json.Schema.JsonSchema CurrentSchema => _currentSchema;

		private void Push(global::Newtonsoft.Json.Schema.JsonSchemaGenerator.TypeSchema typeSchema)
		{
			_currentSchema = typeSchema.Schema;
			_stack.Add(typeSchema);
			_resolver.LoadedSchemas.Add(typeSchema.Schema);
		}

		private global::Newtonsoft.Json.Schema.JsonSchemaGenerator.TypeSchema Pop()
		{
			global::Newtonsoft.Json.Schema.JsonSchemaGenerator.TypeSchema result = _stack[_stack.Count - 1];
			_stack.RemoveAt(_stack.Count - 1);
			global::Newtonsoft.Json.Schema.JsonSchemaGenerator.TypeSchema typeSchema = global::System.Linq.Enumerable.LastOrDefault(_stack);
			if (typeSchema != null)
			{
				_currentSchema = typeSchema.Schema;
				return result;
			}
			_currentSchema = null;
			return result;
		}

		public global::Newtonsoft.Json.Schema.JsonSchema Generate(global::System.Type type)
		{
			return Generate(type, new global::Newtonsoft.Json.Schema.JsonSchemaResolver(), rootSchemaNullable: false);
		}

		public global::Newtonsoft.Json.Schema.JsonSchema Generate(global::System.Type type, global::Newtonsoft.Json.Schema.JsonSchemaResolver resolver)
		{
			return Generate(type, resolver, rootSchemaNullable: false);
		}

		public global::Newtonsoft.Json.Schema.JsonSchema Generate(global::System.Type type, bool rootSchemaNullable)
		{
			return Generate(type, new global::Newtonsoft.Json.Schema.JsonSchemaResolver(), rootSchemaNullable);
		}

		public global::Newtonsoft.Json.Schema.JsonSchema Generate(global::System.Type type, global::Newtonsoft.Json.Schema.JsonSchemaResolver resolver, bool rootSchemaNullable)
		{
			global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(type, "type");
			global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(resolver, "resolver");
			_resolver = resolver;
			return GenerateInternal(type, (!rootSchemaNullable) ? global::Newtonsoft.Json.Required.Always : global::Newtonsoft.Json.Required.Default, required: false);
		}

		private string GetTitle(global::System.Type type)
		{
			global::Newtonsoft.Json.JsonContainerAttribute cachedAttribute = global::Newtonsoft.Json.Serialization.JsonTypeReflector.GetCachedAttribute<global::Newtonsoft.Json.JsonContainerAttribute>(type);
			if (!global::Newtonsoft.Json.Utilities.StringUtils.IsNullOrEmpty(cachedAttribute?.Title))
			{
				return cachedAttribute.Title;
			}
			return null;
		}

		private string GetDescription(global::System.Type type)
		{
			global::Newtonsoft.Json.JsonContainerAttribute cachedAttribute = global::Newtonsoft.Json.Serialization.JsonTypeReflector.GetCachedAttribute<global::Newtonsoft.Json.JsonContainerAttribute>(type);
			if (!global::Newtonsoft.Json.Utilities.StringUtils.IsNullOrEmpty(cachedAttribute?.Description))
			{
				return cachedAttribute.Description;
			}
			return global::Newtonsoft.Json.Utilities.ReflectionUtils.GetAttribute<global::System.ComponentModel.DescriptionAttribute>(type)?.Description;
		}

		private string GetTypeId(global::System.Type type, bool explicitOnly)
		{
			global::Newtonsoft.Json.JsonContainerAttribute cachedAttribute = global::Newtonsoft.Json.Serialization.JsonTypeReflector.GetCachedAttribute<global::Newtonsoft.Json.JsonContainerAttribute>(type);
			if (!global::Newtonsoft.Json.Utilities.StringUtils.IsNullOrEmpty(cachedAttribute?.Id))
			{
				return cachedAttribute.Id;
			}
			if (explicitOnly)
			{
				return null;
			}
			return UndefinedSchemaIdHandling switch
			{
				global::Newtonsoft.Json.Schema.UndefinedSchemaIdHandling.UseTypeName => type.FullName, 
				global::Newtonsoft.Json.Schema.UndefinedSchemaIdHandling.UseAssemblyQualifiedName => type.AssemblyQualifiedName, 
				_ => null, 
			};
		}

		private global::Newtonsoft.Json.Schema.JsonSchema GenerateInternal(global::System.Type type, global::Newtonsoft.Json.Required valueRequired, bool required)
		{
			global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(type, "type");
			string typeId = GetTypeId(type, explicitOnly: false);
			string typeId2 = GetTypeId(type, explicitOnly: true);
			if (!global::Newtonsoft.Json.Utilities.StringUtils.IsNullOrEmpty(typeId))
			{
				global::Newtonsoft.Json.Schema.JsonSchema schema = _resolver.GetSchema(typeId);
				if (schema != null)
				{
					if (valueRequired != global::Newtonsoft.Json.Required.Always && !HasFlag(schema.Type, global::Newtonsoft.Json.Schema.JsonSchemaType.Null))
					{
						schema.Type |= global::Newtonsoft.Json.Schema.JsonSchemaType.Null;
					}
					if (required && schema.Required != true)
					{
						schema.Required = true;
					}
					return schema;
				}
			}
			if (global::System.Linq.Enumerable.Any(_stack, (global::Newtonsoft.Json.Schema.JsonSchemaGenerator.TypeSchema tc) => tc.Type == type))
			{
				throw new global::Newtonsoft.Json.JsonException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Unresolved circular reference for type '{0}'. Explicitly define an Id for the type using a JsonObject/JsonArray attribute or automatically generate a type Id using the UndefinedSchemaIdHandling property.", global::System.Globalization.CultureInfo.InvariantCulture, type));
			}
			global::Newtonsoft.Json.Serialization.JsonContract jsonContract = ContractResolver.ResolveContract(type);
			global::Newtonsoft.Json.JsonConverter? obj = jsonContract.Converter ?? jsonContract.InternalConverter;
			Push(new global::Newtonsoft.Json.Schema.JsonSchemaGenerator.TypeSchema(type, new global::Newtonsoft.Json.Schema.JsonSchema()));
			if (typeId2 != null)
			{
				CurrentSchema.Id = typeId2;
			}
			if (required)
			{
				CurrentSchema.Required = true;
			}
			CurrentSchema.Title = GetTitle(type);
			CurrentSchema.Description = GetDescription(type);
			if (obj != null)
			{
				CurrentSchema.Type = global::Newtonsoft.Json.Schema.JsonSchemaType.Any;
			}
			else
			{
				switch (jsonContract.ContractType)
				{
				case global::Newtonsoft.Json.Serialization.JsonContractType.Object:
					CurrentSchema.Type = AddNullType(global::Newtonsoft.Json.Schema.JsonSchemaType.Object, valueRequired);
					CurrentSchema.Id = GetTypeId(type, explicitOnly: false);
					GenerateObjectSchema(type, (global::Newtonsoft.Json.Serialization.JsonObjectContract)jsonContract);
					break;
				case global::Newtonsoft.Json.Serialization.JsonContractType.Array:
				{
					CurrentSchema.Type = AddNullType(global::Newtonsoft.Json.Schema.JsonSchemaType.Array, valueRequired);
					CurrentSchema.Id = GetTypeId(type, explicitOnly: false);
					bool flag = global::Newtonsoft.Json.Serialization.JsonTypeReflector.GetCachedAttribute<global::Newtonsoft.Json.JsonArrayAttribute>(type)?.AllowNullItems ?? true;
					global::System.Type collectionItemType = global::Newtonsoft.Json.Utilities.ReflectionUtils.GetCollectionItemType(type);
					if (collectionItemType != null)
					{
						CurrentSchema.Items = new global::System.Collections.Generic.List<global::Newtonsoft.Json.Schema.JsonSchema>();
						CurrentSchema.Items.Add(GenerateInternal(collectionItemType, (!flag) ? global::Newtonsoft.Json.Required.Always : global::Newtonsoft.Json.Required.Default, required: false));
					}
					break;
				}
				case global::Newtonsoft.Json.Serialization.JsonContractType.Primitive:
					CurrentSchema.Type = GetJsonSchemaType(type, valueRequired);
					if (CurrentSchema.Type == global::Newtonsoft.Json.Schema.JsonSchemaType.Integer && global::Newtonsoft.Json.Utilities.TypeExtensions.IsEnum(type) && !type.IsDefined(typeof(global::System.FlagsAttribute), inherit: true))
					{
						CurrentSchema.Enum = new global::System.Collections.Generic.List<global::Newtonsoft.Json.Linq.JToken>();
						global::Newtonsoft.Json.Utilities.EnumInfo enumValuesAndNames = global::Newtonsoft.Json.Utilities.EnumUtils.GetEnumValuesAndNames(type);
						for (int num = 0; num < enumValuesAndNames.Names.Length; num++)
						{
							ulong value = enumValuesAndNames.Values[num];
							global::Newtonsoft.Json.Linq.JToken item = global::Newtonsoft.Json.Linq.JToken.FromObject(global::System.Enum.ToObject(type, value));
							CurrentSchema.Enum.Add(item);
						}
					}
					break;
				case global::Newtonsoft.Json.Serialization.JsonContractType.String:
				{
					global::Newtonsoft.Json.Schema.JsonSchemaType value2 = ((!global::Newtonsoft.Json.Utilities.ReflectionUtils.IsNullable(jsonContract.UnderlyingType)) ? global::Newtonsoft.Json.Schema.JsonSchemaType.String : AddNullType(global::Newtonsoft.Json.Schema.JsonSchemaType.String, valueRequired));
					CurrentSchema.Type = value2;
					break;
				}
				case global::Newtonsoft.Json.Serialization.JsonContractType.Dictionary:
				{
					CurrentSchema.Type = AddNullType(global::Newtonsoft.Json.Schema.JsonSchemaType.Object, valueRequired);
					global::Newtonsoft.Json.Utilities.ReflectionUtils.GetDictionaryKeyValueTypes(type, out global::System.Type keyType, out global::System.Type valueType);
					if (keyType != null && ContractResolver.ResolveContract(keyType).ContractType == global::Newtonsoft.Json.Serialization.JsonContractType.Primitive)
					{
						CurrentSchema.AdditionalProperties = GenerateInternal(valueType, global::Newtonsoft.Json.Required.Default, required: false);
					}
					break;
				}
				case global::Newtonsoft.Json.Serialization.JsonContractType.Serializable:
					CurrentSchema.Type = AddNullType(global::Newtonsoft.Json.Schema.JsonSchemaType.Object, valueRequired);
					CurrentSchema.Id = GetTypeId(type, explicitOnly: false);
					GenerateISerializableContract(type, (global::Newtonsoft.Json.Serialization.JsonISerializableContract)jsonContract);
					break;
				case global::Newtonsoft.Json.Serialization.JsonContractType.Dynamic:
				case global::Newtonsoft.Json.Serialization.JsonContractType.Linq:
					CurrentSchema.Type = global::Newtonsoft.Json.Schema.JsonSchemaType.Any;
					break;
				default:
					throw new global::Newtonsoft.Json.JsonException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Unexpected contract type: {0}", global::System.Globalization.CultureInfo.InvariantCulture, jsonContract));
				}
			}
			return Pop().Schema;
		}

		private global::Newtonsoft.Json.Schema.JsonSchemaType AddNullType(global::Newtonsoft.Json.Schema.JsonSchemaType type, global::Newtonsoft.Json.Required valueRequired)
		{
			if (valueRequired != global::Newtonsoft.Json.Required.Always)
			{
				return type | global::Newtonsoft.Json.Schema.JsonSchemaType.Null;
			}
			return type;
		}

		private bool HasFlag(global::Newtonsoft.Json.DefaultValueHandling value, global::Newtonsoft.Json.DefaultValueHandling flag)
		{
			return (value & flag) == flag;
		}

		private void GenerateObjectSchema(global::System.Type type, global::Newtonsoft.Json.Serialization.JsonObjectContract contract)
		{
			CurrentSchema.Properties = new global::System.Collections.Generic.Dictionary<string, global::Newtonsoft.Json.Schema.JsonSchema>();
			foreach (global::Newtonsoft.Json.Serialization.JsonProperty property in contract.Properties)
			{
				if (!property.Ignored)
				{
					bool flag = property.NullValueHandling == global::Newtonsoft.Json.NullValueHandling.Ignore || HasFlag(property.DefaultValueHandling.GetValueOrDefault(), global::Newtonsoft.Json.DefaultValueHandling.Ignore) || property.ShouldSerialize != null || property.GetIsSpecified != null;
					global::Newtonsoft.Json.Schema.JsonSchema jsonSchema = GenerateInternal(property.PropertyType, property.Required, !flag);
					if (property.DefaultValue != null)
					{
						jsonSchema.Default = global::Newtonsoft.Json.Linq.JToken.FromObject(property.DefaultValue);
					}
					CurrentSchema.Properties.Add(property.PropertyName, jsonSchema);
				}
			}
			if (global::Newtonsoft.Json.Utilities.TypeExtensions.IsSealed(type))
			{
				CurrentSchema.AllowAdditionalProperties = false;
			}
		}

		private void GenerateISerializableContract(global::System.Type type, global::Newtonsoft.Json.Serialization.JsonISerializableContract contract)
		{
			CurrentSchema.AllowAdditionalProperties = true;
		}

		internal static bool HasFlag(global::Newtonsoft.Json.Schema.JsonSchemaType? value, global::Newtonsoft.Json.Schema.JsonSchemaType flag)
		{
			if (!value.HasValue)
			{
				return true;
			}
			if ((value & flag) == flag)
			{
				return true;
			}
			if (flag == global::Newtonsoft.Json.Schema.JsonSchemaType.Integer && ((uint?)value & 2u) == 2)
			{
				return true;
			}
			return false;
		}

		private global::Newtonsoft.Json.Schema.JsonSchemaType GetJsonSchemaType(global::System.Type type, global::Newtonsoft.Json.Required valueRequired)
		{
			global::Newtonsoft.Json.Schema.JsonSchemaType jsonSchemaType = global::Newtonsoft.Json.Schema.JsonSchemaType.None;
			if (valueRequired != global::Newtonsoft.Json.Required.Always && global::Newtonsoft.Json.Utilities.ReflectionUtils.IsNullable(type))
			{
				jsonSchemaType = global::Newtonsoft.Json.Schema.JsonSchemaType.Null;
				if (global::Newtonsoft.Json.Utilities.ReflectionUtils.IsNullableType(type))
				{
					type = global::System.Nullable.GetUnderlyingType(type);
				}
			}
			global::Newtonsoft.Json.Utilities.PrimitiveTypeCode typeCode = global::Newtonsoft.Json.Utilities.ConvertUtils.GetTypeCode(type);
			switch (typeCode)
			{
			case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Empty:
			case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Object:
				return jsonSchemaType | global::Newtonsoft.Json.Schema.JsonSchemaType.String;
			case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.DBNull:
				return jsonSchemaType | global::Newtonsoft.Json.Schema.JsonSchemaType.Null;
			case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Boolean:
				return jsonSchemaType | global::Newtonsoft.Json.Schema.JsonSchemaType.Boolean;
			case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Char:
				return jsonSchemaType | global::Newtonsoft.Json.Schema.JsonSchemaType.String;
			case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.SByte:
			case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Int16:
			case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.UInt16:
			case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Int32:
			case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Byte:
			case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.UInt32:
			case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Int64:
			case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.UInt64:
			case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.BigInteger:
				return jsonSchemaType | global::Newtonsoft.Json.Schema.JsonSchemaType.Integer;
			case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Single:
			case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Double:
			case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Decimal:
				return jsonSchemaType | global::Newtonsoft.Json.Schema.JsonSchemaType.Float;
			case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.DateTime:
			case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.DateTimeOffset:
				return jsonSchemaType | global::Newtonsoft.Json.Schema.JsonSchemaType.String;
			case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Guid:
			case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.TimeSpan:
			case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Uri:
			case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.String:
			case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Bytes:
				return jsonSchemaType | global::Newtonsoft.Json.Schema.JsonSchemaType.String;
			default:
				throw new global::Newtonsoft.Json.JsonException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Unexpected type code '{0}' for type '{1}'.", global::System.Globalization.CultureInfo.InvariantCulture, typeCode, type));
			}
		}
	}
}
