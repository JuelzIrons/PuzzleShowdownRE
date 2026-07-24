namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Math/Vector 3")]
	[global::Unity.VisualScripting.UnitTitle("Distance")]
	public sealed class Vector3Distance : global::Unity.VisualScripting.Distance<global::UnityEngine.Vector3>
	{
		public override float Operation(global::UnityEngine.Vector3 a, global::UnityEngine.Vector3 b)
		{
			return global::UnityEngine.Vector3.Distance(a, b);
		}
	}
}
