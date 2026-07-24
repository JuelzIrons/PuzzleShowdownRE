namespace Unity.VisualScripting
{
	[global::System.AttributeUsage(global::System.AttributeTargets.Class | global::System.AttributeTargets.Struct | global::System.AttributeTargets.Property | global::System.AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
	public sealed class InspectableAttribute : global::System.Attribute, global::Unity.VisualScripting.IInspectableAttribute
	{
		public int order { get; set; }
	}
}
