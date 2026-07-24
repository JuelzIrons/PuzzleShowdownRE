namespace Unity.VisualScripting
{
	[global::System.AttributeUsage(global::System.AttributeTargets.All, AllowMultiple = false, Inherited = true)]
	public sealed class InspectorLabelAttribute : global::System.Attribute
	{
		public string text { get; private set; }

		public string tooltip { get; private set; }

		public global::UnityEngine.Texture image { get; set; }

		public InspectorLabelAttribute(string text)
		{
			this.text = text;
		}

		public InspectorLabelAttribute(string text, string tooltip)
		{
			this.text = text;
			this.tooltip = tooltip;
		}
	}
}
