namespace DG.Tweening
{
	public static class TweenSettingsExtensions
	{
		public static T SetAutoKill<T>(this T t) where T : global::DG.Tweening.Tween
		{
			if (t == null || !t.active || t.creationLocked)
			{
				return t;
			}
			t.autoKill = true;
			return t;
		}

		public static T SetAutoKill<T>(this T t, bool autoKillOnCompletion) where T : global::DG.Tweening.Tween
		{
			if (t == null || !t.active || t.creationLocked)
			{
				return t;
			}
			t.autoKill = autoKillOnCompletion;
			return t;
		}

		public static T SetId<T>(this T t, object objectId) where T : global::DG.Tweening.Tween
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.id = objectId;
			return t;
		}

		public static T SetId<T>(this T t, string stringId) where T : global::DG.Tweening.Tween
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.stringId = stringId;
			return t;
		}

		public static T SetId<T>(this T t, int intId) where T : global::DG.Tweening.Tween
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.intId = intId;
			return t;
		}

		public static T SetLink<T>(this T t, global::UnityEngine.GameObject gameObject) where T : global::DG.Tweening.Tween
		{
			if (t == null || !t.active || t.isSequenced || gameObject == null)
			{
				return t;
			}
			global::DG.Tweening.Core.TweenManager.AddTweenLink(t, new global::DG.Tweening.Core.TweenLink(gameObject, global::DG.Tweening.LinkBehaviour.KillOnDestroy));
			return t;
		}

		public static T SetLink<T>(this T t, global::UnityEngine.GameObject gameObject, global::DG.Tweening.LinkBehaviour behaviour) where T : global::DG.Tweening.Tween
		{
			if (t == null || !t.active || t.isSequenced || gameObject == null)
			{
				return t;
			}
			global::DG.Tweening.Core.TweenManager.AddTweenLink(t, new global::DG.Tweening.Core.TweenLink(gameObject, behaviour));
			return t;
		}

		public static T SetTarget<T>(this T t, object target) where T : global::DG.Tweening.Tween
		{
			if (t == null || !t.active)
			{
				return t;
			}
			if (global::DG.Tweening.DOTween.debugStoreTargetId)
			{
				global::UnityEngine.Component component = target as global::UnityEngine.Component;
				t.debugTargetId = ((component != null) ? component.name : target.ToString());
			}
			t.target = target;
			return t;
		}

		public static T SetLoops<T>(this T t, int loops) where T : global::DG.Tweening.Tween
		{
			if (t == null || !t.active || t.creationLocked)
			{
				return t;
			}
			if (loops < -1)
			{
				loops = -1;
			}
			else if (loops == 0)
			{
				loops = 1;
			}
			t.loops = loops;
			if (t.tweenType == global::DG.Tweening.TweenType.Tweener)
			{
				if (loops > -1)
				{
					t.fullDuration = t.duration * (float)loops;
				}
				else
				{
					t.fullDuration = float.PositiveInfinity;
				}
			}
			return t;
		}

		public static T SetLoops<T>(this T t, int loops, global::DG.Tweening.LoopType loopType) where T : global::DG.Tweening.Tween
		{
			if (t == null || !t.active || t.creationLocked)
			{
				return t;
			}
			if (loops < -1)
			{
				loops = -1;
			}
			else if (loops == 0)
			{
				loops = 1;
			}
			t.loops = loops;
			t.loopType = loopType;
			if (t.tweenType == global::DG.Tweening.TweenType.Tweener)
			{
				if (loops > -1)
				{
					t.fullDuration = t.duration * (float)loops;
				}
				else
				{
					t.fullDuration = float.PositiveInfinity;
				}
			}
			return t;
		}

		public static T SetEase<T>(this T t, global::DG.Tweening.Ease ease) where T : global::DG.Tweening.Tween
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.easeType = ease;
			if (global::DG.Tweening.Core.Easing.EaseManager.IsFlashEase(ease))
			{
				t.easeOvershootOrAmplitude = (int)t.easeOvershootOrAmplitude;
			}
			t.customEase = null;
			return t;
		}

		public static T SetEase<T>(this T t, global::DG.Tweening.Ease ease, float overshoot) where T : global::DG.Tweening.Tween
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.easeType = ease;
			if (global::DG.Tweening.Core.Easing.EaseManager.IsFlashEase(ease))
			{
				overshoot = (int)overshoot;
			}
			t.easeOvershootOrAmplitude = overshoot;
			t.customEase = null;
			return t;
		}

		public static T SetEase<T>(this T t, global::DG.Tweening.Ease ease, float amplitude, float period) where T : global::DG.Tweening.Tween
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.easeType = ease;
			if (global::DG.Tweening.Core.Easing.EaseManager.IsFlashEase(ease))
			{
				amplitude = (int)amplitude;
			}
			t.easeOvershootOrAmplitude = amplitude;
			t.easePeriod = period;
			t.customEase = null;
			return t;
		}

		public static T SetEase<T>(this T t, global::UnityEngine.AnimationCurve animCurve) where T : global::DG.Tweening.Tween
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.easeType = global::DG.Tweening.Ease.INTERNAL_Custom;
			t.customEase = new global::DG.Tweening.Core.Easing.EaseCurve(animCurve).Evaluate;
			return t;
		}

		public static T SetEase<T>(this T t, global::DG.Tweening.EaseFunction customEase) where T : global::DG.Tweening.Tween
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.easeType = global::DG.Tweening.Ease.INTERNAL_Custom;
			t.customEase = customEase;
			return t;
		}

		public static T SetRecyclable<T>(this T t) where T : global::DG.Tweening.Tween
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.isRecyclable = true;
			return t;
		}

		public static T SetRecyclable<T>(this T t, bool recyclable) where T : global::DG.Tweening.Tween
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.isRecyclable = recyclable;
			return t;
		}

		public static T SetUpdate<T>(this T t, bool isIndependentUpdate) where T : global::DG.Tweening.Tween
		{
			if (t == null || !t.active)
			{
				return t;
			}
			global::DG.Tweening.Core.TweenManager.SetUpdateType(t, global::DG.Tweening.DOTween.defaultUpdateType, isIndependentUpdate);
			return t;
		}

		public static T SetUpdate<T>(this T t, global::DG.Tweening.UpdateType updateType) where T : global::DG.Tweening.Tween
		{
			if (t == null || !t.active)
			{
				return t;
			}
			global::DG.Tweening.Core.TweenManager.SetUpdateType(t, updateType, global::DG.Tweening.DOTween.defaultTimeScaleIndependent);
			return t;
		}

		public static T SetUpdate<T>(this T t, global::DG.Tweening.UpdateType updateType, bool isIndependentUpdate) where T : global::DG.Tweening.Tween
		{
			if (t == null || !t.active)
			{
				return t;
			}
			global::DG.Tweening.Core.TweenManager.SetUpdateType(t, updateType, isIndependentUpdate);
			return t;
		}

		public static T SetInverted<T>(this T t) where T : global::DG.Tweening.Tween
		{
			if (t == null || !t.active || t.creationLocked)
			{
				return t;
			}
			t.isInverted = true;
			return t;
		}

		public static T SetInverted<T>(this T t, bool inverted) where T : global::DG.Tweening.Tween
		{
			if (t == null || !t.active || t.creationLocked)
			{
				return t;
			}
			t.isInverted = inverted;
			return t;
		}

		public static T OnStart<T>(this T t, global::DG.Tweening.TweenCallback action) where T : global::DG.Tweening.Tween
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.onStart = action;
			return t;
		}

		public static T OnPlay<T>(this T t, global::DG.Tweening.TweenCallback action) where T : global::DG.Tweening.Tween
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.onPlay = action;
			return t;
		}

		public static T OnPause<T>(this T t, global::DG.Tweening.TweenCallback action) where T : global::DG.Tweening.Tween
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.onPause = action;
			return t;
		}

		public static T OnRewind<T>(this T t, global::DG.Tweening.TweenCallback action) where T : global::DG.Tweening.Tween
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.onRewind = action;
			return t;
		}

		public static T OnUpdate<T>(this T t, global::DG.Tweening.TweenCallback action) where T : global::DG.Tweening.Tween
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.onUpdate = action;
			return t;
		}

		public static T OnStepComplete<T>(this T t, global::DG.Tweening.TweenCallback action) where T : global::DG.Tweening.Tween
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.onStepComplete = action;
			return t;
		}

		public static T OnComplete<T>(this T t, global::DG.Tweening.TweenCallback action) where T : global::DG.Tweening.Tween
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.onComplete = action;
			return t;
		}

		public static T OnKill<T>(this T t, global::DG.Tweening.TweenCallback action) where T : global::DG.Tweening.Tween
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.onKill = action;
			return t;
		}

		public static T OnWaypointChange<T>(this T t, global::DG.Tweening.TweenCallback<int> action) where T : global::DG.Tweening.Tween
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.onWaypointChange = action;
			return t;
		}

		public static T SetAs<T>(this T t, global::DG.Tweening.Tween asTween) where T : global::DG.Tweening.Tween
		{
			if (t == null || !t.active || t.creationLocked)
			{
				return t;
			}
			t.timeScale = asTween.timeScale;
			t.isBackwards = asTween.isBackwards;
			global::DG.Tweening.Core.TweenManager.SetUpdateType(t, asTween.updateType, asTween.isIndependentUpdate);
			t.id = asTween.id;
			t.stringId = asTween.stringId;
			t.intId = asTween.intId;
			t.onStart = asTween.onStart;
			t.onPlay = asTween.onPlay;
			t.onRewind = asTween.onRewind;
			t.onUpdate = asTween.onUpdate;
			t.onStepComplete = asTween.onStepComplete;
			t.onComplete = asTween.onComplete;
			t.onKill = asTween.onKill;
			t.onWaypointChange = asTween.onWaypointChange;
			t.isRecyclable = asTween.isRecyclable;
			t.isSpeedBased = asTween.isSpeedBased;
			t.autoKill = asTween.autoKill;
			t.loops = asTween.loops;
			t.loopType = asTween.loopType;
			if (t.tweenType == global::DG.Tweening.TweenType.Tweener)
			{
				if (t.loops > -1)
				{
					t.fullDuration = t.duration * (float)t.loops;
				}
				else
				{
					t.fullDuration = float.PositiveInfinity;
				}
			}
			t.delay = asTween.delay;
			t.delayComplete = t.delay <= 0f;
			t.isRelative = asTween.isRelative;
			t.easeType = asTween.easeType;
			t.customEase = asTween.customEase;
			t.easeOvershootOrAmplitude = asTween.easeOvershootOrAmplitude;
			t.easePeriod = asTween.easePeriod;
			return t;
		}

		public static T SetAs<T>(this T t, global::DG.Tweening.TweenParams tweenParams) where T : global::DG.Tweening.Tween
		{
			if (t == null || !t.active || t.creationLocked)
			{
				return t;
			}
			global::DG.Tweening.Core.TweenManager.SetUpdateType(t, tweenParams.updateType, tweenParams.isIndependentUpdate);
			t.id = tweenParams.id;
			t.stringId = tweenParams.stringId;
			t.intId = tweenParams.intId;
			t.onStart = tweenParams.onStart;
			t.onPlay = tweenParams.onPlay;
			t.onRewind = tweenParams.onRewind;
			t.onUpdate = tweenParams.onUpdate;
			t.onStepComplete = tweenParams.onStepComplete;
			t.onComplete = tweenParams.onComplete;
			t.onKill = tweenParams.onKill;
			t.onWaypointChange = tweenParams.onWaypointChange;
			t.isRecyclable = tweenParams.isRecyclable;
			t.isSpeedBased = tweenParams.isSpeedBased;
			t.autoKill = tweenParams.autoKill;
			t.loops = tweenParams.loops;
			t.loopType = tweenParams.loopType;
			if (t.tweenType == global::DG.Tweening.TweenType.Tweener)
			{
				if (t.loops > -1)
				{
					t.fullDuration = t.duration * (float)t.loops;
				}
				else
				{
					t.fullDuration = float.PositiveInfinity;
				}
			}
			t.delay = tweenParams.delay;
			t.delayComplete = t.delay <= 0f;
			t.isRelative = tweenParams.isRelative;
			if (tweenParams.easeType == global::DG.Tweening.Ease.Unset)
			{
				if (t.tweenType == global::DG.Tweening.TweenType.Sequence)
				{
					t.easeType = global::DG.Tweening.Ease.Linear;
				}
				else
				{
					t.easeType = global::DG.Tweening.DOTween.defaultEaseType;
				}
			}
			else
			{
				t.easeType = tweenParams.easeType;
			}
			t.customEase = tweenParams.customEase;
			t.easeOvershootOrAmplitude = tweenParams.easeOvershootOrAmplitude;
			t.easePeriod = tweenParams.easePeriod;
			return t;
		}

		public static global::DG.Tweening.Sequence Append(this global::DG.Tweening.Sequence s, global::DG.Tweening.Tween t)
		{
			if (!ValidateAddToSequence(s, t))
			{
				return s;
			}
			global::DG.Tweening.Sequence.DoInsert(s, t, s.duration);
			return s;
		}

		public static global::DG.Tweening.Sequence Prepend(this global::DG.Tweening.Sequence s, global::DG.Tweening.Tween t)
		{
			if (!ValidateAddToSequence(s, t))
			{
				return s;
			}
			global::DG.Tweening.Sequence.DoPrepend(s, t);
			return s;
		}

		public static global::DG.Tweening.Sequence Join(this global::DG.Tweening.Sequence s, global::DG.Tweening.Tween t)
		{
			if (!ValidateAddToSequence(s, t))
			{
				return s;
			}
			global::DG.Tweening.Sequence.DoInsert(s, t, s.lastTweenInsertTime);
			return s;
		}

		public static global::DG.Tweening.Sequence Insert(this global::DG.Tweening.Sequence s, float atPosition, global::DG.Tweening.Tween t)
		{
			if (!ValidateAddToSequence(s, t))
			{
				return s;
			}
			global::DG.Tweening.Sequence.DoInsert(s, t, atPosition);
			return s;
		}

		public static global::DG.Tweening.Sequence AppendInterval(this global::DG.Tweening.Sequence s, float interval)
		{
			if (!ValidateAddToSequence(s, null, ignoreTween: true))
			{
				return s;
			}
			global::DG.Tweening.Sequence.DoAppendInterval(s, interval);
			return s;
		}

		public static global::DG.Tweening.Sequence PrependInterval(this global::DG.Tweening.Sequence s, float interval)
		{
			if (!ValidateAddToSequence(s, null, ignoreTween: true))
			{
				return s;
			}
			global::DG.Tweening.Sequence.DoPrependInterval(s, interval);
			return s;
		}

		public static global::DG.Tweening.Sequence AppendCallback(this global::DG.Tweening.Sequence s, global::DG.Tweening.TweenCallback callback)
		{
			if (!ValidateAddToSequence(s, null, ignoreTween: true))
			{
				return s;
			}
			if (callback == null)
			{
				return s;
			}
			global::DG.Tweening.Sequence.DoInsertCallback(s, callback, s.duration);
			return s;
		}

		public static global::DG.Tweening.Sequence PrependCallback(this global::DG.Tweening.Sequence s, global::DG.Tweening.TweenCallback callback)
		{
			if (!ValidateAddToSequence(s, null, ignoreTween: true))
			{
				return s;
			}
			if (callback == null)
			{
				return s;
			}
			global::DG.Tweening.Sequence.DoInsertCallback(s, callback, 0f);
			return s;
		}

		public static global::DG.Tweening.Sequence JoinCallback(this global::DG.Tweening.Sequence s, global::DG.Tweening.TweenCallback callback)
		{
			if (!ValidateAddToSequence(s, null, ignoreTween: true))
			{
				return s;
			}
			if (callback == null)
			{
				return s;
			}
			global::DG.Tweening.Sequence.DoInsertCallback(s, callback, s.lastTweenInsertTime);
			return s;
		}

		public static global::DG.Tweening.Sequence InsertCallback(this global::DG.Tweening.Sequence s, float atPosition, global::DG.Tweening.TweenCallback callback)
		{
			if (!ValidateAddToSequence(s, null, ignoreTween: true))
			{
				return s;
			}
			if (callback == null)
			{
				return s;
			}
			global::DG.Tweening.Sequence.DoInsertCallback(s, callback, atPosition);
			return s;
		}

		private static bool ValidateAddToSequence(global::DG.Tweening.Sequence s, global::DG.Tweening.Tween t, bool ignoreTween = false)
		{
			if (s == null)
			{
				global::DG.Tweening.Core.Debugger.Sequence.LogAddToNullSequence();
				return false;
			}
			if (!s.active)
			{
				global::DG.Tweening.Core.Debugger.Sequence.LogAddToInactiveSequence();
				return false;
			}
			if (s.creationLocked)
			{
				global::DG.Tweening.Core.Debugger.Sequence.LogAddToLockedSequence();
				return false;
			}
			if (!ignoreTween)
			{
				if (t == null)
				{
					global::DG.Tweening.Core.Debugger.Sequence.LogAddNullTween();
					return false;
				}
				if (!t.active)
				{
					global::DG.Tweening.Core.Debugger.Sequence.LogAddInactiveTween(t);
					return false;
				}
				if (t.isSequenced)
				{
					global::DG.Tweening.Core.Debugger.Sequence.LogAddAlreadySequencedTween(t);
					return false;
				}
			}
			return true;
		}

		public static T From<T>(this T t) where T : global::DG.Tweening.Tweener
		{
			return t.From(setImmediately: true, isRelative: false);
		}

		public static T From<T>(this T t, bool isRelative) where T : global::DG.Tweening.Tweener
		{
			return t.From(setImmediately: true, isRelative);
		}

		public static T From<T>(this T t, bool setImmediately, bool isRelative) where T : global::DG.Tweening.Tweener
		{
			if (t == null || !t.active || t.creationLocked || !t.isFromAllowed)
			{
				return t;
			}
			t.isFrom = true;
			if (setImmediately)
			{
				t.SetFrom(isRelative && !t.isBlendable);
			}
			else
			{
				t.isRelative = isRelative;
			}
			return t;
		}

		public static global::DG.Tweening.Core.TweenerCore<T1, T2, TPlugOptions> From<T1, T2, TPlugOptions>(this global::DG.Tweening.Core.TweenerCore<T1, T2, TPlugOptions> t, T2 fromValue, bool setImmediately = true, bool isRelative = false) where TPlugOptions : struct, global::DG.Tweening.Plugins.Options.IPlugOptions
		{
			if (t == null || !t.active || t.creationLocked || !t.isFromAllowed)
			{
				return t;
			}
			t.isFrom = true;
			t.SetFrom(fromValue, setImmediately, isRelative);
			return t;
		}

		public static global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Color, global::UnityEngine.Color, global::DG.Tweening.Plugins.Options.ColorOptions> From(this global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Color, global::UnityEngine.Color, global::DG.Tweening.Plugins.Options.ColorOptions> t, float fromAlphaValue, bool setImmediately = true, bool isRelative = false)
		{
			if (t == null || !t.active || t.creationLocked || !t.isFromAllowed)
			{
				return t;
			}
			t.isFrom = true;
			t.SetFrom(new global::UnityEngine.Color(0f, 0f, 0f, fromAlphaValue), setImmediately, isRelative);
			return t;
		}

		public static global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Options.VectorOptions> From(this global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Options.VectorOptions> t, float fromValue, bool setImmediately = true, bool isRelative = false)
		{
			if (t == null || !t.active || t.creationLocked || !t.isFromAllowed)
			{
				return t;
			}
			t.isFrom = true;
			t.SetFrom(new global::UnityEngine.Vector3(fromValue, fromValue, fromValue), setImmediately, isRelative);
			return t;
		}

		public static global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector2, global::UnityEngine.Vector2, global::DG.Tweening.Plugins.CircleOptions> From(this global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector2, global::UnityEngine.Vector2, global::DG.Tweening.Plugins.CircleOptions> t, float fromValueDegrees, bool setImmediately = true, bool isRelative = false)
		{
			if (t == null || !t.active || t.creationLocked || !t.isFromAllowed)
			{
				return t;
			}
			t.isFrom = true;
			t.SetFrom(new global::UnityEngine.Vector2(fromValueDegrees, 0f), setImmediately, isRelative);
			return t;
		}

		public static T SetDelay<T>(this T t, float delay) where T : global::DG.Tweening.Tween
		{
			if (t == null || !t.active || t.creationLocked)
			{
				return t;
			}
			if (t.tweenType == global::DG.Tweening.TweenType.Sequence)
			{
				(t as global::DG.Tweening.Sequence).PrependInterval(delay);
			}
			else
			{
				t.delay = delay;
				t.delayComplete = delay <= 0f;
			}
			return t;
		}

		public static T SetDelay<T>(this T t, float delay, bool asPrependedIntervalIfSequence) where T : global::DG.Tweening.Tween
		{
			if (t == null || !t.active || t.creationLocked)
			{
				return t;
			}
			if (t.tweenType != global::DG.Tweening.TweenType.Sequence || !asPrependedIntervalIfSequence)
			{
				t.delay = delay;
				t.delayComplete = delay <= 0f;
			}
			else
			{
				(t as global::DG.Tweening.Sequence).PrependInterval(delay);
			}
			return t;
		}

		public static T SetRelative<T>(this T t) where T : global::DG.Tweening.Tween
		{
			if (t == null || !t.active || t.creationLocked || t.isFrom || t.isBlendable)
			{
				return t;
			}
			t.isRelative = true;
			return t;
		}

		public static T SetRelative<T>(this T t, bool isRelative) where T : global::DG.Tweening.Tween
		{
			if (t == null || !t.active || t.creationLocked || t.isFrom || t.isBlendable)
			{
				return t;
			}
			t.isRelative = isRelative;
			return t;
		}

		public static T SetSpeedBased<T>(this T t) where T : global::DG.Tweening.Tween
		{
			if (t == null || !t.active || t.creationLocked)
			{
				return t;
			}
			t.isSpeedBased = true;
			return t;
		}

		public static T SetSpeedBased<T>(this T t, bool isSpeedBased) where T : global::DG.Tweening.Tween
		{
			if (t == null || !t.active || t.creationLocked)
			{
				return t;
			}
			t.isSpeedBased = isSpeedBased;
			return t;
		}

		public static global::DG.Tweening.Tweener SetOptions(this global::DG.Tweening.Core.TweenerCore<float, float, global::DG.Tweening.Plugins.Options.FloatOptions> t, bool snapping)
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.plugOptions.snapping = snapping;
			return t;
		}

		public static global::DG.Tweening.Tweener SetOptions(this global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector2, global::UnityEngine.Vector2, global::DG.Tweening.Plugins.Options.VectorOptions> t, bool snapping)
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.plugOptions.snapping = snapping;
			return t;
		}

		public static global::DG.Tweening.Tweener SetOptions(this global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector2, global::UnityEngine.Vector2, global::DG.Tweening.Plugins.Options.VectorOptions> t, global::DG.Tweening.AxisConstraint axisConstraint, bool snapping = false)
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.plugOptions.axisConstraint = axisConstraint;
			t.plugOptions.snapping = snapping;
			return t;
		}

		public static global::DG.Tweening.Tweener SetOptions(this global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Options.VectorOptions> t, bool snapping)
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.plugOptions.snapping = snapping;
			return t;
		}

		public static global::DG.Tweening.Tweener SetOptions(this global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Options.VectorOptions> t, global::DG.Tweening.AxisConstraint axisConstraint, bool snapping = false)
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.plugOptions.axisConstraint = axisConstraint;
			t.plugOptions.snapping = snapping;
			return t;
		}

		public static global::DG.Tweening.Tweener SetOptions(this global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector4, global::UnityEngine.Vector4, global::DG.Tweening.Plugins.Options.VectorOptions> t, bool snapping)
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.plugOptions.snapping = snapping;
			return t;
		}

		public static global::DG.Tweening.Tweener SetOptions(this global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector4, global::UnityEngine.Vector4, global::DG.Tweening.Plugins.Options.VectorOptions> t, global::DG.Tweening.AxisConstraint axisConstraint, bool snapping = false)
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.plugOptions.axisConstraint = axisConstraint;
			t.plugOptions.snapping = snapping;
			return t;
		}

		public static global::DG.Tweening.Tweener SetOptions(this global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Quaternion, global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Options.QuaternionOptions> t, bool useShortest360Route = true)
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.plugOptions.rotateMode = ((!useShortest360Route) ? global::DG.Tweening.RotateMode.FastBeyond360 : global::DG.Tweening.RotateMode.Fast);
			return t;
		}

		public static global::DG.Tweening.Tweener SetOptions(this global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Color, global::UnityEngine.Color, global::DG.Tweening.Plugins.Options.ColorOptions> t, bool alphaOnly)
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.plugOptions.alphaOnly = alphaOnly;
			return t;
		}

		public static global::DG.Tweening.Tweener SetOptions(this global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Rect, global::UnityEngine.Rect, global::DG.Tweening.Plugins.Options.RectOptions> t, bool snapping)
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.plugOptions.snapping = snapping;
			return t;
		}

		public static global::DG.Tweening.Tweener SetOptions(this global::DG.Tweening.Core.TweenerCore<string, string, global::DG.Tweening.Plugins.Options.StringOptions> t, bool richTextEnabled, global::DG.Tweening.ScrambleMode scrambleMode = global::DG.Tweening.ScrambleMode.None, string scrambleChars = null)
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.plugOptions.richTextEnabled = richTextEnabled;
			t.plugOptions.scrambleMode = scrambleMode;
			if (!string.IsNullOrEmpty(scrambleChars))
			{
				if (scrambleChars.Length <= 1)
				{
					scrambleChars += scrambleChars;
				}
				t.plugOptions.scrambledChars = scrambleChars.ToCharArray();
				global::DG.Tweening.Plugins.StringPluginExtensions.ScrambleChars(t.plugOptions.scrambledChars);
			}
			return t;
		}

		public static global::DG.Tweening.Tweener SetOptions(this global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::UnityEngine.Vector3[], global::DG.Tweening.Plugins.Options.Vector3ArrayOptions> t, bool snapping)
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.plugOptions.snapping = snapping;
			return t;
		}

		public static global::DG.Tweening.Tweener SetOptions(this global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::UnityEngine.Vector3[], global::DG.Tweening.Plugins.Options.Vector3ArrayOptions> t, global::DG.Tweening.AxisConstraint axisConstraint, bool snapping = false)
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.plugOptions.axisConstraint = axisConstraint;
			t.plugOptions.snapping = snapping;
			return t;
		}

		public static global::DG.Tweening.Tweener SetOptions(this global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector2, global::UnityEngine.Vector2, global::DG.Tweening.Plugins.CircleOptions> t, float endValueDegrees, bool relativeCenter = true, bool snapping = false)
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.plugOptions.endValueDegrees = endValueDegrees;
			t.plugOptions.relativeCenter = relativeCenter;
			t.plugOptions.snapping = snapping;
			return t;
		}

		public static global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Core.PathCore.Path, global::DG.Tweening.Plugins.Options.PathOptions> SetOptions(this global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Core.PathCore.Path, global::DG.Tweening.Plugins.Options.PathOptions> t, global::DG.Tweening.AxisConstraint lockPosition, global::DG.Tweening.AxisConstraint lockRotation = global::DG.Tweening.AxisConstraint.None)
		{
			return t.SetOptions(closePath: false, lockPosition, lockRotation);
		}

		public static global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Core.PathCore.Path, global::DG.Tweening.Plugins.Options.PathOptions> SetOptions(this global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Core.PathCore.Path, global::DG.Tweening.Plugins.Options.PathOptions> t, bool closePath, global::DG.Tweening.AxisConstraint lockPosition = global::DG.Tweening.AxisConstraint.None, global::DG.Tweening.AxisConstraint lockRotation = global::DG.Tweening.AxisConstraint.None)
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.plugOptions.isClosedPath = closePath;
			t.plugOptions.lockPositionAxis = lockPosition;
			t.plugOptions.lockRotationAxis = lockRotation;
			return t;
		}

		public static global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Core.PathCore.Path, global::DG.Tweening.Plugins.Options.PathOptions> SetLookAt(this global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Core.PathCore.Path, global::DG.Tweening.Plugins.Options.PathOptions> t, global::UnityEngine.Vector3 lookAtPosition, global::UnityEngine.Vector3? forwardDirection = null, global::UnityEngine.Vector3? up = null)
		{
			return t.SetLookAt(global::DG.Tweening.Plugins.Options.OrientType.LookAtPosition, lookAtPosition, null, -1f, forwardDirection, up);
		}

		public static global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Core.PathCore.Path, global::DG.Tweening.Plugins.Options.PathOptions> SetLookAt(this global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Core.PathCore.Path, global::DG.Tweening.Plugins.Options.PathOptions> t, global::UnityEngine.Vector3 lookAtPosition, bool stableZRotation)
		{
			return t.SetLookAt(global::DG.Tweening.Plugins.Options.OrientType.LookAtPosition, lookAtPosition, null, -1f, null, null, stableZRotation);
		}

		public static global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Core.PathCore.Path, global::DG.Tweening.Plugins.Options.PathOptions> SetLookAt(this global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Core.PathCore.Path, global::DG.Tweening.Plugins.Options.PathOptions> t, global::UnityEngine.Transform lookAtTransform, global::UnityEngine.Vector3? forwardDirection = null, global::UnityEngine.Vector3? up = null)
		{
			return t.SetLookAt(global::DG.Tweening.Plugins.Options.OrientType.LookAtTransform, global::UnityEngine.Vector3.zero, lookAtTransform, -1f, forwardDirection, up);
		}

		public static global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Core.PathCore.Path, global::DG.Tweening.Plugins.Options.PathOptions> SetLookAt(this global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Core.PathCore.Path, global::DG.Tweening.Plugins.Options.PathOptions> t, global::UnityEngine.Transform lookAtTransform, bool stableZRotation)
		{
			return t.SetLookAt(global::DG.Tweening.Plugins.Options.OrientType.LookAtTransform, global::UnityEngine.Vector3.zero, lookAtTransform, -1f, null, null, stableZRotation);
		}

		public static global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Core.PathCore.Path, global::DG.Tweening.Plugins.Options.PathOptions> SetLookAt(this global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Core.PathCore.Path, global::DG.Tweening.Plugins.Options.PathOptions> t, float lookAhead, global::UnityEngine.Vector3? forwardDirection = null, global::UnityEngine.Vector3? up = null)
		{
			return t.SetLookAt(global::DG.Tweening.Plugins.Options.OrientType.ToPath, global::UnityEngine.Vector3.zero, null, lookAhead, forwardDirection, up);
		}

		public static global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Core.PathCore.Path, global::DG.Tweening.Plugins.Options.PathOptions> SetLookAt(this global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Core.PathCore.Path, global::DG.Tweening.Plugins.Options.PathOptions> t, float lookAhead, bool stableZRotation)
		{
			return t.SetLookAt(global::DG.Tweening.Plugins.Options.OrientType.ToPath, global::UnityEngine.Vector3.zero, null, lookAhead, null, null, stableZRotation);
		}

		private static global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Core.PathCore.Path, global::DG.Tweening.Plugins.Options.PathOptions> SetLookAt(this global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Core.PathCore.Path, global::DG.Tweening.Plugins.Options.PathOptions> t, global::DG.Tweening.Plugins.Options.OrientType orientType, global::UnityEngine.Vector3 lookAtPosition, global::UnityEngine.Transform lookAtTransform, float lookAhead, global::UnityEngine.Vector3? forwardDirection = null, global::UnityEngine.Vector3? up = null, bool stableZRotation = false)
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.plugOptions.orientType = orientType;
			switch (orientType)
			{
			case global::DG.Tweening.Plugins.Options.OrientType.LookAtPosition:
				t.plugOptions.lookAtPosition = lookAtPosition;
				break;
			case global::DG.Tweening.Plugins.Options.OrientType.LookAtTransform:
				t.plugOptions.lookAtTransform = lookAtTransform;
				break;
			case global::DG.Tweening.Plugins.Options.OrientType.ToPath:
				if (lookAhead < 0.0001f)
				{
					lookAhead = 0.0001f;
				}
				t.plugOptions.lookAhead = lookAhead;
				break;
			}
			t.plugOptions.lookAtPosition = lookAtPosition;
			t.plugOptions.stableZRotation = stableZRotation;
			t.SetPathForwardDirection(forwardDirection, up);
			return t;
		}

		private static void SetPathForwardDirection(this global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Core.PathCore.Path, global::DG.Tweening.Plugins.Options.PathOptions> t, global::UnityEngine.Vector3? forwardDirection = null, global::UnityEngine.Vector3? up = null)
		{
			if (t == null || !t.active)
			{
				return;
			}
			t.plugOptions.hasCustomForwardDirection = (forwardDirection.HasValue && forwardDirection != global::UnityEngine.Vector3.zero) || (up.HasValue && up != global::UnityEngine.Vector3.zero);
			if (t.plugOptions.hasCustomForwardDirection)
			{
				if (forwardDirection == global::UnityEngine.Vector3.zero)
				{
					forwardDirection = global::UnityEngine.Vector3.forward;
				}
				t.plugOptions.forward = global::UnityEngine.Quaternion.LookRotation((!forwardDirection.HasValue) ? global::UnityEngine.Vector3.forward : forwardDirection.Value, (!up.HasValue) ? global::UnityEngine.Vector3.up : up.Value);
			}
		}
	}
}
