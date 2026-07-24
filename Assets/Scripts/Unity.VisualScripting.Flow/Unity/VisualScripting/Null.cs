namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Nulls")]
	public sealed class Null : global::Unity.VisualScripting.Unit
	{
		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ValueOutput @null { get; private set; }

		protected override void Definition()
		{
			@null = ValueOutput("null", (global::Unity.VisualScripting.Flow recursion) => (object)null).Predictable();
		}
	}
}
