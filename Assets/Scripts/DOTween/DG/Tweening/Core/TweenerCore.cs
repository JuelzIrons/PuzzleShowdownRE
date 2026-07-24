namespace DG.Tweening.Core
{
	public class TweenerCore<T1, T2, TPlugOptions> : global::DG.Tweening.Tweener where TPlugOptions : struct, global::DG.Tweening.Plugins.Options.IPlugOptions
	{
		public T2 startValue;

		public T2 endValue;

		public T2 changeValue;

		public TPlugOptions plugOptions;

		public global::DG.Tweening.Core.DOGetter<T1> getter;

		public global::DG.Tweening.Core.DOSetter<T1> setter;

		internal global::DG.Tweening.Plugins.Core.ABSTweenPlugin<T1, T2, TPlugOptions> tweenPlugin;

		private const string _TxtCantChangeSequencedValues = "You cannot change the values of a tween contained inside a Sequence";

		private global::System.Type _colorType = typeof(global::UnityEngine.Color);

		private global::System.Type _color32Type = typeof(global::UnityEngine.Color32);

		internal TweenerCore()
		{
			typeofT1 = typeof(T1);
			typeofT2 = typeof(T2);
			typeofTPlugOptions = typeof(TPlugOptions);
			tweenType = global::DG.Tweening.TweenType.Tweener;
			Reset();
		}

		public override global::DG.Tweening.Tweener ChangeStartValue(object newStartValue, float newDuration = -1f)
		{
			if (isSequenced)
			{
				global::DG.Tweening.Core.Debugger.LogError("You cannot change the values of a tween contained inside a Sequence", this);
				return this;
			}
			global::System.Type type = newStartValue.GetType();
			if (!ValidateChangeValueType(type, out var isColor32ToColor))
			{
				global::DG.Tweening.Core.Debugger.LogError("ChangeStartValue: incorrect newStartValue type (is " + type?.ToString() + ", should be " + typeofT2?.ToString() + ")", this);
				return this;
			}
			if (isColor32ToColor)
			{
				return global::DG.Tweening.Tweener.DoChangeStartValue(this, (T2)(object)(global::UnityEngine.Color)(global::UnityEngine.Color32)newStartValue, newDuration);
			}
			return global::DG.Tweening.Tweener.DoChangeStartValue(this, (T2)newStartValue, newDuration);
		}

		public override global::DG.Tweening.Tweener ChangeEndValue(object newEndValue, bool snapStartValue)
		{
			return ChangeEndValue(newEndValue, -1f, snapStartValue);
		}

		public override global::DG.Tweening.Tweener ChangeEndValue(object newEndValue, float newDuration = -1f, bool snapStartValue = false)
		{
			if (isSequenced)
			{
				global::DG.Tweening.Core.Debugger.LogError("You cannot change the values of a tween contained inside a Sequence", this);
				return this;
			}
			global::System.Type type = newEndValue.GetType();
			if (!ValidateChangeValueType(type, out var isColor32ToColor))
			{
				global::DG.Tweening.Core.Debugger.LogError("ChangeEndValue: incorrect newEndValue type (is " + type?.ToString() + ", should be " + typeofT2?.ToString() + ")", this);
				return this;
			}
			if (isColor32ToColor)
			{
				return global::DG.Tweening.Tweener.DoChangeEndValue(this, (T2)(object)(global::UnityEngine.Color)(global::UnityEngine.Color32)newEndValue, newDuration, snapStartValue);
			}
			return global::DG.Tweening.Tweener.DoChangeEndValue(this, (T2)newEndValue, newDuration, snapStartValue);
		}

		public override global::DG.Tweening.Tweener ChangeValues(object newStartValue, object newEndValue, float newDuration = -1f)
		{
			if (isSequenced)
			{
				global::DG.Tweening.Core.Debugger.LogError("You cannot change the values of a tween contained inside a Sequence", this);
				return this;
			}
			global::System.Type type = newStartValue.GetType();
			global::System.Type type2 = newEndValue.GetType();
			if (!ValidateChangeValueType(type, out var isColor32ToColor))
			{
				global::DG.Tweening.Core.Debugger.LogError("ChangeValues: incorrect value type (is " + type?.ToString() + ", should be " + typeofT2?.ToString() + ")", this);
				return this;
			}
			if (!ValidateChangeValueType(type2, out isColor32ToColor))
			{
				global::DG.Tweening.Core.Debugger.LogError("ChangeValues: incorrect value type (is " + type2?.ToString() + ", should be " + typeofT2?.ToString() + ")", this);
				return this;
			}
			if (isColor32ToColor)
			{
				return global::DG.Tweening.Tweener.DoChangeValues(this, (T2)(object)(global::UnityEngine.Color)(global::UnityEngine.Color32)newStartValue, (T2)(object)(global::UnityEngine.Color)(global::UnityEngine.Color32)newEndValue, newDuration);
			}
			return global::DG.Tweening.Tweener.DoChangeValues(this, (T2)newStartValue, (T2)newEndValue, newDuration);
		}

		public global::DG.Tweening.Core.TweenerCore<T1, T2, TPlugOptions> ChangeStartValue(T2 newStartValue, float newDuration = -1f)
		{
			if (isSequenced)
			{
				global::DG.Tweening.Core.Debugger.LogError("You cannot change the values of a tween contained inside a Sequence", this);
				return this;
			}
			return global::DG.Tweening.Tweener.DoChangeStartValue(this, newStartValue, newDuration);
		}

		public global::DG.Tweening.Core.TweenerCore<T1, T2, TPlugOptions> ChangeEndValue(T2 newEndValue, bool snapStartValue)
		{
			return ChangeEndValue(newEndValue, -1f, snapStartValue);
		}

		public global::DG.Tweening.Core.TweenerCore<T1, T2, TPlugOptions> ChangeEndValue(T2 newEndValue, float newDuration = -1f, bool snapStartValue = false)
		{
			if (isSequenced)
			{
				global::DG.Tweening.Core.Debugger.LogError("You cannot change the values of a tween contained inside a Sequence", this);
				return this;
			}
			return global::DG.Tweening.Tweener.DoChangeEndValue(this, newEndValue, newDuration, snapStartValue);
		}

		public global::DG.Tweening.Core.TweenerCore<T1, T2, TPlugOptions> ChangeValues(T2 newStartValue, T2 newEndValue, float newDuration = -1f)
		{
			if (isSequenced)
			{
				global::DG.Tweening.Core.Debugger.LogError("You cannot change the values of a tween contained inside a Sequence", this);
				return this;
			}
			return global::DG.Tweening.Tweener.DoChangeValues(this, newStartValue, newEndValue, newDuration);
		}

		internal override global::DG.Tweening.Tweener SetFrom(bool relative)
		{
			tweenPlugin.SetFrom(this, relative);
			hasManuallySetStartValue = true;
			return this;
		}

		internal global::DG.Tweening.Tweener SetFrom(T2 fromValue, bool setImmediately, bool relative)
		{
			tweenPlugin.SetFrom(this, fromValue, setImmediately, relative);
			hasManuallySetStartValue = true;
			return this;
		}

		internal sealed override void Reset()
		{
			base.Reset();
			if (tweenPlugin != null)
			{
				tweenPlugin.Reset(this);
			}
			plugOptions.Reset();
			getter = null;
			setter = null;
			hasManuallySetStartValue = false;
			isFromAllowed = true;
		}

		internal override bool Validate()
		{
			try
			{
				getter();
			}
			catch
			{
				return false;
			}
			return true;
		}

		private bool ValidateChangeValueType(global::System.Type newType, out bool isColor32ToColor)
		{
			if ((object)newType == typeofT2)
			{
				isColor32ToColor = false;
				return true;
			}
			if ((object)typeofT2 == _colorType && (object)newType == _color32Type)
			{
				isColor32ToColor = true;
				return true;
			}
			isColor32ToColor = false;
			return false;
		}

		internal override float UpdateDelay(float elapsed)
		{
			return global::DG.Tweening.Tweener.DoUpdateDelay(this, elapsed);
		}

		internal override bool Startup()
		{
			return global::DG.Tweening.Tweener.DoStartup(this);
		}

		internal override bool ApplyTween(float prevPosition, int prevCompletedLoops, int newCompletedSteps, bool useInversePosition, global::DG.Tweening.Core.Enums.UpdateMode updateMode, global::DG.Tweening.Core.Enums.UpdateNotice updateNotice)
		{
			if (isInverted)
			{
				useInversePosition = !useInversePosition;
			}
			float elapsed = (useInversePosition ? (duration - base.position) : base.position);
			if (global::DG.Tweening.DOTween.useSafeMode)
			{
				try
				{
					tweenPlugin.EvaluateAndApply(plugOptions, this, base.isRelative, getter, setter, elapsed, startValue, changeValue, duration, useInversePosition, newCompletedSteps, updateNotice);
				}
				catch (global::System.Exception ex)
				{
					if (global::DG.Tweening.Core.Debugger.ShouldLogSafeModeCapturedError())
					{
						global::DG.Tweening.Core.Debugger.LogSafeModeCapturedError($"Target or field is missing/null ({ex.TargetSite}) ► {ex.Message}\n\n{ex.StackTrace}\n\n", this);
					}
					global::DG.Tweening.DOTween.safeModeReport.Add(global::DG.Tweening.Core.SafeModeReport.SafeModeReportType.TargetOrFieldMissing);
					return true;
				}
			}
			else
			{
				tweenPlugin.EvaluateAndApply(plugOptions, this, base.isRelative, getter, setter, elapsed, startValue, changeValue, duration, useInversePosition, newCompletedSteps, updateNotice);
			}
			return false;
		}
	}
}
