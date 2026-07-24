namespace DG.Tweening.Plugins
{
	public class QuaternionPlugin : global::DG.Tweening.Plugins.Core.ABSTweenPlugin<global::UnityEngine.Quaternion, global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Options.QuaternionOptions>
	{
		public override void Reset(global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Quaternion, global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Options.QuaternionOptions> t)
		{
		}

		public override void SetFrom(global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Quaternion, global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Options.QuaternionOptions> t, bool isRelative)
		{
			global::UnityEngine.Vector3 endValue = t.endValue;
			t.endValue = t.getter().eulerAngles;
			if (t.plugOptions.rotateMode == global::DG.Tweening.RotateMode.Fast && !t.isRelative)
			{
				t.startValue = GetEulerValForCalculations(t, endValue, t.endValue);
			}
			else if (t.plugOptions.rotateMode == global::DG.Tweening.RotateMode.FastBeyond360)
			{
				t.startValue = GetEulerValForCalculations(t, t.endValue + endValue, t.endValue);
			}
			else
			{
				global::UnityEngine.Quaternion quaternion = t.getter();
				if (t.plugOptions.rotateMode == global::DG.Tweening.RotateMode.WorldAxisAdd)
				{
					t.startValue = (quaternion * global::UnityEngine.Quaternion.Inverse(quaternion) * global::UnityEngine.Quaternion.Euler(endValue) * quaternion).eulerAngles;
				}
				else
				{
					t.startValue = (quaternion * global::UnityEngine.Quaternion.Euler(endValue)).eulerAngles;
				}
				t.endValue = -endValue;
			}
			t.setter(global::UnityEngine.Quaternion.Euler(t.startValue));
		}

		public override void SetFrom(global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Quaternion, global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Options.QuaternionOptions> t, global::UnityEngine.Vector3 fromValue, bool setImmediately, bool isRelative)
		{
			if (isRelative)
			{
				global::UnityEngine.Vector3 eulerAngles = t.getter().eulerAngles;
				t.endValue += eulerAngles;
				fromValue += eulerAngles;
			}
			t.startValue = GetEulerValForCalculations(t, fromValue, t.endValue);
			if (setImmediately)
			{
				t.setter(global::UnityEngine.Quaternion.Euler(fromValue));
			}
		}

		public override global::UnityEngine.Vector3 ConvertToStartValue(global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Quaternion, global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Options.QuaternionOptions> t, global::UnityEngine.Quaternion value)
		{
			return value.eulerAngles;
		}

		public override void SetRelativeEndValue(global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Quaternion, global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Options.QuaternionOptions> t)
		{
			t.endValue += t.startValue;
		}

		public override void SetChangeValue(global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Quaternion, global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Options.QuaternionOptions> t)
		{
			global::UnityEngine.Vector3 vector = (t.isFrom ? t.endValue : GetEulerValForCalculations(t, t.endValue, t.startValue));
			global::UnityEngine.Vector3 startValue = t.startValue;
			if (t.plugOptions.rotateMode == global::DG.Tweening.RotateMode.Fast && !t.isRelative)
			{
				if (vector.x > 360f || vector.x < 360f)
				{
					vector.x %= 360f;
				}
				if (vector.y > 360f || vector.y < 360f)
				{
					vector.y %= 360f;
				}
				if (vector.z > 360f || vector.z < 360f)
				{
					vector.z %= 360f;
				}
				global::UnityEngine.Vector3 changeValue = vector - startValue;
				float num = ((changeValue.x > 0f) ? changeValue.x : (0f - changeValue.x));
				if (num > 180f)
				{
					changeValue.x = ((changeValue.x > 0f) ? (0f - (360f - num)) : (360f - num));
				}
				num = ((changeValue.y > 0f) ? changeValue.y : (0f - changeValue.y));
				if (num > 180f)
				{
					changeValue.y = ((changeValue.y > 0f) ? (0f - (360f - num)) : (360f - num));
				}
				num = ((changeValue.z > 0f) ? changeValue.z : (0f - changeValue.z));
				if (num > 180f)
				{
					changeValue.z = ((changeValue.z > 0f) ? (0f - (360f - num)) : (360f - num));
				}
				t.changeValue = changeValue;
			}
			else if (t.plugOptions.rotateMode == global::DG.Tweening.RotateMode.FastBeyond360 || t.isRelative)
			{
				t.changeValue = vector - startValue;
			}
			else
			{
				t.changeValue = vector;
			}
		}

		public override float GetSpeedBasedDuration(global::DG.Tweening.Plugins.Options.QuaternionOptions options, float unitsXSecond, global::UnityEngine.Vector3 changeValue)
		{
			return changeValue.magnitude / unitsXSecond;
		}

		public override void EvaluateAndApply(global::DG.Tweening.Plugins.Options.QuaternionOptions options, global::DG.Tweening.Tween t, bool isRelative, global::DG.Tweening.Core.DOGetter<global::UnityEngine.Quaternion> getter, global::DG.Tweening.Core.DOSetter<global::UnityEngine.Quaternion> setter, float elapsed, global::UnityEngine.Vector3 startValue, global::UnityEngine.Vector3 changeValue, float duration, bool usingInversePosition, int newCompletedSteps, global::DG.Tweening.Core.Enums.UpdateNotice updateNotice)
		{
			if (options.dynamicLookAt)
			{
				global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Quaternion, global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Options.QuaternionOptions> tweenerCore = (global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Quaternion, global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Options.QuaternionOptions>)t;
				tweenerCore.endValue = options.dynamicLookAtWorldPosition;
				global::DG.Tweening.Plugins.Core.SpecialPluginsUtils.SetLookAt(tweenerCore);
				SetChangeValue(tweenerCore);
				changeValue = tweenerCore.changeValue;
			}
			global::UnityEngine.Vector3 euler = startValue;
			if (t.loopType == global::DG.Tweening.LoopType.Incremental)
			{
				euler += changeValue * (t.isComplete ? (t.completedLoops - 1) : t.completedLoops);
			}
			if (t.isSequenced && t.sequenceParent.loopType == global::DG.Tweening.LoopType.Incremental)
			{
				euler += changeValue * ((t.loopType != global::DG.Tweening.LoopType.Incremental) ? 1 : t.loops) * (t.sequenceParent.isComplete ? (t.sequenceParent.completedLoops - 1) : t.sequenceParent.completedLoops);
			}
			float num = global::DG.Tweening.Core.Easing.EaseManager.Evaluate(t.easeType, t.customEase, elapsed, duration, t.easeOvershootOrAmplitude, t.easePeriod);
			global::DG.Tweening.RotateMode rotateMode = options.rotateMode;
			if ((uint)(rotateMode - 2) <= 1u)
			{
				global::UnityEngine.Quaternion quaternion = global::UnityEngine.Quaternion.Euler(startValue);
				euler.x = changeValue.x * num;
				euler.y = changeValue.y * num;
				euler.z = changeValue.z * num;
				if (options.rotateMode == global::DG.Tweening.RotateMode.WorldAxisAdd)
				{
					setter(quaternion * global::UnityEngine.Quaternion.Inverse(quaternion) * global::UnityEngine.Quaternion.Euler(euler) * quaternion);
				}
				else
				{
					setter(quaternion * global::UnityEngine.Quaternion.Euler(euler));
				}
			}
			else
			{
				euler.x += changeValue.x * num;
				euler.y += changeValue.y * num;
				euler.z += changeValue.z * num;
				setter(global::UnityEngine.Quaternion.Euler(euler));
			}
		}

		private global::UnityEngine.Vector3 GetEulerValForCalculations(global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Quaternion, global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Options.QuaternionOptions> t, global::UnityEngine.Vector3 val, global::UnityEngine.Vector3 counterVal)
		{
			if (t.isRelative)
			{
				return val;
			}
			global::DG.Tweening.RotateMode rotateMode = t.plugOptions.rotateMode;
			if ((uint)(rotateMode - 2) <= 1u)
			{
				return val;
			}
			global::UnityEngine.Vector3 result = FlipEulerAngles(val);
			bool flag = global::UnityEngine.Mathf.Approximately(counterVal.x, val.x);
			bool flag2 = global::UnityEngine.Mathf.Approximately(counterVal.x, result.x);
			bool flag3 = global::UnityEngine.Mathf.Approximately(counterVal.y, val.y);
			bool flag4 = global::UnityEngine.Mathf.Approximately(counterVal.y, result.y);
			bool flag5 = global::UnityEngine.Mathf.Approximately(counterVal.z, val.z);
			bool flag6 = global::UnityEngine.Mathf.Approximately(counterVal.z, result.z);
			bool flag7 = (flag && (flag3 || flag5)) || (flag3 && flag5);
			bool flag8 = (!flag7 && flag2 && (flag4 || flag6)) || (flag4 && flag6);
			if (!flag7 && !flag8)
			{
				return val;
			}
			int num = 0;
			num = ((!flag7) ? (flag2 ? ((!flag4) ? 1 : 2) : 0) : (flag ? ((!flag3) ? 1 : 2) : 0));
			bool flag9 = false;
			switch (num)
			{
			case 0:
				flag9 = !global::UnityEngine.Mathf.Approximately(counterVal.y, val.y) || !global::UnityEngine.Mathf.Approximately(counterVal.z, val.z);
				break;
			case 1:
				flag9 = !global::UnityEngine.Mathf.Approximately(counterVal.x, val.x) || !global::UnityEngine.Mathf.Approximately(counterVal.z, val.z);
				break;
			case 2:
				flag9 = !global::UnityEngine.Mathf.Approximately(counterVal.x, val.x) || !global::UnityEngine.Mathf.Approximately(counterVal.y, val.y);
				break;
			}
			if (!flag9)
			{
				return val;
			}
			return result;
		}

		private global::UnityEngine.Vector3 FlipEulerAngles(global::UnityEngine.Vector3 euler)
		{
			return new global::UnityEngine.Vector3(180f - euler.x, euler.y + 180f, euler.z + 180f);
		}
	}
}
