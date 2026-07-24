namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitOrder(406)]
	public abstract class Project<T> : global::Unity.VisualScripting.Unit
	{
		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ValueInput a { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ValueInput b { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ValueOutput projection { get; private set; }

		protected override void Definition()
		{
			a = ValueInput<T>("a");
			b = ValueInput<T>("b");
			projection = ValueOutput("projection", Operation).Predictable();
			Requirement(a, projection);
			Requirement(b, projection);
		}

		private T Operation(global::Unity.VisualScripting.Flow flow)
		{
			return Operation(flow.GetValue<T>(a), flow.GetValue<T>(b));
		}

		public abstract T Operation(T a, T b);
	}
}
