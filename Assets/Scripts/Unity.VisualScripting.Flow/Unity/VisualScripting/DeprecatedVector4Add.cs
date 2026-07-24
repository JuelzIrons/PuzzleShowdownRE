namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Math/Vector 4")]
	[global::Unity.VisualScripting.UnitTitle("Add")]
	[global::System.Obsolete("Use the new \"Add (Math/Vector 4)\" instead.")]
	[global::Unity.VisualScripting.RenamedFrom("Bolt.Vector4Add")]
	[global::Unity.VisualScripting.RenamedFrom("Unity.VisualScripting.Vector4Add")]
	public sealed class DeprecatedVector4Add : global::Unity.VisualScripting.Add<global::UnityEngine.Vector4>
	{
		protected override global::UnityEngine.Vector4 defaultB => global::UnityEngine.Vector4.zero;

		public override global::UnityEngine.Vector4 Operation(global::UnityEngine.Vector4 a, global::UnityEngine.Vector4 b)
		{
			return a + b;
		}
	}
}
