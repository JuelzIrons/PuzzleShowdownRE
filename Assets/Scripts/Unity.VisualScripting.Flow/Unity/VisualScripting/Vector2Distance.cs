namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Math/Vector 2")]
	[global::Unity.VisualScripting.UnitTitle("Distance")]
	public sealed class Vector2Distance : global::Unity.VisualScripting.Distance<global::UnityEngine.Vector2>
	{
		public override float Operation(global::UnityEngine.Vector2 a, global::UnityEngine.Vector2 b)
		{
			return global::UnityEngine.Vector2.Distance(a, b);
		}
	}
}
