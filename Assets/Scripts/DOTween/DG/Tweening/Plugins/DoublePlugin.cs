namespace DG.Tweening.Plugins
{
	public class DoublePlugin : global::DG.Tweening.Plugins.Core.ABSTweenPlugin<double, double, global::DG.Tweening.Plugins.Options.NoOptions>
	{
		public override void Reset(global::DG.Tweening.Core.TweenerCore<double, double, global::DG.Tweening.Plugins.Options.NoOptions> t)
		{
		}

		public override void SetFrom(global::DG.Tweening.Core.TweenerCore<double, double, global::DG.Tweening.Plugins.Options.NoOptions> t, bool isRelative)
		{
			double endValue = t.endValue;
			t.endValue = t.getter();
			t.startValue = (isRelative ? (t.endValue + endValue) : endValue);
			t.setter(t.startValue);
		}

		public override void SetFrom(global::DG.Tweening.Core.TweenerCore<double, double, global::DG.Tweening.Plugins.Options.NoOptions> t, double fromValue, bool setImmediately, bool isRelative)
		{
			if (isRelative)
			{
				double num = t.getter();
				t.endValue += num;
				fromValue += num;
			}
			t.startValue = fromValue;
			if (setImmediately)
			{
				t.setter(fromValue);
			}
		}

		public override double ConvertToStartValue(global::DG.Tweening.Core.TweenerCore<double, double, global::DG.Tweening.Plugins.Options.NoOptions> t, double value)
		{
			return value;
		}

		public override void SetRelativeEndValue(global::DG.Tweening.Core.TweenerCore<double, double, global::DG.Tweening.Plugins.Options.NoOptions> t)
		{
			t.endValue += t.startValue;
		}

		public override void SetChangeValue(global::DG.Tweening.Core.TweenerCore<double, double, global::DG.Tweening.Plugins.Options.NoOptions> t)
		{
			t.changeValue = t.endValue - t.startValue;
		}

		public override float GetSpeedBasedDuration(global::DG.Tweening.Plugins.Options.NoOptions options, float unitsXSecond, double changeValue)
		{
			float num = (float)changeValue / unitsXSecond;
			if (num < 0f)
			{
				num = 0f - num;
			}
			return num;
		}

		public override void EvaluateAndApply(global::DG.Tweening.Plugins.Options.NoOptions options, global::DG.Tweening.Tween t, bool isRelative, global::DG.Tweening.Core.DOGetter<double> getter, global::DG.Tweening.Core.DOSetter<double> setter, float elapsed, double startValue, double changeValue, float duration, bool usingInversePosition, int newCompletedSteps, global::DG.Tweening.Core.Enums.UpdateNotice updateNotice)
		{
			if (t.loopType == global::DG.Tweening.LoopType.Incremental)
			{
				startValue += changeValue * (double)(t.isComplete ? (t.completedLoops - 1) : t.completedLoops);
			}
			if (t.isSequenced && t.sequenceParent.loopType == global::DG.Tweening.LoopType.Incremental)
			{
				startValue += changeValue * (double)((t.loopType != global::DG.Tweening.LoopType.Incremental) ? 1 : t.loops) * (double)(t.sequenceParent.isComplete ? (t.sequenceParent.completedLoops - 1) : t.sequenceParent.completedLoops);
			}
			setter(startValue + changeValue * (double)global::DG.Tweening.Core.Easing.EaseManager.Evaluate(t.easeType, t.customEase, elapsed, duration, t.easeOvershootOrAmplitude, t.easePeriod));
		}
	}
}
