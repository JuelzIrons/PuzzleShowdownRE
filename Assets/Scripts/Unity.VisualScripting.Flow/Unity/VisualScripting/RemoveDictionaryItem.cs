namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Collections/Dictionaries")]
	[global::Unity.VisualScripting.UnitSurtitle("Dictionary")]
	[global::Unity.VisualScripting.UnitShortTitle("Remove Item")]
	[global::Unity.VisualScripting.UnitOrder(3)]
	public sealed class RemoveDictionaryItem : global::Unity.VisualScripting.Unit
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
		public global::Unity.VisualScripting.ValueInput key { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ControlOutput exit { get; private set; }

		protected override void Definition()
		{
			enter = ControlInput("enter", Remove);
			dictionaryInput = ValueInput<global::System.Collections.IDictionary>("dictionaryInput");
			dictionaryOutput = ValueOutput<global::System.Collections.IDictionary>("dictionaryOutput");
			key = ValueInput<object>("key");
			exit = ControlOutput("exit");
			Requirement(dictionaryInput, enter);
			Requirement(key, enter);
			Assignment(enter, dictionaryOutput);
			Succession(enter, exit);
		}

		public global::Unity.VisualScripting.ControlOutput Remove(global::Unity.VisualScripting.Flow flow)
		{
			global::System.Collections.IDictionary value = flow.GetValue<global::System.Collections.IDictionary>(dictionaryInput);
			object value2 = flow.GetValue<object>(key);
			flow.SetValue(dictionaryOutput, value);
			value.Remove(value2);
			return exit;
		}
	}
}
