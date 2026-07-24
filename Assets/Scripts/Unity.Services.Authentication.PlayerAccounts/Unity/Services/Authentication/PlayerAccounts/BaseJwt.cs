namespace Unity.Services.Authentication.PlayerAccounts
{
	public class BaseJwt
	{
		[global::Newtonsoft.Json.JsonProperty("exp")]
		public int ExpirationTimeUnix;

		[global::Newtonsoft.Json.JsonProperty("iat")]
		public int IssuedAtTimeUnix;

		[global::Newtonsoft.Json.JsonProperty("nbf")]
		public int NotBeforeTimeUnix;

		[global::Newtonsoft.Json.JsonIgnore]
		public global::System.DateTime ExpirationTime => ConvertTimestamp(ExpirationTimeUnix);

		[global::Newtonsoft.Json.JsonIgnore]
		public global::System.DateTime IssuedAtTime => ConvertTimestamp(IssuedAtTimeUnix);

		[global::Newtonsoft.Json.JsonIgnore]
		public global::System.DateTime NotBeforeTime => ConvertTimestamp(NotBeforeTimeUnix);

		[global::UnityEngine.Scripting.Preserve]
		internal BaseJwt()
		{
		}

		internal global::System.DateTime ConvertTimestamp(int timestamp)
		{
			if (timestamp != 0)
			{
				return global::System.DateTimeOffset.FromUnixTimeSeconds(timestamp).DateTime;
			}
			throw new global::System.Exception("Token does not contain a value for this timestamp.");
		}
	}
}
