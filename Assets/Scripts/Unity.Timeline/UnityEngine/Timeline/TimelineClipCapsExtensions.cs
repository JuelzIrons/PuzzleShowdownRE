namespace UnityEngine.Timeline
{
	internal static class TimelineClipCapsExtensions
	{
		public static bool SupportsLooping(this global::UnityEngine.Timeline.TimelineClip clip)
		{
			if (clip != null)
			{
				return (clip.clipCaps & global::UnityEngine.Timeline.ClipCaps.Looping) != 0;
			}
			return false;
		}

		public static bool SupportsExtrapolation(this global::UnityEngine.Timeline.TimelineClip clip)
		{
			if (clip != null)
			{
				return (clip.clipCaps & global::UnityEngine.Timeline.ClipCaps.Extrapolation) != 0;
			}
			return false;
		}

		public static bool SupportsClipIn(this global::UnityEngine.Timeline.TimelineClip clip)
		{
			if (clip != null)
			{
				return (clip.clipCaps & global::UnityEngine.Timeline.ClipCaps.ClipIn) != 0;
			}
			return false;
		}

		public static bool SupportsSpeedMultiplier(this global::UnityEngine.Timeline.TimelineClip clip)
		{
			if (clip != null)
			{
				return (clip.clipCaps & global::UnityEngine.Timeline.ClipCaps.SpeedMultiplier) != 0;
			}
			return false;
		}

		public static bool SupportsBlending(this global::UnityEngine.Timeline.TimelineClip clip)
		{
			if (clip != null)
			{
				return (clip.clipCaps & global::UnityEngine.Timeline.ClipCaps.Blending) != 0;
			}
			return false;
		}

		public static bool HasAll(this global::UnityEngine.Timeline.ClipCaps caps, global::UnityEngine.Timeline.ClipCaps flags)
		{
			return (caps & flags) == flags;
		}

		public static bool HasAny(this global::UnityEngine.Timeline.ClipCaps caps, global::UnityEngine.Timeline.ClipCaps flags)
		{
			return (caps & flags) != 0;
		}
	}
}
