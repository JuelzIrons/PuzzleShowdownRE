namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Math/Vector 2")]
	[global::Unity.VisualScripting.UnitTitle("Move Towards")]
	public sealed class Vector2MoveTowards : global::Unity.VisualScripting.MoveTowards<global::UnityEngine.Vector2>
	{
		protected override global::UnityEngine.Vector2 defaultCurrent => global::UnityEngine.Vector2.zero;

		protected override global::UnityEngine.Vector2 defaultTarget => global::UnityEngine.Vector2.one;

		public override global::UnityEngine.Vector2 Operation(global::UnityEngine.Vector2 current, global::UnityEngine.Vector2 target, float maxDelta)
		{
			return global::UnityEngine.Vector2.MoveTowards(current, target, maxDelta);
		}
	}
}
