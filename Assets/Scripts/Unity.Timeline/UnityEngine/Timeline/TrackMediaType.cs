namespace UnityEngine.Timeline
{
	[global::System.AttributeUsage(global::System.AttributeTargets.Class)]
	[global::System.Obsolete("TrackMediaType has been deprecated. It is no longer required, and will be removed in a future release.", false)]
	public class TrackMediaType : global::System.Attribute
	{
		public readonly global::UnityEngine.Timeline.TimelineAsset.MediaType m_MediaType;

		public TrackMediaType(global::UnityEngine.Timeline.TimelineAsset.MediaType mt)
		{
			m_MediaType = mt;
		}
	}
}
