namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Collections/Dictionaries")]
	[global::Unity.VisualScripting.UnitSurtitle("Dictionary")]
	[global::Unity.VisualScripting.UnitShortTitle("Get Item")]
	[global::Unity.VisualScripting.UnitOrder(0)]
	[global::Unity.VisualScripting.TypeIcon(typeof(global::System.Collections.IDictionary))]
	public sealed class GetDictionaryItem : global::Unity.VisualScripting.Unit
	{
		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ValueInput dictionary { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ValueInput key { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ValueOutput value { get; private set; }

		protected override void Definition()
		{
			dictionary = ValueInput<global::System.Collections.IDictionary>("dictionary");
			key = ValueInput<object>("key");
			value = ValueOutput("value", Get);
			Requirement(dictionary, value);
			Requirement(key, value);
		}

		private object Get(global::Unity.VisualScripting.Flow flow)
		{
			global::System.Collections.IDictionary obj = flow.GetValue<global::System.Collections.IDictionary>(dictionary);
			object obj2 = flow.GetValue<object>(key);
			return obj[obj2];
		}
	}
}
