namespace Newtonsoft.Json
{
	public class JsonSerializer
	{
		internal global::Newtonsoft.Json.TypeNameHandling _typeNameHandling;

		internal global::Newtonsoft.Json.TypeNameAssemblyFormatHandling _typeNameAssemblyFormatHandling;

		internal global::Newtonsoft.Json.PreserveReferencesHandling _preserveReferencesHandling;

		internal global::Newtonsoft.Json.ReferenceLoopHandling _referenceLoopHandling;

		internal global::Newtonsoft.Json.MissingMemberHandling _missingMemberHandling;

		internal global::Newtonsoft.Json.ObjectCreationHandling _objectCreationHandling;

		internal global::Newtonsoft.Json.NullValueHandling _nullValueHandling;

		internal global::Newtonsoft.Json.DefaultValueHandling _defaultValueHandling;

		internal global::Newtonsoft.Json.ConstructorHandling _constructorHandling;

		internal global::Newtonsoft.Json.MetadataPropertyHandling _metadataPropertyHandling;

		internal global::Newtonsoft.Json.JsonConverterCollection? _converters;

		internal global::Newtonsoft.Json.Serialization.IContractResolver _contractResolver;

		internal global::Newtonsoft.Json.Serialization.ITraceWriter? _traceWriter;

		internal global::System.Collections.IEqualityComparer? _equalityComparer;

		internal global::Newtonsoft.Json.Serialization.ISerializationBinder _serializationBinder;

		internal global::System.Runtime.Serialization.StreamingContext _context;

		private global::Newtonsoft.Json.Serialization.IReferenceResolver? _referenceResolver;

		private global::Newtonsoft.Json.Formatting? _formatting;

		private global::Newtonsoft.Json.DateFormatHandling? _dateFormatHandling;

		private global::Newtonsoft.Json.DateTimeZoneHandling? _dateTimeZoneHandling;

		private global::Newtonsoft.Json.DateParseHandling? _dateParseHandling;

		private global::Newtonsoft.Json.FloatFormatHandling? _floatFormatHandling;

		private global::Newtonsoft.Json.FloatParseHandling? _floatParseHandling;

		private global::Newtonsoft.Json.StringEscapeHandling? _stringEscapeHandling;

		private global::System.Globalization.CultureInfo _culture;

		private int? _maxDepth;

		private bool _maxDepthSet;

		private bool? _checkAdditionalContent;

		private string? _dateFormatString;

		private bool _dateFormatStringSet;

		public virtual global::Newtonsoft.Json.Serialization.IReferenceResolver? ReferenceResolver
		{
			get
			{
				return GetReferenceResolver();
			}
			set
			{
				if (value == null)
				{
					throw new global::System.ArgumentNullException("value", "Reference resolver cannot be null.");
				}
				_referenceResolver = value;
			}
		}

		[global::System.Obsolete("Binder is obsolete. Use SerializationBinder instead.")]
		public virtual global::System.Runtime.Serialization.SerializationBinder Binder
		{
			get
			{
				if (_serializationBinder is global::System.Runtime.Serialization.SerializationBinder result)
				{
					return result;
				}
				if (_serializationBinder is global::Newtonsoft.Json.Serialization.SerializationBinderAdapter serializationBinderAdapter)
				{
					return serializationBinderAdapter.SerializationBinder;
				}
				throw new global::System.InvalidOperationException("Cannot get SerializationBinder because an ISerializationBinder was previously set.");
			}
			set
			{
				if (value == null)
				{
					throw new global::System.ArgumentNullException("value", "Serialization binder cannot be null.");
				}
				_serializationBinder = (value as global::Newtonsoft.Json.Serialization.ISerializationBinder) ?? new global::Newtonsoft.Json.Serialization.SerializationBinderAdapter(value);
			}
		}

		public virtual global::Newtonsoft.Json.Serialization.ISerializationBinder SerializationBinder
		{
			get
			{
				return _serializationBinder;
			}
			set
			{
				if (value == null)
				{
					throw new global::System.ArgumentNullException("value", "Serialization binder cannot be null.");
				}
				_serializationBinder = value;
			}
		}

		public virtual global::Newtonsoft.Json.Serialization.ITraceWriter? TraceWriter
		{
			get
			{
				return _traceWriter;
			}
			set
			{
				_traceWriter = value;
			}
		}

