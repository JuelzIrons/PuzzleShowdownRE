namespace Unity.Services.Lobbies.Http
{
	[global::UnityEngine.Scripting.Preserve]
	[global::Newtonsoft.Json.JsonConverter(typeof(global::Unity.Services.Lobbies.Http.JsonObjectConverter))]
	public interface IDeserializable
	{
		string GetAsString();

		T GetAs<T>(global::Unity.Services.Lobbies.Http.DeserializationSettings deserializationSettings = null);
	}
}
