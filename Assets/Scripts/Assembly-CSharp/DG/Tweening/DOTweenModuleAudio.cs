namespace DG.Tweening
{
	public static class DOTweenModuleAudio
	{
		public static global::DG.Tweening.Core.TweenerCore<float, float, global::DG.Tweening.Plugins.Options.FloatOptions> DOFade(this global::UnityEngine.AudioSource target, float endValue, float duration)
		{
			if (endValue < 0f)
			{
				endValue = 0f;
			}
			else if (endValue > 1f)
			{
				endValue = 1f;
			}
			global::DG.Tweening.Core.TweenerCore<float, float, global::DG.Tweening.Plugins.Options.FloatOptions> tweenerCore = global::DG.Tweening.DOTween.To(() => target.volume, delegate(float x)
			{
				target.volume = x;
			}, endValue, duration);
			tweenerCore.SetTarget(target);
			return tweenerCore;
		}

		public static global::DG.Tweening.Core.TweenerCore<float, float, global::DG.Tweening.Plugins.Options.FloatOptions> DOPitch(this global::UnityEngine.AudioSource target, float endValue, float duration)
		{
			global::DG.Tweening.Core.TweenerCore<float, float, global::DG.Tweening.Plugins.Options.FloatOptions> tweenerCore = global::DG.Tweening.DOTween.To(() => target.pitch, delegate(float x)
			{
				target.pitch = x;
			}, endValue, duration);
			tweenerCore.SetTarget(target);
			return tweenerCore;
		}

		public static global::DG.Tweening.Core.TweenerCore<float, float, global::DG.Tweening.Plugins.Options.FloatOptions> DOSetFloat(this global::UnityEngine.Audio.AudioMixer target, string floatName, float endValue, float duration)
		{
			global::DG.Tweening.Core.TweenerCore<float, float, global::DG.Tweening.Plugins.Options.FloatOptions> tweenerCore = global::DG.Tweening.DOTween.To(delegate
			{
				target.GetFloat(floatName, out var value);
				return value;
			}, delegate(float x)
			{
				target.SetFloat(floatName, x);
			}, endValue, duration);
			tweenerCore.SetTarget(target);
			return tweenerCore;
		}

		public static int DOComplete(this global::UnityEngine.Audio.AudioMixer target, bool withCallbacks = false)
		{
			return global::DG.Tweening.DOTween.Complete(target, withCallbacks);
		}

		public static int DOKill(this global::UnityEngine.Audio.AudioMixer target, bool complete = false)
		{
			return global::DG.Tweening.DOTween.Kill(target, complete);
		}

		public static int DOFlip(this global::UnityEngine.Audio.AudioMixer target)
		{
			return global::DG.Tweening.DOTween.Flip(target);
		}

		public static int DOGoto(this global::UnityEngine.Audio.AudioMixer target, float to, bool andPlay = false)
		{
			return global::DG.Tweening.DOTween.Goto(target, to, andPlay);
		}

		public static int DOPause(this global::UnityEngine.Audio.AudioMixer target)
		{
			return global::DG.Tweening.DOTween.Pause(target);
		}

		public static int DOPlay(this global::UnityEngine.Audio.AudioMixer target)
		{
			return global::DG.Tweening.DOTween.Play(target);
		}

		public static int DOPlayBackwards(this global::UnityEngine.Audio.AudioMixer target)
		{
			return global::DG.Tweening.DOTween.PlayBackwards(target);
		}

		public static int DOPlayForward(this global::UnityEngine.Audio.AudioMixer target)
		{
			return global::DG.Tweening.DOTween.PlayForward(target);
		}

		public static int DORestart(this global::UnityEngine.Audio.AudioMixer target)
		{
			return global::DG.Tweening.DOTween.Restart(target);
		}

		public static int DORewind(this global::UnityEngine.Audio.AudioMixer target)
		{
			return global::DG.Tweening.DOTween.Rewind(target);
		}

		public static int DOSmoothRewind(this global::UnityEngine.Audio.AudioMixer target)
		{
			return global::DG.Tweening.DOTween.SmoothRewind(target);
		}

		public static int DOTogglePause(this global::UnityEngine.Audio.AudioMixer target)
		{
			return global::DG.Tweening.DOTween.TogglePause(target);
		}
	}
}
