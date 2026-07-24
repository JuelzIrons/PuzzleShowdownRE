namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Math/Scalar")]
	[global::Unity.VisualScripting.UnitTitle("Normalize")]
	public sealed class ScalarNormalize : global::Unity.VisualScripting.Normalize<float>
	{
		public override float Operation(float input)
		{
			if (input == 0f)
			{
				return 0f;
			}
			return input / global::UnityEngine.Mathf.Abs(input);
		}
	}
}
