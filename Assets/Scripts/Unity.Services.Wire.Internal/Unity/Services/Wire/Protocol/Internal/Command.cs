namespace Unity.Services.Wire.Protocol.Internal
{
	[global::Newtonsoft.Json.JsonObject(ItemNullValueHandling = global::Newtonsoft.Json.NullValueHandling.Ignore)]
	internal class Command
	{
		public uint id;

		public global::Unity.Services.Wire.Protocol.Internal.ConnectRequest connect;

		public global::Unity.Services.Wire.Protocol.Internal.SubscribeRequest subscribe;

		public global::Unity.Services.Wire.Protocol.Internal.UnsubscribeRequest unsubscribe;

		public global::Unity.Services.Wire.Protocol.Internal.PingRequest ping;

		[global::UnityEngine.Scripting.Preserve]
		public Command()
		{
		}

		public Command(global::Unity.Services.Wire.Protocol.Internal.PingRequest request)
		{
			id = global::Unity.Services.Wire.Protocol.Internal.CommandID.GenerateNewId();
			ping = request;
		}

		public Command(global::Unity.Services.Wire.Protocol.Internal.ConnectRequest request)
		{
			id = global::Unity.Services.Wire.Protocol.Internal.CommandID.GenerateNewId();
			connect = request;
		}

		public Command(global::Unity.Services.Wire.Protocol.Internal.SubscribeRequest request)
		{
			id = global::Unity.Services.Wire.Protocol.Internal.CommandID.GenerateNewId();
			subscribe = request;
		}

		public Command(global::Unity.Services.Wire.Protocol.Internal.UnsubscribeRequest request)
		{
			id = global::Unity.Services.Wire.Protocol.Internal.CommandID.GenerateNewId();
			unsubscribe = request;
		}

		public static global::Unity.Services.Wire.Protocol.Internal.Command FromJSON(byte[] data)
		{
			return global::Newtonsoft.Json.JsonConvert.DeserializeObject<global::Unity.Services.Wire.Protocol.Internal.Command>(global::System.Text.Encoding.UTF8.GetString(data));
		}

		public byte[] GetBytes()
		{
			return global::System.Text.Encoding.UTF8.GetBytes(global::Newtonsoft.Json.JsonConvert.SerializeObject(this, global::Newtonsoft.Json.Formatting.None));
		}

		public new string ToString()
		{
			return global::Newtonsoft.Json.JsonConvert.SerializeObject(this, global::Newtonsoft.Json.Formatting.None);
		}

		internal bool IsPing()
		{
			return ping != null;
		}

		public string GetMethod()
		{
			if (connect != null)
			{
				return "CONNECT";
			}
			if (subscribe != null)
			{
				return "SUBSCRIBE";
			}
			if (unsubscribe != null)
			{
				return "UNSUBSCRIBE";
			}
			if (ping != null)
			{
				return "PING";
			}
			return "UNKNOWN";
		}
	}
}
