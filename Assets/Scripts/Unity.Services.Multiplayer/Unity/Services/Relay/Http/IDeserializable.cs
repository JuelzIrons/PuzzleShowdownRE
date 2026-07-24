namespace Unity.Services.Relay.Http
{
	[global::UnityEngine.Scripting.Preserve]
	[global::Newtonsoft.Json.JsonConverter(typeof(global::Unity.Services.Relay.Http.JsonObjectConverter))]
	internal interface IDeserializable
	{
		string GetAsString();

		T GetAs<T>(global::Unity.Services.Relay.Http.DeserializationSettings deserializationSettings = null);
	}
}
