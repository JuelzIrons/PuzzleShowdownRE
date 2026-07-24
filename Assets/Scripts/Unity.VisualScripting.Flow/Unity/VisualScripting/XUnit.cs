namespace Unity.VisualScripting
{
	public static class XUnit
	{
		public static global::Unity.VisualScripting.ValueInput CompatibleValueInput(this global::Unity.VisualScripting.IUnit unit, global::System.Type outputType)
		{
			global::Unity.VisualScripting.Ensure.That("outputType").IsNotNull(outputType);
			return global::System.Linq.Enumerable.FirstOrDefault(global::System.Linq.Enumerable.OrderBy(global::System.Linq.Enumerable.Where(unit.valueInputs, (global::Unity.VisualScripting.ValueInput valueInput) => global::Unity.VisualScripting.ConversionUtility.CanConvert(outputType, valueInput.type, guaranteed: false)), delegate(global::Unity.VisualScripting.ValueInput valueInput)
			{
				bool flag = outputType == valueInput.type;
				bool flag2 = !valueInput.hasValidConnection;
				if (flag2 && flag)
				{
					return 1;
				}
				if (flag2)
				{
					return 2;
				}
				return flag ? 3 : 4;
			}));
		}

		public static global::Unity.VisualScripting.ValueOutput CompatibleValueOutput(this global::Unity.VisualScripting.IUnit unit, global::System.Type inputType)
		{
			global::Unity.VisualScripting.Ensure.That("inputType").IsNotNull(inputType);
			return global::System.Linq.Enumerable.FirstOrDefault(global::System.Linq.Enumerable.OrderBy(global::System.Linq.Enumerable.Where(unit.valueOutputs, (global::Unity.VisualScripting.ValueOutput valueOutput) => global::Unity.VisualScripting.ConversionUtility.CanConvert(valueOutput.type, inputType, guaranteed: false)), delegate(global::Unity.VisualScripting.ValueOutput valueOutput)
			{
				bool flag = inputType == valueOutput.type;
				bool flag2 = !valueOutput.hasValidConnection;
				if (flag2 && flag)
				{
					return 1;
				}
				if (flag2)
				{
					return 2;
				}
				return flag ? 3 : 4;
			}));
		}
	}
}
