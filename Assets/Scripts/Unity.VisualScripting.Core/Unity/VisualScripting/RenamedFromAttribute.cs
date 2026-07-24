namespace Unity.VisualScripting
{
	[global::System.AttributeUsage(global::System.AttributeTargets.All, AllowMultiple = true, Inherited = true)]
	public sealed class RenamedFromAttribute : global::System.Attribute
	{
		public string previousName { get; }

		public RenamedFromAttribute(string previousName)
		{
			this.previousName = previousName;
		}
	}
}
