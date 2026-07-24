namespace UnityEngine.Timeline
{
	public class TimeControlPlayable : global::UnityEngine.Playables.PlayableBehaviour
	{
		private global::UnityEngine.Timeline.ITimeControl m_timeControl;

		private bool m_started;

		public static global::UnityEngine.Playables.ScriptPlayable<global::UnityEngine.Timeline.TimeControlPlayable> Create(global::UnityEngine.Playables.PlayableGraph graph, global::UnityEngine.Timeline.ITimeControl timeControl)
		{
			if (timeControl == null)
			{
				return global::UnityEngine.Playables.ScriptPlayable<global::UnityEngine.Timeline.TimeControlPlayable>.Null;
			}
			global::UnityEngine.Playables.ScriptPlayable<global::UnityEngine.Timeline.TimeControlPlayable> result = global::UnityEngine.Playables.ScriptPlayable<global::UnityEngine.Timeline.TimeControlPlayable>.Create(graph);
			result.GetBehaviour().Initialize(timeControl);
			return result;
		}

		public void Initialize(global::UnityEngine.Timeline.ITimeControl timeControl)
		{
			m_timeControl = timeControl;
		}

		public override void PrepareFrame(global::UnityEngine.Playables.Playable playable, global::UnityEngine.Playables.FrameData info)
		{
			if (m_timeControl != null)
			{
				m_timeControl.SetTime(global::UnityEngine.Playables.PlayableExtensions.GetTime(playable));
			}
		}

		public override void OnBehaviourPlay(global::UnityEngine.Playables.Playable playable, global::UnityEngine.Playables.FrameData info)
		{
			if (m_timeControl != null && !m_started)
			{
				m_timeControl.OnControlTimeStart();
				m_started = true;
			}
		}

		public override void OnBehaviourPause(global::UnityEngine.Playables.Playable playable, global::UnityEngine.Playables.FrameData info)
		{
			if (m_timeControl != null && m_started)
			{
				m_timeControl.OnControlTimeStop();
				m_started = false;
			}
		}
	}
}
