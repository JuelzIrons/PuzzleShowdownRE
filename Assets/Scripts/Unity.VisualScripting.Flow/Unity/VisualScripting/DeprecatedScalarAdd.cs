namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Math/Scalar")]
	[global::Unity.VisualScripting.UnitTitle("Add")]
	[global::System.Obsolete("Use the new \"Add (Math/Scalar)\" node instead.")]
	[global::Unity.VisualScripting.RenamedFrom("Bolt.ScalarAdd")]
	[global::Unity.VisualScripting.RenamedFrom("Unity.VisualScripting.ScalarAdd")]
	public sealed class DeprecatedScalarAdd : global::Unity.VisualScripting.Add<float>
	{
		protected override float defaultB => 1f;

		public override float Operation(float a, float b)
		{
			return a + b;
		}
	}
}
