namespace Unity.VisualScripting
{
	[global::System.AttributeUsage(global::System.AttributeTargets.Property | global::System.AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
	public sealed class WarnBeforeRemovingAttribute : global::System.Attribute
	{
		public string warningTitle { get; }

		public string warningMessage { get; }

		public WarnBeforeRemovingAttribute(string warningTitle, string warningMessage)
		{
			this.warningTitle = warningTitle;
			this.warningMessage = warningMessage;
		}
	}
}
