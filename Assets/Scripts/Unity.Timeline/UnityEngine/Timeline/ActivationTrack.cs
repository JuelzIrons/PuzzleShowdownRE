namespace UnityEngine.Timeline
{
	[global::System.Serializable]
	[global::UnityEngine.Timeline.TrackClipType(typeof(global::UnityEngine.Timeline.ActivationPlayableAsset))]
	[global::UnityEngine.Timeline.TrackBindingType(typeof(global::UnityEngine.GameObject))]
	[global::UnityEngine.ExcludeFromPreset]
	public class ActivationTrack : global::UnityEngine.Timeline.TrackAsset
	{
		public enum PostPlaybackState
		{
			Active = 0,
			Inactive = 1,
			Revert = 2,
			LeaveAsIs = 3
		}

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Timeline.ActivationTrack.PostPlaybackState m_PostPlaybackState = global::UnityEngine.Timeline.ActivationTrack.PostPlaybackState.LeaveAsIs;

		private global::UnityEngine.Timeline.ActivationMixerPlayable m_ActivationMixer;

		public global::UnityEngine.Timeline.ActivationTrack.PostPlaybackState postPlaybackState
		{
			get
			{
				return m_PostPlaybackState;
			}
			set
			{
				m_PostPlaybackState = value;
				UpdateTrackMode();
			}
		}

		internal override bool CanCompileClips()
		{
			if (base.hasClips)
			{
				return base.CanCompileClips();
			}
			return true;
		}

		public override global::UnityEngine.Playables.Playable CreateTrackMixer(global::UnityEngine.Playables.PlayableGraph graph, global::UnityEngine.GameObject go, int inputCount)
		{
			global::UnityEngine.Playables.ScriptPlayable<global::UnityEngine.Timeline.ActivationMixerPlayable> scriptPlayable = global::UnityEngine.Timeline.ActivationMixerPlayable.Create(graph, inputCount);
			m_ActivationMixer = scriptPlayable.GetBehaviour();
			UpdateTrackMode();
			return scriptPlayable;
		}

		internal void UpdateTrackMode()
		{
			if (m_ActivationMixer != null)
			{
				m_ActivationMixer.postPlaybackState = m_PostPlaybackState;
			}
		}

		public override void GatherProperties(global::UnityEngine.Playables.PlayableDirector director, global::UnityEngine.Timeline.IPropertyCollector driver)
		{
			global::UnityEngine.GameObject gameObjectBinding = GetGameObjectBinding(director);
			if (gameObjectBinding != null)
			{
				driver.AddFromName(gameObjectBinding, "m_IsActive");
			}
		}

		protected override void OnCreateClip(global::UnityEngine.Timeline.TimelineClip clip)
		{
			clip.displayName = "Active";
			base.OnCreateClip(clip);
		}
	}
}
