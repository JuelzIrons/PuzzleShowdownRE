namespace Unity.VisualScripting
{
	[global::System.AttributeUsage(global::System.AttributeTargets.Interface, AllowMultiple = false, Inherited = false)]
	public sealed class TypeIconPriorityAttribute : global::System.Attribute
	{
		public int priority { get; }

		public TypeIconPriorityAttribute(int priority)
		{
			this.priority = priority;
		}

		public TypeIconPriorityAttribute()
		{
			priority = 0;
		}
	}
}