		public virtual global::System.Collections.IEqualityComparer? EqualityComparer
		{
			get
			{
				return _equalityComparer;
			}
			set
			{
				_equalityComparer = value;
			}
		}

		public virtual global::Newtonsoft.Json.TypeNameHandling TypeNameHandling
		{
			get
			{
				return _typeNameHandling;
			}
			set
			{
				if (value < global::Newtonsoft.Json.TypeNameHandling.None || value > global::Newtonsoft.Json.TypeNameHandling.Auto)
				{
					throw new global::System.ArgumentOutOfRangeException("value");
				}
				_typeNameHandling = value;
			}
		}

		[global::System.Obsolete("TypeNameAssemblyFormat is obsolete. Use TypeNameAssemblyFormatHandling instead.")]
		public virtual global::System.Runtime.Serialization.Formatters.FormatterAssemblyStyle TypeNameAssemblyFormat
		{
			get
			{
				return (global::System.Runtime.Serialization.Formatters.FormatterAssemblyStyle)_typeNameAssemblyFormatHandling;
			}
			set
			{
				if (value < global::System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple || value > global::System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Full)
				{
					throw new global::System.ArgumentOutOfRangeException("value");
				}
				_typeNameAssemblyFormatHandling = (global::Newtonsoft.Json.TypeNameAssemblyFormatHandling)value;
			}
		}

		public virtual global::Newtonsoft.Json.TypeNameAssemblyFormatHandling TypeNameAssemblyFormatHandling
		{
			get
			{
				return _typeNameAssemblyFormatHandling;
			}
			set
			{
				if (value < global::Newtonsoft.Json.TypeNameAssemblyFormatHandling.Simple || value > global::Newtonsoft.Json.TypeNameAssemblyFormatHandling.Full)
				{
					throw new global::System.ArgumentOutOfRangeException("value");
				}
				_typeNameAssemblyFormatHandling = value;
			}
		}

		public virtual global::Newtonsoft.Json.PreserveReferencesHandling PreserveReferencesHandling
		{
			get
			{
				return _preserveReferencesHandling;
			}
			set
			{
				if (value < global::Newtonsoft.Json.PreserveReferencesHandling.None || value > global::Newtonsoft.Json.PreserveReferencesHandling.All)
				{
					throw new global::System.ArgumentOutOfRangeException("value");
				}
				_preserveReferencesHandling = value;
			}
		}

		public virtual global::Newtonsoft.Json.ReferenceLoopHandling ReferenceLoopHandling
		{
			get
			{
				return _referenceLoopHandling;
			}
			set
			{
				if (value < global::Newtonsoft.Json.ReferenceLoopHandling.Error || value > global::Newtonsoft.Json.ReferenceLoopHandling.Serialize)
				{
					throw new global::System.ArgumentOutOfRangeException("value");
				}
				_referenceLoopHandling = value;
			}
		}

		public virtual global::Newtonsoft.Json.MissingMemberHandling MissingMemberHandling
		{
			get
			{
				return _missingMemberHandling;
			}
			set
			{
				if (value < global::Newtonsoft.Json.MissingMemberHandling.Ignore || value > global::Newtonsoft.Json.MissingMemberHandling.Error)
				{
					throw new global::System.ArgumentOutOfRangeException("value");
				}
				_missingMemberHandling = value;
			}
		}

		public virtual global::Newtonsoft.Json.NullValueHandling NullValueHandling
		{
			get
			{
				return _nullValueHandling;
			}
			set
			{
				if (value < global::Newtonsoft.Json.NullValueHandling.Include || value > global::Newtonsoft.Json.NullValueHandling.Ignore)
				{
					throw new global::System.ArgumentOutOfRangeException("value");
				}
				_nullValueHandling = value;
			}
		}

		public virtual global::Newtonsoft.Json.DefaultValueHandling DefaultValueHandling
		{
			get
			{
				return _defaultValueHandling;
			}
			set
			{
				if (value < global::Newtonsoft.Json.DefaultValueHandling.Include || value > global::Newtonsoft.Json.DefaultValueHandling.IgnoreAndPopulate)
				{
					throw new global::System.ArgumentOutOfRangeException("value");
				}
				_defaultValueHandling = value;
			}
		}

		public virtual global::Newtonsoft.Json.ObjectCreationHandling ObjectCreationHandling
		{
			get
			{
				return _objectCreationHandling;
			}
			set
			{
				if (value < global::Newtonsoft.Json.ObjectCreationHandling.Auto || value > global::Newtonsoft.Json.ObjectCreationHandling.Replace)
				{
					throw new global::System.ArgumentOutOfRangeException("value");
				}
				_objectCreationHandling = value;
			}
		}

