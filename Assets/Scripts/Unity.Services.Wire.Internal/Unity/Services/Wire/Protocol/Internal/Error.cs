namespace Unity.Services.Wire.Protocol.Internal
{
	[global::Newtonsoft.Json.JsonObject(ItemNullValueHandling = global::Newtonsoft.Json.NullValueHandling.Ignore)]
	internal class Error
	{
		public global::Unity.Services.Wire.Protocol.Internal.CentrifugeErrorCode code;

		public string message;

		[global::UnityEngine.Scripting.Preserve]
		public Error()
		{
		}
	}
}
