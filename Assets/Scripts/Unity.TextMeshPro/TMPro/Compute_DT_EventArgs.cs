namespace TMPro
{
	public class Compute_DT_EventArgs
	{
		public global::TMPro.Compute_DistanceTransform_EventTypes EventType;

		public float ProgressPercentage;

		public global::UnityEngine.Color[] Colors;

		public Compute_DT_EventArgs(global::TMPro.Compute_DistanceTransform_EventTypes type, float progress)
		{
			EventType = type;
			ProgressPercentage = progress;
		}

		public Compute_DT_EventArgs(global::TMPro.Compute_DistanceTransform_EventTypes type, global::UnityEngine.Color[] colors)
		{
			EventType = type;
			Colors = colors;
		}
	}
}
