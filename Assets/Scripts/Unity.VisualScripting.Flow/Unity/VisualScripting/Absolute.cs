namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitOrder(201)]
	public abstract class Absolute<TInput> : global::Unity.VisualScripting.Unit
	{
		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ValueInput input { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ValueOutput output { get; private set; }

		protected override void Definition()
		{
			input = ValueInput<TInput>("input");
			output = ValueOutput("output", Operation).Predictable();
			Requirement(input, output);
		}

		protected abstract TInput Operation(TInput input);

		public TInput Operation(global::Unity.VisualScripting.Flow flow)
		{
			return Operation(flow.GetValue<TInput>(input));
		}
	}
}
