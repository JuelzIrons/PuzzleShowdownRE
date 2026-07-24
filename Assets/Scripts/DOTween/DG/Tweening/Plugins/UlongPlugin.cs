namespace DG.Tweening.Plugins
{
	public class UlongPlugin : global::DG.Tweening.Plugins.Core.ABSTweenPlugin<ulong, ulong, global::DG.Tweening.Plugins.Options.NoOptions>
	{
		public override void Reset(global::DG.Tweening.Core.TweenerCore<ulong, ulong, global::DG.Tweening.Plugins.Options.NoOptions> t)
		{
		}

		public override void SetFrom(global::DG.Tweening.Core.TweenerCore<ulong, ulong, global::DG.Tweening.Plugins.Options.NoOptions> t, bool isRelative)
		{
			ulong endValue = t.endValue;
			t.endValue = t.getter();
			t.startValue = (isRelative ? (t.endValue + endValue) : endValue);
			t.setter(t.startValue);
		}

		public override void SetFrom(global::DG.Tweening.Core.TweenerCore<ulong, ulong, global::DG.Tweening.Plugins.Options.NoOptions> t, ulong fromValue, bool setImmediately, bool isRelative)
		{
			if (isRelative)
			{
				ulong num = t.getter();
				t.endValue += num;
				fromValue += num;
			}
			t.startValue = fromValue;
			if (setImmediately)
			{
				t.setter(fromValue);
			}
		}

		public override ulong ConvertToStartValue(global::DG.Tweening.Core.TweenerCore<ulong, ulong, global::DG.Tweening.Plugins.Options.NoOptions> t, ulong value)
		{
			return value;
		}

		public override void SetRelativeEndValue(global::DG.Tweening.Core.TweenerCore<ulong, ulong, global::DG.Tweening.Plugins.Options.NoOptions> t)
		{
			t.endValue += t.startValue;
		}

		public override void SetChangeValue(global::DG.Tweening.Core.TweenerCore<ulong, ulong, global::DG.Tweening.Plugins.Options.NoOptions> t)
		{
			t.changeValue = t.endValue - t.startValue;
		}

		public override float GetSpeedBasedDuration(global::DG.Tweening.Plugins.Options.NoOptions options, float unitsXSecond, ulong changeValue)
		{
			float num = (float)changeValue / unitsXSecond;
			if (num < 0f)
			{
				num = 0f - num;
			}
			return num;
		}

		public override void EvaluateAndApply(global::DG.Tweening.Plugins.Options.NoOptions options, global::DG.Tweening.Tween t, bool isRelative, global::DG.Tweening.Core.DOGetter<ulong> getter, global::DG.Tweening.Core.DOSetter<ulong> setter, float elapsed, ulong startValue, ulong changeValue, float duration, bool usingInversePosition, int newCompletedSteps, global::DG.Tweening.Core.Enums.UpdateNotice updateNotice)
		{
			if (t.loopType == global::DG.Tweening.LoopType.Incremental)
			{
				startValue += changeValue * (uint)(t.isComplete ? (t.completedLoops - 1) : t.completedLoops);
			}
			if (t.isSequenced && t.sequenceParent.loopType == global::DG.Tweening.LoopType.Incremental)
			{
				startValue += changeValue * (uint)((t.loopType != global::DG.Tweening.LoopType.Incremental) ? 1 : t.loops) * (uint)(t.sequenceParent.isComplete ? (t.sequenceParent.completedLoops - 1) : t.sequenceParent.completedLoops);
			}
			setter((ulong)((decimal)startValue + (decimal)changeValue * (decimal)global::DG.Tweening.Core.Easing.EaseManager.Evaluate(t.easeType, t.customEase, elapsed, duration, t.easeOvershootOrAmplitude, t.easePeriod)));
		}
	}
}
