namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Math/Vector 2")]
	[global::Unity.VisualScripting.UnitTitle("Absolute")]
	public sealed class Vector2Absolute : global::Unity.VisualScripting.Absolute<global::UnityEngine.Vector2>
	{
		protected override global::UnityEngine.Vector2 Operation(global::UnityEngine.Vector2 input)
		{
			return new global::UnityEngine.Vector2(global::UnityEngine.Mathf.Abs(input.x), global::UnityEngine.Mathf.Abs(input.y));
		}
	}
}
