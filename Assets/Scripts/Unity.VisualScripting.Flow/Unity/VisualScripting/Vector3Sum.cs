namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Math/Vector 3")]
	[global::Unity.VisualScripting.UnitTitle("Add")]
	public sealed class Vector3Sum : global::Unity.VisualScripting.Sum<global::UnityEngine.Vector3>, global::Unity.VisualScripting.IDefaultValue<global::UnityEngine.Vector3>
	{
		[global::Unity.VisualScripting.DoNotSerialize]
		public global::UnityEngine.Vector3 defaultValue => global::UnityEngine.Vector3.zero;

		public override global::UnityEngine.Vector3 Operation(global::UnityEngine.Vector3 a, global::UnityEngine.Vector3 b)
		{
			return a + b;
		}

		public override global::UnityEngine.Vector3 Operation(global::System.Collections.Generic.IEnumerable<global::UnityEngine.Vector3> values)
		{
			global::UnityEngine.Vector3 zero = global::UnityEngine.Vector3.zero;
			foreach (global::UnityEngine.Vector3 value in values)
			{
				zero += value;
			}
			return zero;
		}
	}
}
