namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Math/Vector 3")]
	[global::Unity.VisualScripting.UnitTitle("Lerp")]
	public sealed class Vector3Lerp : global::Unity.VisualScripting.Lerp<global::UnityEngine.Vector3>
	{
		protected override global::UnityEngine.Vector3 defaultA => global::UnityEngine.Vector3.zero;

		protected override global::UnityEngine.Vector3 defaultB => global::UnityEngine.Vector3.one;

		public override global::UnityEngine.Vector3 Operation(global::UnityEngine.Vector3 a, global::UnityEngine.Vector3 b, float t)
		{
			return global::UnityEngine.Vector3.Lerp(a, b, t);
		}
	}
}
