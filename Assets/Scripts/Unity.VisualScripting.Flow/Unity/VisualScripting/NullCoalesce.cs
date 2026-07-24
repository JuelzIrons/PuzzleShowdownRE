namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Nulls")]
	[global::Unity.VisualScripting.TypeIcon(typeof(global::Unity.VisualScripting.Null))]
	public sealed class NullCoalesce : global::Unity.VisualScripting.Unit
	{
		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ValueInput input { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ValueInput fallback { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ValueOutput result { get; private set; }

		protected override void Definition()
		{
			input = ValueInput<object>("input").AllowsNull();
			fallback = ValueInput<object>("fallback");
			result = ValueOutput("result", Coalesce).Predictable();
			Requirement(input, result);
			Requirement(fallback, result);
		}

		public object Coalesce(global::Unity.VisualScripting.Flow flow)
		{
			object value = flow.GetValue(input);
			if (!((!(value is global::UnityEngine.Object)) ? (value == null) : ((global::UnityEngine.Object)value == null)))
			{
				return value;
			}
			return flow.GetValue(fallback);
		}
	}
}
