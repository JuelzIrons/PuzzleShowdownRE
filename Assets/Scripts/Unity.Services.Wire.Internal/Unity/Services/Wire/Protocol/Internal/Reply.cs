namespace Unity.Services.Wire.Protocol.Internal
{
	[global::Newtonsoft.Json.JsonObject(ItemNullValueHandling = global::Newtonsoft.Json.NullValueHandling.Ignore)]
	internal class Reply
	{
		public uint id;

		public global::Unity.Services.Wire.Protocol.Internal.Error error;

		public global::Unity.Services.Wire.Protocol.Internal.ConnectResult connect;

		public global::Unity.Services.Wire.Protocol.Internal.SubscribeResult subscribe;

		public global::Unity.Services.Wire.Protocol.Internal.UnsubscribeResult unsubscribe;

		public global::Unity.Services.Wire.Protocol.Internal.Push push;

		public global::Unity.Services.Wire.Protocol.Internal.PingResult ping;

		[global::Newtonsoft.Json.JsonIgnore]
		public string originalString = "";

		[global::UnityEngine.Scripting.Preserve]
		public Reply()
		{
		}

		internal static global::Unity.Services.Wire.Protocol.Internal.Reply PingReply(uint id)
		{
			return new global::Unity.Services.Wire.Protocol.Internal.Reply
			{
				id = id,
				ping = new global::Unity.Services.Wire.Protocol.Internal.PingResult()
			};
		}

		internal static global::Unity.Services.Wire.Protocol.Internal.Reply ErrorReply(uint id, global::Unity.Services.Wire.Protocol.Internal.Error error)
		{
			return new global::Unity.Services.Wire.Protocol.Internal.Reply
			{
				id = id,
				error = error
			};
		}

		internal static global::Unity.Services.Wire.Protocol.Internal.Reply SubscribeReply(uint id, global::Unity.Services.Wire.Protocol.Internal.SubscribeResult result)
		{
			return new global::Unity.Services.Wire.Protocol.Internal.Reply
			{
				id = id,
				subscribe = result
			};
		}

		internal static global::Unity.Services.Wire.Protocol.Internal.Reply UnsubscribeReply(uint id)
		{
			return new global::Unity.Services.Wire.Protocol.Internal.Reply
			{
				id = id,
				unsubscribe = new global::Unity.Services.Wire.Protocol.Internal.UnsubscribeResult()
			};
		}

		internal static global::Unity.Services.Wire.Protocol.Internal.Reply ConnectReply(uint id, global::Unity.Services.Wire.Protocol.Internal.ConnectResult result)
		{
			return new global::Unity.Services.Wire.Protocol.Internal.Reply
			{
				id = id,
				connect = result
			};
		}

		internal static global::Unity.Services.Wire.Protocol.Internal.Reply PushReply(global::Unity.Services.Wire.Protocol.Internal.Push push)
		{
			return new global::Unity.Services.Wire.Protocol.Internal.Reply
			{
				push = push
			};
		}

		public static global::Unity.Services.Wire.Protocol.Internal.Reply FromJson(byte[] jsonData)
		{
			return FromJson(global::System.Text.Encoding.UTF8.GetString(jsonData));
		}

		public static global::Unity.Services.Wire.Protocol.Internal.Reply FromJson(string jsonData)
		{
			global::Unity.Services.Wire.Protocol.Internal.Reply? reply = global::Newtonsoft.Json.JsonConvert.DeserializeObject<global::Unity.Services.Wire.Protocol.Internal.Reply>(jsonData);
			reply.originalString = jsonData;
			return reply;
		}

		public byte[] ToJson()
		{
			return global::System.Text.Encoding.UTF8.GetBytes(global::Newtonsoft.Json.JsonConvert.SerializeObject(this));
		}

		public bool HasError()
		{
			if (error != null)
			{
				return error.code != (global::Unity.Services.Wire.Protocol.Internal.CentrifugeErrorCode)0;
			}
			return false;
		}
	}
}
