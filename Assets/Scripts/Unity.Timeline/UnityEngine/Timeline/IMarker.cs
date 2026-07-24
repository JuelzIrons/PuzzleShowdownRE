namespace UnityEngine.Timeline
{
	public interface IMarker
	{
		double time { get; set; }

		global::UnityEngine.Timeline.TrackAsset parent { get; }

		void Initialize(global::UnityEngine.Timeline.TrackAsset parent);
	}
}
