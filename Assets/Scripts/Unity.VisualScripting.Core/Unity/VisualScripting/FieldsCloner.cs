namespace Unity.VisualScripting
{
	public sealed class FieldsCloner : global::Unity.VisualScripting.ReflectedCloner
	{
		protected override bool IncludeField(global::System.Reflection.FieldInfo field)
		{
			return true;
		}

		protected override bool IncludeProperty(global::System.Reflection.PropertyInfo property)
		{
			return false;
		}
	}
}
