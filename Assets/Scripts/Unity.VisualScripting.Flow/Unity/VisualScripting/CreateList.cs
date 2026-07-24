namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Collections/Lists")]
	[global::Unity.VisualScripting.UnitOrder(-1)]
	[global::Unity.VisualScripting.TypeIcon(typeof(global::System.Collections.IList))]
	public sealed class CreateList : global::Unity.VisualScripting.MultiInputUnit<object>
	{
		[global::Unity.VisualScripting.DoNotSerialize]
		protected override int minInputCount => 0;

		[global::Unity.VisualScripting.InspectorLabel("Elements")]
		[global::Unity.VisualScripting.UnitHeaderInspectable("Elements")]
		[global::Unity.VisualScripting.Inspectable]
		public override int inputCount
		{
			get
			{
				return base.inputCount;
			}
			set
			{
				base.inputCount = value;
			}
		}

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ValueOutput list { get; private set; }

		protected override void Definition()
		{
			list = ValueOutput("list", Create);
			base.Definition();
			foreach (global::Unity.VisualScripting.ValueInput multiInput in base.multiInputs)
			{
				Requirement(multiInput, list);
			}
			InputsAllowNull();
		}

		public global::System.Collections.IList Create(global::Unity.VisualScripting.Flow flow)
		{
			global::Unity.VisualScripting.AotList aotList = new global::Unity.VisualScripting.AotList();
			for (int i = 0; i < inputCount; i++)
			{
				aotList.Add(flow.GetValue<object>(base.multiInputs[i]));
			}
			return aotList;
		}
	}
}
