namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Math/Generic")]
	[global::Unity.VisualScripting.UnitTitle("Add")]
	[global::Unity.VisualScripting.RenamedFrom("Bolt.GenericAdd")]
	[global::Unity.VisualScripting.RenamedFrom("Unity.VisualScripting.GenericAdd")]
	[global::System.Obsolete("Use the new \"Add (Math/Generic)\" node instead.")]
	public sealed class DeprecatedGenericAdd : global::Unity.VisualScripting.Add<object>
	{
		public override object Operation(object a, object b)
		{
			return global::Unity.VisualScripting.OperatorUtility.Add(a, b);
		}
	}
}
