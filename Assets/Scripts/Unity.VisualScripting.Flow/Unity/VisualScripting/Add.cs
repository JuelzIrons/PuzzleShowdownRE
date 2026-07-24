namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitOrder(101)]
	public abstract class Add<T> : global::Unity.VisualScripting.Unit
	{
		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ValueInput a { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ValueInput b { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabel("A + B")]
		public global::Unity.VisualScripting.ValueOutput sum { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		protected virtual T defaultB => default(T);

		protected override void Definition()
		{
			a = ValueInput<T>("a");
			b = ValueInput("b", defaultB);
			sum = ValueOutput("sum", Operation).Predictable();
			Requirement(a, sum);
			Requirement(b, sum);
		}

		private T Operation(global::Unity.VisualScripting.Flow flow)
		{
			return Operation(flow.GetValue<T>(a), flow.GetValue<T>(b));
		}

		public abstract T Operation(T a, T b);
	}
}
