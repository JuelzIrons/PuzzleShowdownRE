namespace DG.Tweening
{
	public abstract class Tween : global::DG.Tweening.Core.ABSSequentiable
	{
		public float timeScale;

		public bool isBackwards;

		internal bool isInverted;

		public object id;

		public string stringId;

		public int intId = -999;

		public object target;

		internal global::DG.Tweening.UpdateType updateType;

		internal bool isIndependentUpdate;

		public global::DG.Tweening.TweenCallback onPlay;

		public global::DG.Tweening.TweenCallback onPause;

		public global::DG.Tweening.TweenCallback onRewind;

		public global::DG.Tweening.TweenCallback onUpdate;

		public global::DG.Tweening.TweenCallback onStepComplete;

		public global::DG.Tweening.TweenCallback onComplete;

		public global::DG.Tweening.TweenCallback onKill;

		public global::DG.Tweening.TweenCallback<int> onWaypointChange;

		internal bool isFrom;

		internal bool isBlendable;

		internal bool isRecyclable;

		internal bool isSpeedBased;

		internal bool autoKill;

		internal float duration;

		internal int loops;

		internal global::DG.Tweening.LoopType loopType;

		internal float delay;

		internal global::DG.Tweening.Ease easeType;

		internal global::DG.Tweening.EaseFunction customEase;

		public float easeOvershootOrAmplitude;

		public float easePeriod;

		public string debugTargetId;

		internal global::System.Type typeofT1;

		internal global::System.Type typeofT2;

		internal global::System.Type typeofTPlugOptions;

		internal bool isSequenced;

		internal global::DG.Tweening.Sequence sequenceParent;

		internal int activeId = -1;

		internal global::DG.Tweening.Core.Enums.SpecialStartupMode specialStartupMode;

		internal bool creationLocked;

		internal bool startupDone;

		internal float fullDuration;

		internal int completedLoops;

		internal bool isPlaying;

		internal bool isComplete;

		internal float elapsedDelay;

		internal bool delayComplete = true;

		internal int miscInt = -1;

		public bool isRelative { get; internal set; }

		public bool active { get; internal set; }

		public float fullPosition
		{
			get
			{
				return this.Elapsed();
			}
			set
			{
				this.Goto(value, isPlaying);
			}
		}

		public bool hasLoops
		{
			get
			{
				if (loops != -1)
				{
					return loops > 1;
				}
				return true;
			}
		}

		public bool playedOnce { get; private set; }

		public float position { get; internal set; }

		internal virtual void Reset()
		{
			timeScale = 1f;
			isBackwards = false;
			id = null;
			stringId = null;
			intId = -999;
			isIndependentUpdate = false;
			onStart = (onPlay = (onRewind = (onUpdate = (onComplete = (onStepComplete = (onKill = null))))));
			onWaypointChange = null;
			debugTargetId = null;
			target = null;
			isFrom = false;
			isBlendable = false;
			isSpeedBased = false;
			duration = 0f;
			loops = 1;
			delay = 0f;
			isRelative = false;
			customEase = null;
			isSequenced = false;
			sequenceParent = null;
			specialStartupMode = global::DG.Tweening.Core.Enums.SpecialStartupMode.None;
			bool flag = (playedOnce = false);
			creationLocked = (startupDone = flag);
			position = (fullDuration = (completedLoops = 0));
			isPlaying = (isComplete = false);
			elapsedDelay = 0f;
			delayComplete = true;
			miscInt = -1;
		}

		internal abstract bool Validate();

		internal virtual float UpdateDelay(float elapsed)
		{
			return 0f;
		}

		internal abstract bool Startup();

		internal abstract bool ApplyTween(float prevPosition, int prevCompletedLoops, int newCompletedSteps, bool useInversePosition, global::DG.Tweening.Core.Enums.UpdateMode updateMode, global::DG.Tweening.Core.Enums.UpdateNotice updateNotice);

		internal static bool DoGoto(global::DG.Tweening.Tween t, float toPosition, int toCompletedLoops, global::DG.Tweening.Core.Enums.UpdateMode updateMode)
		{
			if (!t.startupDone && !t.Startup())
			{
				return true;
			}
			if (!t.playedOnce && updateMode == global::DG.Tweening.Core.Enums.UpdateMode.Update)
			{
				t.playedOnce = true;
				if (t.onStart != null)
				{
					OnTweenCallback(t.onStart, t);
					if (!t.active)
					{
						return true;
					}
				}
				if (t.onPlay != null)
				{
					OnTweenCallback(t.onPlay, t);
					if (!t.active)
					{
						return true;
					}
				}
			}
			float prevPosition = t.position;
			int num = t.completedLoops;
			t.completedLoops = toCompletedLoops;
			bool flag = t.position <= 0f && num <= 0;
			bool flag2 = t.isComplete;
			if (t.loops != -1)
			{
				t.isComplete = t.completedLoops == t.loops;
			}
			int num2 = 0;
			if (updateMode == global::DG.Tweening.Core.Enums.UpdateMode.Update)
			{
				if (t.isBackwards)
				{
					num2 = ((t.completedLoops < num) ? (num - t.completedLoops) : ((toPosition <= 0f && !flag) ? 1 : 0));
					if (flag2)
					{
						num2--;
					}
				}
				else
				{
					num2 = ((t.completedLoops > num) ? (t.completedLoops - num) : 0);
				}
			}
			else if (t.tweenType == global::DG.Tweening.TweenType.Sequence)
			{
				num2 = num - toCompletedLoops;
				if (num2 < 0)
				{
					num2 = -num2;
				}
			}
			t.position = toPosition;
			if (t.position > t.duration)
			{
				t.position = t.duration;
			}
			else if (t.position <= 0f)
			{
				if (t.completedLoops > 0 || t.isComplete)
				{
					t.position = t.duration;
				}
				else
				{
					t.position = 0f;
				}
			}
			bool flag3 = t.isPlaying;
			if (t.isPlaying)
			{
				if (!t.isBackwards)
				{
					t.isPlaying = !t.isComplete;
				}
				else
				{
					t.isPlaying = t.completedLoops != 0 || !(t.position <= 0f);
				}
			}
			bool useInversePosition = t.hasLoops && t.loopType == global::DG.Tweening.LoopType.Yoyo && ((t.position < t.duration) ? (t.completedLoops % 2 != 0) : (t.completedLoops % 2 == 0));
			global::DG.Tweening.Core.Enums.UpdateNotice updateNotice = ((!flag && ((t.loopType == global::DG.Tweening.LoopType.Restart && t.completedLoops != num && (t.loops == -1 || t.completedLoops < t.loops)) || (t.position <= 0f && t.completedLoops <= 0))) ? global::DG.Tweening.Core.Enums.UpdateNotice.RewindStep : global::DG.Tweening.Core.Enums.UpdateNotice.None);
			if (t.ApplyTween(prevPosition, num, num2, useInversePosition, updateMode, updateNotice))
			{
				return true;
			}
			if (t.onUpdate != null && updateMode != global::DG.Tweening.Core.Enums.UpdateMode.IgnoreOnUpdate)
			{
				OnTweenCallback(t.onUpdate, t);
			}
			if (t.position <= 0f && t.completedLoops <= 0 && !flag && t.onRewind != null)
			{
				OnTweenCallback(t.onRewind, t);
			}
			if (num2 > 0 && updateMode == global::DG.Tweening.Core.Enums.UpdateMode.Update && t.onStepComplete != null)
			{
				for (int i = 0; i < num2; i++)
				{
					OnTweenCallback(t.onStepComplete, t);
					if (!t.active)
					{
						break;
					}
				}
			}
			if (t.isComplete && !flag2 && updateMode != global::DG.Tweening.Core.Enums.UpdateMode.IgnoreOnComplete && t.onComplete != null)
			{
				OnTweenCallback(t.onComplete, t);
			}
			if (!t.isPlaying && flag3 && (!t.isComplete || !t.autoKill) && t.onPause != null)
			{
				OnTweenCallback(t.onPause, t);
			}
			if (t.autoKill)
			{
				return t.isComplete;
			}
			return false;
		}

		internal static bool OnTweenCallback(global::DG.Tweening.TweenCallback callback, global::DG.Tweening.Tween t)
		{
			if (global::DG.Tweening.DOTween.useSafeMode)
			{
				try
				{
					callback();
				}
				catch (global::System.Exception ex)
				{
					if (global::DG.Tweening.Core.Debugger.ShouldLogSafeModeCapturedError())
					{
						global::DG.Tweening.Core.Debugger.LogSafeModeCapturedError($"An error inside a tween callback was taken care of ({ex.TargetSite}) ► {ex.Message}\n\n{ex.StackTrace}\n\n", t);
					}
					global::DG.Tweening.DOTween.safeModeReport.Add(global::DG.Tweening.Core.SafeModeReport.SafeModeReportType.Callback);
					return false;
				}
			}
			else
			{
				callback();
			}
			return true;
		}

		internal static bool OnTweenCallback<T>(global::DG.Tweening.TweenCallback<T> callback, global::DG.Tweening.Tween t, T param)
		{
			if (global::DG.Tweening.DOTween.useSafeMode)
			{
				try
				{
					callback(param);
				}
				catch (global::System.Exception ex)
				{
					if (global::DG.Tweening.Core.Debugger.ShouldLogSafeModeCapturedError())
					{
						global::DG.Tweening.Core.Debugger.LogSafeModeCapturedError($"An error inside a tween callback was taken care of ({ex.TargetSite}) ► {ex.Message}", t);
					}
					global::DG.Tweening.DOTween.safeModeReport.Add(global::DG.Tweening.Core.SafeModeReport.SafeModeReportType.Callback);
					return false;
				}
			}
			else
			{
				callback(param);
			}
			return true;
		}
	}
}
