namespace Unity.VisualScripting
{
	[global::System.AttributeUsage(global::System.AttributeTargets.Property | global::System.AttributeTargets.Field | global::System.AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
	public sealed class InspectorActionDirectionAttribute : global::System.Attribute
	{
		public global::Unity.VisualScripting.ActionDirection direction { get; private set; }

		public InspectorActionDirectionAttribute(global::Unity.VisualScripting.ActionDirection direction)
		{
			this.direction = direction;
		}
	}
}
