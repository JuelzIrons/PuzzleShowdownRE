namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Math/Vector 3")]
	[global::Unity.VisualScripting.UnitTitle("Move Towards")]
	public sealed class Vector3MoveTowards : global::Unity.VisualScripting.MoveTowards<global::UnityEngine.Vector3>
	{
		protected override global::UnityEngine.Vector3 defaultCurrent => global::UnityEngine.Vector3.zero;

		protected override global::UnityEngine.Vector3 defaultTarget => global::UnityEngine.Vector3.one;

		public override global::UnityEngine.Vector3 Operation(global::UnityEngine.Vector3 current, global::UnityEngine.Vector3 target, float maxDelta)
		{
			return global::UnityEngine.Vector3.MoveTowards(current, target, maxDelta);
		}
	}
}
