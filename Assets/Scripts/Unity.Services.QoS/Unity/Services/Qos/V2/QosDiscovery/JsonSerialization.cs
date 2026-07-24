namespace Unity.Services.Qos.V2.QosDiscovery
{
	internal static class JsonSerialization
	{
		public static byte[] Serialize<T>(T obj)
		{
			return global::System.Text.Encoding.UTF8.GetBytes(SerializeToString(obj));
		}

		public static string SerializeToString<T>(T obj)
		{
			return global::Unity.Services.Qos.V2.Http.IsolatedJsonConvert.SerializeObject(obj, new global::Newtonsoft.Json.JsonSerializerSettings
			{
				ReferenceLoopHandling = global::Newtonsoft.Json.ReferenceLoopHandling.Ignore
			});
		}
	}
}
