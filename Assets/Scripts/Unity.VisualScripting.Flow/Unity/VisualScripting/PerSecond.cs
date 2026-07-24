namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitOrder(601)]
	public abstract class PerSecond<T> : global::Unity.VisualScripting.Unit
	{
		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ValueInput input { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ValueOutput output { get; private set; }

		protected override void Definition()
		{
			input = ValueInput("input", default(T));
			output = ValueOutput("output", Operation);
			Requirement(input, output);
		}

		public abstract T Operation(T input);

		public T Operation(global::Unity.VisualScripting.Flow flow)
		{
			return Operation(flow.GetValue<T>(input));
		}
	}
}
