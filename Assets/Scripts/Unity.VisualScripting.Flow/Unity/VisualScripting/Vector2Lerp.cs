namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Math/Vector 2")]
	[global::Unity.VisualScripting.UnitTitle("Lerp")]
	public sealed class Vector2Lerp : global::Unity.VisualScripting.Lerp<global::UnityEngine.Vector2>
	{
		protected override global::UnityEngine.Vector2 defaultA => global::UnityEngine.Vector2.zero;

		protected override global::UnityEngine.Vector2 defaultB => global::UnityEngine.Vector2.one;

		public override global::UnityEngine.Vector2 Operation(global::UnityEngine.Vector2 a, global::UnityEngine.Vector2 b, float t)
		{
			return global::UnityEngine.Vector2.Lerp(a, b, t);
		}
	}
}
