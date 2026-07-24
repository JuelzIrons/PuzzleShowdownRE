namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Collections/Lists")]
	[global::Unity.VisualScripting.UnitSurtitle("List")]
	[global::Unity.VisualScripting.UnitShortTitle("Contains Item")]
	[global::Unity.VisualScripting.TypeIcon(typeof(global::System.Collections.IList))]
	public sealed class ListContainsItem : global::Unity.VisualScripting.Unit
	{
		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ValueInput list { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ValueInput item { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ValueOutput contains { get; private set; }

		protected override void Definition()
		{
			list = ValueInput<global::System.Collections.IList>("list");
			item = ValueInput<object>("item");
			contains = ValueOutput("contains", Contains);
			Requirement(list, contains);
			Requirement(item, contains);
		}

		public bool Contains(global::Unity.VisualScripting.Flow flow)
		{
			global::System.Collections.IList value = flow.GetValue<global::System.Collections.IList>(list);
			object value2 = flow.GetValue<object>(item);
			return value.Contains(value2);
		}
	}
}
