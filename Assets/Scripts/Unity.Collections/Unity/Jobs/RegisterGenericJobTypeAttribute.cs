namespace Unity.Jobs
{
	[global::UnityEngine.Scripting.APIUpdating.MovedFrom(true, "Unity.Entities", "Unity.Entities", null)]
	[global::System.AttributeUsage(global::System.AttributeTargets.Assembly, AllowMultiple = true)]
	public class RegisterGenericJobTypeAttribute : global::System.Attribute
	{
		public global::System.Type ConcreteType;

		public RegisterGenericJobTypeAttribute(global::System.Type type)
		{
			ConcreteType = type;
		}
	}
}
