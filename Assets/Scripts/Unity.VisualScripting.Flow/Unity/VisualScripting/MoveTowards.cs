namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitOrder(502)]
	public abstract class MoveTowards<T> : global::Unity.VisualScripting.Unit
	{
		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ValueInput current { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ValueInput target { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ValueInput maxDelta { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ValueOutput result { get; private set; }

		[global::Unity.VisualScripting.Serialize]
		[global::Unity.VisualScripting.Inspectable]
		[global::Unity.VisualScripting.UnitHeaderInspectable("Per Second")]
		[global::Unity.VisualScripting.InspectorToggleLeft]
		public bool perSecond { get; set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		protected virtual T defaultCurrent => default(T);

		[global::Unity.VisualScripting.DoNotSerialize]
		protected virtual T defaultTarget => default(T);

		protected override void Definition()
		{
			current = ValueInput("current", defaultCurrent);
			target = ValueInput("target", defaultTarget);
			maxDelta = ValueInput("maxDelta", 0f);
			result = ValueOutput("result", Operation);
			Requirement(current, result);
			Requirement(target, result);
			Requirement(maxDelta, result);
		}

		private T Operation(global::Unity.VisualScripting.Flow flow)
		{
			return Operation(flow.GetValue<T>(current), flow.GetValue<T>(target), flow.GetValue<float>(maxDelta) * (perSecond ? global::UnityEngine.Time.deltaTime : 1f));
		}

		public abstract T Operation(T current, T target, float maxDelta);
	}
}
