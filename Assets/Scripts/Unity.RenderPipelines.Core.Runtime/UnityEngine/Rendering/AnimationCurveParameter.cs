namespace UnityEngine.Rendering
{
	[global::System.Serializable]
	public class AnimationCurveParameter : global::UnityEngine.Rendering.VolumeParameter<global::UnityEngine.AnimationCurve>
	{
		public AnimationCurveParameter(global::UnityEngine.AnimationCurve value, bool overrideState = false)
			: base(value, overrideState)
		{
		}

		public override void Interp(global::UnityEngine.AnimationCurve lhsCurve, global::UnityEngine.AnimationCurve rhsCurve, float t)
		{
			m_Value = lhsCurve;
			global::UnityEngine.Rendering.KeyframeUtility.InterpAnimationCurve(ref m_Value, rhsCurve, t);
		}

		public override void SetValue(global::UnityEngine.Rendering.VolumeParameter parameter)
		{
			m_Value.CopyFrom(((global::UnityEngine.Rendering.AnimationCurveParameter)parameter).m_Value);
		}

		public override object Clone()
		{
			return new global::UnityEngine.Rendering.AnimationCurveParameter(new global::UnityEngine.AnimationCurve(GetValue<global::UnityEngine.AnimationCurve>().keys), overrideState);
		}

		public override int GetHashCode()
		{
			return overrideState.GetHashCode() * 23 + value.GetHashCode();
		}
	}
}
