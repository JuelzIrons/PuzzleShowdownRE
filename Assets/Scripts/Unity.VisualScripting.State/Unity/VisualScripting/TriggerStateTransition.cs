namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitSurtitle("State")]
	[global::Unity.VisualScripting.UnitCategory("Nesting")]
	[global::Unity.VisualScripting.UnitShortTitle("Trigger Transition")]
	[global::Unity.VisualScripting.TypeIcon(typeof(global::Unity.VisualScripting.IStateTransition))]
	public sealed class TriggerStateTransition : global::Unity.VisualScripting.Unit
	{
		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ControlInput trigger { get; private set; }

		protected override void Definition()
		{
			trigger = ControlInput("trigger", Trigger);
		}

		private global::Unity.VisualScripting.ControlOutput Trigger(global::Unity.VisualScripting.Flow flow)
		{
			global::Unity.VisualScripting.INesterStateTransition parent = flow.stack.GetParent<global::Unity.VisualScripting.INesterStateTransition>();
			flow.stack.ExitParentElement();
			parent.Branch(flow);
			return null;
		}
	}
}