		public virtual global::Newtonsoft.Json.ConstructorHandling ConstructorHandling
		{
			get
			{
				return _constructorHandling;
			}
			set
			{
				if (value < global::Newtonsoft.Json.ConstructorHandling.Default || value > global::Newtonsoft.Json.ConstructorHandling.AllowNonPublicDefaultConstructor)
				{
					throw new global::System.ArgumentOutOfRangeException("value");
				}
				_constructorHandling = value;
			}
		}

		public virtual global::Newtonsoft.Json.MetadataPropertyHandling MetadataPropertyHandling
		{
			get
			{
				return _metadataPropertyHandling;
			}
			set
			{
				if (value < global::Newtonsoft.Json.MetadataPropertyHandling.Default || value > global::Newtonsoft.Json.MetadataPropertyHandling.Ignore)
				{
					throw new global::System.ArgumentOutOfRangeException("value");
				}
				_metadataPropertyHandling = value;
			}
		}

		public virtual global::Newtonsoft.Json.JsonConverterCollection Converters
		{
			get
			{
				if (_converters == null)
				{
					_converters = new global::Newtonsoft.Json.JsonConverterCollection();
				}
				return _converters;
			}
		}

		public virtual global::Newtonsoft.Json.Serialization.IContractResolver ContractResolver
		{
			get
			{
				return _contractResolver;
			}
			set
			{
				_contractResolver = value ?? global::Newtonsoft.Json.Serialization.DefaultContractResolver.Instance;
			}
		}

		public virtual global::System.Runtime.Serialization.StreamingContext Context
		{
			get
			{
				return _context;
			}
			set
			{
				_context = value;
			}
		}

		public virtual global::Newtonsoft.Json.Formatting Formatting
		{
			get
			{
				return _formatting.GetValueOrDefault();
			}
			set
			{
				_formatting = value;
			}
		}

		public virtual global::Newtonsoft.Json.DateFormatHandling DateFormatHandling
		{
			get
			{
				return _dateFormatHandling.GetValueOrDefault();
			}
			set
			{
				_dateFormatHandling = value;
			}
		}

		public virtual global::Newtonsoft.Json.DateTimeZoneHandling DateTimeZoneHandling
		{
			get
			{
				return _dateTimeZoneHandling ?? global::Newtonsoft.Json.DateTimeZoneHandling.RoundtripKind;
			}
			set
			{
				_dateTimeZoneHandling = value;
			}
		}

		public virtual global::Newtonsoft.Json.DateParseHandling DateParseHandling
		{
			get
			{
				return _dateParseHandling ?? global::Newtonsoft.Json.DateParseHandling.DateTime;
			}
			set
			{
				_dateParseHandling = value;
			}
		}

		public virtual global::Newtonsoft.Json.FloatParseHandling FloatParseHandling
		{
			get
			{
				return _floatParseHandling.GetValueOrDefault();
			}
			set
			{
				_floatParseHandling = value;
			}
		}

		public virtual global::Newtonsoft.Json.FloatFormatHandling FloatFormatHandling
		{
			get
			{
				return _floatFormatHandling.GetValueOrDefault();
			}
			set
			{
				_floatFormatHandling = value;
			}
		}

		public virtual global::Newtonsoft.Json.StringEscapeHandling StringEscapeHandling
		{
			get
			{
				return _stringEscapeHandling.GetValueOrDefault();
			}
			set
			{
				_stringEscapeHandling = value;
			}
		}

		public virtual string DateFormatString
		{
			get
			{
				return _dateFormatString ?? "yyyy'-'MM'-'dd'T'HH':'mm':'ss.FFFFFFFK";
			}
			set
			{
				_dateFormatString = value;
				_dateFormatStringSet = true;
			}
		}

		public virtual global::System.Globalization.CultureInfo Culture
		{
			get
			{
				return _culture ?? global::Newtonsoft.Json.JsonSerializerSettings.DefaultCulture;
			}
			set
			{
				_culture = value;
			}
		}

		public virtual int? MaxDepth
		{
			get
			{
				return _maxDepth;
			}
			set
			{
				if (value <= 0)
				{
					throw new global::System.ArgumentException("Value must be positive.", "value");
				}
				_maxDepth = value;
				_maxDepthSet = true;
			}
		}

