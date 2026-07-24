namespace Unity.Services.Qos.Http
{
	[global::UnityEngine.Scripting.Preserve]
	[global::Newtonsoft.Json.JsonConverter(typeof(global::Unity.Services.Qos.Http.JsonObjectConverter))]
	internal interface IDeserializable
	{
		string GetAsString();

		T GetAs<T>(global::Unity.Services.Qos.Http.DeserializationSettings deserializationSettings = null);
	}
}
