namespace DG.Tweening.Plugins
{
	public class FloatPlugin : global::DG.Tweening.Plugins.Core.ABSTweenPlugin<float, float, global::DG.Tweening.Plugins.Options.FloatOptions>
	{
		public override void Reset(global::DG.Tweening.Core.TweenerCore<float, float, global::DG.Tweening.Plugins.Options.FloatOptions> t)
		{
		}

		public override void SetFrom(global::DG.Tweening.Core.TweenerCore<float, float, global::DG.Tweening.Plugins.Options.FloatOptions> t, bool isRelative)
		{
			float endValue = t.endValue;
			t.endValue = t.getter();
			t.startValue = (isRelative ? (t.endValue + endValue) : endValue);
			t.setter((!t.plugOptions.snapping) ? t.startValue : ((float)global::System.Math.Round(t.startValue)));
		}

		public override void SetFrom(global::DG.Tweening.Core.TweenerCore<float, float, global::DG.Tweening.Plugins.Options.FloatOptions> t, float fromValue, bool setImmediately, bool isRelative)
		{
			if (isRelative)
			{
				float num = t.getter();
				t.endValue += num;
				fromValue += num;
			}
			t.startValue = fromValue;
			if (setImmediately)
			{
				t.setter((!t.plugOptions.snapping) ? fromValue : ((float)global::System.Math.Round(fromValue)));
			}
		}

		public override float ConvertToStartValue(global::DG.Tweening.Core.TweenerCore<float, float, global::DG.Tweening.Plugins.Options.FloatOptions> t, float value)
		{
			return value;
		}

		public override void SetRelativeEndValue(global::DG.Tweening.Core.TweenerCore<float, float, global::DG.Tweening.Plugins.Options.FloatOptions> t)
		{
			t.endValue += t.startValue;
		}

		public override void SetChangeValue(global::DG.Tweening.Core.TweenerCore<float, float, global::DG.Tweening.Plugins.Options.FloatOptions> t)
		{
			t.changeValue = t.endValue - t.startValue;
		}

		public override float GetSpeedBasedDuration(global::DG.Tweening.Plugins.Options.FloatOptions options, float unitsXSecond, float changeValue)
		{
			float num = changeValue / unitsXSecond;
			if (num < 0f)
			{
				num = 0f - num;
			}
			return num;
		}

		public override void EvaluateAndApply(global::DG.Tweening.Plugins.Options.FloatOptions options, global::DG.Tweening.Tween t, bool isRelative, global::DG.Tweening.Core.DOGetter<float> getter, global::DG.Tweening.Core.DOSetter<float> setter, float elapsed, float startValue, float changeValue, float duration, bool usingInversePosition, int newCompletedSteps, global::DG.Tweening.Core.Enums.UpdateNotice updateNotice)
		{
			if (t.loopType == global::DG.Tweening.LoopType.Incremental)
			{
				startValue += changeValue * (float)(t.isComplete ? (t.completedLoops - 1) : t.completedLoops);
			}
			if (t.isSequenced && t.sequenceParent.loopType == global::DG.Tweening.LoopType.Incremental)
			{
				startValue += changeValue * (float)((t.loopType != global::DG.Tweening.LoopType.Incremental) ? 1 : t.loops) * (float)(t.sequenceParent.isComplete ? (t.sequenceParent.completedLoops - 1) : t.sequenceParent.completedLoops);
			}
			setter((!options.snapping) ? (startValue + changeValue * global::DG.Tweening.Core.Easing.EaseManager.Evaluate(t.easeType, t.customEase, elapsed, duration, t.easeOvershootOrAmplitude, t.easePeriod)) : ((float)global::System.Math.Round(startValue + changeValue * global::DG.Tweening.Core.Easing.EaseManager.Evaluate(t.easeType, t.customEase, elapsed, duration, t.easeOvershootOrAmplitude, t.easePeriod))));
		}
	}
}
