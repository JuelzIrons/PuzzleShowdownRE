namespace DG.Tweening.Plugins.Core
{
	public abstract class ABSTweenPlugin<T1, T2, TPlugOptions> : global::DG.Tweening.Plugins.Core.ITweenPlugin where TPlugOptions : struct, global::DG.Tweening.Plugins.Options.IPlugOptions
	{
		public abstract void Reset(global::DG.Tweening.Core.TweenerCore<T1, T2, TPlugOptions> t);

		public abstract void SetFrom(global::DG.Tweening.Core.TweenerCore<T1, T2, TPlugOptions> t, bool isRelative);

		public abstract void SetFrom(global::DG.Tweening.Core.TweenerCore<T1, T2, TPlugOptions> t, T2 fromValue, bool setImmediately, bool isRelative);

		public abstract T2 ConvertToStartValue(global::DG.Tweening.Core.TweenerCore<T1, T2, TPlugOptions> t, T1 value);

		public abstract void SetRelativeEndValue(global::DG.Tweening.Core.TweenerCore<T1, T2, TPlugOptions> t);

		public abstract void SetChangeValue(global::DG.Tweening.Core.TweenerCore<T1, T2, TPlugOptions> t);

		public abstract float GetSpeedBasedDuration(TPlugOptions options, float unitsXSecond, T2 changeValue);

		public abstract void EvaluateAndApply(TPlugOptions options, global::DG.Tweening.Tween t, bool isRelative, global::DG.Tweening.Core.DOGetter<T1> getter, global::DG.Tweening.Core.DOSetter<T1> setter, float elapsed, T2 startValue, T2 changeValue, float duration, bool usingInversePosition, int newCompletedSteps, global::DG.Tweening.Core.Enums.UpdateNotice updateNotice);
	}
}
