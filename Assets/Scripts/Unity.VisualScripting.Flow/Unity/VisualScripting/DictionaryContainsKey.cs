namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Collections/Dictionaries")]
	[global::Unity.VisualScripting.UnitSurtitle("Dictionary")]
	[global::Unity.VisualScripting.UnitShortTitle("Contains Key")]
	[global::Unity.VisualScripting.TypeIcon(typeof(global::System.Collections.IDictionary))]
	public sealed class DictionaryContainsKey : global::Unity.VisualScripting.Unit
	{
		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ValueInput dictionary { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ValueInput key { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ValueOutput contains { get; private set; }

		protected override void Definition()
		{
			dictionary = ValueInput<global::System.Collections.IDictionary>("dictionary");
			key = ValueInput<object>("key");
			contains = ValueOutput("contains", Contains);
			Requirement(dictionary, contains);
			Requirement(key, contains);
		}

		private bool Contains(global::Unity.VisualScripting.Flow flow)
		{
			global::System.Collections.IDictionary value = flow.GetValue<global::System.Collections.IDictionary>(dictionary);
			object value2 = flow.GetValue<object>(key);
			return value.Contains(value2);
		}
	}
}
