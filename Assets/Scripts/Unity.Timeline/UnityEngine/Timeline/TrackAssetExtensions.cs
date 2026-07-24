namespace UnityEngine.Timeline
{
	public static class TrackAssetExtensions
	{
		public static global::UnityEngine.Timeline.GroupTrack GetGroup(this global::UnityEngine.Timeline.TrackAsset asset)
		{
			if (asset == null)
			{
				return null;
			}
			return asset.parent as global::UnityEngine.Timeline.GroupTrack;
		}

		public static void SetGroup(this global::UnityEngine.Timeline.TrackAsset asset, global::UnityEngine.Timeline.GroupTrack group)
		{
			if (asset == null || asset == group || asset.parent == group)
			{
				return;
			}
			if (group != null && asset.timelineAsset != group.timelineAsset)
			{
				throw new global::System.InvalidOperationException("Cannot assign to a group in a different timeline");
			}
			global::UnityEngine.Timeline.TimelineAsset timelineAsset = asset.timelineAsset;
			global::UnityEngine.Timeline.TrackAsset trackAsset = asset.parent as global::UnityEngine.Timeline.TrackAsset;
			global::UnityEngine.Timeline.TimelineAsset timelineAsset2 = asset.parent as global::UnityEngine.Timeline.TimelineAsset;
			if (trackAsset != null || timelineAsset2 != null)
			{
				if (timelineAsset2 != null)
				{
					timelineAsset2.RemoveTrack(asset);
				}
				else
				{
					trackAsset.RemoveSubTrack(asset);
				}
			}
			if (group == null)
			{
				asset.parent = asset.timelineAsset;
				timelineAsset.AddTrackInternal(asset);
			}
			else
			{
				group.AddChild(asset);
			}
		}

		internal static void ComputeBlendsFromOverlaps(this global::UnityEngine.Timeline.TrackAsset asset, bool force = false)
		{
			if (!asset.blendsValid || force)
			{
				global::UnityEngine.Timeline.BlendUtility.ComputeBlendsFromOverlaps(asset.clips);
				asset.blendsValid = true;
			}
		}
	}
}
