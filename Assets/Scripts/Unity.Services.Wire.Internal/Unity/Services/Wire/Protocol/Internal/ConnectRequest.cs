namespace Unity.Services.Wire.Protocol.Internal
{
	[global::Newtonsoft.Json.JsonObject(ItemNullValueHandling = global::Newtonsoft.Json.NullValueHandling.Ignore)]
	internal class ConnectRequest
	{
		public string token;

		public global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Wire.Protocol.Internal.SubscribeRequest> subs;

		[global::UnityEngine.Scripting.Preserve]
		public ConnectRequest()
		{
			subs = new global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Wire.Protocol.Internal.SubscribeRequest>();
		}

		public ConnectRequest(string token)
			: this()
		{
			this.token = token;
		}

		public ConnectRequest(string token, global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Wire.Protocol.Internal.SubscribeRequest> subscriptionRequests)
		{
			subs = subscriptionRequests;
			this.token = token;
		}
	}
}
