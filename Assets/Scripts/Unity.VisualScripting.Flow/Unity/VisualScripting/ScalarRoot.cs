namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Math/Scalar")]
	[global::Unity.VisualScripting.UnitTitle("Root")]
	[global::Unity.VisualScripting.UnitOrder(106)]
	public sealed class ScalarRoot : global::Unity.VisualScripting.Unit
	{
		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabel("x")]
		public global::Unity.VisualScripting.ValueInput radicand { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabel("n")]
		public global::Unity.VisualScripting.ValueInput degree { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabel("ⁿ√x")]
		public global::Unity.VisualScripting.ValueOutput root { get; private set; }

		protected override void Definition()
		{
			radicand = ValueInput("radicand", 1f);
			degree = ValueInput("degree", 2f);
			root = ValueOutput("root", Root);
			Requirement(radicand, root);
			Requirement(degree, root);
		}

		public float Root(global::Unity.VisualScripting.Flow flow)
		{
			float value = flow.GetValue<float>(degree);
			float value2 = flow.GetValue<float>(radicand);
			if (value == 2f)
			{
				return global::UnityEngine.Mathf.Sqrt(value2);
			}
			return global::UnityEngine.Mathf.Pow(value2, 1f / value);
		}
	}
}
