namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Collections")]
	public sealed class FirstItem : global::Unity.VisualScripting.Unit
	{
		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ValueInput collection { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ValueOutput firstItem { get; private set; }

		protected override void Definition()
		{
			collection = ValueInput<global::System.Collections.IEnumerable>("collection");
			firstItem = ValueOutput("firstItem", First);
			Requirement(collection, firstItem);
		}

		public object First(global::Unity.VisualScripting.Flow flow)
		{
			global::System.Collections.IEnumerable value = flow.GetValue<global::System.Collections.IEnumerable>(collection);
			if (value is global::System.Collections.IList)
			{
				return ((global::System.Collections.IList)value)[0];
			}
			return global::System.Linq.Enumerable.First(global::System.Linq.Enumerable.Cast<object>(value));
		}
	}
}
