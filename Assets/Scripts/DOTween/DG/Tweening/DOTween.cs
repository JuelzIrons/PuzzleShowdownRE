namespace DG.Tweening
{
	public class DOTween
	{
		public static readonly string Version = "1.2.765";

		public static bool useSafeMode = true;

		public static global::DG.Tweening.Core.Enums.SafeModeLogBehaviour safeModeLogBehaviour = global::DG.Tweening.Core.Enums.SafeModeLogBehaviour.Warning;

		public static global::DG.Tweening.Core.Enums.NestedTweenFailureBehaviour nestedTweenFailureBehaviour = global::DG.Tweening.Core.Enums.NestedTweenFailureBehaviour.TryToPreserveSequence;

		public static bool showUnityEditorReport = false;

		public static float timeScale = 1f;

		public static float unscaledTimeScale = 1f;

		public static bool useSmoothDeltaTime;

		public static float maxSmoothUnscaledTime = 0.15f;

		internal static global::DG.Tweening.Core.Enums.RewindCallbackMode rewindCallbackMode = global::DG.Tweening.Core.Enums.RewindCallbackMode.FireIfPositionChanged;

		private static global::DG.Tweening.LogBehaviour _logBehaviour = global::DG.Tweening.LogBehaviour.ErrorsOnly;

		public static global::System.Func<global::UnityEngine.LogType, object, bool> onWillLog;

		public static bool drawGizmos = true;

		public static bool debugMode = false;

		private static bool _fooDebugStoreTargetId = true;

		public static global::DG.Tweening.UpdateType defaultUpdateType = global::DG.Tweening.UpdateType.Normal;

		public static bool defaultTimeScaleIndependent = false;

		public static global::DG.Tweening.AutoPlay defaultAutoPlay = global::DG.Tweening.AutoPlay.All;

		public static bool defaultAutoKill = true;

		public static global::DG.Tweening.LoopType defaultLoopType = global::DG.Tweening.LoopType.Restart;

		public static bool defaultRecyclable;

		public static global::DG.Tweening.Ease defaultEaseType = global::DG.Tweening.Ease.OutQuad;

		public static float defaultEaseOvershootOrAmplitude = 1.70158f;

		public static float defaultEasePeriod = 0f;

		public static global::DG.Tweening.Core.DOTweenComponent instance;

		private static bool _foo_isQuitting;

		internal static int maxActiveTweenersReached;

		internal static int maxActiveSequencesReached;

		internal static global::DG.Tweening.Core.SafeModeReport safeModeReport;

		internal static readonly global::System.Collections.Generic.List<global::DG.Tweening.TweenCallback> GizmosDelegates = new global::System.Collections.Generic.List<global::DG.Tweening.TweenCallback>();

		internal static bool initialized;

		private static int _isQuittingFrame = -1;

		public static global::DG.Tweening.LogBehaviour logBehaviour
		{
			get
			{
				return _logBehaviour;
			}
			set
			{
				_logBehaviour = value;
				global::DG.Tweening.Core.Debugger.SetLogPriority(_logBehaviour);
			}
		}

		public static bool debugStoreTargetId
		{
			get
			{
				if (debugMode && useSafeMode)
				{
					return _fooDebugStoreTargetId;
				}
				return false;
			}
			set
			{
				_fooDebugStoreTargetId = value;
			}
		}

		internal static bool isQuitting
		{
			get
			{
				if (!_foo_isQuitting)
				{
					return false;
				}
				if (global::UnityEngine.Time.frameCount >= 0 && _isQuittingFrame != global::UnityEngine.Time.frameCount)
				{
					_foo_isQuitting = false;
					return false;
				}
				return true;
			}
			set
			{
				_foo_isQuitting = value;
				if (value)
				{
					_isQuittingFrame = global::UnityEngine.Time.frameCount;
				}
			}
		}

		public static global::DG.Tweening.IDOTweenInit Init(bool? recycleAllByDefault = null, bool? useSafeMode = null, global::DG.Tweening.LogBehaviour? logBehaviour = null)
		{
			if (initialized)
			{
				return instance;
			}
			if (!global::UnityEngine.Application.isPlaying || isQuitting)
			{
				return null;
			}
			return Init(global::UnityEngine.Resources.Load("DOTweenSettings") as global::DG.Tweening.Core.DOTweenSettings, recycleAllByDefault, useSafeMode, logBehaviour);
		}

		private static void AutoInit()
		{
			if (global::UnityEngine.Application.isPlaying && !isQuitting)
			{
				Init(global::UnityEngine.Resources.Load("DOTweenSettings") as global::DG.Tweening.Core.DOTweenSettings, null, null, null);
			}
		}

		private static global::DG.Tweening.IDOTweenInit Init(global::DG.Tweening.Core.DOTweenSettings settings, bool? recycleAllByDefault, bool? useSafeMode, global::DG.Tweening.LogBehaviour? logBehaviour)
		{
			initialized = true;
			if (recycleAllByDefault.HasValue)
			{
				defaultRecyclable = recycleAllByDefault.Value;
			}
			if (useSafeMode.HasValue)
			{
				global::DG.Tweening.DOTween.useSafeMode = useSafeMode.Value;
			}
			if (logBehaviour.HasValue)
			{
				global::DG.Tweening.DOTween.logBehaviour = logBehaviour.Value;
			}
			global::DG.Tweening.Core.DOTweenComponent.Create();
			if (settings != null)
			{
				if (!useSafeMode.HasValue)
				{
					global::DG.Tweening.DOTween.useSafeMode = settings.useSafeMode;
				}
				if (!logBehaviour.HasValue)
				{
					global::DG.Tweening.DOTween.logBehaviour = settings.logBehaviour;
				}
				if (!recycleAllByDefault.HasValue)
				{
					defaultRecyclable = settings.defaultRecyclable;
				}
				safeModeLogBehaviour = settings.safeModeOptions.logBehaviour;
				nestedTweenFailureBehaviour = settings.safeModeOptions.nestedTweenFailureBehaviour;
				timeScale = settings.timeScale;
				unscaledTimeScale = settings.unscaledTimeScale;
				useSmoothDeltaTime = settings.useSmoothDeltaTime;
				maxSmoothUnscaledTime = settings.maxSmoothUnscaledTime;
				rewindCallbackMode = settings.rewindCallbackMode;
				defaultRecyclable = ((!recycleAllByDefault.HasValue) ? settings.defaultRecyclable : recycleAllByDefault.Value);
				showUnityEditorReport = settings.showUnityEditorReport;
				drawGizmos = settings.drawGizmos;
				defaultAutoPlay = settings.defaultAutoPlay;
				defaultUpdateType = settings.defaultUpdateType;
				defaultTimeScaleIndependent = settings.defaultTimeScaleIndependent;
				defaultEaseType = settings.defaultEaseType;
				defaultEaseOvershootOrAmplitude = settings.defaultEaseOvershootOrAmplitude;
				defaultEasePeriod = settings.defaultEasePeriod;
				defaultAutoKill = settings.defaultAutoKill;
				defaultLoopType = settings.defaultLoopType;
				debugMode = settings.debugMode;
				debugStoreTargetId = settings.debugStoreTargetId;
			}
			if (global::DG.Tweening.Core.Debugger.logPriority >= 2)
			{
				global::DG.Tweening.Core.Debugger.Log("DOTween initialization (useSafeMode: " + global::DG.Tweening.DOTween.useSafeMode + ", recycling: " + (defaultRecyclable ? "ON" : "OFF") + ", logBehaviour: " + global::DG.Tweening.DOTween.logBehaviour.ToString() + ")");
			}
			return instance;
		}

		public static void SetTweensCapacity(int tweenersCapacity, int sequencesCapacity)
		{
			global::DG.Tweening.Core.TweenManager.SetCapacities(tweenersCapacity, sequencesCapacity);
		}

		public static void Clear(bool destroy = false)
		{
			Clear(destroy, isApplicationQuitting: false);
		}

		internal static void Clear(bool destroy, bool isApplicationQuitting)
		{
			global::DG.Tweening.Core.TweenManager.PurgeAll(isApplicationQuitting);
			global::DG.Tweening.Plugins.Core.PluginsManager.PurgeAll();
			if (destroy)
			{
				initialized = false;
				useSafeMode = false;
				safeModeLogBehaviour = global::DG.Tweening.Core.Enums.SafeModeLogBehaviour.Warning;
				nestedTweenFailureBehaviour = global::DG.Tweening.Core.Enums.NestedTweenFailureBehaviour.TryToPreserveSequence;
				showUnityEditorReport = false;
				drawGizmos = true;
				timeScale = 1f;
				unscaledTimeScale = 1f;
				useSmoothDeltaTime = false;
				maxSmoothUnscaledTime = 0.15f;
				rewindCallbackMode = global::DG.Tweening.Core.Enums.RewindCallbackMode.FireIfPositionChanged;
				logBehaviour = global::DG.Tweening.LogBehaviour.ErrorsOnly;
				onWillLog = null;
				defaultEaseType = global::DG.Tweening.Ease.OutQuad;
				defaultEaseOvershootOrAmplitude = 1.70158f;
				defaultEasePeriod = 0f;
				defaultUpdateType = global::DG.Tweening.UpdateType.Normal;
				defaultTimeScaleIndependent = false;
				defaultAutoPlay = global::DG.Tweening.AutoPlay.All;
				defaultLoopType = global::DG.Tweening.LoopType.Restart;
				defaultAutoKill = true;
				defaultRecyclable = false;
				maxActiveTweenersReached = (maxActiveSequencesReached = 0);
				GizmosDelegates.Clear();
				global::DG.Tweening.Core.DOTweenComponent.DestroyInstance();
			}
		}

		public static void ClearCachedTweens()
		{
			global::DG.Tweening.Core.TweenManager.PurgePools();
		}

		public static int Validate()
		{
			return global::DG.Tweening.Core.TweenManager.Validate();
		}

		public static void ManualUpdate(float deltaTime, float unscaledDeltaTime)
		{
			InitCheck();
			if (global::DG.Tweening.Core.TweenManager.hasActiveManualTweens)
			{
				global::DG.Tweening.Core.TweenManager.Update(global::DG.Tweening.UpdateType.Manual, deltaTime * timeScale, unscaledDeltaTime * unscaledTimeScale * timeScale);
			}
		}

		public static global::DG.Tweening.Core.TweenerCore<float, float, global::DG.Tweening.Plugins.Options.FloatOptions> To(global::DG.Tweening.Core.DOGetter<float> getter, global::DG.Tweening.Core.DOSetter<float> setter, float endValue, float duration)
		{
			return ApplyTo<float, float, global::DG.Tweening.Plugins.Options.FloatOptions>(getter, setter, endValue, duration);
		}

		public static global::DG.Tweening.Core.TweenerCore<double, double, global::DG.Tweening.Plugins.Options.NoOptions> To(global::DG.Tweening.Core.DOGetter<double> getter, global::DG.Tweening.Core.DOSetter<double> setter, double endValue, float duration)
		{
			return ApplyTo<double, double, global::DG.Tweening.Plugins.Options.NoOptions>(getter, setter, endValue, duration);
		}

		public static global::DG.Tweening.Core.TweenerCore<int, int, global::DG.Tweening.Plugins.Options.NoOptions> To(global::DG.Tweening.Core.DOGetter<int> getter, global::DG.Tweening.Core.DOSetter<int> setter, int endValue, float duration)
		{
			return ApplyTo<int, int, global::DG.Tweening.Plugins.Options.NoOptions>(getter, setter, endValue, duration);
		}

		public static global::DG.Tweening.Core.TweenerCore<uint, uint, global::DG.Tweening.Plugins.Options.UintOptions> To(global::DG.Tweening.Core.DOGetter<uint> getter, global::DG.Tweening.Core.DOSetter<uint> setter, uint endValue, float duration)
		{
			return ApplyTo<uint, uint, global::DG.Tweening.Plugins.Options.UintOptions>(getter, setter, endValue, duration);
		}

		public static global::DG.Tweening.Core.TweenerCore<long, long, global::DG.Tweening.Plugins.Options.NoOptions> To(global::DG.Tweening.Core.DOGetter<long> getter, global::DG.Tweening.Core.DOSetter<long> setter, long endValue, float duration)
		{
			return ApplyTo<long, long, global::DG.Tweening.Plugins.Options.NoOptions>(getter, setter, endValue, duration);
		}

		public static global::DG.Tweening.Core.TweenerCore<ulong, ulong, global::DG.Tweening.Plugins.Options.NoOptions> To(global::DG.Tweening.Core.DOGetter<ulong> getter, global::DG.Tweening.Core.DOSetter<ulong> setter, ulong endValue, float duration)
		{
			return ApplyTo<ulong, ulong, global::DG.Tweening.Plugins.Options.NoOptions>(getter, setter, endValue, duration);
		}

		public static global::DG.Tweening.Core.TweenerCore<string, string, global::DG.Tweening.Plugins.Options.StringOptions> To(global::DG.Tweening.Core.DOGetter<string> getter, global::DG.Tweening.Core.DOSetter<string> setter, string endValue, float duration)
		{
			return ApplyTo<string, string, global::DG.Tweening.Plugins.Options.StringOptions>(getter, setter, endValue, duration);
		}

		public static global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector2, global::UnityEngine.Vector2, global::DG.Tweening.Plugins.Options.VectorOptions> To(global::DG.Tweening.Core.DOGetter<global::UnityEngine.Vector2> getter, global::DG.Tweening.Core.DOSetter<global::UnityEngine.Vector2> setter, global::UnityEngine.Vector2 endValue, float duration)
		{
			return ApplyTo<global::UnityEngine.Vector2, global::UnityEngine.Vector2, global::DG.Tweening.Plugins.Options.VectorOptions>(getter, setter, endValue, duration);
		}

		public static global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Options.VectorOptions> To(global::DG.Tweening.Core.DOGetter<global::UnityEngine.Vector3> getter, global::DG.Tweening.Core.DOSetter<global::UnityEngine.Vector3> setter, global::UnityEngine.Vector3 endValue, float duration)
		{
			return ApplyTo<global::UnityEngine.Vector3, global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Options.VectorOptions>(getter, setter, endValue, duration);
		}

		public static global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector4, global::UnityEngine.Vector4, global::DG.Tweening.Plugins.Options.VectorOptions> To(global::DG.Tweening.Core.DOGetter<global::UnityEngine.Vector4> getter, global::DG.Tweening.Core.DOSetter<global::UnityEngine.Vector4> setter, global::UnityEngine.Vector4 endValue, float duration)
		{
			return ApplyTo<global::UnityEngine.Vector4, global::UnityEngine.Vector4, global::DG.Tweening.Plugins.Options.VectorOptions>(getter, setter, endValue, duration);
		}

		public static global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Quaternion, global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Options.QuaternionOptions> To(global::DG.Tweening.Core.DOGetter<global::UnityEngine.Quaternion> getter, global::DG.Tweening.Core.DOSetter<global::UnityEngine.Quaternion> setter, global::UnityEngine.Vector3 endValue, float duration)
		{
			return ApplyTo<global::UnityEngine.Quaternion, global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Options.QuaternionOptions>(getter, setter, endValue, duration);
		}

		public static global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Color, global::UnityEngine.Color, global::DG.Tweening.Plugins.Options.ColorOptions> To(global::DG.Tweening.Core.DOGetter<global::UnityEngine.Color> getter, global::DG.Tweening.Core.DOSetter<global::UnityEngine.Color> setter, global::UnityEngine.Color endValue, float duration)
		{
			return ApplyTo<global::UnityEngine.Color, global::UnityEngine.Color, global::DG.Tweening.Plugins.Options.ColorOptions>(getter, setter, endValue, duration);
		}

		public static global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Rect, global::UnityEngine.Rect, global::DG.Tweening.Plugins.Options.RectOptions> To(global::DG.Tweening.Core.DOGetter<global::UnityEngine.Rect> getter, global::DG.Tweening.Core.DOSetter<global::UnityEngine.Rect> setter, global::UnityEngine.Rect endValue, float duration)
		{
			return ApplyTo<global::UnityEngine.Rect, global::UnityEngine.Rect, global::DG.Tweening.Plugins.Options.RectOptions>(getter, setter, endValue, duration);
		}

		public static global::DG.Tweening.Tweener To(global::DG.Tweening.Core.DOGetter<global::UnityEngine.RectOffset> getter, global::DG.Tweening.Core.DOSetter<global::UnityEngine.RectOffset> setter, global::UnityEngine.RectOffset endValue, float duration)
		{
			return ApplyTo<global::UnityEngine.RectOffset, global::UnityEngine.RectOffset, global::DG.Tweening.Plugins.Options.NoOptions>(getter, setter, endValue, duration);
		}

		public static global::DG.Tweening.Core.TweenerCore<T1, T2, TPlugOptions> To<T1, T2, TPlugOptions>(global::DG.Tweening.Plugins.Core.ABSTweenPlugin<T1, T2, TPlugOptions> plugin, global::DG.Tweening.Core.DOGetter<T1> getter, global::DG.Tweening.Core.DOSetter<T1> setter, T2 endValue, float duration) where TPlugOptions : struct, global::DG.Tweening.Plugins.Options.IPlugOptions
		{
			return ApplyTo(getter, setter, endValue, duration, plugin);
		}

		public static global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Options.VectorOptions> ToAxis(global::DG.Tweening.Core.DOGetter<global::UnityEngine.Vector3> getter, global::DG.Tweening.Core.DOSetter<global::UnityEngine.Vector3> setter, float endValue, float duration, global::DG.Tweening.AxisConstraint axisConstraint = global::DG.Tweening.AxisConstraint.X)
		{
			global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Options.VectorOptions> tweenerCore = ApplyTo<global::UnityEngine.Vector3, global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Options.VectorOptions>(getter, setter, new global::UnityEngine.Vector3(endValue, endValue, endValue), duration);
			tweenerCore.plugOptions.axisConstraint = axisConstraint;
			return tweenerCore;
		}

		public static global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Color, global::UnityEngine.Color, global::DG.Tweening.Plugins.Options.ColorOptions> ToAlpha(global::DG.Tweening.Core.DOGetter<global::UnityEngine.Color> getter, global::DG.Tweening.Core.DOSetter<global::UnityEngine.Color> setter, float endValue, float duration)
		{
			global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Color, global::UnityEngine.Color, global::DG.Tweening.Plugins.Options.ColorOptions> tweenerCore = ApplyTo<global::UnityEngine.Color, global::UnityEngine.Color, global::DG.Tweening.Plugins.Options.ColorOptions>(getter, setter, new global::UnityEngine.Color(0f, 0f, 0f, endValue), duration);
			tweenerCore.SetOptions(alphaOnly: true);
			return tweenerCore;
		}

		public static global::DG.Tweening.Tweener To(global::DG.Tweening.Core.DOSetter<float> setter, float startValue, float endValue, float duration)
		{
			return global::DG.Tweening.Core.Extensions.NoFrom(To(() => startValue, delegate(float x)
			{
				startValue = x;
				setter(startValue);
			}, endValue, duration));
		}

		public static global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::UnityEngine.Vector3[], global::DG.Tweening.Plugins.Options.Vector3ArrayOptions> Punch(global::DG.Tweening.Core.DOGetter<global::UnityEngine.Vector3> getter, global::DG.Tweening.Core.DOSetter<global::UnityEngine.Vector3> setter, global::UnityEngine.Vector3 direction, float duration, int vibrato = 10, float elasticity = 1f)
		{
			if (elasticity > 1f)
			{
				elasticity = 1f;
			}
			else if (elasticity < 0f)
			{
				elasticity = 0f;
			}
			float num = direction.magnitude;
			int num2 = (int)((float)vibrato * duration);
			if (num2 < 2)
			{
				num2 = 2;
			}
			float num3 = num / (float)num2;
			float[] array = new float[num2];
			float num4 = 0f;
			for (int i = 0; i < num2; i++)
			{
				float num5 = (float)(i + 1) / (float)num2;
				float num6 = duration * num5;
				num4 += num6;
				array[i] = num6;
			}
			float num7 = duration / num4;
			for (int j = 0; j < num2; j++)
			{
				array[j] *= num7;
			}
			global::UnityEngine.Vector3[] array2 = new global::UnityEngine.Vector3[num2];
			for (int k = 0; k < num2; k++)
			{
				if (k < num2 - 1)
				{
					if (k == 0)
					{
						array2[k] = direction;
					}
					else if (k % 2 != 0)
					{
						array2[k] = -global::UnityEngine.Vector3.ClampMagnitude(direction, num * elasticity);
					}
					else
					{
						array2[k] = global::UnityEngine.Vector3.ClampMagnitude(direction, num);
					}
					num -= num3;
				}
				else
				{
					array2[k] = global::UnityEngine.Vector3.zero;
				}
			}
			return global::DG.Tweening.Core.Extensions.SetSpecialStartupMode(global::DG.Tweening.Core.Extensions.NoFrom(ToArray(getter, setter, array2, array)), global::DG.Tweening.Core.Enums.SpecialStartupMode.SetPunch);
		}

		public static global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::UnityEngine.Vector3[], global::DG.Tweening.Plugins.Options.Vector3ArrayOptions> Shake(global::DG.Tweening.Core.DOGetter<global::UnityEngine.Vector3> getter, global::DG.Tweening.Core.DOSetter<global::UnityEngine.Vector3> setter, float duration, float strength = 3f, int vibrato = 10, float randomness = 90f, bool ignoreZAxis = true, bool fadeOut = true, global::DG.Tweening.ShakeRandomnessMode randomnessMode = global::DG.Tweening.ShakeRandomnessMode.Full)
		{
			return Shake(getter, setter, duration, new global::UnityEngine.Vector3(strength, strength, strength), vibrato, randomness, ignoreZAxis, vectorBased: false, fadeOut, randomnessMode);
		}

		public static global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::UnityEngine.Vector3[], global::DG.Tweening.Plugins.Options.Vector3ArrayOptions> Shake(global::DG.Tweening.Core.DOGetter<global::UnityEngine.Vector3> getter, global::DG.Tweening.Core.DOSetter<global::UnityEngine.Vector3> setter, float duration, global::UnityEngine.Vector3 strength, int vibrato = 10, float randomness = 90f, bool fadeOut = true, global::DG.Tweening.ShakeRandomnessMode randomnessMode = global::DG.Tweening.ShakeRandomnessMode.Full)
		{
			return Shake(getter, setter, duration, strength, vibrato, randomness, ignoreZAxis: false, vectorBased: true, fadeOut, randomnessMode);
		}

		private static global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::UnityEngine.Vector3[], global::DG.Tweening.Plugins.Options.Vector3ArrayOptions> Shake(global::DG.Tweening.Core.DOGetter<global::UnityEngine.Vector3> getter, global::DG.Tweening.Core.DOSetter<global::UnityEngine.Vector3> setter, float duration, global::UnityEngine.Vector3 strength, int vibrato, float randomness, bool ignoreZAxis, bool vectorBased, bool fadeOut, global::DG.Tweening.ShakeRandomnessMode randomnessMode)
		{
			float num = (vectorBased ? strength.magnitude : strength.x);
			int num2 = (int)((float)vibrato * duration);
			if (num2 < 2)
			{
				num2 = 2;
			}
			float num3 = num / (float)num2;
			float[] array = new float[num2];
			float num4 = 0f;
			for (int i = 0; i < num2; i++)
			{
				float num5 = (float)(i + 1) / (float)num2;
				float num6 = (fadeOut ? (duration * num5) : (duration / (float)num2));
				num4 += num6;
				array[i] = num6;
			}
			float num7 = duration / num4;
			for (int j = 0; j < num2; j++)
			{
				array[j] *= num7;
			}
			float num8 = global::UnityEngine.Random.Range(0f, 360f);
			global::UnityEngine.Vector3[] array2 = new global::UnityEngine.Vector3[num2];
			for (int k = 0; k < num2; k++)
			{
				if (k < num2 - 1)
				{
					global::UnityEngine.Quaternion quaternion = global::UnityEngine.Quaternion.identity;
					if (randomnessMode == global::DG.Tweening.ShakeRandomnessMode.Harmonic)
					{
						if (k > 0)
						{
							num8 = num8 - 180f + global::UnityEngine.Random.Range(0f, randomness);
						}
						if (vectorBased || !ignoreZAxis)
						{
							quaternion = global::UnityEngine.Quaternion.AngleAxis(global::UnityEngine.Random.Range(0f, randomness), global::UnityEngine.Vector3.up);
						}
					}
					else
					{
						if (k > 0)
						{
							num8 = num8 - 180f + global::UnityEngine.Random.Range(0f - randomness, randomness);
						}
						if (vectorBased || !ignoreZAxis)
						{
							quaternion = global::UnityEngine.Quaternion.AngleAxis(global::UnityEngine.Random.Range(0f - randomness, randomness), global::UnityEngine.Vector3.up);
						}
					}
					if (vectorBased)
					{
						global::UnityEngine.Vector3 vector = quaternion * global::DG.Tweening.Core.DOTweenUtils.Vector3FromAngle(num8, num);
						vector.x = global::UnityEngine.Vector3.ClampMagnitude(vector, strength.x).x;
						vector.y = global::UnityEngine.Vector3.ClampMagnitude(vector, strength.y).y;
						vector.z = global::UnityEngine.Vector3.ClampMagnitude(vector, strength.z).z;
						vector = vector.normalized * num;
						array2[k] = vector;
						if (fadeOut)
						{
							num -= num3;
						}
						strength = global::UnityEngine.Vector3.ClampMagnitude(strength, num);
					}
					else
					{
						if (ignoreZAxis)
						{
							array2[k] = global::DG.Tweening.Core.DOTweenUtils.Vector3FromAngle(num8, num);
						}
						else
						{
							array2[k] = quaternion * global::DG.Tweening.Core.DOTweenUtils.Vector3FromAngle(num8, num);
						}
						if (fadeOut)
						{
							num -= num3;
						}
					}
				}
				else
				{
					array2[k] = global::UnityEngine.Vector3.zero;
				}
			}
			return global::DG.Tweening.Core.Extensions.SetSpecialStartupMode(global::DG.Tweening.Core.Extensions.NoFrom(ToArray(getter, setter, array2, array)), global::DG.Tweening.Core.Enums.SpecialStartupMode.SetShake);
		}

		public static global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::UnityEngine.Vector3[], global::DG.Tweening.Plugins.Options.Vector3ArrayOptions> ToArray(global::DG.Tweening.Core.DOGetter<global::UnityEngine.Vector3> getter, global::DG.Tweening.Core.DOSetter<global::UnityEngine.Vector3> setter, global::UnityEngine.Vector3[] endValues, float[] durations)
		{
			int num = durations.Length;
			if (num != endValues.Length)
			{
				global::DG.Tweening.Core.Debugger.LogError("To Vector3 array tween: endValues and durations arrays must have the same length");
				return null;
			}
			global::UnityEngine.Vector3[] array = new global::UnityEngine.Vector3[num];
			float[] array2 = new float[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = endValues[i];
				array2[i] = durations[i];
			}
			float num2 = 0f;
			for (int j = 0; j < num; j++)
			{
				num2 += array2[j];
			}
			global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::UnityEngine.Vector3[], global::DG.Tweening.Plugins.Options.Vector3ArrayOptions> tweenerCore = global::DG.Tweening.Core.Extensions.NoFrom(ApplyTo<global::UnityEngine.Vector3, global::UnityEngine.Vector3[], global::DG.Tweening.Plugins.Options.Vector3ArrayOptions>(getter, setter, array, num2));
			tweenerCore.plugOptions.durations = array2;
			return tweenerCore;
		}

		internal static global::DG.Tweening.Core.TweenerCore<global::DG.Tweening.Color2, global::DG.Tweening.Color2, global::DG.Tweening.Plugins.Options.ColorOptions> To(global::DG.Tweening.Core.DOGetter<global::DG.Tweening.Color2> getter, global::DG.Tweening.Core.DOSetter<global::DG.Tweening.Color2> setter, global::DG.Tweening.Color2 endValue, float duration)
		{
			return ApplyTo<global::DG.Tweening.Color2, global::DG.Tweening.Color2, global::DG.Tweening.Plugins.Options.ColorOptions>(getter, setter, endValue, duration);
		}

		public static global::DG.Tweening.Sequence Sequence()
		{
			InitCheck();
			global::DG.Tweening.Sequence sequence = global::DG.Tweening.Core.TweenManager.GetSequence();
			global::DG.Tweening.Sequence.Setup(sequence);
			return sequence;
		}

		public static global::DG.Tweening.Sequence Sequence(object target)
		{
			return Sequence().SetTarget(target);
		}

		public static int CompleteAll(bool withCallbacks = false)
		{
			return global::DG.Tweening.Core.TweenManager.FilteredOperation(global::DG.Tweening.Core.Enums.OperationType.Complete, global::DG.Tweening.Core.Enums.FilterType.All, null, optionalBool: false, withCallbacks ? 1 : 0);
		}

		public static int Complete(object targetOrId, bool withCallbacks = false)
		{
			if (targetOrId == null)
			{
				return 0;
			}
			return global::DG.Tweening.Core.TweenManager.FilteredOperation(global::DG.Tweening.Core.Enums.OperationType.Complete, global::DG.Tweening.Core.Enums.FilterType.TargetOrId, targetOrId, optionalBool: false, withCallbacks ? 1 : 0);
		}

		internal static int CompleteAndReturnKilledTot()
		{
			return global::DG.Tweening.Core.TweenManager.FilteredOperation(global::DG.Tweening.Core.Enums.OperationType.Complete, global::DG.Tweening.Core.Enums.FilterType.All, null, optionalBool: true, 0f);
		}

		internal static int CompleteAndReturnKilledTot(object targetOrId)
		{
			if (targetOrId == null)
			{
				return 0;
			}
			return global::DG.Tweening.Core.TweenManager.FilteredOperation(global::DG.Tweening.Core.Enums.OperationType.Complete, global::DG.Tweening.Core.Enums.FilterType.TargetOrId, targetOrId, optionalBool: true, 0f);
		}

		internal static int CompleteAndReturnKilledTot(object target, object id)
		{
			if (target == null || id == null)
			{
				return 0;
			}
			return global::DG.Tweening.Core.TweenManager.FilteredOperation(global::DG.Tweening.Core.Enums.OperationType.Complete, global::DG.Tweening.Core.Enums.FilterType.TargetAndId, id, optionalBool: true, 0f, target);
		}

		internal static int CompleteAndReturnKilledTotExceptFor(params object[] excludeTargetsOrIds)
		{
			return global::DG.Tweening.Core.TweenManager.FilteredOperation(global::DG.Tweening.Core.Enums.OperationType.Complete, global::DG.Tweening.Core.Enums.FilterType.AllExceptTargetsOrIds, null, optionalBool: true, 0f, null, excludeTargetsOrIds);
		}

		public static int FlipAll()
		{
			return global::DG.Tweening.Core.TweenManager.FilteredOperation(global::DG.Tweening.Core.Enums.OperationType.Flip, global::DG.Tweening.Core.Enums.FilterType.All, null, optionalBool: false, 0f);
		}

		public static int Flip(object targetOrId)
		{
			if (targetOrId == null)
			{
				return 0;
			}
			return global::DG.Tweening.Core.TweenManager.FilteredOperation(global::DG.Tweening.Core.Enums.OperationType.Flip, global::DG.Tweening.Core.Enums.FilterType.TargetOrId, targetOrId, optionalBool: false, 0f);
		}

		public static int GotoAll(float to, bool andPlay = false)
		{
			return global::DG.Tweening.Core.TweenManager.FilteredOperation(global::DG.Tweening.Core.Enums.OperationType.Goto, global::DG.Tweening.Core.Enums.FilterType.All, null, andPlay, to);
		}

		public static int Goto(object targetOrId, float to, bool andPlay = false)
		{
			if (targetOrId == null)
			{
				return 0;
			}
			return global::DG.Tweening.Core.TweenManager.FilteredOperation(global::DG.Tweening.Core.Enums.OperationType.Goto, global::DG.Tweening.Core.Enums.FilterType.TargetOrId, targetOrId, andPlay, to);
		}

		public static int KillAll(bool complete = false)
		{
			return (complete ? CompleteAndReturnKilledTot() : 0) + global::DG.Tweening.Core.TweenManager.DespawnAll();
		}

		public static int KillAll(bool complete, params object[] idsOrTargetsToExclude)
		{
			if (idsOrTargetsToExclude == null)
			{
				return (complete ? CompleteAndReturnKilledTot() : 0) + global::DG.Tweening.Core.TweenManager.DespawnAll();
			}
			return (complete ? CompleteAndReturnKilledTotExceptFor(idsOrTargetsToExclude) : 0) + global::DG.Tweening.Core.TweenManager.FilteredOperation(global::DG.Tweening.Core.Enums.OperationType.Despawn, global::DG.Tweening.Core.Enums.FilterType.AllExceptTargetsOrIds, null, optionalBool: false, 0f, null, idsOrTargetsToExclude);
		}

		public static int Kill(object targetOrId, bool complete = false)
		{
			if (targetOrId == null)
			{
				return 0;
			}
			return (complete ? CompleteAndReturnKilledTot(targetOrId) : 0) + global::DG.Tweening.Core.TweenManager.FilteredOperation(global::DG.Tweening.Core.Enums.OperationType.Despawn, global::DG.Tweening.Core.Enums.FilterType.TargetOrId, targetOrId, optionalBool: false, 0f);
		}

		public static int Kill(object target, object id, bool complete = false)
		{
			if (target == null || id == null)
			{
				return 0;
			}
			return (complete ? CompleteAndReturnKilledTot(target, id) : 0) + global::DG.Tweening.Core.TweenManager.FilteredOperation(global::DG.Tweening.Core.Enums.OperationType.Despawn, global::DG.Tweening.Core.Enums.FilterType.TargetAndId, id, optionalBool: false, 0f, target);
		}

		public static int PauseAll()
		{
			return global::DG.Tweening.Core.TweenManager.FilteredOperation(global::DG.Tweening.Core.Enums.OperationType.Pause, global::DG.Tweening.Core.Enums.FilterType.All, null, optionalBool: false, 0f);
		}

		public static int Pause(object targetOrId)
		{
			if (targetOrId == null)
			{
				return 0;
			}
			return global::DG.Tweening.Core.TweenManager.FilteredOperation(global::DG.Tweening.Core.Enums.OperationType.Pause, global::DG.Tweening.Core.Enums.FilterType.TargetOrId, targetOrId, optionalBool: false, 0f);
		}

		public static int PlayAll()
		{
			return global::DG.Tweening.Core.TweenManager.FilteredOperation(global::DG.Tweening.Core.Enums.OperationType.Play, global::DG.Tweening.Core.Enums.FilterType.All, null, optionalBool: false, 0f);
		}

		public static int Play(object targetOrId)
		{
			if (targetOrId == null)
			{
				return 0;
			}
			return global::DG.Tweening.Core.TweenManager.FilteredOperation(global::DG.Tweening.Core.Enums.OperationType.Play, global::DG.Tweening.Core.Enums.FilterType.TargetOrId, targetOrId, optionalBool: false, 0f);
		}

		public static int Play(object target, object id)
		{
			if (target == null || id == null)
			{
				return 0;
			}
			return global::DG.Tweening.Core.TweenManager.FilteredOperation(global::DG.Tweening.Core.Enums.OperationType.Play, global::DG.Tweening.Core.Enums.FilterType.TargetAndId, id, optionalBool: false, 0f, target);
		}

		public static int PlayBackwardsAll()
		{
			return global::DG.Tweening.Core.TweenManager.FilteredOperation(global::DG.Tweening.Core.Enums.OperationType.PlayBackwards, global::DG.Tweening.Core.Enums.FilterType.All, null, optionalBool: false, 0f);
		}

		public static int PlayBackwards(object targetOrId)
		{
			if (targetOrId == null)
			{
				return 0;
			}
			return global::DG.Tweening.Core.TweenManager.FilteredOperation(global::DG.Tweening.Core.Enums.OperationType.PlayBackwards, global::DG.Tweening.Core.Enums.FilterType.TargetOrId, targetOrId, optionalBool: false, 0f);
		}

		public static int PlayBackwards(object target, object id)
		{
			if (target == null || id == null)
			{
				return 0;
			}
			return global::DG.Tweening.Core.TweenManager.FilteredOperation(global::DG.Tweening.Core.Enums.OperationType.PlayBackwards, global::DG.Tweening.Core.Enums.FilterType.TargetAndId, id, optionalBool: false, 0f, target);
		}

		public static int PlayForwardAll()
		{
			return global::DG.Tweening.Core.TweenManager.FilteredOperation(global::DG.Tweening.Core.Enums.OperationType.PlayForward, global::DG.Tweening.Core.Enums.FilterType.All, null, optionalBool: false, 0f);
		}

		public static int PlayForward(object targetOrId)
		{
			if (targetOrId == null)
			{
				return 0;
			}
			return global::DG.Tweening.Core.TweenManager.FilteredOperation(global::DG.Tweening.Core.Enums.OperationType.PlayForward, global::DG.Tweening.Core.Enums.FilterType.TargetOrId, targetOrId, optionalBool: false, 0f);
		}

		public static int PlayForward(object target, object id)
		{
			if (target == null || id == null)
			{
				return 0;
			}
			return global::DG.Tweening.Core.TweenManager.FilteredOperation(global::DG.Tweening.Core.Enums.OperationType.PlayForward, global::DG.Tweening.Core.Enums.FilterType.TargetAndId, id, optionalBool: false, 0f, target);
		}

		public static int RestartAll(bool includeDelay = true)
		{
			return global::DG.Tweening.Core.TweenManager.FilteredOperation(global::DG.Tweening.Core.Enums.OperationType.Restart, global::DG.Tweening.Core.Enums.FilterType.All, null, includeDelay, 0f);
		}

		public static int Restart(object targetOrId, bool includeDelay = true, float changeDelayTo = -1f)
		{
			if (targetOrId == null)
			{
				return 0;
			}
			return global::DG.Tweening.Core.TweenManager.FilteredOperation(global::DG.Tweening.Core.Enums.OperationType.Restart, global::DG.Tweening.Core.Enums.FilterType.TargetOrId, targetOrId, includeDelay, changeDelayTo);
		}

		public static int Restart(object target, object id, bool includeDelay = true, float changeDelayTo = -1f)
		{
			if (target == null || id == null)
			{
				return 0;
			}
			return global::DG.Tweening.Core.TweenManager.FilteredOperation(global::DG.Tweening.Core.Enums.OperationType.Restart, global::DG.Tweening.Core.Enums.FilterType.TargetAndId, id, includeDelay, changeDelayTo, target);
		}

		public static int RewindAll(bool includeDelay = true)
		{
			return global::DG.Tweening.Core.TweenManager.FilteredOperation(global::DG.Tweening.Core.Enums.OperationType.Rewind, global::DG.Tweening.Core.Enums.FilterType.All, null, includeDelay, 0f);
		}

		public static int Rewind(object targetOrId, bool includeDelay = true)
		{
			if (targetOrId == null)
			{
				return 0;
			}
			return global::DG.Tweening.Core.TweenManager.FilteredOperation(global::DG.Tweening.Core.Enums.OperationType.Rewind, global::DG.Tweening.Core.Enums.FilterType.TargetOrId, targetOrId, includeDelay, 0f);
		}

		public static int SmoothRewindAll()
		{
			return global::DG.Tweening.Core.TweenManager.FilteredOperation(global::DG.Tweening.Core.Enums.OperationType.SmoothRewind, global::DG.Tweening.Core.Enums.FilterType.All, null, optionalBool: false, 0f);
		}

		public static int SmoothRewind(object targetOrId)
		{
			if (targetOrId == null)
			{
				return 0;
			}
			return global::DG.Tweening.Core.TweenManager.FilteredOperation(global::DG.Tweening.Core.Enums.OperationType.SmoothRewind, global::DG.Tweening.Core.Enums.FilterType.TargetOrId, targetOrId, optionalBool: false, 0f);
		}

		public static int TogglePauseAll()
		{
			return global::DG.Tweening.Core.TweenManager.FilteredOperation(global::DG.Tweening.Core.Enums.OperationType.TogglePause, global::DG.Tweening.Core.Enums.FilterType.All, null, optionalBool: false, 0f);
		}

		public static int TogglePause(object targetOrId)
		{
			if (targetOrId == null)
			{
				return 0;
			}
			return global::DG.Tweening.Core.TweenManager.FilteredOperation(global::DG.Tweening.Core.Enums.OperationType.TogglePause, global::DG.Tweening.Core.Enums.FilterType.TargetOrId, targetOrId, optionalBool: false, 0f);
		}

		public static bool IsTweening(object targetOrId, bool alsoCheckIfIsPlaying = false)
		{
			return global::DG.Tweening.Core.TweenManager.FilteredOperation(global::DG.Tweening.Core.Enums.OperationType.IsTweening, global::DG.Tweening.Core.Enums.FilterType.TargetOrId, targetOrId, alsoCheckIfIsPlaying, 0f) > 0;
		}

		public static int TotalActiveTweens()
		{
			return global::DG.Tweening.Core.TweenManager.totActiveTweens;
		}

		public static int TotalActiveTweeners()
		{
			return global::DG.Tweening.Core.TweenManager.totActiveTweeners;
		}

		public static int TotalActiveSequences()
		{
			return global::DG.Tweening.Core.TweenManager.totActiveSequences;
		}

		public static int TotalPlayingTweens()
		{
			return global::DG.Tweening.Core.TweenManager.TotalPlayingTweens();
		}

		public static int TotalTweensById(object id, bool playingOnly = false)
		{
			if (id == null)
			{
				return 0;
			}
			return global::DG.Tweening.Core.TweenManager.TotalTweensById(id, playingOnly);
		}

		public static global::System.Collections.Generic.List<global::DG.Tweening.Tween> PlayingTweens(global::System.Collections.Generic.List<global::DG.Tweening.Tween> fillableList = null)
		{
			fillableList?.Clear();
			return global::DG.Tweening.Core.TweenManager.GetActiveTweens(playing: true, fillableList);
		}

		public static global::System.Collections.Generic.List<global::DG.Tweening.Tween> PausedTweens(global::System.Collections.Generic.List<global::DG.Tweening.Tween> fillableList = null)
		{
			fillableList?.Clear();
			return global::DG.Tweening.Core.TweenManager.GetActiveTweens(playing: false, fillableList);
		}

		public static global::System.Collections.Generic.List<global::DG.Tweening.Tween> TweensById(object id, bool playingOnly = false, global::System.Collections.Generic.List<global::DG.Tweening.Tween> fillableList = null)
		{
			if (id == null)
			{
				return null;
			}
			fillableList?.Clear();
			return global::DG.Tweening.Core.TweenManager.GetTweensById(id, playingOnly, fillableList);
		}

		public static global::System.Collections.Generic.List<global::DG.Tweening.Tween> TweensByTarget(object target, bool playingOnly = false, global::System.Collections.Generic.List<global::DG.Tweening.Tween> fillableList = null)
		{
			fillableList?.Clear();
			return global::DG.Tweening.Core.TweenManager.GetTweensByTarget(target, playingOnly, fillableList);
		}

		private static void InitCheck()
		{
			if (!initialized && global::UnityEngine.Application.isPlaying && !isQuitting)
			{
				AutoInit();
			}
		}

		private static global::DG.Tweening.Core.TweenerCore<T1, T2, TPlugOptions> ApplyTo<T1, T2, TPlugOptions>(global::DG.Tweening.Core.DOGetter<T1> getter, global::DG.Tweening.Core.DOSetter<T1> setter, T2 endValue, float duration, global::DG.Tweening.Plugins.Core.ABSTweenPlugin<T1, T2, TPlugOptions> plugin = null) where TPlugOptions : struct, global::DG.Tweening.Plugins.Options.IPlugOptions
		{
			InitCheck();
			global::DG.Tweening.Core.TweenerCore<T1, T2, TPlugOptions> tweener = global::DG.Tweening.Core.TweenManager.GetTweener<T1, T2, TPlugOptions>();
			if (!global::DG.Tweening.Tweener.Setup(tweener, getter, setter, endValue, duration, plugin))
			{
				global::DG.Tweening.Core.TweenManager.Despawn(tweener);
				return null;
			}
			return tweener;
		}
	}
}
