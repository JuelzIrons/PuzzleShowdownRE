namespace UnityEngine.Timeline
{
	internal class ScheduleRuntimeClip : global::UnityEngine.Timeline.RuntimeClipBase
	{
		private global::UnityEngine.Timeline.TimelineClip m_Clip;

		private global::UnityEngine.Playables.Playable m_Playable;

		private global::UnityEngine.Playables.Playable m_ParentMixer;

		private double m_StartDelay;

		private double m_FinishTail;

		private bool m_Started;

		public override double start => global::System.Math.Max(0.0, m_Clip.start - m_StartDelay);

		public override double duration => m_Clip.duration + m_FinishTail + m_Clip.start - start;

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
				}
				else if (!value && global::UnityEngine.Playables.PlayableExtensions.GetPlayState(m_Playable) != global::UnityEngine.Playables.PlayState.Paused)
				{
					global::UnityEngine.Playables.PlayableExtensions.Pause(m_Playable);
					if (global::UnityEngine.Playables.PlayableExtensions.IsValid(m_ParentMixer))
					{
						global::UnityEngine.Playables.PlayableExtensions.SetInputWeight(m_ParentMixer, m_Playable, 0f);
					}
				}
				m_Started &= value;
			}
		}

		public void SetTime(double time)
		{
			global::UnityEngine.Playables.PlayableExtensions.SetTime(m_Playable, time);
		}

		public ScheduleRuntimeClip(global::UnityEngine.Timeline.TimelineClip clip, global::UnityEngine.Playables.Playable clipPlayable, global::UnityEngine.Playables.Playable parentMixer, double startDelay = 0.2, double finishTail = 0.1)
		{
			Create(clip, clipPlayable, parentMixer, startDelay, finishTail);
		}

		private void Create(global::UnityEngine.Timeline.TimelineClip clip, global::UnityEngine.Playables.Playable clipPlayable, global::UnityEngine.Playables.Playable parentMixer, double startDelay, double finishTail)
		{
			m_Clip = clip;
			m_Playable = clipPlayable;
			m_ParentMixer = parentMixer;
			m_StartDelay = startDelay;
			m_FinishTail = finishTail;
			global::UnityEngine.Playables.PlayableExtensions.Pause(clipPlayable);
		}

		public override void EvaluateAt(double localTime, global::UnityEngine.Playables.FrameData frameData)
		{
			if (frameData.timeHeld)
			{
				enable = false;
				return;
			}
			bool flag = frameData.seekOccurred || frameData.timeLooped || frameData.evaluationType == global::UnityEngine.Playables.FrameData.EvaluationType.Evaluate;
			if (localTime > start + duration - m_FinishTail)
			{
				return;
			}
			float weight = clip.EvaluateMixIn(localTime) * clip.EvaluateMixOut(localTime);
			if (global::UnityEngine.Playables.PlayableExtensions.IsValid(mixer))
			{
				global::UnityEngine.Playables.PlayableExtensions.SetInputWeight(mixer, playable, weight);
			}
			if (!m_Started || flag)
			{
				double startTime = clip.ToLocalTime(global::System.Math.Max(localTime, clip.start));
				double startDelay = global::System.Math.Max(clip.start - localTime, 0.0) * clip.timeScale;
				double num = m_Clip.duration * clip.timeScale;
				if (m_Playable.IsPlayableOfType<global::UnityEngine.Audio.AudioClipPlayable>())
				{
					((global::UnityEngine.Audio.AudioClipPlayable)m_Playable).Seek(startTime, startDelay, num);
				}
				m_Started = true;
			}
		}

		public override void DisableAt(double localTime, double rootDuration, global::UnityEngine.Playables.FrameData frameData)
		{
			enable = false;
		}
	}
}
