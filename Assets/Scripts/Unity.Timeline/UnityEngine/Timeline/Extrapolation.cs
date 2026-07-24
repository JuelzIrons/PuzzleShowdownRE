namespace UnityEngine.Timeline
{
	internal static class Extrapolation
	{
		internal static readonly double kMinExtrapolationTime = global::UnityEngine.Timeline.TimeUtility.kTimeEpsilon * 1000.0;

		internal static void CalculateExtrapolationTimes(this global::UnityEngine.Timeline.TrackAsset asset)
		{
			global::UnityEngine.Timeline.TimelineClip[] clips = asset.clips;
			if (clips == null || clips.Length == 0 || !clips[0].SupportsExtrapolation())
			{
				return;
			}
			global::UnityEngine.Timeline.TimelineClip[] array = SortClipsByStartTime(clips);
			if (array.Length == 0)
			{
				return;
			}
			for (int i = 0; i < array.Length; i++)
			{
				double num = double.PositiveInfinity;
				for (int j = 0; j < array.Length; j++)
				{
					if (i != j)
					{
						double num2 = array[j].start - array[i].end;
						if (num2 >= 0.0 - global::UnityEngine.Timeline.TimeUtility.kTimeEpsilon && num2 < num)
						{
							num = global::System.Math.Min(num, num2);
						}
						if (array[j].start <= array[i].end && array[j].end > array[i].end)
						{
							num = 0.0;
						}
					}
				}
				num = ((num <= kMinExtrapolationTime) ? 0.0 : num);
				array[i].SetPostExtrapolationTime(num);
			}
			array[0].SetPreExtrapolationTime(global::System.Math.Max(0.0, array[0].start));
			for (int k = 1; k < array.Length; k++)
			{
				double num3 = 0.0;
				int num4 = -1;
				for (int l = 0; l < k; l++)
				{
					if (array[l].end > array[k].start)
					{
						num4 = -1;
						num3 = 0.0;
						break;
					}
					double num5 = array[k].start - array[l].end;
					if (num4 == -1 || num5 < num3)
					{
						num3 = num5;
						num4 = l;
					}
				}
				if (num4 >= 0 && array[num4].postExtrapolationMode != global::UnityEngine.Timeline.TimelineClip.ClipExtrapolation.None)
				{
					num3 = 0.0;
				}
				num3 = ((num3 <= kMinExtrapolationTime) ? 0.0 : num3);
				array[k].SetPreExtrapolationTime(num3);
			}
		}

		private static global::UnityEngine.Timeline.TimelineClip[] SortClipsByStartTime(global::UnityEngine.Timeline.TimelineClip[] clips)
		{
			global::UnityEngine.Timeline.TimelineClip[] array = new global::UnityEngine.Timeline.TimelineClip[clips.Length];
			global::System.Array.Copy(clips, array, clips.Length);
			global::System.Array.Sort(array, (global::UnityEngine.Timeline.TimelineClip clip1, global::UnityEngine.Timeline.TimelineClip clip2) => clip1.start.CompareTo(clip2.start));
			return array;
		}
	}
}
