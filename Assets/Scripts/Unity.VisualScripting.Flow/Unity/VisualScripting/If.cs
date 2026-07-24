namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Control")]
	[global::Unity.VisualScripting.UnitOrder(0)]
	[global::Unity.VisualScripting.RenamedFrom("Bolt.Branch")]
	[global::Unity.VisualScripting.RenamedFrom("Unity.VisualScripting.Branch")]
	public sealed class If : global::Unity.VisualScripting.Unit, global::Unity.VisualScripting.IBranchUnit, global::Unity.VisualScripting.IUnit, global::Unity.VisualScripting.IGraphElementWithDebugData, global::Unity.VisualScripting.IGraphElement, global::Unity.VisualScripting.IGraphItem, global::Unity.VisualScripting.INotifiedCollectionItem, global::System.IDisposable, global::Unity.VisualScripting.IPrewarmable, global::Unity.VisualScripting.IAotStubbable, global::Unity.VisualScripting.IIdentifiable, global::Unity.VisualScripting.IAnalyticsIdentifiable
	{
		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ControlInput enter { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ValueInput condition { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabel("True")]
		public global::Unity.VisualScripting.ControlOutput ifTrue { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabel("False")]
		public global::Unity.VisualScripting.ControlOutput ifFalse { get; private set; }

		global::Unity.VisualScripting.FlowGraph global::Unity.VisualScripting.IUnit.graph => base.graph;

		protected override void Definition()
		{
			enter = ControlInput("enter", Enter);
			condition = ValueInput<bool>("condition");
			ifTrue = ControlOutput("ifTrue");
			ifFalse = ControlOutput("ifFalse");
			Requirement(condition, enter);
			Succession(enter, ifTrue);
			Succession(enter, ifFalse);
		}

		public global::Unity.VisualScripting.ControlOutput Enter(global::Unity.VisualScripting.Flow flow)
		{
			if (!flow.GetValue<bool>(condition))
			{
				return ifFalse;
			}
			return ifTrue;
		}
	}
}
