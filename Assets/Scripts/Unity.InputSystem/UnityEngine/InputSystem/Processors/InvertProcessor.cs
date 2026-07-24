namespace UnityEngine.InputSystem.Processors
{
	public class InvertProcessor : global::UnityEngine.InputSystem.InputProcessor<float>
	{
		public override float Process(float value, global::UnityEngine.InputSystem.InputControl control)
		{
			return value * -1f;
		}

		public override string ToString()
		{
			return "Invert()";
		}
	}
}
