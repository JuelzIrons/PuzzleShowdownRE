namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Nulls")]
	[global::Unity.VisualScripting.TypeIcon(typeof(global::Unity.VisualScripting.Null))]
	public sealed class NullCheck : global::Unity.VisualScripting.Unit
	{
		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ValueInput input { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ControlInput enter { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabel("Not Null")]
		public global::Unity.VisualScripting.ControlOutput ifNotNull { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabel("Null")]
		public global::Unity.VisualScripting.ControlOutput ifNull { get; private set; }

		protected override void Definition()
		{
			enter = ControlInput("enter", Enter);
			input = ValueInput<object>("input").AllowsNull();
			ifNotNull = ControlOutput("ifNotNull");
			ifNull = ControlOutput("ifNull");
			Requirement(input, enter);
			Succession(enter, ifNotNull);
			Succession(enter, ifNull);
		}

		public global::Unity.VisualScripting.ControlOutput Enter(global::Unity.VisualScripting.Flow flow)
		{
			object value = flow.GetValue(input);
			if ((!(value is global::UnityEngine.Object)) ? (value == null) : ((global::UnityEngine.Object)value == null))
			{
				return ifNull;
			}
			return ifNotNull;
		}
	}
}
