namespace DG.Tweening.Plugins
{
	public class CirclePlugin : global::DG.Tweening.Plugins.Core.ABSTweenPlugin<global::UnityEngine.Vector2, global::UnityEngine.Vector2, global::DG.Tweening.Plugins.CircleOptions>
	{
		public override void Reset(global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector2, global::UnityEngine.Vector2, global::DG.Tweening.Plugins.CircleOptions> t)
		{
		}

		public override void SetFrom(global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector2, global::UnityEngine.Vector2, global::DG.Tweening.Plugins.CircleOptions> t, bool isRelative)
		{
			if (!t.plugOptions.initialized)
			{
				t.startValue = t.getter();
				t.plugOptions.Initialize(t.startValue, t.endValue);
			}
			float endValueDegrees = t.plugOptions.endValueDegrees;
			t.plugOptions.endValueDegrees = t.plugOptions.startValueDegrees;
			t.plugOptions.startValueDegrees = (isRelative ? (t.plugOptions.endValueDegrees + endValueDegrees) : endValueDegrees);
			t.startValue = GetPositionOnCircle(t.plugOptions, t.plugOptions.startValueDegrees);
			t.setter(t.startValue);
		}

		public override void SetFrom(global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector2, global::UnityEngine.Vector2, global::DG.Tweening.Plugins.CircleOptions> t, global::UnityEngine.Vector2 fromValue, bool setImmediately, bool isRelative)
		{
			if (!t.plugOptions.initialized)
			{
				t.startValue = t.getter();
				t.plugOptions.Initialize(t.startValue, t.endValue);
			}
			float num = fromValue.x;
			if (isRelative)
			{
				float startValueDegrees = t.plugOptions.startValueDegrees;
				t.plugOptions.endValueDegrees += startValueDegrees;
				num += startValueDegrees;
			}
			t.plugOptions.startValueDegrees = num;
			t.startValue = GetPositionOnCircle(t.plugOptions, num);
			if (setImmediately)
			{
				t.setter(t.startValue);
			}
		}

		public static global::DG.Tweening.Plugins.Core.ABSTweenPlugin<global::UnityEngine.Vector2, global::UnityEngine.Vector2, global::DG.Tweening.Plugins.CircleOptions> Get()
		{
			return global::DG.Tweening.Plugins.Core.PluginsManager.GetCustomPlugin<global::DG.Tweening.Plugins.CirclePlugin, global::UnityEngine.Vector2, global::UnityEngine.Vector2, global::DG.Tweening.Plugins.CircleOptions>();
		}

		public override global::UnityEngine.Vector2 ConvertToStartValue(global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector2, global::UnityEngine.Vector2, global::DG.Tweening.Plugins.CircleOptions> t, global::UnityEngine.Vector2 value)
		{
			return value;
		}

		public override void SetRelativeEndValue(global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector2, global::UnityEngine.Vector2, global::DG.Tweening.Plugins.CircleOptions> t)
		{
			if (!t.plugOptions.initialized)
			{
				t.plugOptions.Initialize(t.startValue, t.endValue);
			}
			t.plugOptions.endValueDegrees += t.plugOptions.startValueDegrees;
		}

		public override void SetChangeValue(global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector2, global::UnityEngine.Vector2, global::DG.Tweening.Plugins.CircleOptions> t)
		{
			if (!t.plugOptions.initialized)
			{
				t.plugOptions.Initialize(t.startValue, t.endValue);
			}
			t.changeValue = new global::UnityEngine.Vector2(t.plugOptions.endValueDegrees - t.plugOptions.startValueDegrees, 0f);
		}

		public override float GetSpeedBasedDuration(global::DG.Tweening.Plugins.CircleOptions options, float unitsXSecond, global::UnityEngine.Vector2 changeValue)
		{
			return changeValue.x / unitsXSecond;
		}

		public override void EvaluateAndApply(global::DG.Tweening.Plugins.CircleOptions options, global::DG.Tweening.Tween t, bool isRelative, global::DG.Tweening.Core.DOGetter<global::UnityEngine.Vector2> getter, global::DG.Tweening.Core.DOSetter<global::UnityEngine.Vector2> setter, float elapsed, global::UnityEngine.Vector2 startValue, global::UnityEngine.Vector2 changeValue, float duration, bool usingInversePosition, int newCompletedSteps, global::DG.Tweening.Core.Enums.UpdateNotice updateNotice)
		{
			float num = options.startValueDegrees;
			if (t.loopType == global::DG.Tweening.LoopType.Incremental)
			{
				num += changeValue.x * (float)(t.isComplete ? (t.completedLoops - 1) : t.completedLoops);
			}
			if (t.isSequenced && t.sequenceParent.loopType == global::DG.Tweening.LoopType.Incremental)
			{
				num += changeValue.x * (float)((t.loopType != global::DG.Tweening.LoopType.Incremental) ? 1 : t.loops) * (float)(t.sequenceParent.isComplete ? (t.sequenceParent.completedLoops - 1) : t.sequenceParent.completedLoops);
			}
			float num2 = global::DG.Tweening.Core.Easing.EaseManager.Evaluate(t.easeType, t.customEase, elapsed, duration, t.easeOvershootOrAmplitude, t.easePeriod);
			setter(GetPositionOnCircle(options, num + changeValue.x * num2));
		}

		public global::UnityEngine.Vector2 GetPositionOnCircle(global::DG.Tweening.Plugins.CircleOptions options, float degrees)
		{
			global::UnityEngine.Vector2 pointOnCircle = global::DG.Tweening.Core.DOTweenUtils.GetPointOnCircle(options.center, options.radius, degrees);
			if (options.snapping)
			{
				pointOnCircle.x = global::UnityEngine.Mathf.Round(pointOnCircle.x);
				pointOnCircle.y = global::UnityEngine.Mathf.Round(pointOnCircle.y);
			}
			return pointOnCircle;
		}
	}
}
