namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Math/Generic")]
	[global::Unity.VisualScripting.UnitTitle("Modulo")]
	public sealed class GenericModulo : global::Unity.VisualScripting.Modulo<object>
	{
		public override object Operation(object a, object b)
		{
			return global::Unity.VisualScripting.OperatorUtility.Modulo(a, b);
		}
	}
}
