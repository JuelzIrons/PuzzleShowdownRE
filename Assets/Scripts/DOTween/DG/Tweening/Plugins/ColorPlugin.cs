namespace DG.Tweening.Plugins
{
	public class ColorPlugin : global::DG.Tweening.Plugins.Core.ABSTweenPlugin<global::UnityEngine.Color, global::UnityEngine.Color, global::DG.Tweening.Plugins.Options.ColorOptions>
	{
		public override void Reset(global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Color, global::UnityEngine.Color, global::DG.Tweening.Plugins.Options.ColorOptions> t)
		{
		}

		public override void SetFrom(global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Color, global::UnityEngine.Color, global::DG.Tweening.Plugins.Options.ColorOptions> t, bool isRelative)
		{
			global::UnityEngine.Color endValue = t.endValue;
			t.endValue = t.getter();
			t.startValue = (isRelative ? (t.endValue + endValue) : endValue);
			global::UnityEngine.Color pNewValue = t.endValue;
			if (!t.plugOptions.alphaOnly)
			{
				pNewValue = t.startValue;
			}
			else
			{
				pNewValue.a = t.startValue.a;
			}
			t.setter(pNewValue);
		}

		public override void SetFrom(global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Color, global::UnityEngine.Color, global::DG.Tweening.Plugins.Options.ColorOptions> t, global::UnityEngine.Color fromValue, bool setImmediately, bool isRelative)
		{
			if (isRelative)
			{
				global::UnityEngine.Color color = t.getter();
				t.endValue += color;
				fromValue += color;
			}
			t.startValue = fromValue;
			if (setImmediately)
			{
				global::UnityEngine.Color pNewValue = fromValue;
				if (t.plugOptions.alphaOnly)
				{
					pNewValue = t.getter();
					pNewValue.a = fromValue.a;
				}
				t.setter(pNewValue);
			}
		}

		public override global::UnityEngine.Color ConvertToStartValue(global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Color, global::UnityEngine.Color, global::DG.Tweening.Plugins.Options.ColorOptions> t, global::UnityEngine.Color value)
		{
			return value;
		}

		public override void SetRelativeEndValue(global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Color, global::UnityEngine.Color, global::DG.Tweening.Plugins.Options.ColorOptions> t)
		{
			t.endValue += t.startValue;
		}

		public override void SetChangeValue(global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Color, global::UnityEngine.Color, global::DG.Tweening.Plugins.Options.ColorOptions> t)
		{
			t.changeValue = t.endValue - t.startValue;
		}

		public override float GetSpeedBasedDuration(global::DG.Tweening.Plugins.Options.ColorOptions options, float unitsXSecond, global::UnityEngine.Color changeValue)
		{
			return 1f / unitsXSecond;
		}

		public override void EvaluateAndApply(global::DG.Tweening.Plugins.Options.ColorOptions options, global::DG.Tweening.Tween t, bool isRelative, global::DG.Tweening.Core.DOGetter<global::UnityEngine.Color> getter, global::DG.Tweening.Core.DOSetter<global::UnityEngine.Color> setter, float elapsed, global::UnityEngine.Color startValue, global::UnityEngine.Color changeValue, float duration, bool usingInversePosition, int newCompletedSteps, global::DG.Tweening.Core.Enums.UpdateNotice updateNotice)
		{
			if (t.loopType == global::DG.Tweening.LoopType.Incremental)
			{
				startValue += changeValue * (t.isComplete ? (t.completedLoops - 1) : t.completedLoops);
			}
			if (t.isSequenced && t.sequenceParent.loopType == global::DG.Tweening.LoopType.Incremental)
			{
				startValue += changeValue * ((t.loopType != global::DG.Tweening.LoopType.Incremental) ? 1 : t.loops) * (t.sequenceParent.isComplete ? (t.sequenceParent.completedLoops - 1) : t.sequenceParent.completedLoops);
			}
			float num = global::DG.Tweening.Core.Easing.EaseManager.Evaluate(t.easeType, t.customEase, elapsed, duration, t.easeOvershootOrAmplitude, t.easePeriod);
			if (!options.alphaOnly)
			{
				startValue.r += changeValue.r * num;
				startValue.g += changeValue.g * num;
				startValue.b += changeValue.b * num;
				startValue.a += changeValue.a * num;
				setter(startValue);
			}
			else
			{
				global::UnityEngine.Color pNewValue = getter();
				pNewValue.a = startValue.a + changeValue.a * num;
				setter(pNewValue);
			}
		}
	}
}
