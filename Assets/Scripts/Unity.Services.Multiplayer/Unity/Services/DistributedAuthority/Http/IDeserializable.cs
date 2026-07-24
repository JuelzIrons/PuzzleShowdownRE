namespace Unity.Services.DistributedAuthority.Http
{
	[global::UnityEngine.Scripting.Preserve]
	[global::Newtonsoft.Json.JsonConverter(typeof(global::Unity.Services.DistributedAuthority.Http.JsonObjectConverter))]
	internal interface IDeserializable
	{
		string GetAsString();

		T GetAs<T>(global::Unity.Services.DistributedAuthority.Http.DeserializationSettings deserializationSettings = null);
	}
}
