namespace UnityEngine.InputSystem.Processors
{
	public class NormalizeVector2Processor : global::UnityEngine.InputSystem.InputProcessor<global::UnityEngine.Vector2>
	{
		public override global::UnityEngine.Vector2 Process(global::UnityEngine.Vector2 value, global::UnityEngine.InputSystem.InputControl control)
		{
			return value.normalized;
		}

		public override string ToString()
		{
			return "NormalizeVector2()";
		}
	}
}
