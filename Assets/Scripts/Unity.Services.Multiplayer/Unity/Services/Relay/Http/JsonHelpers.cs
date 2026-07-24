namespace Unity.Services.Relay.Http
{
	internal static class JsonHelpers
	{
		[global::UnityEngine.RuntimeInitializeOnLoadMethod(global::UnityEngine.RuntimeInitializeLoadType.BeforeSplashScreen)]
		internal static void RegisterTypesForAOT()
		{
			global::Newtonsoft.Json.Utilities.AotHelper.EnsureType<global::Newtonsoft.Json.Converters.StringEnumConverter>();
			global::Newtonsoft.Json.Utilities.AotHelper.EnsureType<global::Unity.Services.Relay.Http.JsonObjectConverter>();
		}

		internal static bool TryParseJson<T>(this string @this, out T result)
		{
			bool success = true;
			global::Newtonsoft.Json.JsonSerializerSettings settings = new global::Newtonsoft.Json.JsonSerializerSettings
			{
				Error = delegate(object sender, global::Newtonsoft.Json.Serialization.ErrorEventArgs args)
				{
					success = false;
					args.ErrorContext.Handled = true;
				},
				MissingMemberHandling = global::Newtonsoft.Json.MissingMemberHandling.Ignore,
				ReferenceLoopHandling = global::Newtonsoft.Json.ReferenceLoopHandling.Ignore
			};
			result = global::Newtonsoft.Json.JsonConvert.DeserializeObject<T>(@this, settings);
			return success;
		}
	}
}
