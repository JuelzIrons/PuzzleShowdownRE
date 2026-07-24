namespace Newtonsoft.Json.Serialization
{
	public class JsonObjectContract : global::Newtonsoft.Json.Serialization.JsonContainerContract
	{
		internal bool ExtensionDataIsJToken;

		private bool? _hasRequiredOrDefaultValueProperties;

		private global::Newtonsoft.Json.Serialization.ObjectConstructor<object>? _overrideCreator;

		private global::Newtonsoft.Json.Serialization.ObjectConstructor<object>? _parameterizedCreator;

		private global::Newtonsoft.Json.Serialization.JsonPropertyCollection? _creatorParameters;

		private global::System.Type? _extensionDataValueType;

		public global::Newtonsoft.Json.MemberSerialization MemberSerialization { get; set; }

		public global::Newtonsoft.Json.MissingMemberHandling? MissingMemberHandling { get; set; }

		public global::Newtonsoft.Json.Required? ItemRequired { get; set; }

		public global::Newtonsoft.Json.NullValueHandling? ItemNullValueHandling { get; set; }

		public global::Newtonsoft.Json.Serialization.JsonPropertyCollection Properties { get; }

		public global::Newtonsoft.Json.Serialization.JsonPropertyCollection CreatorParameters
		{
			get
			{
				if (_creatorParameters == null)
				{
					_creatorParameters = new global::Newtonsoft.Json.Serialization.JsonPropertyCollection(base.UnderlyingType);
				}
				return _creatorParameters;
			}
		}

		public global::Newtonsoft.Json.Serialization.ObjectConstructor<object>? OverrideCreator
		{
			get
			{
				return _overrideCreator;
			}
			set
			{
				_overrideCreator = value;
			}
		}

		internal global::Newtonsoft.Json.Serialization.ObjectConstructor<object>? ParameterizedCreator
		{
			get
			{
				return _parameterizedCreator;
			}
			set
			{
				_parameterizedCreator = value;
			}
		}

		public global::Newtonsoft.Json.Serialization.ExtensionDataSetter? ExtensionDataSetter { get; set; }

		public global::Newtonsoft.Json.Serialization.ExtensionDataGetter? ExtensionDataGetter { get; set; }

		public global::System.Type? ExtensionDataValueType
		{
			get
			{
				return _extensionDataValueType;
			}
			set
			{
				_extensionDataValueType = value;
				ExtensionDataIsJToken = value != null && typeof(global::Newtonsoft.Json.Linq.JToken).IsAssignableFrom(value);
			}
		}

		public global::System.Func<string, string>? ExtensionDataNameResolver { get; set; }

		internal bool HasRequiredOrDefaultValueProperties
		{
			get
			{
				if (!_hasRequiredOrDefaultValueProperties.HasValue)
				{
					_hasRequiredOrDefaultValueProperties = false;
					if ((ItemRequired ?? global::Newtonsoft.Json.Required.Default) != global::Newtonsoft.Json.Required.Default)
					{
						_hasRequiredOrDefaultValueProperties = true;
					}
					else
					{
						foreach (global::Newtonsoft.Json.Serialization.JsonProperty property in Properties)
						{
							if (property.Required != global::Newtonsoft.Json.Required.Default || ((uint?)property.DefaultValueHandling & 2u) == 2)
							{
								_hasRequiredOrDefaultValueProperties = true;
								break;
							}
						}
					}
				}
				return _hasRequiredOrDefaultValueProperties == true;
			}
		}

		public JsonObjectContract(global::System.Type underlyingType)
			: base(underlyingType)
		{
			ContractType = global::Newtonsoft.Json.Serialization.JsonContractType.Object;
			Properties = new global::Newtonsoft.Json.Serialization.JsonPropertyCollection(base.UnderlyingType);
		}

		[global::System.Security.SecuritySafeCritical]
		internal object GetUninitializedObject()
		{
			if (!global::Newtonsoft.Json.Serialization.JsonTypeReflector.FullyTrusted)
			{
				throw new global::Newtonsoft.Json.JsonException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Insufficient permissions. Creating an uninitialized '{0}' type requires full trust.", global::System.Globalization.CultureInfo.InvariantCulture, NonNullableUnderlyingType));
			}
			return global::System.Runtime.Serialization.FormatterServices.GetUninitializedObject(NonNullableUnderlyingType);
		}
	}
}
