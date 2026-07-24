namespace DG.Tweening
{
	public class EaseFactory
	{
		public static global::DG.Tweening.EaseFunction StopMotion(int motionFps, global::DG.Tweening.Ease? ease = null)
		{
			global::DG.Tweening.EaseFunction customEase = global::DG.Tweening.Core.Easing.EaseManager.ToEaseFunction((!ease.HasValue) ? global::DG.Tweening.DOTween.defaultEaseType : ease.Value);
			return StopMotion(motionFps, customEase);
		}

		public static global::DG.Tweening.EaseFunction StopMotion(int motionFps, global::UnityEngine.AnimationCurve animCurve)
		{
			return StopMotion(motionFps, new global::DG.Tweening.Core.Easing.EaseCurve(animCurve).Evaluate);
		}

		public static global::DG.Tweening.EaseFunction StopMotion(int motionFps, global::DG.Tweening.EaseFunction customEase)
		{
			float motionDelay = 1f / (float)motionFps;
			return delegate(float time, float duration, float overshootOrAmplitude, float period)
			{
				float time2 = ((time < duration) ? (time - time % motionDelay) : time);
				return customEase(time2, duration, overshootOrAmplitude, period);
			};
		}
	}
}
