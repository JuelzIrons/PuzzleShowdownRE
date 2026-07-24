namespace Newtonsoft.Json.Serialization
{
	public class JsonPrimitiveContract : global::Newtonsoft.Json.Serialization.JsonContract
	{
		private static readonly global::System.Collections.Generic.Dictionary<global::System.Type, global::Newtonsoft.Json.ReadType> ReadTypeMap = new global::System.Collections.Generic.Dictionary<global::System.Type, global::Newtonsoft.Json.ReadType>
		{
			[typeof(byte[])] = global::Newtonsoft.Json.ReadType.ReadAsBytes,
			[typeof(byte)] = global::Newtonsoft.Json.ReadType.ReadAsInt32,
			[typeof(short)] = global::Newtonsoft.Json.ReadType.ReadAsInt32,
			[typeof(int)] = global::Newtonsoft.Json.ReadType.ReadAsInt32,
			[typeof(decimal)] = global::Newtonsoft.Json.ReadType.ReadAsDecimal,
			[typeof(bool)] = global::Newtonsoft.Json.ReadType.ReadAsBoolean,
			[typeof(string)] = global::Newtonsoft.Json.ReadType.ReadAsString,
			[typeof(global::System.DateTime)] = global::Newtonsoft.Json.ReadType.ReadAsDateTime,
			[typeof(global::System.DateTimeOffset)] = global::Newtonsoft.Json.ReadType.ReadAsDateTimeOffset,
			[typeof(float)] = global::Newtonsoft.Json.ReadType.ReadAsDouble,
			[typeof(double)] = global::Newtonsoft.Json.ReadType.ReadAsDouble,
			[typeof(long)] = global::Newtonsoft.Json.ReadType.ReadAsInt64
		};

		internal global::Newtonsoft.Json.Utilities.PrimitiveTypeCode TypeCode { get; set; }

		public JsonPrimitiveContract(global::System.Type underlyingType)
			: base(underlyingType)
		{
			ContractType = global::Newtonsoft.Json.Serialization.JsonContractType.Primitive;
			TypeCode = global::Newtonsoft.Json.Utilities.ConvertUtils.GetTypeCode(underlyingType);
			IsReadOnlyOrFixedSize = true;
			if (ReadTypeMap.TryGetValue(NonNullableUnderlyingType, out var value))
			{
				InternalReadType = value;
			}
		}
	}
}
