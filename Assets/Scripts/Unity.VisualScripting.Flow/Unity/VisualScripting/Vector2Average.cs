namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Math/Vector 2")]
	[global::Unity.VisualScripting.UnitTitle("Average")]
	public sealed class Vector2Average : global::Unity.VisualScripting.Average<global::UnityEngine.Vector2>
	{
		public override global::UnityEngine.Vector2 Operation(global::UnityEngine.Vector2 a, global::UnityEngine.Vector2 b)
		{
			return (a + b) / 2f;
		}

		public override global::UnityEngine.Vector2 Operation(global::System.Collections.Generic.IEnumerable<global::UnityEngine.Vector2> values)
		{
			global::UnityEngine.Vector2 zero = global::UnityEngine.Vector2.zero;
			int num = 0;
			foreach (global::UnityEngine.Vector2 value in values)
			{
				zero += value;
				num++;
			}
			return zero / num;
		}
	}
}
