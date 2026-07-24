namespace Unity.VisualScripting
{
	[global::System.AttributeUsage(global::System.AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	public sealed class UnitShortTitleAttribute : global::System.Attribute
	{
		public string title { get; private set; }

		public UnitShortTitleAttribute(string title)
		{
			this.title = title;
		}
	}
}
