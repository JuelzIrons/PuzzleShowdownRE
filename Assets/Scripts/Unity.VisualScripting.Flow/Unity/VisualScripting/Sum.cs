namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitOrder(303)]
	[global::Unity.VisualScripting.TypeIcon(typeof(global::Unity.VisualScripting.Add<>))]
	public abstract class Sum<T> : global::Unity.VisualScripting.MultiInputUnit<T>
	{
		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ValueOutput sum { get; private set; }

		protected override void Definition()
		{
			if (this is global::Unity.VisualScripting.IDefaultValue<T> defaultValue)
			{
				global::System.Collections.Generic.List<global::Unity.VisualScripting.ValueInput> list = new global::System.Collections.Generic.List<global::Unity.VisualScripting.ValueInput>();
				base.multiInputs = list.AsReadOnly();
				for (int i = 0; i < inputCount; i++)
				{
					if (i == 0)
					{
						list.Add(ValueInput<T>(i.ToString()));
					}
					else
					{
						list.Add(ValueInput(i.ToString(), defaultValue.defaultValue));
					}
				}
			}
			else
			{
				base.Definition();
			}
			sum = ValueOutput("sum", Operation).Predictable();
			foreach (global::Unity.VisualScripting.ValueInput multiInput in base.multiInputs)
			{
				Requirement(multiInput, sum);
			}
		}

		public abstract T Operation(T a, T b);

		public abstract T Operation(global::System.Collections.Generic.IEnumerable<T> values);

		public T Operation(global::Unity.VisualScripting.Flow flow)
		{
			if (inputCount == 2)
			{
				return Operation(flow.GetValue<T>(base.multiInputs[0]), flow.GetValue<T>(base.multiInputs[1]));
			}
			return Operation(global::System.Linq.Enumerable.Select(base.multiInputs, flow.GetValue<T>));
		}
	}
}
