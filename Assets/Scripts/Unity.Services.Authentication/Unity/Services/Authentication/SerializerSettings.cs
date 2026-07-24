namespace Unity.Services.Authentication
{
	internal static class SerializerSettings
	{
		private static global::Newtonsoft.Json.JsonSerializerSettings s_Instance;

		internal static global::Newtonsoft.Json.JsonSerializerSettings DefaultSerializerSettings
		{
			get
			{
				if (s_Instance == null)
				{
					s_Instance = new global::Newtonsoft.Json.JsonSerializerSettings();
				}
				return s_Instance;
			}
		}
	}
}
