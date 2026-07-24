namespace Newtonsoft.Json.Bson
{
	internal enum BsonBinaryType : byte
	{
		Binary = 0,
		Function = 1,
		[global::System.Obsolete("This type has been deprecated in the BSON specification. Use Binary instead.")]
		BinaryOld = 2,
		[global::System.Obsolete("This type has been deprecated in the BSON specification. Use Uuid instead.")]
		UuidOld = 3,
		Uuid = 4,
		Md5 = 5,
		UserDefined = 128
	}
}
