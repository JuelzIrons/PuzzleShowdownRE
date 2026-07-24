namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Collections/Lists")]
	[global::Unity.VisualScripting.UnitSurtitle("List")]
	[global::Unity.VisualScripting.UnitShortTitle("Clear")]
	[global::Unity.VisualScripting.UnitOrder(6)]
	[global::Unity.VisualScripting.TypeIcon(typeof(global::Unity.VisualScripting.RemoveListItem))]
	public sealed class ClearList : global::Unity.VisualScripting.Unit
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
		public global::Unity.VisualScripting.ControlOutput exit { get; private set; }

		protected override void Definition()
		{
			enter = ControlInput("enter", Clear);
			listInput = ValueInput<global::System.Collections.IList>("listInput");
			listOutput = ValueOutput<global::System.Collections.IList>("listOutput");
			exit = ControlOutput("exit");
			Requirement(listInput, enter);
			Assignment(enter, listOutput);
			Succession(enter, exit);
		}

		public global::Unity.VisualScripting.ControlOutput Clear(global::Unity.VisualScripting.Flow flow)
		{
			global::System.Collections.IList value = flow.GetValue<global::System.Collections.IList>(listInput);
			if (value is global::System.Array)
			{
				flow.SetValue(listOutput, global::System.Array.CreateInstance(value.GetType().GetElementType(), 0));
			}
			else
			{
				value.Clear();
				flow.SetValue(listOutput, value);
			}
			return exit;
		}
	}
}
