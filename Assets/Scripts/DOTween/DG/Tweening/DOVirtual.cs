namespace DG.Tweening
{
	public static class DOVirtual
	{
		public static global::DG.Tweening.Tweener Float(float from, float to, float duration, global::DG.Tweening.TweenCallback<float> onVirtualUpdate)
		{
			return global::DG.Tweening.DOTween.To(() => from, delegate(float x)
			{
				from = x;
			}, to, duration).OnUpdate(delegate
			{
				onVirtualUpdate(from);
			});
		}

		public static global::DG.Tweening.Tweener Int(int from, int to, float duration, global::DG.Tweening.TweenCallback<int> onVirtualUpdate)
		{
			return global::DG.Tweening.DOTween.To(() => from, delegate(int x)
			{
				from = x;
			}, to, duration).OnUpdate(delegate
			{
				onVirtualUpdate(from);
			});
		}

		public static global::DG.Tweening.Tweener Vector2(global::UnityEngine.Vector2 from, global::UnityEngine.Vector2 to, float duration, global::DG.Tweening.TweenCallback<global::UnityEngine.Vector2> onVirtualUpdate)
		{
			return global::DG.Tweening.DOTween.To(() => from, delegate(global::UnityEngine.Vector2 x)
			{
				from = x;
			}, to, duration).OnUpdate(delegate
			{
				onVirtualUpdate(from);
			});
		}

		public static global::DG.Tweening.Tweener Vector3(global::UnityEngine.Vector3 from, global::UnityEngine.Vector3 to, float duration, global::DG.Tweening.TweenCallback<global::UnityEngine.Vector3> onVirtualUpdate)
		{
			return global::DG.Tweening.DOTween.To(() => from, delegate(global::UnityEngine.Vector3 x)
			{
				from = x;
			}, to, duration).OnUpdate(delegate
			{
				onVirtualUpdate(from);
			});
		}

		public static global::DG.Tweening.Tweener Color(global::UnityEngine.Color from, global::UnityEngine.Color to, float duration, global::DG.Tweening.TweenCallback<global::UnityEngine.Color> onVirtualUpdate)
		{
			return global::DG.Tweening.DOTween.To(() => from, delegate(global::UnityEngine.Color x)
			{
				from = x;
			}, to, duration).OnUpdate(delegate
			{
				onVirtualUpdate(from);
			});
		}

		public static float EasedValue(float from, float to, float lifetimePercentage, global::DG.Tweening.Ease easeType)
		{
			return from + (to - from) * global::DG.Tweening.Core.Easing.EaseManager.Evaluate(easeType, null, lifetimePercentage, 1f, global::DG.Tweening.DOTween.defaultEaseOvershootOrAmplitude, global::DG.Tweening.DOTween.defaultEasePeriod);
		}

		public static float EasedValue(float from, float to, float lifetimePercentage, global::DG.Tweening.Ease easeType, float overshoot)
		{
			return from + (to - from) * global::DG.Tweening.Core.Easing.EaseManager.Evaluate(easeType, null, lifetimePercentage, 1f, overshoot, global::DG.Tweening.DOTween.defaultEasePeriod);
		}

		public static float EasedValue(float from, float to, float lifetimePercentage, global::DG.Tweening.Ease easeType, float amplitude, float period)
		{
			return from + (to - from) * global::DG.Tweening.Core.Easing.EaseManager.Evaluate(easeType, null, lifetimePercentage, 1f, amplitude, period);
		}

		public static float EasedValue(float from, float to, float lifetimePercentage, global::UnityEngine.AnimationCurve easeCurve)
		{
			return from + (to - from) * global::DG.Tweening.Core.Easing.EaseManager.Evaluate(global::DG.Tweening.Ease.INTERNAL_Custom, new global::DG.Tweening.Core.Easing.EaseCurve(easeCurve).Evaluate, lifetimePercentage, 1f, global::DG.Tweening.DOTween.defaultEaseOvershootOrAmplitude, global::DG.Tweening.DOTween.defaultEasePeriod);
		}

		public static global::UnityEngine.Vector3 EasedValue(global::UnityEngine.Vector3 from, global::UnityEngine.Vector3 to, float lifetimePercentage, global::DG.Tweening.Ease easeType)
		{
			return from + (to - from) * global::DG.Tweening.Core.Easing.EaseManager.Evaluate(easeType, null, lifetimePercentage, 1f, global::DG.Tweening.DOTween.defaultEaseOvershootOrAmplitude, global::DG.Tweening.DOTween.defaultEasePeriod);
		}

		public static global::UnityEngine.Vector3 EasedValue(global::UnityEngine.Vector3 from, global::UnityEngine.Vector3 to, float lifetimePercentage, global::DG.Tweening.Ease easeType, float overshoot)
		{
			return from + (to - from) * global::DG.Tweening.Core.Easing.EaseManager.Evaluate(easeType, null, lifetimePercentage, 1f, overshoot, global::DG.Tweening.DOTween.defaultEasePeriod);
		}

		public static global::UnityEngine.Vector3 EasedValue(global::UnityEngine.Vector3 from, global::UnityEngine.Vector3 to, float lifetimePercentage, global::DG.Tweening.Ease easeType, float amplitude, float period)
		{
			return from + (to - from) * global::DG.Tweening.Core.Easing.EaseManager.Evaluate(easeType, null, lifetimePercentage, 1f, amplitude, period);
		}

		public static global::UnityEngine.Vector3 EasedValue(global::UnityEngine.Vector3 from, global::UnityEngine.Vector3 to, float lifetimePercentage, global::UnityEngine.AnimationCurve easeCurve)
		{
			return from + (to - from) * global::DG.Tweening.Core.Easing.EaseManager.Evaluate(global::DG.Tweening.Ease.INTERNAL_Custom, new global::DG.Tweening.Core.Easing.EaseCurve(easeCurve).Evaluate, lifetimePercentage, 1f, global::DG.Tweening.DOTween.defaultEaseOvershootOrAmplitude, global::DG.Tweening.DOTween.defaultEasePeriod);
		}

		public static global::DG.Tweening.Tween DelayedCall(float delay, global::DG.Tweening.TweenCallback callback, bool ignoreTimeScale = true)
		{
			return global::DG.Tweening.DOTween.Sequence().AppendInterval(delay).OnStepComplete(callback)
				.SetUpdate(global::DG.Tweening.UpdateType.Normal, ignoreTimeScale)
				.SetAutoKill(autoKillOnCompletion: true);
		}
	}
}
