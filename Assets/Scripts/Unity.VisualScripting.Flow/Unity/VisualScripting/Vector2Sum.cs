namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Math/Vector 2")]
	[global::Unity.VisualScripting.UnitTitle("Add")]
	public sealed class Vector2Sum : global::Unity.VisualScripting.Sum<global::UnityEngine.Vector2>, global::Unity.VisualScripting.IDefaultValue<global::UnityEngine.Vector2>
	{
		[global::Unity.VisualScripting.DoNotSerialize]
		public global::UnityEngine.Vector2 defaultValue => global::UnityEngine.Vector2.zero;

		public override global::UnityEngine.Vector2 Operation(global::UnityEngine.Vector2 a, global::UnityEngine.Vector2 b)
		{
			return a + b;
		}

		public override global::UnityEngine.Vector2 Operation(global::System.Collections.Generic.IEnumerable<global::UnityEngine.Vector2> values)
		{
			global::UnityEngine.Vector2 zero = global::UnityEngine.Vector2.zero;
			foreach (global::UnityEngine.Vector2 value in values)
			{
				zero += value;
			}
			return zero;
		}
	}
}
