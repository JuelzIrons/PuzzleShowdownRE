namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Math/Vector 2")]
	[global::Unity.VisualScripting.UnitTitle("Per Second")]
	public sealed class Vector2PerSecond : global::Unity.VisualScripting.PerSecond<global::UnityEngine.Vector2>
	{
		public override global::UnityEngine.Vector2 Operation(global::UnityEngine.Vector2 input)
		{
			return input * global::UnityEngine.Time.deltaTime;
		}
	}
}
