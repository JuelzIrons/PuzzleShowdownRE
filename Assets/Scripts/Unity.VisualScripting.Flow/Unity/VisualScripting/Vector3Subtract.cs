namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Math/Vector 3")]
	[global::Unity.VisualScripting.UnitTitle("Subtract")]
	public sealed class Vector3Subtract : global::Unity.VisualScripting.Subtract<global::UnityEngine.Vector3>
	{
		protected override global::UnityEngine.Vector3 defaultMinuend => global::UnityEngine.Vector3.zero;

		protected override global::UnityEngine.Vector3 defaultSubtrahend => global::UnityEngine.Vector3.zero;

		public override global::UnityEngine.Vector3 Operation(global::UnityEngine.Vector3 a, global::UnityEngine.Vector3 b)
		{
			return a - b;
		}
	}
}
