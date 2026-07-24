namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Math/Vector 4")]
	[global::Unity.VisualScripting.UnitTitle("Dot Product")]
	public sealed class Vector4DotProduct : global::Unity.VisualScripting.DotProduct<global::UnityEngine.Vector4>
	{
		public override float Operation(global::UnityEngine.Vector4 a, global::UnityEngine.Vector4 b)
		{
			return global::UnityEngine.Vector4.Dot(a, b);
		}
	}
}
