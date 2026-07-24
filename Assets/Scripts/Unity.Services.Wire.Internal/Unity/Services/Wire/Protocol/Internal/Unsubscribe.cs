namespace Unity.Services.Wire.Protocol.Internal
{
	[global::Newtonsoft.Json.JsonObject(ItemNullValueHandling = global::Newtonsoft.Json.NullValueHandling.Ignore)]
	internal class Unsubscribe
	{
		public uint code;

		public string reason;

		[global::UnityEngine.Scripting.Preserve]
		public Unsubscribe()
		{
		}
	}
}
