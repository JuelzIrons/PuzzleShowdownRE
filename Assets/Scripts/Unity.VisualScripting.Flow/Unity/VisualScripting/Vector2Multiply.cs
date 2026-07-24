namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Math/Vector 2")]
	[global::Unity.VisualScripting.UnitTitle("Multiply")]
	public sealed class Vector2Multiply : global::Unity.VisualScripting.Multiply<global::UnityEngine.Vector2>
	{
		protected override global::UnityEngine.Vector2 defaultB => global::UnityEngine.Vector2.zero;

		public override global::UnityEngine.Vector2 Operation(global::UnityEngine.Vector2 a, global::UnityEngine.Vector2 b)
		{
			return new global::UnityEngine.Vector2(a.x * b.x, a.y * b.y);
		}
	}
}
