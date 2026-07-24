namespace Unity.VisualScripting
{
	[global::System.AttributeUsage(global::System.AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	public sealed class InspectorAdaptiveWidthAttribute : global::System.Attribute
	{
		public float width { get; private set; }

		public InspectorAdaptiveWidthAttribute(float width)
		{
			this.width = width;
		}
	}
}
