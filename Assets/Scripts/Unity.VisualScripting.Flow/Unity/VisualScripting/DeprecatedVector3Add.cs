namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Math/Vector 3")]
	[global::Unity.VisualScripting.UnitTitle("Add")]
	[global::System.Obsolete("Use the new \"Add (Math/Vector 3)\" instead.")]
	[global::Unity.VisualScripting.RenamedFrom("Bolt.Vector3Add")]
	[global::Unity.VisualScripting.RenamedFrom("Unity.VisualScripting.Vector3Add")]
	public sealed class DeprecatedVector3Add : global::Unity.VisualScripting.Add<global::UnityEngine.Vector3>
	{
		protected override global::UnityEngine.Vector3 defaultB => global::UnityEngine.Vector3.zero;

		public override global::UnityEngine.Vector3 Operation(global::UnityEngine.Vector3 a, global::UnityEngine.Vector3 b)
		{
			return a + b;
		}
	}
}
