namespace Unity.VisualScripting
{
	[global::System.AttributeUsage(global::System.AttributeTargets.Property | global::System.AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
	public class TypeSetAttribute : global::System.Attribute
	{
		public global::Unity.VisualScripting.TypeSet typeSet { get; }

		public TypeSetAttribute(global::Unity.VisualScripting.TypeSet typeSet)
		{
			this.typeSet = typeSet;
		}
	}
}
