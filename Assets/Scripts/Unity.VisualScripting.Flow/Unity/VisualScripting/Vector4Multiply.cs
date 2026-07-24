namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Math/Vector 4")]
	[global::Unity.VisualScripting.UnitTitle("Multiply")]
	public sealed class Vector4Multiply : global::Unity.VisualScripting.Multiply<global::UnityEngine.Vector4>
	{
		protected override global::UnityEngine.Vector4 defaultB => global::UnityEngine.Vector4.zero;

		public override global::UnityEngine.Vector4 Operation(global::UnityEngine.Vector4 a, global::UnityEngine.Vector4 b)
		{
			return new global::UnityEngine.Vector4(a.x * b.x, a.y * b.y, a.z * b.z, a.w * b.w);
		}
	}
}
