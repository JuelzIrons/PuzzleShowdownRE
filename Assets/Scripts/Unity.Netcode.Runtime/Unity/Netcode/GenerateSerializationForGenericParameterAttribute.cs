namespace Unity.Netcode
{
	[global::System.AttributeUsage(global::System.AttributeTargets.Class | global::System.AttributeTargets.Struct, AllowMultiple = true)]
	public class GenerateSerializationForGenericParameterAttribute : global::System.Attribute
	{
		internal int ParameterIndex;

		public GenerateSerializationForGenericParameterAttribute(int parameterIndex)
		{
			ParameterIndex = parameterIndex;
		}
	}
}
