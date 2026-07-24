namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Collections/Lists")]
	[global::Unity.VisualScripting.UnitSurtitle("List")]
	[global::Unity.VisualScripting.UnitShortTitle("Add Item")]
	[global::Unity.VisualScripting.UnitOrder(2)]
	public sealed class AddListItem : global::Unity.VisualScripting.Unit
	{
		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ControlInput enter { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabel("List")]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ValueInput listInput { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabel("List")]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ValueOutput listOutput { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ValueInput item { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ControlOutput exit { get; private set; }

		protected override void Definition()
		{
			enter = ControlInput("enter", Add);
			listInput = ValueInput<global::System.Collections.IList>("listInput");
			item = ValueInput<object>("item");
			listOutput = ValueOutput<global::System.Collections.IList>("listOutput");
			exit = ControlOutput("exit");
			Requirement(listInput, enter);
			Requirement(item, enter);
			Assignment(enter, listOutput);
			Succession(enter, exit);
		}

		public global::Unity.VisualScripting.ControlOutput Add(global::Unity.VisualScripting.Flow flow)
		{
			global::System.Collections.IList value = flow.GetValue<global::System.Collections.IList>(listInput);
			object value2 = flow.GetValue<object>(item);
			if (value is global::System.Array)
			{
				global::System.Collections.ArrayList arrayList = new global::System.Collections.ArrayList(value);
				arrayList.Add(value2);
				flow.SetValue(listOutput, arrayList.ToArray(value.GetType().GetElementType()));
			}
			else
			{
				value.Add(value2);
				flow.SetValue(listOutput, value);
			}
			return exit;
		}
	}
}
