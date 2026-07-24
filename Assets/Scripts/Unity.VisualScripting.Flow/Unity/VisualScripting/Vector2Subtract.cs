namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Math/Vector 2")]
	[global::Unity.VisualScripting.UnitTitle("Subtract")]
	public sealed class Vector2Subtract : global::Unity.VisualScripting.Subtract<global::UnityEngine.Vector2>
	{
		protected override global::UnityEngine.Vector2 defaultMinuend => global::UnityEngine.Vector2.zero;

		protected override global::UnityEngine.Vector2 defaultSubtrahend => global::UnityEngine.Vector2.zero;

		public override global::UnityEngine.Vector2 Operation(global::UnityEngine.Vector2 a, global::UnityEngine.Vector2 b)
		{
			return a - b;
		}
	}
}
