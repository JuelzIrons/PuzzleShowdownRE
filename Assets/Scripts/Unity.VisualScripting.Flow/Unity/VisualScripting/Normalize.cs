namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitOrder(401)]
	public abstract class Normalize<T> : global::Unity.VisualScripting.Unit
	{
		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ValueInput input { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ValueOutput output { get; private set; }

		protected override void Definition()
		{
			input = ValueInput<T>("input");
			output = ValueOutput("output", Operation).Predictable();
			Requirement(input, output);
		}

		private T Operation(global::Unity.VisualScripting.Flow flow)
		{
			return Operation(flow.GetValue<T>(input));
		}

		public abstract T Operation(T input);
	}
}
