namespace Unity.VisualScripting
{
	[global::System.AttributeUsage(global::System.AttributeTargets.Property | global::System.AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
	public sealed class UnitHeaderInspectableAttribute : global::System.Attribute
	{
		public string label { get; }

		public UnitHeaderInspectableAttribute()
		{
		}

		public UnitHeaderInspectableAttribute(string label)
		{
			this.label = label;
		}
	}
}
