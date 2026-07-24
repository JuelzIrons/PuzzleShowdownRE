namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Math/Vector 2")]
	[global::Unity.VisualScripting.UnitTitle("Minimum")]
	public sealed class Vector2Minimum : global::Unity.VisualScripting.Minimum<global::UnityEngine.Vector2>
	{
		public override global::UnityEngine.Vector2 Operation(global::UnityEngine.Vector2 a, global::UnityEngine.Vector2 b)
		{
			return global::UnityEngine.Vector2.Min(a, b);
		}

		public override global::UnityEngine.Vector2 Operation(global::System.Collections.Generic.IEnumerable<global::UnityEngine.Vector2> values)
		{
			bool flag = false;
			global::UnityEngine.Vector2 vector = global::UnityEngine.Vector2.zero;
			foreach (global::UnityEngine.Vector2 value in values)
			{
				if (!flag)
				{
					vector = value;
					flag = true;
				}
				else
				{
					vector = global::UnityEngine.Vector2.Min(vector, value);
				}
			}
			return vector;
		}
	}
}
