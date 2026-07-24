namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.TypeIcon(typeof(global::Unity.VisualScripting.IBranchUnit))]
	public abstract class SwitchUnit<T> : global::Unity.VisualScripting.Unit, global::Unity.VisualScripting.IBranchUnit, global::Unity.VisualScripting.IUnit, global::Unity.VisualScripting.IGraphElementWithDebugData, global::Unity.VisualScripting.IGraphElement, global::Unity.VisualScripting.IGraphItem, global::Unity.VisualScripting.INotifiedCollectionItem, global::System.IDisposable, global::Unity.VisualScripting.IPrewarmable, global::Unity.VisualScripting.IAotStubbable, global::Unity.VisualScripting.IIdentifiable, global::Unity.VisualScripting.IAnalyticsIdentifiable
	{
		[global::Unity.VisualScripting.DoNotSerialize]
		public global::System.Collections.Generic.List<global::System.Collections.Generic.KeyValuePair<T, global::Unity.VisualScripting.ControlOutput>> branches { get; private set; }

		[global::Unity.VisualScripting.Inspectable]
		[global::Unity.VisualScripting.Serialize]
		public global::System.Collections.Generic.List<T> options { get; set; } = new global::System.Collections.Generic.List<T>();

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ControlInput enter { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ValueInput selector { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ControlOutput @default { get; private set; }

		public override bool canDefine => options != null;

		global::Unity.VisualScripting.FlowGraph global::Unity.VisualScripting.IUnit.graph => base.graph;

		protected override void Definition()
		{
			enter = ControlInput("enter", Enter);
			selector = ValueInput<T>("selector");
			Requirement(selector, enter);
			branches = new global::System.Collections.Generic.List<global::System.Collections.Generic.KeyValuePair<T, global::Unity.VisualScripting.ControlOutput>>();
			foreach (T option in options)
			{
				T val = option;
				string key = "%" + val;
				if (!base.controlOutputs.Contains(key))
				{
					global::Unity.VisualScripting.ControlOutput controlOutput = ControlOutput(key);
					branches.Add(new global::System.Collections.Generic.KeyValuePair<T, global::Unity.VisualScripting.ControlOutput>(option, controlOutput));
					Succession(enter, controlOutput);
				}
			}
			@default = ControlOutput("default");
			Succession(enter, @default);
		}

		protected virtual bool Matches(T a, T b)
		{
			return object.Equals(a, b);
		}

		public global::Unity.VisualScripting.ControlOutput Enter(global::Unity.VisualScripting.Flow flow)
		{
			T value = flow.GetValue<T>(selector);
			foreach (global::System.Collections.Generic.KeyValuePair<T, global::Unity.VisualScripting.ControlOutput> branch in branches)
			{
				if (Matches(branch.Key, value))
				{
					return branch.Value;
				}
			}
			return @default;
		}
	}
}
