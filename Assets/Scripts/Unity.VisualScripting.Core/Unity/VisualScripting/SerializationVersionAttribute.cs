namespace Unity.VisualScripting
{
	public class SerializationVersionAttribute : global::Unity.VisualScripting.FullSerializer.fsObjectAttribute
	{
		public SerializationVersionAttribute(string versionString, params global::System.Type[] previousModels)
			: base(versionString, previousModels)
		{
		}
	}
}
