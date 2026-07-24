namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Math/Vector 2")]
	[global::Unity.VisualScripting.UnitTitle("Project")]
	public sealed class Vector2Project : global::Unity.VisualScripting.Project<global::UnityEngine.Vector2>
	{
		public override global::UnityEngine.Vector2 Operation(global::UnityEngine.Vector2 a, global::UnityEngine.Vector2 b)
		{
			return global::UnityEngine.Vector2.Dot(a, b) * b.normalized;
		}
	}
}
