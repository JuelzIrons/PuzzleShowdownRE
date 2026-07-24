namespace Unity.VisualScripting
{
	[global::System.AttributeUsage(global::System.AttributeTargets.Property | global::System.AttributeTargets.Field | global::System.AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
	public sealed class InspectorRangeAttribute : global::System.Attribute
	{
		public float min { get; private set; }

		public float max { get; private set; }

		public InspectorRangeAttribute(float min, float max)
		{
			this.min = min;
			this.max = max;
		}
	}
}
