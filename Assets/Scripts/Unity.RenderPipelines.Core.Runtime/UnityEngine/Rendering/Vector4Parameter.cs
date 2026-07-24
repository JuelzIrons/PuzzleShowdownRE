namespace UnityEngine.Rendering
{
	[global::System.Serializable]
	[global::System.Diagnostics.DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	public class Vector4Parameter : global::UnityEngine.Rendering.VolumeParameter<global::UnityEngine.Vector4>
	{
		public Vector4Parameter(global::UnityEngine.Vector4 value, bool overrideState = false)
			: base(value, overrideState)
		{
		}

		public override void Interp(global::UnityEngine.Vector4 from, global::UnityEngine.Vector4 to, float t)
		{
			m_Value.x = from.x + (to.x - from.x) * t;
			m_Value.y = from.y + (to.y - from.y) * t;
			m_Value.z = from.z + (to.z - from.z) * t;
			m_Value.w = from.w + (to.w - from.w) * t;
		}
	}
}
