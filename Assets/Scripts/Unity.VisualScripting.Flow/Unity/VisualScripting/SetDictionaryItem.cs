namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Collections/Dictionaries")]
	[global::Unity.VisualScripting.UnitSurtitle("Dictionary")]
	[global::Unity.VisualScripting.UnitShortTitle("Set Item")]
	[global::Unity.VisualScripting.UnitOrder(1)]
	[global::Unity.VisualScripting.TypeIcon(typeof(global::System.Collections.IDictionary))]
	public sealed class SetDictionaryItem : global::Unity.VisualScripting.Unit
	{
		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ControlInput enter { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ValueInput dictionary { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ValueInput key { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ValueInput value { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ControlOutput exit { get; private set; }

		protected override void Definition()
		{
			enter = ControlInput("enter", Set);
			dictionary = ValueInput<global::System.Collections.IDictionary>("dictionary");
			key = ValueInput<object>("key");
			value = ValueInput<object>("value");
			exit = ControlOutput("exit");
			Requirement(dictionary, enter);
			Requirement(key, enter);
			Requirement(value, enter);
			Succession(enter, exit);
		}

		public global::Unity.VisualScripting.ControlOutput Set(global::Unity.VisualScripting.Flow flow)
		{
			global::System.Collections.IDictionary obj = flow.GetValue<global::System.Collections.IDictionary>(dictionary);
			object obj2 = flow.GetValue<object>(key);
			object obj3 = flow.GetValue<object>(value);
			obj[obj2] = obj3;
			return exit;
		}
	}
}
