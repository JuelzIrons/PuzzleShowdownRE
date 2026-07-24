namespace Unity.VisualScripting.FullSerializer
{
	[global::System.AttributeUsage(global::System.AttributeTargets.Class | global::System.AttributeTargets.Struct)]
	public class fsObjectAttribute : global::System.Attribute
	{
		public global::System.Type[] PreviousModels;

		public string VersionString;

		public global::Unity.VisualScripting.FullSerializer.fsMemberSerialization MemberSerialization = global::Unity.VisualScripting.FullSerializer.fsMemberSerialization.Default;

		public global::System.Type Converter;

		public global::System.Type Processor;

		public fsObjectAttribute()
		{
		}

		public fsObjectAttribute(string versionString, params global::System.Type[] previousModels)
		{
			VersionString = versionString;
			PreviousModels = previousModels;
		}
	}
}
