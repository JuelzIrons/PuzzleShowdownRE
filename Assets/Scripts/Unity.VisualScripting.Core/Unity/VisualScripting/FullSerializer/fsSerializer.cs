namespace Unity.VisualScripting.FullSerializer
{
	public class fsSerializer
	{
		internal class fsLazyCycleDefinitionWriter
		{
			private global::System.Collections.Generic.Dictionary<int, global::Unity.VisualScripting.FullSerializer.fsData> _pendingDefinitions = new global::System.Collections.Generic.Dictionary<int, global::Unity.VisualScripting.FullSerializer.fsData>();

			private global::System.Collections.Generic.HashSet<int> _references = new global::System.Collections.Generic.HashSet<int>();

			public void WriteDefinition(int id, global::Unity.VisualScripting.FullSerializer.fsData data)
			{
				if (_references.Contains(id))
				{
					EnsureDictionary(data);
					data.AsDictionary[Key_ObjectDefinition] = new global::Unity.VisualScripting.FullSerializer.fsData(id.ToString());
				}
				else
				{
					_pendingDefinitions[id] = data;
				}
			}

			public void WriteReference(int id, global::System.Collections.Generic.Dictionary<string, global::Unity.VisualScripting.FullSerializer.fsData> dict)
			{
				if (_pendingDefinitions.ContainsKey(id))
				{
					global::Unity.VisualScripting.FullSerializer.fsData obj = _pendingDefinitions[id];
					EnsureDictionary(obj);
					obj.AsDictionary[Key_ObjectDefinition] = new global::Unity.VisualScripting.FullSerializer.fsData(id.ToString());
					_pendingDefinitions.Remove(id);
				}
				else
				{
					_references.Add(id);
				}
				dict[Key_ObjectReference] = new global::Unity.VisualScripting.FullSerializer.fsData(id.ToString());
			}

			public void Clear()
			{
				_pendingDefinitions.Clear();
				_references.Clear();
			}
		}

		private readonly global::System.Collections.Generic.List<global::Unity.VisualScripting.FullSerializer.fsConverter> _availableConverters;

		private readonly global::System.Collections.Generic.Dictionary<global::System.Type, global::Unity.VisualScripting.FullSerializer.fsDirectConverter> _availableDirectConverters;

		private readonly global::System.Collections.Generic.List<global::Unity.VisualScripting.FullSerializer.fsObjectProcessor> _processors;

		private readonly global::Unity.VisualScripting.FullSerializer.Internal.fsCyclicReferenceManager _references;

		private readonly global::Unity.VisualScripting.FullSerializer.fsSerializer.fsLazyCycleDefinitionWriter _lazyReferenceWriter;

		private readonly global::System.Collections.Generic.Dictionary<global::System.Type, global::System.Type> _abstractTypeRemap;

		private global::System.Collections.Generic.Dictionary<global::System.Type, global::Unity.VisualScripting.FullSerializer.fsBaseConverter> _cachedConverterTypeInstances;

		private global::System.Collections.Generic.Dictionary<global::System.Type, global::Unity.VisualScripting.FullSerializer.fsBaseConverter> _cachedConverters;

		private global::System.Collections.Generic.Dictionary<global::System.Type, global::System.Collections.Generic.List<global::Unity.VisualScripting.FullSerializer.fsObjectProcessor>> _cachedProcessors;

		public global::Unity.VisualScripting.FullSerializer.fsContext Context;

		public global::Unity.VisualScripting.FullSerializer.fsConfig Config;

		private static global::System.Collections.Generic.HashSet<string> _reservedKeywords;

		private static readonly string Key_ObjectReference;

		private static readonly string Key_ObjectDefinition;

		private static readonly string Key_InstanceType;

		private static readonly string Key_Version;

		private static readonly string Key_Content;

		internal static readonly string Key_UnitDefault;

		internal static readonly string Key_UnitPosition;

		internal static readonly string Key_UnitGuid;

		internal static readonly string Key_UnitFormerType;

		internal static readonly string Key_UnitFormerValue;

		internal static readonly string TypeName_Unit;

		private static readonly global::System.Type Type_Unit;

		internal static readonly string TypeName_MissingType;

		private static readonly global::System.Type Type_MissingType;

		public fsSerializer()
		{
			_cachedConverterTypeInstances = new global::System.Collections.Generic.Dictionary<global::System.Type, global::Unity.VisualScripting.FullSerializer.fsBaseConverter>();
			_cachedConverters = new global::System.Collections.Generic.Dictionary<global::System.Type, global::Unity.VisualScripting.FullSerializer.fsBaseConverter>();
			_cachedProcessors = new global::System.Collections.Generic.Dictionary<global::System.Type, global::System.Collections.Generic.List<global::Unity.VisualScripting.FullSerializer.fsObjectProcessor>>();
			_references = new global::Unity.VisualScripting.FullSerializer.Internal.fsCyclicReferenceManager();
			_lazyReferenceWriter = new global::Unity.VisualScripting.FullSerializer.fsSerializer.fsLazyCycleDefinitionWriter();
			_availableConverters = new global::System.Collections.Generic.List<global::Unity.VisualScripting.FullSerializer.fsConverter>
			{
				new global::Unity.VisualScripting.FullSerializer.fsNullableConverter
				{
					Serializer = this
				},
				new global::Unity.VisualScripting.FullSerializer.fsGuidConverter
				{
					Serializer = this
				},
				new global::Unity.VisualScripting.FullSerializer.fsTypeConverter
				{
					Serializer = this
				},
				new global::Unity.VisualScripting.FullSerializer.fsDateConverter
				{
					Serializer = this
				},
				new global::Unity.VisualScripting.FullSerializer.fsEnumConverter
				{
					Serializer = this
				},
				new global::Unity.VisualScripting.FullSerializer.fsPrimitiveConverter
				{
					Serializer = this
				},
				new global::Unity.VisualScripting.FullSerializer.fsArrayConverter
				{
					Serializer = this
				},
				new global::Unity.VisualScripting.FullSerializer.fsDictionaryConverter
				{
					Serializer = this
				},
				new global::Unity.VisualScripting.FullSerializer.fsIEnumerableConverter
				{
					Serializer = this
				},
				new global::Unity.VisualScripting.FullSerializer.fsKeyValuePairConverter
				{
					Serializer = this
				},
				new global::Unity.VisualScripting.FullSerializer.fsWeakReferenceConverter
				{
					Serializer = this
				},
				new global::Unity.VisualScripting.FullSerializer.fsReflectedConverter
				{
					Serializer = this
				}
			};
			_availableDirectConverters = new global::System.Collections.Generic.Dictionary<global::System.Type, global::Unity.VisualScripting.FullSerializer.fsDirectConverter>();
			_processors = new global::System.Collections.Generic.List<global::Unity.VisualScripting.FullSerializer.fsObjectProcessor>
			{
				new global::Unity.VisualScripting.FullSerializer.fsSerializationCallbackProcessor()
			};
			_processors.Add(new global::Unity.VisualScripting.FullSerializer.fsSerializationCallbackReceiverProcessor());
			_abstractTypeRemap = new global::System.Collections.Generic.Dictionary<global::System.Type, global::System.Type>();
			SetDefaultStorageType(typeof(global::System.Collections.Generic.ICollection<>), typeof(global::System.Collections.Generic.List<>));
			SetDefaultStorageType(typeof(global::System.Collections.Generic.IList<>), typeof(global::System.Collections.Generic.List<>));
			SetDefaultStorageType(typeof(global::System.Collections.Generic.IDictionary<, >), typeof(global::System.Collections.Generic.Dictionary<, >));
			Context = new global::Unity.VisualScripting.FullSerializer.fsContext();
			Config = new global::Unity.VisualScripting.FullSerializer.fsConfig();
			foreach (global::System.Type converter in global::Unity.VisualScripting.FullSerializer.fsConverterRegistrar.Converters)
			{
				AddConverter((global::Unity.VisualScripting.FullSerializer.fsBaseConverter)global::System.Activator.CreateInstance(converter));
			}
		}

		private void RemapAbstractStorageTypeToDefaultType(ref global::System.Type storageType)
		{
			if (!global::Unity.VisualScripting.FullSerializer.Internal.fsPortableReflection.Resolve(storageType).IsInterface && !global::Unity.VisualScripting.FullSerializer.Internal.fsPortableReflection.Resolve(storageType).IsAbstract)
			{
				return;
			}
			global::System.Type value2;
			if (global::Unity.VisualScripting.FullSerializer.Internal.fsPortableReflection.Resolve(storageType).IsGenericType)
			{
				if (_abstractTypeRemap.TryGetValue(global::Unity.VisualScripting.FullSerializer.Internal.fsPortableReflection.Resolve(storageType).GetGenericTypeDefinition(), out var value))
				{
					global::System.Type[] genericArguments = storageType.GetGenericArguments();
					storageType = global::Unity.VisualScripting.FullSerializer.Internal.fsPortableReflection.Resolve(value).MakeGenericType(genericArguments);
				}
			}
			else if (_abstractTypeRemap.TryGetValue(storageType, out value2))
			{
				storageType = value2;
			}
		}

		public void AddProcessor(global::Unity.VisualScripting.FullSerializer.fsObjectProcessor processor)
		{
			_processors.Add(processor);
			_cachedProcessors = new global::System.Collections.Generic.Dictionary<global::System.Type, global::System.Collections.Generic.List<global::Unity.VisualScripting.FullSerializer.fsObjectProcessor>>();
		}

		public void RemoveProcessor<TProcessor>()
		{
			int num = 0;
			while (num < _processors.Count)
			{
				if (_processors[num] is TProcessor)
				{
					_processors.RemoveAt(num);
				}
				else
				{
					num++;
				}
			}
			_cachedProcessors = new global::System.Collections.Generic.Dictionary<global::System.Type, global::System.Collections.Generic.List<global::Unity.VisualScripting.FullSerializer.fsObjectProcessor>>();
		}

		public void SetDefaultStorageType(global::System.Type abstractType, global::System.Type defaultStorageType)
		{
			if (!global::Unity.VisualScripting.FullSerializer.Internal.fsPortableReflection.Resolve(abstractType).IsInterface && !global::Unity.VisualScripting.FullSerializer.Internal.fsPortableReflection.Resolve(abstractType).IsAbstract)
			{
				throw new global::System.ArgumentException("|abstractType| must be an interface or abstract type");
			}
			_abstractTypeRemap[abstractType] = defaultStorageType;
		}

		private global::System.Collections.Generic.List<global::Unity.VisualScripting.FullSerializer.fsObjectProcessor> GetProcessors(global::System.Type type)
		{
			global::Unity.VisualScripting.FullSerializer.fsObjectAttribute attribute = global::Unity.VisualScripting.FullSerializer.Internal.fsPortableReflection.GetAttribute<global::Unity.VisualScripting.FullSerializer.fsObjectAttribute>(type);
			global::System.Collections.Generic.List<global::Unity.VisualScripting.FullSerializer.fsObjectProcessor> value;
			if (attribute != null && attribute.Processor != null)
			{
				global::Unity.VisualScripting.FullSerializer.fsObjectProcessor item = (global::Unity.VisualScripting.FullSerializer.fsObjectProcessor)global::System.Activator.CreateInstance(attribute.Processor);
				value = new global::System.Collections.Generic.List<global::Unity.VisualScripting.FullSerializer.fsObjectProcessor>();
				value.Add(item);
				_cachedProcessors[type] = value;
			}
			else if (!_cachedProcessors.TryGetValue(type, out value))
			{
				value = new global::System.Collections.Generic.List<global::Unity.VisualScripting.FullSerializer.fsObjectProcessor>();
				for (int i = 0; i < _processors.Count; i++)
				{
					global::Unity.VisualScripting.FullSerializer.fsObjectProcessor fsObjectProcessor2 = _processors[i];
					if (fsObjectProcessor2.CanProcess(type))
					{
						value.Add(fsObjectProcessor2);
					}
				}
				_cachedProcessors[type] = value;
			}
			return value;
		}

		public void AddConverter(global::Unity.VisualScripting.FullSerializer.fsBaseConverter converter)
		{
			if (converter.Serializer != null)
			{
				throw new global::System.InvalidOperationException("Cannot add a single converter instance to multiple fsConverters -- please construct a new instance for " + converter);
			}
			if (converter is global::Unity.VisualScripting.FullSerializer.fsDirectConverter)
			{
				global::Unity.VisualScripting.FullSerializer.fsDirectConverter fsDirectConverter2 = (global::Unity.VisualScripting.FullSerializer.fsDirectConverter)converter;
				_availableDirectConverters[fsDirectConverter2.ModelType] = fsDirectConverter2;
			}
			else
			{
				if (!(converter is global::Unity.VisualScripting.FullSerializer.fsConverter))
				{
					throw new global::System.InvalidOperationException("Unable to add converter " + converter?.ToString() + "; the type association strategy is unknown. Please use either fsDirectConverter or fsConverter as your base type.");
				}
				_availableConverters.Insert(0, (global::Unity.VisualScripting.FullSerializer.fsConverter)converter);
			}
			converter.Serializer = this;
			_cachedConverters = new global::System.Collections.Generic.Dictionary<global::System.Type, global::Unity.VisualScripting.FullSerializer.fsBaseConverter>();
		}

		private global::Unity.VisualScripting.FullSerializer.fsBaseConverter GetConverter(global::System.Type type, global::System.Type overrideConverterType)
		{
			if (overrideConverterType != null)
			{
				if (!_cachedConverterTypeInstances.TryGetValue(overrideConverterType, out var value))
				{
					value = (global::Unity.VisualScripting.FullSerializer.fsBaseConverter)global::System.Activator.CreateInstance(overrideConverterType);
					value.Serializer = this;
					_cachedConverterTypeInstances[overrideConverterType] = value;
				}
				return value;
			}
			if (_cachedConverters.TryGetValue(type, out var value2))
			{
				return value2;
			}
			global::Unity.VisualScripting.FullSerializer.fsObjectAttribute attribute = global::Unity.VisualScripting.FullSerializer.Internal.fsPortableReflection.GetAttribute<global::Unity.VisualScripting.FullSerializer.fsObjectAttribute>(type);
			if (attribute != null && attribute.Converter != null)
			{
				value2 = (global::Unity.VisualScripting.FullSerializer.fsBaseConverter)global::System.Activator.CreateInstance(attribute.Converter);
				value2.Serializer = this;
				return _cachedConverters[type] = value2;
			}
			global::Unity.VisualScripting.FullSerializer.fsForwardAttribute attribute2 = global::Unity.VisualScripting.FullSerializer.Internal.fsPortableReflection.GetAttribute<global::Unity.VisualScripting.FullSerializer.fsForwardAttribute>(type);
			if (attribute2 != null)
			{
				value2 = new global::Unity.VisualScripting.FullSerializer.fsForwardConverter(attribute2);
				value2.Serializer = this;
				return _cachedConverters[type] = value2;
			}
			if (!_cachedConverters.TryGetValue(type, out value2))
			{
				if (_availableDirectConverters.ContainsKey(type))
				{
					value2 = _availableDirectConverters[type];
					return _cachedConverters[type] = value2;
				}
				for (int i = 0; i < _availableConverters.Count; i++)
				{
					if (_availableConverters[i].CanProcess(type))
					{
						value2 = _availableConverters[i];
						return _cachedConverters[type] = value2;
					}
				}
			}
			throw new global::System.InvalidOperationException("Internal error -- could not find a converter for " + type);
		}

		public global::Unity.VisualScripting.FullSerializer.fsResult TrySerialize<T>(T instance, out global::Unity.VisualScripting.FullSerializer.fsData data)
		{
			return TrySerialize(typeof(T), instance, out data);
		}

		public global::Unity.VisualScripting.FullSerializer.fsResult TryDeserialize<T>(global::Unity.VisualScripting.FullSerializer.fsData data, ref T instance)
		{
			object result = instance;
			global::Unity.VisualScripting.FullSerializer.fsResult result2 = TryDeserialize(data, typeof(T), ref result);
			if (result2.Succeeded)
			{
				instance = (T)result;
			}
			return result2;
		}

		public global::Unity.VisualScripting.FullSerializer.fsResult TrySerialize(global::System.Type storageType, object instance, out global::Unity.VisualScripting.FullSerializer.fsData data)
		{
			return TrySerialize(storageType, null, instance, out data);
		}

		public global::Unity.VisualScripting.FullSerializer.fsResult TrySerialize(global::System.Type storageType, global::System.Type overrideConverterType, object instance, out global::Unity.VisualScripting.FullSerializer.fsData data)
		{
			global::System.Collections.Generic.List<global::Unity.VisualScripting.FullSerializer.fsObjectProcessor> processors = GetProcessors((instance == null) ? storageType : instance.GetType());
			try
			{
				Invoke_OnBeforeSerialize(processors, storageType, instance);
			}
			catch (global::System.Exception ex)
			{
				data = new global::Unity.VisualScripting.FullSerializer.fsData();
				return global::Unity.VisualScripting.FullSerializer.fsResult.Fail(ex.ToString());
			}
			if (instance == null)
			{
				data = new global::Unity.VisualScripting.FullSerializer.fsData();
				Invoke_OnAfterSerialize(processors, storageType, instance, ref data);
				return global::Unity.VisualScripting.FullSerializer.fsResult.Success;
			}
			global::Unity.VisualScripting.FullSerializer.fsResult result = InternalSerialize_1_ProcessCycles(storageType, overrideConverterType, instance, out data);
			try
			{
				Invoke_OnAfterSerialize(processors, storageType, instance, ref data);
			}
			catch (global::System.Exception ex2)
			{
				result += global::Unity.VisualScripting.FullSerializer.fsResult.Fail(ex2.ToString());
			}
			return result;
		}

		private global::Unity.VisualScripting.FullSerializer.fsResult InternalSerialize_1_ProcessCycles(global::System.Type storageType, global::System.Type overrideConverterType, object instance, out global::Unity.VisualScripting.FullSerializer.fsData data)
		{
			try
			{
				_references.Enter();
				if (!GetConverter(instance.GetType(), overrideConverterType).RequestCycleSupport(instance.GetType()))
				{
					return InternalSerialize_2_Inheritance(storageType, overrideConverterType, instance, out data);
				}
				if (_references.IsReference(instance))
				{
					data = global::Unity.VisualScripting.FullSerializer.fsData.CreateDictionary();
					_lazyReferenceWriter.WriteReference(_references.GetReferenceId(instance), data.AsDictionary);
					return global::Unity.VisualScripting.FullSerializer.fsResult.Success;
				}
				_references.MarkSerialized(instance);
				global::Unity.VisualScripting.FullSerializer.fsResult result = InternalSerialize_2_Inheritance(storageType, overrideConverterType, instance, out data);
				if (result.Failed)
				{
					return result;
				}
				_lazyReferenceWriter.WriteDefinition(_references.GetReferenceId(instance), data);
				return result;
			}
			finally
			{
				if (_references.Exit())
				{
					_lazyReferenceWriter.Clear();
				}
			}
		}

		private global::Unity.VisualScripting.FullSerializer.fsResult InternalSerialize_2_Inheritance(global::System.Type storageType, global::System.Type overrideConverterType, object instance, out global::Unity.VisualScripting.FullSerializer.fsData data)
		{
			global::Unity.VisualScripting.FullSerializer.fsResult result = InternalSerialize_3_ProcessVersioning(overrideConverterType, instance, out data);
			if (result.Failed)
			{
				return result;
			}
			if (storageType != instance.GetType() && GetConverter(storageType, overrideConverterType).RequestInheritanceSupport(storageType))
			{
				global::System.Type type = instance.GetType();
				if (instance is global::UnityEngine.Object)
				{
					global::System.Type type2 = type;
					do
					{
						type = type2;
						type2 = type2.BaseType;
					}
					while (type2 != null && type != typeof(global::UnityEngine.Object) && storageType.IsAssignableFrom(type2));
				}
				EnsureDictionary(data);
				data.AsDictionary[Key_InstanceType] = new global::Unity.VisualScripting.FullSerializer.fsData(global::Unity.VisualScripting.RuntimeCodebase.SerializeType(type));
			}
			return result;
		}

		private global::Unity.VisualScripting.FullSerializer.fsResult InternalSerialize_3_ProcessVersioning(global::System.Type overrideConverterType, object instance, out global::Unity.VisualScripting.FullSerializer.fsData data)
		{
			global::Unity.VisualScripting.FullSerializer.Internal.fsOption<global::Unity.VisualScripting.FullSerializer.Internal.fsVersionedType> versionedType = global::Unity.VisualScripting.FullSerializer.Internal.fsVersionManager.GetVersionedType(instance.GetType());
			if (versionedType.HasValue)
			{
				global::Unity.VisualScripting.FullSerializer.Internal.fsVersionedType value = versionedType.Value;
				global::Unity.VisualScripting.FullSerializer.fsResult result = InternalSerialize_4_Converter(overrideConverterType, instance, out data);
				if (result.Failed)
				{
					return result;
				}
				EnsureDictionary(data);
				data.AsDictionary[Key_Version] = new global::Unity.VisualScripting.FullSerializer.fsData(value.VersionString);
				return result;
			}
			return InternalSerialize_4_Converter(overrideConverterType, instance, out data);
		}

		private global::Unity.VisualScripting.FullSerializer.fsResult InternalSerialize_4_Converter(global::System.Type overrideConverterType, object instance, out global::Unity.VisualScripting.FullSerializer.fsData data)
		{
			global::System.Type type = instance.GetType();
			return GetConverter(type, overrideConverterType).TrySerialize(instance, out data, type);
		}

		public global::Unity.VisualScripting.FullSerializer.fsResult TryDeserialize(global::Unity.VisualScripting.FullSerializer.fsData data, global::System.Type storageType, ref object result)
		{
			return TryDeserialize(data, storageType, null, ref result);
		}

		public global::Unity.VisualScripting.FullSerializer.fsResult TryDeserialize(global::Unity.VisualScripting.FullSerializer.fsData data, global::System.Type storageType, global::System.Type overrideConverterType, ref object result)
		{
			if (data.IsNull)
			{
				result = null;
				global::System.Collections.Generic.List<global::Unity.VisualScripting.FullSerializer.fsObjectProcessor> processors = GetProcessors(storageType);
				Invoke_OnBeforeDeserialize(processors, storageType, ref data);
				Invoke_OnAfterDeserialize(processors, storageType, null);
				return global::Unity.VisualScripting.FullSerializer.fsResult.Success;
			}
			ConvertLegacyData(ref data);
			try
			{
				_references.Enter();
				global::System.Collections.Generic.List<global::Unity.VisualScripting.FullSerializer.fsObjectProcessor> processors2;
				global::Unity.VisualScripting.FullSerializer.fsResult result2 = InternalDeserialize_1_CycleReference(overrideConverterType, data, storageType, ref result, out processors2);
				if (result2.Succeeded)
				{
					try
					{
						Invoke_OnAfterDeserialize(processors2, storageType, result);
					}
					catch (global::System.Exception ex)
					{
						result2 += global::Unity.VisualScripting.FullSerializer.fsResult.Fail(ex.ToString());
					}
				}
				return result2;
			}
			finally
			{
				_references.Exit();
			}
		}

		private global::Unity.VisualScripting.FullSerializer.fsResult InternalDeserialize_1_CycleReference(global::System.Type overrideConverterType, global::Unity.VisualScripting.FullSerializer.fsData data, global::System.Type storageType, ref object result, out global::System.Collections.Generic.List<global::Unity.VisualScripting.FullSerializer.fsObjectProcessor> processors)
		{
			if (IsObjectReference(data))
			{
				int id = int.Parse(data.AsDictionary[Key_ObjectReference].AsString);
				result = _references.GetReferenceObject(id);
				processors = GetProcessors(result.GetType());
				return global::Unity.VisualScripting.FullSerializer.fsResult.Success;
			}
			return InternalDeserialize_2_Version(overrideConverterType, data, storageType, ref result, out processors);
		}

		private global::Unity.VisualScripting.FullSerializer.fsResult InternalDeserialize_2_Version(global::System.Type overrideConverterType, global::Unity.VisualScripting.FullSerializer.fsData data, global::System.Type storageType, ref object result, out global::System.Collections.Generic.List<global::Unity.VisualScripting.FullSerializer.fsObjectProcessor> processors)
		{
			if (IsVersioned(data))
			{
				string asString = data.AsDictionary[Key_Version].AsString;
				global::Unity.VisualScripting.FullSerializer.Internal.fsOption<global::Unity.VisualScripting.FullSerializer.Internal.fsVersionedType> versionedType = global::Unity.VisualScripting.FullSerializer.Internal.fsVersionManager.GetVersionedType(storageType);
				if (versionedType.HasValue && versionedType.Value.VersionString != asString)
				{
					global::Unity.VisualScripting.FullSerializer.fsResult success = global::Unity.VisualScripting.FullSerializer.fsResult.Success;
					success += global::Unity.VisualScripting.FullSerializer.Internal.fsVersionManager.GetVersionImportPath(asString, versionedType.Value, out var path);
					if (success.Failed)
					{
						processors = GetProcessors(storageType);
						return success;
					}
					success += InternalDeserialize_3_Inheritance(overrideConverterType, data, path[0].ModelType, ref result, out processors);
					if (success.Failed)
					{
						return success;
					}
					for (int i = 1; i < path.Count; i++)
					{
						result = path[i].Migrate(result);
					}
					if (IsObjectDefinition(data))
					{
						int id = int.Parse(data.AsDictionary[Key_ObjectDefinition].AsString);
						_references.AddReferenceWithId(id, result);
					}
					processors = GetProcessors(success.GetType());
					return success;
				}
			}
			return InternalDeserialize_3_Inheritance(overrideConverterType, data, storageType, ref result, out processors);
		}

		private global::Unity.VisualScripting.FullSerializer.fsResult InternalDeserialize_3_Inheritance(global::System.Type overrideConverterType, global::Unity.VisualScripting.FullSerializer.fsData data, global::System.Type storageType, ref object result, out global::System.Collections.Generic.List<global::Unity.VisualScripting.FullSerializer.fsObjectProcessor> processors)
		{
			global::Unity.VisualScripting.FullSerializer.fsResult deserializeResult = global::Unity.VisualScripting.FullSerializer.fsResult.Success;
			global::System.Type storageType2 = storageType;
			if (IsTypeSpecified(data))
			{
				storageType2 = GetDataType(ref data, storageType, ref deserializeResult);
			}
			RemapAbstractStorageTypeToDefaultType(ref storageType2);
			processors = GetProcessors(storageType2);
			if (deserializeResult.Failed)
			{
				return deserializeResult;
			}
			try
			{
				Invoke_OnBeforeDeserialize(processors, storageType, ref data);
			}
			catch (global::System.Exception ex)
			{
				return deserializeResult + global::Unity.VisualScripting.FullSerializer.fsResult.Fail(ex.ToString());
			}
			if (result == null || result.GetType() != storageType2)
			{
				result = GetConverter(storageType2, overrideConverterType).CreateInstance(data, storageType2);
			}
			try
			{
				Invoke_OnBeforeDeserializeAfterInstanceCreation(processors, storageType, result, ref data);
			}
			catch (global::System.Exception ex2)
			{
				return deserializeResult + global::Unity.VisualScripting.FullSerializer.fsResult.Fail(ex2.ToString());
			}
			return deserializeResult + InternalDeserialize_4_Cycles(overrideConverterType, data, storageType2, ref result);
		}

		private global::Unity.VisualScripting.FullSerializer.fsResult InternalDeserialize_4_Cycles(global::System.Type overrideConverterType, global::Unity.VisualScripting.FullSerializer.fsData data, global::System.Type resultType, ref object result)
		{
			if (IsObjectDefinition(data))
			{
				int id = int.Parse(data.AsDictionary[Key_ObjectDefinition].AsString);
				_references.AddReferenceWithId(id, result);
			}
			return InternalDeserialize_5_Converter(overrideConverterType, data, resultType, ref result);
		}

		private global::Unity.VisualScripting.FullSerializer.fsResult InternalDeserialize_5_Converter(global::System.Type overrideConverterType, global::Unity.VisualScripting.FullSerializer.fsData data, global::System.Type resultType, ref object result)
		{
			if (IsWrappedData(data))
			{
				data = data.AsDictionary[Key_Content];
			}
			return GetConverter(resultType, overrideConverterType).TryDeserialize(data, ref result, resultType);
		}

		private static global::System.Type GetDataType(ref global::Unity.VisualScripting.FullSerializer.fsData data, global::System.Type defaultType, ref global::Unity.VisualScripting.FullSerializer.fsResult deserializeResult)
		{
			global::System.Collections.Generic.Dictionary<string, global::Unity.VisualScripting.FullSerializer.fsData> asDictionary = data.AsDictionary;
			global::Unity.VisualScripting.FullSerializer.fsData fsData2 = asDictionary[Key_InstanceType];
			if (!fsData2.IsString)
			{
				deserializeResult.AddMessage(Key_InstanceType + " value must be a string (in " + data?.ToString() + ")");
				return defaultType;
			}
			string asString = fsData2.AsString;
			if (!global::Unity.VisualScripting.RuntimeCodebase.TryDeserializeType(asString, out var type))
			{
				if (IsVisualScriptingUnit(data))
				{
					asDictionary[Key_UnitFormerValue] = new global::Unity.VisualScripting.FullSerializer.fsData(data.ToString());
					asDictionary[Key_UnitFormerType] = fsData2;
					asDictionary[Key_InstanceType] = new global::Unity.VisualScripting.FullSerializer.fsData(TypeName_MissingType);
					deserializeResult += global::Unity.VisualScripting.FullSerializer.fsResult.Warn("Type definition for '" + asString + "' is missing.\nConverted '" + asString + "' unit to '" + TypeName_MissingType + "'. Did you delete the type's script file?");
					return Type_MissingType;
				}
				deserializeResult += global::Unity.VisualScripting.FullSerializer.fsResult.Warn("Unable to find type: \"" + asString + "\"");
				return defaultType;
			}
			if (asString == TypeName_MissingType)
			{
				if (asDictionary.ContainsKey(Key_UnitFormerType) && IsVisualScriptingUnit(data))
				{
					string asString2 = asDictionary[Key_UnitFormerType].AsString;
					if (global::Unity.VisualScripting.RuntimeCodebase.TryDeserializeType(asString2, out var type2))
					{
						if (defaultType.IsAssignableFrom(type2))
						{
							if (asDictionary.ContainsKey(Key_UnitFormerValue))
							{
								global::Unity.VisualScripting.FullSerializer.fsData value = asDictionary[Key_UnitPosition];
								data = global::Unity.VisualScripting.FullSerializer.fsJsonParser.Parse(asDictionary[Key_UnitFormerValue].AsString);
								asDictionary = data.AsDictionary;
								asDictionary[Key_UnitPosition] = value;
								deserializeResult += global::Unity.VisualScripting.FullSerializer.fsResult.Warn("Missing unit type '" + asString2 + "' was found.\nConverted '" + TypeName_MissingType + "' unit back to '" + asString2 + "'");
							}
							else
							{
								asDictionary[Key_InstanceType] = new global::Unity.VisualScripting.FullSerializer.fsData(asString2);
								deserializeResult += global::Unity.VisualScripting.FullSerializer.fsResult.Warn("Missing unit type '" + asString2 + "' was found.\nConverted '" + TypeName_MissingType + "' unit back to '" + asString2 + "'\nNo former state can be found. Reverting node to defaults.\n" + data);
							}
							return type2;
						}
						deserializeResult += global::Unity.VisualScripting.FullSerializer.fsResult.Warn("Missing unit type '" + asString2 + "' was found, but is not assignable to '" + defaultType.FullName + "'. Did you forget to inherit from '" + TypeName_Unit + "'?");
					}
					else
					{
						deserializeResult += global::Unity.VisualScripting.FullSerializer.fsResult.Warn("Type definition for '" + asString2 + "' unit is missing. Did you remove its script file?");
					}
				}
				else
				{
					deserializeResult += global::Unity.VisualScripting.FullSerializer.fsResult.Warn("Serialized '" + TypeName_MissingType + "' unit has an unrecognized format.");
				}
			}
			if (!defaultType.IsAssignableFrom(type))
			{
				if (IsVisualScriptingUnit(data))
				{
					asDictionary[Key_UnitFormerType] = fsData2;
					asDictionary[Key_InstanceType] = new global::Unity.VisualScripting.FullSerializer.fsData(TypeName_MissingType);
					deserializeResult += global::Unity.VisualScripting.FullSerializer.fsResult.Warn("Type '" + asString + "' is no longer assignable to '" + defaultType.FullName + "'. Did you remove inheritance from '" + TypeName_Unit + "'?\nConverted '" + asString + "' unit to '" + TypeName_MissingType + "'.");
					return Type_MissingType;
				}
				deserializeResult.AddMessage("Ignoring type specifier; a field/property of type " + defaultType?.ToString() + " cannot hold an instance of " + type);
				return defaultType;
			}
			return type;
		}

		private static void EnsureDictionary(global::Unity.VisualScripting.FullSerializer.fsData data)
		{
			if (!data.IsDictionary)
			{
				global::Unity.VisualScripting.FullSerializer.fsData value = data.Clone();
				data.BecomeDictionary();
				data.AsDictionary[Key_Content] = value;
			}
		}

		static fsSerializer()
		{
			Key_ObjectReference = global::Unity.VisualScripting.FullSerializer.fsGlobalConfig.InternalFieldPrefix + "ref";
			Key_ObjectDefinition = global::Unity.VisualScripting.FullSerializer.fsGlobalConfig.InternalFieldPrefix + "id";
			Key_InstanceType = global::Unity.VisualScripting.FullSerializer.fsGlobalConfig.InternalFieldPrefix + "type";
			Key_Version = global::Unity.VisualScripting.FullSerializer.fsGlobalConfig.InternalFieldPrefix + "version";
			Key_Content = global::Unity.VisualScripting.FullSerializer.fsGlobalConfig.InternalFieldPrefix + "content";
			Key_UnitDefault = "defaultValues";
			Key_UnitPosition = "position";
			Key_UnitGuid = "guid";
			Key_UnitFormerType = "formerType";
			Key_UnitFormerValue = "formerValue";
			TypeName_Unit = "Unity.VisualScripting.Unit";
			Type_Unit = global::Unity.VisualScripting.RuntimeCodebase.DeserializeType(TypeName_Unit);
			TypeName_MissingType = "Unity.VisualScripting.MissingType";
			Type_MissingType = global::Unity.VisualScripting.RuntimeCodebase.DeserializeType(TypeName_MissingType);
			_reservedKeywords = new global::System.Collections.Generic.HashSet<string> { Key_ObjectReference, Key_ObjectDefinition, Key_InstanceType, Key_Version, Key_Content };
		}

		public static bool IsReservedKeyword(string key)
		{
			return _reservedKeywords.Contains(key);
		}

		private static bool IsObjectReference(global::Unity.VisualScripting.FullSerializer.fsData data)
		{
			if (!data.IsDictionary)
			{
				return false;
			}
			return data.AsDictionary.ContainsKey(Key_ObjectReference);
		}

		private static bool IsObjectDefinition(global::Unity.VisualScripting.FullSerializer.fsData data)
		{
			if (!data.IsDictionary)
			{
				return false;
			}
			return data.AsDictionary.ContainsKey(Key_ObjectDefinition);
		}

		private static bool IsVersioned(global::Unity.VisualScripting.FullSerializer.fsData data)
		{
			if (!data.IsDictionary)
			{
				return false;
			}
			return data.AsDictionary.ContainsKey(Key_Version);
		}

		private static bool IsTypeSpecified(global::Unity.VisualScripting.FullSerializer.fsData data)
		{
			if (!data.IsDictionary)
			{
				return false;
			}
			return data.AsDictionary.ContainsKey(Key_InstanceType);
		}

		private static bool IsWrappedData(global::Unity.VisualScripting.FullSerializer.fsData data)
		{
			if (!data.IsDictionary)
			{
				return false;
			}
			return data.AsDictionary.ContainsKey(Key_Content);
		}

		private static bool IsVisualScriptingUnit(global::Unity.VisualScripting.FullSerializer.fsData data)
		{
			if (!data.IsDictionary)
			{
				return false;
			}
			global::System.Collections.Generic.Dictionary<string, global::Unity.VisualScripting.FullSerializer.fsData> asDictionary = data.AsDictionary;
			if (asDictionary.ContainsKey(Key_UnitDefault) && asDictionary.ContainsKey(Key_UnitPosition) && asDictionary.ContainsKey(Key_UnitGuid) && asDictionary[Key_UnitPosition].AsDictionary.ContainsKey("x"))
			{
				return asDictionary[Key_UnitPosition].AsDictionary.ContainsKey("y");
			}
			return false;
		}

		public static void StripDeserializationMetadata(ref global::Unity.VisualScripting.FullSerializer.fsData data)
		{
			if (data.IsDictionary && data.AsDictionary.ContainsKey(Key_Content))
			{
				data = data.AsDictionary[Key_Content];
			}
			if (data.IsDictionary)
			{
				global::System.Collections.Generic.Dictionary<string, global::Unity.VisualScripting.FullSerializer.fsData> asDictionary = data.AsDictionary;
				asDictionary.Remove(Key_ObjectReference);
				asDictionary.Remove(Key_ObjectDefinition);
				asDictionary.Remove(Key_InstanceType);
				asDictionary.Remove(Key_Version);
			}
		}

		private static void ConvertLegacyData(ref global::Unity.VisualScripting.FullSerializer.fsData data)
		{
			if (!data.IsDictionary)
			{
				return;
			}
			global::System.Collections.Generic.Dictionary<string, global::Unity.VisualScripting.FullSerializer.fsData> asDictionary = data.AsDictionary;
			if (asDictionary.Count <= 2)
			{
				string key = "ReferenceId";
				string key2 = "SourceId";
				string key3 = "Data";
				string key4 = "Type";
				string key5 = "Data";
				if (asDictionary.Count == 2 && asDictionary.ContainsKey(key4) && asDictionary.ContainsKey(key5))
				{
					data = asDictionary[key5];
					EnsureDictionary(data);
					ConvertLegacyData(ref data);
					data.AsDictionary[Key_InstanceType] = asDictionary[key4];
				}
				else if (asDictionary.Count == 2 && asDictionary.ContainsKey(key2) && asDictionary.ContainsKey(key3))
				{
					data = asDictionary[key3];
					EnsureDictionary(data);
					ConvertLegacyData(ref data);
					data.AsDictionary[Key_ObjectDefinition] = asDictionary[key2];
				}
				else if (asDictionary.Count == 1 && asDictionary.ContainsKey(key))
				{
					data = global::Unity.VisualScripting.FullSerializer.fsData.CreateDictionary();
					data.AsDictionary[Key_ObjectReference] = asDictionary[key];
				}
			}
		}

		private static void Invoke_OnBeforeSerialize(global::System.Collections.Generic.List<global::Unity.VisualScripting.FullSerializer.fsObjectProcessor> processors, global::System.Type storageType, object instance)
		{
			for (int i = 0; i < processors.Count; i++)
			{
				processors[i].OnBeforeSerialize(storageType, instance);
			}
		}

		private static void Invoke_OnAfterSerialize(global::System.Collections.Generic.List<global::Unity.VisualScripting.FullSerializer.fsObjectProcessor> processors, global::System.Type storageType, object instance, ref global::Unity.VisualScripting.FullSerializer.fsData data)
		{
			for (int num = processors.Count - 1; num >= 0; num--)
			{
				processors[num].OnAfterSerialize(storageType, instance, ref data);
			}
		}

		private static void Invoke_OnBeforeDeserialize(global::System.Collections.Generic.List<global::Unity.VisualScripting.FullSerializer.fsObjectProcessor> processors, global::System.Type storageType, ref global::Unity.VisualScripting.FullSerializer.fsData data)
		{
			for (int i = 0; i < processors.Count; i++)
			{
				processors[i].OnBeforeDeserialize(storageType, ref data);
			}
		}

		private static void Invoke_OnBeforeDeserializeAfterInstanceCreation(global::System.Collections.Generic.List<global::Unity.VisualScripting.FullSerializer.fsObjectProcessor> processors, global::System.Type storageType, object instance, ref global::Unity.VisualScripting.FullSerializer.fsData data)
		{
			for (int i = 0; i < processors.Count; i++)
			{
				processors[i].OnBeforeDeserializeAfterInstanceCreation(storageType, instance, ref data);
			}
		}

		private static void Invoke_OnAfterDeserialize(global::System.Collections.Generic.List<global::Unity.VisualScripting.FullSerializer.fsObjectProcessor> processors, global::System.Type storageType, object instance)
		{
			for (int num = processors.Count - 1; num >= 0; num--)
			{
				processors[num].OnAfterDeserialize(storageType, instance);
			}
		}
	}
}
