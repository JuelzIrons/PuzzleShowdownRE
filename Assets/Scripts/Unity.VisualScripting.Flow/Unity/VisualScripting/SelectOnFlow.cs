namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Control")]
	[global::Unity.VisualScripting.UnitTitle("Select On Flow")]
	[global::Unity.VisualScripting.UnitShortTitle("Select")]
	[global::Unity.VisualScripting.UnitSubtitle("On Flow")]
	[global::Unity.VisualScripting.UnitOrder(8)]
	[global::Unity.VisualScripting.TypeIcon(typeof(global::Unity.VisualScripting.ISelectUnit))]
	public sealed class SelectOnFlow : global::Unity.VisualScripting.Unit, global::Unity.VisualScripting.ISelectUnit, global::Unity.VisualScripting.IUnit, global::Unity.VisualScripting.IGraphElementWithDebugData, global::Unity.VisualScripting.IGraphElement, global::Unity.VisualScripting.IGraphItem, global::Unity.VisualScripting.INotifiedCollectionItem, global::System.IDisposable, global::Unity.VisualScripting.IPrewarmable, global::Unity.VisualScripting.IAotStubbable, global::Unity.VisualScripting.IIdentifiable, global::Unity.VisualScripting.IAnalyticsIdentifiable
	{
		[global::Unity.VisualScripting.SerializeAs("branchCount")]
		private int _branchCount = 2;

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.Inspectable]
		[global::Unity.VisualScripting.UnitHeaderInspectable("Branches")]
		public int branchCount
		{
			get
			{
				return _branchCount;
			}
			set
			{
				_branchCount = global::UnityEngine.Mathf.Clamp(value, 2, 10);
			}
		}

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::System.Collections.Generic.Dictionary<global::Unity.VisualScripting.ControlInput, global::Unity.VisualScripting.ValueInput> branches { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ControlOutput exit { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ValueOutput selection { get; private set; }

		global::Unity.VisualScripting.FlowGraph global::Unity.VisualScripting.IUnit.graph => base.graph;

		protected override void Definition()
		{
			branches = new global::System.Collections.Generic.Dictionary<global::Unity.VisualScripting.ControlInput, global::Unity.VisualScripting.ValueInput>();
			selection = ValueOutput<object>("selection");
			exit = ControlOutput("exit");
			for (int i = 0; i < branchCount; i++)
			{
				global::Unity.VisualScripting.ValueInput branchValue = ValueInput<object>("value_" + i);
				global::Unity.VisualScripting.ControlInput controlInput = ControlInput("enter_" + i, (global::Unity.VisualScripting.Flow flow) => Select(flow, branchValue));
				Requirement(branchValue, controlInput);
				Assignment(controlInput, selection);
				Succession(controlInput, exit);
				branches.Add(controlInput, branchValue);
			}
		}

		public global::Unity.VisualScripting.ControlOutput Select(global::Unity.VisualScripting.Flow flow, global::Unity.VisualScripting.ValueInput branchValue)
		{
			flow.SetValue(selection, flow.GetValue(branchValue));
			return exit;
		}
	}
}
