namespace Unity.Services.Authentication
{
	internal class BaseJwt
	{
		[global::Newtonsoft.Json.JsonProperty("exp")]
		public int ExpirationTimeUnix;

		[global::Newtonsoft.Json.JsonProperty("iat")]
		public int IssuedAtTimeUnix;

		[global::Newtonsoft.Json.JsonProperty("nbf")]
		public int NotBeforeTimeUnix;

		[global::Newtonsoft.Json.JsonIgnore]
		public global::System.DateTime? ExpirationTime => ConvertTimestamp(ExpirationTimeUnix);

		[global::Newtonsoft.Json.JsonIgnore]
		public global::System.DateTime? IssuedAtTime => ConvertTimestamp(IssuedAtTimeUnix);

		[global::Newtonsoft.Json.JsonIgnore]
		public global::System.DateTime? NotBeforeTime => ConvertTimestamp(NotBeforeTimeUnix);

		[global::UnityEngine.Scripting.Preserve]
		public BaseJwt()
		{
		}

		protected global::System.DateTime? ConvertTimestamp(int timestamp)
		{
			if (timestamp != 0)
			{
				return global::System.DateTimeOffset.FromUnixTimeSeconds(timestamp).DateTime;
			}
			return null;
		}
	}
}
