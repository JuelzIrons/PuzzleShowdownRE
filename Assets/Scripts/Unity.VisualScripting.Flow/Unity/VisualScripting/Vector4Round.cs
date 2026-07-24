namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Math/Vector 4")]
	[global::Unity.VisualScripting.UnitTitle("Round")]
	public sealed class Vector4Round : global::Unity.VisualScripting.Round<global::UnityEngine.Vector4, global::UnityEngine.Vector4>
	{
		protected override global::UnityEngine.Vector4 Floor(global::UnityEngine.Vector4 input)
		{
			return new global::UnityEngine.Vector4(global::UnityEngine.Mathf.Floor(input.x), global::UnityEngine.Mathf.Floor(input.y), global::UnityEngine.Mathf.Floor(input.z), global::UnityEngine.Mathf.Floor(input.w));
		}

		protected override global::UnityEngine.Vector4 AwayFromZero(global::UnityEngine.Vector4 input)
		{
			return new global::UnityEngine.Vector4(global::UnityEngine.Mathf.Round(input.x), global::UnityEngine.Mathf.Round(input.y), global::UnityEngine.Mathf.Round(input.z), global::UnityEngine.Mathf.Round(input.w));
		}

		protected override global::UnityEngine.Vector4 Ceiling(global::UnityEngine.Vector4 input)
		{
			return new global::UnityEngine.Vector4(global::UnityEngine.Mathf.Ceil(input.x), global::UnityEngine.Mathf.Ceil(input.y), global::UnityEngine.Mathf.Ceil(input.z), global::UnityEngine.Mathf.Ceil(input.w));
		}
	}
}
