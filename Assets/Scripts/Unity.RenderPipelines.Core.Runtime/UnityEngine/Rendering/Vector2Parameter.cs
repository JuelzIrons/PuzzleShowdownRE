namespace UnityEngine.Rendering
{
	[global::System.Serializable]
	[global::System.Diagnostics.DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	public class Vector2Parameter : global::UnityEngine.Rendering.VolumeParameter<global::UnityEngine.Vector2>
	{
		public Vector2Parameter(global::UnityEngine.Vector2 value, bool overrideState = false)
			: base(value, overrideState)
		{
		}

		public override void Interp(global::UnityEngine.Vector2 from, global::UnityEngine.Vector2 to, float t)
		{
			m_Value.x = from.x + (to.x - from.x) * t;
			m_Value.y = from.y + (to.y - from.y) * t;
		}
	}
}
