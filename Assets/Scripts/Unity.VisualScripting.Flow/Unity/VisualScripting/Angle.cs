namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitOrder(403)]
	public abstract class Angle<T> : global::Unity.VisualScripting.Unit
	{
		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ValueInput a { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ValueInput b { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ValueOutput angle { get; private set; }

		protected override void Definition()
		{
			a = ValueInput<T>("a");
			b = ValueInput<T>("b");
			angle = ValueOutput("angle", Operation).Predictable();
			Requirement(a, angle);
			Requirement(b, angle);
		}

		private float Operation(global::Unity.VisualScripting.Flow flow)
		{
			return Operation(flow.GetValue<T>(a), flow.GetValue<T>(b));
		}

		public abstract float Operation(T a, T b);
	}
}
