namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Math/Vector 4")]
	[global::Unity.VisualScripting.UnitTitle("Lerp")]
	public sealed class Vector4Lerp : global::Unity.VisualScripting.Lerp<global::UnityEngine.Vector4>
	{
		protected override global::UnityEngine.Vector4 defaultA => global::UnityEngine.Vector4.zero;

		protected override global::UnityEngine.Vector4 defaultB => global::UnityEngine.Vector4.one;

		public override global::UnityEngine.Vector4 Operation(global::UnityEngine.Vector4 a, global::UnityEngine.Vector4 b, float t)
		{
			return global::UnityEngine.Vector4.Lerp(a, b, t);
		}
	}
}
