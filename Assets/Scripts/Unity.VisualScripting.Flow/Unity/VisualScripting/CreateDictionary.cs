namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Collections/Dictionaries")]
	[global::Unity.VisualScripting.UnitOrder(-1)]
	[global::Unity.VisualScripting.TypeIcon(typeof(global::System.Collections.IDictionary))]
	[global::Unity.VisualScripting.RenamedFrom("Bolt.CreateDitionary")]
	public sealed class CreateDictionary : global::Unity.VisualScripting.Unit
	{
		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ValueOutput dictionary { get; private set; }

		protected override void Definition()
		{
			dictionary = ValueOutput("dictionary", Create);
		}

		public global::System.Collections.IDictionary Create(global::Unity.VisualScripting.Flow flow)
		{
			return new global::Unity.VisualScripting.AotDictionary();
		}
	}
}
