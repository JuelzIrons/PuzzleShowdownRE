namespace UnityEngine.InputSystem.Utilities
{
	[global::System.AttributeUsage(global::System.AttributeTargets.Class, Inherited = true)]
	public class DisplayStringFormatAttribute : global::System.Attribute
	{
		public string formatString { get; set; }

		public DisplayStringFormatAttribute(string formatString)
		{
			this.formatString = formatString;
		}
	}
}
