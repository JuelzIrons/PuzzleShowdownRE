namespace UnityEngine.Timeline
{
	[global::System.Serializable]
	[global::UnityEngine.Timeline.TrackClipType(typeof(global::UnityEngine.Timeline.TrackAsset))]
	[global::UnityEngine.Timeline.SupportsChildTracks(null, int.MaxValue)]
	[global::UnityEngine.ExcludeFromPreset]
	public class GroupTrack : global::UnityEngine.Timeline.TrackAsset
	{
		public override global::System.Collections.Generic.IEnumerable<global::UnityEngine.Playables.PlayableBinding> outputs => global::UnityEngine.Playables.PlayableBinding.None;

		internal override bool CanCompileClips()
		{
			return false;
		}
	}
}
