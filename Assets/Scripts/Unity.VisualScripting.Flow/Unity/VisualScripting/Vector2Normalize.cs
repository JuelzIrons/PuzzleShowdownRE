namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Math/Vector 2")]
	[global::Unity.VisualScripting.UnitTitle("Normalize")]
	public sealed class Vector2Normalize : global::Unity.VisualScripting.Normalize<global::UnityEngine.Vector2>
	{
		public override global::UnityEngine.Vector2 Operation(global::UnityEngine.Vector2 input)
		{
			return input.normalized;
		}
	}
}