		public virtual bool CheckAdditionalContent
		{
			get
			{
				return _checkAdditionalContent == true;
			}
			set
			{
				_checkAdditionalContent = value;
			}
		}

		public virtual event global::System.EventHandler<global::Newtonsoft.Json.Serialization.ErrorEventArgs>? Error;

		internal bool IsCheckAdditionalContentSet()
		{
			return _checkAdditionalContent.HasValue;
		}

		public JsonSerializer()
		{
			_referenceLoopHandling = global::Newtonsoft.Json.ReferenceLoopHandling.Error;
			_missingMemberHandling = global::Newtonsoft.Json.MissingMemberHandling.Ignore;
			_nullValueHandling = global::Newtonsoft.Json.NullValueHandling.Include;
			_defaultValueHandling = global::Newtonsoft.Json.DefaultValueHandling.Include;
			_objectCreationHandling = global::Newtonsoft.Json.ObjectCreationHandling.Auto;
			_preserveReferencesHandling = global::Newtonsoft.Json.PreserveReferencesHandling.None;
			_constructorHandling = global::Newtonsoft.Json.ConstructorHandling.Default;
			_typeNameHandling = global::Newtonsoft.Json.TypeNameHandling.None;
			_metadataPropertyHandling = global::Newtonsoft.Json.MetadataPropertyHandling.Default;
			_context = global::Newtonsoft.Json.JsonSerializerSettings.DefaultContext;
			_serializationBinder = global::Newtonsoft.Json.Serialization.DefaultSerializationBinder.Instance;
			_culture = global::Newtonsoft.Json.JsonSerializerSettings.DefaultCulture;
			_contractResolver = global::Newtonsoft.Json.Serialization.DefaultContractResolver.Instance;
		}

		public static global::Newtonsoft.Json.JsonSerializer Create()
		{
			return new global::Newtonsoft.Json.JsonSerializer();
		}

		public static global::Newtonsoft.Json.JsonSerializer Create(global::Newtonsoft.Json.JsonSerializerSettings? settings)
		{
			global::Newtonsoft.Json.JsonSerializer jsonSerializer = Create();
			if (settings != null)
			{
				ApplySerializerSettings(jsonSerializer, settings);
			}
			return jsonSerializer;
		}

		public static global::Newtonsoft.Json.JsonSerializer CreateDefault()
		{
			return Create(global::Newtonsoft.Json.JsonConvert.DefaultSettings?.Invoke());
		}

		public static global::Newtonsoft.Json.JsonSerializer CreateDefault(global::Newtonsoft.Json.JsonSerializerSettings? settings)
		{
			global::Newtonsoft.Json.JsonSerializer jsonSerializer = CreateDefault();
			if (settings != null)
			{
				ApplySerializerSettings(jsonSerializer, settings);
			}
			return jsonSerializer;
		}

