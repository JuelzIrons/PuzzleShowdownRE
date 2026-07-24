namespace DG.Tweening.Plugins
{
	public class Vector3ArrayPlugin : global::DG.Tweening.Plugins.Core.ABSTweenPlugin<global::UnityEngine.Vector3, global::UnityEngine.Vector3[], global::DG.Tweening.Plugins.Options.Vector3ArrayOptions>
	{
		public override void Reset(global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::UnityEngine.Vector3[], global::DG.Tweening.Plugins.Options.Vector3ArrayOptions> t)
		{
			t.startValue = (t.endValue = (t.changeValue = null));
		}

		public override void SetFrom(global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::UnityEngine.Vector3[], global::DG.Tweening.Plugins.Options.Vector3ArrayOptions> t, bool isRelative)
		{
		}

		public override void SetFrom(global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::UnityEngine.Vector3[], global::DG.Tweening.Plugins.Options.Vector3ArrayOptions> t, global::UnityEngine.Vector3[] fromValue, bool setImmediately, bool isRelative)
		{
		}

		public override global::UnityEngine.Vector3[] ConvertToStartValue(global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::UnityEngine.Vector3[], global::DG.Tweening.Plugins.Options.Vector3ArrayOptions> t, global::UnityEngine.Vector3 value)
		{
			int num = t.endValue.Length;
			global::UnityEngine.Vector3[] array = new global::UnityEngine.Vector3[num];
			for (int i = 0; i < num; i++)
			{
				if (i == 0)
				{
					array[i] = value;
				}
				else
				{
					array[i] = t.endValue[i - 1];
				}
			}
			return array;
		}

		public override void SetRelativeEndValue(global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::UnityEngine.Vector3[], global::DG.Tweening.Plugins.Options.Vector3ArrayOptions> t)
		{
			int num = t.endValue.Length;
			for (int i = 0; i < num; i++)
			{
				if (i > 0)
				{
					t.startValue[i] = t.endValue[i - 1];
				}
				t.endValue[i] = t.startValue[i] + t.endValue[i];
			}
		}

		public override void SetChangeValue(global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::UnityEngine.Vector3[], global::DG.Tweening.Plugins.Options.Vector3ArrayOptions> t)
		{
			int num = t.endValue.Length;
			t.changeValue = new global::UnityEngine.Vector3[num];
			for (int i = 0; i < num; i++)
			{
				t.changeValue[i] = t.endValue[i] - t.startValue[i];
			}
		}

		public override float GetSpeedBasedDuration(global::DG.Tweening.Plugins.Options.Vector3ArrayOptions options, float unitsXSecond, global::UnityEngine.Vector3[] changeValue)
		{
			float num = 0f;
			int num2 = changeValue.Length;
			for (int i = 0; i < num2; i++)
			{
				float num3 = changeValue[i].magnitude / options.durations[i];
				options.durations[i] = num3;
				num += num3;
			}
			return num;
		}

		public override void EvaluateAndApply(global::DG.Tweening.Plugins.Options.Vector3ArrayOptions options, global::DG.Tweening.Tween t, bool isRelative, global::DG.Tweening.Core.DOGetter<global::UnityEngine.Vector3> getter, global::DG.Tweening.Core.DOSetter<global::UnityEngine.Vector3> setter, float elapsed, global::UnityEngine.Vector3[] startValue, global::UnityEngine.Vector3[] changeValue, float duration, bool usingInversePosition, int newCompletedSteps, global::DG.Tweening.Core.Enums.UpdateNotice updateNotice)
		{
			global::UnityEngine.Vector3 vector = global::UnityEngine.Vector3.zero;
			if (t.loopType == global::DG.Tweening.LoopType.Incremental)
			{
				int num = (t.isComplete ? (t.completedLoops - 1) : t.completedLoops);
				if (num > 0)
				{
					int num2 = startValue.Length - 1;
					vector = (startValue[num2] + changeValue[num2] - startValue[0]) * num;
				}
			}
			if (t.isSequenced && t.sequenceParent.loopType == global::DG.Tweening.LoopType.Incremental)
			{
				int num3 = ((t.loopType != global::DG.Tweening.LoopType.Incremental) ? 1 : t.loops) * (t.sequenceParent.isComplete ? (t.sequenceParent.completedLoops - 1) : t.sequenceParent.completedLoops);
				if (num3 > 0)
				{
					int num4 = startValue.Length - 1;
					vector += (startValue[num4] + changeValue[num4] - startValue[0]) * num3;
				}
			}
			int num5 = 0;
			float num6 = 0f;
			float num7 = 0f;
			int num8 = options.durations.Length;
			float num9 = 0f;
			for (int i = 0; i < num8; i++)
			{
				num7 = options.durations[i];
				num9 += num7;
				if (elapsed > num9)
				{
					num6 += num7;
					continue;
				}
				num5 = i;
				num6 = elapsed - num6;
				break;
			}
			float num10 = global::DG.Tweening.Core.Easing.EaseManager.Evaluate(t.easeType, t.customEase, num6, num7, t.easeOvershootOrAmplitude, t.easePeriod);
			global::UnityEngine.Vector3 pNewValue = default(global::UnityEngine.Vector3);
			switch (options.axisConstraint)
			{
			case global::DG.Tweening.AxisConstraint.X:
				pNewValue = getter();
				pNewValue.x = startValue[num5].x + vector.x + changeValue[num5].x * num10;
				if (options.snapping)
				{
					pNewValue.x = (float)global::System.Math.Round(pNewValue.x);
				}
				setter(pNewValue);
				return;
			case global::DG.Tweening.AxisConstraint.Y:
				pNewValue = getter();
				pNewValue.y = startValue[num5].y + vector.y + changeValue[num5].y * num10;
				if (options.snapping)
				{
					pNewValue.y = (float)global::System.Math.Round(pNewValue.y);
				}
				setter(pNewValue);
				return;
			case global::DG.Tweening.AxisConstraint.Z:
				pNewValue = getter();
				pNewValue.z = startValue[num5].z + vector.z + changeValue[num5].z * num10;
				if (options.snapping)
				{
					pNewValue.z = (float)global::System.Math.Round(pNewValue.z);
				}
				setter(pNewValue);
				return;
			}
			pNewValue.x = startValue[num5].x + vector.x + changeValue[num5].x * num10;
			pNewValue.y = startValue[num5].y + vector.y + changeValue[num5].y * num10;
			pNewValue.z = startValue[num5].z + vector.z + changeValue[num5].z * num10;
			if (options.snapping)
			{
				pNewValue.x = (float)global::System.Math.Round(pNewValue.x);
				pNewValue.y = (float)global::System.Math.Round(pNewValue.y);
				pNewValue.z = (float)global::System.Math.Round(pNewValue.z);
			}
			setter(pNewValue);
		}
	}
}
