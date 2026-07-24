namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Math/Scalar")]
	[global::Unity.VisualScripting.UnitTitle("Exponentiate")]
	[global::Unity.VisualScripting.UnitOrder(105)]
	public sealed class ScalarExponentiate : global::Unity.VisualScripting.Unit
	{
		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabel("x")]
		public global::Unity.VisualScripting.ValueInput @base { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabel("n")]
		public global::Unity.VisualScripting.ValueInput exponent { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabel("xⁿ")]
		public global::Unity.VisualScripting.ValueOutput power { get; private set; }

		protected override void Definition()
		{
			@base = ValueInput("base", 1f);
			exponent = ValueInput("exponent", 2f);
			power = ValueOutput("power", Exponentiate);
			Requirement(@base, power);
			Requirement(exponent, power);
		}

		public float Exponentiate(global::Unity.VisualScripting.Flow flow)
		{
			return global::UnityEngine.Mathf.Pow(flow.GetValue<float>(@base), flow.GetValue<float>(exponent));
		}
	}
}
