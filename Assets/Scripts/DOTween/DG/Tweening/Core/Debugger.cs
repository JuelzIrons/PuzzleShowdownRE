namespace DG.Tweening.Core
{
	public static class Debugger
	{
		internal static class Sequence
		{
			public static void LogAddToNullSequence()
			{
				LogWarning("You can't add elements to a NULL Sequence");
			}

			public static void LogAddToInactiveSequence()
			{
				LogWarning("You can't add elements to an inactive/killed Sequence");
			}

			public static void LogAddToLockedSequence()
			{
				LogWarning("The Sequence has started and is now locked, you can only elements to a Sequence before it starts");
			}

			public static void LogAddNullTween()
			{
				LogWarning("You can't add a NULL tween to a Sequence");
			}

			public static void LogAddInactiveTween(global::DG.Tweening.Tween t)
			{
				LogWarning("You can't add an inactive/killed tween to a Sequence", t);
			}

			public static void LogAddAlreadySequencedTween(global::DG.Tweening.Tween t)
			{
				LogWarning("You can't add a tween that is already nested into a Sequence to another Sequence", t);
			}
		}

		private static int _logPriority;

		private const string _LogPrefix = "<color=#0099bc><b>DOTWEEN ► </b></color>";

		public static int logPriority => _logPriority;

		public static void Log(object message)
		{
			string text = "<color=#0099bc><b>DOTWEEN ► </b></color>" + message;
			if (global::DG.Tweening.DOTween.onWillLog == null || global::DG.Tweening.DOTween.onWillLog(global::UnityEngine.LogType.Log, text))
			{
				global::UnityEngine.Debug.Log(text);
			}
		}

		public static void LogWarning(object message, global::DG.Tweening.Tween t = null)
		{
			string text = ((!global::DG.Tweening.DOTween.debugMode) ? ("<color=#0099bc><b>DOTWEEN ► </b></color>" + message) : ("<color=#0099bc><b>DOTWEEN ► </b></color>" + GetDebugDataMessage(t) + message));
			if (global::DG.Tweening.DOTween.onWillLog == null || global::DG.Tweening.DOTween.onWillLog(global::UnityEngine.LogType.Warning, text))
			{
				global::UnityEngine.Debug.LogWarning(text);
			}
		}

		public static void LogError(object message, global::DG.Tweening.Tween t = null)
		{
			string text = ((!global::DG.Tweening.DOTween.debugMode) ? ("<color=#0099bc><b>DOTWEEN ► </b></color>" + message) : ("<color=#0099bc><b>DOTWEEN ► </b></color>" + GetDebugDataMessage(t) + message));
			if (global::DG.Tweening.DOTween.onWillLog == null || global::DG.Tweening.DOTween.onWillLog(global::UnityEngine.LogType.Error, text))
			{
				global::UnityEngine.Debug.LogError(text);
			}
		}

		public static void LogSafeModeCapturedError(object message, global::DG.Tweening.Tween t = null)
		{
			string text = ((!global::DG.Tweening.DOTween.debugMode) ? ("<color=#0099bc><b>DOTWEEN ► </b></color>" + message) : ("<color=#0099bc><b>DOTWEEN ► </b></color>" + GetDebugDataMessage(t) + message));
			if (global::DG.Tweening.DOTween.onWillLog == null || global::DG.Tweening.DOTween.onWillLog(global::UnityEngine.LogType.Log, text))
			{
				switch (global::DG.Tweening.DOTween.safeModeLogBehaviour)
				{
				case global::DG.Tweening.Core.Enums.SafeModeLogBehaviour.Normal:
					global::UnityEngine.Debug.Log(text);
					break;
				case global::DG.Tweening.Core.Enums.SafeModeLogBehaviour.Warning:
					global::UnityEngine.Debug.LogWarning(text);
					break;
				case global::DG.Tweening.Core.Enums.SafeModeLogBehaviour.Error:
					global::UnityEngine.Debug.LogError(text);
					break;
				}
			}
		}

		public static void LogReport(object message)
		{
			string text = string.Format("<color=#00B500FF>{0} REPORT ►</color> {1}", "<color=#0099bc><b>DOTWEEN ► </b></color>", message);
			if (global::DG.Tweening.DOTween.onWillLog == null || global::DG.Tweening.DOTween.onWillLog(global::UnityEngine.LogType.Log, text))
			{
				global::UnityEngine.Debug.Log(text);
			}
		}

		public static void LogSafeModeReport(object message)
		{
			string text = string.Format("<color=#ff7337>{0} SAFE MODE ►</color> {1}", "<color=#0099bc><b>DOTWEEN ► </b></color>", message);
			if (global::DG.Tweening.DOTween.onWillLog == null || global::DG.Tweening.DOTween.onWillLog(global::UnityEngine.LogType.Log, text))
			{
				global::UnityEngine.Debug.LogWarning(text);
			}
		}

		public static void LogInvalidTween(global::DG.Tweening.Tween t)
		{
			LogWarning("This Tween has been killed and is now invalid");
		}

		public static void LogNestedTween(global::DG.Tweening.Tween t)
		{
			LogWarning("This Tween was added to a Sequence and can't be controlled directly", t);
		}

		public static void LogNullTween(global::DG.Tweening.Tween t)
		{
			LogWarning("Null Tween");
		}

		public static void LogNonPathTween(global::DG.Tweening.Tween t)
		{
			LogWarning("This Tween is not a path tween", t);
		}

		public static void LogMissingMaterialProperty(string propertyName)
		{
			LogWarning($"This material doesn't have a {propertyName} property");
		}

		public static void LogMissingMaterialProperty(int propertyId)
		{
			LogWarning($"This material doesn't have a {propertyId} property ID");
		}

		public static void LogRemoveActiveTweenError(string errorInfo, global::DG.Tweening.Tween t)
		{
			LogWarning($"Error in RemoveActiveTween ({errorInfo}). It's been taken care of so no problems, but Daniele (DOTween's author) is trying to pinpoint it (it's very rare and he can't reproduce it) so it would be awesome if you could reproduce this log in a sample project and send it to him. Or even just write him the complete log that was generated by this message. Fixing this would make DOTween slightly faster. Thanks.", t);
		}

		public static void LogAddActiveTweenError(string errorInfo, global::DG.Tweening.Tween t)
		{
			LogWarning($"Error in AddActiveTween ({errorInfo}). It's been taken care of so no problems, but Daniele (DOTween's author) is trying to pinpoint it (it's very rare and he can't reproduce it) so it would be awesome if you could reproduce this log in a sample project and send it to him. Or even just write him the complete log that was generated by this message. Fixing this would make DOTween slightly faster. Thanks.", t);
		}

		public static void SetLogPriority(global::DG.Tweening.LogBehaviour logBehaviour)
		{
			switch (logBehaviour)
			{
			case global::DG.Tweening.LogBehaviour.Default:
				_logPriority = 1;
				break;
			case global::DG.Tweening.LogBehaviour.Verbose:
				_logPriority = 2;
				break;
			default:
				_logPriority = 0;
				break;
			}
		}

		public static bool ShouldLogSafeModeCapturedError()
		{
			switch (global::DG.Tweening.DOTween.safeModeLogBehaviour)
			{
			case global::DG.Tweening.Core.Enums.SafeModeLogBehaviour.None:
				return false;
			case global::DG.Tweening.Core.Enums.SafeModeLogBehaviour.Normal:
			case global::DG.Tweening.Core.Enums.SafeModeLogBehaviour.Warning:
				return _logPriority >= 1;
			default:
				return true;
			}
		}

		private static string GetDebugDataMessage(global::DG.Tweening.Tween t)
		{
			string message = "";
			AddDebugDataToMessage(ref message, t);
			return message;
		}

		private static void AddDebugDataToMessage(ref string message, global::DG.Tweening.Tween t)
		{
			if (t == null)
			{
				return;
			}
			bool flag = t.debugTargetId != null;
			bool flag2 = t.stringId != null;
			bool flag3 = t.intId != -999;
			if (flag || flag2 || flag3)
			{
				message += "DEBUG MODE INFO ► ";
				if (flag)
				{
					message += $"[tween target: {t.debugTargetId}]";
				}
				if (flag2)
				{
					message += $"[stringId: {t.stringId}]";
				}
				if (flag3)
				{
					message += $"[intId: {t.intId}]";
				}
				message += "\n";
			}
		}
	}
}
