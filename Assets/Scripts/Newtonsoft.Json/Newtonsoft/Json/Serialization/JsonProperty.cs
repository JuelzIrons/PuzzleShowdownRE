namespace Newtonsoft.Json.Serialization
{
	public class JsonProperty
	{
		internal global::Newtonsoft.Json.Required? _required;

		internal bool _hasExplicitDefaultValue;

		private object? _defaultValue;

		private bool _hasGeneratedDefaultValue;

		private string? _propertyName;

		internal bool _skipPropertyNameEscape;

		private global::System.Type? _propertyType;

		internal global::Newtonsoft.Json.Serialization.JsonContract? PropertyContract { get; set; }

		public string? PropertyName
		{
			get
			{
				return _propertyName;
			}
			set
			{
				_propertyName = value;
				_skipPropertyNameEscape = !global::Newtonsoft.Json.Utilities.JavaScriptUtils.ShouldEscapeJavaScriptString(_propertyName, global::Newtonsoft.Json.Utilities.JavaScriptUtils.HtmlCharEscapeFlags);
			}
		}

		public global::System.Type? DeclaringType { get; set; }

		public int? Order { get; set; }

		public string? UnderlyingName { get; set; }

		public global::Newtonsoft.Json.Serialization.IValueProvider? ValueProvider { get; set; }

		public global::Newtonsoft.Json.Serialization.IAttributeProvider? AttributeProvider { get; set; }

		public global::System.Type? PropertyType
		{
			get
			{
				return _propertyType;
			}
			set
			{
				if (_propertyType != value)
				{
					_propertyType = value;
					_hasGeneratedDefaultValue = false;
				}
			}
		}

		public global::Newtonsoft.Json.JsonConverter? Converter { get; set; }

		[global::System.Obsolete("MemberConverter is obsolete. Use Converter instead.")]
		public global::Newtonsoft.Json.JsonConverter? MemberConverter
		{
			get
			{
				return Converter;
			}
			set
			{
				Converter = value;
			}
		}

		public bool Ignored { get; set; }

		public bool Readable { get; set; }

		public bool Writable { get; set; }

		public bool HasMemberAttribute { get; set; }

		public object? DefaultValue
		{
			get
			{
				if (!_hasExplicitDefaultValue)
				{
					return null;
				}
				return _defaultValue;
			}
			set
			{
				_hasExplicitDefaultValue = true;
				_defaultValue = value;
			}
		}

		public global::Newtonsoft.Json.Required Required
		{
			get
			{
				return _required.GetValueOrDefault();
			}
			set
			{
				_required = value;
			}
		}

		public bool IsRequiredSpecified => _required.HasValue;

		public bool? IsReference { get; set; }

		public global::Newtonsoft.Json.NullValueHandling? NullValueHandling { get; set; }

		public global::Newtonsoft.Json.DefaultValueHandling? DefaultValueHandling { get; set; }

		public global::Newtonsoft.Json.ReferenceLoopHandling? ReferenceLoopHandling { get; set; }

		public global::Newtonsoft.Json.ObjectCreationHandling? ObjectCreationHandling { get; set; }

		public global::Newtonsoft.Json.TypeNameHandling? TypeNameHandling { get; set; }

		public global::System.Predicate<object>? ShouldSerialize { get; set; }

		public global::System.Predicate<object>? ShouldDeserialize { get; set; }

		public global::System.Predicate<object>? GetIsSpecified { get; set; }

		public global::System.Action<object, object?>? SetIsSpecified { get; set; }

		public global::Newtonsoft.Json.JsonConverter? ItemConverter { get; set; }

		public bool? ItemIsReference { get; set; }

		public global::Newtonsoft.Json.TypeNameHandling? ItemTypeNameHandling { get; set; }

		public global::Newtonsoft.Json.ReferenceLoopHandling? ItemReferenceLoopHandling { get; set; }

		internal object? GetResolvedDefaultValue()
		{
			if (_propertyType == null)
			{
				return null;
			}
			if (!_hasExplicitDefaultValue && !_hasGeneratedDefaultValue)
			{
				_defaultValue = global::Newtonsoft.Json.Utilities.ReflectionUtils.GetDefaultValue(_propertyType);
				_hasGeneratedDefaultValue = true;
			}
			return _defaultValue;
		}

		public override string ToString()
		{
			return PropertyName ?? string.Empty;
		}

		internal void WritePropertyName(global::Newtonsoft.Json.JsonWriter writer)
		{
			string propertyName = PropertyName;
			if (_skipPropertyNameEscape)
			{
				writer.WritePropertyName(propertyName, escape: false);
			}
			else
			{
				writer.WritePropertyName(propertyName);
			}
		}
	}
}
