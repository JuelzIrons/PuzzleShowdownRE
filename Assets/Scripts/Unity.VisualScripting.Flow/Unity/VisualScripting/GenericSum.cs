namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Math/Generic")]
	[global::Unity.VisualScripting.UnitTitle("Add")]
	public sealed class GenericSum : global::Unity.VisualScripting.Sum<object>
	{
		public override object Operation(object a, object b)
		{
			return global::Unity.VisualScripting.OperatorUtility.Add(a, b);
		}

		public override object Operation(global::System.Collections.Generic.IEnumerable<object> values)
		{
			global::System.Collections.Generic.List<object> list = global::System.Linq.Enumerable.ToList(values);
			object obj = global::Unity.VisualScripting.OperatorUtility.Add(list[0], list[1]);
			for (int i = 2; i < list.Count; i++)
			{
				obj = global::Unity.VisualScripting.OperatorUtility.Add(obj, list[i]);
			}
			return obj;
		}
	}
}
