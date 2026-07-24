namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Collections/Dictionaries")]
	[global::Unity.VisualScripting.UnitOrder(5)]
	public sealed class MergeDictionaries : global::Unity.VisualScripting.MultiInputUnit<global::System.Collections.IDictionary>
	{
		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ValueOutput dictionary { get; private set; }

		protected override void Definition()
		{
			dictionary = ValueOutput("dictionary", Merge);
			base.Definition();
			foreach (global::Unity.VisualScripting.ValueInput multiInput in base.multiInputs)
			{
				Requirement(multiInput, dictionary);
			}
		}

		public global::System.Collections.IDictionary Merge(global::Unity.VisualScripting.Flow flow)
		{
			global::Unity.VisualScripting.AotDictionary aotDictionary = new global::Unity.VisualScripting.AotDictionary();
			for (int i = 0; i < inputCount; i++)
			{
				global::System.Collections.IDictionaryEnumerator enumerator = flow.GetValue<global::System.Collections.IDictionary>(base.multiInputs[i]).GetEnumerator();
				while (enumerator.MoveNext())
				{
					if (!aotDictionary.Contains(enumerator.Key))
					{
						aotDictionary.Add(enumerator.Key, enumerator.Value);
					}
				}
			}
			return aotDictionary;
		}
	}
}
