namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Math/Vector 3")]
	[global::Unity.VisualScripting.UnitTitle("Divide")]
	public sealed class Vector3Divide : global::Unity.VisualScripting.Divide<global::UnityEngine.Vector3>
	{
		protected override global::UnityEngine.Vector3 defaultDividend => global::UnityEngine.Vector3.zero;

		protected override global::UnityEngine.Vector3 defaultDivisor => global::UnityEngine.Vector3.zero;

		public override global::UnityEngine.Vector3 Operation(global::UnityEngine.Vector3 a, global::UnityEngine.Vector3 b)
		{
			return new global::UnityEngine.Vector3(a.x / b.x, a.y / b.y, a.z / b.z);
		}
	}
}
