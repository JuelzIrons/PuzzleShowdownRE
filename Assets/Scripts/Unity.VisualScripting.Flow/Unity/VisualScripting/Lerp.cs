namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitOrder(501)]
	public abstract class Lerp<T> : global::Unity.VisualScripting.Unit
	{
		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ValueInput a { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ValueInput b { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ValueInput t { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ValueOutput interpolation { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		protected virtual T defaultA => default(T);

		[global::Unity.VisualScripting.DoNotSerialize]
		protected virtual T defaultB => default(T);

		protected override void Definition()
		{
			a = ValueInput("a", defaultA);
			b = ValueInput("b", defaultB);
			t = ValueInput("t", 0f);
			interpolation = ValueOutput("interpolation", Operation).Predictable();
			Requirement(a, interpolation);
			Requirement(b, interpolation);
			Requirement(t, interpolation);
		}

		private T Operation(global::Unity.VisualScripting.Flow flow)
		{
			return Operation(flow.GetValue<T>(a), flow.GetValue<T>(b), flow.GetValue<float>(t));
		}

		public abstract T Operation(T a, T b, float t);
	}
}
