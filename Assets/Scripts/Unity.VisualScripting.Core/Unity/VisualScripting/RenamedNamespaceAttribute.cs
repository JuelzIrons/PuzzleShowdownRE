namespace Unity.VisualScripting
{
	[global::System.AttributeUsage(global::System.AttributeTargets.Assembly, AllowMultiple = true)]
	public sealed class RenamedNamespaceAttribute : global::System.Attribute
	{
		public string previousName { get; }

		public string newName { get; }

		public RenamedNamespaceAttribute(string previousName, string newName)
		{
			this.previousName = previousName;
			this.newName = newName;
		}
	}
}
