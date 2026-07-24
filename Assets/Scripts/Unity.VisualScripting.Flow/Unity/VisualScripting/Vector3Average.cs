namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Math/Vector 3")]
	[global::Unity.VisualScripting.UnitTitle("Average")]
	public sealed class Vector3Average : global::Unity.VisualScripting.Average<global::UnityEngine.Vector3>
	{
		public override global::UnityEngine.Vector3 Operation(global::UnityEngine.Vector3 a, global::UnityEngine.Vector3 b)
		{
			return (a + b) / 2f;
		}

		public override global::UnityEngine.Vector3 Operation(global::System.Collections.Generic.IEnumerable<global::UnityEngine.Vector3> values)
		{
			global::UnityEngine.Vector3 zero = global::UnityEngine.Vector3.zero;
			int num = 0;
			foreach (global::UnityEngine.Vector3 value in values)
			{
				zero += value;
				num++;
			}
			return zero / num;
		}
	}
}