		private static void ApplySerializerSettings(global::Newtonsoft.Json.JsonSerializer serializer, global::Newtonsoft.Json.JsonSerializerSettings settings)
		{
			if (!global::Newtonsoft.Json.Utilities.CollectionUtils.IsNullOrEmpty(settings.Converters))
			{
				for (int i = 0; i < settings.Converters.Count; i++)
				{
					serializer.Converters.Insert(i, settings.Converters[i]);
				}
			}
			if (settings._typeNameHandling.HasValue)
			{
				serializer.TypeNameHandling = settings.TypeNameHandling;
			}
			if (settings._metadataPropertyHandling.HasValue)
			{
				serializer.MetadataPropertyHandling = settings.MetadataPropertyHandling;
			}
			if (settings._typeNameAssemblyFormatHandling.HasValue)
			{
				serializer.TypeNameAssemblyFormatHandling = settings.TypeNameAssemblyFormatHandling;
			}
			if (settings._preserveReferencesHandling.HasValue)
			{
				serializer.PreserveReferencesHandling = settings.PreserveReferencesHandling;
			}
			if (settings._referenceLoopHandling.HasValue)
			{
				serializer.ReferenceLoopHandling = settings.ReferenceLoopHandling;
			}
			if (settings._missingMemberHandling.HasValue)
			{
				serializer.MissingMemberHandling = settings.MissingMemberHandling;
			}
			if (settings._objectCreationHandling.HasValue)
			{
				serializer.ObjectCreationHandling = settings.ObjectCreationHandling;
			}
			if (settings._nullValueHandling.HasValue)
			{
				serializer.NullValueHandling = settings.NullValueHandling;
			}
			if (settings._defaultValueHandling.HasValue)
			{
				serializer.DefaultValueHandling = settings.DefaultValueHandling;
			}
			if (settings._constructorHandling.HasValue)
			{
				serializer.ConstructorHandling = settings.ConstructorHandling;
			}
			if (settings._context.HasValue)
			{
				serializer.Context = settings.Context;
			}
			if (settings._checkAdditionalContent.HasValue)
			{
				serializer._checkAdditionalContent = settings._checkAdditionalContent;
			}
			if (settings.Error != null)
			{
				serializer.Error += settings.Error;
			}
			if (settings.ContractResolver != null)
			{
				serializer.ContractResolver = settings.ContractResolver;
			}
			if (settings.ReferenceResolverProvider != null)
			{
				serializer.ReferenceResolver = settings.ReferenceResolverProvider();
			}
			if (settings.TraceWriter != null)
			{
				serializer.TraceWriter = settings.TraceWriter;
			}
			if (settings.EqualityComparer != null)
			{
				serializer.EqualityComparer = settings.EqualityComparer;
			}
			if (settings.SerializationBinder != null)
			{
				serializer.SerializationBinder = settings.SerializationBinder;
			}
			if (settings._formatting.HasValue)
			{
				serializer._formatting = settings._formatting;
			}
			if (settings._dateFormatHandling.HasValue)
			{
				serializer._dateFormatHandling = settings._dateFormatHandling;
			}
			if (settings._dateTimeZoneHandling.HasValue)
			{
				serializer._dateTimeZoneHandling = settings._dateTimeZoneHandling;
			}
			if (settings._dateParseHandling.HasValue)
			{
				serializer._dateParseHandling = settings._dateParseHandling;
			}
			if (settings._dateFormatStringSet)
			{
				serializer._dateFormatString = settings._dateFormatString;
				serializer._dateFormatStringSet = settings._dateFormatStringSet;
			}
			if (settings._floatFormatHandling.HasValue)
			{
				serializer._floatFormatHandling = settings._floatFormatHandling;
			}
			if (settings._floatParseHandling.HasValue)
			{
				serializer._floatParseHandling = settings._floatParseHandling;
			}
			if (settings._stringEscapeHandling.HasValue)
			{
				serializer._stringEscapeHandling = settings._stringEscapeHandling;
			}
			if (settings._culture != null)
			{
				serializer._culture = settings._culture;
			}
			if (settings._maxDepthSet)
			{
				serializer._maxDepth = settings._maxDepth;
				serializer._maxDepthSet = settings._maxDepthSet;
			}
		}

		[global::System.Diagnostics.DebuggerStepThrough]
		public void Populate(global::System.IO.TextReader reader, object target)
		{
			Populate(new global::Newtonsoft.Json.JsonTextReader(reader), target);
		}

		[global::System.Diagnostics.DebuggerStepThrough]
		public void Populate(global::Newtonsoft.Json.JsonReader reader, object target)
		{
			PopulateInternal(reader, target);
		}

		internal virtual void PopulateInternal(global::Newtonsoft.Json.JsonReader reader, object target)
		{
			global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(reader, "reader");
			global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(target, "target");
			SetupReader(reader, out global::System.Globalization.CultureInfo previousCulture, out global::Newtonsoft.Json.DateTimeZoneHandling? previousDateTimeZoneHandling, out global::Newtonsoft.Json.DateParseHandling? previousDateParseHandling, out global::Newtonsoft.Json.FloatParseHandling? previousFloatParseHandling, out int? previousMaxDepth, out string previousDateFormatString);
			global::Newtonsoft.Json.Serialization.TraceJsonReader traceJsonReader = ((TraceWriter != null && TraceWriter.LevelFilter >= global::System.Diagnostics.TraceLevel.Verbose) ? CreateTraceJsonReader(reader) : null);
			new global::Newtonsoft.Json.Serialization.JsonSerializerInternalReader(this).Populate(traceJsonReader ?? reader, target);
			if (traceJsonReader != null)
			{
				TraceWriter.Trace(global::System.Diagnostics.TraceLevel.Verbose, traceJsonReader.GetDeserializedJsonMessage(), null);
			}
			ResetReader(reader, previousCulture, previousDateTimeZoneHandling, previousDateParseHandling, previousFloatParseHandling, previousMaxDepth, previousDateFormatString);
		}

