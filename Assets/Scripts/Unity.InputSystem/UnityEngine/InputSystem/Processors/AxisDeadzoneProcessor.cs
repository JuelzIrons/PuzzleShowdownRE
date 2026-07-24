namespace UnityEngine.InputSystem.Processors
{
	public class AxisDeadzoneProcessor : global::UnityEngine.InputSystem.InputProcessor<float>
	{
		public float min;

		public float max;

		private float minOrDefault
		{
			get
			{
				if (min != 0f)
				{
					return min;
				}
				return global::UnityEngine.InputSystem.InputSystem.settings.defaultDeadzoneMin;
			}
		}

		private float maxOrDefault
		{
			get
			{
				if (max != 0f)
				{
					return max;
				}
				return global::UnityEngine.InputSystem.InputSystem.settings.defaultDeadzoneMax;
			}
		}

		public override float Process(float value, global::UnityEngine.InputSystem.InputControl control = null)
		{
			float num = minOrDefault;
			float num2 = maxOrDefault;
			float num3 = global::UnityEngine.Mathf.Abs(value);
			if (num3 < num)
			{
				return 0f;
			}
			if (num3 > num2)
			{
				return global::UnityEngine.Mathf.Sign(value);
			}
			return global::UnityEngine.Mathf.Sign(value) * ((num3 - num) / (num2 - num));
		}

		public override string ToString()
		{
			return $"AxisDeadzone(min={minOrDefault},max={maxOrDefault})";
		}
	}
}
