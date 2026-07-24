namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Collections/Lists")]
	[global::Unity.VisualScripting.UnitSurtitle("List")]
	[global::Unity.VisualScripting.UnitShortTitle("Remove Item At Index")]
	[global::Unity.VisualScripting.UnitOrder(5)]
	[global::Unity.VisualScripting.TypeIcon(typeof(global::Unity.VisualScripting.RemoveListItem))]
	public sealed class RemoveListItemAt : global::Unity.VisualScripting.Unit
	{
		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ControlInput enter { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ValueInput listInput { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ValueOutput listOutput { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ValueInput index { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ControlOutput exit { get; private set; }

		protected override void Definition()
		{
			enter = ControlInput("enter", RemoveAt);
			listInput = ValueInput<global::System.Collections.IList>("listInput");
			listOutput = ValueOutput<global::System.Collections.IList>("listOutput");
			index = ValueInput("index", 0);
			exit = ControlOutput("exit");
			Requirement(listInput, enter);
			Requirement(index, enter);
			Assignment(enter, listOutput);
			Succession(enter, exit);
		}

		public global::Unity.VisualScripting.ControlOutput RemoveAt(global::Unity.VisualScripting.Flow flow)
		{
			global::System.Collections.IList value = flow.GetValue<global::System.Collections.IList>(listInput);
			int value2 = flow.GetValue<int>(index);
			if (value is global::System.Array)
			{
				global::System.Collections.ArrayList arrayList = new global::System.Collections.ArrayList(value);
				arrayList.RemoveAt(value2);
				flow.SetValue(listOutput, arrayList.ToArray(value.GetType().GetElementType()));
			}
			else
			{
				value.RemoveAt(value2);
				flow.SetValue(listOutput, value);
			}
			return exit;
		}
	}
}
