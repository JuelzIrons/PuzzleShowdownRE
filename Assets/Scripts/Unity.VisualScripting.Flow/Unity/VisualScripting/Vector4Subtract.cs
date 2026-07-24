namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Math/Vector 4")]
	[global::Unity.VisualScripting.UnitTitle("Subtract")]
	public sealed class Vector4Subtract : global::Unity.VisualScripting.Subtract<global::UnityEngine.Vector4>
	{
		protected override global::UnityEngine.Vector4 defaultMinuend => global::UnityEngine.Vector4.zero;

		protected override global::UnityEngine.Vector4 defaultSubtrahend => global::UnityEngine.Vector4.zero;

		public override global::UnityEngine.Vector4 Operation(global::UnityEngine.Vector4 a, global::UnityEngine.Vector4 b)
		{
			return a - b;
		}
	}
}
