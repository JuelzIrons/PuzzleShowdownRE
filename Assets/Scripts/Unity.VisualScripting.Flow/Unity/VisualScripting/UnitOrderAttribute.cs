namespace Unity.VisualScripting
{
	[global::System.AttributeUsage(global::System.AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	public sealed class UnitOrderAttribute : global::System.Attribute
	{
		public int order { get; private set; }

		public UnitOrderAttribute(int order)
		{
			this.order = order;
		}
	}
}
