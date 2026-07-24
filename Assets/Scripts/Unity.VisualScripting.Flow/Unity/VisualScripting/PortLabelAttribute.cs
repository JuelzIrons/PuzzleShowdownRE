namespace Unity.VisualScripting
{
	[global::System.AttributeUsage(global::System.AttributeTargets.Property | global::System.AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
	public class PortLabelAttribute : global::System.Attribute
	{
		public string label { get; private set; }

		public bool hidden { get; set; }

		public PortLabelAttribute(string label)
		{
			this.label = label;
		}
	}
}
