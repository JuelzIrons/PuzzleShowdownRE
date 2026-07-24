namespace DG.Tweening.CustomPlugins
{
	public class PureQuaternionPlugin : global::DG.Tweening.Plugins.Core.ABSTweenPlugin<global::UnityEngine.Quaternion, global::UnityEngine.Quaternion, global::DG.Tweening.Plugins.Options.NoOptions>
	{
		private static global::DG.Tweening.CustomPlugins.PureQuaternionPlugin _plug;

		public static global::DG.Tweening.CustomPlugins.PureQuaternionPlugin Plug()
		{
			if (_plug == null)
			{
				_plug = new global::DG.Tweening.CustomPlugins.PureQuaternionPlugin();
			}
			return _plug;
		}

		public override void Reset(global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Quaternion, global::UnityEngine.Quaternion, global::DG.Tweening.Plugins.Options.NoOptions> t)
		{
		}

		public override void SetFrom(global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Quaternion, global::UnityEngine.Quaternion, global::DG.Tweening.Plugins.Options.NoOptions> t, bool isRelative)
		{
			global::UnityEngine.Quaternion endValue = t.endValue;
			t.endValue = t.getter();
			t.startValue = (isRelative ? (t.endValue * endValue) : endValue);
			t.setter(t.startValue);
		}

		public override void SetFrom(global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Quaternion, global::UnityEngine.Quaternion, global::DG.Tweening.Plugins.Options.NoOptions> t, global::UnityEngine.Quaternion fromValue, bool setImmediately, bool isRelative)
		{
			if (isRelative)
			{
				global::UnityEngine.Quaternion quaternion = t.getter();
				t.endValue = quaternion * t.endValue;
				fromValue = quaternion * fromValue;
			}
			t.startValue = fromValue;
			if (setImmediately)
			{
				t.setter(fromValue);
			}
		}

		public override global::UnityEngine.Quaternion ConvertToStartValue(global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Quaternion, global::UnityEngine.Quaternion, global::DG.Tweening.Plugins.Options.NoOptions> t, global::UnityEngine.Quaternion value)
		{
			return value;
		}

		public override void SetRelativeEndValue(global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Quaternion, global::UnityEngine.Quaternion, global::DG.Tweening.Plugins.Options.NoOptions> t)
		{
			t.endValue *= t.startValue;
		}

		public override void SetChangeValue(global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Quaternion, global::UnityEngine.Quaternion, global::DG.Tweening.Plugins.Options.NoOptions> t)
		{
			t.changeValue = t.endValue;
		}

		public override float GetSpeedBasedDuration(global::DG.Tweening.Plugins.Options.NoOptions options, float unitsXSecond, global::UnityEngine.Quaternion changeValue)
		{
			return changeValue.eulerAngles.magnitude / unitsXSecond;
		}

		public override void EvaluateAndApply(global::DG.Tweening.Plugins.Options.NoOptions options, global::DG.Tweening.Tween t, bool isRelative, global::DG.Tweening.Core.DOGetter<global::UnityEngine.Quaternion> getter, global::DG.Tweening.Core.DOSetter<global::UnityEngine.Quaternion> setter, float elapsed, global::UnityEngine.Quaternion startValue, global::UnityEngine.Quaternion changeValue, float duration, bool usingInversePosition, int newCompletedSteps, global::DG.Tweening.Core.Enums.UpdateNotice updateNotice)
		{
			float t2 = global::DG.Tweening.Core.Easing.EaseManager.Evaluate(t.easeType, t.customEase, elapsed, duration, t.easeOvershootOrAmplitude, t.easePeriod);
			setter(global::UnityEngine.Quaternion.Slerp(startValue, changeValue, t2));
		}
	}
}
