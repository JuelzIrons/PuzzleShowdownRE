namespace DG.Tweening.Plugins
{
	public class Vector2Plugin : global::DG.Tweening.Plugins.Core.ABSTweenPlugin<global::UnityEngine.Vector2, global::UnityEngine.Vector2, global::DG.Tweening.Plugins.Options.VectorOptions>
	{
		public override void Reset(global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector2, global::UnityEngine.Vector2, global::DG.Tweening.Plugins.Options.VectorOptions> t)
		{
		}

		public override void SetFrom(global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector2, global::UnityEngine.Vector2, global::DG.Tweening.Plugins.Options.VectorOptions> t, bool isRelative)
		{
			global::UnityEngine.Vector2 endValue = t.endValue;
			t.endValue = t.getter();
			t.startValue = (isRelative ? (t.endValue + endValue) : endValue);
			global::UnityEngine.Vector2 pNewValue = t.endValue;
			switch (t.plugOptions.axisConstraint)
			{
			case global::DG.Tweening.AxisConstraint.X:
				pNewValue.x = t.startValue.x;
				break;
			case global::DG.Tweening.AxisConstraint.Y:
				pNewValue.y = t.startValue.y;
				break;
			default:
				pNewValue = t.startValue;
				break;
			}
			if (t.plugOptions.snapping)
			{
				pNewValue.x = (float)global::System.Math.Round(pNewValue.x);
				pNewValue.y = (float)global::System.Math.Round(pNewValue.y);
			}
			t.setter(pNewValue);
		}

		public override void SetFrom(global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector2, global::UnityEngine.Vector2, global::DG.Tweening.Plugins.Options.VectorOptions> t, global::UnityEngine.Vector2 fromValue, bool setImmediately, bool isRelative)
		{
			if (isRelative)
			{
				global::UnityEngine.Vector2 vector = t.getter();
				t.endValue += vector;
				fromValue += vector;
			}
			t.startValue = fromValue;
			if (setImmediately)
			{
				global::UnityEngine.Vector2 pNewValue;
				switch (t.plugOptions.axisConstraint)
				{
				case global::DG.Tweening.AxisConstraint.X:
					pNewValue = t.getter();
					pNewValue.x = fromValue.x;
					break;
				case global::DG.Tweening.AxisConstraint.Y:
					pNewValue = t.getter();
					pNewValue.y = fromValue.y;
					break;
				default:
					pNewValue = fromValue;
					break;
				}
				if (t.plugOptions.snapping)
				{
					pNewValue.x = (float)global::System.Math.Round(pNewValue.x);
					pNewValue.y = (float)global::System.Math.Round(pNewValue.y);
				}
				t.setter(pNewValue);
			}
		}

		public override global::UnityEngine.Vector2 ConvertToStartValue(global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector2, global::UnityEngine.Vector2, global::DG.Tweening.Plugins.Options.VectorOptions> t, global::UnityEngine.Vector2 value)
		{
			return value;
		}

		public override void SetRelativeEndValue(global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector2, global::UnityEngine.Vector2, global::DG.Tweening.Plugins.Options.VectorOptions> t)
		{
			t.endValue += t.startValue;
		}

		public override void SetChangeValue(global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector2, global::UnityEngine.Vector2, global::DG.Tweening.Plugins.Options.VectorOptions> t)
		{
			switch (t.plugOptions.axisConstraint)
			{
			case global::DG.Tweening.AxisConstraint.X:
				t.changeValue = new global::UnityEngine.Vector2(t.endValue.x - t.startValue.x, 0f);
				break;
			case global::DG.Tweening.AxisConstraint.Y:
				t.changeValue = new global::UnityEngine.Vector2(0f, t.endValue.y - t.startValue.y);
				break;
			default:
				t.changeValue = t.endValue - t.startValue;
				break;
			}
		}

		public override float GetSpeedBasedDuration(global::DG.Tweening.Plugins.Options.VectorOptions options, float unitsXSecond, global::UnityEngine.Vector2 changeValue)
		{
			return changeValue.magnitude / unitsXSecond;
		}

		public override void EvaluateAndApply(global::DG.Tweening.Plugins.Options.VectorOptions options, global::DG.Tweening.Tween t, bool isRelative, global::DG.Tweening.Core.DOGetter<global::UnityEngine.Vector2> getter, global::DG.Tweening.Core.DOSetter<global::UnityEngine.Vector2> setter, float elapsed, global::UnityEngine.Vector2 startValue, global::UnityEngine.Vector2 changeValue, float duration, bool usingInversePosition, int newCompletedSteps, global::DG.Tweening.Core.Enums.UpdateNotice updateNotice)
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
			switch (options.axisConstraint)
			{
			case global::DG.Tweening.AxisConstraint.X:
			{
				global::UnityEngine.Vector2 pNewValue = getter();
				pNewValue.x = startValue.x + changeValue.x * num;
				if (options.snapping)
				{
					pNewValue.x = (float)global::System.Math.Round(pNewValue.x);
				}
				setter(pNewValue);
				return;
			}
			case global::DG.Tweening.AxisConstraint.Y:
			{
				global::UnityEngine.Vector2 pNewValue2 = getter();
				pNewValue2.y = startValue.y + changeValue.y * num;
				if (options.snapping)
				{
					pNewValue2.y = (float)global::System.Math.Round(pNewValue2.y);
				}
				setter(pNewValue2);
				return;
			}
			}
			startValue.x += changeValue.x * num;
			startValue.y += changeValue.y * num;
			if (options.snapping)
			{
				startValue.x = (float)global::System.Math.Round(startValue.x);
				startValue.y = (float)global::System.Math.Round(startValue.y);
			}
			setter(startValue);
		}
	}
}
