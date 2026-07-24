namespace UnityEngine.Timeline
{
	public static class TimelineClipExtensions
	{
		private static readonly string k_UndoSetParentTrackText = "Move Clip";

		public static void MoveToTrack(this global::UnityEngine.Timeline.TimelineClip clip, global::UnityEngine.Timeline.TrackAsset destinationTrack)
		{
			if (clip == null)
			{
				throw new global::System.ArgumentNullException("'this' argument for MoveToTrack cannot be null.");
			}
			if (destinationTrack == null)
			{
				throw new global::System.ArgumentNullException("Cannot move TimelineClip to a null track.");
			}
			global::UnityEngine.Timeline.TrackAsset parentTrack = clip.GetParentTrack();
			global::UnityEngine.Object asset = clip.asset;
			if (asset == null)
			{
				throw new global::System.InvalidOperationException("Cannot move a TimelineClip to a different track if the TimelineClip's PlayableAsset is null.");
			}
			if (parentTrack == destinationTrack)
			{
				throw new global::System.InvalidOperationException("TimelineClip is already on " + destinationTrack.name + ".");
			}
			if (!destinationTrack.ValidateClipType(asset.GetType()))
			{
				throw new global::System.InvalidOperationException("Track " + destinationTrack.name + " cannot contain clips of type " + clip.GetType().Name + ".");
			}
			MoveToTrack_Impl(clip, destinationTrack, asset, parentTrack);
		}

		public static bool TryMoveToTrack(this global::UnityEngine.Timeline.TimelineClip clip, global::UnityEngine.Timeline.TrackAsset destinationTrack)
		{
			if (clip == null)
			{
				throw new global::System.ArgumentNullException("'this' argument for TryMoveToTrack cannot be null.");
			}
			if (destinationTrack == null)
			{
				throw new global::System.ArgumentNullException("Cannot move TimelineClip to a null parent.");
			}
			global::UnityEngine.Timeline.TrackAsset parentTrack = clip.GetParentTrack();
			global::UnityEngine.Object asset = clip.asset;
			if (asset == null)
			{
				return false;
			}
			if (parentTrack != destinationTrack)
			{
				if (!destinationTrack.ValidateClipType(asset.GetType()))
				{
					return false;
				}
				MoveToTrack_Impl(clip, destinationTrack, asset, parentTrack);
				return true;
			}
			return false;
		}

		private static void MoveToTrack_Impl(global::UnityEngine.Timeline.TimelineClip clip, global::UnityEngine.Timeline.TrackAsset destinationTrack, global::UnityEngine.Object asset, global::UnityEngine.Timeline.TrackAsset parentTrack)
		{
			_ = parentTrack != null;
			clip.SetParentTrack_Internal(destinationTrack);
			if (parentTrack == null)
			{
				global::UnityEngine.Timeline.TimelineCreateUtilities.SaveAssetIntoObject(asset, destinationTrack);
			}
			else if (parentTrack.timelineAsset != destinationTrack.timelineAsset)
			{
				global::UnityEngine.Timeline.TimelineCreateUtilities.RemoveAssetFromObject(asset, parentTrack);
				global::UnityEngine.Timeline.TimelineCreateUtilities.SaveAssetIntoObject(asset, destinationTrack);
			}
		}
	}
}
