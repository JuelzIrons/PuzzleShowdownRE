namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitTitle("Break Loop")]
	[global::Unity.VisualScripting.UnitCategory("Control")]
	[global::Unity.VisualScripting.UnitOrder(13)]
	public class Break : global::Unity.VisualScripting.Unit
	{
		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ControlInput enter { get; private set; }

		protected override void Definition()
		{
			enter = ControlInput("enter", Operation);
		}

		public global::Unity.VisualScripting.ControlOutput Operation(global::Unity.VisualScripting.Flow flow)
		{
			flow.BreakLoop();
			return null;
		}
	}
}
