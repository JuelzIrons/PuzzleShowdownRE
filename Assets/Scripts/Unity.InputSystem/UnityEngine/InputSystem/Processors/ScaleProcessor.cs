namespace UnityEngine.InputSystem.Processors
{
	public class ScaleProcessor : global::UnityEngine.InputSystem.InputProcessor<float>
	{
		[global::UnityEngine.Tooltip("Scale factor to multiply incoming float values by.")]
		public float factor = 1f;

		public override float Process(float value, global::UnityEngine.InputSystem.InputControl control)
		{
			return value * factor;
		}

		public override string ToString()
		{
			return $"Scale(factor={factor})";
		}
	}
}
