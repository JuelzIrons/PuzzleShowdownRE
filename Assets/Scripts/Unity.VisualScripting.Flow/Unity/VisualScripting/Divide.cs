namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitOrder(104)]
	public abstract class Divide<T> : global::Unity.VisualScripting.Unit
	{
		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabel("A")]
		public global::Unity.VisualScripting.ValueInput dividend { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabel("B")]
		public global::Unity.VisualScripting.ValueInput divisor { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabel("A ÷ B")]
		public global::Unity.VisualScripting.ValueOutput quotient { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		protected virtual T defaultDivisor => default(T);

		[global::Unity.VisualScripting.DoNotSerialize]
		protected virtual T defaultDividend => default(T);

		protected override void Definition()
		{
			dividend = ValueInput("dividend", defaultDividend);
			divisor = ValueInput("divisor", defaultDivisor);
			quotient = ValueOutput("quotient", Operation).Predictable();
			Requirement(dividend, quotient);
			Requirement(divisor, quotient);
		}

		public abstract T Operation(T divident, T divisor);

		public T Operation(global::Unity.VisualScripting.Flow flow)
		{
			return Operation(flow.GetValue<T>(dividend), flow.GetValue<T>(divisor));
		}
	}
}
