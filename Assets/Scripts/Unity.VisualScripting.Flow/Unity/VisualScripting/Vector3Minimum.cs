namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Math/Vector 3")]
	[global::Unity.VisualScripting.UnitTitle("Minimum")]
	public sealed class Vector3Minimum : global::Unity.VisualScripting.Minimum<global::UnityEngine.Vector3>
	{
		public override global::UnityEngine.Vector3 Operation(global::UnityEngine.Vector3 a, global::UnityEngine.Vector3 b)
		{
			return global::UnityEngine.Vector3.Min(a, b);
		}

		public override global::UnityEngine.Vector3 Operation(global::System.Collections.Generic.IEnumerable<global::UnityEngine.Vector3> values)
		{
			bool flag = false;
			global::UnityEngine.Vector3 vector = global::UnityEngine.Vector3.zero;
			foreach (global::UnityEngine.Vector3 value in values)
			{
				if (!flag)
				{
					vector = value;
					flag = true;
				}
				else
				{
					vector = global::UnityEngine.Vector3.Min(vector, value);
				}
			}
			return vector;
		}
	}
}
