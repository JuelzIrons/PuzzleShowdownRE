namespace Unity.VisualScripting
{
	[global::System.AttributeUsage(global::System.AttributeTargets.Class | global::System.AttributeTargets.Struct, AllowMultiple = false, Inherited = true)]
	public sealed class IncludeInSettingsAttribute : global::System.Attribute
	{
		public bool include { get; private set; }

		public IncludeInSettingsAttribute(bool include)
		{
			this.include = include;
		}
	}
}
