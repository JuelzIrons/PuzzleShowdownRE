namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Math/Scalar")]
	[global::Unity.VisualScripting.UnitTitle("Per Second")]
	public sealed class ScalarPerSecond : global::Unity.VisualScripting.PerSecond<float>
	{
		public override float Operation(float input)
		{
			return input * global::UnityEngine.Time.deltaTime;
		}
	}
}
