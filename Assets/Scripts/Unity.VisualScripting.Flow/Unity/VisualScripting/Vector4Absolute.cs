namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Math/Vector 4")]
	[global::Unity.VisualScripting.UnitTitle("Absolute")]
	public sealed class Vector4Absolute : global::Unity.VisualScripting.Absolute<global::UnityEngine.Vector4>
	{
		protected override global::UnityEngine.Vector4 Operation(global::UnityEngine.Vector4 input)
		{
			return new global::UnityEngine.Vector4(global::UnityEngine.Mathf.Abs(input.x), global::UnityEngine.Mathf.Abs(input.y), global::UnityEngine.Mathf.Abs(input.z), global::UnityEngine.Mathf.Abs(input.w));
		}
	}
}
