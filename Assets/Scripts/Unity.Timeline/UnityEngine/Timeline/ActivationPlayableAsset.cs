namespace UnityEngine.Timeline
{
	internal class ActivationPlayableAsset : global::UnityEngine.Playables.PlayableAsset, global::UnityEngine.Timeline.ITimelineClipAsset
	{
		public global::UnityEngine.Timeline.ClipCaps clipCaps => global::UnityEngine.Timeline.ClipCaps.None;

		public override global::UnityEngine.Playables.Playable CreatePlayable(global::UnityEngine.Playables.PlayableGraph graph, global::UnityEngine.GameObject go)
		{
			return global::UnityEngine.Playables.Playable.Create(graph);
		}
	}
}
