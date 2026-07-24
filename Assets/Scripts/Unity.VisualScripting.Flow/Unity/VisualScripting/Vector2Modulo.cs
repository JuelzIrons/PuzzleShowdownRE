namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Math/Vector 2")]
	[global::Unity.VisualScripting.UnitTitle("Modulo")]
	public sealed class Vector2Modulo : global::Unity.VisualScripting.Modulo<global::UnityEngine.Vector2>
	{
		protected override global::UnityEngine.Vector2 defaultDividend => global::UnityEngine.Vector2.zero;

		protected override global::UnityEngine.Vector2 defaultDivisor => global::UnityEngine.Vector2.zero;

		public override global::UnityEngine.Vector2 Operation(global::UnityEngine.Vector2 a, global::UnityEngine.Vector2 b)
		{
			return new global::UnityEngine.Vector2(a.x % b.x, a.y % b.y);
		}
	}
}
