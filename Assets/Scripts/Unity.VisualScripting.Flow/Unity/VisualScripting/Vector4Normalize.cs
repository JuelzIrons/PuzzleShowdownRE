namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Math/Vector 4")]
	[global::Unity.VisualScripting.UnitTitle("Normalize")]
	public sealed class Vector4Normalize : global::Unity.VisualScripting.Normalize<global::UnityEngine.Vector4>
	{
		public override global::UnityEngine.Vector4 Operation(global::UnityEngine.Vector4 input)
		{
			return global::UnityEngine.Vector4.Normalize(input);
		}
	}
}
