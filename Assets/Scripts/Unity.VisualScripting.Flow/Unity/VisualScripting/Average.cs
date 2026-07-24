namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitOrder(304)]
	public abstract class Average<T> : global::Unity.VisualScripting.MultiInputUnit<T>
	{
		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ValueOutput average { get; private set; }

		protected override void Definition()
		{
			base.Definition();
			average = ValueOutput("average", Operation).Predictable();
			foreach (global::Unity.VisualScripting.ValueInput multiInput in base.multiInputs)
			{
				Requirement(multiInput, average);
			}
		}

		public abstract T Operation(T a, T b);

		public abstract T Operation(global::System.Collections.Generic.IEnumerable<T> values);

		public T Operation(global::Unity.VisualScripting.Flow flow)
		{
			if (inputCount == 2)
			{
				return Operation(flow.GetValue<T>(base.multiInputs[0]), flow.GetValue<T>(base.multiInputs[1]));
			}
			return Operation(global::System.Linq.Enumerable.Select(base.multiInputs, flow.GetValue<T>));
		}
	}
}
