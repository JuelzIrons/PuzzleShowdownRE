namespace Unity.Services.Qos.V2.Http
{
	[global::UnityEngine.Scripting.Preserve]
	[global::Newtonsoft.Json.JsonConverter(typeof(global::Unity.Services.Qos.V2.Http.JsonObjectConverter))]
	internal interface IDeserializable
	{
		string GetAsString();

		T GetAs<T>(global::Unity.Services.Qos.V2.Http.DeserializationSettings deserializationSettings = null);
	}
}
