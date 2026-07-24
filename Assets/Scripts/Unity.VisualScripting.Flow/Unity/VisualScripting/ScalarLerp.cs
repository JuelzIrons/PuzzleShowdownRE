namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Math/Scalar")]
	[global::Unity.VisualScripting.UnitTitle("Lerp")]
	public sealed class ScalarLerp : global::Unity.VisualScripting.Lerp<float>
	{
		protected override float defaultA => 0f;

		protected override float defaultB => 1f;

		public override float Operation(float a, float b, float t)
		{
			return global::UnityEngine.Mathf.Lerp(a, b, t);
		}
	}
}
