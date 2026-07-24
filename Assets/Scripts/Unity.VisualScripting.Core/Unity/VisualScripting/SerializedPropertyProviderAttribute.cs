namespace Unity.VisualScripting
{
	[global::System.AttributeUsage(global::System.AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	public sealed class SerializedPropertyProviderAttribute : global::System.Attribute, global::Unity.VisualScripting.IDecoratorAttribute
	{
		public global::System.Type type { get; private set; }

		public SerializedPropertyProviderAttribute(global::System.Type type)
		{
			this.type = type;
		}
	}
}
