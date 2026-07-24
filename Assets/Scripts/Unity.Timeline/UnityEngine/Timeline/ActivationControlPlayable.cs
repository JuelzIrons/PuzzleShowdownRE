namespace UnityEngine.Timeline
{
	public class ActivationControlPlayable : global::UnityEngine.Playables.PlayableBehaviour
	{
		public enum PostPlaybackState
		{
			Active = 0,
			Inactive = 1,
			Revert = 2
		}

		private enum InitialState
		{
			Unset = 0,
			Active = 1,
			Inactive = 2
		}

		public global::UnityEngine.GameObject gameObject;

		public global::UnityEngine.Timeline.ActivationControlPlayable.PostPlaybackState postPlayback = global::UnityEngine.Timeline.ActivationControlPlayable.PostPlaybackState.Revert;

		private global::UnityEngine.Timeline.ActivationControlPlayable.InitialState m_InitialState;

		public static global::UnityEngine.Playables.ScriptPlayable<global::UnityEngine.Timeline.ActivationControlPlayable> Create(global::UnityEngine.Playables.PlayableGraph graph, global::UnityEngine.GameObject gameObject, global::UnityEngine.Timeline.ActivationControlPlayable.PostPlaybackState postPlaybackState)
		{
			if (gameObject == null)
			{
				return global::UnityEngine.Playables.ScriptPlayable<global::UnityEngine.Timeline.ActivationControlPlayable>.Null;
			}
			global::UnityEngine.Playables.ScriptPlayable<global::UnityEngine.Timeline.ActivationControlPlayable> result = global::UnityEngine.Playables.ScriptPlayable<global::UnityEngine.Timeline.ActivationControlPlayable>.Create(graph);
			global::UnityEngine.Timeline.ActivationControlPlayable behaviour = result.GetBehaviour();
			behaviour.gameObject = gameObject;
			behaviour.postPlayback = postPlaybackState;
			return result;
		}

		public override void OnBehaviourPlay(global::UnityEngine.Playables.Playable playable, global::UnityEngine.Playables.FrameData info)
		{
			if (!(gameObject == null))
			{
				gameObject.SetActive(value: true);
			}
		}

		public override void OnBehaviourPause(global::UnityEngine.Playables.Playable playable, global::UnityEngine.Playables.FrameData info)
		{
			if (gameObject != null && info.effectivePlayState == global::UnityEngine.Playables.PlayState.Paused)
			{
				gameObject.SetActive(value: false);
			}
		}

		public override void ProcessFrame(global::UnityEngine.Playables.Playable playable, global::UnityEngine.Playables.FrameData info, object userData)
		{
			if (gameObject != null)
			{
				gameObject.SetActive(value: true);
			}
		}

		public override void OnGraphStart(global::UnityEngine.Playables.Playable playable)
		{
			if (gameObject != null && m_InitialState == global::UnityEngine.Timeline.ActivationControlPlayable.InitialState.Unset)
			{
				m_InitialState = (gameObject.activeSelf ? global::UnityEngine.Timeline.ActivationControlPlayable.InitialState.Active : global::UnityEngine.Timeline.ActivationControlPlayable.InitialState.Inactive);
			}
		}

		public override void OnPlayableDestroy(global::UnityEngine.Playables.Playable playable)
		{
			if (!(gameObject == null) && m_InitialState != global::UnityEngine.Timeline.ActivationControlPlayable.InitialState.Unset)
			{
				switch (postPlayback)
				{
				case global::UnityEngine.Timeline.ActivationControlPlayable.PostPlaybackState.Active:
					gameObject.SetActive(value: true);
					break;
				case global::UnityEngine.Timeline.ActivationControlPlayable.PostPlaybackState.Inactive:
					gameObject.SetActive(value: false);
					break;
				case global::UnityEngine.Timeline.ActivationControlPlayable.PostPlaybackState.Revert:
					gameObject.SetActive(m_InitialState == global::UnityEngine.Timeline.ActivationControlPlayable.InitialState.Active);
					break;
				}
			}
		}
	}
}
