namespace Unity.VisualScripting.FullSerializer
{
	[global::System.AttributeUsage(global::System.AttributeTargets.Property | global::System.AttributeTargets.Field)]
	public class fsPropertyAttribute : global::System.Attribute
	{
		public string Name;

		public global::System.Type Converter;

		public fsPropertyAttribute()
			: this(string.Empty)
		{
		}

		public fsPropertyAttribute(string name)
		{
			Name = name;
		}
	}
}
