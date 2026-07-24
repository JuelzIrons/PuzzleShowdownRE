namespace UnityEngine.InputSystem.Processors
{
	public class StickDeadzoneProcessor : global::UnityEngine.InputSystem.InputProcessor<global::UnityEngine.Vector2>
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

		public override global::UnityEngine.Vector2 Process(global::UnityEngine.Vector2 value, global::UnityEngine.InputSystem.InputControl control = null)
		{
			float magnitude = value.magnitude;
			float deadZoneAdjustedValue = GetDeadZoneAdjustedValue(magnitude);
			if (deadZoneAdjustedValue == 0f)
			{
				value = global::UnityEngine.Vector2.zero;
			}
			else
			{
				value *= deadZoneAdjustedValue / magnitude;
			}
			return value;
		}

		private float GetDeadZoneAdjustedValue(float value)
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
			return $"StickDeadzone(min={minOrDefault},max={maxOrDefault})";
		}
	}
}
