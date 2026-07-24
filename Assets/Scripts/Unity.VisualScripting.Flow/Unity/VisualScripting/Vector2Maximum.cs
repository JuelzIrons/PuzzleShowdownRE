namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Math/Vector 2")]
	[global::Unity.VisualScripting.UnitTitle("Maximum")]
	public sealed class Vector2Maximum : global::Unity.VisualScripting.Maximum<global::UnityEngine.Vector2>
	{
		public override global::UnityEngine.Vector2 Operation(global::UnityEngine.Vector2 a, global::UnityEngine.Vector2 b)
		{
			return global::UnityEngine.Vector2.Max(a, b);
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
					vector = global::UnityEngine.Vector2.Max(vector, value);
				}
			}
			return vector;
		}
	}
}
