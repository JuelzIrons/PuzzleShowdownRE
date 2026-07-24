namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Math/Generic")]
	[global::Unity.VisualScripting.UnitTitle("Multiply")]
	public sealed class GenericMultiply : global::Unity.VisualScripting.Multiply<object>
	{
		public override object Operation(object a, object b)
		{
			return global::Unity.VisualScripting.OperatorUtility.Multiply(a, b);
		}
	}
}
