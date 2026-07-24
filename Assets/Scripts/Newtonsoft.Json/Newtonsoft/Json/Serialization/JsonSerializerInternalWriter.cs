namespace Newtonsoft.Json.Serialization
{
	internal class JsonSerializerInternalWriter : global::Newtonsoft.Json.Serialization.JsonSerializerInternalBase
	{
		private global::System.Type? _rootType;

		private int _rootLevel;

		private readonly global::System.Collections.Generic.List<object> _serializeStack = new global::System.Collections.Generic.List<object>();

		public JsonSerializerInternalWriter(global::Newtonsoft.Json.JsonSerializer serializer)
			: base(serializer)
		{
		}

		public void Serialize(global::Newtonsoft.Json.JsonWriter jsonWriter, object? value, global::System.Type? objectType)
		{
			if (jsonWriter == null)
			{
				throw new global::System.ArgumentNullException("jsonWriter");
			}
			_rootType = objectType;
			_rootLevel = _serializeStack.Count + 1;
			global::Newtonsoft.Json.Serialization.JsonContract contractSafe = GetContractSafe(value);
			try
			{
				if (ShouldWriteReference(value, null, contractSafe, null, null))
				{
					WriteReference(jsonWriter, value);
				}
				else
				{
					SerializeValue(jsonWriter, value, contractSafe, null, null, null);
				}
			}
			catch (global::System.Exception ex)
			{
				if (IsErrorHandled(null, contractSafe, null, null, jsonWriter.Path, ex))
				{
					HandleError(jsonWriter, 0);
					return;
				}
				ClearErrorContext();
				throw;
			}
			finally
			{
				_rootType = null;
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

		private global::Newtonsoft.Json.Serialization.JsonContract? GetContractSafe(object? value)
		{
			if (value == null)
			{
				return null;
			}
			return GetContract(value);
		}

		private global::Newtonsoft.Json.Serialization.JsonContract GetContract(object value)
		{
			return Serializer._contractResolver.ResolveContract(value.GetType());
		}

		private void SerializePrimitive(global::Newtonsoft.Json.JsonWriter writer, object value, global::Newtonsoft.Json.Serialization.JsonPrimitiveContract contract, global::Newtonsoft.Json.Serialization.JsonProperty? member, global::Newtonsoft.Json.Serialization.JsonContainerContract? containerContract, global::Newtonsoft.Json.Serialization.JsonProperty? containerProperty)
		{
			if (contract.TypeCode == global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Bytes && ShouldWriteType(global::Newtonsoft.Json.TypeNameHandling.Objects, contract, member, containerContract, containerProperty))
			{
				writer.WriteStartObject();
				WriteTypeProperty(writer, contract.CreatedType);
				writer.WritePropertyName("$value", escape: false);
				global::Newtonsoft.Json.JsonWriter.WriteValue(writer, contract.TypeCode, value);
				writer.WriteEndObject();
			}
			else
			{
				global::Newtonsoft.Json.JsonWriter.WriteValue(writer, contract.TypeCode, value);
			}
		}

		private void SerializeValue(global::Newtonsoft.Json.JsonWriter writer, object? value, global::Newtonsoft.Json.Serialization.JsonContract? valueContract, global::Newtonsoft.Json.Serialization.JsonProperty? member, global::Newtonsoft.Json.Serialization.JsonContainerContract? containerContract, global::Newtonsoft.Json.Serialization.JsonProperty? containerProperty)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}
			global::Newtonsoft.Json.JsonConverter jsonConverter = member?.Converter ?? containerProperty?.ItemConverter ?? containerContract?.ItemConverter ?? valueContract.Converter ?? Serializer.GetMatchingConverter(valueContract.UnderlyingType) ?? valueContract.InternalConverter;
			if (jsonConverter != null && jsonConverter.CanWrite)
			{
				SerializeConvertable(writer, jsonConverter, value, valueContract, containerContract, containerProperty);
				return;
			}
			switch (valueContract.ContractType)
			{
			case global::Newtonsoft.Json.Serialization.JsonContractType.Object:
				SerializeObject(writer, value, (global::Newtonsoft.Json.Serialization.JsonObjectContract)valueContract, member, containerContract, containerProperty);
				break;
			case global::Newtonsoft.Json.Serialization.JsonContractType.Array:
			{
				global::Newtonsoft.Json.Serialization.JsonArrayContract jsonArrayContract = (global::Newtonsoft.Json.Serialization.JsonArrayContract)valueContract;
				if (!jsonArrayContract.IsMultidimensionalArray)
				{
					SerializeList(writer, (global::System.Collections.IEnumerable)value, jsonArrayContract, member, containerContract, containerProperty);
				}
				else
				{
					SerializeMultidimensionalArray(writer, (global::System.Array)value, jsonArrayContract, member, containerContract, containerProperty);
				}
				break;
			}
			case global::Newtonsoft.Json.Serialization.JsonContractType.Primitive:
				SerializePrimitive(writer, value, (global::Newtonsoft.Json.Serialization.JsonPrimitiveContract)valueContract, member, containerContract, containerProperty);
				break;
			case global::Newtonsoft.Json.Serialization.JsonContractType.String:
				SerializeString(writer, value, (global::Newtonsoft.Json.Serialization.JsonStringContract)valueContract);
				break;
			case global::Newtonsoft.Json.Serialization.JsonContractType.Dictionary:
			{
				global::Newtonsoft.Json.Serialization.JsonDictionaryContract jsonDictionaryContract = (global::Newtonsoft.Json.Serialization.JsonDictionaryContract)valueContract;
				global::System.Collections.IDictionary values;
				if (!(value is global::System.Collections.IDictionary dictionary))
				{
					global::System.Collections.IDictionary dictionary2 = jsonDictionaryContract.CreateWrapper(value);
					values = dictionary2;
				}
				else
				{
					values = dictionary;
				}
				SerializeDictionary(writer, values, jsonDictionaryContract, member, containerContract, containerProperty);
				break;
			}
			case global::Newtonsoft.Json.Serialization.JsonContractType.Dynamic:
				SerializeDynamic(writer, (global::System.Dynamic.IDynamicMetaObjectProvider)value, (global::Newtonsoft.Json.Serialization.JsonDynamicContract)valueContract, member, containerContract, containerProperty);
				break;
			case global::Newtonsoft.Json.Serialization.JsonContractType.Serializable:
				SerializeISerializable(writer, (global::System.Runtime.Serialization.ISerializable)value, (global::Newtonsoft.Json.Serialization.JsonISerializableContract)valueContract, member, containerContract, containerProperty);
				break;
			case global::Newtonsoft.Json.Serialization.JsonContractType.Linq:
				((global::Newtonsoft.Json.Linq.JToken)value).WriteTo(writer, global::System.Linq.Enumerable.ToArray(Serializer.Converters));
				break;
			}
		}

		private bool? ResolveIsReference(global::Newtonsoft.Json.Serialization.JsonContract contract, global::Newtonsoft.Json.Serialization.JsonProperty? property, global::Newtonsoft.Json.Serialization.JsonContainerContract? collectionContract, global::Newtonsoft.Json.Serialization.JsonProperty? containerProperty)
		{
			bool? result = null;
			if (property != null)
			{
				result = property.IsReference;
			}
			if (!result.HasValue && containerProperty != null)
			{
				result = containerProperty.ItemIsReference;
			}
			if (!result.HasValue && collectionContract != null)
			{
				result = collectionContract.ItemIsReference;
			}
			if (!result.HasValue)
			{
				result = contract.IsReference;
			}
			return result;
		}

		private bool ShouldWriteReference(object? value, global::Newtonsoft.Json.Serialization.JsonProperty? property, global::Newtonsoft.Json.Serialization.JsonContract? valueContract, global::Newtonsoft.Json.Serialization.JsonContainerContract? collectionContract, global::Newtonsoft.Json.Serialization.JsonProperty? containerProperty)
		{
			if (value == null)
			{
				return false;
			}
			if (valueContract.ContractType == global::Newtonsoft.Json.Serialization.JsonContractType.Primitive || valueContract.ContractType == global::Newtonsoft.Json.Serialization.JsonContractType.String)
			{
				return false;
			}
			bool? flag = ResolveIsReference(valueContract, property, collectionContract, containerProperty);
			if (!flag.HasValue)
			{
				flag = ((valueContract.ContractType != global::Newtonsoft.Json.Serialization.JsonContractType.Array) ? new bool?(HasFlag(Serializer._preserveReferencesHandling, global::Newtonsoft.Json.PreserveReferencesHandling.Objects)) : new bool?(HasFlag(Serializer._preserveReferencesHandling, global::Newtonsoft.Json.PreserveReferencesHandling.Arrays)));
			}
			if (flag != true)
			{
				return false;
			}
			return Serializer.GetReferenceResolver().IsReferenced(this, value);
		}

		private bool ShouldWriteProperty(object? memberValue, global::Newtonsoft.Json.Serialization.JsonObjectContract? containerContract, global::Newtonsoft.Json.Serialization.JsonProperty property)
		{
			if (memberValue == null && ResolvedNullValueHandling(containerContract, property) == global::Newtonsoft.Json.NullValueHandling.Ignore)
			{
				return false;
			}
			if (HasFlag(property.DefaultValueHandling.GetValueOrDefault(Serializer._defaultValueHandling), global::Newtonsoft.Json.DefaultValueHandling.Ignore) && global::Newtonsoft.Json.Utilities.MiscellaneousUtils.ValueEquals(memberValue, property.GetResolvedDefaultValue()))
			{
				return false;
			}
			return true;
		}

		private bool CheckForCircularReference(global::Newtonsoft.Json.JsonWriter writer, object? value, global::Newtonsoft.Json.Serialization.JsonProperty? property, global::Newtonsoft.Json.Serialization.JsonContract? contract, global::Newtonsoft.Json.Serialization.JsonContainerContract? containerContract, global::Newtonsoft.Json.Serialization.JsonProperty? containerProperty)
		{
			if (value == null)
			{
				return true;
			}
			if (contract.ContractType == global::Newtonsoft.Json.Serialization.JsonContractType.Primitive || contract.ContractType == global::Newtonsoft.Json.Serialization.JsonContractType.String)
			{
				return true;
			}
			global::Newtonsoft.Json.ReferenceLoopHandling? referenceLoopHandling = null;
			if (property != null)
			{
				referenceLoopHandling = property.ReferenceLoopHandling;
			}
			if (!referenceLoopHandling.HasValue && containerProperty != null)
			{
				referenceLoopHandling = containerProperty.ItemReferenceLoopHandling;
			}
			if (!referenceLoopHandling.HasValue && containerContract != null)
			{
				referenceLoopHandling = containerContract.ItemReferenceLoopHandling;
			}
			if ((Serializer._equalityComparer != null) ? global::Newtonsoft.Json.Utilities.CollectionUtils.Contains(_serializeStack, value, Serializer._equalityComparer) : _serializeStack.Contains(value))
			{
				string text = "Self referencing loop detected";
				if (property != null)
				{
					text += global::Newtonsoft.Json.Utilities.StringUtils.FormatWith(" for property '{0}'", global::System.Globalization.CultureInfo.InvariantCulture, property.PropertyName);
				}
				text += global::Newtonsoft.Json.Utilities.StringUtils.FormatWith(" with type '{0}'.", global::System.Globalization.CultureInfo.InvariantCulture, value.GetType());
				switch (referenceLoopHandling.GetValueOrDefault(Serializer._referenceLoopHandling))
				{
				case global::Newtonsoft.Json.ReferenceLoopHandling.Error:
					throw global::Newtonsoft.Json.JsonSerializationException.Create(null, writer.ContainerPath, text, null);
				case global::Newtonsoft.Json.ReferenceLoopHandling.Ignore:
					if (TraceWriter != null && TraceWriter.LevelFilter >= global::System.Diagnostics.TraceLevel.Verbose)
					{
						TraceWriter.Trace(global::System.Diagnostics.TraceLevel.Verbose, global::Newtonsoft.Json.JsonPosition.FormatMessage(null, writer.Path, text + ". Skipping serializing self referenced value."), null);
					}
					return false;
				case global::Newtonsoft.Json.ReferenceLoopHandling.Serialize:
					if (TraceWriter != null && TraceWriter.LevelFilter >= global::System.Diagnostics.TraceLevel.Verbose)
					{
						TraceWriter.Trace(global::System.Diagnostics.TraceLevel.Verbose, global::Newtonsoft.Json.JsonPosition.FormatMessage(null, writer.Path, text + ". Serializing self referenced value."), null);
					}
					return true;
				}
			}
			return true;
		}

		private void WriteReference(global::Newtonsoft.Json.JsonWriter writer, object value)
		{
			string reference = GetReference(writer, value);
			if (TraceWriter != null && TraceWriter.LevelFilter >= global::System.Diagnostics.TraceLevel.Info)
			{
				TraceWriter.Trace(global::System.Diagnostics.TraceLevel.Info, global::Newtonsoft.Json.JsonPosition.FormatMessage(null, writer.Path, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Writing object reference to Id '{0}' for {1}.", global::System.Globalization.CultureInfo.InvariantCulture, reference, value.GetType())), null);
			}
			writer.WriteStartObject();
			writer.WritePropertyName("$ref", escape: false);
			writer.WriteValue(reference);
			writer.WriteEndObject();
		}

		private string GetReference(global::Newtonsoft.Json.JsonWriter writer, object value)
		{
			try
			{
				return Serializer.GetReferenceResolver().GetReference(this, value);
			}
			catch (global::System.Exception ex)
			{
				throw global::Newtonsoft.Json.JsonSerializationException.Create(null, writer.ContainerPath, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Error writing object reference for '{0}'.", global::System.Globalization.CultureInfo.InvariantCulture, value.GetType()), ex);
			}
		}

		internal static bool TryConvertToString(object value, global::System.Type type, [global::System.Diagnostics.CodeAnalysis.NotNullWhen(true)] out string? s)
		{
			if (global::Newtonsoft.Json.Serialization.JsonTypeReflector.CanTypeDescriptorConvertString(type, out global::System.ComponentModel.TypeConverter typeConverter))
			{
				s = typeConverter.ConvertToInvariantString(value);
				return true;
			}
			if (value is global::System.Type type2)
			{
				s = type2.AssemblyQualifiedName;
				return true;
			}
			s = null;
			return false;
		}

		private void SerializeString(global::Newtonsoft.Json.JsonWriter writer, object value, global::Newtonsoft.Json.Serialization.JsonStringContract contract)
		{
			OnSerializing(writer, contract, value);
			TryConvertToString(value, contract.UnderlyingType, out string s);
			writer.WriteValue(s);
			OnSerialized(writer, contract, value);
		}

		private void OnSerializing(global::Newtonsoft.Json.JsonWriter writer, global::Newtonsoft.Json.Serialization.JsonContract contract, object value)
		{
			if (TraceWriter != null && TraceWriter.LevelFilter >= global::System.Diagnostics.TraceLevel.Info)
			{
				TraceWriter.Trace(global::System.Diagnostics.TraceLevel.Info, global::Newtonsoft.Json.JsonPosition.FormatMessage(null, writer.Path, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Started serializing {0}", global::System.Globalization.CultureInfo.InvariantCulture, contract.UnderlyingType)), null);
			}
			contract.InvokeOnSerializing(value, Serializer._context);
		}

		private void OnSerialized(global::Newtonsoft.Json.JsonWriter writer, global::Newtonsoft.Json.Serialization.JsonContract contract, object value)
		{
			if (TraceWriter != null && TraceWriter.LevelFilter >= global::System.Diagnostics.TraceLevel.Info)
			{
				TraceWriter.Trace(global::System.Diagnostics.TraceLevel.Info, global::Newtonsoft.Json.JsonPosition.FormatMessage(null, writer.Path, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Finished serializing {0}", global::System.Globalization.CultureInfo.InvariantCulture, contract.UnderlyingType)), null);
			}
			contract.InvokeOnSerialized(value, Serializer._context);
		}

		private void SerializeObject(global::Newtonsoft.Json.JsonWriter writer, object value, global::Newtonsoft.Json.Serialization.JsonObjectContract contract, global::Newtonsoft.Json.Serialization.JsonProperty? member, global::Newtonsoft.Json.Serialization.JsonContainerContract? collectionContract, global::Newtonsoft.Json.Serialization.JsonProperty? containerProperty)
		{
			OnSerializing(writer, contract, value);
			_serializeStack.Add(value);
			WriteObjectStart(writer, value, contract, member, collectionContract, containerProperty);
			int top = writer.Top;
			for (int i = 0; i < contract.Properties.Count; i++)
			{
				global::Newtonsoft.Json.Serialization.JsonProperty jsonProperty = contract.Properties[i];
				try
				{
					if (CalculatePropertyValues(writer, value, contract, member, jsonProperty, out global::Newtonsoft.Json.Serialization.JsonContract memberContract, out object memberValue))
					{
						jsonProperty.WritePropertyName(writer);
						SerializeValue(writer, memberValue, memberContract, jsonProperty, contract, member);
					}
				}
				catch (global::System.Exception ex)
				{
					if (IsErrorHandled(value, contract, jsonProperty.PropertyName, null, writer.ContainerPath, ex))
					{
						HandleError(writer, top);
						continue;
					}
					throw;
				}
			}
			global::System.Collections.Generic.IEnumerable<global::System.Collections.Generic.KeyValuePair<object, object>> enumerable = contract.ExtensionDataGetter?.Invoke(value);
			if (enumerable != null)
			{
				foreach (global::System.Collections.Generic.KeyValuePair<object, object> item in enumerable)
				{
					global::Newtonsoft.Json.Serialization.JsonContract contract2 = GetContract(item.Key);
					global::Newtonsoft.Json.Serialization.JsonContract contractSafe = GetContractSafe(item.Value);
					string propertyName = GetPropertyName(writer, item.Key, contract2, out var _);
					propertyName = ((contract.ExtensionDataNameResolver != null) ? contract.ExtensionDataNameResolver(propertyName) : propertyName);
					if (ShouldWriteReference(item.Value, null, contractSafe, contract, member))
					{
						writer.WritePropertyName(propertyName);
						WriteReference(writer, item.Value);
					}
					else if (CheckForCircularReference(writer, item.Value, null, contractSafe, contract, member))
					{
						writer.WritePropertyName(propertyName);
						SerializeValue(writer, item.Value, contractSafe, null, contract, member);
					}
				}
			}
			writer.WriteEndObject();
			_serializeStack.RemoveAt(_serializeStack.Count - 1);
			OnSerialized(writer, contract, value);
		}

		private bool CalculatePropertyValues(global::Newtonsoft.Json.JsonWriter writer, object value, global::Newtonsoft.Json.Serialization.JsonContainerContract contract, global::Newtonsoft.Json.Serialization.JsonProperty? member, global::Newtonsoft.Json.Serialization.JsonProperty property, [global::System.Diagnostics.CodeAnalysis.NotNullWhen(true)] out global::Newtonsoft.Json.Serialization.JsonContract? memberContract, out object? memberValue)
		{
			if (!property.Ignored && property.Readable && ShouldSerialize(writer, property, value) && IsSpecified(writer, property, value))
			{
				if (property.PropertyContract == null)
				{
					property.PropertyContract = Serializer._contractResolver.ResolveContract(property.PropertyType);
				}
				memberValue = property.ValueProvider.GetValue(value);
				memberContract = (property.PropertyContract.IsSealed ? property.PropertyContract : GetContractSafe(memberValue));
				if (ShouldWriteProperty(memberValue, contract as global::Newtonsoft.Json.Serialization.JsonObjectContract, property))
				{
					if (ShouldWriteReference(memberValue, property, memberContract, contract, member))
					{
						property.WritePropertyName(writer);
						WriteReference(writer, memberValue);
						return false;
					}
					if (!CheckForCircularReference(writer, memberValue, property, memberContract, contract, member))
					{
						return false;
					}
					if (memberValue == null)
					{
						global::Newtonsoft.Json.Serialization.JsonObjectContract jsonObjectContract = contract as global::Newtonsoft.Json.Serialization.JsonObjectContract;
						switch (property._required ?? (jsonObjectContract?.ItemRequired).GetValueOrDefault())
						{
						case global::Newtonsoft.Json.Required.Always:
							throw global::Newtonsoft.Json.JsonSerializationException.Create(null, writer.ContainerPath, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Cannot write a null value for property '{0}'. Property requires a value.", global::System.Globalization.CultureInfo.InvariantCulture, property.PropertyName), null);
						case global::Newtonsoft.Json.Required.DisallowNull:
							throw global::Newtonsoft.Json.JsonSerializationException.Create(null, writer.ContainerPath, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Cannot write a null value for property '{0}'. Property requires a non-null value.", global::System.Globalization.CultureInfo.InvariantCulture, property.PropertyName), null);
						}
					}
					return true;
				}
			}
			memberContract = null;
			memberValue = null;
			return false;
		}

		private void WriteObjectStart(global::Newtonsoft.Json.JsonWriter writer, object value, global::Newtonsoft.Json.Serialization.JsonContract contract, global::Newtonsoft.Json.Serialization.JsonProperty? member, global::Newtonsoft.Json.Serialization.JsonContainerContract? collectionContract, global::Newtonsoft.Json.Serialization.JsonProperty? containerProperty)
		{
			writer.WriteStartObject();
			if ((ResolveIsReference(contract, member, collectionContract, containerProperty) ?? HasFlag(Serializer._preserveReferencesHandling, global::Newtonsoft.Json.PreserveReferencesHandling.Objects)) && (member == null || member.Writable || HasCreatorParameter(collectionContract, member)))
			{
				WriteReferenceIdProperty(writer, contract.UnderlyingType, value);
			}
			if (ShouldWriteType(global::Newtonsoft.Json.TypeNameHandling.Objects, contract, member, collectionContract, containerProperty))
			{
				WriteTypeProperty(writer, contract.UnderlyingType);
			}
		}

		private bool HasCreatorParameter(global::Newtonsoft.Json.Serialization.JsonContainerContract? contract, global::Newtonsoft.Json.Serialization.JsonProperty property)
		{
			if (!(contract is global::Newtonsoft.Json.Serialization.JsonObjectContract jsonObjectContract))
			{
				return false;
			}
			return jsonObjectContract.CreatorParameters.Contains(property.PropertyName);
		}

		private void WriteReferenceIdProperty(global::Newtonsoft.Json.JsonWriter writer, global::System.Type type, object value)
		{
			string reference = GetReference(writer, value);
			if (TraceWriter != null && TraceWriter.LevelFilter >= global::System.Diagnostics.TraceLevel.Verbose)
			{
				TraceWriter.Trace(global::System.Diagnostics.TraceLevel.Verbose, global::Newtonsoft.Json.JsonPosition.FormatMessage(null, writer.Path, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Writing object reference Id '{0}' for {1}.", global::System.Globalization.CultureInfo.InvariantCulture, reference, type)), null);
			}
			writer.WritePropertyName("$id", escape: false);
			writer.WriteValue(reference);
		}

		private void WriteTypeProperty(global::Newtonsoft.Json.JsonWriter writer, global::System.Type type)
		{
			string typeName = global::Newtonsoft.Json.Utilities.ReflectionUtils.GetTypeName(type, Serializer._typeNameAssemblyFormatHandling, Serializer._serializationBinder);
			if (TraceWriter != null && TraceWriter.LevelFilter >= global::System.Diagnostics.TraceLevel.Verbose)
			{
				TraceWriter.Trace(global::System.Diagnostics.TraceLevel.Verbose, global::Newtonsoft.Json.JsonPosition.FormatMessage(null, writer.Path, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Writing type name '{0}' for {1}.", global::System.Globalization.CultureInfo.InvariantCulture, typeName, type)), null);
			}
			writer.WritePropertyName("$type", escape: false);
			writer.WriteValue(typeName);
		}

		private bool HasFlag(global::Newtonsoft.Json.DefaultValueHandling value, global::Newtonsoft.Json.DefaultValueHandling flag)
		{
			return (value & flag) == flag;
		}

		private bool HasFlag(global::Newtonsoft.Json.PreserveReferencesHandling value, global::Newtonsoft.Json.PreserveReferencesHandling flag)
		{
			return (value & flag) == flag;
		}

		private bool HasFlag(global::Newtonsoft.Json.TypeNameHandling value, global::Newtonsoft.Json.TypeNameHandling flag)
		{
			return (value & flag) == flag;
		}

		private void SerializeConvertable(global::Newtonsoft.Json.JsonWriter writer, global::Newtonsoft.Json.JsonConverter converter, object value, global::Newtonsoft.Json.Serialization.JsonContract contract, global::Newtonsoft.Json.Serialization.JsonContainerContract? collectionContract, global::Newtonsoft.Json.Serialization.JsonProperty? containerProperty)
		{
			if (ShouldWriteReference(value, null, contract, collectionContract, containerProperty))
			{
				WriteReference(writer, value);
			}
			else if (CheckForCircularReference(writer, value, null, contract, collectionContract, containerProperty))
			{
				_serializeStack.Add(value);
				if (TraceWriter != null && TraceWriter.LevelFilter >= global::System.Diagnostics.TraceLevel.Info)
				{
					TraceWriter.Trace(global::System.Diagnostics.TraceLevel.Info, global::Newtonsoft.Json.JsonPosition.FormatMessage(null, writer.Path, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Started serializing {0} with converter {1}.", global::System.Globalization.CultureInfo.InvariantCulture, value.GetType(), converter.GetType())), null);
				}
				converter.WriteJson(writer, value, GetInternalSerializer());
				if (TraceWriter != null && TraceWriter.LevelFilter >= global::System.Diagnostics.TraceLevel.Info)
				{
					TraceWriter.Trace(global::System.Diagnostics.TraceLevel.Info, global::Newtonsoft.Json.JsonPosition.FormatMessage(null, writer.Path, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Finished serializing {0} with converter {1}.", global::System.Globalization.CultureInfo.InvariantCulture, value.GetType(), converter.GetType())), null);
				}
				_serializeStack.RemoveAt(_serializeStack.Count - 1);
			}
		}

		private void SerializeList(global::Newtonsoft.Json.JsonWriter writer, global::System.Collections.IEnumerable values, global::Newtonsoft.Json.Serialization.JsonArrayContract contract, global::Newtonsoft.Json.Serialization.JsonProperty? member, global::Newtonsoft.Json.Serialization.JsonContainerContract? collectionContract, global::Newtonsoft.Json.Serialization.JsonProperty? containerProperty)
		{
			object obj = ((values is global::Newtonsoft.Json.Utilities.IWrappedCollection wrappedCollection) ? wrappedCollection.UnderlyingCollection : values);
			OnSerializing(writer, contract, obj);
			_serializeStack.Add(obj);
			bool flag = WriteStartArray(writer, obj, contract, member, collectionContract, containerProperty);
			writer.WriteStartArray();
			int top = writer.Top;
			int num = 0;
			foreach (object value in values)
			{
				try
				{
					global::Newtonsoft.Json.Serialization.JsonContract jsonContract = contract.FinalItemContract ?? GetContractSafe(value);
					if (ShouldWriteReference(value, null, jsonContract, contract, member))
					{
						WriteReference(writer, value);
					}
					else if (CheckForCircularReference(writer, value, null, jsonContract, contract, member))
					{
						SerializeValue(writer, value, jsonContract, null, contract, member);
					}
				}
				catch (global::System.Exception ex)
				{
					if (IsErrorHandled(obj, contract, num, null, writer.ContainerPath, ex))
					{
						HandleError(writer, top);
						continue;
					}
					throw;
				}
				finally
				{
					num++;
				}
			}
			writer.WriteEndArray();
			if (flag)
			{
				writer.WriteEndObject();
			}
			_serializeStack.RemoveAt(_serializeStack.Count - 1);
			OnSerialized(writer, contract, obj);
		}

		private void SerializeMultidimensionalArray(global::Newtonsoft.Json.JsonWriter writer, global::System.Array values, global::Newtonsoft.Json.Serialization.JsonArrayContract contract, global::Newtonsoft.Json.Serialization.JsonProperty? member, global::Newtonsoft.Json.Serialization.JsonContainerContract? collectionContract, global::Newtonsoft.Json.Serialization.JsonProperty? containerProperty)
		{
			OnSerializing(writer, contract, values);
			_serializeStack.Add(values);
			bool num = WriteStartArray(writer, values, contract, member, collectionContract, containerProperty);
			SerializeMultidimensionalArray(writer, values, contract, member, writer.Top, global::Newtonsoft.Json.Utilities.CollectionUtils.ArrayEmpty<int>());
			if (num)
			{
				writer.WriteEndObject();
			}
			_serializeStack.RemoveAt(_serializeStack.Count - 1);
			OnSerialized(writer, contract, values);
		}

		private void SerializeMultidimensionalArray(global::Newtonsoft.Json.JsonWriter writer, global::System.Array values, global::Newtonsoft.Json.Serialization.JsonArrayContract contract, global::Newtonsoft.Json.Serialization.JsonProperty? member, int initialDepth, int[] indices)
		{
			int num = indices.Length;
			int[] array = new int[num + 1];
			for (int i = 0; i < num; i++)
			{
				array[i] = indices[i];
			}
			writer.WriteStartArray();
			for (int j = values.GetLowerBound(num); j <= values.GetUpperBound(num); j++)
			{
				array[num] = j;
				if (array.Length == values.Rank)
				{
					object value = values.GetValue(array);
					try
					{
						global::Newtonsoft.Json.Serialization.JsonContract jsonContract = contract.FinalItemContract ?? GetContractSafe(value);
						if (ShouldWriteReference(value, null, jsonContract, contract, member))
						{
							WriteReference(writer, value);
						}
						else if (CheckForCircularReference(writer, value, null, jsonContract, contract, member))
						{
							SerializeValue(writer, value, jsonContract, null, contract, member);
						}
					}
					catch (global::System.Exception ex)
					{
						if (IsErrorHandled(values, contract, j, null, writer.ContainerPath, ex))
						{
							HandleError(writer, initialDepth + 1);
							continue;
						}
						throw;
					}
				}
				else
				{
					SerializeMultidimensionalArray(writer, values, contract, member, initialDepth + 1, array);
				}
			}
			writer.WriteEndArray();
		}

		private bool WriteStartArray(global::Newtonsoft.Json.JsonWriter writer, object values, global::Newtonsoft.Json.Serialization.JsonArrayContract contract, global::Newtonsoft.Json.Serialization.JsonProperty? member, global::Newtonsoft.Json.Serialization.JsonContainerContract? containerContract, global::Newtonsoft.Json.Serialization.JsonProperty? containerProperty)
		{
			bool flag = (ResolveIsReference(contract, member, containerContract, containerProperty) ?? HasFlag(Serializer._preserveReferencesHandling, global::Newtonsoft.Json.PreserveReferencesHandling.Arrays)) && (member == null || member.Writable || HasCreatorParameter(containerContract, member));
			bool flag2 = ShouldWriteType(global::Newtonsoft.Json.TypeNameHandling.Arrays, contract, member, containerContract, containerProperty);
			bool num = flag || flag2;
			if (num)
			{
				writer.WriteStartObject();
				if (flag)
				{
					WriteReferenceIdProperty(writer, contract.UnderlyingType, values);
				}
				if (flag2)
				{
					WriteTypeProperty(writer, values.GetType());
				}
				writer.WritePropertyName("$values", escape: false);
			}
			if (contract.ItemContract == null)
			{
				contract.ItemContract = Serializer._contractResolver.ResolveContract(contract.CollectionItemType ?? typeof(object));
			}
			return num;
		}

		[global::System.Security.SecuritySafeCritical]
		private void SerializeISerializable(global::Newtonsoft.Json.JsonWriter writer, global::System.Runtime.Serialization.ISerializable value, global::Newtonsoft.Json.Serialization.JsonISerializableContract contract, global::Newtonsoft.Json.Serialization.JsonProperty? member, global::Newtonsoft.Json.Serialization.JsonContainerContract? collectionContract, global::Newtonsoft.Json.Serialization.JsonProperty? containerProperty)
		{
			if (!global::Newtonsoft.Json.Serialization.JsonTypeReflector.FullyTrusted)
			{
				string format = "Type '{0}' implements ISerializable but cannot be serialized using the ISerializable interface because the current application is not fully trusted and ISerializable can expose secure data." + global::System.Environment.NewLine + "To fix this error either change the environment to be fully trusted, change the application to not deserialize the type, add JsonObjectAttribute to the type or change the JsonSerializer setting ContractResolver to use a new DefaultContractResolver with IgnoreSerializableInterface set to true." + global::System.Environment.NewLine;
				format = global::Newtonsoft.Json.Utilities.StringUtils.FormatWith(format, global::System.Globalization.CultureInfo.InvariantCulture, value.GetType());
				throw global::Newtonsoft.Json.JsonSerializationException.Create(null, writer.ContainerPath, format, null);
			}
			OnSerializing(writer, contract, value);
			_serializeStack.Add(value);
			WriteObjectStart(writer, value, contract, member, collectionContract, containerProperty);
			global::System.Runtime.Serialization.SerializationInfo serializationInfo = new global::System.Runtime.Serialization.SerializationInfo(contract.UnderlyingType, new global::System.Runtime.Serialization.FormatterConverter());
			value.GetObjectData(serializationInfo, Serializer._context);
			global::System.Runtime.Serialization.SerializationInfoEnumerator enumerator = serializationInfo.GetEnumerator();
			while (enumerator.MoveNext())
			{
				global::System.Runtime.Serialization.SerializationEntry current = enumerator.Current;
				global::Newtonsoft.Json.Serialization.JsonContract contractSafe = GetContractSafe(current.Value);
				if (ShouldWriteReference(current.Value, null, contractSafe, contract, member))
				{
					writer.WritePropertyName(current.Name);
					WriteReference(writer, current.Value);
				}
				else if (CheckForCircularReference(writer, current.Value, null, contractSafe, contract, member))
				{
					writer.WritePropertyName(current.Name);
					SerializeValue(writer, current.Value, contractSafe, null, contract, member);
				}
			}
			writer.WriteEndObject();
			_serializeStack.RemoveAt(_serializeStack.Count - 1);
			OnSerialized(writer, contract, value);
		}

		private void SerializeDynamic(global::Newtonsoft.Json.JsonWriter writer, global::System.Dynamic.IDynamicMetaObjectProvider value, global::Newtonsoft.Json.Serialization.JsonDynamicContract contract, global::Newtonsoft.Json.Serialization.JsonProperty? member, global::Newtonsoft.Json.Serialization.JsonContainerContract? collectionContract, global::Newtonsoft.Json.Serialization.JsonProperty? containerProperty)
		{
			OnSerializing(writer, contract, value);
			_serializeStack.Add(value);
			WriteObjectStart(writer, value, contract, member, collectionContract, containerProperty);
			int top = writer.Top;
			for (int i = 0; i < contract.Properties.Count; i++)
			{
				global::Newtonsoft.Json.Serialization.JsonProperty jsonProperty = contract.Properties[i];
				if (!jsonProperty.HasMemberAttribute)
				{
					continue;
				}
				try
				{
					if (CalculatePropertyValues(writer, value, contract, member, jsonProperty, out global::Newtonsoft.Json.Serialization.JsonContract memberContract, out object memberValue))
					{
						jsonProperty.WritePropertyName(writer);
						SerializeValue(writer, memberValue, memberContract, jsonProperty, contract, member);
					}
				}
				catch (global::System.Exception ex)
				{
					if (IsErrorHandled(value, contract, jsonProperty.PropertyName, null, writer.ContainerPath, ex))
					{
						HandleError(writer, top);
						continue;
					}
					throw;
				}
			}
			foreach (string dynamicMemberName in global::Newtonsoft.Json.Utilities.DynamicUtils.GetDynamicMemberNames(value))
			{
				if (!contract.TryGetMember(value, dynamicMemberName, out object value2))
				{
					continue;
				}
				try
				{
					global::Newtonsoft.Json.Serialization.JsonContract contractSafe = GetContractSafe(value2);
					if (ShouldWriteDynamicProperty(value2) && CheckForCircularReference(writer, value2, null, contractSafe, contract, member))
					{
						string name = ((contract.PropertyNameResolver != null) ? contract.PropertyNameResolver(dynamicMemberName) : dynamicMemberName);
						writer.WritePropertyName(name);
						SerializeValue(writer, value2, contractSafe, null, contract, member);
					}
				}
				catch (global::System.Exception ex2)
				{
					if (IsErrorHandled(value, contract, dynamicMemberName, null, writer.ContainerPath, ex2))
					{
						HandleError(writer, top);
						continue;
					}
					throw;
				}
			}
			writer.WriteEndObject();
			_serializeStack.RemoveAt(_serializeStack.Count - 1);
			OnSerialized(writer, contract, value);
		}

		private bool ShouldWriteDynamicProperty(object? memberValue)
		{
			if (Serializer._nullValueHandling == global::Newtonsoft.Json.NullValueHandling.Ignore && memberValue == null)
			{
				return false;
			}
			if (HasFlag(Serializer._defaultValueHandling, global::Newtonsoft.Json.DefaultValueHandling.Ignore) && (memberValue == null || global::Newtonsoft.Json.Utilities.MiscellaneousUtils.ValueEquals(memberValue, global::Newtonsoft.Json.Utilities.ReflectionUtils.GetDefaultValue(memberValue.GetType()))))
			{
				return false;
			}
			return true;
		}

		private bool ShouldWriteType(global::Newtonsoft.Json.TypeNameHandling typeNameHandlingFlag, global::Newtonsoft.Json.Serialization.JsonContract contract, global::Newtonsoft.Json.Serialization.JsonProperty? member, global::Newtonsoft.Json.Serialization.JsonContainerContract? containerContract, global::Newtonsoft.Json.Serialization.JsonProperty? containerProperty)
		{
			global::Newtonsoft.Json.TypeNameHandling value = member?.TypeNameHandling ?? containerProperty?.ItemTypeNameHandling ?? containerContract?.ItemTypeNameHandling ?? Serializer._typeNameHandling;
			if (HasFlag(value, typeNameHandlingFlag))
			{
				return true;
			}
			if (HasFlag(value, global::Newtonsoft.Json.TypeNameHandling.Auto))
			{
				if (member != null)
				{
					if (contract.NonNullableUnderlyingType != member.PropertyContract.CreatedType)
					{
						return true;
					}
				}
				else if (containerContract != null)
				{
					if (containerContract.ItemContract == null || contract.NonNullableUnderlyingType != containerContract.ItemContract.CreatedType)
					{
						return true;
					}
				}
				else if (_rootType != null && _serializeStack.Count == _rootLevel)
				{
					global::Newtonsoft.Json.Serialization.JsonContract jsonContract = Serializer._contractResolver.ResolveContract(_rootType);
					if (contract.NonNullableUnderlyingType != jsonContract.CreatedType)
					{
						return true;
					}
				}
			}
			return false;
		}

		private void SerializeDictionary(global::Newtonsoft.Json.JsonWriter writer, global::System.Collections.IDictionary values, global::Newtonsoft.Json.Serialization.JsonDictionaryContract contract, global::Newtonsoft.Json.Serialization.JsonProperty? member, global::Newtonsoft.Json.Serialization.JsonContainerContract? collectionContract, global::Newtonsoft.Json.Serialization.JsonProperty? containerProperty)
		{
			object obj = ((values is global::Newtonsoft.Json.Utilities.IWrappedDictionary wrappedDictionary) ? wrappedDictionary.UnderlyingDictionary : values);
			OnSerializing(writer, contract, obj);
			_serializeStack.Add(obj);
			WriteObjectStart(writer, obj, contract, member, collectionContract, containerProperty);
			if (contract.ItemContract == null)
			{
				contract.ItemContract = Serializer._contractResolver.ResolveContract(contract.DictionaryValueType ?? typeof(object));
			}
			if (contract.KeyContract == null)
			{
				contract.KeyContract = Serializer._contractResolver.ResolveContract(contract.DictionaryKeyType ?? typeof(object));
			}
			int top = writer.Top;
			foreach (global::System.Collections.DictionaryEntry value2 in values)
			{
				string propertyName = GetPropertyName(writer, value2.Key, contract.KeyContract, out var escape);
				propertyName = ((contract.DictionaryKeyResolver != null) ? contract.DictionaryKeyResolver(propertyName) : propertyName);
				try
				{
					object value = value2.Value;
					global::Newtonsoft.Json.Serialization.JsonContract jsonContract = contract.FinalItemContract ?? GetContractSafe(value);
					if (ShouldWriteReference(value, null, jsonContract, contract, member))
					{
						writer.WritePropertyName(propertyName, escape);
						WriteReference(writer, value);
					}
					else if (CheckForCircularReference(writer, value, null, jsonContract, contract, member))
					{
						writer.WritePropertyName(propertyName, escape);
						SerializeValue(writer, value, jsonContract, null, contract, member);
					}
				}
				catch (global::System.Exception ex)
				{
					if (IsErrorHandled(obj, contract, propertyName, null, writer.ContainerPath, ex))
					{
						HandleError(writer, top);
						continue;
					}
					throw;
				}
			}
			writer.WriteEndObject();
			_serializeStack.RemoveAt(_serializeStack.Count - 1);
			OnSerialized(writer, contract, obj);
		}

		private string GetPropertyName(global::Newtonsoft.Json.JsonWriter writer, object name, global::Newtonsoft.Json.Serialization.JsonContract contract, out bool escape)
		{
			if (contract.ContractType == global::Newtonsoft.Json.Serialization.JsonContractType.Primitive)
			{
				global::Newtonsoft.Json.Serialization.JsonPrimitiveContract jsonPrimitiveContract = (global::Newtonsoft.Json.Serialization.JsonPrimitiveContract)contract;
				switch (jsonPrimitiveContract.TypeCode)
				{
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.DateTime:
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.DateTimeNullable:
				{
					global::System.DateTime value = global::Newtonsoft.Json.Utilities.DateTimeUtils.EnsureDateTime((global::System.DateTime)name, writer.DateTimeZoneHandling);
					escape = false;
					global::System.IO.StringWriter stringWriter2 = new global::System.IO.StringWriter(global::System.Globalization.CultureInfo.InvariantCulture);
					global::Newtonsoft.Json.Utilities.DateTimeUtils.WriteDateTimeString(stringWriter2, value, writer.DateFormatHandling, writer.DateFormatString, writer.Culture);
					return stringWriter2.ToString();
				}
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.DateTimeOffset:
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.DateTimeOffsetNullable:
				{
					escape = false;
					global::System.IO.StringWriter stringWriter = new global::System.IO.StringWriter(global::System.Globalization.CultureInfo.InvariantCulture);
					global::Newtonsoft.Json.Utilities.DateTimeUtils.WriteDateTimeOffsetString(stringWriter, (global::System.DateTimeOffset)name, writer.DateFormatHandling, writer.DateFormatString, writer.Culture);
					return stringWriter.ToString();
				}
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Double:
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.DoubleNullable:
				{
					double num = (double)name;
					escape = false;
					return num.ToString("R", global::System.Globalization.CultureInfo.InvariantCulture);
				}
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Single:
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.SingleNullable:
				{
					float num2 = (float)name;
					escape = false;
					return num2.ToString("R", global::System.Globalization.CultureInfo.InvariantCulture);
				}
				default:
				{
					escape = true;
					if (jsonPrimitiveContract.IsEnum && global::Newtonsoft.Json.Utilities.EnumUtils.TryToString(jsonPrimitiveContract.NonNullableUnderlyingType, name, null, out string name2))
					{
						return name2;
					}
					return global::System.Convert.ToString(name, global::System.Globalization.CultureInfo.InvariantCulture);
				}
				}
			}
			if (TryConvertToString(name, name.GetType(), out string s))
			{
				escape = true;
				return s;
			}
			escape = true;
			return name.ToString();
		}

		private void HandleError(global::Newtonsoft.Json.JsonWriter writer, int initialDepth)
		{
			ClearErrorContext();
			if (writer.WriteState == global::Newtonsoft.Json.WriteState.Property)
			{
				writer.WriteNull();
			}
			while (writer.Top > initialDepth)
			{
				writer.WriteEnd();
			}
		}

		private bool ShouldSerialize(global::Newtonsoft.Json.JsonWriter writer, global::Newtonsoft.Json.Serialization.JsonProperty property, object target)
		{
			if (property.ShouldSerialize == null)
			{
				return true;
			}
			bool flag = property.ShouldSerialize(target);
			if (TraceWriter != null && TraceWriter.LevelFilter >= global::System.Diagnostics.TraceLevel.Verbose)
			{
				TraceWriter.Trace(global::System.Diagnostics.TraceLevel.Verbose, global::Newtonsoft.Json.JsonPosition.FormatMessage(null, writer.Path, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("ShouldSerialize result for property '{0}' on {1}: {2}", global::System.Globalization.CultureInfo.InvariantCulture, property.PropertyName, property.DeclaringType, flag)), null);
			}
			return flag;
		}

		private bool IsSpecified(global::Newtonsoft.Json.JsonWriter writer, global::Newtonsoft.Json.Serialization.JsonProperty property, object target)
		{
			if (property.GetIsSpecified == null)
			{
				return true;
			}
			bool flag = property.GetIsSpecified(target);
			if (TraceWriter != null && TraceWriter.LevelFilter >= global::System.Diagnostics.TraceLevel.Verbose)
			{
				TraceWriter.Trace(global::System.Diagnostics.TraceLevel.Verbose, global::Newtonsoft.Json.JsonPosition.FormatMessage(null, writer.Path, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("IsSpecified result for property '{0}' on {1}: {2}", global::System.Globalization.CultureInfo.InvariantCulture, property.PropertyName, property.DeclaringType, flag)), null);
			}
			return flag;
		}
	}
}
