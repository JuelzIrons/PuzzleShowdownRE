namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Math/Vector 2")]
	[global::Unity.VisualScripting.UnitTitle("Angle")]
	public sealed class Vector2Angle : global::Unity.VisualScripting.Angle<global::UnityEngine.Vector2>
	{
		public override float Operation(global::UnityEngine.Vector2 a, global::UnityEngine.Vector2 b)
		{
			return global::UnityEngine.Vector2.Angle(a, b);
		}
	}
}
