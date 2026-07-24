namespace UnityEngine.Timeline
{
	[global::System.Serializable]
	[global::UnityEngine.Timeline.TrackClipType(typeof(global::UnityEngine.Timeline.AudioPlayableAsset), false)]
	[global::UnityEngine.Timeline.TrackBindingType(typeof(global::UnityEngine.AudioSource))]
	[global::UnityEngine.ExcludeFromPreset]
	public class AudioTrack : global::UnityEngine.Timeline.TrackAsset
	{
		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Timeline.AudioMixerProperties m_TrackProperties = new global::UnityEngine.Timeline.AudioMixerProperties();

		public override global::System.Collections.Generic.IEnumerable<global::UnityEngine.Playables.PlayableBinding> outputs
		{
			get
			{
				yield return global::UnityEngine.Audio.AudioPlayableBinding.Create(base.name, this);
			}
		}

		public global::UnityEngine.Timeline.TimelineClip CreateClip(global::UnityEngine.AudioClip clip)
		{
			if (clip == null)
			{
				return null;
			}
			global::UnityEngine.Timeline.TimelineClip timelineClip = CreateDefaultClip();
			global::UnityEngine.Timeline.AudioPlayableAsset audioPlayableAsset = timelineClip.asset as global::UnityEngine.Timeline.AudioPlayableAsset;
			if (audioPlayableAsset != null)
			{
				audioPlayableAsset.clip = clip;
			}
			timelineClip.duration = clip.length;
			timelineClip.displayName = clip.name;
			return timelineClip;
		}

		internal override global::UnityEngine.Playables.Playable CompileClips(global::UnityEngine.Playables.PlayableGraph graph, global::UnityEngine.GameObject go, global::System.Collections.Generic.IList<global::UnityEngine.Timeline.TimelineClip> timelineClips, global::UnityEngine.Timeline.IntervalTree<global::UnityEngine.Timeline.RuntimeElement> tree)
		{
			global::UnityEngine.Audio.AudioMixerPlayable audioMixerPlayable = global::UnityEngine.Audio.AudioMixerPlayable.Create(graph, timelineClips.Count);
			if (base.hasCurves)
			{
				audioMixerPlayable.GetHandle().SetScriptInstance(m_TrackProperties.Clone());
			}
			for (int i = 0; i < timelineClips.Count; i++)
			{
				global::UnityEngine.Timeline.TimelineClip timelineClip = timelineClips[i];
				global::UnityEngine.Playables.PlayableAsset playableAsset = timelineClip.asset as global::UnityEngine.Playables.PlayableAsset;
				if (playableAsset == null)
				{
					continue;
				}
				float num = 0.1f;
				global::UnityEngine.Timeline.AudioPlayableAsset audioPlayableAsset = timelineClip.asset as global::UnityEngine.Timeline.AudioPlayableAsset;
				if (audioPlayableAsset != null)
				{
					num = audioPlayableAsset.bufferingTime;
				}
				global::UnityEngine.Playables.Playable playable = playableAsset.CreatePlayable(graph, go);
				if (global::UnityEngine.Playables.PlayableExtensions.IsValid(playable))
				{
					if (playable.IsPlayableOfType<global::UnityEngine.Audio.AudioClipPlayable>())
					{
						global::UnityEngine.Audio.AudioClipPlayable audioClipPlayable = (global::UnityEngine.Audio.AudioClipPlayable)playable;
						global::UnityEngine.Timeline.AudioClipProperties audioClipProperties = audioClipPlayable.GetHandle().GetObject<global::UnityEngine.Timeline.AudioClipProperties>();
						audioClipPlayable.SetVolume(global::UnityEngine.Mathf.Clamp01(m_TrackProperties.volume * audioClipProperties.volume));
						audioClipPlayable.SetStereoPan(global::UnityEngine.Mathf.Clamp(m_TrackProperties.stereoPan, -1f, 1f));
						audioClipPlayable.SetSpatialBlend(global::UnityEngine.Mathf.Clamp01(m_TrackProperties.spatialBlend));
					}
					tree.Add(new global::UnityEngine.Timeline.ScheduleRuntimeClip(timelineClip, playable, audioMixerPlayable, num));
					graph.Connect(playable, 0, audioMixerPlayable, i);
					global::UnityEngine.Playables.PlayableExtensions.SetSpeed(playable, timelineClip.timeScale);
					global::UnityEngine.Playables.PlayableExtensions.SetDuration(playable, timelineClip.extrapolatedDuration);
					global::UnityEngine.Playables.PlayableExtensions.SetInputWeight(audioMixerPlayable, playable, 1f);
				}
			}
			ConfigureTrackAnimation(tree, go, audioMixerPlayable);
			return audioMixerPlayable;
		}

		private void OnValidate()
		{
			m_TrackProperties.volume = global::UnityEngine.Mathf.Clamp01(m_TrackProperties.volume);
			m_TrackProperties.stereoPan = global::UnityEngine.Mathf.Clamp(m_TrackProperties.stereoPan, -1f, 1f);
			m_TrackProperties.spatialBlend = global::UnityEngine.Mathf.Clamp01(m_TrackProperties.spatialBlend);
		}
	}
}
