namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Math/Vector 4")]
	[global::Unity.VisualScripting.UnitTitle("Distance")]
	public sealed class Vector4Distance : global::Unity.VisualScripting.Distance<global::UnityEngine.Vector4>
	{
		public override float Operation(global::UnityEngine.Vector4 a, global::UnityEngine.Vector4 b)
		{
			return global::UnityEngine.Vector4.Distance(a, b);
		}
	}
}
