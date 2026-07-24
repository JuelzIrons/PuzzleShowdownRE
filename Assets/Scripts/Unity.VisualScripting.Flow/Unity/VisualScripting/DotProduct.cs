namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitOrder(404)]
	public abstract class DotProduct<T> : global::Unity.VisualScripting.Unit
	{
		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ValueInput a { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ValueInput b { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabel("A∙B")]
		public global::Unity.VisualScripting.ValueOutput dotProduct { get; private set; }

		protected override void Definition()
		{
			a = ValueInput<T>("a");
			b = ValueInput<T>("b");
			dotProduct = ValueOutput("dotProduct", Operation).Predictable();
			Requirement(a, dotProduct);
			Requirement(b, dotProduct);
		}

		private float Operation(global::Unity.VisualScripting.Flow flow)
		{
			return Operation(flow.GetValue<T>(a), flow.GetValue<T>(b));
		}

		public abstract float Operation(T a, T b);
	}
}
