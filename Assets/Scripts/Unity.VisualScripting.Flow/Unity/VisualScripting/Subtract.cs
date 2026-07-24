namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitOrder(102)]
	public abstract class Subtract<T> : global::Unity.VisualScripting.Unit
	{
		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabel("A")]
		public global::Unity.VisualScripting.ValueInput minuend { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabel("B")]
		public global::Unity.VisualScripting.ValueInput subtrahend { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabel("A − B")]
		public global::Unity.VisualScripting.ValueOutput difference { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		protected virtual T defaultMinuend => default(T);

		[global::Unity.VisualScripting.DoNotSerialize]
		protected virtual T defaultSubtrahend => default(T);

		protected override void Definition()
		{
			minuend = ValueInput("minuend", defaultMinuend);
			subtrahend = ValueInput("subtrahend", defaultSubtrahend);
			difference = ValueOutput("difference", Operation).Predictable();
			Requirement(minuend, difference);
			Requirement(subtrahend, difference);
		}

		public abstract T Operation(T a, T b);

		public T Operation(global::Unity.VisualScripting.Flow flow)
		{
			return Operation(flow.GetValue<T>(minuend), flow.GetValue<T>(subtrahend));
		}
	}
}
