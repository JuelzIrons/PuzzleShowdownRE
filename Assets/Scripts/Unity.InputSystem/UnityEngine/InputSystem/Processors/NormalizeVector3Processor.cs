namespace UnityEngine.InputSystem.Processors
{
	public class NormalizeVector3Processor : global::UnityEngine.InputSystem.InputProcessor<global::UnityEngine.Vector3>
	{
		public override global::UnityEngine.Vector3 Process(global::UnityEngine.Vector3 value, global::UnityEngine.InputSystem.InputControl control)
		{
			return value.normalized;
		}

		public override string ToString()
		{
			return "NormalizeVector3()";
		}
	}
}