		[global::System.Diagnostics.DebuggerStepThrough]
		public object? Deserialize(global::Newtonsoft.Json.JsonReader reader)
		{
			return Deserialize(reader, null);
		}

		[global::System.Diagnostics.DebuggerStepThrough]
		public object? Deserialize(global::System.IO.TextReader reader, global::System.Type objectType)
		{
			return Deserialize(new global::Newtonsoft.Json.JsonTextReader(reader), objectType);
		}

		[global::System.Diagnostics.DebuggerStepThrough]
		public T? Deserialize<T>(global::Newtonsoft.Json.JsonReader reader)
		{
			return (T)Deserialize(reader, typeof(T));
		}

		[global::System.Diagnostics.DebuggerStepThrough]
		public object? Deserialize(global::Newtonsoft.Json.JsonReader reader, global::System.Type? objectType)
		{
			return DeserializeInternal(reader, objectType);
		}

		internal virtual object? DeserializeInternal(global::Newtonsoft.Json.JsonReader reader, global::System.Type? objectType)
		{
			global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(reader, "reader");
			SetupReader(reader, out global::System.Globalization.CultureInfo previousCulture, out global::Newtonsoft.Json.DateTimeZoneHandling? previousDateTimeZoneHandling, out global::Newtonsoft.Json.DateParseHandling? previousDateParseHandling, out global::Newtonsoft.Json.FloatParseHandling? previousFloatParseHandling, out int? previousMaxDepth, out string previousDateFormatString);
			global::Newtonsoft.Json.Serialization.TraceJsonReader traceJsonReader = ((TraceWriter != null && TraceWriter.LevelFilter >= global::System.Diagnostics.TraceLevel.Verbose) ? CreateTraceJsonReader(reader) : null);
			object? result = new global::Newtonsoft.Json.Serialization.JsonSerializerInternalReader(this).Deserialize(traceJsonReader ?? reader, objectType, CheckAdditionalContent);
			if (traceJsonReader != null)
			{
				TraceWriter.Trace(global::System.Diagnostics.TraceLevel.Verbose, traceJsonReader.GetDeserializedJsonMessage(), null);
			}
			ResetReader(reader, previousCulture, previousDateTimeZoneHandling, previousDateParseHandling, previousFloatParseHandling, previousMaxDepth, previousDateFormatString);
			return result;
		}

		internal void SetupReader(global::Newtonsoft.Json.JsonReader reader, out global::System.Globalization.CultureInfo? previousCulture, out global::Newtonsoft.Json.DateTimeZoneHandling? previousDateTimeZoneHandling, out global::Newtonsoft.Json.DateParseHandling? previousDateParseHandling, out global::Newtonsoft.Json.FloatParseHandling? previousFloatParseHandling, out int? previousMaxDepth, out string? previousDateFormatString)
		{
			if (_culture != null && !_culture.Equals(reader.Culture))
			{
				previousCulture = reader.Culture;
				reader.Culture = _culture;
			}
			else
			{
				previousCulture = null;
			}
			if (_dateTimeZoneHandling.HasValue && reader.DateTimeZoneHandling != _dateTimeZoneHandling)
			{
				previousDateTimeZoneHandling = reader.DateTimeZoneHandling;
				reader.DateTimeZoneHandling = _dateTimeZoneHandling.GetValueOrDefault();
			}
			else
			{
				previousDateTimeZoneHandling = null;
			}
			if (_dateParseHandling.HasValue && reader.DateParseHandling != _dateParseHandling)
			{
				previousDateParseHandling = reader.DateParseHandling;
				reader.DateParseHandling = _dateParseHandling.GetValueOrDefault();
			}
			else
			{
				previousDateParseHandling = null;
			}
			if (_floatParseHandling.HasValue && reader.FloatParseHandling != _floatParseHandling)
			{
				previousFloatParseHandling = reader.FloatParseHandling;
				reader.FloatParseHandling = _floatParseHandling.GetValueOrDefault();
			}
			else
			{
				previousFloatParseHandling = null;
			}
			if (_maxDepthSet && reader.MaxDepth != _maxDepth)
			{
				previousMaxDepth = reader.MaxDepth;
				reader.MaxDepth = _maxDepth;
			}
			else
			{
				previousMaxDepth = null;
			}
			if (_dateFormatStringSet && reader.DateFormatString != _dateFormatString)
			{
				previousDateFormatString = reader.DateFormatString;
				reader.DateFormatString = _dateFormatString;
			}
			else
			{
				previousDateFormatString = null;
			}
			if (reader is global::Newtonsoft.Json.JsonTextReader { PropertyNameTable: null } jsonTextReader && _contractResolver is global::Newtonsoft.Json.Serialization.DefaultContractResolver defaultContractResolver)
			{
				jsonTextReader.PropertyNameTable = defaultContractResolver.GetNameTable();
			}
		}

