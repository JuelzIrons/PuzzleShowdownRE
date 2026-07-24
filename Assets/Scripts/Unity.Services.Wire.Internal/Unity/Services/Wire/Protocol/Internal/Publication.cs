namespace Unity.Services.Wire.Protocol.Internal
{
	[global::Newtonsoft.Json.JsonObject(ItemNullValueHandling = global::Newtonsoft.Json.NullValueHandling.Ignore)]
	internal class Publication
	{
		public global::Unity.Services.Wire.Internal.WireMessage data;

		public global::Unity.Services.Wire.Protocol.Internal.ClientInfo info;

		public ulong offset;

		public global::System.Collections.Generic.Dictionary<string, string> tags;

		[global::UnityEngine.Scripting.Preserve]
		public Publication()
		{
		}
	}
}
