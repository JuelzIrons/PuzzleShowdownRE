namespace UnityEngine.InputSystem.Processors
{
	public class ClampProcessor : global::UnityEngine.InputSystem.InputProcessor<float>
	{
		public float min;

		public float max;

		public override float Process(float value, global::UnityEngine.InputSystem.InputControl control)
		{
			return global::UnityEngine.Mathf.Clamp(value, min, max);
		}

		public override string ToString()
		{
			return $"Clamp(min={min},max={max})";
		}
	}
}
