namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Math/Vector 3")]
	[global::Unity.VisualScripting.UnitTitle("Dot Product")]
	public sealed class Vector3DotProduct : global::Unity.VisualScripting.DotProduct<global::UnityEngine.Vector3>
	{
		public override float Operation(global::UnityEngine.Vector3 a, global::UnityEngine.Vector3 b)
		{
			return global::UnityEngine.Vector3.Dot(a, b);
		}
	}
}
