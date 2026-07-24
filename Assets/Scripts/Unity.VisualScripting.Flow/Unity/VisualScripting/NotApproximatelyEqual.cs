namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Logic")]
	[global::Unity.VisualScripting.UnitShortTitle("Not Equal")]
	[global::Unity.VisualScripting.UnitSubtitle("(Approximately)")]
	[global::Unity.VisualScripting.UnitOrder(8)]
	[global::System.Obsolete("Use the Not Equal node with Numeric enabled instead.")]
	public sealed class NotApproximatelyEqual : global::Unity.VisualScripting.Unit
	{
		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ValueInput a { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ValueInput b { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabel("A ≉ B")]
		public global::Unity.VisualScripting.ValueOutput notEqual { get; private set; }

		protected override void Definition()
		{
			a = ValueInput<float>("a");
			b = ValueInput("b", 0f);
			notEqual = ValueOutput("notEqual", Comparison).Predictable();
			Requirement(a, notEqual);
			Requirement(b, notEqual);
		}

		public bool Comparison(global::Unity.VisualScripting.Flow flow)
		{
			return !global::UnityEngine.Mathf.Approximately(flow.GetValue<float>(a), flow.GetValue<float>(b));
		}
	}
}
