namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Math/Vector 3")]
	[global::Unity.VisualScripting.UnitTitle("Normalize")]
	public sealed class Vector3Normalize : global::Unity.VisualScripting.Normalize<global::UnityEngine.Vector3>
	{
		public override global::UnityEngine.Vector3 Operation(global::UnityEngine.Vector3 input)
		{
			return global::UnityEngine.Vector3.Normalize(input);
		}
	}
}
