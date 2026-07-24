namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Math/Generic")]
	[global::Unity.VisualScripting.UnitTitle("Divide")]
	public sealed class GenericDivide : global::Unity.VisualScripting.Divide<object>
	{
		public override object Operation(object a, object b)
		{
			return global::Unity.VisualScripting.OperatorUtility.Divide(a, b);
		}
	}
}
