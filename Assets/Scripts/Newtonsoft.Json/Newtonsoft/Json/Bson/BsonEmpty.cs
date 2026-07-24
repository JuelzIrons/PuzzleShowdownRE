namespace Newtonsoft.Json.Bson
{
	internal class BsonEmpty : global::Newtonsoft.Json.Bson.BsonToken
	{
		public static readonly global::Newtonsoft.Json.Bson.BsonToken Null = new global::Newtonsoft.Json.Bson.BsonEmpty(global::Newtonsoft.Json.Bson.BsonType.Null);

		public static readonly global::Newtonsoft.Json.Bson.BsonToken Undefined = new global::Newtonsoft.Json.Bson.BsonEmpty(global::Newtonsoft.Json.Bson.BsonType.Undefined);

		public override global::Newtonsoft.Json.Bson.BsonType Type { get; }

		private BsonEmpty(global::Newtonsoft.Json.Bson.BsonType type)
		{
			Type = type;
		}
	}
}
