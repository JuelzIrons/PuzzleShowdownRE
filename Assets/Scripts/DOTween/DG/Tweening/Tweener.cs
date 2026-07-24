namespace DG.Tweening
{
	public abstract class Tweener : global::DG.Tweening.Tween
	{
		internal bool hasManuallySetStartValue;

		internal bool isFromAllowed = true;

		internal Tweener()
		{
		}

		public abstract global::DG.Tweening.Tweener ChangeStartValue(object newStartValue, float newDuration = -1f);

		public abstract global::DG.Tweening.Tweener ChangeEndValue(object newEndValue, float newDuration = -1f, bool snapStartValue = false);

		public abstract global::DG.Tweening.Tweener ChangeEndValue(object newEndValue, bool snapStartValue);

		public abstract global::DG.Tweening.Tweener ChangeValues(object newStartValue, object newEndValue, float newDuration = -1f);

		internal abstract global::DG.Tweening.Tweener SetFrom(bool relative);

		internal static bool Setup<T1, T2, TPlugOptions>(global::DG.Tweening.Core.TweenerCore<T1, T2, TPlugOptions> t, global::DG.Tweening.Core.DOGetter<T1> getter, global::DG.Tweening.Core.DOSetter<T1> setter, T2 endValue, float duration, global::DG.Tweening.Plugins.Core.ABSTweenPlugin<T1, T2, TPlugOptions> plugin = null) where TPlugOptions : struct, global::DG.Tweening.Plugins.Options.IPlugOptions
		{
			if (plugin != null)
			{
				t.tweenPlugin = plugin;
			}
			else
			{
				if (t.tweenPlugin == null)
				{
					t.tweenPlugin = global::DG.Tweening.Plugins.Core.PluginsManager.GetDefaultPlugin<T1, T2, TPlugOptions>();
				}
				if (t.tweenPlugin == null)
				{
					global::DG.Tweening.Core.Debugger.LogError("No suitable plugin found for this type");
					return false;
				}
			}
			t.getter = getter;
			t.setter = setter;
			t.endValue = endValue;
			t.duration = duration;
			t.autoKill = global::DG.Tweening.DOTween.defaultAutoKill;
			t.isRecyclable = global::DG.Tweening.DOTween.defaultRecyclable;
			t.easeType = global::DG.Tweening.DOTween.defaultEaseType;
			t.easeOvershootOrAmplitude = global::DG.Tweening.DOTween.defaultEaseOvershootOrAmplitude;
			t.easePeriod = global::DG.Tweening.DOTween.defaultEasePeriod;
			t.loopType = global::DG.Tweening.DOTween.defaultLoopType;
			t.isPlaying = global::DG.Tweening.DOTween.defaultAutoPlay == global::DG.Tweening.AutoPlay.All || global::DG.Tweening.DOTween.defaultAutoPlay == global::DG.Tweening.AutoPlay.AutoPlayTweeners;
			return true;
		}

		internal static float DoUpdateDelay<T1, T2, TPlugOptions>(global::DG.Tweening.Core.TweenerCore<T1, T2, TPlugOptions> t, float elapsed) where TPlugOptions : struct, global::DG.Tweening.Plugins.Options.IPlugOptions
		{
			float num = t.delay;
			if (elapsed > num)
			{
				t.elapsedDelay = num;
				t.delayComplete = true;
				return elapsed - num;
			}
			t.elapsedDelay = elapsed;
			return 0f;
		}

		internal static bool DoStartup<T1, T2, TPlugOptions>(global::DG.Tweening.Core.TweenerCore<T1, T2, TPlugOptions> t) where TPlugOptions : struct, global::DG.Tweening.Plugins.Options.IPlugOptions
		{
			t.startupDone = true;
			if (t.specialStartupMode != global::DG.Tweening.Core.Enums.SpecialStartupMode.None && !DOStartupSpecials(t))
			{
				return false;
			}
			if (!t.hasManuallySetStartValue)
			{
				if (global::DG.Tweening.DOTween.useSafeMode)
				{
					try
					{
						if (t.isFrom)
						{
							t.SetFrom(t.isRelative && !t.isBlendable);
							t.isRelative = false;
						}
						else
						{
							t.startValue = t.tweenPlugin.ConvertToStartValue(t, t.getter());
						}
					}
					catch (global::System.Exception ex)
					{
						if (global::DG.Tweening.Core.Debugger.ShouldLogSafeModeCapturedError())
						{
							global::DG.Tweening.Core.Debugger.LogSafeModeCapturedError($"Tween startup failed (NULL target/property - {ex.TargetSite}): the tween will now be killed ► {ex.Message}", t);
						}
						global::DG.Tweening.DOTween.safeModeReport.Add(global::DG.Tweening.Core.SafeModeReport.SafeModeReportType.StartupFailure);
						return false;
					}
				}
				else if (t.isFrom)
				{
					t.SetFrom(t.isRelative && !t.isBlendable);
					t.isRelative = false;
				}
				else
				{
					t.startValue = t.tweenPlugin.ConvertToStartValue(t, t.getter());
				}
			}
			if (t.isRelative)
			{
				t.tweenPlugin.SetRelativeEndValue(t);
			}
			t.tweenPlugin.SetChangeValue(t);
			DOStartupDurationBased(t);
			if (t.duration <= 0f)
			{
				t.easeType = global::DG.Tweening.Ease.INTERNAL_Zero;
			}
			return true;
		}

		internal static global::DG.Tweening.Core.TweenerCore<T1, T2, TPlugOptions> DoChangeStartValue<T1, T2, TPlugOptions>(global::DG.Tweening.Core.TweenerCore<T1, T2, TPlugOptions> t, T2 newStartValue, float newDuration) where TPlugOptions : struct, global::DG.Tweening.Plugins.Options.IPlugOptions
		{
			t.hasManuallySetStartValue = true;
			t.startValue = newStartValue;
			if (t.startupDone)
			{
				if (t.specialStartupMode != global::DG.Tweening.Core.Enums.SpecialStartupMode.None && !DOStartupSpecials(t))
				{
					return null;
				}
				t.tweenPlugin.SetChangeValue(t);
			}
			if (newDuration > 0f)
			{
				t.duration = newDuration;
				if (t.startupDone)
				{
					DOStartupDurationBased(t);
				}
			}
			global::DG.Tweening.Tween.DoGoto(t, 0f, 0, global::DG.Tweening.Core.Enums.UpdateMode.IgnoreOnUpdate);
			return t;
		}

		internal static global::DG.Tweening.Core.TweenerCore<T1, T2, TPlugOptions> DoChangeEndValue<T1, T2, TPlugOptions>(global::DG.Tweening.Core.TweenerCore<T1, T2, TPlugOptions> t, T2 newEndValue, float newDuration, bool snapStartValue) where TPlugOptions : struct, global::DG.Tweening.Plugins.Options.IPlugOptions
		{
			t.endValue = newEndValue;
			t.isRelative = false;
			if (t.startupDone)
			{
				if (t.specialStartupMode != global::DG.Tweening.Core.Enums.SpecialStartupMode.None && !DOStartupSpecials(t))
				{
					return null;
				}
				if (snapStartValue)
				{
					if (global::DG.Tweening.DOTween.useSafeMode)
					{
						try
						{
							t.startValue = t.tweenPlugin.ConvertToStartValue(t, t.getter());
						}
						catch (global::System.Exception ex)
						{
							if (global::DG.Tweening.Core.Debugger.ShouldLogSafeModeCapturedError())
							{
								global::DG.Tweening.Core.Debugger.LogSafeModeCapturedError($"Target or field is missing/null ({ex.TargetSite}) ► {ex.Message}\n\n{ex.StackTrace}\n\n", t);
							}
							global::DG.Tweening.Core.TweenManager.Despawn(t);
							global::DG.Tweening.DOTween.safeModeReport.Add(global::DG.Tweening.Core.SafeModeReport.SafeModeReportType.TargetOrFieldMissing);
							return null;
						}
					}
					else
					{
						t.startValue = t.tweenPlugin.ConvertToStartValue(t, t.getter());
					}
				}
				t.tweenPlugin.SetChangeValue(t);
			}
			if (newDuration > 0f)
			{
				t.duration = newDuration;
				if (t.startupDone)
				{
					DOStartupDurationBased(t);
				}
			}
			global::DG.Tweening.Tween.DoGoto(t, 0f, 0, global::DG.Tweening.Core.Enums.UpdateMode.IgnoreOnUpdate);
			return t;
		}

		internal static global::DG.Tweening.Core.TweenerCore<T1, T2, TPlugOptions> DoChangeValues<T1, T2, TPlugOptions>(global::DG.Tweening.Core.TweenerCore<T1, T2, TPlugOptions> t, T2 newStartValue, T2 newEndValue, float newDuration) where TPlugOptions : struct, global::DG.Tweening.Plugins.Options.IPlugOptions
		{
			t.hasManuallySetStartValue = true;
			t.isRelative = (t.isFrom = false);
			t.startValue = newStartValue;
			t.endValue = newEndValue;
			if (t.startupDone)
			{
				if (t.specialStartupMode != global::DG.Tweening.Core.Enums.SpecialStartupMode.None && !DOStartupSpecials(t))
				{
					return null;
				}
				t.tweenPlugin.SetChangeValue(t);
			}
			if (newDuration > 0f)
			{
				t.duration = newDuration;
				if (t.startupDone)
				{
					DOStartupDurationBased(t);
				}
			}
			global::DG.Tweening.Tween.DoGoto(t, 0f, 0, global::DG.Tweening.Core.Enums.UpdateMode.IgnoreOnUpdate);
			return t;
		}

		private static bool DOStartupSpecials<T1, T2, TPlugOptions>(global::DG.Tweening.Core.TweenerCore<T1, T2, TPlugOptions> t) where TPlugOptions : struct, global::DG.Tweening.Plugins.Options.IPlugOptions
		{
			try
			{
				switch (t.specialStartupMode)
				{
				case global::DG.Tweening.Core.Enums.SpecialStartupMode.SetLookAt:
					if (!global::DG.Tweening.Plugins.Core.SpecialPluginsUtils.SetLookAt(t as global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Quaternion, global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Options.QuaternionOptions>))
					{
						return false;
					}
					break;
				case global::DG.Tweening.Core.Enums.SpecialStartupMode.SetPunch:
					if (!global::DG.Tweening.Plugins.Core.SpecialPluginsUtils.SetPunch(t as global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::UnityEngine.Vector3[], global::DG.Tweening.Plugins.Options.Vector3ArrayOptions>))
					{
						return false;
					}
					break;
				case global::DG.Tweening.Core.Enums.SpecialStartupMode.SetShake:
					if (!global::DG.Tweening.Plugins.Core.SpecialPluginsUtils.SetShake(t as global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::UnityEngine.Vector3[], global::DG.Tweening.Plugins.Options.Vector3ArrayOptions>))
					{
						return false;
					}
					break;
				case global::DG.Tweening.Core.Enums.SpecialStartupMode.SetCameraShakePosition:
					if (!global::DG.Tweening.Plugins.Core.SpecialPluginsUtils.SetCameraShakePosition(t as global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::UnityEngine.Vector3[], global::DG.Tweening.Plugins.Options.Vector3ArrayOptions>))
					{
						return false;
					}
					break;
				}
				return true;
			}
			catch
			{
				return false;
			}
		}

		private static void DOStartupDurationBased<T1, T2, TPlugOptions>(global::DG.Tweening.Core.TweenerCore<T1, T2, TPlugOptions> t) where TPlugOptions : struct, global::DG.Tweening.Plugins.Options.IPlugOptions
		{
			if (t.isSpeedBased)
			{
				t.duration = t.tweenPlugin.GetSpeedBasedDuration(t.plugOptions, t.duration, t.changeValue);
			}
			t.fullDuration = ((t.loops > -1) ? (t.duration * (float)t.loops) : float.PositiveInfinity);
		}
	}
}
