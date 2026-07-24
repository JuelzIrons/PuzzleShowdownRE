namespace Unity.VisualScripting
{
	[global::System.AttributeUsage(global::System.AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	public sealed class UnitTitleAttribute : global::System.Attribute
	{
		public string title { get; private set; }

		public UnitTitleAttribute(string title)
		{
			this.title = title;
		}
	}
}
