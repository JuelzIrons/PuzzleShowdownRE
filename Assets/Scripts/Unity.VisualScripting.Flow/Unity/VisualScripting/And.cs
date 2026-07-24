namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Logic")]
	[global::Unity.VisualScripting.UnitOrder(0)]
	public sealed class And : global::Unity.VisualScripting.Unit
	{
		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ValueInput a { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ValueInput b { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabel("A & B")]
		public global::Unity.VisualScripting.ValueOutput result { get; private set; }

		protected override void Definition()
		{
			a = ValueInput<bool>("a");
			b = ValueInput<bool>("b");
			result = ValueOutput("result", Operation).Predictable();
			Requirement(a, result);
			Requirement(b, result);
		}

		public bool Operation(global::Unity.VisualScripting.Flow flow)
		{
			if (flow.GetValue<bool>(a))
			{
				return flow.GetValue<bool>(b);
			}
			return false;
		}
	}
}
