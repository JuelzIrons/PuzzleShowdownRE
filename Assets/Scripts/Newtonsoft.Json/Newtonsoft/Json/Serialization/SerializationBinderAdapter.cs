namespace Newtonsoft.Json.Serialization
{
	internal class SerializationBinderAdapter : global::Newtonsoft.Json.Serialization.ISerializationBinder
	{
		public readonly global::System.Runtime.Serialization.SerializationBinder SerializationBinder;

		public SerializationBinderAdapter(global::System.Runtime.Serialization.SerializationBinder serializationBinder)
		{
			SerializationBinder = serializationBinder;
		}

		public global::System.Type BindToType(string? assemblyName, string typeName)
		{
			return SerializationBinder.BindToType(assemblyName, typeName);
		}

		public void BindToName(global::System.Type serializedType, out string? assemblyName, out string? typeName)
		{
			SerializationBinder.BindToName(serializedType, out assemblyName, out typeName);
		}
	}
}
