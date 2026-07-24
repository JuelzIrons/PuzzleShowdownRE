namespace Unity.VisualScripting
{
	[global::System.AttributeUsage(global::System.AttributeTargets.Property | global::System.AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
	public sealed class WarnBeforeEditingAttribute : global::System.Attribute
	{
		public string warningTitle { get; }

		public string warningMessage { get; }

		public object[] emptyValues { get; }

		public WarnBeforeEditingAttribute(string warningTitle, string warningMessage)
		{
			this.warningTitle = warningTitle;
			this.warningMessage = warningMessage;
		}

		public WarnBeforeEditingAttribute(string warningTitle, string warningMessage, params object[] emptyValues)
			: this(warningTitle, warningMessage)
		{
			this.emptyValues = emptyValues;
		}
	}
}
