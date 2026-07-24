namespace DG.Tweening.Plugins
{
	public class RectPlugin : global::DG.Tweening.Plugins.Core.ABSTweenPlugin<global::UnityEngine.Rect, global::UnityEngine.Rect, global::DG.Tweening.Plugins.Options.RectOptions>
	{
		public override void Reset(global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Rect, global::UnityEngine.Rect, global::DG.Tweening.Plugins.Options.RectOptions> t)
		{
		}

		public override void SetFrom(global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Rect, global::UnityEngine.Rect, global::DG.Tweening.Plugins.Options.RectOptions> t, bool isRelative)
		{
			global::UnityEngine.Rect endValue = t.endValue;
			t.endValue = t.getter();
			t.startValue = endValue;
			if (isRelative)
			{
				t.startValue.x += t.endValue.x;
				t.startValue.y += t.endValue.y;
				t.startValue.width += t.endValue.width;
				t.startValue.height += t.endValue.height;
			}
			global::UnityEngine.Rect startValue = t.startValue;
			if (t.plugOptions.snapping)
			{
				startValue.x = (float)global::System.Math.Round(startValue.x);
				startValue.y = (float)global::System.Math.Round(startValue.y);
				startValue.width = (float)global::System.Math.Round(startValue.width);
				startValue.height = (float)global::System.Math.Round(startValue.height);
			}
			t.setter(startValue);
		}

		public override void SetFrom(global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Rect, global::UnityEngine.Rect, global::DG.Tweening.Plugins.Options.RectOptions> t, global::UnityEngine.Rect fromValue, bool setImmediately, bool isRelative)
		{
			if (isRelative)
			{
				global::UnityEngine.Rect rect = t.getter();
				t.endValue.x += rect.x;
				t.endValue.y += rect.y;
				t.endValue.width += rect.width;
				t.endValue.height += rect.height;
				fromValue.x += rect.x;
				fromValue.y += rect.y;
				fromValue.width += rect.width;
				fromValue.height += rect.height;
			}
			t.startValue = fromValue;
			if (setImmediately)
			{
				global::UnityEngine.Rect pNewValue = fromValue;
				if (t.plugOptions.snapping)
				{
					pNewValue.x = (float)global::System.Math.Round(pNewValue.x);
					pNewValue.y = (float)global::System.Math.Round(pNewValue.y);
					pNewValue.width = (float)global::System.Math.Round(pNewValue.width);
					pNewValue.height = (float)global::System.Math.Round(pNewValue.height);
				}
				t.setter(pNewValue);
			}
		}

		public override global::UnityEngine.Rect ConvertToStartValue(global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Rect, global::UnityEngine.Rect, global::DG.Tweening.Plugins.Options.RectOptions> t, global::UnityEngine.Rect value)
		{
			return value;
		}

		public override void SetRelativeEndValue(global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Rect, global::UnityEngine.Rect, global::DG.Tweening.Plugins.Options.RectOptions> t)
		{
			t.endValue.x += t.startValue.x;
			t.endValue.y += t.startValue.y;
			t.endValue.width += t.startValue.width;
			t.endValue.height += t.startValue.height;
		}

		public override void SetChangeValue(global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Rect, global::UnityEngine.Rect, global::DG.Tweening.Plugins.Options.RectOptions> t)
		{
			t.changeValue = new global::UnityEngine.Rect(t.endValue.x - t.startValue.x, t.endValue.y - t.startValue.y, t.endValue.width - t.startValue.width, t.endValue.height - t.startValue.height);
		}

		public override float GetSpeedBasedDuration(global::DG.Tweening.Plugins.Options.RectOptions options, float unitsXSecond, global::UnityEngine.Rect changeValue)
		{
			float width = changeValue.width;
			float height = changeValue.height;
			return (float)global::System.Math.Sqrt(width * width + height * height) / unitsXSecond;
		}

		public override void EvaluateAndApply(global::DG.Tweening.Plugins.Options.RectOptions options, global::DG.Tweening.Tween t, bool isRelative, global::DG.Tweening.Core.DOGetter<global::UnityEngine.Rect> getter, global::DG.Tweening.Core.DOSetter<global::UnityEngine.Rect> setter, float elapsed, global::UnityEngine.Rect startValue, global::UnityEngine.Rect changeValue, float duration, bool usingInversePosition, int newCompletedSteps, global::DG.Tweening.Core.Enums.UpdateNotice updateNotice)
		{
			if (t.loopType == global::DG.Tweening.LoopType.Incremental)
			{
				int num = (t.isComplete ? (t.completedLoops - 1) : t.completedLoops);
				startValue.x += changeValue.x * (float)num;
				startValue.y += changeValue.y * (float)num;
				startValue.width += changeValue.width * (float)num;
				startValue.height += changeValue.height * (float)num;
			}
			if (t.isSequenced && t.sequenceParent.loopType == global::DG.Tweening.LoopType.Incremental)
			{
				int num2 = ((t.loopType != global::DG.Tweening.LoopType.Incremental) ? 1 : t.loops) * (t.sequenceParent.isComplete ? (t.sequenceParent.completedLoops - 1) : t.sequenceParent.completedLoops);
				startValue.x += changeValue.x * (float)num2;
				startValue.y += changeValue.y * (float)num2;
				startValue.width += changeValue.width * (float)num2;
				startValue.height += changeValue.height * (float)num2;
			}
			float num3 = global::DG.Tweening.Core.Easing.EaseManager.Evaluate(t.easeType, t.customEase, elapsed, duration, t.easeOvershootOrAmplitude, t.easePeriod);
			startValue.x += changeValue.x * num3;
			startValue.y += changeValue.y * num3;
			startValue.width += changeValue.width * num3;
			startValue.height += changeValue.height * num3;
			if (options.snapping)
			{
				startValue.x = (float)global::System.Math.Round(startValue.x);
				startValue.y = (float)global::System.Math.Round(startValue.y);
				startValue.width = (float)global::System.Math.Round(startValue.width);
				startValue.height = (float)global::System.Math.Round(startValue.height);
			}
			setter(startValue);
		}
	}
}
