namespace Unity.Services.Wire.Protocol.Internal
{
	[global::Newtonsoft.Json.JsonObject(ItemNullValueHandling = global::Newtonsoft.Json.NullValueHandling.Ignore)]
	internal class Push
	{
		public string channel;

		public global::Unity.Services.Wire.Protocol.Internal.Publication pub;

		public global::Unity.Services.Wire.Protocol.Internal.Unsubscribe unsubscribe;

		[global::UnityEngine.Scripting.Preserve]
		public Push()
		{
		}

		internal string GetPushType()
		{
			if (IsPub())
			{
				return "PUB";
			}
			if (IsUnsub())
			{
				return "UNSUB";
			}
			return "UNKNOWN";
		}

		internal bool IsUnsub()
		{
			return unsubscribe != null;
		}

		internal bool IsPub()
		{
			return pub != null;
		}
	}
}
