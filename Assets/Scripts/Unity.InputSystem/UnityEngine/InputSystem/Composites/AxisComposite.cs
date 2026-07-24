namespace UnityEngine.InputSystem.Composites
{
	[global::UnityEngine.InputSystem.Utilities.DisplayStringFormat("{negative}/{positive}")]
	[global::System.ComponentModel.DisplayName("Positive/Negative Binding")]
	public class AxisComposite : global::UnityEngine.InputSystem.InputBindingComposite<float>
	{
		public enum WhichSideWins
		{
			Neither = 0,
			Positive = 1,
			Negative = 2
		}

		[global::UnityEngine.InputSystem.Layouts.InputControl(layout = "Axis")]
		public int negative;

		[global::UnityEngine.InputSystem.Layouts.InputControl(layout = "Axis")]
		public int positive;

		[global::UnityEngine.Tooltip("Value to return when the negative side is fully actuated.")]
		public float minValue = -1f;

		[global::UnityEngine.Tooltip("Value to return when the positive side is fully actuated.")]
		public float maxValue = 1f;

		[global::UnityEngine.Tooltip("If both the positive and negative side are actuated, decides what value to return. 'Neither' (default) means that the resulting value is the midpoint between min and max. 'Positive' means that max will be returned. 'Negative' means that min will be returned.")]
		public global::UnityEngine.InputSystem.Composites.AxisComposite.WhichSideWins whichSideWins;

		public float midPoint => (maxValue + minValue) / 2f;

		public override float ReadValue(ref global::UnityEngine.InputSystem.InputBindingCompositeContext context)
		{
			float num = global::UnityEngine.Mathf.Abs(context.ReadValue<float>(negative));
			float num2 = global::UnityEngine.Mathf.Abs(context.ReadValue<float>(positive));
			bool flag = num > global::UnityEngine.Mathf.Epsilon;
			bool flag2 = num2 > global::UnityEngine.Mathf.Epsilon;
			if (flag == flag2)
			{
				switch (whichSideWins)
				{
				case global::UnityEngine.InputSystem.Composites.AxisComposite.WhichSideWins.Negative:
					flag2 = false;
					break;
				case global::UnityEngine.InputSystem.Composites.AxisComposite.WhichSideWins.Positive:
					flag = false;
					break;
				case global::UnityEngine.InputSystem.Composites.AxisComposite.WhichSideWins.Neither:
					return midPoint;
				}
			}
			float num3 = midPoint;
			if (flag)
			{
				return num3 - (num3 - minValue) * num;
			}
			return num3 + (maxValue - num3) * num2;
		}

		public override float EvaluateMagnitude(ref global::UnityEngine.InputSystem.InputBindingCompositeContext context)
		{
			float num = ReadValue(ref context);
			if (num < midPoint)
			{
				num = global::UnityEngine.Mathf.Abs(num - midPoint);
				return global::UnityEngine.InputSystem.Processors.NormalizeProcessor.Normalize(num, 0f, global::UnityEngine.Mathf.Abs(minValue), 0f);
			}
			num = global::UnityEngine.Mathf.Abs(num - midPoint);
			return global::UnityEngine.InputSystem.Processors.NormalizeProcessor.Normalize(num, 0f, global::UnityEngine.Mathf.Abs(maxValue), 0f);
		}
	}
}
