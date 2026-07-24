namespace Unity.VisualScripting
{
	[global::System.AttributeUsage(global::System.AttributeTargets.Property | global::System.AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
	public sealed class ExpectedTypeAttribute : global::System.Attribute
	{
		public global::System.Type type { get; }

		public ExpectedTypeAttribute(global::System.Type type)
		{
			global::Unity.VisualScripting.Ensure.That("type").IsNotNull(type);
			this.type = type;
		}
	}
}
