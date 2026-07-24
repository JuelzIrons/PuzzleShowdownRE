namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Math/Generic")]
	[global::Unity.VisualScripting.UnitTitle("Subtract")]
	public sealed class GenericSubtract : global::Unity.VisualScripting.Subtract<object>
	{
		public override object Operation(object a, object b)
		{
			return global::Unity.VisualScripting.OperatorUtility.Subtract(a, b);
		}
	}
}
