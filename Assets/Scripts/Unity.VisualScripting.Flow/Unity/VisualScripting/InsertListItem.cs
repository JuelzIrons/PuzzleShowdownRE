namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Collections/Lists")]
	[global::Unity.VisualScripting.UnitSurtitle("List")]
	[global::Unity.VisualScripting.UnitShortTitle("Insert Item")]
	[global::Unity.VisualScripting.UnitOrder(3)]
	[global::Unity.VisualScripting.TypeIcon(typeof(global::Unity.VisualScripting.AddListItem))]
	public sealed class InsertListItem : global::Unity.VisualScripting.Unit
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
		public global::Unity.VisualScripting.ValueInput index { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ValueInput item { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ControlOutput exit { get; private set; }

		protected override void Definition()
		{
			enter = ControlInput("enter", Insert);
			listInput = ValueInput<global::System.Collections.IList>("listInput");
			item = ValueInput<object>("item");
			index = ValueInput("index", 0);
			listOutput = ValueOutput<global::System.Collections.IList>("listOutput");
			exit = ControlOutput("exit");
			Requirement(listInput, enter);
			Requirement(item, enter);
			Requirement(index, enter);
			Assignment(enter, listOutput);
			Succession(enter, exit);
		}

		public global::Unity.VisualScripting.ControlOutput Insert(global::Unity.VisualScripting.Flow flow)
		{
			global::System.Collections.IList value = flow.GetValue<global::System.Collections.IList>(listInput);
			int value2 = flow.GetValue<int>(index);
			object value3 = flow.GetValue<object>(item);
			if (value is global::System.Array)
			{
				global::System.Collections.ArrayList arrayList = new global::System.Collections.ArrayList(value);
				arrayList.Insert(value2, value3);
				flow.SetValue(listOutput, arrayList.ToArray(value.GetType().GetElementType()));
			}
			else
			{
				value.Insert(value2, value3);
				flow.SetValue(listOutput, value);
			}
			return exit;
		}
	}
}
