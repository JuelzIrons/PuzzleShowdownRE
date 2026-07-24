namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Control")]
	[global::Unity.VisualScripting.UnitOrder(15)]
	public sealed class Cache : global::Unity.VisualScripting.Unit
	{
		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ControlInput enter { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ValueInput input { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabel("Cached")]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ValueOutput output { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ControlOutput exit { get; private set; }

		protected override void Definition()
		{
			enter = ControlInput("enter", Store);
			input = ValueInput<object>("input");
			output = ValueOutput<object>("output");
			exit = ControlOutput("exit");
			Requirement(input, enter);
			Assignment(enter, output);
			Succession(enter, exit);
		}

		private global::Unity.VisualScripting.ControlOutput Store(global::Unity.VisualScripting.Flow flow)
		{
			flow.SetValue(output, flow.GetValue(input));
			return exit;
		}
	}
}
