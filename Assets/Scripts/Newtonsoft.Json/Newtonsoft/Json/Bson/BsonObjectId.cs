namespace Newtonsoft.Json.Bson
{
	[global::System.Obsolete("BSON reading and writing has been moved to its own package. See https://www.nuget.org/packages/Newtonsoft.Json.Bson for more details.")]
	public class BsonObjectId
	{
		public byte[] Value { get; }

		public BsonObjectId(byte[] value)
		{
			global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(value, "value");
			if (value.Length != 12)
			{
				throw new global::System.ArgumentException("An ObjectId must be 12 bytes", "value");
			}
			Value = value;
		}
	}
}
