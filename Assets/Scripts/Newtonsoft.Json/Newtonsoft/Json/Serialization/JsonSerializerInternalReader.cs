namespace Newtonsoft.Json.Serialization
{
	internal class JsonSerializerInternalReader : global::Newtonsoft.Json.Serialization.JsonSerializerInternalBase
	{
		internal enum PropertyPresence
		{
			None = 0,
			Null = 1,
			Value = 2
		}

		internal class CreatorPropertyContext
		{
			public readonly string Name;

			public global::Newtonsoft.Json.Serialization.JsonProperty? Property;

			public global::Newtonsoft.Json.Serialization.JsonProperty? ConstructorProperty;

			public global::Newtonsoft.Json.Serialization.JsonSerializerInternalReader.PropertyPresence? Presence;

			public object? Value;

			public bool Used;

			public CreatorPropertyContext(string name)
			{
				Name = name;
			}
		}

		public JsonSerializerInternalReader(global::Newtonsoft.Json.JsonSerializer serializer)
			: base(serializer)
		{
		}

		public void Populate(global::Newtonsoft.Json.JsonReader reader, object target)
		{
			global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(target, "target");
			global::System.Type type = target.GetType();
			global::Newtonsoft.Json.Serialization.JsonContract jsonContract = Serializer._contractResolver.ResolveContract(type);
			if (!reader.MoveToContent())
			{
				throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, "No JSON content found.");
			}
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.StartArray)
			{
				if (jsonContract.ContractType == global::Newtonsoft.Json.Serialization.JsonContractType.Array)
				{
					global::Newtonsoft.Json.Serialization.JsonArrayContract jsonArrayContract = (global::Newtonsoft.Json.Serialization.JsonArrayContract)jsonContract;
					object list;
					if (!jsonArrayContract.ShouldCreateWrapper)
					{
						list = (global::System.Collections.IList)target;
					}
					else
					{
						global::System.Collections.IList list2 = jsonArrayContract.CreateWrapper(target);
						list = list2;
					}
					PopulateList((global::System.Collections.IList)list, reader, jsonArrayContract, null, null);
					return;
				}
				throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Cannot populate JSON array onto type '{0}'.", global::System.Globalization.CultureInfo.InvariantCulture, type));
			}
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.StartObject)
			{
				reader.ReadAndAssert();
				string id = null;
				if (Serializer.MetadataPropertyHandling != global::Newtonsoft.Json.MetadataPropertyHandling.Ignore && reader.TokenType == global::Newtonsoft.Json.JsonToken.PropertyName && string.Equals(reader.Value.ToString(), "$id", global::System.StringComparison.Ordinal))
				{
					reader.ReadAndAssert();
					id = reader.Value?.ToString();
					reader.ReadAndAssert();
				}
				if (jsonContract.ContractType == global::Newtonsoft.Json.Serialization.JsonContractType.Dictionary)
				{
					global::Newtonsoft.Json.Serialization.JsonDictionaryContract jsonDictionaryContract = (global::Newtonsoft.Json.Serialization.JsonDictionaryContract)jsonContract;
					object dictionary;
					if (!jsonDictionaryContract.ShouldCreateWrapper)
					{
						dictionary = (global::System.Collections.IDictionary)target;
					}
					else
					{
						global::System.Collections.IDictionary dictionary2 = jsonDictionaryContract.CreateWrapper(target);
						dictionary = dictionary2;
					}
					PopulateDictionary((global::System.Collections.IDictionary)dictionary, reader, jsonDictionaryContract, null, id);
				}
				else
				{
					if (jsonContract.ContractType != global::Newtonsoft.Json.Serialization.JsonContractType.Object)
					{
						throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Cannot populate JSON object onto type '{0}'.", global::System.Globalization.CultureInfo.InvariantCulture, type));
					}
					PopulateObject(target, reader, (global::Newtonsoft.Json.Serialization.JsonObjectContract)jsonContract, null, id);
				}
				return;
			}
			throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Unexpected initial token '{0}' when populating object. Expected JSON object or array.", global::System.Globalization.CultureInfo.InvariantCulture, reader.TokenType));
		}

		private global::Newtonsoft.Json.Serialization.JsonContract? GetContractSafe(global::System.Type? type)
		{
			if (type == null)
			{
				return null;
			}
			return GetContract(type);
		}

		private global::Newtonsoft.Json.Serialization.JsonContract GetContract(global::System.Type type)
		{
			return Serializer._contractResolver.ResolveContract(type);
		}

		public object? Deserialize(global::Newtonsoft.Json.JsonReader reader, global::System.Type? objectType, bool checkAdditionalContent)
		{
			if (reader == null)
			{
				throw new global::System.ArgumentNullException("reader");
			}
			global::Newtonsoft.Json.Serialization.JsonContract contractSafe = GetContractSafe(objectType);
			try
			{
				global::Newtonsoft.Json.JsonConverter converter = GetConverter(contractSafe, null, null, null);
				if (reader.TokenType == global::Newtonsoft.Json.JsonToken.None && !reader.ReadForType(contractSafe, converter != null))
				{
					if (contractSafe != null && !contractSafe.IsNullable)
					{
						throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("No JSON content found and type '{0}' is not nullable.", global::System.Globalization.CultureInfo.InvariantCulture, contractSafe.UnderlyingType));
					}
					return null;
				}
				object result = ((converter == null || !converter.CanRead) ? CreateValueInternal(reader, objectType, contractSafe, null, null, null, null) : DeserializeConvertable(converter, reader, objectType, null));
				if (checkAdditionalContent)
				{
					while (reader.Read())
					{
						if (reader.TokenType != global::Newtonsoft.Json.JsonToken.Comment)
						{
							throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, "Additional text found in JSON string after finishing deserializing object.");
						}
					}
				}
				return result;
			}
			catch (global::System.Exception ex)
			{
				if (IsErrorHandled(null, contractSafe, null, reader as global::Newtonsoft.Json.IJsonLineInfo, reader.Path, ex))
				{
					HandleError(reader, readPastError: false, 0);
					return null;
				}
				ClearErrorContext();
				throw;
			}
		}

		private global::Newtonsoft.Json.Serialization.JsonSerializerProxy GetInternalSerializer()
		{
			if (InternalSerializer == null)
			{
				InternalSerializer = new global::Newtonsoft.Json.Serialization.JsonSerializerProxy(this);
			}
			return InternalSerializer;
		}

		private global::Newtonsoft.Json.Linq.JToken? CreateJToken(global::Newtonsoft.Json.JsonReader reader, global::Newtonsoft.Json.Serialization.JsonContract? contract)
		{
			global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(reader, "reader");
			if (contract != null)
			{
				if (contract.UnderlyingType == typeof(global::Newtonsoft.Json.Linq.JRaw))
				{
					return global::Newtonsoft.Json.Linq.JRaw.Create(reader);
				}
				if (reader.TokenType == global::Newtonsoft.Json.JsonToken.Null && !(contract.UnderlyingType == typeof(global::Newtonsoft.Json.Linq.JValue)) && !(contract.UnderlyingType == typeof(global::Newtonsoft.Json.Linq.JToken)))
				{
					return null;
				}
			}
			global::Newtonsoft.Json.Linq.JToken token;
			using (global::Newtonsoft.Json.Linq.JTokenWriter jTokenWriter = new global::Newtonsoft.Json.Linq.JTokenWriter())
			{
				jTokenWriter.WriteToken(reader);
				token = jTokenWriter.Token;
			}
			if (contract != null && token != null && !contract.UnderlyingType.IsAssignableFrom(token.GetType()))
			{
				throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Deserialized JSON type '{0}' is not compatible with expected type '{1}'.", global::System.Globalization.CultureInfo.InvariantCulture, token.GetType().FullName, contract.UnderlyingType.FullName));
			}
			return token;
		}

		private global::Newtonsoft.Json.Linq.JToken CreateJObject(global::Newtonsoft.Json.JsonReader reader)
		{
			global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(reader, "reader");
			using global::Newtonsoft.Json.Linq.JTokenWriter jTokenWriter = new global::Newtonsoft.Json.Linq.JTokenWriter();
			jTokenWriter.WriteStartObject();
			do
			{
				if (reader.TokenType == global::Newtonsoft.Json.JsonToken.PropertyName)
				{
					string text = (string)reader.Value;
					if (!reader.ReadAndMoveToContent())
					{
						break;
					}
					if (!CheckPropertyName(reader, text))
					{
						jTokenWriter.WritePropertyName(text);
						jTokenWriter.WriteToken(reader, writeChildren: true, writeDateConstructorAsDate: true, writeComments: false);
					}
				}
				else if (reader.TokenType != global::Newtonsoft.Json.JsonToken.Comment)
				{
					jTokenWriter.WriteEndObject();
					return jTokenWriter.Token;
				}
			}
			while (reader.Read());
			throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, "Unexpected end when deserializing object.");
		}

		private object? CreateValueInternal(global::Newtonsoft.Json.JsonReader reader, global::System.Type? objectType, global::Newtonsoft.Json.Serialization.JsonContract? contract, global::Newtonsoft.Json.Serialization.JsonProperty? member, global::Newtonsoft.Json.Serialization.JsonContainerContract? containerContract, global::Newtonsoft.Json.Serialization.JsonProperty? containerMember, object? existingValue)
		{
			if (contract != null && contract.ContractType == global::Newtonsoft.Json.Serialization.JsonContractType.Linq)
			{
				return CreateJToken(reader, contract);
			}
			do
			{
				switch (reader.TokenType)
				{
				case global::Newtonsoft.Json.JsonToken.StartObject:
					return CreateObject(reader, objectType, contract, member, containerContract, containerMember, existingValue);
				case global::Newtonsoft.Json.JsonToken.StartArray:
					return CreateList(reader, objectType, contract, member, existingValue, null);
				case global::Newtonsoft.Json.JsonToken.Integer:
				case global::Newtonsoft.Json.JsonToken.Float:
				case global::Newtonsoft.Json.JsonToken.Boolean:
				case global::Newtonsoft.Json.JsonToken.Date:
				case global::Newtonsoft.Json.JsonToken.Bytes:
					return EnsureType(reader, reader.Value, global::System.Globalization.CultureInfo.InvariantCulture, contract, objectType);
				case global::Newtonsoft.Json.JsonToken.String:
				{
					string text = (string)reader.Value;
					if (objectType == typeof(byte[]))
					{
						return global::System.Convert.FromBase64String(text);
					}
					if (CoerceEmptyStringToNull(objectType, contract, text))
					{
						return null;
					}
					return EnsureType(reader, text, global::System.Globalization.CultureInfo.InvariantCulture, contract, objectType);
				}
				case global::Newtonsoft.Json.JsonToken.StartConstructor:
				{
					string value = reader.Value.ToString();
					return EnsureType(reader, value, global::System.Globalization.CultureInfo.InvariantCulture, contract, objectType);
				}
				case global::Newtonsoft.Json.JsonToken.Null:
				case global::Newtonsoft.Json.JsonToken.Undefined:
					if (objectType == typeof(global::System.DBNull))
					{
						return global::System.DBNull.Value;
					}
					return EnsureType(reader, reader.Value, global::System.Globalization.CultureInfo.InvariantCulture, contract, objectType);
				case global::Newtonsoft.Json.JsonToken.Raw:
					return new global::Newtonsoft.Json.Linq.JRaw((string)reader.Value);
				default:
					throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, "Unexpected token while deserializing object: " + reader.TokenType);
				case global::Newtonsoft.Json.JsonToken.Comment:
					break;
				}
			}
			while (reader.Read());
			throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, "Unexpected end when deserializing object.");
		}

		private static bool CoerceEmptyStringToNull(global::System.Type? objectType, global::Newtonsoft.Json.Serialization.JsonContract? contract, string s)
		{
			if (global::Newtonsoft.Json.Utilities.StringUtils.IsNullOrEmpty(s) && objectType != null && objectType != typeof(string) && objectType != typeof(object) && contract != null)
			{
				return contract.IsNullable;
			}
			return false;
		}

		internal string GetExpectedDescription(global::Newtonsoft.Json.Serialization.JsonContract contract)
		{
			switch (contract.ContractType)
			{
			case global::Newtonsoft.Json.Serialization.JsonContractType.Object:
			case global::Newtonsoft.Json.Serialization.JsonContractType.Dictionary:
			case global::Newtonsoft.Json.Serialization.JsonContractType.Dynamic:
			case global::Newtonsoft.Json.Serialization.JsonContractType.Serializable:
				return "JSON object (e.g. {\"name\":\"value\"})";
			case global::Newtonsoft.Json.Serialization.JsonContractType.Array:
				return "JSON array (e.g. [1,2,3])";
			case global::Newtonsoft.Json.Serialization.JsonContractType.Primitive:
				return "JSON primitive value (e.g. string, number, boolean, null)";
			case global::Newtonsoft.Json.Serialization.JsonContractType.String:
				return "JSON string value";
			default:
				throw new global::System.ArgumentOutOfRangeException();
			}
		}

		private global::Newtonsoft.Json.JsonConverter? GetConverter(global::Newtonsoft.Json.Serialization.JsonContract? contract, global::Newtonsoft.Json.JsonConverter? memberConverter, global::Newtonsoft.Json.Serialization.JsonContainerContract? containerContract, global::Newtonsoft.Json.Serialization.JsonProperty? containerProperty)
		{
			global::Newtonsoft.Json.JsonConverter result = null;
			if (memberConverter != null)
			{
				result = memberConverter;
			}
			else if (containerProperty?.ItemConverter != null)
			{
				result = containerProperty.ItemConverter;
			}
			else if (containerContract?.ItemConverter != null)
			{
				result = containerContract.ItemConverter;
			}
			else if (contract != null)
			{
				if (contract.Converter != null)
				{
					result = contract.Converter;
				}
				else
				{
					global::Newtonsoft.Json.JsonConverter matchingConverter = Serializer.GetMatchingConverter(contract.UnderlyingType);
					if (matchingConverter != null)
					{
						result = matchingConverter;
					}
					else if (contract.InternalConverter != null)
					{
						result = contract.InternalConverter;
					}
				}
			}
			return result;
		}

		private object? CreateObject(global::Newtonsoft.Json.JsonReader reader, global::System.Type? objectType, global::Newtonsoft.Json.Serialization.JsonContract? contract, global::Newtonsoft.Json.Serialization.JsonProperty? member, global::Newtonsoft.Json.Serialization.JsonContainerContract? containerContract, global::Newtonsoft.Json.Serialization.JsonProperty? containerMember, object? existingValue)
		{
			global::System.Type objectType2 = objectType;
			string id;
			if (Serializer.MetadataPropertyHandling == global::Newtonsoft.Json.MetadataPropertyHandling.Ignore)
			{
				reader.ReadAndAssert();
				id = null;
			}
			else if (Serializer.MetadataPropertyHandling == global::Newtonsoft.Json.MetadataPropertyHandling.ReadAhead)
			{
				global::Newtonsoft.Json.Linq.JTokenReader jTokenReader = reader as global::Newtonsoft.Json.Linq.JTokenReader;
				if (jTokenReader == null)
				{
					jTokenReader = (global::Newtonsoft.Json.Linq.JTokenReader)global::Newtonsoft.Json.Linq.JToken.ReadFrom(reader).CreateReader();
					jTokenReader.Culture = reader.Culture;
					jTokenReader.DateFormatString = reader.DateFormatString;
					jTokenReader.DateParseHandling = reader.DateParseHandling;
					jTokenReader.DateTimeZoneHandling = reader.DateTimeZoneHandling;
					jTokenReader.FloatParseHandling = reader.FloatParseHandling;
					jTokenReader.SupportMultipleContent = reader.SupportMultipleContent;
					jTokenReader.ReadAndAssert();
					reader = jTokenReader;
				}
				if (ReadMetadataPropertiesToken(jTokenReader, ref objectType2, ref contract, member, containerContract, containerMember, existingValue, out object newValue, out id))
				{
					return newValue;
				}
			}
			else
			{
				reader.ReadAndAssert();
				if (ReadMetadataProperties(reader, ref objectType2, ref contract, member, containerContract, containerMember, existingValue, out object newValue2, out id))
				{
					return newValue2;
				}
			}
			if (HasNoDefinedType(contract))
			{
				return CreateJObject(reader);
			}
			switch (contract.ContractType)
			{
			case global::Newtonsoft.Json.Serialization.JsonContractType.Object:
			{
				bool createdFromNonDefaultCreator2 = false;
				global::Newtonsoft.Json.Serialization.JsonObjectContract jsonObjectContract = (global::Newtonsoft.Json.Serialization.JsonObjectContract)contract;
				object obj = ((existingValue == null || (!(objectType2 == objectType) && !objectType2.IsAssignableFrom(existingValue.GetType()))) ? CreateNewObject(reader, jsonObjectContract, member, containerMember, id, out createdFromNonDefaultCreator2) : existingValue);
				if (createdFromNonDefaultCreator2)
				{
					return obj;
				}
				return PopulateObject(obj, reader, jsonObjectContract, member, id);
			}
			case global::Newtonsoft.Json.Serialization.JsonContractType.Primitive:
			{
				global::Newtonsoft.Json.Serialization.JsonPrimitiveContract contract4 = (global::Newtonsoft.Json.Serialization.JsonPrimitiveContract)contract;
				if (Serializer.MetadataPropertyHandling != global::Newtonsoft.Json.MetadataPropertyHandling.Ignore && reader.TokenType == global::Newtonsoft.Json.JsonToken.PropertyName && string.Equals(reader.Value.ToString(), "$value", global::System.StringComparison.Ordinal))
				{
					reader.ReadAndAssert();
					if (reader.TokenType == global::Newtonsoft.Json.JsonToken.StartObject)
					{
						throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, "Unexpected token when deserializing primitive value: " + reader.TokenType);
					}
					object? result = CreateValueInternal(reader, objectType2, contract4, member, null, null, existingValue);
					reader.ReadAndAssert();
					return result;
				}
				break;
			}
			case global::Newtonsoft.Json.Serialization.JsonContractType.Dictionary:
			{
				global::Newtonsoft.Json.Serialization.JsonDictionaryContract jsonDictionaryContract = (global::Newtonsoft.Json.Serialization.JsonDictionaryContract)contract;
				if (existingValue == null)
				{
					bool createdFromNonDefaultCreator;
					global::System.Collections.IDictionary dictionary = CreateNewDictionary(reader, jsonDictionaryContract, out createdFromNonDefaultCreator);
					if (createdFromNonDefaultCreator)
					{
						if (id != null)
						{
							throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Cannot preserve reference to readonly dictionary, or dictionary created from a non-default constructor: {0}.", global::System.Globalization.CultureInfo.InvariantCulture, contract.UnderlyingType));
						}
						if (contract.OnSerializingCallbacks.Count > 0)
						{
							throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Cannot call OnSerializing on readonly dictionary, or dictionary created from a non-default constructor: {0}.", global::System.Globalization.CultureInfo.InvariantCulture, contract.UnderlyingType));
						}
						if (contract.OnErrorCallbacks.Count > 0)
						{
							throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Cannot call OnError on readonly list, or dictionary created from a non-default constructor: {0}.", global::System.Globalization.CultureInfo.InvariantCulture, contract.UnderlyingType));
						}
						if (!jsonDictionaryContract.HasParameterizedCreatorInternal)
						{
							throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Cannot deserialize readonly or fixed size dictionary: {0}.", global::System.Globalization.CultureInfo.InvariantCulture, contract.UnderlyingType));
						}
					}
					PopulateDictionary(dictionary, reader, jsonDictionaryContract, member, id);
					if (createdFromNonDefaultCreator)
					{
						return (jsonDictionaryContract.OverrideCreator ?? jsonDictionaryContract.ParameterizedCreator)(dictionary);
					}
					if (dictionary is global::Newtonsoft.Json.Utilities.IWrappedDictionary wrappedDictionary)
					{
						return wrappedDictionary.UnderlyingDictionary;
					}
					return dictionary;
				}
				object dictionary2;
				if (!jsonDictionaryContract.ShouldCreateWrapper && existingValue is global::System.Collections.IDictionary)
				{
					dictionary2 = (global::System.Collections.IDictionary)existingValue;
				}
				else
				{
					global::System.Collections.IDictionary dictionary3 = jsonDictionaryContract.CreateWrapper(existingValue);
					dictionary2 = dictionary3;
				}
				return PopulateDictionary((global::System.Collections.IDictionary)dictionary2, reader, jsonDictionaryContract, member, id);
			}
			case global::Newtonsoft.Json.Serialization.JsonContractType.Dynamic:
			{
				global::Newtonsoft.Json.Serialization.JsonDynamicContract contract3 = (global::Newtonsoft.Json.Serialization.JsonDynamicContract)contract;
				return CreateDynamic(reader, contract3, member, id);
			}
			case global::Newtonsoft.Json.Serialization.JsonContractType.Serializable:
			{
				global::Newtonsoft.Json.Serialization.JsonISerializableContract contract2 = (global::Newtonsoft.Json.Serialization.JsonISerializableContract)contract;
				return CreateISerializable(reader, contract2, member, id);
			}
			}
			string format = "Cannot deserialize the current JSON object (e.g. {{\"name\":\"value\"}}) into type '{0}' because the type requires a {1} to deserialize correctly." + global::System.Environment.NewLine + "To fix this error either change the JSON to a {1} or change the deserialized type so that it is a normal .NET type (e.g. not a primitive type like integer, not a collection type like an array or List<T>) that can be deserialized from a JSON object. JsonObjectAttribute can also be added to the type to force it to deserialize from a JSON object." + global::System.Environment.NewLine;
			format = global::Newtonsoft.Json.Utilities.StringUtils.FormatWith(format, global::System.Globalization.CultureInfo.InvariantCulture, objectType2, GetExpectedDescription(contract));
			throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, format);
		}

		private bool ReadMetadataPropertiesToken(global::Newtonsoft.Json.Linq.JTokenReader reader, ref global::System.Type? objectType, ref global::Newtonsoft.Json.Serialization.JsonContract? contract, global::Newtonsoft.Json.Serialization.JsonProperty? member, global::Newtonsoft.Json.Serialization.JsonContainerContract? containerContract, global::Newtonsoft.Json.Serialization.JsonProperty? containerMember, object? existingValue, out object? newValue, out string? id)
		{
			id = null;
			newValue = null;
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.StartObject)
			{
				global::Newtonsoft.Json.Linq.JObject jObject = (global::Newtonsoft.Json.Linq.JObject)reader.CurrentToken;
				global::Newtonsoft.Json.Linq.JProperty jProperty = jObject.Property("$ref", global::System.StringComparison.Ordinal);
				if (jProperty != null)
				{
					global::Newtonsoft.Json.Linq.JToken value = jProperty.Value;
					if (value.Type != global::Newtonsoft.Json.Linq.JTokenType.String && value.Type != global::Newtonsoft.Json.Linq.JTokenType.Null)
					{
						throw global::Newtonsoft.Json.JsonSerializationException.Create(value, value.Path, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("JSON reference {0} property must have a string or null value.", global::System.Globalization.CultureInfo.InvariantCulture, "$ref"), null);
					}
					string text = (string?)(global::Newtonsoft.Json.Linq.JToken?)jProperty;
					if (text != null)
					{
						global::Newtonsoft.Json.Linq.JToken jToken = jProperty.Next ?? jProperty.Previous;
						if (jToken != null)
						{
							throw global::Newtonsoft.Json.JsonSerializationException.Create(jToken, jToken.Path, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Additional content found in JSON reference object. A JSON reference object should only have a {0} property.", global::System.Globalization.CultureInfo.InvariantCulture, "$ref"), null);
						}
						newValue = Serializer.GetReferenceResolver().ResolveReference(this, text);
						if (TraceWriter != null && TraceWriter.LevelFilter >= global::System.Diagnostics.TraceLevel.Info)
						{
							TraceWriter.Trace(global::System.Diagnostics.TraceLevel.Info, global::Newtonsoft.Json.JsonPosition.FormatMessage(reader, reader.Path, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Resolved object reference '{0}' to {1}.", global::System.Globalization.CultureInfo.InvariantCulture, text, newValue.GetType())), null);
						}
						reader.Skip();
						return true;
					}
				}
				global::Newtonsoft.Json.Linq.JToken jToken2 = jObject["$type"];
				if (jToken2 != null)
				{
					string qualifiedTypeName = (string?)jToken2;
					global::Newtonsoft.Json.JsonReader jsonReader = jToken2.CreateReader();
					jsonReader.ReadAndAssert();
					ResolveTypeName(jsonReader, ref objectType, ref contract, member, containerContract, containerMember, qualifiedTypeName);
					if (jObject["$value"] != null)
					{
						while (true)
						{
							reader.ReadAndAssert();
							if (reader.TokenType == global::Newtonsoft.Json.JsonToken.PropertyName && (string)reader.Value == "$value")
							{
								break;
							}
							reader.ReadAndAssert();
							reader.Skip();
						}
						return false;
					}
				}
				global::Newtonsoft.Json.Linq.JToken jToken3 = jObject["$id"];
				if (jToken3 != null)
				{
					id = (string?)jToken3;
				}
				global::Newtonsoft.Json.Linq.JToken jToken4 = jObject["$values"];
				if (jToken4 != null)
				{
					global::Newtonsoft.Json.JsonReader jsonReader2 = jToken4.CreateReader();
					jsonReader2.ReadAndAssert();
					newValue = CreateList(jsonReader2, objectType, contract, member, existingValue, id);
					reader.Skip();
					return true;
				}
			}
			reader.ReadAndAssert();
			return false;
		}

		private bool ReadMetadataProperties(global::Newtonsoft.Json.JsonReader reader, ref global::System.Type? objectType, ref global::Newtonsoft.Json.Serialization.JsonContract? contract, global::Newtonsoft.Json.Serialization.JsonProperty? member, global::Newtonsoft.Json.Serialization.JsonContainerContract? containerContract, global::Newtonsoft.Json.Serialization.JsonProperty? containerMember, object? existingValue, out object? newValue, out string? id)
		{
			id = null;
			newValue = null;
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.PropertyName)
			{
				string text = reader.Value.ToString();
				if (text.Length > 0 && text[0] == '$')
				{
					bool flag;
					do
					{
						text = reader.Value.ToString();
						if (string.Equals(text, "$ref", global::System.StringComparison.Ordinal))
						{
							reader.ReadAndAssert();
							if (reader.TokenType != global::Newtonsoft.Json.JsonToken.String && reader.TokenType != global::Newtonsoft.Json.JsonToken.Null)
							{
								throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("JSON reference {0} property must have a string or null value.", global::System.Globalization.CultureInfo.InvariantCulture, "$ref"));
							}
							string text2 = reader.Value?.ToString();
							reader.ReadAndAssert();
							if (text2 != null)
							{
								if (reader.TokenType == global::Newtonsoft.Json.JsonToken.PropertyName)
								{
									throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Additional content found in JSON reference object. A JSON reference object should only have a {0} property.", global::System.Globalization.CultureInfo.InvariantCulture, "$ref"));
								}
								newValue = Serializer.GetReferenceResolver().ResolveReference(this, text2);
								if (TraceWriter != null && TraceWriter.LevelFilter >= global::System.Diagnostics.TraceLevel.Info)
								{
									TraceWriter.Trace(global::System.Diagnostics.TraceLevel.Info, global::Newtonsoft.Json.JsonPosition.FormatMessage(reader as global::Newtonsoft.Json.IJsonLineInfo, reader.Path, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Resolved object reference '{0}' to {1}.", global::System.Globalization.CultureInfo.InvariantCulture, text2, newValue.GetType())), null);
								}
								return true;
							}
							flag = true;
						}
						else if (string.Equals(text, "$type", global::System.StringComparison.Ordinal))
						{
							reader.ReadAndAssert();
							string qualifiedTypeName = reader.Value.ToString();
							ResolveTypeName(reader, ref objectType, ref contract, member, containerContract, containerMember, qualifiedTypeName);
							reader.ReadAndAssert();
							flag = true;
						}
						else if (string.Equals(text, "$id", global::System.StringComparison.Ordinal))
						{
							reader.ReadAndAssert();
							id = reader.Value?.ToString();
							reader.ReadAndAssert();
							flag = true;
						}
						else
						{
							if (string.Equals(text, "$values", global::System.StringComparison.Ordinal))
							{
								reader.ReadAndAssert();
								object obj = CreateList(reader, objectType, contract, member, existingValue, id);
								reader.ReadAndAssert();
								newValue = obj;
								return true;
							}
							flag = false;
						}
					}
					while (flag && reader.TokenType == global::Newtonsoft.Json.JsonToken.PropertyName);
				}
			}
			return false;
		}

		private void ResolveTypeName(global::Newtonsoft.Json.JsonReader reader, ref global::System.Type? objectType, ref global::Newtonsoft.Json.Serialization.JsonContract? contract, global::Newtonsoft.Json.Serialization.JsonProperty? member, global::Newtonsoft.Json.Serialization.JsonContainerContract? containerContract, global::Newtonsoft.Json.Serialization.JsonProperty? containerMember, string qualifiedTypeName)
		{
			if ((member?.TypeNameHandling ?? containerContract?.ItemTypeNameHandling ?? containerMember?.ItemTypeNameHandling ?? Serializer._typeNameHandling) != global::Newtonsoft.Json.TypeNameHandling.None)
			{
				global::Newtonsoft.Json.Utilities.StructMultiKey<string, string> structMultiKey = global::Newtonsoft.Json.Utilities.ReflectionUtils.SplitFullyQualifiedTypeName(qualifiedTypeName);
				global::System.Type type;
				try
				{
					type = Serializer._serializationBinder.BindToType(structMultiKey.Value1, structMultiKey.Value2);
				}
				catch (global::System.Exception ex)
				{
					throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Error resolving type specified in JSON '{0}'.", global::System.Globalization.CultureInfo.InvariantCulture, qualifiedTypeName), ex);
				}
				if (type == null)
				{
					throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Type specified in JSON '{0}' was not resolved.", global::System.Globalization.CultureInfo.InvariantCulture, qualifiedTypeName));
				}
				if (TraceWriter != null && TraceWriter.LevelFilter >= global::System.Diagnostics.TraceLevel.Verbose)
				{
					TraceWriter.Trace(global::System.Diagnostics.TraceLevel.Verbose, global::Newtonsoft.Json.JsonPosition.FormatMessage(reader as global::Newtonsoft.Json.IJsonLineInfo, reader.Path, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Resolved type '{0}' to {1}.", global::System.Globalization.CultureInfo.InvariantCulture, qualifiedTypeName, type)), null);
				}
				if (objectType != null && objectType != typeof(global::System.Dynamic.IDynamicMetaObjectProvider) && !objectType.IsAssignableFrom(type))
				{
					throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Type specified in JSON '{0}' is not compatible with '{1}'.", global::System.Globalization.CultureInfo.InvariantCulture, type.AssemblyQualifiedName, objectType.AssemblyQualifiedName));
				}
				objectType = type;
				contract = GetContract(type);
			}
		}

		private global::Newtonsoft.Json.Serialization.JsonArrayContract EnsureArrayContract(global::Newtonsoft.Json.JsonReader reader, global::System.Type objectType, global::Newtonsoft.Json.Serialization.JsonContract contract)
		{
			if (contract == null)
			{
				throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Could not resolve type '{0}' to a JsonContract.", global::System.Globalization.CultureInfo.InvariantCulture, objectType));
			}
			global::Newtonsoft.Json.Serialization.JsonArrayContract obj = contract as global::Newtonsoft.Json.Serialization.JsonArrayContract;
			if (obj == null)
			{
				string format = "Cannot deserialize the current JSON array (e.g. [1,2,3]) into type '{0}' because the type requires a {1} to deserialize correctly." + global::System.Environment.NewLine + "To fix this error either change the JSON to a {1} or change the deserialized type to an array or a type that implements a collection interface (e.g. ICollection, IList) like List<T> that can be deserialized from a JSON array. JsonArrayAttribute can also be added to the type to force it to deserialize from a JSON array." + global::System.Environment.NewLine;
				format = global::Newtonsoft.Json.Utilities.StringUtils.FormatWith(format, global::System.Globalization.CultureInfo.InvariantCulture, objectType, GetExpectedDescription(contract));
				throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, format);
			}
			return obj;
		}

		private object? CreateList(global::Newtonsoft.Json.JsonReader reader, global::System.Type? objectType, global::Newtonsoft.Json.Serialization.JsonContract? contract, global::Newtonsoft.Json.Serialization.JsonProperty? member, object? existingValue, string? id)
		{
			if (HasNoDefinedType(contract))
			{
				return CreateJToken(reader, contract);
			}
			global::Newtonsoft.Json.Serialization.JsonArrayContract jsonArrayContract = EnsureArrayContract(reader, objectType, contract);
			if (existingValue == null)
			{
				bool createdFromNonDefaultCreator;
				global::System.Collections.IList list = CreateNewList(reader, jsonArrayContract, out createdFromNonDefaultCreator);
				if (createdFromNonDefaultCreator)
				{
					if (id != null)
					{
						throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Cannot preserve reference to array or readonly list, or list created from a non-default constructor: {0}.", global::System.Globalization.CultureInfo.InvariantCulture, contract.UnderlyingType));
					}
					if (contract.OnSerializingCallbacks.Count > 0)
					{
						throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Cannot call OnSerializing on an array or readonly list, or list created from a non-default constructor: {0}.", global::System.Globalization.CultureInfo.InvariantCulture, contract.UnderlyingType));
					}
					if (contract.OnErrorCallbacks.Count > 0)
					{
						throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Cannot call OnError on an array or readonly list, or list created from a non-default constructor: {0}.", global::System.Globalization.CultureInfo.InvariantCulture, contract.UnderlyingType));
					}
					if (!jsonArrayContract.HasParameterizedCreatorInternal && !jsonArrayContract.IsArray)
					{
						throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Cannot deserialize readonly or fixed size list: {0}.", global::System.Globalization.CultureInfo.InvariantCulture, contract.UnderlyingType));
					}
				}
				if (!jsonArrayContract.IsMultidimensionalArray)
				{
					PopulateList(list, reader, jsonArrayContract, member, id);
				}
				else
				{
					PopulateMultidimensionalArray(list, reader, jsonArrayContract, member, id);
				}
				if (createdFromNonDefaultCreator)
				{
					if (jsonArrayContract.IsMultidimensionalArray)
					{
						list = global::Newtonsoft.Json.Utilities.CollectionUtils.ToMultidimensionalArray(list, jsonArrayContract.CollectionItemType, contract.CreatedType.GetArrayRank());
					}
					else
					{
						if (!jsonArrayContract.IsArray)
						{
							return (jsonArrayContract.OverrideCreator ?? jsonArrayContract.ParameterizedCreator)(list);
						}
						global::System.Array array = global::System.Array.CreateInstance(jsonArrayContract.CollectionItemType, list.Count);
						list.CopyTo(array, 0);
						list = array;
					}
				}
				else if (list is global::Newtonsoft.Json.Utilities.IWrappedCollection wrappedCollection)
				{
					return wrappedCollection.UnderlyingCollection;
				}
				return list;
			}
			if (!jsonArrayContract.CanDeserialize)
			{
				throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Cannot populate list type {0}.", global::System.Globalization.CultureInfo.InvariantCulture, contract.CreatedType));
			}
			global::System.Collections.IList list3;
			if (!jsonArrayContract.ShouldCreateWrapper && existingValue is global::System.Collections.IList list2)
			{
				list3 = list2;
			}
			else
			{
				global::System.Collections.IList list4 = jsonArrayContract.CreateWrapper(existingValue);
				list3 = list4;
			}
			return PopulateList(list3, reader, jsonArrayContract, member, id);
		}

		private bool HasNoDefinedType(global::Newtonsoft.Json.Serialization.JsonContract? contract)
		{
			if (contract != null && !(contract.UnderlyingType == typeof(object)) && contract.ContractType != global::Newtonsoft.Json.Serialization.JsonContractType.Linq)
			{
				return contract.UnderlyingType == typeof(global::System.Dynamic.IDynamicMetaObjectProvider);
			}
			return true;
		}

		private object? EnsureType(global::Newtonsoft.Json.JsonReader reader, object? value, global::System.Globalization.CultureInfo culture, global::Newtonsoft.Json.Serialization.JsonContract? contract, global::System.Type? targetType)
		{
			if (targetType == null)
			{
				return value;
			}
			if (global::Newtonsoft.Json.Utilities.ReflectionUtils.GetObjectType(value) != targetType)
			{
				if (value == null && contract.IsNullable)
				{
					return null;
				}
				try
				{
					if (contract.IsConvertable)
					{
						global::Newtonsoft.Json.Serialization.JsonPrimitiveContract jsonPrimitiveContract = (global::Newtonsoft.Json.Serialization.JsonPrimitiveContract)contract;
						global::System.DateTime dt;
						if (contract.IsEnum)
						{
							if (value is string value2)
							{
								return global::Newtonsoft.Json.Utilities.EnumUtils.ParseEnum(contract.NonNullableUnderlyingType, null, value2, disallowNumber: false);
							}
							if (global::Newtonsoft.Json.Utilities.ConvertUtils.IsInteger(jsonPrimitiveContract.TypeCode))
							{
								return global::System.Enum.ToObject(contract.NonNullableUnderlyingType, value);
							}
						}
						else if (contract.NonNullableUnderlyingType == typeof(global::System.DateTime) && value is string s && global::Newtonsoft.Json.Utilities.DateTimeUtils.TryParseDateTime(s, reader.DateTimeZoneHandling, reader.DateFormatString, reader.Culture, out dt))
						{
							return global::Newtonsoft.Json.Utilities.DateTimeUtils.EnsureDateTime(dt, reader.DateTimeZoneHandling);
						}
						if (value is global::System.Numerics.BigInteger i)
						{
							return global::Newtonsoft.Json.Utilities.ConvertUtils.FromBigInteger(i, contract.NonNullableUnderlyingType);
						}
						return global::System.Convert.ChangeType(value, contract.NonNullableUnderlyingType, culture);
					}
					return global::Newtonsoft.Json.Utilities.ConvertUtils.ConvertOrCast(value, culture, contract.NonNullableUnderlyingType);
				}
				catch (global::System.Exception ex)
				{
					throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Error converting value {0} to type '{1}'.", global::System.Globalization.CultureInfo.InvariantCulture, global::Newtonsoft.Json.Utilities.MiscellaneousUtils.ToString(value), targetType), ex);
				}
			}
			return value;
		}

		private bool SetPropertyValue(global::Newtonsoft.Json.Serialization.JsonProperty property, global::Newtonsoft.Json.JsonConverter? propertyConverter, global::Newtonsoft.Json.Serialization.JsonContainerContract? containerContract, global::Newtonsoft.Json.Serialization.JsonProperty? containerProperty, global::Newtonsoft.Json.JsonReader reader, object target)
		{
			if (CalculatePropertyDetails(property, ref propertyConverter, containerContract, containerProperty, reader, target, out bool useExistingValue, out object currentValue, out global::Newtonsoft.Json.Serialization.JsonContract propertyContract, out bool gottenCurrentValue, out bool ignoredValue))
			{
				if (ignoredValue)
				{
					return true;
				}
				return false;
			}
			object obj;
			if (propertyConverter != null && propertyConverter.CanRead)
			{
				if (!gottenCurrentValue && property.Readable)
				{
					currentValue = property.ValueProvider.GetValue(target);
				}
				obj = DeserializeConvertable(propertyConverter, reader, property.PropertyType, currentValue);
			}
			else
			{
				obj = CreateValueInternal(reader, property.PropertyType, propertyContract, property, containerContract, containerProperty, useExistingValue ? currentValue : null);
			}
			if ((!useExistingValue || obj != currentValue) && ShouldSetPropertyValue(property, containerContract as global::Newtonsoft.Json.Serialization.JsonObjectContract, obj))
			{
				property.ValueProvider.SetValue(target, obj);
				if (property.SetIsSpecified != null)
				{
					if (TraceWriter != null && TraceWriter.LevelFilter >= global::System.Diagnostics.TraceLevel.Verbose)
					{
						TraceWriter.Trace(global::System.Diagnostics.TraceLevel.Verbose, global::Newtonsoft.Json.JsonPosition.FormatMessage(reader as global::Newtonsoft.Json.IJsonLineInfo, reader.Path, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("IsSpecified for property '{0}' on {1} set to true.", global::System.Globalization.CultureInfo.InvariantCulture, property.PropertyName, property.DeclaringType)), null);
					}
					property.SetIsSpecified(target, true);
				}
				return true;
			}
			return useExistingValue;
		}

		private bool CalculatePropertyDetails(global::Newtonsoft.Json.Serialization.JsonProperty property, ref global::Newtonsoft.Json.JsonConverter? propertyConverter, global::Newtonsoft.Json.Serialization.JsonContainerContract? containerContract, global::Newtonsoft.Json.Serialization.JsonProperty? containerProperty, global::Newtonsoft.Json.JsonReader reader, object target, out bool useExistingValue, out object? currentValue, out global::Newtonsoft.Json.Serialization.JsonContract? propertyContract, out bool gottenCurrentValue, out bool ignoredValue)
		{
			currentValue = null;
			useExistingValue = false;
			propertyContract = null;
			gottenCurrentValue = false;
			ignoredValue = false;
			if (property.Ignored)
			{
				return true;
			}
			global::Newtonsoft.Json.JsonToken tokenType = reader.TokenType;
			if (property.PropertyContract == null)
			{
				property.PropertyContract = GetContractSafe(property.PropertyType);
			}
			if (property.ObjectCreationHandling.GetValueOrDefault(Serializer._objectCreationHandling) != global::Newtonsoft.Json.ObjectCreationHandling.Replace && (tokenType == global::Newtonsoft.Json.JsonToken.StartArray || tokenType == global::Newtonsoft.Json.JsonToken.StartObject || propertyConverter != null) && property.Readable)
			{
				global::Newtonsoft.Json.Serialization.JsonContract? propertyContract2 = property.PropertyContract;
				if (propertyContract2 == null || propertyContract2.ContractType != global::Newtonsoft.Json.Serialization.JsonContractType.Linq)
				{
					currentValue = property.ValueProvider.GetValue(target);
					gottenCurrentValue = true;
					if (currentValue != null)
					{
						propertyContract = GetContract(currentValue.GetType());
						useExistingValue = !propertyContract.IsReadOnlyOrFixedSize && !global::Newtonsoft.Json.Utilities.TypeExtensions.IsValueType(propertyContract.UnderlyingType);
					}
				}
			}
			if (!property.Writable && !useExistingValue)
			{
				if (TraceWriter != null && TraceWriter.LevelFilter >= global::System.Diagnostics.TraceLevel.Info)
				{
					TraceWriter.Trace(global::System.Diagnostics.TraceLevel.Info, global::Newtonsoft.Json.JsonPosition.FormatMessage(reader as global::Newtonsoft.Json.IJsonLineInfo, reader.Path, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Unable to deserialize value to non-writable property '{0}' on {1}.", global::System.Globalization.CultureInfo.InvariantCulture, property.PropertyName, property.DeclaringType)), null);
				}
				return true;
			}
			if (tokenType == global::Newtonsoft.Json.JsonToken.Null && ResolvedNullValueHandling(containerContract as global::Newtonsoft.Json.Serialization.JsonObjectContract, property) == global::Newtonsoft.Json.NullValueHandling.Ignore)
			{
				ignoredValue = true;
				return true;
			}
			if (HasFlag(property.DefaultValueHandling.GetValueOrDefault(Serializer._defaultValueHandling), global::Newtonsoft.Json.DefaultValueHandling.Ignore) && !HasFlag(property.DefaultValueHandling.GetValueOrDefault(Serializer._defaultValueHandling), global::Newtonsoft.Json.DefaultValueHandling.Populate) && global::Newtonsoft.Json.Utilities.JsonTokenUtils.IsPrimitiveToken(tokenType) && global::Newtonsoft.Json.Utilities.MiscellaneousUtils.ValueEquals(reader.Value, property.GetResolvedDefaultValue()))
			{
				ignoredValue = true;
				return true;
			}
			if (currentValue == null)
			{
				propertyContract = property.PropertyContract;
			}
			else
			{
				propertyContract = GetContract(currentValue.GetType());
				if (propertyContract != property.PropertyContract)
				{
					propertyConverter = GetConverter(propertyContract, property.Converter, containerContract, containerProperty);
				}
			}
			return false;
		}

		private void AddReference(global::Newtonsoft.Json.JsonReader reader, string id, object value)
		{
			try
			{
				if (TraceWriter != null && TraceWriter.LevelFilter >= global::System.Diagnostics.TraceLevel.Verbose)
				{
					TraceWriter.Trace(global::System.Diagnostics.TraceLevel.Verbose, global::Newtonsoft.Json.JsonPosition.FormatMessage(reader as global::Newtonsoft.Json.IJsonLineInfo, reader.Path, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Read object reference Id '{0}' for {1}.", global::System.Globalization.CultureInfo.InvariantCulture, id, value.GetType())), null);
				}
				Serializer.GetReferenceResolver().AddReference(this, id, value);
			}
			catch (global::System.Exception ex)
			{
				throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Error reading object reference '{0}'.", global::System.Globalization.CultureInfo.InvariantCulture, id), ex);
			}
		}

		private bool HasFlag(global::Newtonsoft.Json.DefaultValueHandling value, global::Newtonsoft.Json.DefaultValueHandling flag)
		{
			return (value & flag) == flag;
		}

		private bool ShouldSetPropertyValue(global::Newtonsoft.Json.Serialization.JsonProperty property, global::Newtonsoft.Json.Serialization.JsonObjectContract? contract, object? value)
		{
			if (value == null && ResolvedNullValueHandling(contract, property) == global::Newtonsoft.Json.NullValueHandling.Ignore)
			{
				return false;
			}
			if (HasFlag(property.DefaultValueHandling.GetValueOrDefault(Serializer._defaultValueHandling), global::Newtonsoft.Json.DefaultValueHandling.Ignore) && !HasFlag(property.DefaultValueHandling.GetValueOrDefault(Serializer._defaultValueHandling), global::Newtonsoft.Json.DefaultValueHandling.Populate) && global::Newtonsoft.Json.Utilities.MiscellaneousUtils.ValueEquals(value, property.GetResolvedDefaultValue()))
			{
				return false;
			}
			if (!property.Writable)
			{
				return false;
			}
			return true;
		}

		private global::System.Collections.IList CreateNewList(global::Newtonsoft.Json.JsonReader reader, global::Newtonsoft.Json.Serialization.JsonArrayContract contract, out bool createdFromNonDefaultCreator)
		{
			if (!contract.CanDeserialize)
			{
				throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Cannot create and populate list type {0}.", global::System.Globalization.CultureInfo.InvariantCulture, contract.CreatedType));
			}
			if (contract.OverrideCreator != null)
			{
				if (contract.HasParameterizedCreator)
				{
					createdFromNonDefaultCreator = true;
					return contract.CreateTemporaryCollection();
				}
				object obj = contract.OverrideCreator();
				if (contract.ShouldCreateWrapper)
				{
					obj = contract.CreateWrapper(obj);
				}
				createdFromNonDefaultCreator = false;
				return (global::System.Collections.IList)obj;
			}
			if (contract.IsReadOnlyOrFixedSize)
			{
				createdFromNonDefaultCreator = true;
				global::System.Collections.IList list = contract.CreateTemporaryCollection();
				if (contract.ShouldCreateWrapper)
				{
					list = contract.CreateWrapper(list);
				}
				return list;
			}
			if (contract.DefaultCreator != null && (!contract.DefaultCreatorNonPublic || Serializer._constructorHandling == global::Newtonsoft.Json.ConstructorHandling.AllowNonPublicDefaultConstructor))
			{
				object obj2 = contract.DefaultCreator();
				if (contract.ShouldCreateWrapper)
				{
					obj2 = contract.CreateWrapper(obj2);
				}
				createdFromNonDefaultCreator = false;
				return (global::System.Collections.IList)obj2;
			}
			if (contract.HasParameterizedCreatorInternal)
			{
				createdFromNonDefaultCreator = true;
				return contract.CreateTemporaryCollection();
			}
			if (!contract.IsInstantiable)
			{
				throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Could not create an instance of type {0}. Type is an interface or abstract class and cannot be instantiated.", global::System.Globalization.CultureInfo.InvariantCulture, contract.UnderlyingType));
			}
			throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Unable to find a constructor to use for type {0}.", global::System.Globalization.CultureInfo.InvariantCulture, contract.UnderlyingType));
		}

		private global::System.Collections.IDictionary CreateNewDictionary(global::Newtonsoft.Json.JsonReader reader, global::Newtonsoft.Json.Serialization.JsonDictionaryContract contract, out bool createdFromNonDefaultCreator)
		{
			if (contract.OverrideCreator != null)
			{
				if (contract.HasParameterizedCreator)
				{
					createdFromNonDefaultCreator = true;
					return contract.CreateTemporaryDictionary();
				}
				createdFromNonDefaultCreator = false;
				return (global::System.Collections.IDictionary)contract.OverrideCreator();
			}
			if (contract.IsReadOnlyOrFixedSize)
			{
				createdFromNonDefaultCreator = true;
				return contract.CreateTemporaryDictionary();
			}
			if (contract.DefaultCreator != null && (!contract.DefaultCreatorNonPublic || Serializer._constructorHandling == global::Newtonsoft.Json.ConstructorHandling.AllowNonPublicDefaultConstructor))
			{
				object obj = contract.DefaultCreator();
				if (contract.ShouldCreateWrapper)
				{
					obj = contract.CreateWrapper(obj);
				}
				createdFromNonDefaultCreator = false;
				return (global::System.Collections.IDictionary)obj;
			}
			if (contract.HasParameterizedCreatorInternal)
			{
				createdFromNonDefaultCreator = true;
				return contract.CreateTemporaryDictionary();
			}
			if (!contract.IsInstantiable)
			{
				throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Could not create an instance of type {0}. Type is an interface or abstract class and cannot be instantiated.", global::System.Globalization.CultureInfo.InvariantCulture, contract.UnderlyingType));
			}
			throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Unable to find a default constructor to use for type {0}.", global::System.Globalization.CultureInfo.InvariantCulture, contract.UnderlyingType));
		}

		private void OnDeserializing(global::Newtonsoft.Json.JsonReader reader, global::Newtonsoft.Json.Serialization.JsonContract contract, object value)
		{
			if (TraceWriter != null && TraceWriter.LevelFilter >= global::System.Diagnostics.TraceLevel.Info)
			{
				TraceWriter.Trace(global::System.Diagnostics.TraceLevel.Info, global::Newtonsoft.Json.JsonPosition.FormatMessage(reader as global::Newtonsoft.Json.IJsonLineInfo, reader.Path, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Started deserializing {0}", global::System.Globalization.CultureInfo.InvariantCulture, contract.UnderlyingType)), null);
			}
			contract.InvokeOnDeserializing(value, Serializer._context);
		}

		private void OnDeserialized(global::Newtonsoft.Json.JsonReader reader, global::Newtonsoft.Json.Serialization.JsonContract contract, object value)
		{
			if (TraceWriter != null && TraceWriter.LevelFilter >= global::System.Diagnostics.TraceLevel.Info)
			{
				TraceWriter.Trace(global::System.Diagnostics.TraceLevel.Info, global::Newtonsoft.Json.JsonPosition.FormatMessage(reader as global::Newtonsoft.Json.IJsonLineInfo, reader.Path, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Finished deserializing {0}", global::System.Globalization.CultureInfo.InvariantCulture, contract.UnderlyingType)), null);
			}
			contract.InvokeOnDeserialized(value, Serializer._context);
		}

		private object PopulateDictionary(global::System.Collections.IDictionary dictionary, global::Newtonsoft.Json.JsonReader reader, global::Newtonsoft.Json.Serialization.JsonDictionaryContract contract, global::Newtonsoft.Json.Serialization.JsonProperty? containerProperty, string? id)
		{
			object obj = ((dictionary is global::Newtonsoft.Json.Utilities.IWrappedDictionary wrappedDictionary) ? wrappedDictionary.UnderlyingDictionary : dictionary);
			if (id != null)
			{
				AddReference(reader, id, obj);
			}
			OnDeserializing(reader, contract, obj);
			int depth = reader.Depth;
			if (contract.KeyContract == null)
			{
				contract.KeyContract = GetContractSafe(contract.DictionaryKeyType);
			}
			if (contract.ItemContract == null)
			{
				contract.ItemContract = GetContractSafe(contract.DictionaryValueType);
			}
			global::Newtonsoft.Json.JsonConverter jsonConverter = contract.ItemConverter ?? GetConverter(contract.ItemContract, null, contract, containerProperty);
			global::Newtonsoft.Json.Utilities.PrimitiveTypeCode primitiveTypeCode = ((contract.KeyContract is global::Newtonsoft.Json.Serialization.JsonPrimitiveContract jsonPrimitiveContract) ? jsonPrimitiveContract.TypeCode : global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Empty);
			bool flag = false;
			do
			{
				switch (reader.TokenType)
				{
				case global::Newtonsoft.Json.JsonToken.PropertyName:
				{
					object obj2 = reader.Value;
					if (CheckPropertyName(reader, obj2.ToString()))
					{
						break;
					}
					try
					{
						try
						{
							switch (primitiveTypeCode)
							{
							case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.DateTime:
							case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.DateTimeNullable:
							{
								obj2 = (global::Newtonsoft.Json.Utilities.DateTimeUtils.TryParseDateTime(obj2.ToString(), reader.DateTimeZoneHandling, reader.DateFormatString, reader.Culture, out var dt2) ? ((object)dt2) : EnsureType(reader, obj2, global::System.Globalization.CultureInfo.InvariantCulture, contract.KeyContract, contract.DictionaryKeyType));
								break;
							}
							case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.DateTimeOffset:
							case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.DateTimeOffsetNullable:
							{
								obj2 = (global::Newtonsoft.Json.Utilities.DateTimeUtils.TryParseDateTimeOffset(obj2.ToString(), reader.DateFormatString, reader.Culture, out var dt) ? ((object)dt) : EnsureType(reader, obj2, global::System.Globalization.CultureInfo.InvariantCulture, contract.KeyContract, contract.DictionaryKeyType));
								break;
							}
							default:
								obj2 = ((contract.KeyContract != null && contract.KeyContract.IsEnum) ? global::Newtonsoft.Json.Utilities.EnumUtils.ParseEnum(contract.KeyContract.NonNullableUnderlyingType, (Serializer._contractResolver as global::Newtonsoft.Json.Serialization.DefaultContractResolver)?.NamingStrategy, obj2.ToString(), disallowNumber: false) : EnsureType(reader, obj2, global::System.Globalization.CultureInfo.InvariantCulture, contract.KeyContract, contract.DictionaryKeyType));
								break;
							}
						}
						catch (global::System.Exception ex)
						{
							throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Could not convert string '{0}' to dictionary key type '{1}'. Create a TypeConverter to convert from the string to the key type object.", global::System.Globalization.CultureInfo.InvariantCulture, reader.Value, contract.DictionaryKeyType), ex);
						}
						if (!reader.ReadForType(contract.ItemContract, jsonConverter != null))
						{
							throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, "Unexpected end when deserializing object.");
						}
						object value = ((jsonConverter == null || !jsonConverter.CanRead) ? CreateValueInternal(reader, contract.DictionaryValueType, contract.ItemContract, null, contract, containerProperty, null) : DeserializeConvertable(jsonConverter, reader, contract.DictionaryValueType, null));
						dictionary[obj2] = value;
					}
					catch (global::System.Exception ex2)
					{
						if (IsErrorHandled(obj, contract, obj2, reader as global::Newtonsoft.Json.IJsonLineInfo, reader.Path, ex2))
						{
							HandleError(reader, readPastError: true, depth);
							break;
						}
						throw;
					}
					break;
				}
				case global::Newtonsoft.Json.JsonToken.EndObject:
					flag = true;
					break;
				default:
					throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, "Unexpected token when deserializing object: " + reader.TokenType);
				case global::Newtonsoft.Json.JsonToken.Comment:
					break;
				}
			}
			while (!flag && reader.Read());
			if (!flag)
			{
				ThrowUnexpectedEndException(reader, contract, obj, "Unexpected end when deserializing object.");
			}
			OnDeserialized(reader, contract, obj);
			return obj;
		}

		private object PopulateMultidimensionalArray(global::System.Collections.IList list, global::Newtonsoft.Json.JsonReader reader, global::Newtonsoft.Json.Serialization.JsonArrayContract contract, global::Newtonsoft.Json.Serialization.JsonProperty? containerProperty, string? id)
		{
			int arrayRank = contract.UnderlyingType.GetArrayRank();
			if (id != null)
			{
				AddReference(reader, id, list);
			}
			OnDeserializing(reader, contract, list);
			global::Newtonsoft.Json.Serialization.JsonContract contractSafe = GetContractSafe(contract.CollectionItemType);
			global::Newtonsoft.Json.JsonConverter converter = GetConverter(contractSafe, null, contract, containerProperty);
			int? num = null;
			global::System.Collections.Generic.Stack<global::System.Collections.IList> stack = new global::System.Collections.Generic.Stack<global::System.Collections.IList>();
			stack.Push(list);
			global::System.Collections.IList list2 = list;
			bool flag = false;
			do
			{
				int depth = reader.Depth;
				if (stack.Count == arrayRank)
				{
					try
					{
						if (reader.ReadForType(contractSafe, converter != null))
						{
							switch (reader.TokenType)
							{
							case global::Newtonsoft.Json.JsonToken.EndArray:
								stack.Pop();
								list2 = stack.Peek();
								num = null;
								break;
							default:
							{
								object value = ((converter == null || !converter.CanRead) ? CreateValueInternal(reader, contract.CollectionItemType, contractSafe, null, contract, containerProperty, null) : DeserializeConvertable(converter, reader, contract.CollectionItemType, null));
								list2.Add(value);
								break;
							}
							case global::Newtonsoft.Json.JsonToken.Comment:
								break;
							}
							continue;
						}
					}
					catch (global::System.Exception ex)
					{
						global::Newtonsoft.Json.JsonPosition position = reader.GetPosition(depth);
						if (IsErrorHandled(list, contract, position.Position, reader as global::Newtonsoft.Json.IJsonLineInfo, reader.Path, ex))
						{
							HandleError(reader, readPastError: true, depth + 1);
							if (num.HasValue && num == position.Position)
							{
								throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, "Infinite loop detected from error handling.", ex);
							}
							num = position.Position;
							continue;
						}
						throw;
					}
					break;
				}
				if (!reader.Read())
				{
					break;
				}
				switch (reader.TokenType)
				{
				case global::Newtonsoft.Json.JsonToken.StartArray:
				{
					global::System.Collections.IList list3 = new global::System.Collections.Generic.List<object>();
					list2.Add(list3);
					stack.Push(list3);
					list2 = list3;
					break;
				}
				case global::Newtonsoft.Json.JsonToken.EndArray:
					stack.Pop();
					if (stack.Count > 0)
					{
						list2 = stack.Peek();
					}
					else
					{
						flag = true;
					}
					break;
				default:
					throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, "Unexpected token when deserializing multidimensional array: " + reader.TokenType);
				case global::Newtonsoft.Json.JsonToken.Comment:
					break;
				}
			}
			while (!flag);
			if (!flag)
			{
				ThrowUnexpectedEndException(reader, contract, list, "Unexpected end when deserializing array.");
			}
			OnDeserialized(reader, contract, list);
			return list;
		}

		private void ThrowUnexpectedEndException(global::Newtonsoft.Json.JsonReader reader, global::Newtonsoft.Json.Serialization.JsonContract contract, object? currentObject, string message)
		{
			try
			{
				throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, message);
			}
			catch (global::System.Exception ex)
			{
				if (IsErrorHandled(currentObject, contract, null, reader as global::Newtonsoft.Json.IJsonLineInfo, reader.Path, ex))
				{
					HandleError(reader, readPastError: false, 0);
					return;
				}
				throw;
			}
		}

		private object PopulateList(global::System.Collections.IList list, global::Newtonsoft.Json.JsonReader reader, global::Newtonsoft.Json.Serialization.JsonArrayContract contract, global::Newtonsoft.Json.Serialization.JsonProperty? containerProperty, string? id)
		{
			object obj = ((list is global::Newtonsoft.Json.Utilities.IWrappedCollection wrappedCollection) ? wrappedCollection.UnderlyingCollection : list);
			if (id != null)
			{
				AddReference(reader, id, obj);
			}
			if (list.IsFixedSize)
			{
				reader.Skip();
				return obj;
			}
			OnDeserializing(reader, contract, obj);
			int depth = reader.Depth;
			if (contract.ItemContract == null)
			{
				contract.ItemContract = GetContractSafe(contract.CollectionItemType);
			}
			global::Newtonsoft.Json.JsonConverter converter = GetConverter(contract.ItemContract, null, contract, containerProperty);
			int? num = null;
			bool flag = false;
			do
			{
				try
				{
					if (reader.ReadForType(contract.ItemContract, converter != null))
					{
						switch (reader.TokenType)
						{
						case global::Newtonsoft.Json.JsonToken.EndArray:
							flag = true;
							break;
						default:
						{
							object value = ((converter == null || !converter.CanRead) ? CreateValueInternal(reader, contract.CollectionItemType, contract.ItemContract, null, contract, containerProperty, null) : DeserializeConvertable(converter, reader, contract.CollectionItemType, null));
							list.Add(value);
							break;
						}
						case global::Newtonsoft.Json.JsonToken.Comment:
							break;
						}
						continue;
					}
				}
				catch (global::System.Exception ex)
				{
					global::Newtonsoft.Json.JsonPosition position = reader.GetPosition(depth);
					if (IsErrorHandled(obj, contract, position.Position, reader as global::Newtonsoft.Json.IJsonLineInfo, reader.Path, ex))
					{
						HandleError(reader, readPastError: true, depth + 1);
						if (num.HasValue && num == position.Position)
						{
							throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, "Infinite loop detected from error handling.", ex);
						}
						num = position.Position;
						continue;
					}
					throw;
				}
				break;
			}
			while (!flag);
			if (!flag)
			{
				ThrowUnexpectedEndException(reader, contract, obj, "Unexpected end when deserializing array.");
			}
			OnDeserialized(reader, contract, obj);
			return obj;
		}

		private object CreateISerializable(global::Newtonsoft.Json.JsonReader reader, global::Newtonsoft.Json.Serialization.JsonISerializableContract contract, global::Newtonsoft.Json.Serialization.JsonProperty? member, string? id)
		{
			global::System.Type underlyingType = contract.UnderlyingType;
			if (!global::Newtonsoft.Json.Serialization.JsonTypeReflector.FullyTrusted)
			{
				string format = "Type '{0}' implements ISerializable but cannot be deserialized using the ISerializable interface because the current application is not fully trusted and ISerializable can expose secure data." + global::System.Environment.NewLine + "To fix this error either change the environment to be fully trusted, change the application to not deserialize the type, add JsonObjectAttribute to the type or change the JsonSerializer setting ContractResolver to use a new DefaultContractResolver with IgnoreSerializableInterface set to true." + global::System.Environment.NewLine;
				format = global::Newtonsoft.Json.Utilities.StringUtils.FormatWith(format, global::System.Globalization.CultureInfo.InvariantCulture, underlyingType);
				throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, format);
			}
			if (TraceWriter != null && TraceWriter.LevelFilter >= global::System.Diagnostics.TraceLevel.Info)
			{
				TraceWriter.Trace(global::System.Diagnostics.TraceLevel.Info, global::Newtonsoft.Json.JsonPosition.FormatMessage(reader as global::Newtonsoft.Json.IJsonLineInfo, reader.Path, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Deserializing {0} using ISerializable constructor.", global::System.Globalization.CultureInfo.InvariantCulture, contract.UnderlyingType)), null);
			}
			global::System.Runtime.Serialization.SerializationInfo serializationInfo = new global::System.Runtime.Serialization.SerializationInfo(contract.UnderlyingType, new global::Newtonsoft.Json.Serialization.JsonFormatterConverter(this, contract, member));
			bool flag = false;
			do
			{
				switch (reader.TokenType)
				{
				case global::Newtonsoft.Json.JsonToken.PropertyName:
				{
					string text = reader.Value.ToString();
					if (!reader.Read())
					{
						throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Unexpected end when setting {0}'s value.", global::System.Globalization.CultureInfo.InvariantCulture, text));
					}
					serializationInfo.AddValue(text, global::Newtonsoft.Json.Linq.JToken.ReadFrom(reader));
					break;
				}
				case global::Newtonsoft.Json.JsonToken.EndObject:
					flag = true;
					break;
				default:
					throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, "Unexpected token when deserializing object: " + reader.TokenType);
				case global::Newtonsoft.Json.JsonToken.Comment:
					break;
				}
			}
			while (!flag && reader.Read());
			if (!flag)
			{
				ThrowUnexpectedEndException(reader, contract, serializationInfo, "Unexpected end when deserializing object.");
			}
			if (!contract.IsInstantiable)
			{
				throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Could not create an instance of type {0}. Type is an interface or abstract class and cannot be instantiated.", global::System.Globalization.CultureInfo.InvariantCulture, contract.UnderlyingType));
			}
			if (contract.ISerializableCreator == null)
			{
				throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("ISerializable type '{0}' does not have a valid constructor. To correctly implement ISerializable a constructor that takes SerializationInfo and StreamingContext parameters should be present.", global::System.Globalization.CultureInfo.InvariantCulture, underlyingType));
			}
			object obj = contract.ISerializableCreator(serializationInfo, Serializer._context);
			if (id != null)
			{
				AddReference(reader, id, obj);
			}
			OnDeserializing(reader, contract, obj);
			OnDeserialized(reader, contract, obj);
			return obj;
		}

		internal object? CreateISerializableItem(global::Newtonsoft.Json.Linq.JToken token, global::System.Type type, global::Newtonsoft.Json.Serialization.JsonISerializableContract contract, global::Newtonsoft.Json.Serialization.JsonProperty? member)
		{
			global::Newtonsoft.Json.Serialization.JsonContract contractSafe = GetContractSafe(type);
			global::Newtonsoft.Json.JsonConverter converter = GetConverter(contractSafe, null, contract, member);
			global::Newtonsoft.Json.JsonReader jsonReader = token.CreateReader();
			jsonReader.MaxDepth = Serializer.MaxDepth;
			jsonReader.ReadAndAssert();
			if (converter != null && converter.CanRead)
			{
				return DeserializeConvertable(converter, jsonReader, type, null);
			}
			return CreateValueInternal(jsonReader, type, contractSafe, null, contract, member, null);
		}

		private object CreateDynamic(global::Newtonsoft.Json.JsonReader reader, global::Newtonsoft.Json.Serialization.JsonDynamicContract contract, global::Newtonsoft.Json.Serialization.JsonProperty? member, string? id)
		{
			if (!contract.IsInstantiable)
			{
				throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Could not create an instance of type {0}. Type is an interface or abstract class and cannot be instantiated.", global::System.Globalization.CultureInfo.InvariantCulture, contract.UnderlyingType));
			}
			if (contract.DefaultCreator != null && (!contract.DefaultCreatorNonPublic || Serializer._constructorHandling == global::Newtonsoft.Json.ConstructorHandling.AllowNonPublicDefaultConstructor))
			{
				global::System.Dynamic.IDynamicMetaObjectProvider dynamicMetaObjectProvider = (global::System.Dynamic.IDynamicMetaObjectProvider)contract.DefaultCreator();
				if (id != null)
				{
					AddReference(reader, id, dynamicMetaObjectProvider);
				}
				OnDeserializing(reader, contract, dynamicMetaObjectProvider);
				int depth = reader.Depth;
				bool flag = false;
				do
				{
					switch (reader.TokenType)
					{
					case global::Newtonsoft.Json.JsonToken.PropertyName:
					{
						string text = reader.Value.ToString();
						try
						{
							if (!reader.Read())
							{
								throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Unexpected end when setting {0}'s value.", global::System.Globalization.CultureInfo.InvariantCulture, text));
							}
							global::Newtonsoft.Json.Serialization.JsonProperty closestMatchProperty = contract.Properties.GetClosestMatchProperty(text);
							if (closestMatchProperty != null && closestMatchProperty.Writable && !closestMatchProperty.Ignored)
							{
								if (closestMatchProperty.PropertyContract == null)
								{
									closestMatchProperty.PropertyContract = GetContractSafe(closestMatchProperty.PropertyType);
								}
								global::Newtonsoft.Json.JsonConverter converter = GetConverter(closestMatchProperty.PropertyContract, closestMatchProperty.Converter, null, null);
								if (!SetPropertyValue(closestMatchProperty, converter, null, member, reader, dynamicMetaObjectProvider))
								{
									reader.Skip();
								}
							}
							else
							{
								global::System.Type type = (global::Newtonsoft.Json.Utilities.JsonTokenUtils.IsPrimitiveToken(reader.TokenType) ? reader.ValueType : typeof(global::System.Dynamic.IDynamicMetaObjectProvider));
								global::Newtonsoft.Json.Serialization.JsonContract contractSafe = GetContractSafe(type);
								global::Newtonsoft.Json.JsonConverter converter2 = GetConverter(contractSafe, null, null, member);
								object value = ((converter2 == null || !converter2.CanRead) ? CreateValueInternal(reader, type, contractSafe, null, null, member, null) : DeserializeConvertable(converter2, reader, type, null));
								contract.TrySetMember(dynamicMetaObjectProvider, text, value);
							}
						}
						catch (global::System.Exception ex)
						{
							if (IsErrorHandled(dynamicMetaObjectProvider, contract, text, reader as global::Newtonsoft.Json.IJsonLineInfo, reader.Path, ex))
							{
								HandleError(reader, readPastError: true, depth);
								break;
							}
							throw;
						}
						break;
					}
					case global::Newtonsoft.Json.JsonToken.EndObject:
						flag = true;
						break;
					default:
						throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, "Unexpected token when deserializing object: " + reader.TokenType);
					}
				}
				while (!flag && reader.Read());
				if (!flag)
				{
					ThrowUnexpectedEndException(reader, contract, dynamicMetaObjectProvider, "Unexpected end when deserializing object.");
				}
				OnDeserialized(reader, contract, dynamicMetaObjectProvider);
				return dynamicMetaObjectProvider;
			}
			throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Unable to find a default constructor to use for type {0}.", global::System.Globalization.CultureInfo.InvariantCulture, contract.UnderlyingType));
		}

		private object CreateObjectUsingCreatorWithParameters(global::Newtonsoft.Json.JsonReader reader, global::Newtonsoft.Json.Serialization.JsonObjectContract contract, global::Newtonsoft.Json.Serialization.JsonProperty? containerProperty, global::Newtonsoft.Json.Serialization.ObjectConstructor<object> creator, string? id)
		{
			global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(creator, "creator");
			bool flag = contract.HasRequiredOrDefaultValueProperties || HasFlag(Serializer._defaultValueHandling, global::Newtonsoft.Json.DefaultValueHandling.Populate);
			global::System.Type underlyingType = contract.UnderlyingType;
			if (TraceWriter != null && TraceWriter.LevelFilter >= global::System.Diagnostics.TraceLevel.Info)
			{
				string arg = string.Join(", ", global::System.Linq.Enumerable.Select(contract.CreatorParameters, (global::Newtonsoft.Json.Serialization.JsonProperty p) => p.PropertyName));
				TraceWriter.Trace(global::System.Diagnostics.TraceLevel.Info, global::Newtonsoft.Json.JsonPosition.FormatMessage(reader as global::Newtonsoft.Json.IJsonLineInfo, reader.Path, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Deserializing {0} using creator with parameters: {1}.", global::System.Globalization.CultureInfo.InvariantCulture, contract.UnderlyingType, arg)), null);
			}
			global::System.Collections.Generic.List<global::Newtonsoft.Json.Serialization.JsonSerializerInternalReader.CreatorPropertyContext> list = ResolvePropertyAndCreatorValues(contract, containerProperty, reader, underlyingType);
			if (flag)
			{
				foreach (global::Newtonsoft.Json.Serialization.JsonProperty property in contract.Properties)
				{
					if (!property.Ignored && global::System.Linq.Enumerable.All(list, (global::Newtonsoft.Json.Serialization.JsonSerializerInternalReader.CreatorPropertyContext p) => p.Property != property))
					{
						list.Add(new global::Newtonsoft.Json.Serialization.JsonSerializerInternalReader.CreatorPropertyContext(property.PropertyName)
						{
							Property = property,
							Presence = global::Newtonsoft.Json.Serialization.JsonSerializerInternalReader.PropertyPresence.None
						});
					}
				}
			}
			object[] array = new object[contract.CreatorParameters.Count];
			foreach (global::Newtonsoft.Json.Serialization.JsonSerializerInternalReader.CreatorPropertyContext item in list)
			{
				if (flag && item.Property != null && !item.Presence.HasValue)
				{
					object value = item.Value;
					global::Newtonsoft.Json.Serialization.JsonSerializerInternalReader.PropertyPresence value2 = ((value == null) ? global::Newtonsoft.Json.Serialization.JsonSerializerInternalReader.PropertyPresence.Null : ((!(value is string s)) ? global::Newtonsoft.Json.Serialization.JsonSerializerInternalReader.PropertyPresence.Value : (CoerceEmptyStringToNull(item.Property.PropertyType, item.Property.PropertyContract, s) ? global::Newtonsoft.Json.Serialization.JsonSerializerInternalReader.PropertyPresence.Null : global::Newtonsoft.Json.Serialization.JsonSerializerInternalReader.PropertyPresence.Value)));
					item.Presence = value2;
				}
				global::Newtonsoft.Json.Serialization.JsonProperty jsonProperty = item.ConstructorProperty;
				if (jsonProperty == null && item.Property != null)
				{
					jsonProperty = global::Newtonsoft.Json.Utilities.StringUtils.ForgivingCaseSensitiveFind(contract.CreatorParameters, (global::Newtonsoft.Json.Serialization.JsonProperty p) => p.PropertyName, item.Property.UnderlyingName);
				}
				if (jsonProperty == null || jsonProperty.Ignored)
				{
					continue;
				}
				if (flag && (item.Presence == global::Newtonsoft.Json.Serialization.JsonSerializerInternalReader.PropertyPresence.None || item.Presence == global::Newtonsoft.Json.Serialization.JsonSerializerInternalReader.PropertyPresence.Null))
				{
					if (jsonProperty.PropertyContract == null)
					{
						jsonProperty.PropertyContract = GetContractSafe(jsonProperty.PropertyType);
					}
					if (HasFlag(jsonProperty.DefaultValueHandling.GetValueOrDefault(Serializer._defaultValueHandling), global::Newtonsoft.Json.DefaultValueHandling.Populate))
					{
						item.Value = EnsureType(reader, jsonProperty.GetResolvedDefaultValue(), global::System.Globalization.CultureInfo.InvariantCulture, jsonProperty.PropertyContract, jsonProperty.PropertyType);
					}
				}
				int num = contract.CreatorParameters.IndexOf(jsonProperty);
				array[num] = item.Value;
				item.Used = true;
			}
			object obj = creator(array);
			if (id != null)
			{
				AddReference(reader, id, obj);
			}
			OnDeserializing(reader, contract, obj);
			foreach (global::Newtonsoft.Json.Serialization.JsonSerializerInternalReader.CreatorPropertyContext item2 in list)
			{
				if (item2.Used || item2.Property == null || item2.Property.Ignored || item2.Presence == global::Newtonsoft.Json.Serialization.JsonSerializerInternalReader.PropertyPresence.None)
				{
					continue;
				}
				global::Newtonsoft.Json.Serialization.JsonProperty property2 = item2.Property;
				object value3 = item2.Value;
				if (ShouldSetPropertyValue(property2, contract, value3))
				{
					property2.ValueProvider.SetValue(obj, value3);
					item2.Used = true;
				}
				else
				{
					if (property2.Writable || value3 == null)
					{
						continue;
					}
					global::Newtonsoft.Json.Serialization.JsonContract jsonContract = Serializer._contractResolver.ResolveContract(property2.PropertyType);
					if (jsonContract.ContractType == global::Newtonsoft.Json.Serialization.JsonContractType.Array)
					{
						global::Newtonsoft.Json.Serialization.JsonArrayContract jsonArrayContract = (global::Newtonsoft.Json.Serialization.JsonArrayContract)jsonContract;
						if (jsonArrayContract.CanDeserialize && !jsonArrayContract.IsReadOnlyOrFixedSize)
						{
							object value4 = property2.ValueProvider.GetValue(obj);
							if (value4 != null)
							{
								jsonArrayContract = (global::Newtonsoft.Json.Serialization.JsonArrayContract)GetContract(value4.GetType());
								object obj2;
								if (!jsonArrayContract.ShouldCreateWrapper)
								{
									obj2 = (global::System.Collections.IList)value4;
								}
								else
								{
									global::System.Collections.IList list2 = jsonArrayContract.CreateWrapper(value4);
									obj2 = list2;
								}
								global::System.Collections.IList list3 = (global::System.Collections.IList)obj2;
								if (!list3.IsFixedSize)
								{
									object obj3;
									if (!jsonArrayContract.ShouldCreateWrapper)
									{
										obj3 = (global::System.Collections.IList)value3;
									}
									else
									{
										global::System.Collections.IList list2 = jsonArrayContract.CreateWrapper(value3);
										obj3 = list2;
									}
									foreach (object item3 in (global::System.Collections.IEnumerable)obj3)
									{
										list3.Add(item3);
									}
								}
							}
						}
					}
					else if (jsonContract.ContractType == global::Newtonsoft.Json.Serialization.JsonContractType.Dictionary)
					{
						global::Newtonsoft.Json.Serialization.JsonDictionaryContract jsonDictionaryContract = (global::Newtonsoft.Json.Serialization.JsonDictionaryContract)jsonContract;
						if (!jsonDictionaryContract.IsReadOnlyOrFixedSize)
						{
							object value5 = property2.ValueProvider.GetValue(obj);
							if (value5 != null)
							{
								object obj4;
								if (!jsonDictionaryContract.ShouldCreateWrapper)
								{
									obj4 = (global::System.Collections.IDictionary)value5;
								}
								else
								{
									global::System.Collections.IDictionary dictionary = jsonDictionaryContract.CreateWrapper(value5);
									obj4 = dictionary;
								}
								global::System.Collections.IDictionary dictionary2 = (global::System.Collections.IDictionary)obj4;
								object obj5;
								if (!jsonDictionaryContract.ShouldCreateWrapper)
								{
									obj5 = (global::System.Collections.IDictionary)value3;
								}
								else
								{
									global::System.Collections.IDictionary dictionary = jsonDictionaryContract.CreateWrapper(value3);
									obj5 = dictionary;
								}
								foreach (global::System.Collections.DictionaryEntry item4 in (global::System.Collections.IDictionary)obj5)
								{
									dictionary2[item4.Key] = item4.Value;
								}
							}
						}
					}
					item2.Used = true;
				}
			}
			if (contract.ExtensionDataSetter != null)
			{
				foreach (global::Newtonsoft.Json.Serialization.JsonSerializerInternalReader.CreatorPropertyContext item5 in list)
				{
					if (!item5.Used && item5.Presence != global::Newtonsoft.Json.Serialization.JsonSerializerInternalReader.PropertyPresence.None)
					{
						contract.ExtensionDataSetter(obj, item5.Name, item5.Value);
					}
				}
			}
			if (flag)
			{
				foreach (global::Newtonsoft.Json.Serialization.JsonSerializerInternalReader.CreatorPropertyContext item6 in list)
				{
					if (item6.Property != null)
					{
						EndProcessProperty(obj, reader, contract, reader.Depth, item6.Property, item6.Presence.GetValueOrDefault(), !item6.Used);
					}
				}
			}
			OnDeserialized(reader, contract, obj);
			return obj;
		}

		private object? DeserializeConvertable(global::Newtonsoft.Json.JsonConverter converter, global::Newtonsoft.Json.JsonReader reader, global::System.Type objectType, object? existingValue)
		{
			if (TraceWriter != null && TraceWriter.LevelFilter >= global::System.Diagnostics.TraceLevel.Info)
			{
				TraceWriter.Trace(global::System.Diagnostics.TraceLevel.Info, global::Newtonsoft.Json.JsonPosition.FormatMessage(reader as global::Newtonsoft.Json.IJsonLineInfo, reader.Path, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Started deserializing {0} with converter {1}.", global::System.Globalization.CultureInfo.InvariantCulture, objectType, converter.GetType())), null);
			}
			object? result = converter.ReadJson(reader, objectType, existingValue, GetInternalSerializer());
			if (TraceWriter != null && TraceWriter.LevelFilter >= global::System.Diagnostics.TraceLevel.Info)
			{
				TraceWriter.Trace(global::System.Diagnostics.TraceLevel.Info, global::Newtonsoft.Json.JsonPosition.FormatMessage(reader as global::Newtonsoft.Json.IJsonLineInfo, reader.Path, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Finished deserializing {0} with converter {1}.", global::System.Globalization.CultureInfo.InvariantCulture, objectType, converter.GetType())), null);
			}
			return result;
		}

		private global::System.Collections.Generic.List<global::Newtonsoft.Json.Serialization.JsonSerializerInternalReader.CreatorPropertyContext> ResolvePropertyAndCreatorValues(global::Newtonsoft.Json.Serialization.JsonObjectContract contract, global::Newtonsoft.Json.Serialization.JsonProperty? containerProperty, global::Newtonsoft.Json.JsonReader reader, global::System.Type objectType)
		{
			global::System.Collections.Generic.List<global::Newtonsoft.Json.Serialization.JsonSerializerInternalReader.CreatorPropertyContext> list = new global::System.Collections.Generic.List<global::Newtonsoft.Json.Serialization.JsonSerializerInternalReader.CreatorPropertyContext>();
			bool flag = false;
			do
			{
				switch (reader.TokenType)
				{
				case global::Newtonsoft.Json.JsonToken.PropertyName:
				{
					string text = reader.Value.ToString();
					global::Newtonsoft.Json.Serialization.JsonSerializerInternalReader.CreatorPropertyContext creatorPropertyContext = new global::Newtonsoft.Json.Serialization.JsonSerializerInternalReader.CreatorPropertyContext(text)
					{
						ConstructorProperty = contract.CreatorParameters.GetClosestMatchProperty(text),
						Property = contract.Properties.GetClosestMatchProperty(text)
					};
					list.Add(creatorPropertyContext);
					global::Newtonsoft.Json.Serialization.JsonProperty jsonProperty = creatorPropertyContext.ConstructorProperty ?? creatorPropertyContext.Property;
					if (jsonProperty != null)
					{
						if (!jsonProperty.Ignored)
						{
							if (jsonProperty.PropertyContract == null)
							{
								jsonProperty.PropertyContract = GetContractSafe(jsonProperty.PropertyType);
							}
							global::Newtonsoft.Json.JsonConverter converter = GetConverter(jsonProperty.PropertyContract, jsonProperty.Converter, contract, containerProperty);
							if (!reader.ReadForType(jsonProperty.PropertyContract, converter != null))
							{
								throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Unexpected end when setting {0}'s value.", global::System.Globalization.CultureInfo.InvariantCulture, text));
							}
							if (converter != null && converter.CanRead)
							{
								creatorPropertyContext.Value = DeserializeConvertable(converter, reader, jsonProperty.PropertyType, null);
							}
							else
							{
								creatorPropertyContext.Value = CreateValueInternal(reader, jsonProperty.PropertyType, jsonProperty.PropertyContract, jsonProperty, contract, containerProperty, null);
							}
							break;
						}
						if (!reader.Read())
						{
							throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Unexpected end when setting {0}'s value.", global::System.Globalization.CultureInfo.InvariantCulture, text));
						}
					}
					else
					{
						if (!reader.Read())
						{
							throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Unexpected end when setting {0}'s value.", global::System.Globalization.CultureInfo.InvariantCulture, text));
						}
						if (TraceWriter != null && TraceWriter.LevelFilter >= global::System.Diagnostics.TraceLevel.Verbose)
						{
							TraceWriter.Trace(global::System.Diagnostics.TraceLevel.Verbose, global::Newtonsoft.Json.JsonPosition.FormatMessage(reader as global::Newtonsoft.Json.IJsonLineInfo, reader.Path, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Could not find member '{0}' on {1}.", global::System.Globalization.CultureInfo.InvariantCulture, text, contract.UnderlyingType)), null);
						}
						if ((contract.MissingMemberHandling ?? Serializer._missingMemberHandling) == global::Newtonsoft.Json.MissingMemberHandling.Error)
						{
							throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Could not find member '{0}' on object of type '{1}'", global::System.Globalization.CultureInfo.InvariantCulture, text, objectType.Name));
						}
					}
					if (contract.ExtensionDataSetter != null)
					{
						creatorPropertyContext.Value = ReadExtensionDataValue(contract, containerProperty, reader);
					}
					else
					{
						reader.Skip();
					}
					break;
				}
				case global::Newtonsoft.Json.JsonToken.EndObject:
					flag = true;
					break;
				default:
					throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, "Unexpected token when deserializing object: " + reader.TokenType);
				case global::Newtonsoft.Json.JsonToken.Comment:
					break;
				}
			}
			while (!flag && reader.Read());
			if (!flag)
			{
				ThrowUnexpectedEndException(reader, contract, null, "Unexpected end when deserializing object.");
			}
			return list;
		}

		public object CreateNewObject(global::Newtonsoft.Json.JsonReader reader, global::Newtonsoft.Json.Serialization.JsonObjectContract objectContract, global::Newtonsoft.Json.Serialization.JsonProperty? containerMember, global::Newtonsoft.Json.Serialization.JsonProperty? containerProperty, string? id, out bool createdFromNonDefaultCreator)
		{
			object obj = null;
			if (objectContract.OverrideCreator != null)
			{
				if (objectContract.CreatorParameters.Count > 0)
				{
					createdFromNonDefaultCreator = true;
					return CreateObjectUsingCreatorWithParameters(reader, objectContract, containerMember, objectContract.OverrideCreator, id);
				}
				obj = objectContract.OverrideCreator(global::Newtonsoft.Json.Utilities.CollectionUtils.ArrayEmpty<object>());
			}
			else if (objectContract.DefaultCreator != null && (!objectContract.DefaultCreatorNonPublic || Serializer._constructorHandling == global::Newtonsoft.Json.ConstructorHandling.AllowNonPublicDefaultConstructor || objectContract.ParameterizedCreator == null))
			{
				obj = objectContract.DefaultCreator();
			}
			else if (objectContract.ParameterizedCreator != null)
			{
				createdFromNonDefaultCreator = true;
				return CreateObjectUsingCreatorWithParameters(reader, objectContract, containerMember, objectContract.ParameterizedCreator, id);
			}
			if (obj == null)
			{
				if (!objectContract.IsInstantiable)
				{
					throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Could not create an instance of type {0}. Type is an interface or abstract class and cannot be instantiated.", global::System.Globalization.CultureInfo.InvariantCulture, objectContract.UnderlyingType));
				}
				throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Unable to find a constructor to use for type {0}. A class should either have a default constructor, one constructor with arguments or a constructor marked with the JsonConstructor attribute.", global::System.Globalization.CultureInfo.InvariantCulture, objectContract.UnderlyingType));
			}
			createdFromNonDefaultCreator = false;
			return obj;
		}

		private object PopulateObject(object newObject, global::Newtonsoft.Json.JsonReader reader, global::Newtonsoft.Json.Serialization.JsonObjectContract contract, global::Newtonsoft.Json.Serialization.JsonProperty? member, string? id)
		{
			OnDeserializing(reader, contract, newObject);
			global::System.Collections.Generic.Dictionary<global::Newtonsoft.Json.Serialization.JsonProperty, global::Newtonsoft.Json.Serialization.JsonSerializerInternalReader.PropertyPresence> dictionary = ((contract.HasRequiredOrDefaultValueProperties || HasFlag(Serializer._defaultValueHandling, global::Newtonsoft.Json.DefaultValueHandling.Populate)) ? global::System.Linq.Enumerable.ToDictionary(contract.Properties, (global::Newtonsoft.Json.Serialization.JsonProperty m) => m, (global::Newtonsoft.Json.Serialization.JsonProperty m) => global::Newtonsoft.Json.Serialization.JsonSerializerInternalReader.PropertyPresence.None) : null);
			if (id != null)
			{
				AddReference(reader, id, newObject);
			}
			int depth = reader.Depth;
			bool flag = false;
			do
			{
				switch (reader.TokenType)
				{
				case global::Newtonsoft.Json.JsonToken.PropertyName:
				{
					string text = reader.Value.ToString();
					if (CheckPropertyName(reader, text))
					{
						break;
					}
					try
					{
						global::Newtonsoft.Json.Serialization.JsonProperty closestMatchProperty = contract.Properties.GetClosestMatchProperty(text);
						if (closestMatchProperty == null)
						{
							if (TraceWriter != null && TraceWriter.LevelFilter >= global::System.Diagnostics.TraceLevel.Verbose)
							{
								TraceWriter.Trace(global::System.Diagnostics.TraceLevel.Verbose, global::Newtonsoft.Json.JsonPosition.FormatMessage(reader as global::Newtonsoft.Json.IJsonLineInfo, reader.Path, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Could not find member '{0}' on {1}", global::System.Globalization.CultureInfo.InvariantCulture, text, contract.UnderlyingType)), null);
							}
							if ((contract.MissingMemberHandling ?? Serializer._missingMemberHandling) == global::Newtonsoft.Json.MissingMemberHandling.Error)
							{
								throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Could not find member '{0}' on object of type '{1}'", global::System.Globalization.CultureInfo.InvariantCulture, text, contract.UnderlyingType.Name));
							}
							if (reader.Read())
							{
								SetExtensionData(contract, member, reader, text, newObject);
							}
							break;
						}
						if (closestMatchProperty.Ignored || !ShouldDeserialize(reader, closestMatchProperty, newObject))
						{
							if (reader.Read())
							{
								SetPropertyPresence(reader, closestMatchProperty, dictionary);
								SetExtensionData(contract, member, reader, text, newObject);
							}
							break;
						}
						if (closestMatchProperty.PropertyContract == null)
						{
							closestMatchProperty.PropertyContract = GetContractSafe(closestMatchProperty.PropertyType);
						}
						global::Newtonsoft.Json.JsonConverter converter = GetConverter(closestMatchProperty.PropertyContract, closestMatchProperty.Converter, contract, member);
						if (!reader.ReadForType(closestMatchProperty.PropertyContract, converter != null))
						{
							throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Unexpected end when setting {0}'s value.", global::System.Globalization.CultureInfo.InvariantCulture, text));
						}
						SetPropertyPresence(reader, closestMatchProperty, dictionary);
						if (!SetPropertyValue(closestMatchProperty, converter, contract, member, reader, newObject))
						{
							SetExtensionData(contract, member, reader, text, newObject);
						}
					}
					catch (global::System.Exception ex)
					{
						if (IsErrorHandled(newObject, contract, text, reader as global::Newtonsoft.Json.IJsonLineInfo, reader.Path, ex))
						{
							HandleError(reader, readPastError: true, depth);
							break;
						}
						throw;
					}
					break;
				}
				case global::Newtonsoft.Json.JsonToken.EndObject:
					flag = true;
					break;
				default:
					throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, "Unexpected token when deserializing object: " + reader.TokenType);
				case global::Newtonsoft.Json.JsonToken.Comment:
					break;
				}
			}
			while (!flag && reader.Read());
			if (!flag)
			{
				ThrowUnexpectedEndException(reader, contract, newObject, "Unexpected end when deserializing object.");
			}
			if (dictionary != null)
			{
				foreach (global::System.Collections.Generic.KeyValuePair<global::Newtonsoft.Json.Serialization.JsonProperty, global::Newtonsoft.Json.Serialization.JsonSerializerInternalReader.PropertyPresence> item in dictionary)
				{
					global::Newtonsoft.Json.Serialization.JsonProperty key = item.Key;
					global::Newtonsoft.Json.Serialization.JsonSerializerInternalReader.PropertyPresence value = item.Value;
					EndProcessProperty(newObject, reader, contract, depth, key, value, setDefaultValue: true);
				}
			}
			OnDeserialized(reader, contract, newObject);
			return newObject;
		}

		private bool ShouldDeserialize(global::Newtonsoft.Json.JsonReader reader, global::Newtonsoft.Json.Serialization.JsonProperty property, object target)
		{
			if (property.ShouldDeserialize == null)
			{
				return true;
			}
			bool flag = property.ShouldDeserialize(target);
			if (TraceWriter != null && TraceWriter.LevelFilter >= global::System.Diagnostics.TraceLevel.Verbose)
			{
				TraceWriter.Trace(global::System.Diagnostics.TraceLevel.Verbose, global::Newtonsoft.Json.JsonPosition.FormatMessage(null, reader.Path, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("ShouldDeserialize result for property '{0}' on {1}: {2}", global::System.Globalization.CultureInfo.InvariantCulture, property.PropertyName, property.DeclaringType, flag)), null);
			}
			return flag;
		}

		private bool CheckPropertyName(global::Newtonsoft.Json.JsonReader reader, string memberName)
		{
			if (Serializer.MetadataPropertyHandling == global::Newtonsoft.Json.MetadataPropertyHandling.ReadAhead)
			{
				switch (memberName)
				{
				case "$id":
				case "$ref":
				case "$type":
				case "$values":
					reader.Skip();
					return true;
				}
			}
			return false;
		}

		private void SetExtensionData(global::Newtonsoft.Json.Serialization.JsonObjectContract contract, global::Newtonsoft.Json.Serialization.JsonProperty? member, global::Newtonsoft.Json.JsonReader reader, string memberName, object o)
		{
			if (contract.ExtensionDataSetter != null)
			{
				try
				{
					object value = ReadExtensionDataValue(contract, member, reader);
					contract.ExtensionDataSetter(o, memberName, value);
					return;
				}
				catch (global::System.Exception ex)
				{
					throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Error setting value in extension data for type '{0}'.", global::System.Globalization.CultureInfo.InvariantCulture, contract.UnderlyingType), ex);
				}
			}
			reader.Skip();
		}

		private object? ReadExtensionDataValue(global::Newtonsoft.Json.Serialization.JsonObjectContract contract, global::Newtonsoft.Json.Serialization.JsonProperty? member, global::Newtonsoft.Json.JsonReader reader)
		{
			if (contract.ExtensionDataIsJToken)
			{
				return global::Newtonsoft.Json.Linq.JToken.ReadFrom(reader);
			}
			return CreateValueInternal(reader, null, null, null, contract, member, null);
		}

		private void EndProcessProperty(object newObject, global::Newtonsoft.Json.JsonReader reader, global::Newtonsoft.Json.Serialization.JsonObjectContract contract, int initialDepth, global::Newtonsoft.Json.Serialization.JsonProperty property, global::Newtonsoft.Json.Serialization.JsonSerializerInternalReader.PropertyPresence presence, bool setDefaultValue)
		{
			if (presence != global::Newtonsoft.Json.Serialization.JsonSerializerInternalReader.PropertyPresence.None && presence != global::Newtonsoft.Json.Serialization.JsonSerializerInternalReader.PropertyPresence.Null)
			{
				return;
			}
			try
			{
				global::Newtonsoft.Json.Required required = ((!property.Ignored) ? (property._required ?? contract.ItemRequired.GetValueOrDefault()) : global::Newtonsoft.Json.Required.Default);
				switch (presence)
				{
				case global::Newtonsoft.Json.Serialization.JsonSerializerInternalReader.PropertyPresence.None:
					if (required == global::Newtonsoft.Json.Required.AllowNull || required == global::Newtonsoft.Json.Required.Always)
					{
						throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Required property '{0}' not found in JSON.", global::System.Globalization.CultureInfo.InvariantCulture, property.PropertyName));
					}
					if (setDefaultValue && !property.Ignored)
					{
						if (property.PropertyContract == null)
						{
							property.PropertyContract = GetContractSafe(property.PropertyType);
						}
						if (HasFlag(property.DefaultValueHandling.GetValueOrDefault(Serializer._defaultValueHandling), global::Newtonsoft.Json.DefaultValueHandling.Populate) && property.Writable)
						{
							property.ValueProvider.SetValue(newObject, EnsureType(reader, property.GetResolvedDefaultValue(), global::System.Globalization.CultureInfo.InvariantCulture, property.PropertyContract, property.PropertyType));
						}
					}
					break;
				case global::Newtonsoft.Json.Serialization.JsonSerializerInternalReader.PropertyPresence.Null:
					switch (required)
					{
					case global::Newtonsoft.Json.Required.Always:
						throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Required property '{0}' expects a value but got null.", global::System.Globalization.CultureInfo.InvariantCulture, property.PropertyName));
					case global::Newtonsoft.Json.Required.DisallowNull:
						throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Required property '{0}' expects a non-null value.", global::System.Globalization.CultureInfo.InvariantCulture, property.PropertyName));
					}
					break;
				}
			}
			catch (global::System.Exception ex)
			{
				if (IsErrorHandled(newObject, contract, property.PropertyName, reader as global::Newtonsoft.Json.IJsonLineInfo, reader.Path, ex))
				{
					HandleError(reader, readPastError: true, initialDepth);
					return;
				}
				throw;
			}
		}

		private void SetPropertyPresence(global::Newtonsoft.Json.JsonReader reader, global::Newtonsoft.Json.Serialization.JsonProperty property, global::System.Collections.Generic.Dictionary<global::Newtonsoft.Json.Serialization.JsonProperty, global::Newtonsoft.Json.Serialization.JsonSerializerInternalReader.PropertyPresence>? requiredProperties)
		{
			if (property != null && requiredProperties != null)
			{
				global::Newtonsoft.Json.Serialization.JsonSerializerInternalReader.PropertyPresence value;
				switch (reader.TokenType)
				{
				case global::Newtonsoft.Json.JsonToken.String:
					value = (CoerceEmptyStringToNull(property.PropertyType, property.PropertyContract, (string)reader.Value) ? global::Newtonsoft.Json.Serialization.JsonSerializerInternalReader.PropertyPresence.Null : global::Newtonsoft.Json.Serialization.JsonSerializerInternalReader.PropertyPresence.Value);
					break;
				case global::Newtonsoft.Json.JsonToken.Null:
				case global::Newtonsoft.Json.JsonToken.Undefined:
					value = global::Newtonsoft.Json.Serialization.JsonSerializerInternalReader.PropertyPresence.Null;
					break;
				default:
					value = global::Newtonsoft.Json.Serialization.JsonSerializerInternalReader.PropertyPresence.Value;
					break;
				}
				requiredProperties[property] = value;
			}
		}

		private void HandleError(global::Newtonsoft.Json.JsonReader reader, bool readPastError, int initialDepth)
		{
			ClearErrorContext();
			if (readPastError)
			{
				reader.Skip();
				while (reader.Depth > initialDepth && reader.Read())
				{
				}
			}
		}
	}
}
