namespace UnityEngine.Timeline
{
	[global::System.AttributeUsage(global::System.AttributeTargets.Class)]
	public class CustomStyleAttribute : global::System.Attribute
	{
		public readonly string ussStyle;

		public CustomStyleAttribute(string ussStyle)
		{
			this.ussStyle = ussStyle;
		}
	}
}
