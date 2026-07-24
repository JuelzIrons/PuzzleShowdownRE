namespace Unity.Services.Wire.Protocol.Internal
{
	[global::Newtonsoft.Json.JsonObject(ItemNullValueHandling = global::Newtonsoft.Json.NullValueHandling.Ignore)]
	internal class ConnectResult
	{
		public string client;

		public string version;

		public bool expires;

		public uint ttl;

		public string data;

		public uint ping;

		public bool pong;

		public global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Wire.Protocol.Internal.SubscribeResult> subs;

		[global::UnityEngine.Scripting.Preserve]
		public ConnectResult()
		{
			subs = new global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Wire.Protocol.Internal.SubscribeResult>();
		}
	}
}
