namespace DG.Tweening.Plugins
{
	public class UintPlugin : global::DG.Tweening.Plugins.Core.ABSTweenPlugin<uint, uint, global::DG.Tweening.Plugins.Options.UintOptions>
	{
		public override void Reset(global::DG.Tweening.Core.TweenerCore<uint, uint, global::DG.Tweening.Plugins.Options.UintOptions> t)
		{
		}

		public override void SetFrom(global::DG.Tweening.Core.TweenerCore<uint, uint, global::DG.Tweening.Plugins.Options.UintOptions> t, bool isRelative)
		{
			uint endValue = t.endValue;
			t.endValue = t.getter();
			t.startValue = (isRelative ? (t.endValue + endValue) : endValue);
			t.setter(t.startValue);
		}

		public override void SetFrom(global::DG.Tweening.Core.TweenerCore<uint, uint, global::DG.Tweening.Plugins.Options.UintOptions> t, uint fromValue, bool setImmediately, bool isRelative)
		{
			if (isRelative)
			{
				uint num = t.getter();
				t.endValue += num;
				fromValue += num;
			}
			t.startValue = fromValue;
			if (setImmediately)
			{
				t.setter(fromValue);
			}
		}

		public override uint ConvertToStartValue(global::DG.Tweening.Core.TweenerCore<uint, uint, global::DG.Tweening.Plugins.Options.UintOptions> t, uint value)
		{
			return value;
		}

		public override void SetRelativeEndValue(global::DG.Tweening.Core.TweenerCore<uint, uint, global::DG.Tweening.Plugins.Options.UintOptions> t)
		{
			t.endValue += t.startValue;
		}

		public override void SetChangeValue(global::DG.Tweening.Core.TweenerCore<uint, uint, global::DG.Tweening.Plugins.Options.UintOptions> t)
		{
			t.plugOptions.isNegativeChangeValue = t.endValue < t.startValue;
			t.changeValue = (t.plugOptions.isNegativeChangeValue ? (t.startValue - t.endValue) : (t.endValue - t.startValue));
		}

		public override float GetSpeedBasedDuration(global::DG.Tweening.Plugins.Options.UintOptions options, float unitsXSecond, uint changeValue)
		{
			float num = (float)changeValue / unitsXSecond;
			if (num < 0f)
			{
				num = 0f - num;
			}
			return num;
		}

		public override void EvaluateAndApply(global::DG.Tweening.Plugins.Options.UintOptions options, global::DG.Tweening.Tween t, bool isRelative, global::DG.Tweening.Core.DOGetter<uint> getter, global::DG.Tweening.Core.DOSetter<uint> setter, float elapsed, uint startValue, uint changeValue, float duration, bool usingInversePosition, int newCompletedSteps, global::DG.Tweening.Core.Enums.UpdateNotice updateNotice)
		{
			uint num;
			if (t.loopType == global::DG.Tweening.LoopType.Incremental)
			{
				num = (uint)(changeValue * (t.isComplete ? (t.completedLoops - 1) : t.completedLoops));
				startValue = ((!options.isNegativeChangeValue) ? (startValue + num) : (startValue - num));
			}
			if (t.isSequenced && t.sequenceParent.loopType == global::DG.Tweening.LoopType.Incremental)
			{
				num = (uint)(changeValue * ((t.loopType != global::DG.Tweening.LoopType.Incremental) ? 1 : t.loops) * (t.sequenceParent.isComplete ? (t.sequenceParent.completedLoops - 1) : t.sequenceParent.completedLoops));
				startValue = ((!options.isNegativeChangeValue) ? (startValue + num) : (startValue - num));
			}
			num = (uint)global::System.Math.Round((float)changeValue * global::DG.Tweening.Core.Easing.EaseManager.Evaluate(t.easeType, t.customEase, elapsed, duration, t.easeOvershootOrAmplitude, t.easePeriod));
			if (options.isNegativeChangeValue)
			{
				setter(startValue - num);
			}
			else
			{
				setter(startValue + num);
			}
		}
	}
}
