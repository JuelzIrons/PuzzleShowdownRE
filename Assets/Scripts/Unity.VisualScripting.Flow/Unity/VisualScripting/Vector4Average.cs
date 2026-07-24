namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Math/Vector 4")]
	[global::Unity.VisualScripting.UnitTitle("Average")]
	public sealed class Vector4Average : global::Unity.VisualScripting.Average<global::UnityEngine.Vector4>
	{
		public override global::UnityEngine.Vector4 Operation(global::UnityEngine.Vector4 a, global::UnityEngine.Vector4 b)
		{
			return (a + b) / 2f;
		}

		public override global::UnityEngine.Vector4 Operation(global::System.Collections.Generic.IEnumerable<global::UnityEngine.Vector4> values)
		{
			global::UnityEngine.Vector4 zero = global::UnityEngine.Vector4.zero;
			int num = 0;
			foreach (global::UnityEngine.Vector4 value in values)
			{
				zero += value;
				num++;
			}
			return zero / num;
		}
	}
}
