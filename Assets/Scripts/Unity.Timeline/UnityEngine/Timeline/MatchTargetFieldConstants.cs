namespace UnityEngine.Timeline
{
	internal static class MatchTargetFieldConstants
	{
		public static global::UnityEngine.Timeline.MatchTargetFields All = global::UnityEngine.Timeline.MatchTargetFields.PositionX | global::UnityEngine.Timeline.MatchTargetFields.PositionY | global::UnityEngine.Timeline.MatchTargetFields.PositionZ | global::UnityEngine.Timeline.MatchTargetFields.RotationX | global::UnityEngine.Timeline.MatchTargetFields.RotationY | global::UnityEngine.Timeline.MatchTargetFields.RotationZ;

		public static global::UnityEngine.Timeline.MatchTargetFields None = (global::UnityEngine.Timeline.MatchTargetFields)0;

		public static global::UnityEngine.Timeline.MatchTargetFields Position = global::UnityEngine.Timeline.MatchTargetFields.PositionX | global::UnityEngine.Timeline.MatchTargetFields.PositionY | global::UnityEngine.Timeline.MatchTargetFields.PositionZ;

		public static global::UnityEngine.Timeline.MatchTargetFields Rotation = global::UnityEngine.Timeline.MatchTargetFields.RotationX | global::UnityEngine.Timeline.MatchTargetFields.RotationY | global::UnityEngine.Timeline.MatchTargetFields.RotationZ;

		public static bool HasAny(this global::UnityEngine.Timeline.MatchTargetFields me, global::UnityEngine.Timeline.MatchTargetFields fields)
		{
			return (me & fields) != None;
		}

		public static global::UnityEngine.Timeline.MatchTargetFields Toggle(this global::UnityEngine.Timeline.MatchTargetFields me, global::UnityEngine.Timeline.MatchTargetFields flag)
		{
			return me ^ flag;
		}
	}
}
