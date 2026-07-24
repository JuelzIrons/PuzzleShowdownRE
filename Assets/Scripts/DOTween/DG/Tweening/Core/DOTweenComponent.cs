namespace DG.Tweening.Core
{
	[global::UnityEngine.AddComponentMenu("")]
	public class DOTweenComponent : global::UnityEngine.MonoBehaviour, global::DG.Tweening.IDOTweenInit
	{
		public int inspectorUpdater;

		private float _unscaledTime;

		private float _unscaledDeltaTime;

		private bool _paused;

		private float _pausedTime;

		private bool _isQuitting;

		private bool _duplicateToDestroy;

		private void Awake()
		{
			if (global::DG.Tweening.DOTween.instance == null)
			{
				global::DG.Tweening.DOTween.instance = this;
				inspectorUpdater = 0;
				_unscaledTime = global::UnityEngine.Time.realtimeSinceStartup;
				global::System.Type looseScriptType = global::DG.Tweening.Core.DOTweenUtils.GetLooseScriptType("DG.Tweening.DOTweenModuleUtils");
				if ((object)looseScriptType == null)
				{
					global::DG.Tweening.Core.Debugger.LogError("Couldn't load Modules system");
				}
				else
				{
					looseScriptType.GetMethod("Init", global::System.Reflection.BindingFlags.Static | global::System.Reflection.BindingFlags.Public).Invoke(null, null);
				}
			}
			else
			{
				if (global::DG.Tweening.Core.Debugger.logPriority >= 1)
				{
					global::DG.Tweening.Core.Debugger.LogWarning("Duplicate DOTweenComponent instance found in scene: destroying it");
				}
				global::UnityEngine.Object.Destroy(base.gameObject);
			}
		}

		private void Start()
		{
			if (global::DG.Tweening.DOTween.instance != this)
			{
				_duplicateToDestroy = true;
				global::UnityEngine.Object.Destroy(base.gameObject);
			}
		}

		private void Update()
		{
			_unscaledDeltaTime = global::UnityEngine.Time.realtimeSinceStartup - _unscaledTime;
			if (global::DG.Tweening.DOTween.useSmoothDeltaTime && _unscaledDeltaTime > global::DG.Tweening.DOTween.maxSmoothUnscaledTime)
			{
				_unscaledDeltaTime = global::DG.Tweening.DOTween.maxSmoothUnscaledTime;
			}
			if (global::DG.Tweening.Core.TweenManager.hasActiveDefaultTweens)
			{
				global::DG.Tweening.Core.TweenManager.Update(global::DG.Tweening.UpdateType.Normal, (global::DG.Tweening.DOTween.useSmoothDeltaTime ? global::UnityEngine.Time.smoothDeltaTime : global::UnityEngine.Time.deltaTime) * global::DG.Tweening.DOTween.timeScale, _unscaledDeltaTime * global::DG.Tweening.DOTween.unscaledTimeScale * global::DG.Tweening.DOTween.timeScale);
			}
			_unscaledTime = global::UnityEngine.Time.realtimeSinceStartup;
			if (!global::DG.Tweening.Core.TweenManager.isUnityEditor)
			{
				return;
			}
			inspectorUpdater++;
			if (global::DG.Tweening.DOTween.showUnityEditorReport && global::DG.Tweening.Core.TweenManager.hasActiveTweens)
			{
				if (global::DG.Tweening.Core.TweenManager.totActiveTweeners > global::DG.Tweening.DOTween.maxActiveTweenersReached)
				{
					global::DG.Tweening.DOTween.maxActiveTweenersReached = global::DG.Tweening.Core.TweenManager.totActiveTweeners;
				}
				if (global::DG.Tweening.Core.TweenManager.totActiveSequences > global::DG.Tweening.DOTween.maxActiveSequencesReached)
				{
					global::DG.Tweening.DOTween.maxActiveSequencesReached = global::DG.Tweening.Core.TweenManager.totActiveSequences;
				}
			}
		}

		private void LateUpdate()
		{
			if (global::DG.Tweening.Core.TweenManager.hasActiveLateTweens)
			{
				global::DG.Tweening.Core.TweenManager.Update(global::DG.Tweening.UpdateType.Late, (global::DG.Tweening.DOTween.useSmoothDeltaTime ? global::UnityEngine.Time.smoothDeltaTime : global::UnityEngine.Time.deltaTime) * global::DG.Tweening.DOTween.timeScale, _unscaledDeltaTime * global::DG.Tweening.DOTween.unscaledTimeScale * global::DG.Tweening.DOTween.timeScale);
			}
		}

		private void FixedUpdate()
		{
			if (global::DG.Tweening.Core.TweenManager.hasActiveFixedTweens && global::UnityEngine.Time.timeScale > 0f)
			{
				global::DG.Tweening.Core.TweenManager.Update(global::DG.Tweening.UpdateType.Fixed, (global::DG.Tweening.DOTween.useSmoothDeltaTime ? global::UnityEngine.Time.smoothDeltaTime : global::UnityEngine.Time.deltaTime) * global::DG.Tweening.DOTween.timeScale, (global::DG.Tweening.DOTween.useSmoothDeltaTime ? global::UnityEngine.Time.smoothDeltaTime : global::UnityEngine.Time.deltaTime) / global::UnityEngine.Time.timeScale * global::DG.Tweening.DOTween.unscaledTimeScale * global::DG.Tweening.DOTween.timeScale);
			}
		}

		private void OnDrawGizmos()
		{
			if (!global::DG.Tweening.DOTween.drawGizmos || !global::DG.Tweening.Core.TweenManager.isUnityEditor)
			{
				return;
			}
			int count = global::DG.Tweening.DOTween.GizmosDelegates.Count;
			if (count != 0)
			{
				for (int i = 0; i < count; i++)
				{
					global::DG.Tweening.DOTween.GizmosDelegates[i]();
				}
			}
		}

		private void OnDestroy()
		{
			if (_duplicateToDestroy)
			{
				return;
			}
			if (global::DG.Tweening.DOTween.showUnityEditorReport)
			{
				global::DG.Tweening.Core.Debugger.LogReport("Max overall simultaneous active Tweeners/Sequences: " + global::DG.Tweening.DOTween.maxActiveTweenersReached + "/" + global::DG.Tweening.DOTween.maxActiveSequencesReached);
			}
			if (global::DG.Tweening.DOTween.useSafeMode)
			{
				int totErrors = global::DG.Tweening.DOTween.safeModeReport.GetTotErrors();
				if (totErrors > 0)
				{
					string text = $"DOTween's safe mode captured {totErrors} errors. This is usually ok (it's what safe mode is there for) but if your game is encountering issues you should set Log Behaviour to Default in DOTween Utility Panel in order to get detailed warnings when an error is captured (consider that these errors are always on the user side).";
					if (global::DG.Tweening.DOTween.safeModeReport.totMissingTargetOrFieldErrors > 0)
					{
						text = text + "\n- " + global::DG.Tweening.DOTween.safeModeReport.totMissingTargetOrFieldErrors + " missing target or field errors";
					}
					if (global::DG.Tweening.DOTween.safeModeReport.totStartupErrors > 0)
					{
						text = text + "\n- " + global::DG.Tweening.DOTween.safeModeReport.totStartupErrors + " startup errors";
					}
					if (global::DG.Tweening.DOTween.safeModeReport.totCallbackErrors > 0)
					{
						text = text + "\n- " + global::DG.Tweening.DOTween.safeModeReport.totCallbackErrors + " errors inside callbacks (these might be important)";
					}
					if (global::DG.Tweening.DOTween.safeModeReport.totUnsetErrors > 0)
					{
						text = text + "\n- " + global::DG.Tweening.DOTween.safeModeReport.totUnsetErrors + " undetermined errors (these might be important)";
					}
					global::DG.Tweening.Core.Debugger.LogSafeModeReport(text);
				}
			}
			if (global::DG.Tweening.DOTween.instance == this)
			{
				global::DG.Tweening.DOTween.instance = null;
			}
			global::DG.Tweening.DOTween.Clear(destroy: true, _isQuitting);
		}

		public void OnApplicationPause(bool pauseStatus)
		{
			if (pauseStatus)
			{
				_paused = true;
				_pausedTime = global::UnityEngine.Time.realtimeSinceStartup;
			}
			else if (_paused)
			{
				_paused = false;
				_unscaledTime += global::UnityEngine.Time.realtimeSinceStartup - _pausedTime;
			}
		}

		private void OnApplicationQuit()
		{
			_isQuitting = true;
			global::DG.Tweening.DOTween.isQuitting = true;
		}

		public global::DG.Tweening.IDOTweenInit SetCapacity(int tweenersCapacity, int sequencesCapacity)
		{
			global::DG.Tweening.Core.TweenManager.SetCapacities(tweenersCapacity, sequencesCapacity);
			return this;
		}

		internal global::System.Collections.IEnumerator WaitForCompletion(global::DG.Tweening.Tween t)
		{
			while (t.active && !t.isComplete)
			{
				yield return null;
			}
		}

		internal global::System.Collections.IEnumerator WaitForRewind(global::DG.Tweening.Tween t)
		{
			while (t.active && (!t.playedOnce || t.position * (float)(t.completedLoops + 1) > 0f))
			{
				yield return null;
			}
		}

		internal global::System.Collections.IEnumerator WaitForKill(global::DG.Tweening.Tween t)
		{
			while (t.active)
			{
				yield return null;
			}
		}

		internal global::System.Collections.IEnumerator WaitForElapsedLoops(global::DG.Tweening.Tween t, int elapsedLoops)
		{
			while (t.active && t.completedLoops < elapsedLoops)
			{
				yield return null;
			}
		}

		internal global::System.Collections.IEnumerator WaitForPosition(global::DG.Tweening.Tween t, float position)
		{
			while (t.active && t.position * (float)(t.completedLoops + 1) < position)
			{
				yield return null;
			}
		}

		internal global::System.Collections.IEnumerator WaitForStart(global::DG.Tweening.Tween t)
		{
			while (t.active && !t.playedOnce)
			{
				yield return null;
			}
		}

		internal static void Create()
		{
			if (!(global::DG.Tweening.DOTween.instance != null))
			{
				global::UnityEngine.GameObject obj = new global::UnityEngine.GameObject("[DOTween]");
				global::UnityEngine.Object.DontDestroyOnLoad(obj);
				global::DG.Tweening.DOTween.instance = obj.AddComponent<global::DG.Tweening.Core.DOTweenComponent>();
			}
		}

		internal static void DestroyInstance()
		{
			if (global::DG.Tweening.DOTween.instance != null)
			{
				global::UnityEngine.Object.Destroy(global::DG.Tweening.DOTween.instance.gameObject);
			}
			global::DG.Tweening.DOTween.instance = null;
		}
	}
}
