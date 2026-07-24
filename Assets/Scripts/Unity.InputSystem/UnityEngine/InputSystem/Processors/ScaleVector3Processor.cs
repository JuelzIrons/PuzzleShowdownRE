namespace UnityEngine.InputSystem.Processors
{
	public class ScaleVector3Processor : global::UnityEngine.InputSystem.InputProcessor<global::UnityEngine.Vector3>
	{
		[global::UnityEngine.Tooltip("Scale factor to multiply the incoming Vector3's X component by.")]
		public float x = 1f;

		[global::UnityEngine.Tooltip("Scale factor to multiply the incoming Vector3's Y component by.")]
		public float y = 1f;

		[global::UnityEngine.Tooltip("Scale factor to multiply the incoming Vector3's Z component by.")]
		public float z = 1f;

		public override global::UnityEngine.Vector3 Process(global::UnityEngine.Vector3 value, global::UnityEngine.InputSystem.InputControl control)
		{
			return new global::UnityEngine.Vector3(value.x * x, value.y * y, value.z * z);
		}

		public override string ToString()
		{
			return $"ScaleVector3(x={x},y={y},z={z})";
		}
	}
}
