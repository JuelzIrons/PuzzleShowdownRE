namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Math/Vector 4")]
	[global::Unity.VisualScripting.UnitTitle("Add")]
	public sealed class Vector4Sum : global::Unity.VisualScripting.Sum<global::UnityEngine.Vector4>, global::Unity.VisualScripting.IDefaultValue<global::UnityEngine.Vector4>
	{
		[global::Unity.VisualScripting.DoNotSerialize]
		public global::UnityEngine.Vector4 defaultValue => global::UnityEngine.Vector4.zero;

		public override global::UnityEngine.Vector4 Operation(global::UnityEngine.Vector4 a, global::UnityEngine.Vector4 b)
		{
			return a + b;
		}

		public override global::UnityEngine.Vector4 Operation(global::System.Collections.Generic.IEnumerable<global::UnityEngine.Vector4> values)
		{
			global::UnityEngine.Vector4 zero = global::UnityEngine.Vector4.zero;
			foreach (global::UnityEngine.Vector4 value in values)
			{
				zero += value;
			}
			return zero;
		}
	}
}
