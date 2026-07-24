namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Math/Vector 4")]
	[global::Unity.VisualScripting.UnitTitle("Per Second")]
	public sealed class Vector4PerSecond : global::Unity.VisualScripting.PerSecond<global::UnityEngine.Vector4>
	{
		public override global::UnityEngine.Vector4 Operation(global::UnityEngine.Vector4 input)
		{
			return input * global::UnityEngine.Time.deltaTime;
		}
	}
}
