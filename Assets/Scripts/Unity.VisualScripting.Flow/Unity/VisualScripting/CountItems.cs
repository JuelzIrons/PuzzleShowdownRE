namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Collections")]
	public sealed class CountItems : global::Unity.VisualScripting.Unit
	{
		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ValueInput collection { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ValueOutput count { get; private set; }

		protected override void Definition()
		{
			collection = ValueInput<global::System.Collections.IEnumerable>("collection");
			count = ValueOutput("count", Count);
			Requirement(collection, count);
		}

		public int Count(global::Unity.VisualScripting.Flow flow)
		{
			global::System.Collections.IEnumerable value = flow.GetValue<global::System.Collections.IEnumerable>(collection);
			if (value is global::System.Collections.ICollection)
			{
				return ((global::System.Collections.ICollection)value).Count;
			}
			return global::System.Linq.Enumerable.Count(global::System.Linq.Enumerable.Cast<object>(value));
		}
	}
}
