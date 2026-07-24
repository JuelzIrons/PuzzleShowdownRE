namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Collections")]
	public sealed class LastItem : global::Unity.VisualScripting.Unit
	{
		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ValueInput collection { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ValueOutput lastItem { get; private set; }

		protected override void Definition()
		{
			collection = ValueInput<global::System.Collections.IEnumerable>("collection");
			lastItem = ValueOutput("lastItem", First);
			Requirement(collection, lastItem);
		}

		public object First(global::Unity.VisualScripting.Flow flow)
		{
			global::System.Collections.IEnumerable value = flow.GetValue<global::System.Collections.IEnumerable>(collection);
			if (value is global::System.Collections.IList)
			{
				global::System.Collections.IList obj = (global::System.Collections.IList)value;
				return obj[obj.Count - 1];
			}
			return global::System.Linq.Enumerable.Last(global::System.Linq.Enumerable.Cast<object>(value));
		}
	}
}
