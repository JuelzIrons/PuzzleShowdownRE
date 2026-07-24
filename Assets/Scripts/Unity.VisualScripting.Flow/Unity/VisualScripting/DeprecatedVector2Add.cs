namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Math/Vector 2")]
	[global::Unity.VisualScripting.UnitTitle("Add")]
	[global::System.Obsolete("Use the new \"Add (Math/Vector 2)\" node instead.")]
	[global::Unity.VisualScripting.RenamedFrom("Bolt.Vector2Add")]
	[global::Unity.VisualScripting.RenamedFrom("Unity.VisualScripting.Vector2Add")]
	public sealed class DeprecatedVector2Add : global::Unity.VisualScripting.Add<global::UnityEngine.Vector2>
	{
		protected override global::UnityEngine.Vector2 defaultB => global::UnityEngine.Vector2.zero;

		public override global::UnityEngine.Vector2 Operation(global::UnityEngine.Vector2 a, global::UnityEngine.Vector2 b)
		{
			return a + b;
		}
	}
}
