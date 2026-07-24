namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Math/Vector 2")]
	[global::Unity.VisualScripting.UnitTitle("Round")]
	public sealed class Vector2Round : global::Unity.VisualScripting.Round<global::UnityEngine.Vector2, global::UnityEngine.Vector2>
	{
		protected override global::UnityEngine.Vector2 Floor(global::UnityEngine.Vector2 input)
		{
			return new global::UnityEngine.Vector2(global::UnityEngine.Mathf.Floor(input.x), global::UnityEngine.Mathf.Floor(input.y));
		}

		protected override global::UnityEngine.Vector2 AwayFromZero(global::UnityEngine.Vector2 input)
		{
			return new global::UnityEngine.Vector2(global::UnityEngine.Mathf.Round(input.x), global::UnityEngine.Mathf.Round(input.y));
		}

		protected override global::UnityEngine.Vector2 Ceiling(global::UnityEngine.Vector2 input)
		{
			return new global::UnityEngine.Vector2(global::UnityEngine.Mathf.Ceil(input.x), global::UnityEngine.Mathf.Ceil(input.y));
		}
	}
}
