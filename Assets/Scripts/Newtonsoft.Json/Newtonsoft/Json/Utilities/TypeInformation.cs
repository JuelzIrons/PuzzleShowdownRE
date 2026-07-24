namespace Newtonsoft.Json.Utilities
{
	internal class TypeInformation
	{
		public global::System.Type Type { get; }

		public global::Newtonsoft.Json.Utilities.PrimitiveTypeCode TypeCode { get; }

		public TypeInformation(global::System.Type type, global::Newtonsoft.Json.Utilities.PrimitiveTypeCode typeCode)
		{
			Type = type;
			TypeCode = typeCode;
		}
	}
}
