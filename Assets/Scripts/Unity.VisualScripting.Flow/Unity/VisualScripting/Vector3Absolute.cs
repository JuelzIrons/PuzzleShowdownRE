namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Math/Vector 3")]
	[global::Unity.VisualScripting.UnitTitle("Absolute")]
	public sealed class Vector3Absolute : global::Unity.VisualScripting.Absolute<global::UnityEngine.Vector3>
	{
		protected override global::UnityEngine.Vector3 Operation(global::UnityEngine.Vector3 input)
		{
			return new global::UnityEngine.Vector3(global::UnityEngine.Mathf.Abs(input.x), global::UnityEngine.Mathf.Abs(input.y), global::UnityEngine.Mathf.Abs(input.z));
		}
	}
}
