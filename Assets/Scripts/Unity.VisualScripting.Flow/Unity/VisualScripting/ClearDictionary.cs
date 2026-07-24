namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Collections/Dictionaries")]
	[global::Unity.VisualScripting.UnitSurtitle("Dictionary")]
	[global::Unity.VisualScripting.UnitShortTitle("Clear")]
	[global::Unity.VisualScripting.UnitOrder(4)]
	[global::Unity.VisualScripting.TypeIcon(typeof(global::Unity.VisualScripting.RemoveDictionaryItem))]
	public sealed class ClearDictionary : global::Unity.VisualScripting.Unit
	{
		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ControlInput enter { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabel("Dictionary")]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ValueInput dictionaryInput { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabel("Dictionary")]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ValueOutput dictionaryOutput { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ControlOutput exit { get; private set; }

		protected override void Definition()
		{
			enter = ControlInput("enter", Clear);
			dictionaryInput = ValueInput<global::System.Collections.IDictionary>("dictionaryInput");
			dictionaryOutput = ValueOutput<global::System.Collections.IDictionary>("dictionaryOutput");
			exit = ControlOutput("exit");
			Requirement(dictionaryInput, enter);
			Assignment(enter, dictionaryOutput);
			Succession(enter, exit);
		}

		private global::Unity.VisualScripting.ControlOutput Clear(global::Unity.VisualScripting.Flow flow)
		{
			global::System.Collections.IDictionary value = flow.GetValue<global::System.Collections.IDictionary>(dictionaryInput);
			flow.SetValue(dictionaryOutput, value);
			value.Clear();
			return exit;
		}
	}
}
