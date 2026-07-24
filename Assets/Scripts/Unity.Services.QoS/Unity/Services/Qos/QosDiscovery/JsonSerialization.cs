namespace Unity.Services.Qos.QosDiscovery
{
	internal static class JsonSerialization
	{
		public static byte[] Serialize<T>(T obj)
		{
			return global::System.Text.Encoding.UTF8.GetBytes(SerializeToString(obj));
		}

		public static string SerializeToString<T>(T obj)
		{
			return global::Newtonsoft.Json.JsonConvert.SerializeObject(obj, new global::Newtonsoft.Json.JsonSerializerSettings
			{
				ReferenceLoopHandling = global::Newtonsoft.Json.ReferenceLoopHandling.Ignore
			});
		}
	}
}
