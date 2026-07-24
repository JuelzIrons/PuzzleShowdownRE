namespace DG.Tweening.Plugins
{
	internal class Color2Plugin : global::DG.Tweening.Plugins.Core.ABSTweenPlugin<global::DG.Tweening.Color2, global::DG.Tweening.Color2, global::DG.Tweening.Plugins.Options.ColorOptions>
	{
		public override void Reset(global::DG.Tweening.Core.TweenerCore<global::DG.Tweening.Color2, global::DG.Tweening.Color2, global::DG.Tweening.Plugins.Options.ColorOptions> t)
		{
		}

		public override void SetFrom(global::DG.Tweening.Core.TweenerCore<global::DG.Tweening.Color2, global::DG.Tweening.Color2, global::DG.Tweening.Plugins.Options.ColorOptions> t, bool isRelative)
		{
			global::DG.Tweening.Color2 endValue = t.endValue;
			t.endValue = t.getter();
			if (isRelative)
			{
				t.startValue = new global::DG.Tweening.Color2(t.endValue.ca + endValue.ca, t.endValue.cb + endValue.cb);
			}
			else
			{
				t.startValue = new global::DG.Tweening.Color2(endValue.ca, endValue.cb);
			}
			global::DG.Tweening.Color2 pNewValue = t.endValue;
			if (!t.plugOptions.alphaOnly)
			{
				pNewValue = t.startValue;
			}
			else
			{
				pNewValue.ca.a = t.startValue.ca.a;
				pNewValue.cb.a = t.startValue.cb.a;
			}
			t.setter(pNewValue);
		}

		public override void SetFrom(global::DG.Tweening.Core.TweenerCore<global::DG.Tweening.Color2, global::DG.Tweening.Color2, global::DG.Tweening.Plugins.Options.ColorOptions> t, global::DG.Tweening.Color2 fromValue, bool setImmediately, bool isRelative)
		{
			if (isRelative)
			{
				global::DG.Tweening.Color2 color = t.getter();
				t.endValue += color;
				fromValue += color;
			}
			t.startValue = fromValue;
			if (setImmediately)
			{
				global::DG.Tweening.Color2 pNewValue = fromValue;
				if (t.plugOptions.alphaOnly)
				{
					pNewValue = t.getter();
					pNewValue.ca.a = fromValue.ca.a;
					pNewValue.cb.a = fromValue.cb.a;
				}
				t.setter(pNewValue);
			}
		}

		public override global::DG.Tweening.Color2 ConvertToStartValue(global::DG.Tweening.Core.TweenerCore<global::DG.Tweening.Color2, global::DG.Tweening.Color2, global::DG.Tweening.Plugins.Options.ColorOptions> t, global::DG.Tweening.Color2 value)
		{
			return value;
		}

		public override void SetRelativeEndValue(global::DG.Tweening.Core.TweenerCore<global::DG.Tweening.Color2, global::DG.Tweening.Color2, global::DG.Tweening.Plugins.Options.ColorOptions> t)
		{
			t.endValue += t.startValue;
		}

		public override void SetChangeValue(global::DG.Tweening.Core.TweenerCore<global::DG.Tweening.Color2, global::DG.Tweening.Color2, global::DG.Tweening.Plugins.Options.ColorOptions> t)
		{
			t.changeValue = t.endValue - t.startValue;
		}

		public override float GetSpeedBasedDuration(global::DG.Tweening.Plugins.Options.ColorOptions options, float unitsXSecond, global::DG.Tweening.Color2 changeValue)
		{
			return 1f / unitsXSecond;
		}

		public override void EvaluateAndApply(global::DG.Tweening.Plugins.Options.ColorOptions options, global::DG.Tweening.Tween t, bool isRelative, global::DG.Tweening.Core.DOGetter<global::DG.Tweening.Color2> getter, global::DG.Tweening.Core.DOSetter<global::DG.Tweening.Color2> setter, float elapsed, global::DG.Tweening.Color2 startValue, global::DG.Tweening.Color2 changeValue, float duration, bool usingInversePosition, int newCompletedSteps, global::DG.Tweening.Core.Enums.UpdateNotice updateNotice)
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
				startValue.ca.r += changeValue.ca.r * num;
				startValue.ca.g += changeValue.ca.g * num;
				startValue.ca.b += changeValue.ca.b * num;
				startValue.ca.a += changeValue.ca.a * num;
				startValue.cb.r += changeValue.cb.r * num;
				startValue.cb.g += changeValue.cb.g * num;
				startValue.cb.b += changeValue.cb.b * num;
				startValue.cb.a += changeValue.cb.a * num;
				setter(startValue);
			}
			else
			{
				global::DG.Tweening.Color2 pNewValue = getter();
				pNewValue.ca.a = startValue.ca.a + changeValue.ca.a * num;
				pNewValue.cb.a = startValue.cb.a + changeValue.cb.a * num;
				setter(pNewValue);
			}
		}
	}
}
