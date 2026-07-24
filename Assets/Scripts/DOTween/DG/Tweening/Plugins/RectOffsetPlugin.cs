namespace DG.Tweening.Plugins
{
	public class RectOffsetPlugin : global::DG.Tweening.Plugins.Core.ABSTweenPlugin<global::UnityEngine.RectOffset, global::UnityEngine.RectOffset, global::DG.Tweening.Plugins.Options.NoOptions>
	{
		private static global::UnityEngine.RectOffset _r = new global::UnityEngine.RectOffset();

		public override void Reset(global::DG.Tweening.Core.TweenerCore<global::UnityEngine.RectOffset, global::UnityEngine.RectOffset, global::DG.Tweening.Plugins.Options.NoOptions> t)
		{
			t.startValue = (t.endValue = (t.changeValue = null));
		}

		public override void SetFrom(global::DG.Tweening.Core.TweenerCore<global::UnityEngine.RectOffset, global::UnityEngine.RectOffset, global::DG.Tweening.Plugins.Options.NoOptions> t, bool isRelative)
		{
			global::UnityEngine.RectOffset endValue = t.endValue;
			t.endValue = t.getter();
			t.startValue = endValue;
			if (isRelative)
			{
				t.startValue.left += t.endValue.left;
				t.startValue.right += t.endValue.right;
				t.startValue.top += t.endValue.top;
				t.startValue.bottom += t.endValue.bottom;
			}
			t.setter(t.startValue);
		}

		public override void SetFrom(global::DG.Tweening.Core.TweenerCore<global::UnityEngine.RectOffset, global::UnityEngine.RectOffset, global::DG.Tweening.Plugins.Options.NoOptions> t, global::UnityEngine.RectOffset fromValue, bool setImmediately, bool isRelative)
		{
			if (isRelative)
			{
				global::UnityEngine.RectOffset rectOffset = t.getter();
				t.endValue.left += rectOffset.left;
				t.endValue.right += rectOffset.right;
				t.endValue.top += rectOffset.top;
				t.endValue.bottom += rectOffset.bottom;
				fromValue.left += rectOffset.left;
				fromValue.right += rectOffset.right;
				fromValue.top += rectOffset.top;
				fromValue.bottom += rectOffset.bottom;
			}
			t.startValue = fromValue;
			if (setImmediately)
			{
				t.setter(fromValue);
			}
		}

		public override global::UnityEngine.RectOffset ConvertToStartValue(global::DG.Tweening.Core.TweenerCore<global::UnityEngine.RectOffset, global::UnityEngine.RectOffset, global::DG.Tweening.Plugins.Options.NoOptions> t, global::UnityEngine.RectOffset value)
		{
			return new global::UnityEngine.RectOffset(value.left, value.right, value.top, value.bottom);
		}

		public override void SetRelativeEndValue(global::DG.Tweening.Core.TweenerCore<global::UnityEngine.RectOffset, global::UnityEngine.RectOffset, global::DG.Tweening.Plugins.Options.NoOptions> t)
		{
			t.endValue.left += t.startValue.left;
			t.endValue.right += t.startValue.right;
			t.endValue.top += t.startValue.top;
			t.endValue.bottom += t.startValue.bottom;
		}

		public override void SetChangeValue(global::DG.Tweening.Core.TweenerCore<global::UnityEngine.RectOffset, global::UnityEngine.RectOffset, global::DG.Tweening.Plugins.Options.NoOptions> t)
		{
			t.changeValue = new global::UnityEngine.RectOffset(t.endValue.left - t.startValue.left, t.endValue.right - t.startValue.right, t.endValue.top - t.startValue.top, t.endValue.bottom - t.startValue.bottom);
		}

		public override float GetSpeedBasedDuration(global::DG.Tweening.Plugins.Options.NoOptions options, float unitsXSecond, global::UnityEngine.RectOffset changeValue)
		{
			float num = changeValue.right;
			if (num < 0f)
			{
				num = 0f - num;
			}
			float num2 = changeValue.bottom;
			if (num2 < 0f)
			{
				num2 = 0f - num2;
			}
			return (float)global::System.Math.Sqrt(num * num + num2 * num2) / unitsXSecond;
		}

		public override void EvaluateAndApply(global::DG.Tweening.Plugins.Options.NoOptions options, global::DG.Tweening.Tween t, bool isRelative, global::DG.Tweening.Core.DOGetter<global::UnityEngine.RectOffset> getter, global::DG.Tweening.Core.DOSetter<global::UnityEngine.RectOffset> setter, float elapsed, global::UnityEngine.RectOffset startValue, global::UnityEngine.RectOffset changeValue, float duration, bool usingInversePosition, int newCompletedSteps, global::DG.Tweening.Core.Enums.UpdateNotice updateNotice)
		{
			_r.left = startValue.left;
			_r.right = startValue.right;
			_r.top = startValue.top;
			_r.bottom = startValue.bottom;
			if (t.loopType == global::DG.Tweening.LoopType.Incremental)
			{
				int num = (t.isComplete ? (t.completedLoops - 1) : t.completedLoops);
				_r.left += changeValue.left * num;
				_r.right += changeValue.right * num;
				_r.top += changeValue.top * num;
				_r.bottom += changeValue.bottom * num;
			}
			if (t.isSequenced && t.sequenceParent.loopType == global::DG.Tweening.LoopType.Incremental)
			{
				int num2 = ((t.loopType != global::DG.Tweening.LoopType.Incremental) ? 1 : t.loops) * (t.sequenceParent.isComplete ? (t.sequenceParent.completedLoops - 1) : t.sequenceParent.completedLoops);
				_r.left += changeValue.left * num2;
				_r.right += changeValue.right * num2;
				_r.top += changeValue.top * num2;
				_r.bottom += changeValue.bottom * num2;
			}
			float num3 = global::DG.Tweening.Core.Easing.EaseManager.Evaluate(t.easeType, t.customEase, elapsed, duration, t.easeOvershootOrAmplitude, t.easePeriod);
			setter(new global::UnityEngine.RectOffset((int)global::System.Math.Round((float)_r.left + (float)changeValue.left * num3), (int)global::System.Math.Round((float)_r.right + (float)changeValue.right * num3), (int)global::System.Math.Round((float)_r.top + (float)changeValue.top * num3), (int)global::System.Math.Round((float)_r.bottom + (float)changeValue.bottom * num3)));
		}
	}
}
