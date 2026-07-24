namespace Unity.VisualScripting
{
	[global::System.AttributeUsage(global::System.AttributeTargets.Class | global::System.AttributeTargets.Struct | global::System.AttributeTargets.Interface, AllowMultiple = false, Inherited = true)]
	public sealed class TypeIconAttribute : global::System.Attribute
	{
		public global::System.Type type { get; }

		public TypeIconAttribute(global::System.Type type)
		{
			global::Unity.VisualScripting.Ensure.That("type").IsNotNull(type);
			this.type = type;
		}
	}
}
