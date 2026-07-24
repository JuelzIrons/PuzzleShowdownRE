namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Math/Vector 4")]
	[global::Unity.VisualScripting.UnitTitle("Move Towards")]
	public sealed class Vector4MoveTowards : global::Unity.VisualScripting.MoveTowards<global::UnityEngine.Vector4>
	{
		protected override global::UnityEngine.Vector4 defaultCurrent => global::UnityEngine.Vector4.zero;

		protected override global::UnityEngine.Vector4 defaultTarget => global::UnityEngine.Vector4.one;

		public override global::UnityEngine.Vector4 Operation(global::UnityEngine.Vector4 current, global::UnityEngine.Vector4 target, float maxDelta)
		{
			return global::UnityEngine.Vector4.MoveTowards(current, target, maxDelta);
		}
	}
}
