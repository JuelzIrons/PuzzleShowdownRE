namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitOrder(402)]
	public abstract class Distance<T> : global::Unity.VisualScripting.Unit
	{
		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ValueInput a { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ValueInput b { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ValueOutput distance { get; private set; }

		protected override void Definition()
		{
			a = ValueInput<T>("a");
			b = ValueInput<T>("b");
			distance = ValueOutput("distance", Operation).Predictable();
			Requirement(a, distance);
			Requirement(b, distance);
		}

		private float Operation(global::Unity.VisualScripting.Flow flow)
		{
			return Operation(flow.GetValue<T>(a), flow.GetValue<T>(b));
		}

		public abstract float Operation(T a, T b);
	}
}
