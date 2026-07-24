namespace UnityEngine.Timeline
{
	[global::System.Serializable]
	public class AudioPlayableAsset : global::UnityEngine.Playables.PlayableAsset, global::UnityEngine.Timeline.ITimelineClipAsset
	{
		[global::UnityEngine.SerializeField]
		private global::UnityEngine.AudioClip m_Clip;

		[global::UnityEngine.SerializeField]
		private bool m_Loop;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.HideInInspector]
		private float m_bufferingTime = 0.1f;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Timeline.AudioClipProperties m_ClipProperties = new global::UnityEngine.Timeline.AudioClipProperties();

		internal float bufferingTime
		{
			get
			{
				return m_bufferingTime;
			}
			set
			{
				m_bufferingTime = value;
			}
		}

		public global::UnityEngine.AudioClip clip
		{
			get
			{
				return m_Clip;
			}
			set
			{
				m_Clip = value;
			}
		}

		public bool loop
		{
			get
			{
				return m_Loop;
			}
			set
			{
				m_Loop = value;
			}
		}

		public override double duration
		{
			get
			{
				if (m_Clip == null)
				{
					return base.duration;
				}
				return (double)m_Clip.samples / (double)m_Clip.frequency;
			}
		}

		public override global::System.Collections.Generic.IEnumerable<global::UnityEngine.Playables.PlayableBinding> outputs
		{
			get
			{
				yield return global::UnityEngine.Audio.AudioPlayableBinding.Create(base.name, this);
			}
		}

		public global::UnityEngine.Timeline.ClipCaps clipCaps => (global::UnityEngine.Timeline.ClipCaps)(0x1C | (m_Loop ? 1 : 0));

		public override global::UnityEngine.Playables.Playable CreatePlayable(global::UnityEngine.Playables.PlayableGraph graph, global::UnityEngine.GameObject go)
		{
			if (m_Clip == null)
			{
				return global::UnityEngine.Playables.Playable.Null;
			}
			global::UnityEngine.Audio.AudioClipPlayable audioClipPlayable = global::UnityEngine.Audio.AudioClipPlayable.Create(graph, m_Clip, m_Loop);
			audioClipPlayable.GetHandle().SetScriptInstance(m_ClipProperties.Clone());
			return audioClipPlayable;
		}
	}
}
