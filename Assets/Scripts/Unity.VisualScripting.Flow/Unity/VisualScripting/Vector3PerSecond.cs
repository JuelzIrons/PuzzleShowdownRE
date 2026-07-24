namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Math/Vector 3")]
	[global::Unity.VisualScripting.UnitTitle("Per Second")]
	public sealed class Vector3PerSecond : global::Unity.VisualScripting.PerSecond<global::UnityEngine.Vector3>
	{
		public override global::UnityEngine.Vector3 Operation(global::UnityEngine.Vector3 input)
		{
			return input * global::UnityEngine.Time.deltaTime;
		}
	}
}
