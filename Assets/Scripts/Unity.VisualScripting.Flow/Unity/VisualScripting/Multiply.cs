namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitOrder(103)]
	public abstract class Multiply<T> : global::Unity.VisualScripting.Unit
	{
		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ValueInput a { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ValueInput b { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabel("A × B")]
		public global::Unity.VisualScripting.ValueOutput product { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		protected virtual T defaultB => default(T);

		protected override void Definition()
		{
			a = ValueInput<T>("a");
			b = ValueInput("b", defaultB);
			product = ValueOutput("product", Operation).Predictable();
			Requirement(a, product);
			Requirement(b, product);
		}

		private T Operation(global::Unity.VisualScripting.Flow flow)
		{
			return Operation(flow.GetValue<T>(a), flow.GetValue<T>(b));
		}

		public abstract T Operation(T a, T b);
	}
}
