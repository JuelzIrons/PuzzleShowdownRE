namespace Unity.VisualScripting
{
	public class SerializationOperation
	{
		public global::Unity.VisualScripting.FullSerializer.fsSerializer serializer { get; private set; }

		public global::System.Collections.Generic.List<global::UnityEngine.Object> objectReferences { get; private set; }

		public SerializationOperation()
		{
			objectReferences = new global::System.Collections.Generic.List<global::UnityEngine.Object>();
			serializer = new global::Unity.VisualScripting.FullSerializer.fsSerializer();
			serializer.AddConverter(new global::Unity.VisualScripting.UnityObjectConverter());
			serializer.AddConverter(new global::Unity.VisualScripting.RayConverter());
			serializer.AddConverter(new global::Unity.VisualScripting.Ray2DConverter());
			serializer.AddConverter(new global::Unity.VisualScripting.NamespaceConverter());
			serializer.AddConverter(new global::Unity.VisualScripting.LooseAssemblyNameConverter());
			serializer.Context.Set(objectReferences);
		}

		public void Reset()
		{
			objectReferences.Clear();
		}
	}
}
