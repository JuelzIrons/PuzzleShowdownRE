namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Collections/Lists")]
	[global::Unity.VisualScripting.UnitSurtitle("List")]
	[global::Unity.VisualScripting.UnitShortTitle("Remove Item")]
	[global::Unity.VisualScripting.UnitOrder(4)]
	public sealed class RemoveListItem : global::Unity.VisualScripting.Unit
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
			enter = ControlInput("enter", Remove);
			listInput = ValueInput<global::System.Collections.IList>("listInput");
			listOutput = ValueOutput<global::System.Collections.IList>("listOutput");
			item = ValueInput<object>("item");
			exit = ControlOutput("exit");
			Requirement(listInput, enter);
			Requirement(item, enter);
			Assignment(enter, listOutput);
			Succession(enter, exit);
		}

		public global::Unity.VisualScripting.ControlOutput Remove(global::Unity.VisualScripting.Flow flow)
		{
			global::System.Collections.IList value = flow.GetValue<global::System.Collections.IList>(listInput);
			object value2 = flow.GetValue<object>(item);
			if (value is global::System.Array)
			{
				global::System.Collections.ArrayList arrayList = new global::System.Collections.ArrayList(value);
				arrayList.Remove(value2);
				flow.SetValue(listOutput, arrayList.ToArray(value.GetType().GetElementType()));
			}
			else
			{
				value.Remove(value2);
				flow.SetValue(listOutput, value);
			}
			return exit;
		}
	}
}
