namespace Newtonsoft.Json.Bson
{
	internal class BsonBoolean : global::Newtonsoft.Json.Bson.BsonValue
	{
		public static readonly global::Newtonsoft.Json.Bson.BsonBoolean False = new global::Newtonsoft.Json.Bson.BsonBoolean(value: false);

		public static readonly global::Newtonsoft.Json.Bson.BsonBoolean True = new global::Newtonsoft.Json.Bson.BsonBoolean(value: true);

		private BsonBoolean(bool value)
			: base(value, global::Newtonsoft.Json.Bson.BsonType.Boolean)
		{
		}
	}
}
