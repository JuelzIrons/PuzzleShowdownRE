namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitOrder(405)]
	[global::Unity.VisualScripting.TypeIcon(typeof(global::Unity.VisualScripting.Multiply<>))]
	public abstract class CrossProduct<T> : global::Unity.VisualScripting.Unit
	{
		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ValueInput a { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ValueInput b { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabel("A × B")]
		public global::Unity.VisualScripting.ValueOutput crossProduct { get; private set; }

		protected override void Definition()
		{
			a = ValueInput<T>("a");
			b = ValueInput<T>("b");
			crossProduct = ValueOutput("crossProduct", Operation).Predictable();
			Requirement(a, crossProduct);
			Requirement(b, crossProduct);
		}

		private T Operation(global::Unity.VisualScripting.Flow flow)
		{
			return Operation(flow.GetValue<T>(a), flow.GetValue<T>(b));
		}

		public abstract T Operation(T a, T b);
	}
}
