namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Math/Scalar")]
	[global::Unity.VisualScripting.UnitTitle("Absolute")]
	public sealed class ScalarAbsolute : global::Unity.VisualScripting.Absolute<float>
	{
		protected override float Operation(float input)
		{
			return global::UnityEngine.Mathf.Abs(input);
		}
	}
}
