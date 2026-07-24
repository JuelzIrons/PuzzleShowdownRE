namespace Unity.Services.Wire.Protocol.Internal
{
	[global::Newtonsoft.Json.JsonObject(ItemNullValueHandling = global::Newtonsoft.Json.NullValueHandling.Ignore)]
	internal class SubscribeResult
	{
		public bool expires;

		public uint ttl;

		public bool recoverable;

		public string epoch;

		public bool recovered;

		public ulong offset;

		public bool positioned;

		public string data;

		public global::Unity.Services.Wire.Protocol.Internal.Publication[] publications;

		[global::UnityEngine.Scripting.Preserve]
		public SubscribeResult()
		{
			publications = new global::Unity.Services.Wire.Protocol.Internal.Publication[0];
		}
	}
}
