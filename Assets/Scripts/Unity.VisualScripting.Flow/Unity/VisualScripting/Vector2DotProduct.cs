namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Math/Vector 2")]
	[global::Unity.VisualScripting.UnitTitle("Dot Product")]
	public sealed class Vector2DotProduct : global::Unity.VisualScripting.DotProduct<global::UnityEngine.Vector2>
	{
		public override float Operation(global::UnityEngine.Vector2 a, global::UnityEngine.Vector2 b)
		{
			return global::UnityEngine.Vector2.Dot(a, b);
		}
	}
}
