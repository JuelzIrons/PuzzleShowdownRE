namespace UnityEngine.Timeline
{
	internal interface ICurvesOwner
	{
		global::UnityEngine.AnimationClip curves { get; }

		bool hasCurves { get; }

		double duration { get; }

		string defaultCurvesName { get; }

		global::UnityEngine.Object asset { get; }

		global::UnityEngine.Object assetOwner { get; }

		global::UnityEngine.Timeline.TrackAsset targetTrack { get; }

		void CreateCurves(string curvesClipName);
	}
}
