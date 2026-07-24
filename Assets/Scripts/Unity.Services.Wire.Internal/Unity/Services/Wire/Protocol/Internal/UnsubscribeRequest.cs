namespace Unity.Services.Wire.Protocol.Internal
{
	[global::Newtonsoft.Json.JsonObject(ItemNullValueHandling = global::Newtonsoft.Json.NullValueHandling.Ignore)]
	internal class UnsubscribeRequest
	{
		public string channel;

		[global::UnityEngine.Scripting.Preserve]
		public UnsubscribeRequest()
		{
		}
	}
}
