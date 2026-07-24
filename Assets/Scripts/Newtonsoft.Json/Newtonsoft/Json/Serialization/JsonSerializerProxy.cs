namespace Newtonsoft.Json.Serialization
{
	internal class JsonSerializerProxy : global::Newtonsoft.Json.JsonSerializer
	{
		private readonly global::Newtonsoft.Json.Serialization.JsonSerializerInternalReader? _serializerReader;

		private readonly global::Newtonsoft.Json.Serialization.JsonSerializerInternalWriter? _serializerWriter;

		internal readonly global::Newtonsoft.Json.JsonSerializer _serializer;

		public override global::Newtonsoft.Json.Serialization.IReferenceResolver? ReferenceResolver
		{
			get
			{
				return _serializer.ReferenceResolver;
			}
			set
			{
				_serializer.ReferenceResolver = value;
			}
		}

		public override global::Newtonsoft.Json.Serialization.ITraceWriter? TraceWriter
		{
			get
			{
				return _serializer.TraceWriter;
			}
			set
			{
				_serializer.TraceWriter = value;
			}
		}

		public override global::System.Collections.IEqualityComparer? EqualityComparer
		{
			get
			{
				return _serializer.EqualityComparer;
			}
			set
			{
				_serializer.EqualityComparer = value;
			}
		}

		public override global::Newtonsoft.Json.JsonConverterCollection Converters => _serializer.Converters;

		public override global::Newtonsoft.Json.DefaultValueHandling DefaultValueHandling
		{
			get
			{
				return _serializer.DefaultValueHandling;
			}
			set
			{
				_serializer.DefaultValueHandling = value;
			}
		}

		public override global::Newtonsoft.Json.Serialization.IContractResolver ContractResolver
		{
			get
			{
				return _serializer.ContractResolver;
			}
			set
			{
				_serializer.ContractResolver = value;
			}
		}

		public override global::Newtonsoft.Json.MissingMemberHandling MissingMemberHandling
		{
			get
			{
				return _serializer.MissingMemberHandling;
			}
			set
			{
				_serializer.MissingMemberHandling = value;
			}
		}

		public override global::Newtonsoft.Json.NullValueHandling NullValueHandling
		{
			get
			{
				return _serializer.NullValueHandling;
			}
			set
			{
				_serializer.NullValueHandling = value;
			}
		}

		public override global::Newtonsoft.Json.ObjectCreationHandling ObjectCreationHandling
		{
			get
			{
				return _serializer.ObjectCreationHandling;
			}
			set
			{
				_serializer.ObjectCreationHandling = value;
			}
		}

		public override global::Newtonsoft.Json.ReferenceLoopHandling ReferenceLoopHandling
		{
			get
			{
				return _serializer.ReferenceLoopHandling;
			}
			set
			{
				_serializer.ReferenceLoopHandling = value;
			}
		}

		public override global::Newtonsoft.Json.PreserveReferencesHandling PreserveReferencesHandling
		{
			get
			{
				return _serializer.PreserveReferencesHandling;
			}
			set
			{
				_serializer.PreserveReferencesHandling = value;
			}
		}

		public override global::Newtonsoft.Json.TypeNameHandling TypeNameHandling
		{
			get
			{
				return _serializer.TypeNameHandling;
			}
			set
			{
				_serializer.TypeNameHandling = value;
			}
		}

		public override global::Newtonsoft.Json.MetadataPropertyHandling MetadataPropertyHandling
		{
			get
			{
				return _serializer.MetadataPropertyHandling;
			}
			set
			{
				_serializer.MetadataPropertyHandling = value;
			}
		}

		[global::System.Obsolete("TypeNameAssemblyFormat is obsolete. Use TypeNameAssemblyFormatHandling instead.")]
		public override global::System.Runtime.Serialization.Formatters.FormatterAssemblyStyle TypeNameAssemblyFormat
		{
			get
			{
				return _serializer.TypeNameAssemblyFormat;
			}
			set
			{
				_serializer.TypeNameAssemblyFormat = value;
			}
		}

		public override global::Newtonsoft.Json.TypeNameAssemblyFormatHandling TypeNameAssemblyFormatHandling
		{
			get
			{
				return _serializer.TypeNameAssemblyFormatHandling;
			}
			set
			{
				_serializer.TypeNameAssemblyFormatHandling = value;
			}
		}

		public override global::Newtonsoft.Json.ConstructorHandling ConstructorHandling
		{
			get
			{
				return _serializer.ConstructorHandling;
			}
			set
			{
				_serializer.ConstructorHandling = value;
			}
		}

		[global::System.Obsolete("Binder is obsolete. Use SerializationBinder instead.")]
		public override global::System.Runtime.Serialization.SerializationBinder Binder
		{
			get
			{
				return _serializer.Binder;
			}
			set
			{
				_serializer.Binder = value;
			}
		}

		public override global::Newtonsoft.Json.Serialization.ISerializationBinder SerializationBinder
		{
			get
			{
				return _serializer.SerializationBinder;
			}
			set
			{
				_serializer.SerializationBinder = value;
			}
		}

		public override global::System.Runtime.Serialization.StreamingContext Context
		{
			get
			{
				return _serializer.Context;
			}
			set
			{
				_serializer.Context = value;
			}
		}

		public override global::Newtonsoft.Json.Formatting Formatting
		{
			get
			{
				return _serializer.Formatting;
			}
			set
			{
				_serializer.Formatting = value;
			}
		}

		public override global::Newtonsoft.Json.DateFormatHandling DateFormatHandling
		{
			get
			{
				return _serializer.DateFormatHandling;
			}
			set
			{
				_serializer.DateFormatHandling = value;
			}
		}

		public override global::Newtonsoft.Json.DateTimeZoneHandling DateTimeZoneHandling
		{
			get
			{
				return _serializer.DateTimeZoneHandling;
			}
			set
			{
				_serializer.DateTimeZoneHandling = value;
			}
		}

		public override global::Newtonsoft.Json.DateParseHandling DateParseHandling
		{
			get
			{
				return _serializer.DateParseHandling;
			}
			set
			{
				_serializer.DateParseHandling = value;
			}
		}

		public override global::Newtonsoft.Json.FloatFormatHandling FloatFormatHandling
		{
			get
			{
				return _serializer.FloatFormatHandling;
			}
			set
			{
				_serializer.FloatFormatHandling = value;
			}
		}

		public override global::Newtonsoft.Json.FloatParseHandling FloatParseHandling
		{
			get
			{
				return _serializer.FloatParseHandling;
			}
			set
			{
				_serializer.FloatParseHandling = value;
			}
		}

		public override global::Newtonsoft.Json.StringEscapeHandling StringEscapeHandling
		{
			get
			{
				return _serializer.StringEscapeHandling;
			}
			set
			{
				_serializer.StringEscapeHandling = value;
			}
		}

		public override string DateFormatString
		{
			get
			{
				return _serializer.DateFormatString;
			}
			set
			{
				_serializer.DateFormatString = value;
			}
		}

		public override global::System.Globalization.CultureInfo Culture
		{
			get
			{
				return _serializer.Culture;
			}
			set
			{
				_serializer.Culture = value;
			}
		}

		public override int? MaxDepth
		{
			get
			{
				return _serializer.MaxDepth;
			}
			set
			{
				_serializer.MaxDepth = value;
			}
		}

		public override bool CheckAdditionalContent
		{
			get
			{
				return _serializer.CheckAdditionalContent;
			}
			set
			{
				_serializer.CheckAdditionalContent = value;
			}
		}

		public override event global::System.EventHandler<global::Newtonsoft.Json.Serialization.ErrorEventArgs>? Error
		{
			add
			{
				_serializer.Error += value;
			}
			remove
			{
				_serializer.Error -= value;
			}
		}

		internal global::Newtonsoft.Json.Serialization.JsonSerializerInternalBase GetInternalSerializer()
		{
			if (_serializerReader != null)
			{
				return _serializerReader;
			}
			return _serializerWriter;
		}

		public JsonSerializerProxy(global::Newtonsoft.Json.Serialization.JsonSerializerInternalReader serializerReader)
		{
			global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(serializerReader, "serializerReader");
			_serializerReader = serializerReader;
			_serializer = serializerReader.Serializer;
		}

		public JsonSerializerProxy(global::Newtonsoft.Json.Serialization.JsonSerializerInternalWriter serializerWriter)
		{
			global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(serializerWriter, "serializerWriter");
			_serializerWriter = serializerWriter;
			_serializer = serializerWriter.Serializer;
		}

		internal override object? DeserializeInternal(global::Newtonsoft.Json.JsonReader reader, global::System.Type? objectType)
		{
			if (_serializerReader != null)
			{
				return _serializerReader.Deserialize(reader, objectType, checkAdditionalContent: false);
			}
			return _serializer.Deserialize(reader, objectType);
		}

		internal override void PopulateInternal(global::Newtonsoft.Json.JsonReader reader, object target)
		{
			if (_serializerReader != null)
			{
				_serializerReader.Populate(reader, target);
			}
			else
			{
				_serializer.Populate(reader, target);
			}
		}

		internal override void SerializeInternal(global::Newtonsoft.Json.JsonWriter jsonWriter, object? value, global::System.Type? rootType)
		{
			if (_serializerWriter != null)
			{
				_serializerWriter.Serialize(jsonWriter, value, rootType);
			}
			else
			{
				_serializer.Serialize(jsonWriter, value);
			}
		}
	}
}
