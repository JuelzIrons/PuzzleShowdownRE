namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Math/Vector 3")]
	[global::Unity.VisualScripting.UnitTitle("Round")]
	public sealed class Vector3Round : global::Unity.VisualScripting.Round<global::UnityEngine.Vector3, global::UnityEngine.Vector3>
	{
		protected override global::UnityEngine.Vector3 Floor(global::UnityEngine.Vector3 input)
		{
			return new global::UnityEngine.Vector3(global::UnityEngine.Mathf.Floor(input.x), global::UnityEngine.Mathf.Floor(input.y), global::UnityEngine.Mathf.Floor(input.z));
		}

		protected override global::UnityEngine.Vector3 AwayFromZero(global::UnityEngine.Vector3 input)
		{
			return new global::UnityEngine.Vector3(global::UnityEngine.Mathf.Round(input.x), global::UnityEngine.Mathf.Round(input.y), global::UnityEngine.Mathf.Round(input.z));
		}

		protected override global::UnityEngine.Vector3 Ceiling(global::UnityEngine.Vector3 input)
		{
			return new global::UnityEngine.Vector3(global::UnityEngine.Mathf.Ceil(input.x), global::UnityEngine.Mathf.Ceil(input.y), global::UnityEngine.Mathf.Ceil(input.z));
		}
	}
}
