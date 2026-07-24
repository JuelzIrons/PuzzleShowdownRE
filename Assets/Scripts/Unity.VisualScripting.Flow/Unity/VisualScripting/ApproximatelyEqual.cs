namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Logic")]
	[global::Unity.VisualScripting.UnitShortTitle("Equal")]
	[global::Unity.VisualScripting.UnitSubtitle("(Approximately)")]
	[global::Unity.VisualScripting.UnitOrder(7)]
	[global::System.Obsolete("Use the Equal node with Numeric enabled instead.")]
	public sealed class ApproximatelyEqual : global::Unity.VisualScripting.Unit
	{
		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ValueInput a { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ValueInput b { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabel("A ≈ B")]
		public global::Unity.VisualScripting.ValueOutput equal { get; private set; }

		protected override void Definition()
		{
			a = ValueInput<float>("a");
			b = ValueInput("b", 0f);
			equal = ValueOutput("equal", Comparison).Predictable();
			Requirement(a, equal);
			Requirement(b, equal);
		}

		public bool Comparison(global::Unity.VisualScripting.Flow flow)
		{
			return global::UnityEngine.Mathf.Approximately(flow.GetValue<float>(a), flow.GetValue<float>(b));
		}
	}
}
