namespace UnityEngine.Timeline
{
	internal class RuntimeClip : global::UnityEngine.Timeline.RuntimeClipBase
	{
		private global::UnityEngine.Timeline.TimelineClip m_Clip;

		private global::UnityEngine.Playables.Playable m_Playable;

		private global::UnityEngine.Playables.Playable m_ParentMixer;

		public override double start => m_Clip.extrapolatedStart;

		public override double duration => m_Clip.extrapolatedDuration;

		public global::UnityEngine.Timeline.TimelineClip clip => m_Clip;

		public global::UnityEngine.Playables.Playable mixer => m_ParentMixer;

		public global::UnityEngine.Playables.Playable playable => m_Playable;

		public override bool enable
		{
			set
			{
				if (value && global::UnityEngine.Playables.PlayableExtensions.GetPlayState(m_Playable) != global::UnityEngine.Playables.PlayState.Playing)
				{
					global::UnityEngine.Playables.PlayableExtensions.Play(m_Playable);
					SetTime(m_Clip.clipIn);
				}
				else if (!value && global::UnityEngine.Playables.PlayableExtensions.GetPlayState(m_Playable) != global::UnityEngine.Playables.PlayState.Paused)
				{
					global::UnityEngine.Playables.PlayableExtensions.Pause(m_Playable);
					if (global::UnityEngine.Playables.PlayableExtensions.IsValid(m_ParentMixer))
					{
						global::UnityEngine.Playables.PlayableExtensions.SetInputWeight(m_ParentMixer, m_Playable, 0f);
					}
				}
			}
		}

		public RuntimeClip(global::UnityEngine.Timeline.TimelineClip clip, global::UnityEngine.Playables.Playable clipPlayable, global::UnityEngine.Playables.Playable parentMixer)
		{
			Create(clip, clipPlayable, parentMixer);
		}

		private void Create(global::UnityEngine.Timeline.TimelineClip clip, global::UnityEngine.Playables.Playable clipPlayable, global::UnityEngine.Playables.Playable parentMixer)
		{
			m_Clip = clip;
			m_Playable = clipPlayable;
			m_ParentMixer = parentMixer;
			global::UnityEngine.Playables.PlayableExtensions.Pause(clipPlayable);
		}

		public void SetTime(double time)
		{
			global::UnityEngine.Playables.PlayableExtensions.SetTime(m_Playable, time);
		}

		public void SetDuration(double duration)
		{
			global::UnityEngine.Playables.PlayableExtensions.SetDuration(m_Playable, duration);
		}

		public override void EvaluateAt(double localTime, global::UnityEngine.Playables.FrameData frameData)
		{
			enable = true;
			if (frameData.timeLooped)
			{
				SetTime(clip.clipIn);
				SetTime(clip.clipIn);
			}
			float num = 1f;
			num = (clip.IsPreExtrapolatedTime(localTime) ? clip.EvaluateMixIn((float)clip.start) : ((!clip.IsPostExtrapolatedTime(localTime)) ? (clip.EvaluateMixIn(localTime) * clip.EvaluateMixOut(localTime)) : clip.EvaluateMixOut((float)clip.end)));
			if (global::UnityEngine.Playables.PlayableExtensions.IsValid(mixer))
			{
				global::UnityEngine.Playables.PlayableExtensions.SetInputWeight(mixer, playable, num);
			}
			double num2 = clip.ToLocalTime(localTime);
			if (num2 >= (0.0 - global::UnityEngine.Timeline.DiscreteTime.tickValue) / 2.0)
			{
				SetTime(num2);
			}
			SetDuration(clip.extrapolatedDuration);
		}

		public override void DisableAt(double localTime, double rootDuration, global::UnityEngine.Playables.FrameData frameData)
		{
			double num = global::System.Math.Min(localTime, (double)global::UnityEngine.Timeline.DiscreteTime.FromTicks(intervalEnd));
			if (frameData.timeLooped)
			{
				num = global::System.Math.Min(num, rootDuration);
			}
			double num2 = clip.ToLocalTime(num);
			if (num2 > (0.0 - global::UnityEngine.Timeline.DiscreteTime.tickValue) / 2.0)
			{
				SetTime(num2);
			}
			enable = false;
		}
	}
}
