namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Control")]
	[global::Unity.VisualScripting.UnitTitle("Select")]
	[global::Unity.VisualScripting.TypeIcon(typeof(global::Unity.VisualScripting.ISelectUnit))]
	[global::Unity.VisualScripting.UnitOrder(6)]
	public sealed class SelectUnit : global::Unity.VisualScripting.Unit, global::Unity.VisualScripting.ISelectUnit, global::Unity.VisualScripting.IUnit, global::Unity.VisualScripting.IGraphElementWithDebugData, global::Unity.VisualScripting.IGraphElement, global::Unity.VisualScripting.IGraphItem, global::Unity.VisualScripting.INotifiedCollectionItem, global::System.IDisposable, global::Unity.VisualScripting.IPrewarmable, global::Unity.VisualScripting.IAotStubbable, global::Unity.VisualScripting.IIdentifiable, global::Unity.VisualScripting.IAnalyticsIdentifiable
	{
		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ValueInput condition { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabel("True")]
		public global::Unity.VisualScripting.ValueInput ifTrue { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabel("False")]
		public global::Unity.VisualScripting.ValueInput ifFalse { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ValueOutput selection { get; private set; }

		global::Unity.VisualScripting.FlowGraph global::Unity.VisualScripting.IUnit.graph => base.graph;

		protected override void Definition()
		{
			condition = ValueInput<bool>("condition");
			ifTrue = ValueInput<object>("ifTrue").AllowsNull();
			ifFalse = ValueInput<object>("ifFalse").AllowsNull();
			selection = ValueOutput("selection", Branch).Predictable();
			Requirement(condition, selection);
			Requirement(ifTrue, selection);
			Requirement(ifFalse, selection);
		}

		public object Branch(global::Unity.VisualScripting.Flow flow)
		{
			return flow.GetValue(flow.GetValue<bool>(condition) ? ifTrue : ifFalse);
		}
	}
	[global::Unity.VisualScripting.TypeIcon(typeof(global::Unity.VisualScripting.ISelectUnit))]
	public abstract class SelectUnit<T> : global::Unity.VisualScripting.Unit, global::Unity.VisualScripting.ISelectUnit, global::Unity.VisualScripting.IUnit, global::Unity.VisualScripting.IGraphElementWithDebugData, global::Unity.VisualScripting.IGraphElement, global::Unity.VisualScripting.IGraphItem, global::Unity.VisualScripting.INotifiedCollectionItem, global::System.IDisposable, global::Unity.VisualScripting.IPrewarmable, global::Unity.VisualScripting.IAotStubbable, global::Unity.VisualScripting.IIdentifiable, global::Unity.VisualScripting.IAnalyticsIdentifiable
	{
		[global::Unity.VisualScripting.DoNotSerialize]
		public global::System.Collections.Generic.List<global::System.Collections.Generic.KeyValuePair<T, global::Unity.VisualScripting.ValueInput>> branches { get; private set; }

		[global::Unity.VisualScripting.Inspectable]
		[global::Unity.VisualScripting.Serialize]
		public global::System.Collections.Generic.List<T> options { get; set; } = new global::System.Collections.Generic.List<T>();

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ValueInput selector { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ValueInput @default { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ValueOutput selection { get; private set; }

		public override bool canDefine => options != null;

		global::Unity.VisualScripting.FlowGraph global::Unity.VisualScripting.IUnit.graph => base.graph;

		protected override void Definition()
		{
			selection = ValueOutput("selection", Result).Predictable();
			selector = ValueInput<T>("selector");
			Requirement(selector, selection);
			branches = new global::System.Collections.Generic.List<global::System.Collections.Generic.KeyValuePair<T, global::Unity.VisualScripting.ValueInput>>();
			foreach (T option in options)
			{
				T val = option;
				string key = "%" + val;
				if (!base.valueInputs.Contains(key))
				{
					global::Unity.VisualScripting.ValueInput valueInput = ValueInput<object>(key).AllowsNull();
					branches.Add(new global::System.Collections.Generic.KeyValuePair<T, global::Unity.VisualScripting.ValueInput>(option, valueInput));
					Requirement(valueInput, selection);
				}
			}
			@default = ValueInput<object>("default");
			Requirement(@default, selection);
		}

		protected virtual bool Matches(T a, T b)
		{
			return object.Equals(a, b);
		}

		public object Result(global::Unity.VisualScripting.Flow flow)
		{
			T value = flow.GetValue<T>(selector);
			foreach (global::System.Collections.Generic.KeyValuePair<T, global::Unity.VisualScripting.ValueInput> branch in branches)
			{
				if (Matches(branch.Key, value))
				{
					return flow.GetValue(branch.Value);
				}
			}
			return flow.GetValue(@default);
		}
	}
}
