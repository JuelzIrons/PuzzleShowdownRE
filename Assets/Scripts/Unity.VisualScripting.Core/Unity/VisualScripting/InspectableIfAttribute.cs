namespace Unity.VisualScripting
{
	[global::System.AttributeUsage(global::System.AttributeTargets.Class | global::System.AttributeTargets.Struct | global::System.AttributeTargets.Property | global::System.AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
	public sealed class InspectableIfAttribute : global::System.Attribute, global::Unity.VisualScripting.IInspectableAttribute
	{
		public int order { get; set; }

		public string conditionMember { get; }

		public InspectableIfAttribute(string conditionMember)
		{
			this.conditionMember = conditionMember;
		}
	}
}
