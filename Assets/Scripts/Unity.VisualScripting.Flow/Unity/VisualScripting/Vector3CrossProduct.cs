namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Math/Vector 3")]
	[global::Unity.VisualScripting.UnitTitle("Cross Product")]
	public sealed class Vector3CrossProduct : global::Unity.VisualScripting.CrossProduct<global::UnityEngine.Vector3>
	{
		public override global::UnityEngine.Vector3 Operation(global::UnityEngine.Vector3 a, global::UnityEngine.Vector3 b)
		{
			return global::UnityEngine.Vector3.Cross(a, b);
		}
	}
}
