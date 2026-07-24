namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Collections/Lists")]
	[global::Unity.VisualScripting.UnitSurtitle("List")]
	[global::Unity.VisualScripting.UnitShortTitle("Get Item")]
	[global::Unity.VisualScripting.UnitOrder(0)]
	[global::Unity.VisualScripting.TypeIcon(typeof(global::System.Collections.IList))]
	public sealed class GetListItem : global::Unity.VisualScripting.Unit
	{
		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ValueInput list { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ValueInput index { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ValueOutput item { get; private set; }

		protected override void Definition()
		{
			list = ValueInput<global::System.Collections.IList>("list");
			index = ValueInput("index", 0);
			item = ValueOutput("item", Get);
			Requirement(list, item);
			Requirement(index, item);
		}

		public object Get(global::Unity.VisualScripting.Flow flow)
		{
			global::System.Collections.IList value = flow.GetValue<global::System.Collections.IList>(list);
			int value2 = flow.GetValue<int>(index);
			return value[value2];
		}
	}
}
