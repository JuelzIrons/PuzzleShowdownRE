namespace UnityEngine.Rendering
{
	[global::System.Serializable]
	[global::System.Diagnostics.DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	public class Vector3Parameter : global::UnityEngine.Rendering.VolumeParameter<global::UnityEngine.Vector3>
	{
		public Vector3Parameter(global::UnityEngine.Vector3 value, bool overrideState = false)
			: base(value, overrideState)
		{
		}

		public override void Interp(global::UnityEngine.Vector3 from, global::UnityEngine.Vector3 to, float t)
		{
			m_Value.x = from.x + (to.x - from.x) * t;
			m_Value.y = from.y + (to.y - from.y) * t;
			m_Value.z = from.z + (to.z - from.z) * t;
		}
	}
}