		private void ResetReader(global::Newtonsoft.Json.JsonReader reader, global::System.Globalization.CultureInfo? previousCulture, global::Newtonsoft.Json.DateTimeZoneHandling? previousDateTimeZoneHandling, global::Newtonsoft.Json.DateParseHandling? previousDateParseHandling, global::Newtonsoft.Json.FloatParseHandling? previousFloatParseHandling, int? previousMaxDepth, string? previousDateFormatString)
		{
			if (previousCulture != null)
			{
				reader.Culture = previousCulture;
			}
			if (previousDateTimeZoneHandling.HasValue)
			{
				reader.DateTimeZoneHandling = previousDateTimeZoneHandling.GetValueOrDefault();
			}
			if (previousDateParseHandling.HasValue)
			{
				reader.DateParseHandling = previousDateParseHandling.GetValueOrDefault();
			}
			if (previousFloatParseHandling.HasValue)
			{
				reader.FloatParseHandling = previousFloatParseHandling.GetValueOrDefault();
			}
			if (_maxDepthSet)
			{
				reader.MaxDepth = previousMaxDepth;
			}
			if (_dateFormatStringSet)
			{
				reader.DateFormatString = previousDateFormatString;
			}
			if (reader is global::Newtonsoft.Json.JsonTextReader { PropertyNameTable: not null } jsonTextReader && _contractResolver is global::Newtonsoft.Json.Serialization.DefaultContractResolver defaultContractResolver && jsonTextReader.PropertyNameTable == defaultContractResolver.GetNameTable())
			{
				jsonTextReader.PropertyNameTable = null;
			}
		}

		public void Serialize(global::System.IO.TextWriter textWriter, object? value)
		{
			Serialize(new global::Newtonsoft.Json.JsonTextWriter(textWriter), value);
		}

		public void Serialize(global::Newtonsoft.Json.JsonWriter jsonWriter, object? value, global::System.Type? objectType)
		{
			SerializeInternal(jsonWriter, value, objectType);
		}

		public void Serialize(global::System.IO.TextWriter textWriter, object? value, global::System.Type objectType)
		{
			Serialize(new global::Newtonsoft.Json.JsonTextWriter(textWriter), value, objectType);
		}

		public void Serialize(global::Newtonsoft.Json.JsonWriter jsonWriter, object? value)
		{
			SerializeInternal(jsonWriter, value, null);
		}

		private global::Newtonsoft.Json.Serialization.TraceJsonReader CreateTraceJsonReader(global::Newtonsoft.Json.JsonReader reader)
		{
			global::Newtonsoft.Json.Serialization.TraceJsonReader traceJsonReader = new global::Newtonsoft.Json.Serialization.TraceJsonReader(reader);
			if (reader.TokenType != global::Newtonsoft.Json.JsonToken.None)
			{
				traceJsonReader.WriteCurrentToken();
			}
			return traceJsonReader;
		}

