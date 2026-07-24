namespace Newtonsoft.Json
{
	public class JsonSerializerSettings
	{
		internal const global::Newtonsoft.Json.ReferenceLoopHandling DefaultReferenceLoopHandling = global::Newtonsoft.Json.ReferenceLoopHandling.Error;

		internal const global::Newtonsoft.Json.MissingMemberHandling DefaultMissingMemberHandling = global::Newtonsoft.Json.MissingMemberHandling.Ignore;

		internal const global::Newtonsoft.Json.NullValueHandling DefaultNullValueHandling = global::Newtonsoft.Json.NullValueHandling.Include;

		internal const global::Newtonsoft.Json.DefaultValueHandling DefaultDefaultValueHandling = global::Newtonsoft.Json.DefaultValueHandling.Include;

		internal const global::Newtonsoft.Json.ObjectCreationHandling DefaultObjectCreationHandling = global::Newtonsoft.Json.ObjectCreationHandling.Auto;

		internal const global::Newtonsoft.Json.PreserveReferencesHandling DefaultPreserveReferencesHandling = global::Newtonsoft.Json.PreserveReferencesHandling.None;

		internal const global::Newtonsoft.Json.ConstructorHandling DefaultConstructorHandling = global::Newtonsoft.Json.ConstructorHandling.Default;

		internal const global::Newtonsoft.Json.TypeNameHandling DefaultTypeNameHandling = global::Newtonsoft.Json.TypeNameHandling.None;

		internal const global::Newtonsoft.Json.MetadataPropertyHandling DefaultMetadataPropertyHandling = global::Newtonsoft.Json.MetadataPropertyHandling.Default;

		internal static readonly global::System.Runtime.Serialization.StreamingContext DefaultContext;

		internal const global::Newtonsoft.Json.Formatting DefaultFormatting = global::Newtonsoft.Json.Formatting.None;

		internal const global::Newtonsoft.Json.DateFormatHandling DefaultDateFormatHandling = global::Newtonsoft.Json.DateFormatHandling.IsoDateFormat;

		internal const global::Newtonsoft.Json.DateTimeZoneHandling DefaultDateTimeZoneHandling = global::Newtonsoft.Json.DateTimeZoneHandling.RoundtripKind;

		internal const global::Newtonsoft.Json.DateParseHandling DefaultDateParseHandling = global::Newtonsoft.Json.DateParseHandling.DateTime;

		internal const global::Newtonsoft.Json.FloatParseHandling DefaultFloatParseHandling = global::Newtonsoft.Json.FloatParseHandling.Double;

		internal const global::Newtonsoft.Json.FloatFormatHandling DefaultFloatFormatHandling = global::Newtonsoft.Json.FloatFormatHandling.String;

		internal const global::Newtonsoft.Json.StringEscapeHandling DefaultStringEscapeHandling = global::Newtonsoft.Json.StringEscapeHandling.Default;

		internal const global::Newtonsoft.Json.TypeNameAssemblyFormatHandling DefaultTypeNameAssemblyFormatHandling = global::Newtonsoft.Json.TypeNameAssemblyFormatHandling.Simple;

		internal static readonly global::System.Globalization.CultureInfo DefaultCulture;

		internal const bool DefaultCheckAdditionalContent = false;

		internal const string DefaultDateFormatString = "yyyy'-'MM'-'dd'T'HH':'mm':'ss.FFFFFFFK";

		internal const int DefaultMaxDepth = 64;

		internal global::Newtonsoft.Json.Formatting? _formatting;

		internal global::Newtonsoft.Json.DateFormatHandling? _dateFormatHandling;

		internal global::Newtonsoft.Json.DateTimeZoneHandling? _dateTimeZoneHandling;

		internal global::Newtonsoft.Json.DateParseHandling? _dateParseHandling;

		internal global::Newtonsoft.Json.FloatFormatHandling? _floatFormatHandling;

		internal global::Newtonsoft.Json.FloatParseHandling? _floatParseHandling;

		internal global::Newtonsoft.Json.StringEscapeHandling? _stringEscapeHandling;

		internal global::System.Globalization.CultureInfo? _culture;

		internal bool? _checkAdditionalContent;

		internal int? _maxDepth;

		internal bool _maxDepthSet;

		internal string? _dateFormatString;

		internal bool _dateFormatStringSet;

		internal global::Newtonsoft.Json.TypeNameAssemblyFormatHandling? _typeNameAssemblyFormatHandling;

		internal global::Newtonsoft.Json.DefaultValueHandling? _defaultValueHandling;

		internal global::Newtonsoft.Json.PreserveReferencesHandling? _preserveReferencesHandling;

		internal global::Newtonsoft.Json.NullValueHandling? _nullValueHandling;

		internal global::Newtonsoft.Json.ObjectCreationHandling? _objectCreationHandling;

		internal global::Newtonsoft.Json.MissingMemberHandling? _missingMemberHandling;

		internal global::Newtonsoft.Json.ReferenceLoopHandling? _referenceLoopHandling;

		internal global::System.Runtime.Serialization.StreamingContext? _context;

		internal global::Newtonsoft.Json.ConstructorHandling? _constructorHandling;

		internal global::Newtonsoft.Json.TypeNameHandling? _typeNameHandling;

		internal global::Newtonsoft.Json.MetadataPropertyHandling? _metadataPropertyHandling;

		public global::Newtonsoft.Json.ReferenceLoopHandling ReferenceLoopHandling
		{
			get
			{
				return _referenceLoopHandling.GetValueOrDefault();
			}
			set
			{
				_referenceLoopHandling = value;
			}
		}

		public global::Newtonsoft.Json.MissingMemberHandling MissingMemberHandling
		{
			get
			{
				return _missingMemberHandling.GetValueOrDefault();
			}
			set
			{
				_missingMemberHandling = value;
			}
		}

		public global::Newtonsoft.Json.ObjectCreationHandling ObjectCreationHandling
		{
			get
			{
				return _objectCreationHandling.GetValueOrDefault();
			}
			set
			{
				_objectCreationHandling = value;
			}
		}

		public global::Newtonsoft.Json.NullValueHandling NullValueHandling
		{
			get
			{
				return _nullValueHandling.GetValueOrDefault();
			}
			set
			{
				_nullValueHandling = value;
			}
		}

		public global::Newtonsoft.Json.DefaultValueHandling DefaultValueHandling
		{
			get
			{
				return _defaultValueHandling.GetValueOrDefault();
			}
			set
			{
				_defaultValueHandling = value;
			}
		}

		public global::System.Collections.Generic.IList<global::Newtonsoft.Json.JsonConverter> Converters { get; set; }

		public global::Newtonsoft.Json.PreserveReferencesHandling PreserveReferencesHandling
		{
			get
			{
				return _preserveReferencesHandling.GetValueOrDefault();
			}
			set
			{
				_preserveReferencesHandling = value;
			}
		}

		public global::Newtonsoft.Json.TypeNameHandling TypeNameHandling
		{
			get
			{
				return _typeNameHandling.GetValueOrDefault();
			}
			set
			{
				_typeNameHandling = value;
			}
		}

		public global::Newtonsoft.Json.MetadataPropertyHandling MetadataPropertyHandling
		{
			get
			{
				return _metadataPropertyHandling.GetValueOrDefault();
			}
			set
			{
				_metadataPropertyHandling = value;
			}
		}

		[global::System.Obsolete("TypeNameAssemblyFormat is obsolete. Use TypeNameAssemblyFormatHandling instead.")]
		public global::System.Runtime.Serialization.Formatters.FormatterAssemblyStyle TypeNameAssemblyFormat
		{
			get
			{
				return (global::System.Runtime.Serialization.Formatters.FormatterAssemblyStyle)TypeNameAssemblyFormatHandling;
			}
			set
			{
				TypeNameAssemblyFormatHandling = (global::Newtonsoft.Json.TypeNameAssemblyFormatHandling)value;
			}
		}

		public global::Newtonsoft.Json.TypeNameAssemblyFormatHandling TypeNameAssemblyFormatHandling
		{
			get
			{
				return _typeNameAssemblyFormatHandling.GetValueOrDefault();
			}
			set
			{
				_typeNameAssemblyFormatHandling = value;
			}
		}

		public global::Newtonsoft.Json.ConstructorHandling ConstructorHandling
		{
			get
			{
				return _constructorHandling.GetValueOrDefault();
			}
			set
			{
				_constructorHandling = value;
			}
		}

		public global::Newtonsoft.Json.Serialization.IContractResolver? ContractResolver { get; set; }

		public global::System.Collections.IEqualityComparer? EqualityComparer { get; set; }

		[global::System.Obsolete("ReferenceResolver property is obsolete. Use the ReferenceResolverProvider property to set the IReferenceResolver: settings.ReferenceResolverProvider = () => resolver")]
		public global::Newtonsoft.Json.Serialization.IReferenceResolver? ReferenceResolver
		{
			get
			{
				return ReferenceResolverProvider?.Invoke();
			}
			set
			{
				ReferenceResolverProvider = ((value != null) ? ((global::System.Func<global::Newtonsoft.Json.Serialization.IReferenceResolver>)(() => value)) : null);
			}
		}

		public global::System.Func<global::Newtonsoft.Json.Serialization.IReferenceResolver?>? ReferenceResolverProvider { get; set; }

		public global::Newtonsoft.Json.Serialization.ITraceWriter? TraceWriter { get; set; }

		[global::System.Obsolete("Binder is obsolete. Use SerializationBinder instead.")]
		public global::System.Runtime.Serialization.SerializationBinder? Binder
		{
			get
			{
				if (SerializationBinder == null)
				{
					return null;
				}
				if (SerializationBinder is global::Newtonsoft.Json.Serialization.SerializationBinderAdapter serializationBinderAdapter)
				{
					return serializationBinderAdapter.SerializationBinder;
				}
				throw new global::System.InvalidOperationException("Cannot get SerializationBinder because an ISerializationBinder was previously set.");
			}
			set
			{
				SerializationBinder = ((value == null) ? null : new global::Newtonsoft.Json.Serialization.SerializationBinderAdapter(value));
			}
		}

		public global::Newtonsoft.Json.Serialization.ISerializationBinder? SerializationBinder { get; set; }

		public global::System.EventHandler<global::Newtonsoft.Json.Serialization.ErrorEventArgs>? Error { get; set; }

		public global::System.Runtime.Serialization.StreamingContext Context
		{
			get
			{
				return _context ?? DefaultContext;
			}
			set
			{
				_context = value;
			}
		}

		public string DateFormatString
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

		public int? MaxDepth
		{
			get
			{
				if (!_maxDepthSet)
				{
					return 64;
				}
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

		public global::Newtonsoft.Json.Formatting Formatting
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

		public global::Newtonsoft.Json.DateFormatHandling DateFormatHandling
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

		public global::Newtonsoft.Json.DateTimeZoneHandling DateTimeZoneHandling
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

		public global::Newtonsoft.Json.DateParseHandling DateParseHandling
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

		public global::Newtonsoft.Json.FloatFormatHandling FloatFormatHandling
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

		public global::Newtonsoft.Json.FloatParseHandling FloatParseHandling
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

		public global::Newtonsoft.Json.StringEscapeHandling StringEscapeHandling
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

		public global::System.Globalization.CultureInfo Culture
		{
			get
			{
				return _culture ?? DefaultCulture;
			}
			set
			{
				_culture = value;
			}
		}

		public bool CheckAdditionalContent
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

		static JsonSerializerSettings()
		{
			DefaultContext = default(global::System.Runtime.Serialization.StreamingContext);
			DefaultCulture = global::System.Globalization.CultureInfo.InvariantCulture;
		}

		[global::System.Diagnostics.DebuggerStepThrough]
		public JsonSerializerSettings()
		{
			Converters = new global::System.Collections.Generic.List<global::Newtonsoft.Json.JsonConverter>();
		}

		public JsonSerializerSettings(global::Newtonsoft.Json.JsonSerializerSettings original)
		{
			_floatParseHandling = original._floatParseHandling;
			_floatFormatHandling = original._floatFormatHandling;
			_dateParseHandling = original._dateParseHandling;
			_dateTimeZoneHandling = original._dateTimeZoneHandling;
			_dateFormatHandling = original._dateFormatHandling;
			_formatting = original._formatting;
			_maxDepth = original._maxDepth;
			_maxDepthSet = original._maxDepthSet;
			_dateFormatString = original._dateFormatString;
			_dateFormatStringSet = original._dateFormatStringSet;
			_context = original._context;
			Error = original.Error;
			SerializationBinder = original.SerializationBinder;
			TraceWriter = original.TraceWriter;
			_culture = original._culture;
			ReferenceResolverProvider = original.ReferenceResolverProvider;
			EqualityComparer = original.EqualityComparer;
			ContractResolver = original.ContractResolver;
			_constructorHandling = original._constructorHandling;
			_typeNameAssemblyFormatHandling = original._typeNameAssemblyFormatHandling;
			_metadataPropertyHandling = original._metadataPropertyHandling;
			_typeNameHandling = original._typeNameHandling;
			_preserveReferencesHandling = original._preserveReferencesHandling;
			Converters = global::System.Linq.Enumerable.ToList(original.Converters);
			_defaultValueHandling = original._defaultValueHandling;
			_nullValueHandling = original._nullValueHandling;
			_objectCreationHandling = original._objectCreationHandling;
			_missingMemberHandling = original._missingMemberHandling;
			_referenceLoopHandling = original._referenceLoopHandling;
			_checkAdditionalContent = original._checkAdditionalContent;
			_stringEscapeHandling = original._stringEscapeHandling;
		}
	}
}
