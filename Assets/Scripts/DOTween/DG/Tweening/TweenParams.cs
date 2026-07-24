namespace DG.Tweening
{
	public class TweenParams
	{
		public static readonly global::DG.Tweening.TweenParams Params = new global::DG.Tweening.TweenParams();

		internal object id;

		internal string stringId;

		internal int intId = -999;

		internal object target;

		internal global::DG.Tweening.UpdateType updateType;

		internal bool isIndependentUpdate;

		internal global::DG.Tweening.TweenCallback onStart;

		internal global::DG.Tweening.TweenCallback onPlay;

		internal global::DG.Tweening.TweenCallback onRewind;

		internal global::DG.Tweening.TweenCallback onUpdate;

		internal global::DG.Tweening.TweenCallback onStepComplete;

		internal global::DG.Tweening.TweenCallback onComplete;

		internal global::DG.Tweening.TweenCallback onKill;

		internal global::DG.Tweening.TweenCallback<int> onWaypointChange;

		internal bool isRecyclable;

		internal bool isSpeedBased;

		internal bool autoKill;

		internal int loops;

		internal global::DG.Tweening.LoopType loopType;

		internal float delay;

		internal bool isRelative;

		internal global::DG.Tweening.Ease easeType;

		internal global::DG.Tweening.EaseFunction customEase;

		internal float easeOvershootOrAmplitude;

		internal float easePeriod;

		public TweenParams()
		{
			Clear();
		}

		public global::DG.Tweening.TweenParams Clear()
		{
			id = (target = null);
			stringId = null;
			intId = -999;
			updateType = global::DG.Tweening.DOTween.defaultUpdateType;
			isIndependentUpdate = global::DG.Tweening.DOTween.defaultTimeScaleIndependent;
			onStart = (onPlay = (onRewind = (onUpdate = (onStepComplete = (onComplete = (onKill = null))))));
			onWaypointChange = null;
			isRecyclable = global::DG.Tweening.DOTween.defaultRecyclable;
			isSpeedBased = false;
			autoKill = global::DG.Tweening.DOTween.defaultAutoKill;
			loops = 1;
			loopType = global::DG.Tweening.DOTween.defaultLoopType;
			delay = 0f;
			isRelative = false;
			easeType = global::DG.Tweening.Ease.Unset;
			customEase = null;
			easeOvershootOrAmplitude = global::DG.Tweening.DOTween.defaultEaseOvershootOrAmplitude;
			easePeriod = global::DG.Tweening.DOTween.defaultEasePeriod;
			return this;
		}

		public global::DG.Tweening.TweenParams SetAutoKill(bool autoKillOnCompletion = true)
		{
			autoKill = autoKillOnCompletion;
			return this;
		}

		public global::DG.Tweening.TweenParams SetId(object objectId)
		{
			id = objectId;
			return this;
		}

		public global::DG.Tweening.TweenParams SetId(string stringId)
		{
			this.stringId = stringId;
			return this;
		}

		public global::DG.Tweening.TweenParams SetId(int intId)
		{
			this.intId = intId;
			return this;
		}

		public global::DG.Tweening.TweenParams SetTarget(object target)
		{
			this.target = target;
			return this;
		}

		public global::DG.Tweening.TweenParams SetLoops(int loops, global::DG.Tweening.LoopType? loopType = null)
		{
			if (loops < -1)
			{
				loops = -1;
			}
			else if (loops == 0)
			{
				loops = 1;
			}
			this.loops = loops;
			if (loopType.HasValue)
			{
				this.loopType = loopType.Value;
			}
			return this;
		}

		public global::DG.Tweening.TweenParams SetEase(global::DG.Tweening.Ease ease, float? overshootOrAmplitude = null, float? period = null)
		{
			easeType = ease;
			easeOvershootOrAmplitude = (overshootOrAmplitude.HasValue ? overshootOrAmplitude.Value : global::DG.Tweening.DOTween.defaultEaseOvershootOrAmplitude);
			easePeriod = (period.HasValue ? period.Value : global::DG.Tweening.DOTween.defaultEasePeriod);
			customEase = null;
			return this;
		}

		public global::DG.Tweening.TweenParams SetEase(global::UnityEngine.AnimationCurve animCurve)
		{
			easeType = global::DG.Tweening.Ease.INTERNAL_Custom;
			customEase = new global::DG.Tweening.Core.Easing.EaseCurve(animCurve).Evaluate;
			return this;
		}

		public global::DG.Tweening.TweenParams SetEase(global::DG.Tweening.EaseFunction customEase)
		{
			easeType = global::DG.Tweening.Ease.INTERNAL_Custom;
			this.customEase = customEase;
			return this;
		}

		public global::DG.Tweening.TweenParams SetRecyclable(bool recyclable = true)
		{
			isRecyclable = recyclable;
			return this;
		}

		public global::DG.Tweening.TweenParams SetUpdate(bool isIndependentUpdate)
		{
			updateType = global::DG.Tweening.DOTween.defaultUpdateType;
			this.isIndependentUpdate = isIndependentUpdate;
			return this;
		}

		public global::DG.Tweening.TweenParams SetUpdate(global::DG.Tweening.UpdateType updateType, bool isIndependentUpdate = false)
		{
			this.updateType = updateType;
			this.isIndependentUpdate = isIndependentUpdate;
			return this;
		}

		public global::DG.Tweening.TweenParams OnStart(global::DG.Tweening.TweenCallback action)
		{
			onStart = action;
			return this;
		}

		public global::DG.Tweening.TweenParams OnPlay(global::DG.Tweening.TweenCallback action)
		{
			onPlay = action;
			return this;
		}

		public global::DG.Tweening.TweenParams OnRewind(global::DG.Tweening.TweenCallback action)
		{
			onRewind = action;
			return this;
		}

		public global::DG.Tweening.TweenParams OnUpdate(global::DG.Tweening.TweenCallback action)
		{
			onUpdate = action;
			return this;
		}

		public global::DG.Tweening.TweenParams OnStepComplete(global::DG.Tweening.TweenCallback action)
		{
			onStepComplete = action;
			return this;
		}

		public global::DG.Tweening.TweenParams OnComplete(global::DG.Tweening.TweenCallback action)
		{
			onComplete = action;
			return this;
		}

		public global::DG.Tweening.TweenParams OnKill(global::DG.Tweening.TweenCallback action)
		{
			onKill = action;
			return this;
		}

		public global::DG.Tweening.TweenParams OnWaypointChange(global::DG.Tweening.TweenCallback<int> action)
		{
			onWaypointChange = action;
			return this;
		}

		public global::DG.Tweening.TweenParams SetDelay(float delay)
		{
			this.delay = delay;
			return this;
		}

		public global::DG.Tweening.TweenParams SetRelative(bool isRelative = true)
		{
			this.isRelative = isRelative;
			return this;
		}

		public global::DG.Tweening.TweenParams SetSpeedBased(bool isSpeedBased = true)
		{
			this.isSpeedBased = isSpeedBased;
			return this;
		}
	}
}
