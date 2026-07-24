namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Collections/Lists")]
	[global::Unity.VisualScripting.UnitSurtitle("List")]
	[global::Unity.VisualScripting.UnitShortTitle("Set Item")]
	[global::Unity.VisualScripting.UnitOrder(1)]
	[global::Unity.VisualScripting.TypeIcon(typeof(global::System.Collections.IList))]
	public sealed class SetListItem : global::Unity.VisualScripting.Unit
	{
		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ControlInput enter { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ValueInput list { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ValueInput index { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ValueInput item { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ControlOutput exit { get; private set; }

		protected override void Definition()
		{
			enter = ControlInput("enter", Set);
			list = ValueInput<global::System.Collections.IList>("list");
			index = ValueInput("index", 0);
			item = ValueInput<object>("item");
			exit = ControlOutput("exit");
			Requirement(list, enter);
			Requirement(index, enter);
			Requirement(item, enter);
			Succession(enter, exit);
		}

		public global::Unity.VisualScripting.ControlOutput Set(global::Unity.VisualScripting.Flow flow)
		{
			global::System.Collections.IList value = flow.GetValue<global::System.Collections.IList>(list);
			int value2 = flow.GetValue<int>(index);
			object value3 = flow.GetValue<object>(item);
			value[value2] = value3;
			return exit;
		}
	}
}
