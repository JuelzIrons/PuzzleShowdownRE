namespace Unity.VisualScripting.FullSerializer
{
	[global::System.AttributeUsage(global::System.AttributeTargets.Class | global::System.AttributeTargets.Struct | global::System.AttributeTargets.Interface)]
	public sealed class fsForwardAttribute : global::System.Attribute
	{
		public string MemberName;

		public fsForwardAttribute(string memberName)
		{
			MemberName = memberName;
		}
	}
}
