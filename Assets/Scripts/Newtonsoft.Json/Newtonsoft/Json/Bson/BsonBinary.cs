namespace Newtonsoft.Json.Bson
{
	internal class BsonBinary : global::Newtonsoft.Json.Bson.BsonValue
	{
		public global::Newtonsoft.Json.Bson.BsonBinaryType BinaryType { get; set; }

		public BsonBinary(byte[] value, global::Newtonsoft.Json.Bson.BsonBinaryType binaryType)
			: base(value, global::Newtonsoft.Json.Bson.BsonType.Binary)
		{
			BinaryType = binaryType;
		}
	}
}
