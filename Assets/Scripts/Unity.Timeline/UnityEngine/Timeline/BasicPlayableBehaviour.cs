namespace UnityEngine.Timeline
{
	[global::System.Serializable]
	[global::System.Obsolete("For best performance use PlayableAsset and PlayableBehaviour.")]
	public class BasicPlayableBehaviour : global::UnityEngine.ScriptableObject, global::UnityEngine.Playables.IPlayableAsset, global::UnityEngine.Playables.IPlayableBehaviour
	{
		public virtual double duration => global::UnityEngine.Playables.PlayableBinding.DefaultDuration;

		public virtual global::System.Collections.Generic.IEnumerable<global::UnityEngine.Playables.PlayableBinding> outputs => global::UnityEngine.Playables.PlayableBinding.None;

		public virtual void OnGraphStart(global::UnityEngine.Playables.Playable playable)
		{
		}

		public virtual void OnGraphStop(global::UnityEngine.Playables.Playable playable)
		{
		}

		public virtual void OnPlayableCreate(global::UnityEngine.Playables.Playable playable)
		{
		}

		public virtual void OnPlayableDestroy(global::UnityEngine.Playables.Playable playable)
		{
		}

		public virtual void OnBehaviourPlay(global::UnityEngine.Playables.Playable playable, global::UnityEngine.Playables.FrameData info)
		{
		}

		public virtual void OnBehaviourPause(global::UnityEngine.Playables.Playable playable, global::UnityEngine.Playables.FrameData info)
		{
		}

		public virtual void PrepareFrame(global::UnityEngine.Playables.Playable playable, global::UnityEngine.Playables.FrameData info)
		{
		}

		public virtual void ProcessFrame(global::UnityEngine.Playables.Playable playable, global::UnityEngine.Playables.FrameData info, object playerData)
		{
		}

		public virtual global::UnityEngine.Playables.Playable CreatePlayable(global::UnityEngine.Playables.PlayableGraph graph, global::UnityEngine.GameObject owner)
		{
			return global::UnityEngine.Playables.ScriptPlayable<global::UnityEngine.Timeline.BasicPlayableBehaviour>.Create(graph, this);
		}
	}
}
