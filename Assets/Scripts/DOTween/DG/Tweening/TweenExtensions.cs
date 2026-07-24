namespace DG.Tweening
{
	public static class TweenExtensions
	{
		public static void Complete(this global::DG.Tweening.Tween t)
		{
			t.Complete(withCallbacks: false);
		}

		public static void Complete(this global::DG.Tweening.Tween t, bool withCallbacks)
		{
			if (t == null)
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 1)
				{
					global::DG.Tweening.Core.Debugger.LogNullTween(t);
				}
			}
			else if (!t.active)
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 1)
				{
					global::DG.Tweening.Core.Debugger.LogInvalidTween(t);
				}
			}
			else if (t.isSequenced)
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 1)
				{
					global::DG.Tweening.Core.Debugger.LogNestedTween(t);
				}
			}
			else
			{
				global::DG.Tweening.Core.TweenManager.Complete(t, modifyActiveLists: true, (!withCallbacks) ? global::DG.Tweening.Core.Enums.UpdateMode.Goto : global::DG.Tweening.Core.Enums.UpdateMode.Update);
			}
		}

		public static T Done<T>(this T t) where T : global::DG.Tweening.Tween
		{
			if (t == null)
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 1)
				{
					global::DG.Tweening.Core.Debugger.LogNullTween(t);
				}
				return t;
			}
			if (!t.active)
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 1)
				{
					global::DG.Tweening.Core.Debugger.LogInvalidTween(t);
				}
				return t;
			}
			if (t.isSequenced)
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 1)
				{
					global::DG.Tweening.Core.Debugger.LogNestedTween(t);
				}
				return t;
			}
			if (t.duration <= 0f)
			{
				global::DG.Tweening.Core.TweenManager.Complete(t);
			}
			return t;
		}

		public static void Flip(this global::DG.Tweening.Tween t)
		{
			if (t == null)
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 1)
				{
					global::DG.Tweening.Core.Debugger.LogNullTween(t);
				}
			}
			else if (!t.active)
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 1)
				{
					global::DG.Tweening.Core.Debugger.LogInvalidTween(t);
				}
			}
			else if (t.isSequenced)
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 1)
				{
					global::DG.Tweening.Core.Debugger.LogNestedTween(t);
				}
			}
			else
			{
				global::DG.Tweening.Core.TweenManager.Flip(t);
			}
		}

		public static void ForceInit(this global::DG.Tweening.Tween t)
		{
			if (t == null)
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 1)
				{
					global::DG.Tweening.Core.Debugger.LogNullTween(t);
				}
			}
			else if (!t.active)
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 1)
				{
					global::DG.Tweening.Core.Debugger.LogInvalidTween(t);
				}
			}
			else if (t.isSequenced)
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 1)
				{
					global::DG.Tweening.Core.Debugger.LogNestedTween(t);
				}
			}
			else
			{
				global::DG.Tweening.Core.TweenManager.ForceInit(t);
			}
		}

		public static void Goto(this global::DG.Tweening.Tween t, float to, bool andPlay = false)
		{
			DoGoto(t, to, andPlay, withCallbacks: false);
		}

		public static void GotoWithCallbacks(this global::DG.Tweening.Tween t, float to, bool andPlay = false)
		{
			DoGoto(t, to, andPlay, withCallbacks: true);
		}

		private static void DoGoto(global::DG.Tweening.Tween t, float to, bool andPlay, bool withCallbacks)
		{
			if (t == null)
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 1)
				{
					global::DG.Tweening.Core.Debugger.LogNullTween(t);
				}
				return;
			}
			if (!t.active)
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 1)
				{
					global::DG.Tweening.Core.Debugger.LogInvalidTween(t);
				}
				return;
			}
			if (t.isSequenced)
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 1)
				{
					global::DG.Tweening.Core.Debugger.LogNestedTween(t);
				}
				return;
			}
			if (to < 0f)
			{
				to = 0f;
			}
			if (!t.startupDone)
			{
				global::DG.Tweening.Core.TweenManager.ForceInit(t);
			}
			global::DG.Tweening.Core.TweenManager.Goto(t, to, andPlay, (!withCallbacks) ? global::DG.Tweening.Core.Enums.UpdateMode.Goto : global::DG.Tweening.Core.Enums.UpdateMode.Update);
		}

		public static void Kill(this global::DG.Tweening.Tween t, bool complete = false)
		{
			if (!global::DG.Tweening.DOTween.initialized || t == null || !t.active)
			{
				return;
			}
			if (t.isSequenced)
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 1)
				{
					global::DG.Tweening.Core.Debugger.LogNestedTween(t);
				}
				return;
			}
			if (complete)
			{
				global::DG.Tweening.Core.TweenManager.Complete(t);
				if (t.autoKill && t.loops >= 0)
				{
					return;
				}
			}
			if (global::DG.Tweening.Core.TweenManager.isUpdateLoop)
			{
				t.active = false;
			}
			else
			{
				global::DG.Tweening.Core.TweenManager.Despawn(t);
			}
		}

		public static void ManualUpdate(this global::DG.Tweening.Tween t, float deltaTime, float unscaledDeltaTime)
		{
			if (t == null)
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 1)
				{
					global::DG.Tweening.Core.Debugger.LogNullTween(t);
				}
			}
			else if (!t.active)
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 1)
				{
					global::DG.Tweening.Core.Debugger.LogInvalidTween(t);
				}
			}
			else if (t.isSequenced)
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 1)
				{
					global::DG.Tweening.Core.Debugger.LogNestedTween(t);
				}
			}
			else
			{
				global::DG.Tweening.Core.TweenManager.Update(t, deltaTime, unscaledDeltaTime, isSingleTweenManualUpdate: true);
			}
		}

		public static T Pause<T>(this T t) where T : global::DG.Tweening.Tween
		{
			if (t == null)
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 1)
				{
					global::DG.Tweening.Core.Debugger.LogNullTween(t);
				}
				return t;
			}
			if (!t.active)
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 1)
				{
					global::DG.Tweening.Core.Debugger.LogInvalidTween(t);
				}
				return t;
			}
			if (t.isSequenced)
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 1)
				{
					global::DG.Tweening.Core.Debugger.LogNestedTween(t);
				}
				return t;
			}
			global::DG.Tweening.Core.TweenManager.Pause(t);
			return t;
		}

		public static T Play<T>(this T t) where T : global::DG.Tweening.Tween
		{
			if (t == null)
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 1)
				{
					global::DG.Tweening.Core.Debugger.LogNullTween(t);
				}
				return t;
			}
			if (!t.active)
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 1)
				{
					global::DG.Tweening.Core.Debugger.LogInvalidTween(t);
				}
				return t;
			}
			if (t.isSequenced)
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 1)
				{
					global::DG.Tweening.Core.Debugger.LogNestedTween(t);
				}
				return t;
			}
			global::DG.Tweening.Core.TweenManager.Play(t);
			return t;
		}

		public static void PlayBackwards(this global::DG.Tweening.Tween t)
		{
			if (t == null)
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 1)
				{
					global::DG.Tweening.Core.Debugger.LogNullTween(t);
				}
			}
			else if (!t.active)
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 1)
				{
					global::DG.Tweening.Core.Debugger.LogInvalidTween(t);
				}
			}
			else if (t.isSequenced)
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 1)
				{
					global::DG.Tweening.Core.Debugger.LogNestedTween(t);
				}
			}
			else
			{
				global::DG.Tweening.Core.TweenManager.PlayBackwards(t);
			}
		}

		public static void PlayForward(this global::DG.Tweening.Tween t)
		{
			if (t == null)
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 1)
				{
					global::DG.Tweening.Core.Debugger.LogNullTween(t);
				}
			}
			else if (!t.active)
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 1)
				{
					global::DG.Tweening.Core.Debugger.LogInvalidTween(t);
				}
			}
			else if (t.isSequenced)
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 1)
				{
					global::DG.Tweening.Core.Debugger.LogNestedTween(t);
				}
			}
			else
			{
				global::DG.Tweening.Core.TweenManager.PlayForward(t);
			}
		}

		public static void Restart(this global::DG.Tweening.Tween t, bool includeDelay = true, float changeDelayTo = -1f)
		{
			if (t == null)
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 1)
				{
					global::DG.Tweening.Core.Debugger.LogNullTween(t);
				}
			}
			else if (!t.active)
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 1)
				{
					global::DG.Tweening.Core.Debugger.LogInvalidTween(t);
				}
			}
			else if (t.isSequenced)
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 1)
				{
					global::DG.Tweening.Core.Debugger.LogNestedTween(t);
				}
			}
			else
			{
				global::DG.Tweening.Core.TweenManager.Restart(t, includeDelay, changeDelayTo);
			}
		}

		public static void Rewind(this global::DG.Tweening.Tween t, bool includeDelay = true)
		{
			if (t == null)
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 1)
				{
					global::DG.Tweening.Core.Debugger.LogNullTween(t);
				}
			}
			else if (!t.active)
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 1)
				{
					global::DG.Tweening.Core.Debugger.LogInvalidTween(t);
				}
			}
			else if (t.isSequenced)
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 1)
				{
					global::DG.Tweening.Core.Debugger.LogNestedTween(t);
				}
			}
			else
			{
				global::DG.Tweening.Core.TweenManager.Rewind(t, includeDelay);
			}
		}

		public static void SmoothRewind(this global::DG.Tweening.Tween t)
		{
			if (t == null)
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 1)
				{
					global::DG.Tweening.Core.Debugger.LogNullTween(t);
				}
			}
			else if (!t.active)
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 1)
				{
					global::DG.Tweening.Core.Debugger.LogInvalidTween(t);
				}
			}
			else if (t.isSequenced)
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 1)
				{
					global::DG.Tweening.Core.Debugger.LogNestedTween(t);
				}
			}
			else
			{
				global::DG.Tweening.Core.TweenManager.SmoothRewind(t);
			}
		}

		public static void TogglePause(this global::DG.Tweening.Tween t)
		{
			if (t == null)
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 1)
				{
					global::DG.Tweening.Core.Debugger.LogNullTween(t);
				}
			}
			else if (!t.active)
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 1)
				{
					global::DG.Tweening.Core.Debugger.LogInvalidTween(t);
				}
			}
			else if (t.isSequenced)
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 1)
				{
					global::DG.Tweening.Core.Debugger.LogNestedTween(t);
				}
			}
			else
			{
				global::DG.Tweening.Core.TweenManager.TogglePause(t);
			}
		}

		public static void GotoWaypoint(this global::DG.Tweening.Tween t, int waypointIndex, bool andPlay = false)
		{
			if (t == null)
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 1)
				{
					global::DG.Tweening.Core.Debugger.LogNullTween(t);
				}
				return;
			}
			if (!t.active)
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 1)
				{
					global::DG.Tweening.Core.Debugger.LogInvalidTween(t);
				}
				return;
			}
			if (t.isSequenced)
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 1)
				{
					global::DG.Tweening.Core.Debugger.LogNestedTween(t);
				}
				return;
			}
			if (!(t is global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Core.PathCore.Path, global::DG.Tweening.Plugins.Options.PathOptions> tweenerCore))
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 1)
				{
					global::DG.Tweening.Core.Debugger.LogNonPathTween(t);
				}
				return;
			}
			if (!t.startupDone)
			{
				global::DG.Tweening.Core.TweenManager.ForceInit(t);
			}
			if (waypointIndex < 0)
			{
				waypointIndex = 0;
			}
			else if (waypointIndex > tweenerCore.changeValue.wps.Length - 1)
			{
				waypointIndex = tweenerCore.changeValue.wps.Length - 1;
			}
			float num = 0f;
			for (int i = 0; i < waypointIndex + 1; i++)
			{
				num += tweenerCore.changeValue.wpLengths[i];
			}
			float num2 = num / tweenerCore.changeValue.length;
			if (t.hasLoops && t.loopType == global::DG.Tweening.LoopType.Yoyo && ((t.position < t.duration) ? ((byte)(t.completedLoops % 2) != 0) : (t.completedLoops % 2 == 0)))
			{
				num2 = 1f - num2;
			}
			float to = (float)(t.isComplete ? (t.completedLoops - 1) : t.completedLoops) * t.duration + num2 * t.duration;
			global::DG.Tweening.Core.TweenManager.Goto(t, to, andPlay);
		}

		public static global::UnityEngine.YieldInstruction WaitForCompletion(this global::DG.Tweening.Tween t)
		{
			if (!t.active)
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 0)
				{
					global::DG.Tweening.Core.Debugger.LogInvalidTween(t);
				}
				return null;
			}
			return global::DG.Tweening.DOTween.instance.StartCoroutine(global::DG.Tweening.DOTween.instance.WaitForCompletion(t));
		}

		public static global::UnityEngine.YieldInstruction WaitForRewind(this global::DG.Tweening.Tween t)
		{
			if (!t.active)
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 0)
				{
					global::DG.Tweening.Core.Debugger.LogInvalidTween(t);
				}
				return null;
			}
			return global::DG.Tweening.DOTween.instance.StartCoroutine(global::DG.Tweening.DOTween.instance.WaitForRewind(t));
		}

		public static global::UnityEngine.YieldInstruction WaitForKill(this global::DG.Tweening.Tween t)
		{
			if (!t.active)
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 0)
				{
					global::DG.Tweening.Core.Debugger.LogInvalidTween(t);
				}
				return null;
			}
			return global::DG.Tweening.DOTween.instance.StartCoroutine(global::DG.Tweening.DOTween.instance.WaitForKill(t));
		}

		public static global::UnityEngine.YieldInstruction WaitForElapsedLoops(this global::DG.Tweening.Tween t, int elapsedLoops)
		{
			if (!t.active)
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 0)
				{
					global::DG.Tweening.Core.Debugger.LogInvalidTween(t);
				}
				return null;
			}
			return global::DG.Tweening.DOTween.instance.StartCoroutine(global::DG.Tweening.DOTween.instance.WaitForElapsedLoops(t, elapsedLoops));
		}

		public static global::UnityEngine.YieldInstruction WaitForPosition(this global::DG.Tweening.Tween t, float position)
		{
			if (!t.active)
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 0)
				{
					global::DG.Tweening.Core.Debugger.LogInvalidTween(t);
				}
				return null;
			}
			return global::DG.Tweening.DOTween.instance.StartCoroutine(global::DG.Tweening.DOTween.instance.WaitForPosition(t, position));
		}

		public static global::UnityEngine.Coroutine WaitForStart(this global::DG.Tweening.Tween t)
		{
			if (!t.active)
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 0)
				{
					global::DG.Tweening.Core.Debugger.LogInvalidTween(t);
				}
				return null;
			}
			return global::DG.Tweening.DOTween.instance.StartCoroutine(global::DG.Tweening.DOTween.instance.WaitForStart(t));
		}

		public static int CompletedLoops(this global::DG.Tweening.Tween t)
		{
			if (!t.active)
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 0)
				{
					global::DG.Tweening.Core.Debugger.LogInvalidTween(t);
				}
				return 0;
			}
			return t.completedLoops;
		}

		public static float Delay(this global::DG.Tweening.Tween t)
		{
			if (!t.active)
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 0)
				{
					global::DG.Tweening.Core.Debugger.LogInvalidTween(t);
				}
				return 0f;
			}
			return t.delay;
		}

		public static float ElapsedDelay(this global::DG.Tweening.Tween t)
		{
			if (!t.active)
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 0)
				{
					global::DG.Tweening.Core.Debugger.LogInvalidTween(t);
				}
				return 0f;
			}
			return t.elapsedDelay;
		}

		public static float Duration(this global::DG.Tweening.Tween t, bool includeLoops = true)
		{
			if (!t.active)
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 0)
				{
					global::DG.Tweening.Core.Debugger.LogInvalidTween(t);
				}
				return 0f;
			}
			if (includeLoops)
			{
				if (t.loops != -1)
				{
					return t.duration * (float)t.loops;
				}
				return float.PositiveInfinity;
			}
			return t.duration;
		}

		public static float Elapsed(this global::DG.Tweening.Tween t, bool includeLoops = true)
		{
			if (!t.active)
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 0)
				{
					global::DG.Tweening.Core.Debugger.LogInvalidTween(t);
				}
				return 0f;
			}
			if (includeLoops)
			{
				return (float)((t.position >= t.duration) ? (t.completedLoops - 1) : t.completedLoops) * t.duration + t.position;
			}
			return t.position;
		}

		public static float ElapsedPercentage(this global::DG.Tweening.Tween t, bool includeLoops = true)
		{
			if (!t.active)
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 0)
				{
					global::DG.Tweening.Core.Debugger.LogInvalidTween(t);
				}
				return 0f;
			}
			if (includeLoops)
			{
				if (t.fullDuration <= 0f)
				{
					return 0f;
				}
				return ((float)((t.position >= t.duration) ? (t.completedLoops - 1) : t.completedLoops) * t.duration + t.position) / t.fullDuration;
			}
			return t.position / t.duration;
		}

		public static float ElapsedDirectionalPercentage(this global::DG.Tweening.Tween t)
		{
			if (!t.active)
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 0)
				{
					global::DG.Tweening.Core.Debugger.LogInvalidTween(t);
				}
				return 0f;
			}
			float num = t.position / t.duration;
			if (t.completedLoops <= 0 || !t.hasLoops || t.loopType != global::DG.Tweening.LoopType.Yoyo || ((t.isComplete || t.completedLoops % 2 == 0) && (!t.isComplete || t.completedLoops % 2 != 0)))
			{
				return num;
			}
			return 1f - num;
		}

		public static bool IsActive(this global::DG.Tweening.Tween t)
		{
			return t?.active ?? false;
		}

		public static bool IsBackwards(this global::DG.Tweening.Tween t)
		{
			if (!t.active)
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 0)
				{
					global::DG.Tweening.Core.Debugger.LogInvalidTween(t);
				}
				return false;
			}
			return t.isBackwards;
		}

		public static bool IsLoopingOrExecutingBackwards(this global::DG.Tweening.Tween t)
		{
			if (!t.active)
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 0)
				{
					global::DG.Tweening.Core.Debugger.LogInvalidTween(t);
				}
				return false;
			}
			if (t.isBackwards)
			{
				if (t.completedLoops >= 1 && t.loopType == global::DG.Tweening.LoopType.Yoyo)
				{
					return t.completedLoops % 2 == 0;
				}
				return true;
			}
			if (t.completedLoops >= 1 && t.loopType == global::DG.Tweening.LoopType.Yoyo)
			{
				return t.completedLoops % 2 != 0;
			}
			return false;
		}

		public static bool IsComplete(this global::DG.Tweening.Tween t)
		{
			if (!t.active)
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 0)
				{
					global::DG.Tweening.Core.Debugger.LogInvalidTween(t);
				}
				return false;
			}
			return t.isComplete;
		}

		public static bool IsInitialized(this global::DG.Tweening.Tween t)
		{
			if (!t.active)
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 0)
				{
					global::DG.Tweening.Core.Debugger.LogInvalidTween(t);
				}
				return false;
			}
			return t.startupDone;
		}

		public static bool IsPlaying(this global::DG.Tweening.Tween t)
		{
			if (!t.active)
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 0)
				{
					global::DG.Tweening.Core.Debugger.LogInvalidTween(t);
				}
				return false;
			}
			return t.isPlaying;
		}

		public static int Loops(this global::DG.Tweening.Tween t)
		{
			if (!t.active)
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 0)
				{
					global::DG.Tweening.Core.Debugger.LogInvalidTween(t);
				}
				return 0;
			}
			return t.loops;
		}

		public static global::UnityEngine.Vector3 PathGetPoint(this global::DG.Tweening.Tween t, float pathPercentage)
		{
			if (pathPercentage > 1f)
			{
				pathPercentage = 1f;
			}
			else if (pathPercentage < 0f)
			{
				pathPercentage = 0f;
			}
			if (t == null)
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 1)
				{
					global::DG.Tweening.Core.Debugger.LogNullTween(t);
				}
				return global::UnityEngine.Vector3.zero;
			}
			if (!t.active)
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 1)
				{
					global::DG.Tweening.Core.Debugger.LogInvalidTween(t);
				}
				return global::UnityEngine.Vector3.zero;
			}
			if (t.isSequenced)
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 1)
				{
					global::DG.Tweening.Core.Debugger.LogNestedTween(t);
				}
				return global::UnityEngine.Vector3.zero;
			}
			if (!(t is global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Core.PathCore.Path, global::DG.Tweening.Plugins.Options.PathOptions> tweenerCore))
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 1)
				{
					global::DG.Tweening.Core.Debugger.LogNonPathTween(t);
				}
				return global::UnityEngine.Vector3.zero;
			}
			if (!tweenerCore.endValue.isFinalized)
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 1)
				{
					global::DG.Tweening.Core.Debugger.LogWarning("The path is not finalized yet", t);
				}
				return global::UnityEngine.Vector3.zero;
			}
			return tweenerCore.endValue.GetPoint(pathPercentage, convertToConstantPerc: true);
		}

		public static global::UnityEngine.Vector3[] PathGetDrawPoints(this global::DG.Tweening.Tween t, int subdivisionsXSegment = 10)
		{
			if (t == null)
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 1)
				{
					global::DG.Tweening.Core.Debugger.LogNullTween(t);
				}
				return null;
			}
			if (!t.active)
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 1)
				{
					global::DG.Tweening.Core.Debugger.LogInvalidTween(t);
				}
				return null;
			}
			if (t.isSequenced)
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 1)
				{
					global::DG.Tweening.Core.Debugger.LogNestedTween(t);
				}
				return null;
			}
			if (!(t is global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Core.PathCore.Path, global::DG.Tweening.Plugins.Options.PathOptions> tweenerCore))
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 1)
				{
					global::DG.Tweening.Core.Debugger.LogNonPathTween(t);
				}
				return null;
			}
			if (!tweenerCore.endValue.isFinalized)
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 1)
				{
					global::DG.Tweening.Core.Debugger.LogWarning("The path is not finalized yet", t);
				}
				return null;
			}
			return global::DG.Tweening.Plugins.Core.PathCore.Path.GetDrawPoints(tweenerCore.endValue, subdivisionsXSegment);
		}

		public static float PathLength(this global::DG.Tweening.Tween t)
		{
			if (t == null)
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 1)
				{
					global::DG.Tweening.Core.Debugger.LogNullTween(t);
				}
				return -1f;
			}
			if (!t.active)
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 1)
				{
					global::DG.Tweening.Core.Debugger.LogInvalidTween(t);
				}
				return -1f;
			}
			if (t.isSequenced)
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 1)
				{
					global::DG.Tweening.Core.Debugger.LogNestedTween(t);
				}
				return -1f;
			}
			if (!(t is global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Core.PathCore.Path, global::DG.Tweening.Plugins.Options.PathOptions> tweenerCore))
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 1)
				{
					global::DG.Tweening.Core.Debugger.LogNonPathTween(t);
				}
				return -1f;
			}
			if (!tweenerCore.endValue.isFinalized)
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 1)
				{
					global::DG.Tweening.Core.Debugger.LogWarning("The path is not finalized yet", t);
				}
				return -1f;
			}
			return tweenerCore.endValue.length;
		}
	}
}
