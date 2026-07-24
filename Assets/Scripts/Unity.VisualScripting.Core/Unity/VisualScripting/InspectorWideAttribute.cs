namespace Unity.VisualScripting
{
	[global::System.AttributeUsage(global::System.AttributeTargets.All, AllowMultiple = false, Inherited = true)]
	public sealed class InspectorWideAttribute : global::System.Attribute
	{
		public bool toEdge { get; private set; }

		public InspectorWideAttribute()
		{
		}

		public InspectorWideAttribute(bool toEdge)
		{
			this.toEdge = toEdge;
		}
	}
}
