namespace UnityEngine.Timeline
{
	[global::System.Serializable]
	public class PlayableTrack : global::UnityEngine.Timeline.TrackAsset
	{
		protected override void OnCreateClip(global::UnityEngine.Timeline.TimelineClip clip)
		{
			if (clip.asset != null)
			{
				clip.displayName = clip.asset.GetType().Name;
			}
		}
	}
}
