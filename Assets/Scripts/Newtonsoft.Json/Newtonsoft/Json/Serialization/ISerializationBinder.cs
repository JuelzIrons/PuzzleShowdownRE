namespace Newtonsoft.Json.Serialization
{
	public interface ISerializationBinder
	{
		global::System.Type BindToType(string? assemblyName, string typeName);

		void BindToName(global::System.Type serializedType, out string? assemblyName, out string? typeName);
	}
}
