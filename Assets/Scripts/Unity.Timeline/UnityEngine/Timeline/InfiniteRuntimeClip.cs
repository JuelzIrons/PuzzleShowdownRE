namespace UnityEngine.Timeline
{
	internal class InfiniteRuntimeClip : global::UnityEngine.Timeline.RuntimeElement
	{
		private global::UnityEngine.Playables.Playable m_Playable;

		private static readonly long kIntervalEnd = global::UnityEngine.Timeline.DiscreteTime.GetNearestTick(global::UnityEngine.Timeline.TimelineClip.kMaxTimeValue);

		public override long intervalStart => 0L;

		public override long intervalEnd => kIntervalEnd;

		public override bool enable
		{
			set
			{
				if (value)
				{
					global::UnityEngine.Playables.PlayableExtensions.Play(m_Playable);
				}
				else
				{
					global::UnityEngine.Playables.PlayableExtensions.Pause(m_Playable);
				}
			}
		}

		public InfiniteRuntimeClip(global::UnityEngine.Playables.Playable playable)
		{
			m_Playable = playable;
		}

		public override void EvaluateAt(double localTime, global::UnityEngine.Playables.FrameData frameData)
		{
			global::UnityEngine.Playables.PlayableExtensions.SetTime(m_Playable, localTime);
		}

		public override void DisableAt(double localTime, double rootDuration, global::UnityEngine.Playables.FrameData frameData)
		{
			global::UnityEngine.Playables.PlayableExtensions.SetTime(m_Playable, localTime);
			enable = false;
		}
	}
}
