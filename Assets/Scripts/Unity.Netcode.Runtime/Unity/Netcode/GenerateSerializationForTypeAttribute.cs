namespace Unity.Netcode
{
	[global::System.AttributeUsage(global::System.AttributeTargets.Class | global::System.AttributeTargets.Struct | global::System.AttributeTargets.Method, AllowMultiple = true)]
	public class GenerateSerializationForTypeAttribute : global::System.Attribute
	{
		internal global::System.Type Type;

		public GenerateSerializationForTypeAttribute(global::System.Type type)
		{
			Type = type;
		}
	}
}
