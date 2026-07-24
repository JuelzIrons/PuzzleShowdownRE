namespace UnityEngine.Timeline
{
	internal static class BlendUtility
	{
		private static bool Overlaps(global::UnityEngine.Timeline.TimelineClip blendOut, global::UnityEngine.Timeline.TimelineClip blendIn)
		{
			if (blendIn == blendOut)
			{
				return false;
			}
			if (global::System.Math.Abs(blendIn.start - blendOut.start) < global::UnityEngine.Timeline.TimeUtility.kTimeEpsilon)
			{
				return blendIn.duration >= blendOut.duration;
			}
			if (blendIn.start >= blendOut.start)
			{
				return blendIn.start < blendOut.end;
			}
			return false;
		}

		public static void ComputeBlendsFromOverlaps(global::UnityEngine.Timeline.TimelineClip[] clips)
		{
			foreach (global::UnityEngine.Timeline.TimelineClip obj in clips)
			{
				obj.blendInDuration = -1.0;
				obj.blendOutDuration = -1.0;
			}
			global::System.Array.Sort(clips, (global::UnityEngine.Timeline.TimelineClip c1, global::UnityEngine.Timeline.TimelineClip c2) => (!(global::System.Math.Abs(c1.start - c2.start) < global::UnityEngine.Timeline.TimeUtility.kTimeEpsilon)) ? c1.start.CompareTo(c2.start) : c1.duration.CompareTo(c2.duration));
			for (int num = 0; num < clips.Length; num++)
			{
				global::UnityEngine.Timeline.TimelineClip timelineClip = clips[num];
				if (timelineClip.SupportsBlending())
				{
					global::UnityEngine.Timeline.TimelineClip timelineClip2 = timelineClip;
					global::UnityEngine.Timeline.TimelineClip timelineClip3 = null;
					global::UnityEngine.Timeline.TimelineClip timelineClip4 = clips[global::System.Math.Max(num - 1, 0)];
					if (Overlaps(timelineClip4, timelineClip2))
					{
						timelineClip3 = timelineClip4;
					}
					if (timelineClip3 != null)
					{
						UpdateClipIntersection(timelineClip3, timelineClip2);
					}
				}
			}
		}

		private static void UpdateClipIntersection(global::UnityEngine.Timeline.TimelineClip blendOutClip, global::UnityEngine.Timeline.TimelineClip blendInClip)
		{
			if (blendOutClip.SupportsBlending() && blendInClip.SupportsBlending() && !(blendInClip.start - blendOutClip.start < blendOutClip.duration - blendInClip.duration))
			{
				double blendInDuration = (blendOutClip.blendOutDuration = global::System.Math.Max(0.0, blendOutClip.start + blendOutClip.duration - blendInClip.start));
				blendInClip.blendInDuration = blendInDuration;
				global::UnityEngine.Timeline.TimelineClip.BlendCurveMode blendInCurveMode = blendInClip.blendInCurveMode;
				global::UnityEngine.Timeline.TimelineClip.BlendCurveMode blendOutCurveMode = blendOutClip.blendOutCurveMode;
				if (blendInCurveMode == global::UnityEngine.Timeline.TimelineClip.BlendCurveMode.Manual && blendOutCurveMode == global::UnityEngine.Timeline.TimelineClip.BlendCurveMode.Auto)
				{
					blendOutClip.mixOutCurve = global::UnityEngine.Timeline.CurveEditUtility.CreateMatchingCurve(blendInClip.mixInCurve);
				}
				else if (blendInCurveMode == global::UnityEngine.Timeline.TimelineClip.BlendCurveMode.Auto && blendOutCurveMode == global::UnityEngine.Timeline.TimelineClip.BlendCurveMode.Manual)
				{
					blendInClip.mixInCurve = global::UnityEngine.Timeline.CurveEditUtility.CreateMatchingCurve(blendOutClip.mixOutCurve);
				}
				else if (blendInCurveMode == global::UnityEngine.Timeline.TimelineClip.BlendCurveMode.Auto && blendOutCurveMode == global::UnityEngine.Timeline.TimelineClip.BlendCurveMode.Auto)
				{
					blendInClip.mixInCurve = null;
					blendOutClip.mixOutCurve = null;
				}
			}
		}
	}
}
