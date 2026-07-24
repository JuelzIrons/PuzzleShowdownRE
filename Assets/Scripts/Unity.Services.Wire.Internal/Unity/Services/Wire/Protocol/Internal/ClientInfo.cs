namespace Unity.Services.Wire.Protocol.Internal
{
	[global::Newtonsoft.Json.JsonObject(ItemNullValueHandling = global::Newtonsoft.Json.NullValueHandling.Ignore)]
	internal class ClientInfo
	{
		public string user;

		public string client;

		public byte[] conn_info;

		public byte[] chan_info;

		[global::UnityEngine.Scripting.Preserve]
		public ClientInfo()
		{
		}
	}
}
