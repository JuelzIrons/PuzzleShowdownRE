namespace UnityEngine.Timeline
{
	[global::System.Serializable]
	internal class AudioMixerProperties : global::UnityEngine.Playables.PlayableBehaviour
	{
		[global::UnityEngine.Range(0f, 1f)]
		public float volume = 1f;

		[global::UnityEngine.Range(-1f, 1f)]
		public float stereoPan;

		[global::UnityEngine.Range(0f, 1f)]
		public float spatialBlend;

		public override void PrepareFrame(global::UnityEngine.Playables.Playable playable, global::UnityEngine.Playables.FrameData info)
		{
			if (!global::UnityEngine.Playables.PlayableExtensions.IsValid(playable) || !playable.IsPlayableOfType<global::UnityEngine.Audio.AudioMixerPlayable>())
			{
				return;
			}
			int inputCount = global::UnityEngine.Playables.PlayableExtensions.GetInputCount(playable);
			for (int i = 0; i < inputCount; i++)
			{
				if (global::UnityEngine.Playables.PlayableExtensions.GetInputWeight(playable, i) > 0f)
				{
					global::UnityEngine.Playables.Playable input = global::UnityEngine.Playables.PlayableExtensions.GetInput(playable, i);
					if (global::UnityEngine.Playables.PlayableExtensions.IsValid(input) && input.IsPlayableOfType<global::UnityEngine.Audio.AudioClipPlayable>())
					{
						global::UnityEngine.Audio.AudioClipPlayable audioClipPlayable = (global::UnityEngine.Audio.AudioClipPlayable)input;
						global::UnityEngine.Timeline.AudioClipProperties audioClipProperties = input.GetHandle().GetObject<global::UnityEngine.Timeline.AudioClipProperties>();
						audioClipPlayable.SetVolume(global::UnityEngine.Mathf.Clamp01(volume * audioClipProperties.volume));
						audioClipPlayable.SetStereoPan(global::UnityEngine.Mathf.Clamp(stereoPan, -1f, 1f));
						audioClipPlayable.SetSpatialBlend(global::UnityEngine.Mathf.Clamp01(spatialBlend));
					}
				}
			}
		}
	}
}
