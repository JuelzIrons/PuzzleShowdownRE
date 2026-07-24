namespace DG.Tweening.Plugins
{
	public class LongPlugin : global::DG.Tweening.Plugins.Core.ABSTweenPlugin<long, long, global::DG.Tweening.Plugins.Options.NoOptions>
	{
		public override void Reset(global::DG.Tweening.Core.TweenerCore<long, long, global::DG.Tweening.Plugins.Options.NoOptions> t)
		{
		}

		public override void SetFrom(global::DG.Tweening.Core.TweenerCore<long, long, global::DG.Tweening.Plugins.Options.NoOptions> t, bool isRelative)
		{
			long endValue = t.endValue;
			t.endValue = t.getter();
			t.startValue = (isRelative ? (t.endValue + endValue) : endValue);
			t.setter(t.startValue);
		}

		public override void SetFrom(global::DG.Tweening.Core.TweenerCore<long, long, global::DG.Tweening.Plugins.Options.NoOptions> t, long fromValue, bool setImmediately, bool isRelative)
		{
			if (isRelative)
			{
				long num = t.getter();
				t.endValue += num;
				fromValue += num;
			}
			t.startValue = fromValue;
			if (setImmediately)
			{
				t.setter(fromValue);
			}
		}

		public override long ConvertToStartValue(global::DG.Tweening.Core.TweenerCore<long, long, global::DG.Tweening.Plugins.Options.NoOptions> t, long value)
		{
			return value;
		}

		public override void SetRelativeEndValue(global::DG.Tweening.Core.TweenerCore<long, long, global::DG.Tweening.Plugins.Options.NoOptions> t)
		{
			t.endValue += t.startValue;
		}

		public override void SetChangeValue(global::DG.Tweening.Core.TweenerCore<long, long, global::DG.Tweening.Plugins.Options.NoOptions> t)
		{
			t.changeValue = t.endValue - t.startValue;
		}

		public override float GetSpeedBasedDuration(global::DG.Tweening.Plugins.Options.NoOptions options, float unitsXSecond, long changeValue)
		{
			float num = (float)changeValue / unitsXSecond;
			if (num < 0f)
			{
				num = 0f - num;
			}
			return num;
		}

		public override void EvaluateAndApply(global::DG.Tweening.Plugins.Options.NoOptions options, global::DG.Tweening.Tween t, bool isRelative, global::DG.Tweening.Core.DOGetter<long> getter, global::DG.Tweening.Core.DOSetter<long> setter, float elapsed, long startValue, long changeValue, float duration, bool usingInversePosition, int newCompletedSteps, global::DG.Tweening.Core.Enums.UpdateNotice updateNotice)
		{
			if (t.loopType == global::DG.Tweening.LoopType.Incremental)
			{
				startValue += changeValue * (t.isComplete ? (t.completedLoops - 1) : t.completedLoops);
			}
			if (t.isSequenced && t.sequenceParent.loopType == global::DG.Tweening.LoopType.Incremental)
			{
				startValue += changeValue * ((t.loopType != global::DG.Tweening.LoopType.Incremental) ? 1 : t.loops) * (t.sequenceParent.isComplete ? (t.sequenceParent.completedLoops - 1) : t.sequenceParent.completedLoops);
			}
			setter((long)global::System.Math.Round((float)startValue + (float)changeValue * global::DG.Tweening.Core.Easing.EaseManager.Evaluate(t.easeType, t.customEase, elapsed, duration, t.easeOvershootOrAmplitude, t.easePeriod)));
		}
	}
}
