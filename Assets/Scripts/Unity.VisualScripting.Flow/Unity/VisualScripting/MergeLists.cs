namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Collections/Lists")]
	[global::Unity.VisualScripting.UnitOrder(7)]
	public sealed class MergeLists : global::Unity.VisualScripting.MultiInputUnit<global::System.Collections.IEnumerable>
	{
		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ValueOutput list { get; private set; }

		protected override void Definition()
		{
			list = ValueOutput("list", Merge);
			base.Definition();
			foreach (global::Unity.VisualScripting.ValueInput multiInput in base.multiInputs)
			{
				Requirement(multiInput, list);
			}
		}

		public global::System.Collections.IList Merge(global::Unity.VisualScripting.Flow flow)
		{
			global::Unity.VisualScripting.AotList result = new global::Unity.VisualScripting.AotList();
			for (int i = 0; i < inputCount; i++)
			{
				result.AddRange(flow.GetValue<global::System.Collections.IEnumerable>(base.multiInputs[i]));
			}
			return result;
		}
	}
}
