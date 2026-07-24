namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Math/Scalar")]
	[global::Unity.VisualScripting.UnitTitle("Move Towards")]
	public sealed class ScalarMoveTowards : global::Unity.VisualScripting.MoveTowards<float>
	{
		protected override float defaultCurrent => 0f;

		protected override float defaultTarget => 1f;

		public override float Operation(float current, float target, float maxDelta)
		{
			return global::UnityEngine.Mathf.MoveTowards(current, target, maxDelta);
		}
	}
}
