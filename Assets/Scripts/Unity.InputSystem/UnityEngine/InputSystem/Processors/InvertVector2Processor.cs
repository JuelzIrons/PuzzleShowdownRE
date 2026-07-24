namespace UnityEngine.InputSystem.Processors
{
	public class InvertVector2Processor : global::UnityEngine.InputSystem.InputProcessor<global::UnityEngine.Vector2>
	{
		public bool invertX = true;

		public bool invertY = true;

		public override global::UnityEngine.Vector2 Process(global::UnityEngine.Vector2 value, global::UnityEngine.InputSystem.InputControl control)
		{
			if (invertX)
			{
				value.x *= -1f;
			}
			if (invertY)
			{
				value.y *= -1f;
			}
			return value;
		}

		public override string ToString()
		{
			return $"InvertVector2(invertX={invertX},invertY={invertY})";
		}
	}
}
