namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Logic")]
	[global::Unity.VisualScripting.UnitOrder(3)]
	public sealed class Negate : global::Unity.VisualScripting.Unit
	{
		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabel("X")]
		public global::Unity.VisualScripting.ValueInput input { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabel("~X")]
		public global::Unity.VisualScripting.ValueOutput output { get; private set; }

		protected override void Definition()
		{
			input = ValueInput<bool>("input");
			output = ValueOutput("output", Operation).Predictable();
			Requirement(input, output);
		}

		public bool Operation(global::Unity.VisualScripting.Flow flow)
		{
			return !flow.GetValue<bool>(input);
		}
	}
}