		internal virtual void SerializeInternal(global::Newtonsoft.Json.JsonWriter jsonWriter, object? value, global::System.Type? objectType)
		{
			global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(jsonWriter, "jsonWriter");
			global::Newtonsoft.Json.Formatting? formatting = null;
			if (_formatting.HasValue && jsonWriter.Formatting != _formatting)
			{
				formatting = jsonWriter.Formatting;
				jsonWriter.Formatting = _formatting.GetValueOrDefault();
			}
			global::Newtonsoft.Json.DateFormatHandling? dateFormatHandling = null;
			if (_dateFormatHandling.HasValue && jsonWriter.DateFormatHandling != _dateFormatHandling)
			{
				dateFormatHandling = jsonWriter.DateFormatHandling;
				jsonWriter.DateFormatHandling = _dateFormatHandling.GetValueOrDefault();
			}
			global::Newtonsoft.Json.DateTimeZoneHandling? dateTimeZoneHandling = null;
			if (_dateTimeZoneHandling.HasValue && jsonWriter.DateTimeZoneHandling != _dateTimeZoneHandling)
			{
				dateTimeZoneHandling = jsonWriter.DateTimeZoneHandling;
				jsonWriter.DateTimeZoneHandling = _dateTimeZoneHandling.GetValueOrDefault();
			}
			global::Newtonsoft.Json.FloatFormatHandling? floatFormatHandling = null;
			if (_floatFormatHandling.HasValue && jsonWriter.FloatFormatHandling != _floatFormatHandling)
			{
				floatFormatHandling = jsonWriter.FloatFormatHandling;
				jsonWriter.FloatFormatHandling = _floatFormatHandling.GetValueOrDefault();
			}
			global::Newtonsoft.Json.StringEscapeHandling? stringEscapeHandling = null;
			if (_stringEscapeHandling.HasValue && jsonWriter.StringEscapeHandling != _stringEscapeHandling)
			{
				stringEscapeHandling = jsonWriter.StringEscapeHandling;
				jsonWriter.StringEscapeHandling = _stringEscapeHandling.GetValueOrDefault();
			}
			global::System.Globalization.CultureInfo cultureInfo = null;
			if (_culture != null && !_culture.Equals(jsonWriter.Culture))
			{
				cultureInfo = jsonWriter.Culture;
				jsonWriter.Culture = _culture;
			}
			string dateFormatString = null;
			if (_dateFormatStringSet && jsonWriter.DateFormatString != _dateFormatString)
			{
				dateFormatString = jsonWriter.DateFormatString;
				jsonWriter.DateFormatString = _dateFormatString;
			}
			global::Newtonsoft.Json.Serialization.TraceJsonWriter traceJsonWriter = ((TraceWriter != null && TraceWriter.LevelFilter >= global::System.Diagnostics.TraceLevel.Verbose) ? new global::Newtonsoft.Json.Serialization.TraceJsonWriter(jsonWriter) : null);
			new global::Newtonsoft.Json.Serialization.JsonSerializerInternalWriter(this).Serialize(traceJsonWriter ?? jsonWriter, value, objectType);
			if (traceJsonWriter != null)
			{
				TraceWriter.Trace(global::System.Diagnostics.TraceLevel.Verbose, traceJsonWriter.GetSerializedJsonMessage(), null);
			}
			if (formatting.HasValue)
			{
				jsonWriter.Formatting = formatting.GetValueOrDefault();
			}
			if (dateFormatHandling.HasValue)
			{
				jsonWriter.DateFormatHandling = dateFormatHandling.GetValueOrDefault();
			}
			if (dateTimeZoneHandling.HasValue)
			{
				jsonWriter.DateTimeZoneHandling = dateTimeZoneHandling.GetValueOrDefault();
			}
			if (floatFormatHandling.HasValue)
			{
				jsonWriter.FloatFormatHandling = floatFormatHandling.GetValueOrDefault();
			}
			if (stringEscapeHandling.HasValue)
			{
				jsonWriter.StringEscapeHandling = stringEscapeHandling.GetValueOrDefault();
			}
			if (_dateFormatStringSet)
			{
				jsonWriter.DateFormatString = dateFormatString;
			}
			if (cultureInfo != null)
			{
				jsonWriter.Culture = cultureInfo;
			}
		}

		internal global::Newtonsoft.Json.Serialization.IReferenceResolver GetReferenceResolver()
		{
			if (_referenceResolver == null)
			{
				_referenceResolver = new global::Newtonsoft.Json.Serialization.DefaultReferenceResolver();
			}
			return _referenceResolver;
		}

		internal global::Newtonsoft.Json.JsonConverter? GetMatchingConverter(global::System.Type type)
		{
			return GetMatchingConverter(_converters, type);
		}

		internal static global::Newtonsoft.Json.JsonConverter? GetMatchingConverter(global::System.Collections.Generic.IList<global::Newtonsoft.Json.JsonConverter>? converters, global::System.Type objectType)
		{
			if (converters != null)
			{
				for (int i = 0; i < converters.Count; i++)
				{
					global::Newtonsoft.Json.JsonConverter jsonConverter = converters[i];
					if (jsonConverter.CanConvert(objectType))
					{
						return jsonConverter;
					}
				}
			}
			return null;
		}

		internal void OnError(global::Newtonsoft.Json.Serialization.ErrorEventArgs e)
		{
			this.Error?.Invoke(this, e);
		}
	}
}
