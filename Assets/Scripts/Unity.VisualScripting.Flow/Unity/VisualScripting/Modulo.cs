namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitOrder(105)]
	public abstract class Modulo<T> : global::Unity.VisualScripting.Unit
	{
		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabel("A")]
		public global::Unity.VisualScripting.ValueInput dividend { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabel("B")]
		public global::Unity.VisualScripting.ValueInput divisor { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabel("A % B")]
		public global::Unity.VisualScripting.ValueOutput remainder { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		protected virtual T defaultDivisor => default(T);

		[global::Unity.VisualScripting.DoNotSerialize]
		protected virtual T defaultDividend => default(T);

		protected override void Definition()
		{
			dividend = ValueInput("dividend", defaultDividend);
			divisor = ValueInput("divisor", defaultDivisor);
			remainder = ValueOutput("remainder", Operation).Predictable();
			Requirement(dividend, remainder);
			Requirement(divisor, remainder);
		}

		public abstract T Operation(T divident, T divisor);

		public T Operation(global::Unity.VisualScripting.Flow flow)
		{
			return Operation(flow.GetValue<T>(dividend), flow.GetValue<T>(divisor));
		}
	}
}
