namespace UnityEngine.InputSystem.Processors
{
	public class ScaleVector2Processor : global::UnityEngine.InputSystem.InputProcessor<global::UnityEngine.Vector2>
	{
		[global::UnityEngine.Tooltip("Scale factor to multiply the incoming Vector2's X component by.")]
		public float x = 1f;

		[global::UnityEngine.Tooltip("Scale factor to multiply the incoming Vector2's Y component by.")]
		public float y = 1f;

		public override global::UnityEngine.Vector2 Process(global::UnityEngine.Vector2 value, global::UnityEngine.InputSystem.InputControl control)
		{
			return new global::UnityEngine.Vector2(value.x * x, value.y * y);
		}

		public override string ToString()
		{
			return $"ScaleVector2(x={x},y={y})";
		}
	}
}
