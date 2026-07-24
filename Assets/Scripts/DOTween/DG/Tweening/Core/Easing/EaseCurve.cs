namespace DG.Tweening.Core.Easing
{
	public class EaseCurve
	{
		private readonly global::UnityEngine.AnimationCurve _animCurve;

		public EaseCurve(global::UnityEngine.AnimationCurve animCurve)
		{
			_animCurve = animCurve;
		}

		public float Evaluate(float time, float duration, float unusedOvershoot, float unusedPeriod)
		{
			float time2 = _animCurve[_animCurve.length - 1].time;
			float num = time / duration;
			return _animCurve.Evaluate(num * time2);
		}
	}
}
