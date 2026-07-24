namespace Unity.Services.Matchmaker.Http
{
	[global::UnityEngine.Scripting.Preserve]
	[global::Newtonsoft.Json.JsonConverter(typeof(global::Unity.Services.Matchmaker.Http.JsonObjectConverter))]
	public interface IDeserializable
	{
		string GetAsString();

		T GetAs<T>(global::Unity.Services.Matchmaker.Http.DeserializationSettings deserializationSettings = null);
	}
}
