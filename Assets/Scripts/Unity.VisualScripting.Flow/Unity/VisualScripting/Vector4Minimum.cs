namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Math/Vector 4")]
	[global::Unity.VisualScripting.UnitTitle("Minimum")]
	public sealed class Vector4Minimum : global::Unity.VisualScripting.Minimum<global::UnityEngine.Vector4>
	{
		public override global::UnityEngine.Vector4 Operation(global::UnityEngine.Vector4 a, global::UnityEngine.Vector4 b)
		{
			return global::UnityEngine.Vector4.Min(a, b);
		}

		public override global::UnityEngine.Vector4 Operation(global::System.Collections.Generic.IEnumerable<global::UnityEngine.Vector4> values)
		{
			bool flag = false;
			global::UnityEngine.Vector4 vector = global::UnityEngine.Vector4.zero;
			foreach (global::UnityEngine.Vector4 value in values)
			{
				if (!flag)
				{
					vector = value;
					flag = true;
				}
				else
				{
					vector = global::UnityEngine.Vector4.Min(vector, value);
				}
			}
			return vector;
		}
	}
}
