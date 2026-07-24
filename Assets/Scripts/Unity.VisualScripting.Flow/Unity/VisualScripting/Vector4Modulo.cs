namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Math/Vector 4")]
	[global::Unity.VisualScripting.UnitTitle("Modulo")]
	public sealed class Vector4Modulo : global::Unity.VisualScripting.Modulo<global::UnityEngine.Vector4>
	{
		protected override global::UnityEngine.Vector4 defaultDividend => global::UnityEngine.Vector4.zero;

		protected override global::UnityEngine.Vector4 defaultDivisor => global::UnityEngine.Vector4.zero;

		public override global::UnityEngine.Vector4 Operation(global::UnityEngine.Vector4 a, global::UnityEngine.Vector4 b)
		{
			return new global::UnityEngine.Vector4(a.x % b.x, a.y % b.y, a.z % b.z, a.w % b.w);
		}
	}
}
