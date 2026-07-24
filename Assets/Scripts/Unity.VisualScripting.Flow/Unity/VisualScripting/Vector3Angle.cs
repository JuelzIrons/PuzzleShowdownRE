namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Math/Vector 3")]
	[global::Unity.VisualScripting.UnitTitle("Angle")]
	public sealed class Vector3Angle : global::Unity.VisualScripting.Angle<global::UnityEngine.Vector3>
	{
		public override float Operation(global::UnityEngine.Vector3 a, global::UnityEngine.Vector3 b)
		{
			return global::UnityEngine.Vector3.Angle(a, b);
		}
	}
}
