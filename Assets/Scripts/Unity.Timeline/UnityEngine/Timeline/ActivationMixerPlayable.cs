namespace UnityEngine.Timeline
{
	internal class ActivationMixerPlayable : global::UnityEngine.Playables.PlayableBehaviour
	{
		private global::UnityEngine.Timeline.ActivationTrack.PostPlaybackState m_PostPlaybackState;

		private bool m_BoundGameObjectInitialStateIsActive;

		private global::UnityEngine.GameObject m_BoundGameObject;

		public global::UnityEngine.Timeline.ActivationTrack.PostPlaybackState postPlaybackState
		{
			get
			{
				return m_PostPlaybackState;
			}
			set
			{
				m_PostPlaybackState = value;
			}
		}

		public static global::UnityEngine.Playables.ScriptPlayable<global::UnityEngine.Timeline.ActivationMixerPlayable> Create(global::UnityEngine.Playables.PlayableGraph graph, int inputCount)
		{
			return global::UnityEngine.Playables.ScriptPlayable<global::UnityEngine.Timeline.ActivationMixerPlayable>.Create(graph, inputCount);
		}

		public override void OnPlayableDestroy(global::UnityEngine.Playables.Playable playable)
		{
			if (!(m_BoundGameObject == null))
			{
				switch (m_PostPlaybackState)
				{
				case global::UnityEngine.Timeline.ActivationTrack.PostPlaybackState.Active:
					m_BoundGameObject.SetActive(value: true);
					break;
				case global::UnityEngine.Timeline.ActivationTrack.PostPlaybackState.Inactive:
					m_BoundGameObject.SetActive(value: false);
					break;
				case global::UnityEngine.Timeline.ActivationTrack.PostPlaybackState.Revert:
					m_BoundGameObject.SetActive(m_BoundGameObjectInitialStateIsActive);
					break;
				case global::UnityEngine.Timeline.ActivationTrack.PostPlaybackState.LeaveAsIs:
					break;
				}
			}
		}

		public override void ProcessFrame(global::UnityEngine.Playables.Playable playable, global::UnityEngine.Playables.FrameData info, object playerData)
		{
			if (m_BoundGameObject == null)
			{
				m_BoundGameObject = playerData as global::UnityEngine.GameObject;
				m_BoundGameObjectInitialStateIsActive = m_BoundGameObject != null && m_BoundGameObject.activeSelf;
			}
			if (m_BoundGameObject == null)
			{
				return;
			}
			int inputCount = global::UnityEngine.Playables.PlayableExtensions.GetInputCount(playable);
			bool active = false;
			for (int i = 0; i < inputCount; i++)
			{
				if (global::UnityEngine.Playables.PlayableExtensions.GetInputWeight(playable, i) > 0f)
				{
					active = true;
					break;
				}
			}
			m_BoundGameObject.SetActive(active);
		}
	}
}
