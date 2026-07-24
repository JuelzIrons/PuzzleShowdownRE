namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Collections/Dictionaries")]
	[global::Unity.VisualScripting.UnitSurtitle("Dictionary")]
	[global::Unity.VisualScripting.UnitShortTitle("Add Item")]
	[global::Unity.VisualScripting.UnitOrder(2)]
	public sealed class AddDictionaryItem : global::Unity.VisualScripting.Unit
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
		public global::Unity.VisualScripting.ValueInput value { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ControlOutput exit { get; private set; }

		protected override void Definition()
		{
			enter = ControlInput("enter", Add);
			dictionaryInput = ValueInput<global::System.Collections.IDictionary>("dictionaryInput");
			key = ValueInput<object>("key");
			value = ValueInput<object>("value");
			dictionaryOutput = ValueOutput<global::System.Collections.IDictionary>("dictionaryOutput");
			exit = ControlOutput("exit");
			Requirement(dictionaryInput, enter);
			Requirement(key, enter);
			Requirement(value, enter);
			Assignment(enter, dictionaryOutput);
			Succession(enter, exit);
		}

		private global::Unity.VisualScripting.ControlOutput Add(global::Unity.VisualScripting.Flow flow)
		{
			global::System.Collections.IDictionary dictionary = flow.GetValue<global::System.Collections.IDictionary>(dictionaryInput);
			object obj = flow.GetValue<object>(key);
			object obj2 = flow.GetValue<object>(value);
			flow.SetValue(dictionaryOutput, dictionary);
			dictionary.Add(obj, obj2);
			return exit;
		}
	}
}
