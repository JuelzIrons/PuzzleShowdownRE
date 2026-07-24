namespace Newtonsoft.Json.Bson
{
	internal class BsonValue : global::Newtonsoft.Json.Bson.BsonToken
	{
		private readonly object _value;

		private readonly global::Newtonsoft.Json.Bson.BsonType _type;

		public object Value => _value;

		public override global::Newtonsoft.Json.Bson.BsonType Type => _type;

		public BsonValue(object value, global::Newtonsoft.Json.Bson.BsonType type)
		{
			_value = value;
			_type = type;
		}
	}
}
