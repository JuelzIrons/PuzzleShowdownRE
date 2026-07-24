namespace Unity.VisualScripting
{
	[global::System.AttributeUsage(global::System.AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	public sealed class UnitSubtitleAttribute : global::System.Attribute
	{
		public string subtitle { get; private set; }

		public UnitSubtitleAttribute(string subtitle)
		{
			this.subtitle = subtitle;
		}
	}
}
